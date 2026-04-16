import numpy as np
from qiskit import QuantumCircuit, transpile
from qiskit_aer import AerSimulator

n = 10**3
backend = AerSimulator()

# lista de n circuitos cuánticos (1 cúbit y 1 bit clásico cada uno)
message = [QuantumCircuit(1, 1) for _ in range(n)]

# Alice genera sus bits y bases aleatorias (0=Computacional, 1=Hadamard) 
alice_bits = np.random.randint(2, size=n)
alice_bases = np.random.randint(2, size=n)

for i, qc in enumerate(message):
    if alice_bits[i] == 1: #if bits = 1, puerta X
        qc.x(0)
    if alice_bases[i] == 1: #if base = 1, puerta H
        qc.h(0)

# Bob elige sus propias bases de medida al azar
bob_bases = np.random.randint(2, size=n)

for i, qc in enumerate(message):
    # Si Bob elige la base Hadamard (1), aplica la puerta H antes de medir
    if bob_bases[i] == 1:
        qc.h(0)

for qc in message:
    qc.measure(0, 0)

bob_measured_bits = []

for qc in message:
    transpiled_qc = transpile(qc, backend) 
    result = backend.run(transpiled_qc, shots=1).result() 
    counts = result.get_counts() 
    
    # Extraemos el bit medido (0 o 1) 
    measured_bit = list(counts.keys())[0]
    bob_measured_bits.append(int(measured_bit)) 

alice_key = []
bob_key = []

for i in range(n):
    # Si las bases coinciden, guardamos los bits para la clave 
    if alice_bases[i] == bob_bases[i]:
        alice_key.append(int(alice_bits[i]))
        bob_key.append(int(bob_measured_bits[i])) 

# Comparamos las claves para ver cuántos errores hay 
errors = 0
for i in range(len(alice_key)):
    if alice_key[i] != bob_key[i]:
        errors += 1

qber = errors / len(alice_key) if len(alice_key) > 0 else 0

# Resultados finales
print(f"--- Protocolo BB84 Finalizado ---")
print(f"Bits enviados: {n}")
print(f"Longitud de la clave final: {len(alice_key)}")
print(f"QBER (Tasa de error): {qber}")