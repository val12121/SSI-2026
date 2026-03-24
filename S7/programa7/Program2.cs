namespace PracticaAES;
using System;

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

  public static void Main()
  {
    string original_S = "00000000000000000000000000000000";
    string key_S = "000102030405060708090a0b0c0d0e0f";
    string bloque_S = "00112233445566778899AABBCCDDEEFF";
    string bloque_S2 = "00000000000000000000000000000000";

    original_ = AESProgram.HexStringToByteArray(original_S);
    key_ = AESProgram.HexStringToByteArray(key_S);
    bloque_ = AESProgram.HexStringToByteArray(bloque_S);
    
    original_ = CBC_method(original_, key_, bloque_);
    bloque_ = AESProgram.HexStringToByteArray(bloque_S2);
    byte[] resultado = CBC_method(original_, key_, bloque_);
  }
  
}