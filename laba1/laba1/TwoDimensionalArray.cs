using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace laba1
{
    class TwoDimensionalArray
    {
        private static void DisplayArray2(int[,] arr) // вывод двухмерного массива
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                Console.Write("[ ");
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    Console.Write(arr[i, j] + " ");
                }
                Console.Write("]");
                Console.WriteLine();
            }
        }
        public static void Solve() // Задание 2
        {
            
            int[,] arr; // главный массив с элементами
            int[] file_input = Laba1.FileInputReciever();
            int LineCount = file_input.Length;
            if (Laba1.FileInputReciever().Length == 0 || Math.Sqrt(LineCount) % 1 != 0) //Заполняем массив вручную
            {
                Console.WriteLine("Файл некорректный. Убедитесь, что количество строк равно квадрату целого числа.");
                Console.WriteLine("\nУкажите количество столбцов-строк двухмерной матрицы.");
                LineCount = Laba1.NumberFromUser(1, Int32.MaxValue);
                arr = new int[LineCount, LineCount];

                for (int i = 0; i < LineCount; i++)
                {
                    for (int j = 0; j < LineCount; j++)
                    {
                        Console.WriteLine("Элемент массива [" + i + ", " + j + "] из [" + (LineCount - 1) + ", " + (LineCount - 1) + "]");
                        arr[i, j] = Laba1.NumberFromUser(Int32.MinValue, Int32.MaxValue);
                    }
                }
            }
            else
            {
                LineCount = (int)Math.Sqrt(LineCount);
                arr = new int[LineCount, LineCount];
                // заполнение массива из файла
                int temp = 0;
                for (int i = 0; i < LineCount; i++)
                {
                    for (int j = 0; j < LineCount; j++)
                    {
                        arr[i, j] = file_input[temp];
                        temp++;
                    }
                }

            }

            // МАССИВ ЗАПОЛНЕН, ВЫПОЛНЯЕМ ЗАДАНИЯ
            // 2.1 Вывод массива
            Console.WriteLine("\nВывод массива в виде матрицы: ");
            DisplayArray2(arr);

            // 2.2 min max
            int BiggestNumber = Int32.MinValue; // значение элемента
            string BiggestNumberId = "i, j", LowestNumberId = "i, j"; // номер элемента
            int LowestNumber = Int32.MaxValue;

            for (int i = 0; i < LineCount; i++)
            {
                for (int j = 0; j < LineCount; j++)
                {
                    if (arr[i, j] > BiggestNumber)
                    {
                        BiggestNumber = arr[i, j];
                        BiggestNumberId = i.ToString() + ", " + j.ToString();
                    }
                    if (arr[i, j] < LowestNumber)
                    {
                        LowestNumber = arr[i, j];
                        LowestNumberId = i.ToString() + ", " + j.ToString();
                    }
                }
            }
            Console.WriteLine("\nМаксимальный элемент [" + BiggestNumberId + "]\nЗначение: " + BiggestNumber);
            Console.WriteLine("\nМинимальный элемент [" + LowestNumberId + "]\nЗначение: " + LowestNumber);

            // 2.3 произведение, сумма, разность массивов
            Console.WriteLine("\nДля арифметических действий возьмем первые 2 массива:");
            Int64 multiply = 1;
            int sum = 0, substract = 0; // произведение, сумма, вычитание
            if (LineCount < 2)
            {
                Console.WriteLine("У вас один массив, это невозможно...");
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < LineCount; j++)
                    {
                        multiply *= arr[i, j];
                        sum += arr[i, j];
                        substract -= arr[i, j];
                    }
                }
                Console.WriteLine("Произведение = " + multiply + "\nСумма = " + sum + "\nРазность = " + substract);
            }
        }
    }
}
