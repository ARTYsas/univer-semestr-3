#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include <locale.h>
#include <math.h>

long int factorial(int);

int main() {
    // Устанавливаем локаль для корректного отображения русских символов
    setlocale(LC_ALL, "Russian");

    double i, z;
    float result, e, sum;
    wprintf(L"Задайте точность e > 0, e = ");
    // Считываем значение e с помощью функции wscanf
    wscanf(L"%f", &e);

    result = i = 0;
    z = -1;

    do
    {
        i++;
        sum = pow(-1, i) / factorial(i);
        result += sum;
    } while (fabs(sum) >= e); // Используем функцию fabs для вычисления модуля значения

    // Выводим результат с помощью функции wprintf
    wprintf(L"Результат: %.2f", result);

    // Ожидаем ввода пользователя с помощью функции getch()
    getch();
    return 0;
}

// Реализация функции вычисления факториала
long int factorial(int n)
{
    if (n == 0) {
        return 1;
    }
    else {
        return(n * factorial(n - 1));
    }
}
