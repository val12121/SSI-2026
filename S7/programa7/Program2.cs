namespace PracticaAES;
using System;

class CBC
{
  public static byte[] XorBytes (byte[] a, byte[] b)
  {
    byte[] resultado = new byte[16];
    for (int i = 0; i < 16; i++)
    {
      resultado[i] = (byte)(a[i] ^ b[i]);
    }
    return resultado;
  }

  public static void Main()
  {
    string original_S = "00000000000000000000000000000000";
    string key_S = "000102030405060708090a0b0c0d0e0f";
    string bloque_S = "00112233445566778899AABBCCDDEEFF";

    byte[] original = AESProgram.HexStringToByteArray(original_S);
    byte[] key = AESProgram.HexStringToByteArray(key_S);
    byte[] bloque = AESProgram.HexStringToByteArray(bloque_S);

    byte[] aux = XorBytes(original, key);

    byte[] state = AESProgram.Aes(bloque, aux);
    Console.WriteLine($"{BitConverter.ToString(state).Replace("-","")}");
  }
}