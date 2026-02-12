#include <iostream>
#include <vector>
#include <string>
#include <sstream>
#include <bitset>
#include <cstdint>

using namespace std;

void Show(std::vector<std::string>&);

void ToBinary(std::string word, std::vector<std::string>& binary) {
  for (int i = 0; i < word.size(); i++) {
    std::bitset<8> bits(word[i]);
    binary.push_back(bits.to_string());
  }
}

std::string VectorToString (std::vector<std::string> vector) {
  std::string result;
  for (int i = 0; i < vector.size(); i++) {
    for (int j = 0; j < vector[i].size(); j++) {
      result.push_back(vector[i][j]);
    }
  }
  //std::cout << result << std::endl;
  return result;
}

std::string XOR (std::string key, std::string lock) {
  std::string result;
  for (int i = 0; i < key.size(); i++) {
    if (key[i] != lock[i]) {
      result.push_back('1');
    } else {
      result.push_back('0');
    }
  }
  return result;
}

void Result(std::string aux, std::vector<std::string>& vector) {
  int n = 0;
  for (int i = 0; i < aux.size(); i+= 8) {
    vector.push_back(aux.substr(i, 8)); //Toma los siguientes ocho caracteres
    //std::cout << vector.back() << endl;
  }

  Show(vector);
}

void Show(std::vector<std::string>& vector) {
  for (int i = 0; i < vector.size(); i++) {
    vector[i] = std::stoi(vector[i], nullptr, 2); //Binario a decimal
  }
  
  for (int i = 0; i < vector.size(); i++) {
    std::cout << vector[i]; //Muestra string
  }
  std::cout << endl;
}


int main() {

  //Así Ciframos
  string word = "SOL";
  vector<string> binary;
  
  std::string key = "001111000001100001110011";

  ToBinary(word, binary);

  std::string aux1 = XOR(key, VectorToString(binary));
  std::vector<std::string> end;

  Result(aux1, end);

  //Así Desciframos
  string word_c = "[t";
  vector<string> binary_c;

  std::string key_c = "0000111100100001";

  ToBinary(word_c, binary_c);

  std::string aux1_c = XOR(key_c, VectorToString(binary_c));
  std::vector<std::string> end_c;

  Result(aux1_c, end_c);

}