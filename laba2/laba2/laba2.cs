using System;
using System.IO;

namespace laba2
{
    class txt
    {
        public static string[][] read() // возврат данных из txt
        {
            string[][] arr = new string[100][]; // На самом деле тут можно задать массив любого размера, но для экономии памяти 100 - верхняя граница
            try // если произошли ошибки при записи данных в массив из файла, то начинаем его с нуля
            {
                using (var file = File.OpenText(@"C:\\input\laba2db.txt"))
                {
                    profile.profiles_count = Convert.ToInt32(file.ReadLine());
                    for (int i = 0; i < profile.profiles_count; i++)
                    {
                        arr[i] = new string[11];
                        for (int j = 0; j < 11; j++)
                        {
                            arr[i][j] = file.ReadLine();
                        }
                    }
                    file.Close();
                }
            }
            catch
            {
                Console.WriteLine("File is does not exist or the data was corrupted.\nPress any key to create a new one...");
                Console.ReadKey();
                arr = new string[100][];
                profile.profiles_count = 0;
            }
            return arr;
        }
        public static void write(string[][] arr) // запись данных обратно в txt
        {
            using (StreamWriter sw = new StreamWriter(@"C:\\input\laba2db.txt", false))
            {
                sw.WriteLine(profile.profiles_count);
                for (int i = 0; i < profile.profiles_count; i++)
                {
                    for (int j = 0; j < 11; j++)
                    {
                        sw.WriteLine(arr[i][j]);
                    }
                }
            }
        }
    }
    class profile
    {
        private static string[][] profile_array = txt.read(); // тут хранятся все профили
        public static int profiles_count; // количество профилей, мы не можем использовать profile_array.Length потому что профилей меньше, чем размер массива
        static string[] terms = {
            "ID",
            "Last name",
            "First name",
            "Middle name",
            "Day of birth",
            "Month of birth",
            "Year of birth",
            "Institute",
            "Group",
            "Course",
            "Average score"
        }; // терминология каждой строчки для сокращения размера кода

