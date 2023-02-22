﻿// Задача 59: Задайте двумерный массив из целых чисел. Напишите программу, которая удалит строку и столбец, на пересечении которых расположен наименьший элемент массива.
// Например, задан массив:
// 1 4 7 2
// 5 9 2 3
// 8 4 2 4
// 5 2 6 7
// Наименьший элемент - 1, на выходе получим 
// следующий массив:
// 9 4 2
// 2 2 6
// 3 4 7

int[,] FillMatrix(int[,] matr, int lBound, int hBound)           //  работа с матрицей - наполнение случайными целыми числами в заданном диапазоне
{
    for (int i = 0; i < matr.GetLength(0); i++)
        for (int j = 0; j < matr.GetLength(1); j++)
            matr[i, j] = new Random().Next(lBound, hBound);
    return matr;
}

int FindMinInMatrix(int[,] matr)                                //  работа с матрицей - поиск минимального значения по всей матрице
{
    int vMin = 99999;                                           //  абсолютный минимум (по всем строкам и столбцам)
    for (int i = 0; i < matr.GetLength(0); i++)                 //  цикл по строкам
        for (int j = 0; j < matr.GetLength(1); j++)             //  цикл по столбцам
            if (matr[i, j] < vMin)
                vMin = matr[i, j];
    return vMin;                                                //  возврат результата
}

int[,] FillArrayMarker(int[,] matr, int vMin, int[,] mark)      //  работа с массивом-маркером - пометка строк и столбцов, содержащих минимум
{
    for (int i = 0; i < matr.GetLength(0); i++)                 //  цикл по строкам матрицы
        for (int j = 0; j < matr.GetLength(1); j++)             //  цикл по столбцам матрицы
            if (matr[i, j] == vMin)                             //  нашли минимум в матрице
            {
                mark[i, 0] = 1;                                 //  помечаем в массиве-маркере номер соответствующей строки - единицей
                mark[j, 1] = 1;                                 //  помечаем в массиве-маркере номер соответствующего столбца - единицей
            }
    return mark;                                                //  возврат результата - массива-маркера
}

int[,] FillNewMatrix(int[,] matr, int[,] mark, int[,] newMatr)  //  работа с исходной матрицей - копирование только тех строк и столбцов, которые не имеют 1 в массиве-маркере
{
    int k = 0;                                                  //  индекс строки новой матрицы
    for (int i = 0; i < matr.GetLength(0); i++)                 //  цикл по строкам исходной матрицы
    {
        if (mark[i, 0] == 0)                                    //  если строка не имеет пометки в массиве-маркере
        {
            int l = 0;                                          //  индекс строки новой матрицы
            for (int j = 0; j < matr.GetLength(1); j++)         //  цикл по столбцам исходной матрицы
                if (mark[j, 1] == 0)                            //  и если также столбец не имеет пометки в массиве-маркере,...
                {
                    newMatr[k, l] = matr[i, j];                 //  ...то переносим ячейку в новую матрицу
                    l += 1;
                }
            k += 1;
        }
    }
    return newMatr;                                             //  возврат результата - новой матрицы
}

void PrintMatrix(int[,] matr)                                   //  форматированный вывод матрицы в консоль
{
    for (int i = 0; i < matr.GetLength(0); i++)
    {
        for (int j = 0; j < matr.GetLength(1); j++)
            Console.Write("\t" + matr[i, j]);
        Console.WriteLine();
    }
}

Console.Clear();				                                //  очистка консоли
Console.WriteLine("Введите размерность по вертикали m: ");	    //  запрос размерности матрицы по вертикали
int m = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Введите размерность по горизонтали n: ");	//  запрос размерности матрицы по вертикали
int n = Convert.ToInt32(Console.ReadLine());

int[,] matrix = new int[m, n];                                  //  инициализация матрицы
FillMatrix(matrix, 1, 10);                                      //  наполнение матрицы случайными целыми числами в заданном диапазоне (от 1 до 100)

Console.WriteLine("\nМатрица в исходном виде:");
PrintMatrix(matrix);                                            //  вывод исходной матрицы в консоль

int minimum = FindMinInMatrix(matrix);                          //  работа с матрицей: поиск минимального значения по всей матрице

int[,] marker = new int[Math.Max(m, n), 2];                     //  инициализация массива-маркера
FillArrayMarker(matrix, minimum, marker);                       //  заполнение массива-маркера

int iSum = 0;                                                   //  подсчет выпадающих строк
int jSum = 0;                                                   //  подсчет выпадающих столбцов
for (int i = 0; i < marker.GetLength(0); i++)                   //  цикл по элементам массива-маркера, соответствующим строкам исходной матрицы
{
    if (marker[i, 0] == 1)                                      //  если строка маркерована, то включаем её в подсчет
        iSum += 1;
    if (marker[i, 1] == 1)                                      //  если столбец маркерован, то включаем его в подсчет
        jSum += 1;
}

if (m - iSum > 0 && n - jSum > 0)
{
    int[,] newMatrix = new int[m - iSum, n - jSum];             //  инициализация новой матрицы (с учетом выпадающих строк и столбцов)
    FillNewMatrix(matrix, marker, newMatrix);                   //  наполнение новой матрицы (с учетом выпадающих строк и столбцов)
    Console.WriteLine("\nРезультат:");
    PrintMatrix(newMatrix);                                     //  вывод новой матрицы в консоль
}