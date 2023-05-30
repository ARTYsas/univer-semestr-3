#include <stdio.h>
#include <locale.h> // библиотека для работы с локалью
#include <stdlib.h> // библиотека для работы с памятью

int main()
{
    setlocale(LC_ALL, "Russian"); // установка локали для поддержки русского языка

    int n;
    wprintf(L"Введите размерность массива: ");
    wscanf(L"%d", &n);

    int* a = (int*) malloc(n * sizeof(int)); // выделение памяти под массив

    int even = 0, odd = 0, result = 1;
    // even - количество четных элементов
    // odd - количество нечетных элементов
    // result - произведение (a1+an)(a2+an-1)...

    wprintf(L"Введите элементы массива: ");
    for (int i = 0; i < n; i++)
    {
        wscanf(L"%d", &a[i]);
        if (a[i] % 2 == 0)
            even++;
        else
            odd++;
    }

    if (even == odd) // если количество четных и нечетных элементов равно
    {
        for (int i = 0; i < n / 2; i++)
        {
            result *= (a[i] + a[n - 1 - i]);
        }

        wprintf(L"Результат: %d", result); // вывод результата
    }
    else
    {
        wprintf(L"Нечетное количество элементов в массиве"); // вывод сообщения о нечетном количестве элементов
    }

    free(a); // освобождение памяти

    getch(); // ожидание ввода пользователя
    return 0;
}