        public static int user_number(int lowest, int highest) // заставляет пользователя написать число в диапозоне lowest...highest
        {
            int user_answer;
            bool terminate;
            terminate = Int32.TryParse(Console.ReadLine(), out user_answer);
            while (!terminate || user_answer < lowest || user_answer > highest)
            {
                Console.Write("You should pick a number between " + lowest + "..." + highest + ": ");
                terminate = Int32.TryParse(Console.ReadLine(), out user_answer);
            }
            return user_answer;
        }
        private static int date_compare(string[] date12) // сравнение двух дат -1: date1 < date2 0: date1 = date2 1: date1 > date2
        {
            DateTime date1 = new DateTime(Int32.Parse(date12[2]), Int32.Parse(date12[1]), Int32.Parse(date12[0]));
            DateTime date2 = new DateTime(Int32.Parse(date12[5]), Int32.Parse(date12[4]), Int32.Parse(date12[3]));
            return DateTime.Compare(date1, date2);
        }
        private static string[] birth_check(string date_check) // проверка даты на правильность написания
        {
            int current_year = Convert.ToInt32(DateTime.Now.Year);
            int date_day, date_month, date_year;
            string[] splitted_date = new string[3]; //Day, Month, Year

            try
            {
                splitted_date = date_check.Split("/");
                date_day = Convert.ToInt32(splitted_date[0]);
                date_month = Convert.ToInt32(splitted_date[1]);
                date_year = Convert.ToInt32(splitted_date[2]);
                DateTime full_date = new DateTime(date_year, date_month, date_day);

                if (date_year > current_year - 17)
                {
                    return new string[1];
                }
                else
                {
                    return splitted_date;
                }
            }
            catch
            {
                return new string[1];
            }
        }
        private static string[] add() // создание нового профиля
        {
            profiles_count++;
            string[] profile_data = new string[11];

            Console.Clear();
            Console.Write("\n\nProfile ID: ");
            profile_data[0] = user_number(1, Int32.MaxValue).ToString();

            Console.Write("\nLast name: ");
            profile_data[1] = Console.ReadLine();
            Console.Write("First name: ");
            profile_data[2] = Console.ReadLine();
            Console.Write("Middle name: ");
            profile_data[3] = Console.ReadLine();

            Console.Write("\nDD/MM/YYYY of birth: ");
            string date_of_birth = Console.ReadLine();
            while (birth_check(date_of_birth).Length == 1)
            {
                Console.WriteLine("DD/MM/YYYY is not correct.");
                Console.Write("DD/MM/YYYY of birth: ");
                date_of_birth = Console.ReadLine();
            }
            for (int i = 0; i < 3; i++)
            {
                profile_data[i + 4] = birth_check(date_of_birth)[i];
            }

            Console.Write("\nInstitution: ");
            profile_data[7] = Console.ReadLine();

            Console.Write("\nGroup: ");
            profile_data[8] = Console.ReadLine();

            Console.Write("\nCourse: ");
            profile_data[9] = user_number(1, 4).ToString();

            Console.Write("\nAverage score: ");
            profile_data[10] = user_number(0, Int32.MaxValue).ToString();
            return profile_data;
        }
        private static string[] edit(string[] profile_data) // редактирование профиля
        {
            int user_option;
            Console.Clear();
            Console.WriteLine("\nProfile info:");
            for (int i = 0; i < 11; i++)
            {
                Console.WriteLine((i + 1) + ") " + terms[i] + ": " + profile_data[i]);
            }
            Console.Write("Choose element to edit: ");
            user_option = user_number(1, 11);
            string tempstr;
            switch (user_option) // 5, 6, 7 - это дата, ее нужно проверить на правильность, прежде чем записывать
            {
                case 1:
                    Console.Write("ID: ");
                    profile_data[0] = user_number(1, Int32.MaxValue).ToString();
                    break;
                case 5:
                    Console.Write(terms[4] + "'s new value: ");
                    tempstr = Console.ReadLine();
                    while (birth_check((tempstr + "/" + profile_data[5] + "/" + profile_data[6])).Length == 1)
                    {
                        Console.WriteLine("Wrong date");
                        Console.Write(terms[4] + "'s new value: ");
                        tempstr = Console.ReadLine();
                    }
                    profile_data[4] = tempstr;
                    break;
                case 6:
                    Console.Write(terms[5] + "'s new value: ");
                    tempstr = Console.ReadLine();
                    while (birth_check((profile_data[4] + "/" + tempstr + "/" + profile_data[6])).Length == 1)
                    {
                        Console.WriteLine("Wrong date");
                        Console.Write(terms[4] + "'s new value: ");
                        tempstr = Console.ReadLine();
                    }
                    profile_data[5] = tempstr;
                    break;
                case 7:
                    Console.Write(terms[6] + "'s new value: ");
                    tempstr = Console.ReadLine();
                    while (birth_check((profile_data[4] + "/" + profile_data[5] + "/" + tempstr)).Length == 1)
                    {
                        Console.WriteLine("Wrong date");
                        Console.Write(terms[5] + "'s new value: ");
                        tempstr = Console.ReadLine();
                    }
                    profile_data[6] = tempstr;
                    break;
                case 10:
                    Console.WriteLine(terms[9] + "'s new value: ");
                    profile_data[10] = user_number(0, Int32.MaxValue).ToString();
                    break;
                default:
                    Console.Write(terms[user_option - 1] + "'s new value: ");
                    profile_data[user_option - 1] = Console.ReadLine();
                    break;
            }
            return profile_data;
        }
        private static void sort_choose() // сортировка профиля по ID, ФИО, Дате рождени
        {
            int order; // вид сортировки
            bool if_descending; // убывает?
            string[][] temp_array = profile_array; // дублирование массива на всякий, потому что строки будут меняться местами
            Console.WriteLine("Choose an order:\n1) ID\n2) Full name\n3) Date of birth");
            Console.Write("Input: ");
            order = user_number(1, 3); 
            Console.Write("\n1) Ascending\n2) Descending\nInput: ");
            if (user_number(1, 2) == 1)
            {
                if_descending = false;
            }
            else
            {
                if_descending = true;
            }
            Console.Clear();

            string[] tempArray; // используется для замены элементов или для формата ввода в функцию date_compare
            switch (order)
            {
                case 1: // сортировка по id
                    for (int i = 0; i < profiles_count; i++)
                    {
                        for (int j = 0; j < profiles_count - 1; j++)
                        {
                            if (if_descending)
                            {
                                if (Int32.Parse(temp_array[j][0]) < Int32.Parse(temp_array[j + 1][0]))
                                {
                                    tempArray = temp_array[j];
                                    temp_array[j] = temp_array[j + 1];
                                    temp_array[j + 1] = tempArray;
                                }
                            }
                            else
                            {
                                if (Int32.Parse(temp_array[j][0]) > Int32.Parse(temp_array[j + 1][0]))
                                {
                                    tempArray = temp_array[j];
                                    temp_array[j] = temp_array[j + 1];
                                    temp_array[j + 1] = tempArray;
                                }
                            }
                        }
                    }
                    break;
                case 2: // сортировка по ФИО
                    string str1, str2;
                    for (int i = 0; i < profiles_count; i++)
                    {
                        for (int j = 0; j < profiles_count - 1; j++)
                        {
                            str1 = temp_array[j + 1][1] + temp_array[j + 1][2] + temp_array[j + 1][3];
                            str2 = temp_array[j][1] + temp_array[j][2] + temp_array[j][3];
                            str1.ToUpper();
                            str2.ToUpper();
                            if (if_descending)
                            {
                                if (str1.CompareTo(str2) == 1)
                                {
                                    tempArray = temp_array[j];
                                    temp_array[j] = temp_array[j + 1];
                                    temp_array[j + 1] = tempArray;
                                }
                            }
                            else
                            {
                                if (str1.CompareTo(str2) == -1)
                                {
                                    tempArray = temp_array[j];
                                    temp_array[j] = temp_array[j + 1];
                                    temp_array[j + 1] = tempArray;
                                }
                            }

                        }
                    }
                    break;
                case 3: // сортировка по Дате Рождения
                    for (int i = 0; i < profiles_count; i++)
                    {
                        for (int j = 0; j < profiles_count - 1; j++)
                        {
                            tempArray = new string[] { temp_array[j][4], temp_array[j][5], temp_array[j][6], temp_array[j + 1][4], temp_array[j + 1][5], temp_array[j + 1][6] };
                            if (if_descending)
                            {
                                if (date_compare(tempArray) == 1)
                                {
                                    tempArray = temp_array[j];
                                    temp_array[j] = temp_array[j + 1];
                                    temp_array[j + 1] = tempArray;
                                }
                            }
                            else
                            {
                                if (date_compare(tempArray) == -1)
                                {
                                    tempArray = temp_array[j];
                                    temp_array[j] = temp_array[j + 1];
                                    temp_array[j + 1] = tempArray;
                                }
                            }
                        }
                    }
                    break;
            }
            display(temp_array, profiles_count);
            profile_array = temp_array; // я убедился, что temp_array был не нужен(ниче не меняется), но удалять его не буду
            Console.Write("\n1) Edit a line\n2) Search\n3) Lobby\nInput: ");
            switch (user_number(1, 3))
            {
                case 1: // редактирование строки
                    Console.Write("\n\nChoose a line: ");
                    int user_line = user_number(1, profiles_count);
                    Console.Write("\nWhat to do with line #" + user_line + "?\n1) Delete\n2) Edit\n3) Exit\n");
                    Console.Write("Input: ");

                    switch (user_number(1, 3))
                    {
                        case 1:
                            profiles_count--;
                            for (int i = user_line - 1; i < profiles_count; i++)
                            {
                                profile_array[i] = profile_array[i + 1];
                            }
                            break;
                        case 2:
                            edit(profile_array[user_line - 1]);
                            break;
                        case 3:
                            break;
                    }
                    break;
                case 2: // поиск по ключевому слову
                    string keyword; // само ключевое слово
                    int category; // категория, в которой слово искать
                    int counter = 0; // для слежения за индексами
                    string[][] keyword_array; // массив в котором только совпадения для вывода
                    int[] lines_with_keyword; // количество строк с совпадениями, чтобы изменить верную строку массива без совпадений
                    int lines_count = 0; // количество совпадений
                    Console.Write("Choose a category:\n");
                    for (int i = 1; i < 12; i++)
                    {
                        Console.WriteLine(i + ") " + terms[i - 1]);
                    }
                    Console.Write("Input: ");
                    category = user_number(1, 11) - 1;
                    Console.Write("Enter a keyword: ");
                    keyword = Console.ReadLine();
                    for (int i = 0; i < profiles_count; i++) // запись количества совпадений
                    {
                        if (profile_array[i][category].ToUpper().Contains(keyword.ToUpper())) 
                        {
                            lines_count++;
                        }
                    }
                    if (lines_count == 0)
                    {
                        Console.Write("\nNo any profiles with this keyword were found\nPress any key...");
                        Console.ReadKey();
                    }
                    else
                    {
                        keyword_array = new string[lines_count][];
                        lines_with_keyword = new int[lines_count];
                        for (int i = 0; i < profiles_count; i++)
                        {
                            if (profile_array[i][category].ToUpper().Contains(keyword.ToUpper()))
                            {
                                keyword_array[counter] = profile_array[i];
                                lines_with_keyword[counter] = i;
                                counter++;
                            }
                        }
                        display(keyword_array, lines_count);
                        Console.Write("Choose a line: ");
                        int keyword_user_line = user_number(1, lines_count);
                        profile_array[keyword_user_line] = edit(keyword_array[keyword_user_line - 1]); // запись измененной строки обратно в главный массив
                    }
                    break;
                case 3:
                    break;
            }

        }
        private static void display(string[][] temp_array, int temp_length)
        {
            int[] max_category_length = new int[11]; // для красоты :) , чтобы не было "лесенки" при выводе
            for (int i = 0; i < 11; i++)
            {
                max_category_length[i] = terms[i].Length;
            }
            for (int i = 0; i < temp_length; i++) //  ищем колонку, в которой нужно больше всего свободного места
            {
                for (int j = 0; j < 11; j++)
                {
                    if (temp_array[i][j].Length > max_category_length[j])
                    {
                        max_category_length[j] = temp_array[i][j].Length;
                    }
                }
            }
            Console.Write("Full table of profiles:\n   ");
            string spaces;
            for (int i = 0; i < 11; i++) // выбираем правильное количество пробелов в заголовке
            {
                Console.Write(terms[i] + new string(' ', max_category_length[i] - terms[i].Length) + "  ");
            }
            for (int i = 0; i < temp_length; i++) // вывод данных профилей с пробелами
            {
                Console.Write("\n" + (i + 1) + ") ");
                for (int j = 0; j < 11; j++)
                {
                    spaces = new string(' ', (max_category_length[j] - temp_array[i][j].Length));
                    Console.Write(temp_array[i][j] + spaces + "  ");
                }
            }
            // названия переменных говорят сами за себя, комментировать не буду
            int min_score = Int32.MaxValue; 
            int max_score = Int32.MinValue;
            int avg_score = 0;
            int sum_score = 0;
            int profile_score;
            for (int i = 0; i < temp_length; i++)
            {
                profile_score = Convert.ToInt32(temp_array[i][10]);
                if (profile_score < min_score) { min_score = profile_score; }
                if (profile_score > max_score) { max_score = profile_score; }
                avg_score += profile_score;
                sum_score += profile_score;
            }
            Console.WriteLine("\n\nBiggest score: " + max_score + "  Lowest score: " + min_score + "  Average score: " + avg_score / temp_length + "  Summary score: " + sum_score);
        }
        private static int arrow_menu(string[] options_arr) // менюшка с управлением через стрелочки, можно без нее, но красиво
        {
            ConsoleKeyInfo key;
            int number_of_options = options_arr.Length;
            int user_option = 1;
            bool enter_pressed = false;
            while (!enter_pressed)
            {
                Console.Clear();
                Console.WriteLine("\nPress enter or use arrows to navigate:\n");

                for (int i = 1; i < number_of_options + 1; i++)
                {
                    if (user_option == i) { Console.WriteLine("-->  " + options_arr[i - 1].ToUpper()); }
                    else { Console.WriteLine(options_arr[i - 1]); }
                }

                key = Console.ReadKey(); // распознаем кнопку и изменяем user_options в зависимости от нее.
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        enter_pressed = true;
                        break;

                    case ConsoleKey.UpArrow:
                        if (user_option == 1) { user_option = number_of_options; }
                        else { user_option--; }
                        break;

                    case ConsoleKey.DownArrow:
                        if (user_option == number_of_options) { user_option = 1; }
                        else { user_option++; }
                        break;

                    default:
                        break;
                }
            }
            return user_option;
        }
        public static bool lobby()
        {
            Console.WriteLine(profile_array); // для инициализации массива, нужно совершить с ним действие
            Console.Clear();
            bool terminate = false;
            switch (arrow_menu(new string[] { "Add new profile", "View Data", "Exit and save" }))
            {
                case 1:
                    profile_array[profiles_count] = add();
                    break;
                case 2:
                    if (profile_array[0][0] == "")
                    {
                        Console.WriteLine("Profiles not found\n Press any key to create a profile...");
                        Console.ReadKey();
                    }
                    else
                    {
                        sort_choose();
                    }
                    break;
                case 3:
                    txt.write(profile_array);
                    terminate = true;
                    break;
            }
            return terminate;
        }
    }
    class simplify_main
    {
        static void Main()
        {
            bool terminate = false;
            while (!terminate)
            {
                terminate = profile.lobby();
            }
            Console.Clear();
            Console.WriteLine("\nSee you later!");
        }
    }
}
