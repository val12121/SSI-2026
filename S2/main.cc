#include <iostream>

std::string SUM (std::string, std::string);
std::string REST (std::string, std::string);

int main() {
  
  //CIFRAR
  std::string text = "QUIERE TOTO DE LOCA";
  std::string key = "MISION";

  std::string key_ext;

  if (key.size() < text.size()) {
    int i = 0; 
    while (key_ext.size() != text.size()) {
      key_ext.push_back(key[i]);
      i++;
      if (i >= key.size()) {
        i = 0;
      }
    }
  } else {
    key_ext = key;
  }

  std::string word = SUM(text, key_ext);
  std::cout << word << std::endl;


  //DESCIFRAR
  std::cout << REST(word, key_ext) << std::endl;

  return 0;
}

std::string SUM (std::string A, std::string B) {
  std::string result;
  for (int i = 0; i < A.size(); i++) {
    result.push_back(((A[i] - 'A') + (B[i] - 'A')) % 26 + 'A');
  }
  return result;
}

std::string REST (std::string A, std::string B) {
  std::string result;
  for (int i = 0; i < A.size(); i++) {
    result.push_back(((A[i] - 'A') - (B[i] - 'A') + 26 ) % 26 + 'A'); //Le sumo +26 para evitar los valores negativos
  }
  return result;
}