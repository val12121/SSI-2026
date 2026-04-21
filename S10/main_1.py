import random
import math
class Primos:
  def __init__(self, p) :
    self.p = p
    self.primo = False
    
  def Lehman(self, show_= False, t=10) :
  
    lista = [2, 3, 5, 7, 11]
    resultado = []
    
    for i in lista :
      if i == self.p :
        self.primo = True 
        if show_ : self.show()
        return True 
      if (self.p % i) == 0:
        return False
      
    for _ in range (t) :
      a = random.randint(2, self.p - 1)
      res = pow (a, (self.p - 1) // 2, self.p)
      resultado.append(res)
    
    if (self.p - 1) in resultado :
      if show_ == True :
        self.primo = True
        self.show()
      return True
    else :
      if show_ == True :
        self.show()
      return False

  def Euclides(self, d, phi) :
    #Caso base
    if d == 0 :
      return phi, 0 , 1 
    
    mcd, x1, y1 = self.Euclides(phi % d, d)
    
    x = y1 - (phi // d) * x1
    y = x1
    
    return mcd, x, y

  def show(self) : 
    print ('\nRevisamos si el número %d es primo' % (self.p))
    if self.primo == True :
      print ("El número es primo")
    else :
      print ("El número no es primo")

def cifrar_rsa(texto_, e, n, tam_bloque) :
  print ("Función por hacer")
  texto = texto_.upper().replace(" ", "")

  while len(texto) % tam_bloque != 0: 
    texto += "X"
    
  resultado_cifrado = []
  for i in range(0, len(texto), tam_bloque) :
    bloque = texto[i:i+tam_bloque]
    valor = 0
    for pos, letra in enumerate(bloque) :
      valor_letra = cifrado.index(letra)
      exponente = tam_bloque - 1 - pos
      valor += valor_letra * (26 ** exponente)

    # Cifrar: C = M^e mod n (Exponenciación rápida)
    cifra = pow(valor, e, n)
    resultado_cifrado.append(cifra)
    print(f"Bloque '{bloque}' -> Decimal: {valor} -> Cifrado: {cifra}")
  
  return resultado_cifrado

#* Fin de la clase

cifrado = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z']

texto = (input("Introduce el texto a cifrar: "))
p = int(input("Introduce el valor de p: "))
q = int(input("Introduce el valor de q: "))
d = int(input("Introduce el valor de d: "))
p_ = Primos(p)
q_ = Primos(q)

p_.Lehman(True)
q_.Lehman(True)

fun_euler = math.lcm((p - 1), (q - 1))
mcd, e_raw, y = p_.Euclides(d, fun_euler)
e = e_raw % fun_euler
n = p * q 

print ('\nProcedemos a evaluar si los números %d y %d son primos -->' % (d, fun_euler))
if mcd == 1 :
  print ('%d y %d son coprimos' % (d, fun_euler))
else :
  print ('%d y %d NO son coprimos' % (d, fun_euler))

print ("\nEl numero de euler es: %d" % e)

tam_bloque = math.floor(math.log(n, 26))

resultado = cifrar_rsa(texto, e, n, tam_bloque)
