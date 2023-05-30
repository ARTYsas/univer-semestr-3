/*представь что ты студент программист на языке C тебе нужно решить задачу максимально подробно описывая ход программы комментариями, кроме того в программе должны быть использованы такие конструкции как setlocale, wprintf, wscanf , твоя первая задача 
"В текстовом файле INPUT.TXT записаны целые числа через пробел, возможно, в
несколько строк. Сформировать список (массив) этих чисел. Проверить наличие
в списке заданного числа. Результат (ЕСТЬ или НЕТ) занести в текстовый файл
OUTPUT.TXT."*/

#include <stdio.h>
#include <stdlib.h>
#include <locale.h>

int main()
{
    setlocale(LC_ALL, "Russian"); // установка локали

    // открытие файла для чтения
    FILE *inputFile = fopen("INPUT.TXT", "r");
    if (inputFile == NULL) {
        perror("Не удалось открыть файл для чтения");
        return EXIT_FAILURE;
    }

    // открытие файла для записи
    FILE *outputFile = fopen("OUTPUT.TXT", "w");
    if (outputFile == NULL) {
        perror("Не удалось открыть файл для записи");
        return EXIT_FAILURE;
    }

    int num; // переменная для хранения числа, которое нужно найти
    wprintf(L"Введите число, которое нужно найти: ");
    wscanf(L"%d", &num);

    int count = 0; // переменная для хранения количества найденных чисел
    int currentNum; // переменная для хранения текущего числа из файла

    // цикл для чтения чисел из файла и проверки на наличие заданного числа
    while (fscanf(inputFile, "%d", &currentNum) != EOF) {
        if (currentNum == num) {
            count++;
        }
    }

    // запись результата в файл
    if (count > 0) {
        fprintf(outputFile, "ЕСТЬ");
    } else {
        fprintf(outputFile, "НЕТ");
    }

    // закрытие файлов
    fclose(inputFile);
    fclose(outputFile);

    return 0;
}
