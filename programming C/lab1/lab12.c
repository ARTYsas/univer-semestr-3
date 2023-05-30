#include <stdio.h>
#include <locale.h>
#include <math.h>

int main(void) {
    setlocale(LC_ALL, "Russian"); // Устанавливаем локаль для поддержки русского языка

    // Объявление переменных
    double x, y;
    x = y = 0;

    // Запрос значения переменных
    wprintf(L"Введите x: ");
    wscanf(L"%lf", &x);
    wprintf(L"Введите y: ");
    wscanf(L"%lf", &y);

    // Проверка условий
    if (x < 0 && y < 0) {
        wprintf(L"x = %.2f\ny = %.2f", fabs(x), fabs(y)); // Вывод значений переменных с абсолютным значением
    }
    else if (x < 0 || y < 0) {
        x = x * 0.5;
        y = y * 0.5;
        wprintf(L"x = %.2f\ny = %.2f", x, y); // Вывод значений переменных, уменьшенных в 2 раза
    }
    else if (x > 0.5 && x < 2 && y > 0.5 && y < 2) {
        x = x / 10;
        y = y / 10;
        wprintf(L"x = %.2f\ny = %.2f", x, y); // Вывод значений переменных, уменьшенных в 10 раз
    }
    else {
        wprintf(L"x = %.2f\ny = %.2f", x, y); // Вывод значений переменных
    }

    // Ожидание ввода пользователя
    getch();
    return 0;
}
