using System;
using System.IO;
using System.Linq;

namespace laba1
{
    class Laba1
    {
        static int NumberFromUser(int OptionsAmount) // функция, заставляет пользователя вернуть число в диапазоне от 1 до OptionsAmount
        {
            bool ifCorrectNumber = false;
            int UserNumber = 0;
            while (!ifCorrectNumber) // пока человек не введет нужное число, цикл не закончится
            {
                Console.Write("Ввод: ");
                ifCorrectNumber = Int32.TryParse(Console.ReadLine(), out UserNumber); // Если удалось записать число из консоли, то ifCorrectNumber = true
                if (!ifCorrectNumber || UserNumber > OptionsAmount || UserNumber < 1)
                {
                    ifCorrectNumber = false;
                    Console.WriteLine("Неправильное число");
                }
            }
            return UserNumber;
        }
        static void DisplayArray1(int[] arr) // вывод одномерного массива
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
        }
        static void DisplayArray2(int[,] arr) // вывод двухмерного массива
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                Console.Write("[ ");
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    Console.Write(arr[i, j] + " ");
                }
                Console.Write(" ]");
                Console.WriteLine();
            }
        }
        static void DisplayArray3(int[][] arr) // вывод ступенчатого массива
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
        static void OneDimensionArray() // Задание 1
        {
            int LineCount;
            int[] arr;
            //Заполняем массив
            try
            {
                LineCount = File.ReadLines(@"C:\\input\input.txt").Count(); // количество строк в файле
                if (LineCount < 2) { File.ReadLines(@"C:\\con"); } // Мало строк в файле? Вызываем ошибку и уходим в catch

                arr = new int[LineCount];
                var reader = File.OpenText(@"C:\\input\input.txt");
                bool Achtung; // если вдруг не число в файле, завершаем программу
                for (int i = 0; i < LineCount; i++)
                {
                    Achtung = Int32.TryParse(reader.ReadLine(), out arr[i]);
                    if (!Achtung)
                    {
                        Console.WriteLine("У вас строка в текстовом файле, вы все сломали! Перевожу вас на ручной ввод...");
                        Console.ReadKey();
                        File.ReadLines(@"C:\\con");
                    }
                }
            }
            catch // если с файлом проблемы
            {
                Console.WriteLine("Файла для ввода не существует, там буквы или в нем слишком мало строк для корректной работы программы");
                Console.WriteLine("Введите количество элементов массива");
                LineCount = NumberFromUser(Int32.MaxValue); // Int32.MaxValue - это максимально допустимое число, с которым нет переполнения
                arr = new int[LineCount];
                bool isNumber = true;
                for (int i = 0; i < LineCount; i++)
                {
                    Console.Write("Элемент массива " + i + " из " + (LineCount - 1) + ": ");
                    isNumber = Int32.TryParse(Console.ReadLine(), out arr[i]);
                    if (!isNumber)
                    {
                        Console.WriteLine("Неправильное число");
                        i--;
                    }
                }
            }
            // С заполнением массива разобрались

            //НАЧАЛО ВЫПОЛНЕНИЯ ПО ПУНКТАМ
            Console.WriteLine("\n\nВариант А (Ручками)");
            // 1.а.1 вывод массива
            Console.WriteLine("Массив целиком: ");
            DisplayArray1(arr);

            // 1.а.2 Min Max
            int BiggestNumber = Int32.MinValue; // значение элемента
            int BiggestNumberId = 0, LowestNumberId = 0; // номер элемента
            int LowestNumber = Int32.MaxValue;
            for (int i = 0; i < LineCount; i++)
            {
                if (arr[i] > BiggestNumber)
                {
                    BiggestNumber = arr[i];
                    BiggestNumberId = i;
                }
                if (arr[i] < LowestNumber)
                {
                    LowestNumber = arr[i];
                    LowestNumberId = i;
                }
            }
            Console.WriteLine("\nМаксимальный элемент #" + BiggestNumberId + "\nЗначение: " + BiggestNumber);
            Console.WriteLine("Минимальный элемент #" + LowestNumberId + "\nЗначение: " + LowestNumber); // не люблю длинные строки

            // 1.а.3.1 прямая сортировка (возрастание)
            int temp;
            int[] TempArr = arr;
            for (int i = 0; i < LineCount; i++)
            {
                for (int j = i + 1; j < LineCount; j++)
                {
                    if (TempArr[i] > TempArr[j])
                    {
                        temp = TempArr[i];
                        TempArr[i] = TempArr[j];
                        TempArr[j] = temp;
                    }
                }
            }
            Console.WriteLine("\nМассив после прямой сортировки: ");
            DisplayArray1(TempArr);

            // 1.а.3.2 обратная сортировка (убывание)
            TempArr = arr;
            for (int i = 0; i < LineCount; i++)
            {
                for (int j = i + 1; j < LineCount; j++)
                {
                    if (TempArr[i] < TempArr[j])
                    {
                        temp = TempArr[i];
                        TempArr[i] = TempArr[j];
                        TempArr[j] = temp;
                    }
                }
            }
            Console.WriteLine("\nМассив после обратной сортировки: ");
            for (int i = 0; i < LineCount; i++)
            {
                Console.Write(TempArr[i] + " ");
            }

            //1.а.4 вывод четных элементов массива
            TempArr = new int[LineCount];
            int CellNumber = 0; // какую по счету ячейку заполняем
            for (int i = 0; i < LineCount; i++)
            {
                if (arr[i] % 2 == 0)
                {
                    TempArr[CellNumber] = arr[i];
                    CellNumber++;
                }
            }
            Console.WriteLine("\n\nМассив четных элементов: ");
            Array.Resize(ref TempArr, CellNumber); // убираем лишние элементы массива, чтобы не выводить нули
            DisplayArray1(TempArr);

            Console.WriteLine("\n\nВариант Б (методами и свойствами)");
            // 1.б.1 вывод
            Console.WriteLine("\nВывод массива: ");

            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr.GetValue(i) + " ");
            }

            // 1.б.2 Min Max
            Console.WriteLine("\n\nМаксимальное значение: " + arr.Max() + "\nМинимальное значение: " + arr.Min());

            // 1.б.3 Сортировки
            TempArr = arr;
            Array.Sort(TempArr);
            Console.WriteLine("\nВывод массива после прямой сортировки: ");
            DisplayArray1(TempArr);

            // обратная 
            TempArr = arr;
            Array.Reverse(TempArr);
            Console.WriteLine("\nВывод массива после обратной сортировки: ");
            DisplayArray1(TempArr);
            Console.WriteLine();
            // 1.б.4 четные элементы
            // зачем это делать? Решение такое же как в ручной части, по другому никак
        }
        static void TwoDimensionArray() // Задание 2
        {
            //Заполняем массив
            int LineCount;
            int[,] arr;
            try
            {
                LineCount = File.ReadLines(@"C:\\input\input.txt").Count(); // количество строк в файле
                if (LineCount < 2 || Math.Sqrt(LineCount) % 1 != 0)
                {
                    Console.WriteLine("Квадратную матрицу не создать с помощью " + LineCount + " элементов. Перевод на ручной ввод...");
                    Console.ReadKey();
                    File.ReadLines(@"C:\\con");
                } // непрваильное число строк в файле? Вызываем ошибку и уходим в catch
                LineCount = Convert.ToInt32(Math.Sqrt(LineCount)); // теперь в этой переменной записано количество столбцов матрицы

                var reader = File.OpenText(@"C:\\input\input.txt");
                bool Achtung = false; // если вдруг не строка в файле, завершаем программу
                arr = new int[LineCount, LineCount];
                for (int i = 0; i < LineCount; i++)
                {
                    for (int j = 0; j < LineCount; j++)
                    {
                        Achtung = Int32.TryParse(reader.ReadLine(), out arr[i, j]);
                        if (!Achtung)
                        {
                            Console.WriteLine("У вас строка в текстовом файле, вы все сломали! Перевожу вас на ручной ввод...");
                            Console.ReadKey();
                            File.ReadLines(@"C:\\con");
                        }
                    }
                }
            }
            catch // если с файлом проблемы
            {
                Console.WriteLine("Файла для ввода не существует, там буквы или в нем неверное кол-во строк для корректной работы");
                Console.WriteLine("Введите количество элементов массива");
                LineCount = NumberFromUser(Int32.MaxValue); // Int32.MaxValue - это максимально допустимое число, с которым нет переполнения
                arr = new int[LineCount, LineCount];
                bool isNumber;
                for (int i = 0; i < LineCount; i++)
                {
                    for (int j = 0; j < LineCount; j++)
                    {
                        Console.WriteLine("Элемент массива " + i + ", " + j + " из " + (LineCount - 1) + ", " + (LineCount - 1));
                        isNumber = Int32.TryParse(Console.ReadLine(), out arr[i, j]);
                        if (!isNumber)
                        {
                            Console.WriteLine("Неправильное число");
                            j--;
                        }
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
            Console.WriteLine("\nМаксимальный элемент #" + BiggestNumberId + "\nЗначение: " + BiggestNumber);
            Console.WriteLine("\nМинимальный элемент #" + LowestNumberId + "\nЗначение: " + LowestNumber);

            // 2.3 произведение, сумма, разность массивов
            Console.WriteLine("\nДля арифметических действий возьмем первые 2 массива:");
            Int64 multiply = 1;
            int sum = 0, substract = 0; // произведение, сумма, вычитание
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
        static void AnyDimensionArray() // Задание 3
        {
            Console.WriteLine("Введите количество массивов: ");
            int ArraysAmount = NumberFromUser(Int32.MaxValue); // количество массивов
            int[][] arr = new int[ArraysAmount][];
            int ElementsAmount = 0; // количество элементов во всем ступенчатом массиве. Пригодится чтобы проверить верное ли количество строк в txt
            int LinesAmount; // временная переменная будет хранить в себе количество элементов в массиве каждой строки
            for (int i = 0; i < ArraysAmount; i++) // создание ступенчатого массива пока без значений
            {
                Console.Write("Размер массива #" + i + ": ");
                LinesAmount = NumberFromUser(Int32.MaxValue);
                ElementsAmount += LinesAmount;
                arr[i] = new int[LinesAmount]; // получаем длину массива на строке i
            }

            try
            {
                int LineCount = File.ReadLines(@"C:\\input\input.txt").Count(); // количество строк в файле
                if (LineCount != ElementsAmount)
                {
                    Console.WriteLine("\nКоличество строк в файле не соответствует введенным данным");
                    File.ReadLines(@"C:\\con"); // уходим в catch
                }

                var reader = File.OpenText(@"C:\\input\input.txt");
                for (int i = 0; i < ArraysAmount; i++)
                {
                    for (int j = 0; j < arr[i].Length; j++)
                    {
                        arr[i][j] = Convert.ToInt32(reader.ReadLine());
                    }
                }


            }
            catch // если с файлом проблемы
            {
                Console.WriteLine("Файла для ввода не существует, там буквы или в нем слишком мало строк для корректной работы программы");
                Console.WriteLine("");
                bool isNumber;
                for (int i = 0; i < ArraysAmount; i++)
                {
                    Console.WriteLine("\nСтрока #" + i);
                    for (int j = 0; j < arr[i].Length; j++)
                    {
                        Console.Write("Элемент массива " + j + " из " + (arr[i].Length - 1) + ": ");
                        isNumber = Int32.TryParse(Console.ReadLine(), out arr[i][j]);
                        if (!isNumber)
                        {
                            Console.WriteLine("Неправильное число");
                            j--;
                        }
                    }

                }
            }
            bool ifNumber;
            // 3.1 вывод одноступенчатого массива
            Console.WriteLine("\nМассив в виде матрицы: ");
            DisplayArray3(arr);
            // 3.2.1 изменить конкретный элемент массива

            // в данной части кода мы заставляем написать правильные координаты точки и новое значение
            Console.Write("\nВыберите номер массива (начиная с 0), в котором вы хотите внести изменение: ");
            ifNumber = Int32.TryParse(Console.ReadLine(), out int UserI);
            while (!ifNumber)
            {
                Console.WriteLine("Неверное число");
                ifNumber = Int32.TryParse(Console.ReadLine(), out UserI);
            }
            Console.Write("\nВыберите элемент массива (начиная с 0), в котором вы хотите внести изменение: ");
            ifNumber = Int32.TryParse(Console.ReadLine(), out int UserJ);
            while (!ifNumber)
            {
                Console.WriteLine("Неверное число");
                ifNumber = Int32.TryParse(Console.ReadLine(), out UserJ);
            }
            Console.Write("\nУкажите новое значение: ");
            ifNumber = Int32.TryParse(Console.ReadLine(), out int UserInt);
            while (!ifNumber)
            {
                Console.WriteLine("Неверное число");
                ifNumber = Int32.TryParse(Console.ReadLine(), out UserInt);
            }
            arr[UserI][UserJ] = UserInt;

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
        static void Main()
        {
            Console.WriteLine("Какой массив?\n1) Одномерный\n2) Двухмерный\n3) Ступенчатый");
            int Choice = NumberFromUser(3);
            switch (Choice)
            {
                case 1: // одномерный
                    OneDimensionArray();
                    break;
                case 2: // двухмерный (несколько массивов одинаковой длины)
                    TwoDimensionArray();
                    break;
                case 3: // ступенчатый (несколько массивов разной длины)
                    AnyDimensionArray();
                    break;
                default:
                    Console.WriteLine("Ты как сюда попал?");
                    break;
            }
        }
    }
}
