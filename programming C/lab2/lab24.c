#include <stdio.h>
#include <stdlib.h>
#include <locale.h>

int main()
{
    setlocale(LC_ALL, "Russian"); // устанавливаем локаль по умолчанию

    int n;
    wprintf(L"Введите размерность квадратной матрицы, а затем все её элементы: ");
    wscanf(L"%d", &n);

    int** x = (int**)malloc(n * sizeof(int*));
    int* y = (int*)malloc(2 * n * sizeof(int));

    for (int i = 0; i < n; i++)
    {
        x[i] = (int*)malloc(n * sizeof(int));
        for (int j = 0; j < n; j++)
            wscanf(L"%d", &x[i][j]);
    }

    int k = 0, sum;
    for (int i = n - 1; i > 0; i--)
    {
        sum = 0;
        for (int j = i; j < n; j++)
            sum += x[j][j - i];
        y[k++] = sum;
    }

    for (int i = 0; i < n; i++)
    {
        sum = 0;
        for (int j = i; j < n; j++)
            sum += x[j - i][j];
        y[k++] = sum;
    }

    wprintf(L"Суммы по диагоналям: ");
    for (int i = 0; i < 2 * n - 1; i++)
        wprintf(L"%d ", y[i]);

    free(x);
    free(y);

    getch(); // ожидание ввода пользователя
    return 0;
}
