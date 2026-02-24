#include <iostream>
#include <vector>
#include <cstdint>
#include <iomanip>
#include <string>

void chacha_init(uint32_t state[16],
                 const uint32_t key[8],
                 uint32_t counter,
                 const uint32_t nonce[3])
{
  // Constantes
  state[0] = 0x61707865;
  state[1] = 0x3320646e;
  state[2] = 0x79622d32;
  state[3] = 0x6b206574;

  // Clave
  for (int i = 0; i < 8; i++)
    state[4 + i] = key[i];

  // Contador
  state[12] = counter;

  // Nonce
  state[13] = nonce[0];
  state[14] = nonce[1];
  state[15] = nonce[2];
}

uint32_t ToNormal(std::string key)
{

  uint32_t aux = 0;
  int shift = 0;

  for (size_t i = 0; i < key.size(); i += 3)
  {
    std::string rest = key.substr(i, 2);
    uint32_t byte = std::stoul(rest, nullptr, 16);

    aux |= (byte << shift);
    shift += 8;
  }
  return aux;
}

uint32_t rotl(uint32_t x, int n)
{
  return (x << n) | (x >> (32 - n));
}

void quarter_round(uint32_t &a,
                   uint32_t &b,
                   uint32_t &c,
                   uint32_t &d)
{
  a += b; //SUMA
  d ^= a; //XOR
  d = rotl(d, 16); //ROTACIÓN CÍCLICA izq

  c += d;
  b ^= c;
  b = rotl(b, 12);

  a += b;
  d ^= a;
  d = rotl(d, 8);

  c += d;
  b ^= c;
  b = rotl(b, 7);
}

void chacha20_rounds(uint32_t state[16], uint32_t working_state[16])
{
  // Copiamos estado original
  for (int i = 0; i < 16; i++)
    working_state[i] = state[i];

  // 10 double rounds
  for (int i = 0; i < 10; i++)
  {
    // Column round
    quarter_round(working_state[0], working_state[4], working_state[8], working_state[12]);
    quarter_round(working_state[1], working_state[5], working_state[9], working_state[13]);
    quarter_round(working_state[2], working_state[6], working_state[10], working_state[14]);
    quarter_round(working_state[3], working_state[7], working_state[11], working_state[15]);

    // Diagonal round
    quarter_round(working_state[0], working_state[5], working_state[10], working_state[15]);
    quarter_round(working_state[1], working_state[6], working_state[11], working_state[12]);
    quarter_round(working_state[2], working_state[7], working_state[8], working_state[13]);
    quarter_round(working_state[3], working_state[4], working_state[9], working_state[14]);
  }
}

void chacha20_add(uint32_t state[16], uint32_t working_state[16])
{
  for (int i = 0; i < 16; i++)
    state[i] += working_state[i];
}

void print_state(uint32_t state[16])
{
  std::cout << std::hex << std::setfill('0');
  for (int i = 0; i < 16; i++)
  {
    std::cout << std::setw(8) << state[i] << " ";
    if ((i + 1) % 4 == 0)
      std::cout << std::endl;
  }
}

int main()
{
  std::vector<std::string> key_str{
      "00:01:02:03", "04:05:06:07", "08:09:0a:0b", "0c:0d:0e:0f",
      "10:11:12:13", "14:15:16:17", "18:19:1a:1b", "1c:1d:1e:1f"};
  std::string counter_str = "01:00:00:00";
  std::vector<std::string> nonce_str{"00:00:00:09", "00:00:00:4a", "00:00:00:00"};

  uint32_t key[8], nonce[3], counter;

  for (int i = 0; i < 8; i++)
    key[i] = ToNormal(key_str[i]);
  for (int i = 0; i < 3; i++)
    nonce[i] = ToNormal(nonce_str[i]);
  counter = ToNormal(counter_str);

  uint32_t state[16], working_state[16];

  chacha_init(state, key, counter, nonce);

  std::cout << "Estado inicial:\n";
  print_state(state);

  chacha20_rounds(state, working_state);

  std::cout << "\nEstado tras 20 rondas:\n";
  print_state(working_state);

  chacha20_add(state, working_state);

  std::cout << "\nEstado final del generador:\n";
  print_state(state);

  return 0;
}