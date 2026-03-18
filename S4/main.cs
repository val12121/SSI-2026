using System;
class GeneradorCA
{
  static int SumG1(int[] g1)
  {
    return g1[2] ^ g1[9];
  }

  static int SumG2(int[] g2)
  {
    return g2[1] ^ g2[2] ^ g2[5] ^ g2[7] ^ g2[8] ^ g2[9];
  }

  static void AllIn1(int[] g)
  {
    for (int i = 0; i < g.Length; i++)
      g[i] = 1;
  }

  static void ShiftRegister(int[] g, int newBit)
  {
    // Desplaza bits a la derecha
    for (int i = g.Length - 1; i > 0; i--) {
      g[i] = g[i - 1];
    }
    g[0] = newBit;
  }

  static void Main()
  {
    Console.Write("Ingresa el número de iteraciones: ");
    string input = Console.ReadLine();
    int iter;
    int.TryParse(input, out iter);

    Random ram = new Random();
    int randomNumber1 = ram.Next(0, 9);
    int randomNumber2 = ram.Next(0, 9);

    int[] g1 = new int[10];
    int[] g2 = new int[10];

    AllIn1(g1);
    AllIn1(g2);

    Console.WriteLine($"Numeros elegidos: {randomNumber1} {randomNumber2}");

    Console.WriteLine("Iteración | g1          | r | g2          | r | bitCA");
    Console.WriteLine("------------------------------------------------------");

    for (int i = 1; i <= iter; i++) // 10 iteraciones de ejemplo
    {
      int newG1 = SumG1(g1);
      int newG2 = SumG2(g2);

      ShiftRegister(g1, newG1);
      ShiftRegister(g2, newG2);

      // g1 XOR (g2[1] ^ g2[5])
      int bitCA = g1[9] ^ (g2[randomNumber1] ^ g2[randomNumber2]);
      //int bitCA = g1[9] ^ (g2[1] ^ g2[5]);

      Console.WriteLine($"{i,9} | {string.Join("", g1)} | {newG1} | {string.Join("", g2)} | {newG2} | {bitCA}");
    }
  }
}