```cpp
#include <bitset>
#include <vector>
#include <functional>
#include <iostream>

class BloomFilter {
    size_t filter_len;
    std::bitset<32> bitset;

public:
    BloomFilter(size_t f_len) : filter_len(f_len) {}

    void hash1(char* str1) {
        for (int i = 0; i < strlen(str1); i++) {
            int code = (int)str1[i];
            bitset[code % filter_len] = 1;
        }
    }

    void hash2(char* str1) {
        int factor = 0;
        for (int i = 0; i < strlen(str1); i++) {
            int code = (int)str1[i];
            factor += code;
        }
        bitset[factor % filter_len] = 1;
    }

    void add(char* str1) {
        hash1(str1);
        hash2(str1);
    }

    bool is_value(char* str1) {
        bool result1 = bitset[(int)str1[0] % filter_len] == 1;
        int factor = 0;
        for (int i = 0; i < strlen(str1); i++) {
            int code = (int)str1[i];
            factor += code;
        }
        bool result2 = bitset[factor % filter_len] == 1;
        return result1 && result2;
    }
};

int main() {
    BloomFilter bloomFilter(32);
    char example1[] = "test";
    char example2[] = "test2";
    bloomFilter.add(example1);
    std::cout << bloomFilter.is_value(example1) << std::endl;
    std::cout << bloomFilter.is_value(example2) << std::endl;
    return 0;
}
```