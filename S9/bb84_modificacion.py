import numpy as np
from qiskit import QuantumCircuit, transpile
from qiskit_aer import AerSimulator

n = 10**3
backend = AerSimulator()
message = [QuantumCircuit(1, 1) for _ in range(n)]

alice_bits = np.random.randint(2, size=n)
alice_bases = np.random.randint(2, size=n)

for i, qc in enumerate(message):
  if alice_bits[i] == 1:
    qc.x(0)
  if alice_bases[i] == 1:
    qc.h(0)

entrada = input("Introduce un número p: ")
probabilidad = float(entrada)
print({probabilidad})

eva_bases = np.random.randint(2, size=n)
classical_bit_index_for_eva_measurement = 1

for i, qc in enumerate(message):
  numero = np.random.random()
  if numero < probabilidad:
    if eva_bases[i] == 1:
      qc.h(0)
      qc.measure(0, 0)
    if eva_bases[i] == 1:
      qc.h(0)

# Bob
bob_bases = np.random.randint(2, size=n)

for i, qc in enumerate(message):
  if bob_bases[i] == 1:
    qc.h(0)
  qc.measure(0, 0)

bob_measured_bits = []

# Ejecución
for qc in message:
  transpiled_qc = transpile(qc, backend)
  result = backend.run(transpiled_qc, shots=1).result()
  counts = result.get_counts()
  measured_bit = int(list(counts.keys())[0])
  bob_measured_bits.append(measured_bit)

alice_key = []
bob_key = []

for i in range(n):
  if alice_bases[i] == bob_bases[i]:
    alice_key.append(alice_bits[i])
    bob_key.append(bob_measured_bits[i])

# Cálculo del QBER con Atacante
errors = 0
for a, b in zip(alice_key, bob_key):
  if a != b:
    errors += 1

qber = errors / len(alice_key) if len(alice_key) > 0 else 0

print(f"--- Protocolo BB84 con Atacante (Eva) ---")
print(f"Longitud de la clave filtrada: {len(alice_key)}")
print(f"QBER (Tasa de error detectada): {qber:.4f}")

if qber > 0.1: 
  print("¡ALERTA! Tasa de error elevada. Se ha detectado un intruso (Eva) en el canal.")
else:
  print("Canal seguro.")