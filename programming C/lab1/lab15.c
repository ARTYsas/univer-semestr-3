#include <stdio.h>
#include <math.h>
#include <locale.h>

int main(void) {

    // установка локали для корректного отображения кириллицы в консоли
    setlocale(LC_ALL, "");

    // объявление переменных
    int n;
    float result;

    result = 2;
    wprintf(L"Введите n: ");
    wscanf(L"%d", &n);

    if (n == 1) {
        wprintf(L"Ответ: %f", sqrt(result));
    }
    else {
        for (int i = n; i > 0; i--) {
            //wprintf(L"i: %d\n", i);
            result = sqrt(2 + sqrt(result));
            //wprintf(L"result: %f\n", result);
            
        }

        wprintf(L"Ответ: %f", result);
    }

    // ожидание ввода
    getch();
    return 0;
}
