using System;

namespace laba1
{
    class AnyDimensionalArray
    {
        private static void DisplayArray3(int[][] arr) // вывод ступенчатого массива
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                Console.Write("[ ");
                for (int j = 0; j < arr[i].Length; j++)
                {
                    Console.Write(arr[i][j] + " ");
                }
                Console.Write("]");
                Console.WriteLine();
            }
        }
        public static void Solve() // Задание 3
        {
            Console.WriteLine("Введите количество массивов: ");
            int ArraysAmount = Laba1.NumberFromUser(1, Int32.MaxValue); // количество массивов
            int[][] arr = new int[ArraysAmount][];
            int ElementsAmount = 0; // количество элементов во всем ступенчатом массиве. Пригодится чтобы проверить верное ли количество строк в txt
            int LinesAmount; // временная переменная будет хранить в себе количество элементов в массиве каждой строки
            for (int i = 0; i < ArraysAmount; i++) // создание ступенчатого массива пока без значений
            {
                Console.Write("Размер массива #" + i + ": ");
                LinesAmount = Laba1.NumberFromUser(1, Int32.MaxValue);
                ElementsAmount += LinesAmount;
                arr[i] = new int[LinesAmount]; // получаем длину массива на строке i
            }

            int[] FileInput = Laba1.FileInputReciever(); // заполнение массива элементами из файла
            if(FileInput.Length != ElementsAmount)
            {
                Console.WriteLine("Неверное количество строк в txt. Должно быть: " + ElementsAmount);
                for (int i = 0; i < ArraysAmount; i++)
                {
                    Console.WriteLine("\nСтрока #" + i);
                    for (int j = 0; j < arr[i].Length; j++)
                    {
                        Console.WriteLine("Элемент массива " + j + " из " + (arr[i].Length - 1) + ".");
                        arr[i][j] = Laba1.NumberFromUser(Int32.MinValue, Int32.MaxValue);
                    }
                }
            }
            else
            {
                int temp = 0;
                for(int i = 0; i < ArraysAmount; i++)
                {
                    for(int j = 0; j < arr[i].Length; j++)
                    {
                        arr[i][j] = FileInput[temp];
                        temp++;
                    }
                }
            }
            
            // 3.1 вывод одноступенчатого массива
            Console.WriteLine("\nМассив в виде матрицы: ");
            DisplayArray3(arr);
            // 3.2.1 изменить конкретный элемент массива

            // в данной части кода мы заставляем написать правильные координаты элемента массива и его новое значение
            Console.Write("\nВыберите номер массива (начиная с 0), в котором вы хотите внести изменение: ");
            int UserI = Laba1.NumberFromUser(0, ArraysAmount - 1);
            Console.Write("\nВыберите элемент массива (начиная с 0), в котором вы хотите внести изменение: ");
            int UserJ = Laba1.NumberFromUser(0, arr[UserI].Length);
            Console.Write("\nУкажите новое значение: ");
            int UserInt = Laba1.NumberFromUser(Int32.MinValue, Int32.MaxValue);
            arr[UserI][UserJ] = UserInt;

            Console.WriteLine("\nМассив после изменения элемента: ");
            DisplayArray3(arr);
            // 3.2.2 Max Min
            int MaxInt = Int32.MinValue;
            int MinInt = Int32.MaxValue;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] > MaxInt)
                    {
                        MaxInt = arr[i][j];
                    }
                    if (arr[i][j] < MinInt)
                    {
                        MinInt = arr[i][j];
                    }
                }
            }
            Console.WriteLine("\n\nМаксимальный элемент массива: " + MaxInt + "\nМинимальный элемент массива: " + MinInt);
        }
    }
}
