using System;
using System.Linq;

namespace laba1
{
    class OneDimensionalArray
    {
        private static void DisplayArray1(int[] arr) // вывод одномерного массива
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
        }
        public static void Solve() // Задание 1
        {
            //Заполняем массив
            int[] arr = Laba1.FileInputReciever();
            int LineCount = arr.Length; // из файла
            
            if (LineCount == 0) // вручную
            {
                Console.WriteLine("\nФайла не существует или в нем присутствуют не цифры.");
                Console.WriteLine("\nВведите количество элементов массива.\n");
                LineCount = Laba1.NumberFromUser(1, Int32.MaxValue); // Int32.MaxValue - это максимально допустимое число, с которым нет переполнения
                arr = new int[LineCount];
                for (int i = 0; i < LineCount; i++)
                {
                    Console.WriteLine("Элемент массива " + i + " из " + (LineCount - 1) + ".");
                    arr[i] = Laba1.NumberFromUser(Int32.MinValue, Int32.MaxValue);
                }
            }
            // С заполнением массива разобрались

            //НАЧАЛО ВЫПОЛНЕНИЯ ПО ПУНКТАМ
            Console.WriteLine("\nВариант А (Ручками)");
            // 1.а.1 вывод массива
            Console.WriteLine("\nМассив целиком: ");
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
            Console.WriteLine("\n\nМаксимальный элемент #" + BiggestNumberId + "\nЗначение: " + BiggestNumber);
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
            if(TempArr.Length == 0)
            {
                Console.WriteLine("Четных элементов не найдено.");
            }
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
    }
}
