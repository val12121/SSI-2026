using System;

class Multiply
{
  static byte multiplying(byte a, byte b, byte poly, int num)
  {
    byte result = 0;
    for (int i = 0; i < 8; i++)
    {
      if (b % 2 != 0)
      {
        result = (byte)(result ^ a);
      }
      bool msb = (a & 0x80) != 0;
      a <<= 1;

      if (msb) {
        num++;
        a = (byte)(poly ^ a);
      }

      b >>= 1;
    }
    Console.WriteLine($"{num}");
    return result;
  }

  static void Main()
  {
    byte first = 0x57;
    byte second = 0x83;
    int num = 0; 

    Console.WriteLine("Primer byte  : " + Convert.ToString(first, 2).PadLeft(8, '0'));
    Console.WriteLine("Segundo byte : " + Convert.ToString(second, 2).PadLeft(8, '0'));

    // AES
    byte aesPoly = 0x1B;
    byte aesResult = multiplying(first, second, aesPoly, num);

    Console.WriteLine("\nAES");
    Console.WriteLine("Polinomio    : " + Convert.ToString(aesPoly, 2).PadLeft(8, '0'));
    Console.WriteLine("Resultado    : " + Convert.ToString(aesResult, 2).PadLeft(8, '0'));
    Console.WriteLine($"{num}");

    num = 0;
    // SNOW 3G
    byte snowPoly = 0xA9;
    byte snowResult = multiplying(first, second, snowPoly, num);

    Console.WriteLine("\nSNOW 3G");
    Console.WriteLine("Polinomio    : " + Convert.ToString(snowPoly, 2).PadLeft(8, '0'));
    Console.WriteLine("Resultado    : " + Convert.ToString(snowResult, 2).PadLeft(8, '0'));
    Console.WriteLine($"{num}");


  }
}