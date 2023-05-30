#include <stdio.h>
#include <stdlib.h>
#include <locale.h>

int main(void) {

    // установка русской локали
    setlocale(LC_ALL, "Russian");

    // объявление переменных
    int i, a, b, c, sum;
    sum = 0;
    wprintf(L"Все числа, которые подходят под условие: \n");

    // цикл для перебора чисел от 1 до 99
    for (i = 1; i <= 99; i++) {
        a = i / 10;
        b = i % 10;
        c = a * a + b * b;
        if (c / 10 == 13) {
            wprintf(L"%d\n", i);
            sum++;
        }
    }
    
    wprintf(L"Всего таких чисел: %d\n", sum);

    // ожидание ввода
    getchar();
    return 0;
}
