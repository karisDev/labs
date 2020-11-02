using System;
using System.IO;
using System.Linq;

namespace laba1
{
    class Laba1
    {
        public static int NumberFromUser(int MinOption, int MaxOption) // функция, заставляет пользователя вернуть число в диапазоне от 1 до OptionsAmount
        {
            int UserNumber;
            Console.Write("Ввод: ");
            while (!Int32.TryParse(Console.ReadLine(), out UserNumber) || UserNumber > MaxOption || UserNumber < MinOption)
            {
                Console.Write("Введите число от " + MinOption + " до " + MaxOption + ": ");
            }
            return UserNumber;
        }
        public static int[] FileInputReciever() // возврат массива длинной 0 если с файлом проблемы
        {
            int LineCount;
            int[] arr;
            try
            {
                LineCount = File.ReadLines(@"C:\\input\input.txt").Count(); // количество строк в файле
                var reader = File.OpenText(@"C:\\input\input.txt");
                bool Achtung;
                arr = new int[LineCount];
                for (int i = 0; i < LineCount; i++)
                {
                    Achtung = Int32.TryParse(reader.ReadLine(), out arr[i]);
                    if (!Achtung)
                    {
                        File.ReadLines(@"C:\\con");
                    }
                }
            }
            catch
            {
                return new int[0];
            }
            return arr;
        }
        static void Main()
        {
            Console.WriteLine("ДИСКЛЕЙМЕР: Нужно создать файл 'C:\\\\input\\input.txt'.\n1 строка - 1 элемент массива. Если файла нет или с ним проблемы, то данные будете вводить вручную.\n");
            Console.WriteLine("Какой массив?\n1) Одномерный\n2) Двухмерный\n3) Ступенчатый\n");
            int Choice = NumberFromUser(1, 3);
            switch (Choice)
            {
                case 1: // одномерный
                    OneDimensionalArray.Solve();
                    break;
                case 2: // двухмерный (несколько массивов одинаковой длины)
                    TwoDimensionalArray.Solve();
                    break;
                case 3: // ступенчатый (несколько массивов разной длины)
                    AnyDimensionalArray.Solve();
                    break;
            }
        }
    }
}
