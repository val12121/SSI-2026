namespace PracticaAES;
using System;
using System.Text;
using System.Collections.Specialized;

class CBC
{

  static private byte[] original_ = new byte[16];
  static private byte[] key_ = new byte[16];
  static private byte[] bloque_ = new byte[16];
  public static byte[] XorBytes (byte[] a, byte[] b)
  {
    byte[] resultado = new byte[16];
    for (int i = 0; i < 16; i++)
    {
      resultado[i] = (byte)(a[i] ^ b[i]);
    }
    return resultado;
  }

  public static byte[] CBC_method (byte [] original, byte [] key, byte [] bloque)
  {
    byte[] aux;
    aux = XorBytes(original_, bloque_);
    byte[] state = AESProgram.Aes(aux, key_);
    Console.WriteLine($"{BitConverter.ToString(state).Replace("-","")}");
    return state;
  }

  public static byte[] Look (byte[] array, byte[] bloque_ant)
  {
    bool incompleto = false;
    int auxiliar = 0;
    
    while (!incompleto && auxiliar < 15)
    {
      if (array[auxiliar].ToString() == "")
      {
        incompleto = true;
      }
      auxiliar++;
    } 

    Console.WriteLine($"{BitConverter.ToString(array).Replace("-","")}");

    if (auxiliar < 16)
    {
      Console.WriteLine("El número introducido: ");
      Console.WriteLine($"{BitConverter.ToString(array).Replace("-","")}");
      Console.WriteLine("Vamos a proceder a su ampliación...");

    }
    byte[] resultado = Amplify(array, bloque_ant, auxiliar);
    Console.WriteLine($"{BitConverter.ToString(resultado).Replace("-","")}");

    return resultado;
  }

  public static byte[] Amplify (byte[] array, byte[] bloque_ant, int auxy)
  {
    int size_amp = 16 - auxy;
    byte[] resultado = new byte [16];
    
    for (int i = 0; i < size_amp; i++)
    {
      resultado[i] = (byte)(array[i]);
    }
    for (int j = size_amp - 1; j < 16; j++)
    {
      resultado[j] = (byte)(bloque_ant[j]);
    }
    return resultado;
  }

  public static void Main()
  {
    string original_S = "00000000000000000000000000000000";
    string key_S = "000102030405060708090a0b0c0d0e0f";
    string bloque_S = "00112233445566778899AABBCCDDEEFF";

    byte[] bloque_bloque = new byte [16];
    bloque_bloque [0] = 0x00;
    bloque_bloque [1] = 0x00;
    bloque_bloque [2] = 0x00;
    bloque_bloque [3] = 0x00;
    bloque_bloque [4] = 0x00;
    bloque_bloque [5] = 0x00;
    bloque_bloque [6] = 0x00;
    bloque_bloque [7] = 0x00;
    bloque_bloque [8] = 0x00;
    bloque_bloque [9] = 0x00;
    bloque_bloque [10] = 0x00;
    

    original_ = AESProgram.HexStringToByteArray(original_S);
    key_ = AESProgram.HexStringToByteArray(key_S);
    bloque_ = AESProgram.HexStringToByteArray(bloque_S);
    
    original_ = CBC_method(original_, key_, bloque_);
    byte [] resultado1 = Look(bloque_bloque, bloque_);

    bloque_ = bloque_bloque;
    byte[] resultado = CBC_method(original_, key_, bloque_);

  }
}