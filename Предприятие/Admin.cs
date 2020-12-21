using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;

namespace Предприятие
{
    class Admin
    {
        static string ReadPasswordLine()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);
                        int pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            return password;
        }

        static strelki str = new strelki();
        static public int ViewMenu(string[] menuText)
        {
            Console.Clear();
            foreach (var text in menuText)
                Console.Write(text);
            int chek = str.CheckerMenu(menuText.Length);
            Console.Clear();
            return chek;
        }

        public void Admin_panel()
        {
            Console.Clear();
            string[] menuText = { "Выберите\n", "  1)Создать пользователя\n", "  2)Создание филиала\n", "  3)Изменить логин / пароль / ФИО пользователя\n", "  4)Просмотреть данные пользователей\n", "  5)Просмотреть сведения о своей заработной плате\n", "  6)Удалить филиал\n", "  7)Выход" };
            Authorization auth = new Authorization();
            while (true)
            {

                int chek = ViewMenu(menuText);

                switch (chek)
                {
                    case 1:
                        Create();
                        break;
                    case 2:
                        Filial();
                        break;
                    case 3:
                        Redact();
                        break;
                    case 4:
                        LookDanie();
                        break;
                    case 5:
                        LookZP();
                        break;
                    case 6:
                        DelFilial();
                        break;
                    case 7:
                        MenuVibor lol = new MenuVibor();
                        lol.Vibor();
                        break;
                }
            }
        }

        public void Create()
        {
            string path11 = Directory.GetCurrentDirectory();
            Console.Clear();
            Console.WriteLine("Создайте работника организации");
        r1: Console.Write("Введите логин: ");
            string st_login = Console.ReadLine();
            if (st_login == "" || st_login == " " || st_login == null || Regex.IsMatch(st_login, @"^[а-яА-Я]+$") || st_login.Contains(" ") || st_login.Length < 2)
            {
                Console.WriteLine("Error!");
                Thread.Sleep(2000);
                goto r1;
            }

            DirectoryInfo dir2 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\Бухгалтер");
            foreach (var item in dir2.GetFiles())
            {
                if (st_login + ".login" == item.Name || st_login == "Admin" || st_login == "admin")
                {
                    Console.WriteLine("Логин уже существует!!");
                    goto r1;
                }
            }
            dir2 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\Кассир-продавец");
            foreach (var item in dir2.GetFiles())
            {
                if (st_login + ".login" == item.Name || st_login == "Admin" || st_login == "admin")
                {
                    Console.WriteLine("Логин уже существует!!");
                    goto r1;
                }
            }
            dir2 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\Кладовщик");
            foreach (var item in dir2.GetFiles())
            {
                if (st_login + ".login" == item.Name || st_login == "Admin" || st_login == "admin")
                {
                    Console.WriteLine("Логин уже существует!!");
                    goto r1;
                }
            }
            dir2 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\Отдел кадров");
            foreach (var item in dir2.GetFiles())
            {
                if (st_login + ".login" == item.Name || st_login == "Admin" || st_login == "admin")
                {
                    Console.WriteLine("Логин уже существует!!");
                    goto r1;
                }
            }
            dir2 = new DirectoryInfo(path11 + @"\Данные\Сотрудники");
            foreach (var item in dir2.GetFiles())
            {
                if (st_login + ".login" == item.Name || st_login == "Admin" || st_login == "admin")
                {
                    Console.WriteLine("Логин уже существует!!");
                    goto r1;
                }
            }
            dir2 = new DirectoryInfo(path11 + @"\Данные\Покупатели");
            foreach (var item in dir2.GetFiles())
            {
                if (st_login + ".login" == item.Name || st_login == "Admin" || st_login == "admin")
                {
                    Console.WriteLine("Логин уже существует!!");
                    Thread.Sleep(2000);
                    goto r1;
                }
            }

        r2: Console.Write("Введите пароль: ");
            string password = ReadPasswordLine();
            if (password.Length < 8)
            {
                Console.WriteLine();
                Console.WriteLine("Длина пароля не соответствует требованиям безопасности");
                Thread.Sleep(2000);
                goto r2;
            }
            int g1 = 0, g2 = 0, g3 = 0, g4 = 0, g5 = 0, g6 = 0, g7 = 0;
            for (int i = 0; i < password.Length; i++)
            {
                Regex regex = new Regex("([A-Z])");
                g1 = regex.Matches(password).Count;
                if (password[i] == '!' || password[i] == '@' || password[i] == '#' || password[i] == '$' || password[i] == '%' || password[i] == '&' || password[i] == '*')
                    g2++;
                if (((password[i] >= 'А') && (password[i] <= 'Я')) || ((password[i] >= 'а') && (password[i] <= 'я')) || password[i] == 'ё')
                    g4++;
                if (password == null || password == "")
                    g5++;
                if (password.Contains(" "))
                    g6++;
                if (g7 != 3)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if ((password[i] >= 'A' && password[i] <= 'Z') && (password[i++] >= 'A' && password[i++] <= 'Z'))
                            g7++;
                        if (g7 >= 3)
                        {
                            g7 = 3;
                        }
                    }
                }
            }

            foreach (char ch in password)
                if (Char.IsDigit(ch))
                    g3++;

            if (g1 < 3)
            {
                Console.WriteLine();
                Console.WriteLine("Минимум 3 заглавных буквы!");
                Thread.Sleep(2000);
                goto r2;
            }
            if (g2 < 1)
            {
                Console.WriteLine();
                Console.WriteLine("Минимум 2 спец. символа!");
                Thread.Sleep(2000);
                goto r2;
            }
            if (g3 < 3)
            {
                Console.WriteLine();
                Console.WriteLine("Минимум 3 цифры!");
                Thread.Sleep(2000);
                goto r2;
            }
            if (g4 > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Никаких русских символов!");
                Thread.Sleep(2000);
                goto r2;
            }
            if (g5 > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Вы ввели пустой пароль");
                Thread.Sleep(2000);
                goto r2;
            }
            if (g6 > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Пароль не должен содержать пробелов");
                Thread.Sleep(2000);
                goto r2;
            }
            if (g7 >= 3)
            {
                Console.WriteLine();
                Console.WriteLine("3 заглавных буквы и более подряд нельзя!");
                Thread.Sleep(2000);
                goto r2;
            }

            Console.WriteLine();
            st: string fam = "", ima = "", otch = " ";
            Console.Write("Введите фамилию: ");
            fam = Console.ReadLine();
            Console.Write("Введите имя: ");
            ima = Console.ReadLine();
            Console.Write("Введите отчество: ");
            otch = Console.ReadLine();
            if (fam == "" || ima == "" || !Regex.IsMatch(fam, @"^[а-яА-Я]+$") || !Regex.IsMatch(ima, @"^[а-яА-Я]+$") || Regex.IsMatch(fam, @"^[a-zA-Z]+$") || Regex.IsMatch(ima, @"^[a-zA-Z]+$") || fam.Contains(" ") || ima.Contains(" "))
            {
                Console.WriteLine("Вы некорректно ввели данные!");
                Thread.Sleep(2000);
                Console.Clear();
                goto st;
            }

            Console.WriteLine();
            string[] menuText = { "Является ли данный сотрудник работником отдела кадров?\n", "  1)ДА\n", "  2)НЕТ\n"};
            while (true)
            {
                int chek = ViewMenu(menuText);
                switch (chek)
                {
                    case 1:
                        Directory.CreateDirectory(path11 + @"\Данные\Сотрудники\Отдел кадров\" + st_login);
                        BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\Отдел кадров\"+st_login+".login", FileMode.OpenOrCreate));
                        writer.Write(st_login);
                        writer.Write(password);
                        writer.Write(fam);
                        writer.Write(ima);
                        writer.Write(otch);
                        writer.Close();

                        writer = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\Отдел кадров\" + st_login + @"\zarplata.zp", FileMode.OpenOrCreate));
                        writer.Write("85000");
                        writer.Close();


                        Console.Clear();
                        Console.WriteLine("Вы создали пользователя: ");
                        Console.WriteLine("Логин: " + st_login);
                        Console.WriteLine("Пароль: " + password);
                        Console.WriteLine("Должность: Сотрудник отдела кадров");
                        Console.WriteLine("Фамилия: " + fam);
                        Console.WriteLine("Имя: "+ima);
                        Console.WriteLine("Отчество (если было указано): " + otch);
                        Console.WriteLine("Зарплата: 85000 рублей");
                        Thread.Sleep(6000);
                        Console.Clear();
                        Admin_panel();
                        break;
                    case 2:
                        writer = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + st_login + ".login", FileMode.OpenOrCreate));
                        writer.Write(st_login);
                        writer.Write(password);
                        writer.Write(fam);
                        writer.Write(ima);
                        writer.Write(otch);
                        writer.Close();

                        Console.Clear();
                        Console.WriteLine("Вы создали пользователя: ");
                        Console.WriteLine("Логин: " + st_login);
                        Console.WriteLine("Пароль: " + password);
                        Console.WriteLine("Должность: Не указана");
                        Console.WriteLine("Фамилия: " + fam);
                        Console.WriteLine("Имя: " + ima);
                        Console.WriteLine("Отчество (если было указано): " + otch);
                        Thread.Sleep(6000);
                        Console.Clear();
                        Admin_panel();
                        break;
                }
            }
        }

        public void Filial()
        {
            rgh1: Console.Clear();
            string path11 = Directory.GetCurrentDirectory();
            string filo = "", gorop = "";
            string[] nums22 = new string[10000];
            int[] checker33 = new int[1000];
            int j = 1;
            DirectoryInfo dir = new DirectoryInfo(path11 + @"\Данные\Сотрудники\");
            foreach (var item1 in dir.GetFiles())
            {
                string lol1 = item1.Name;
                string check1 = lol1.Substring(lol1.Length - 7);
                nums22[j] = item1.Name;
                BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + item1.Name, FileMode.Open));
                int h = 1;
                while (reader.PeekChar() > -1)
                {
                    string name1 = reader.ReadString();
                    if (h == 1)
                        filo = name1;
                    if (h == 2)
                        gorop = name1;
                    h++;
                }
                reader.Close();
                if (check1 == ".filial")
                {
                    //Console.WriteLine(j + ") " + filo + " (" + gorop + ") ");
                    checker33[j] = j;
                    j++;
                }
            }
                r1:
                Console.Write("Введите название филиала: ");
                string name = Console.ReadLine();
                Console.Write("Введите город филиала '" + name + "': ");
                string gorod = Console.ReadLine();

                try
                {
                    DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\");
                    foreach (var item2 in dir1.GetFiles())
                    {
                        string gfg = Path.GetFileNameWithoutExtension(item2.Name);
                        if (gfg == name)
                        {
                            Console.WriteLine("Филиал уже существует!");
                            Thread.Sleep(3000);
                            Console.Clear();
                            goto r1;
                        }
                    }
                    if (name == "" || name == " " || name == null)
                    {
                        Console.WriteLine("Введите название филиала");
                        Thread.Sleep(3000);
                        Console.Clear();
                        goto r1;
                    }
                    BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + name + ".filial", FileMode.OpenOrCreate));
                    writer.Write(name);
                    writer.Write(gorod);
                    writer.Close();
                    Console.WriteLine("Филиал успешно создан!");
                    Thread.Sleep(3000);
                    Admin_panel();
                }
                catch
                {
                    Console.WriteLine("Название не удовлетворяет текущим требованиям!");
                    Thread.Sleep(5000);
                    Console.Clear();
                    goto r1;
                }
        }

        public void Redact()
        {
            string path11 = Directory.GetCurrentDirectory();
            Console.Clear();
            string dolzh = "";
            string[] menuText = { "Выберите должность:\n", "  1)Бухгалтер\n", "  2)Кассир-продавец\n", "  3)Кладовщик\n", "  4)Сотрудник отдела кадров\n" };
            while (true)
            {
                int chek = ViewMenu(menuText);
                switch (chek)
                {
                    case 1:
                        dolzh = "Бухгалтер";
                        goto lolkin;
                        break;
                    case 2:
                        dolzh = "Кассир-продавец";
                        goto lolkin;
                        break;
                    case 3:
                        dolzh = "Кладовщик";
                        goto lolkin;
                        break;
                    case 4:
                        dolzh = "Отдел кадров";
                        goto lolkin;
                        break;
                }
            }

            lolkin:
            Console.WriteLine(dolzh);
            string fam = "", ima = "", otch = "", pas = "", login = "";
            st1:
            Console.Clear();
            string[] nums2 = new string[10000];
            int[] checker3 = new int[1000];
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\"+dolzh);
            int j = 1;
            //string fam = "", ima = "", otch = "";
            foreach (var item in dir1.GetFiles())
            {
                if (item.Name != "zarplata.zrp")
                {
                    nums2[j] = item.Name;
                    BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + item.Name, FileMode.Open));
                    int h = 1;
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (h == 1)
                            login = name;
                        if (h == 2)
                            pas = name;
                        if (h == 3)
                            fam = name;
                        if (h == 4)
                            ima = name;
                        if (h == 5)
                            otch = name;
                        h++;
                    }
                    reader.Close();

                    Console.WriteLine(j + ") " + fam + " " + ima + " " + otch);
                    checker3[j] = j;
                    j++;
                }
            }
            if (j == 1)
            {
                Console.WriteLine("Нет пользователей");
                Thread.Sleep(2000);
                Admin_panel();
            }
            int j2 = 0;
            bool flag1 = true;
            if (j > 1)
            {
            st111: Console.Write("Выбор: ");
                string otvetr = Console.ReadLine();
                try
                {
                    j2 = Convert.ToInt32(otvetr);
                }
                catch
                {
                    Console.WriteLine("Error!");
                    Thread.Sleep(2000);
                    goto st111;
                }

                flag1 = false;
                for (int i1 = 0; i1 < j; ++i1)
                {
                    if (checker3[i1] == j2 && checker3[i1] != 0)
                    {
                        flag1 = true;
                    }
                }

                if (flag1 == false)
                {
                    Console.WriteLine("Error");
                    Thread.Sleep(2000);
                    goto st1;
                }

                if (flag1 == true)
                {
                    string[] menuText1 = { "Выберите что нужно изменить:\n", "  1)Логин\n", "  2)Пароль\n", "  3)ФИО\n" };
                    while (true)
                    {
                        int chek1 = ViewMenu(menuText1);
                        switch (chek1)
                        {
                            case 1:
                            r1: Console.Clear();
                                Console.Write("Введите новый логин: ");
                                string st_login = Console.ReadLine();

                                if (st_login == "" || st_login == " " || st_login == null || Regex.IsMatch(st_login, @"^[а-яА-Я]+$") || st_login.Contains(" ") || st_login.Length < 2)
                                {
                                    Console.WriteLine("Error!");
                                    Thread.Sleep(2000);
                                    goto r1;
                                }

                                DirectoryInfo dir2 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\Бухгалтер");
                                foreach (var item in dir2.GetFiles())
                                {
                                    if (st_login + ".login" == item.Name || st_login == "Admin" || st_login == "admin")
                                    {
                                        Console.WriteLine("Логин уже существует!!");
                                        Thread.Sleep(2000);
                                        goto r1;
                                    }
                                }
                                dir2 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\Кассир-продавец");
                                foreach (var item in dir2.GetFiles())
                                {
                                    if (st_login + ".login" == item.Name || st_login == "Admin" || st_login == "admin")
                                    {
                                        Console.WriteLine("Логин уже существует!!");
                                        Thread.Sleep(2000);
                                        goto r1;
                                    }
                                }
                                dir2 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\Кладовщик");
                                foreach (var item in dir2.GetFiles())
                                {
                                    if (st_login + ".login" == item.Name || st_login == "Admin" || st_login == "admin")
                                    {
                                        Console.WriteLine("Логин уже существует!!");
                                        Thread.Sleep(2000);
                                        goto r1;
                                    }
                                }
                                dir2 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\Отдел кадров");
                                foreach (var item in dir2.GetFiles())
                                {
                                    if (st_login + ".login" == item.Name || st_login == "Admin" || st_login == "admin")
                                    {
                                        Console.WriteLine("Логин уже существует!!");
                                        Thread.Sleep(2000);
                                        goto r1;
                                    }
                                }
                                dir2 = new DirectoryInfo(path11 + @"\Данные\Сотрудники");
                                foreach (var item in dir2.GetFiles())
                                {
                                    if (st_login + ".login" == item.Name || st_login == "Admin" || st_login == "admin")
                                    {
                                        Console.WriteLine("Логин уже существует!!");
                                        Thread.Sleep(2000);
                                        goto r1;
                                    }
                                }
                                dir2 = new DirectoryInfo(path11 + @"\Данные\Покупатели");
                                foreach (var item in dir2.GetFiles())
                                {
                                    if (st_login + ".login" == item.Name || st_login == "Admin" || st_login == "admin")
                                    {
                                        Console.WriteLine("Логин уже существует!!");
                                        Thread.Sleep(2000);
                                        goto r1;
                                    }
                                }

                                BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + nums2[j2], FileMode.Open));
                                int h = 1;
                                while (reader.PeekChar() > -1)
                                {
                                    string name = reader.ReadString();
                                    if (h == 1)
                                        login = name;
                                    if (h == 2)
                                        pas = name;
                                    if (h == 3)
                                        fam = name;
                                    if (h == 4)
                                        ima = name;
                                    if (h == 5)
                                        otch = name;
                                    h++;
                                }
                                reader.Close();

                                BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + st_login + ".login", FileMode.OpenOrCreate));
                                writer.Write(st_login);
                                writer.Write(pas);
                                writer.Write(fam);
                                writer.Write(ima);
                                writer.Write(otch);
                                writer.Close();

                                File.Delete(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + login + ".login");
                                Directory.Move(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + login, path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + st_login);
                                Console.WriteLine("Логин изменён!");
                                Thread.Sleep(5000);
                                Console.Clear();
                                Admin_panel();
                                break;

                            case 2:
                            r2: Console.Clear();
                                Console.Write("Введите новый пароль: ");
                                string password = ReadPasswordLine();
                                if (password.Length < 8)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Длина пароля не соответствует требованиям безопасности");
                                    Thread.Sleep(2000);
                                    goto r2;
                                }
                                int g1 = 0, g2 = 0, g3 = 0, g4 = 0, g5 = 0, g6 = 0, g7 = 0;
                                for (int i = 0; i < password.Length; i++)
                                {
                                    Regex regex = new Regex("([A-Z])");
                                    g1 = regex.Matches(password).Count;
                                    if (password[i] == '!' || password[i] == '@' || password[i] == '#' || password[i] == '$' || password[i] == '%' || password[i] == '&' || password[i] == '*')
                                        g2++;
                                    if (((password[i] >= 'А') && (password[i] <= 'Я')) || ((password[i] >= 'а') && (password[i] <= 'я')) || password[i] == 'ё')
                                        g4++;
                                    if (password == null || password == "")
                                        g5++;
                                    if (password.Contains(" "))
                                        g6++;
                                    if (g7 != 3)
                                    {
                                        for (int j24 = 0; j24 < 3; j24++)
                                        {
                                            if ((password[i] >= 'A' && password[i] <= 'Z') && (password[i++] >= 'A' && password[i++] <= 'Z'))
                                                g7++;
                                            if (g7 >= 3)
                                            {
                                                g7 = 3;
                                            }
                                        }
                                    }
                                }

                                foreach (char ch in password)
                                    if (Char.IsDigit(ch))
                                        g3++;

                                if (g1 < 3)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Минимум 3 заглавных буквы!");
                                    Thread.Sleep(2000);
                                    goto r2;
                                }
                                if (g2 < 1)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Минимум 2 спец. символа!");
                                    Thread.Sleep(2000);
                                    goto r2;
                                }
                                if (g3 < 3)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Минимум 3 цифры!");
                                    Thread.Sleep(2000);
                                    goto r2;
                                }
                                if (g4 > 0)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Никаких русских символов!");
                                    Thread.Sleep(2000);
                                    goto r2;
                                }
                                if (g5 > 0)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Вы ввели пустой пароль");
                                    Thread.Sleep(2000);
                                    goto r2;
                                }
                                if (g6 > 0)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Пароль не должен содержать пробелов");
                                    Thread.Sleep(2000);
                                    goto r2;
                                }
                                if (g7 >= 3)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("3 заглавных буквы и более подряд нельзя!");
                                    Thread.Sleep(2000);
                                    goto r2;
                                }

                                reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\"+ dolzh + @"\" + nums2[j2], FileMode.Open));
                                h = 1;
                                while (reader.PeekChar() > -1)
                                {
                                    string name = reader.ReadString();
                                    if (h == 1)
                                        login = name;
                                    if (h == 2)
                                        pas = name;
                                    if (h == 3)
                                        fam = name;
                                    if (h == 4)
                                        ima = name;
                                    if (h == 5)
                                        otch = name;
                                    h++;
                                }
                                reader.Close();
                                File.Delete(path11 + @"\Данные\Сотрудники\"+ dolzh + @"\" + login + ".login");
                                writer = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\"+dolzh + @"\" + login + ".login", FileMode.OpenOrCreate));
                                writer.Write(login);
                                writer.Write(password);
                                writer.Write(fam);
                                writer.Write(ima);
                                writer.Write(otch);
                                writer.Close();
                                Console.WriteLine("Пароль изменён!");
                                Thread.Sleep(5000);
                                Console.Clear();
                                Admin_panel();
                                break;
                            case 3:
                            st: Console.Clear();
                                fam = ""; ima = ""; otch = " ";
                                Console.Write("Введите фамилию: ");
                                fam = Console.ReadLine();
                                Console.Write("Введите имя: ");
                                ima = Console.ReadLine();
                                Console.Write("Введите отчество: ");
                                otch = Console.ReadLine();
                                if (fam == "" || ima == "" || !Regex.IsMatch(fam, @"^[а-яА-Я]+$") || !Regex.IsMatch(ima, @"^[а-яА-Я]+$") || Regex.IsMatch(fam, @"^[a-zA-Z]+$") || Regex.IsMatch(ima, @"^[a-zA-Z]+$") || fam.Contains(" ") || ima.Contains(" "))
                                {
                                    Console.WriteLine("Вы некорректно ввели данные!");
                                    Thread.Sleep(2000);
                                    Console.Clear();
                                    goto st;
                                }

                                reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\"+ dolzh + @"\" + nums2[j2], FileMode.Open));
                                h = 1;
                                string fam1 = "", ima1 = "", otch1 = "";
                                while (reader.PeekChar() > -1)
                                {
                                    string name = reader.ReadString();
                                    if (h == 1)
                                        login = name;
                                    if (h == 2)
                                        pas = name;
                                    if (h == 3)
                                        fam1 = name;
                                    if (h == 4)
                                        ima1 = name;
                                    if (h == 5)
                                        otch1 = name;
                                    h++;
                                }
                                reader.Close();
                                File.Delete(path11 + @"\Данные\Сотрудники\"+ dolzh + @"\" + login + ".login");
                                writer = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\"+ dolzh + @"\" + login + ".login", FileMode.OpenOrCreate));
                                writer.Write(login);
                                writer.Write(pas);
                                writer.Write(fam);
                                writer.Write(ima);
                                writer.Write(otch);
                                writer.Close();
                                Console.WriteLine("ФИО изменены!");
                                Thread.Sleep(5000);
                                Console.Clear();
                                Admin_panel();
                                break;
                        }
                    }
                }
            }
        }
        public void LookDanie()
        {
            string path11 = Directory.GetCurrentDirectory();
            Console.Clear();
            string dolzh = "";
            string[] menuText = { "Выберите должность:\n", "  1)Бухгалтер\n", "  2)Кассир-продавец\n", "  3)Кладовщик\n", "  4)Сотрудник отдела кадров\n" };
            while (true)
            {
                int chek = ViewMenu(menuText);
                switch (chek)
                {
                    case 1:
                        dolzh = "Бухгалтер";
                        goto lolkin;
                        break;
                    case 2:
                        dolzh = "Кассир-продавец";
                        goto lolkin;
                        break;
                    case 3:
                        dolzh = "Кладовщик";
                        goto lolkin;
                        break;
                    case 4:
                        dolzh = "Отдел кадров";
                        goto lolkin;
                        break;
                }
            }
            lolkin:
            Console.WriteLine(dolzh);
            string fam = "", ima = "", otch = "", pas = "", login = "";
            st1:
            Console.Clear();
            string[] nums2 = new string[10000];
            int[] checker3 = new int[1000];
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\" + dolzh);
            int j = 1;
            foreach (var item in dir1.GetFiles())
            {
                if (item.Name != "zarplata.zrp")
                {
                    nums2[j] = item.Name;
                    BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + item.Name, FileMode.Open));
                    int h = 1;
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (h == 1)
                            login = name;
                        if (h == 2)
                            pas = name;
                        if (h == 3)
                            fam = name;
                        if (h == 4)
                            ima = name;
                        if (h == 5)
                            otch = name;
                        h++;
                    }
                    reader.Close();

                    Console.WriteLine(j + ") " + fam + " " + ima + " " + otch);
                    checker3[j] = j;
                    j++;
                }
            }
            if (j == 1)
            {
                Console.WriteLine("Нет пользователей");
                Thread.Sleep(2000);
                Admin_panel();
            }
            int j2 = 0;
            bool flag1 = true;
            if (j > 1)
            {
            st11: Console.Write("Выбор: ");
                string otvetr = Console.ReadLine();
                try
                {
                    j2 = Convert.ToInt32(otvetr);
                }
                catch
                {
                    Console.WriteLine("Error!");
                    Thread.Sleep(2000);
                    goto st11;
                }

                flag1 = false;
                for (int i1 = 0; i1 < j; ++i1)
                {
                    if (checker3[i1] == j2 && checker3[i1] != 0)
                    {
                        flag1 = true;
                    }
                }

                if (flag1 == false)
                {
                    Console.WriteLine("Error");
                    Thread.Sleep(2000);
                    goto st1;
                }
                string zp = "", dr = "", obr = "", opt = "", filial = "";
                if (flag1 == true)
                {
                    int ind = nums2[j2].Length - 6;
                    string jkl = nums2[j2].Remove(ind);
                    dir1 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" +jkl);
                    foreach (var item in dir1.GetFiles())
                    {
                        nums2[j] = item.Name;
                        BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + jkl + @"\"+item.Name, FileMode.Open));
                        int h = 1;
                        while (reader.PeekChar() > -1)
                        {
                            string name = reader.ReadString();
                            if (item.Name == "zarplata.zp")
                                zp = name;
                            if (item.Name == "denrozh.dr")
                                dr = name;
                            if (item.Name == "obrazovanie.obr")
                                obr = name;
                            if (item.Name == "opit.opit")
                                opt = name;
                            if (item.Name == "filial.fil")
                                filial = name;

                        }
                        reader.Close();
                    }

                    foreach (var item in dir1.GetFiles())
                    {
                        nums2[j] = item.Name;
                        BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh +  @"\" + nums2[j2], FileMode.Open));
                        int h = 1;
                        while (reader.PeekChar() > -1)
                        {
                            string name = reader.ReadString();
                            if (h == 1)
                                login = name;
                            if (h == 2)
                                pas = name;
                            if (h == 3)
                                fam = name;
                            if (h == 4)
                                ima = name;
                            if (h == 5)
                                otch = name;
                            h++;
                        }
                        reader.Close();
                    }

                    Console.Clear();
                    Console.WriteLine("Данные сотрудника: ");
                    Console.WriteLine("ФИО: " + fam + " " + ima + " " + otch);
                    Console.WriteLine("Логин: " + login);
                    Console.WriteLine("Пароль: " + pas);
                    if (zp == "")
                        Console.WriteLine("Зарплата: не указана");
                    if (zp != "")
                        Console.WriteLine("Зарплата: "+zp+" рублей");
                    if (dr == "")
                        Console.WriteLine("Год рожденя: не указана");
                    if (dr != "")
                        Console.WriteLine("Год рождения: " + dr + " год");
                    if (dr == "")
                        Console.WriteLine("Возраст: не указан год рождения");
                    if (dr != "")
                    {
                        int vozr1=0;
                        int vozr = 0;
                        try
                        {
                            vozr1 = int.Parse(dr);
                            vozr = DateTime.Now.Year - vozr1;
                            Console.WriteLine("Возраст: "+vozr);
                        }
                        catch
                        {
                            Console.WriteLine("Возраст: ошибка");
                        }
                    }
                    if (obr == "")
                        Console.WriteLine("Образование: не указана");
                    if (obr != "")
                        Console.WriteLine("Образование: " +obr);
                    if (opt == "")
                        Console.WriteLine("Опыт: не указана");
                    if (opt != "")
                        Console.WriteLine("Опыт: " + opt + " лет");
                    if (filial == "")
                        Console.WriteLine("Филиал: не указана");
                    if (filial != "")
                        Console.WriteLine("Филиал: " + filial);
                    Thread.Sleep(15000);
                    Admin_panel();
                }
            }
        }
        public void LookZP()
        {
            Console.Clear();
            string path11 = Directory.GetCurrentDirectory();
            BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Администратор\zarplata.zrp", FileMode.Open));
            string zp = "";
            while (reader.PeekChar() > -1)
            {
                zp = reader.ReadString();
            }
            reader.Close();
            Console.WriteLine("Ваша установленная месячная зарплата: "+zp+" рублей");
            double ndfl = 0, pfr = 0, ffoms = 0, fss = 0, nsl = 0, usno = 0, zarp=0, itog=0;

            zarp = Double.Parse(zp);
            ndfl = (zarp*13.00)/100.00;
            pfr = (zarp * 22.00)/100.00;
            ffoms = (zarp * 5.10) / 100.00;
            fss = (zarp * 2.90) / 100.00;
            nsl = (zarp *0.20) / 100.00;
            usno = (zarp * 6.00)/100.00;
            itog = zarp - ndfl;

            Console.WriteLine("Из этой зарплаты: ");
            Console.WriteLine("13% НДФ из вашей ЗП: "+ndfl);
            Console.WriteLine();
            Console.WriteLine("Работодатель отчисляет: ");
            Console.WriteLine("В ПФР: " + pfr);
            Console.WriteLine("ФФОМС: " + ffoms);
            Console.WriteLine("ФФС: " + fss);
            Console.WriteLine("НСЛ: " + nsl);
            Console.WriteLine("УСНО: "+usno);
            Console.WriteLine();
            Console.WriteLine("С учётом НДС вы получаете: " + itog);
            Thread.Sleep(16000);
            Console.Clear();
            Admin_panel();
        }
        public void DelFilial()
        {
            string path11 = Directory.GetCurrentDirectory();
            Console.Clear();
        lolkin:
            string filial = "";
        st1:
            Console.Clear();
            string[] nums2 = new string[10000];
            int[] checker3 = new int[1000];
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\");
            int j = 1;
            foreach (var item in dir1.GetFiles())
            {
                string lol = item.Name;
                string check = lol.Substring(lol.Length - 7);
                if (check == ".filial")
                {
                    nums2[j] = item.Name;
                    Console.WriteLine(j + ") " + Path.GetFileNameWithoutExtension(item.Name));
                    checker3[j] = j;
                    j++;
                }
            }
            if (j == 1)
            {
                Console.WriteLine("Нет филиалов");
                Thread.Sleep(2000);
                Admin_panel();
            }
            int j2 = 0;
            bool flag1 = true;
            if (j > 1)
            {
                Console.Write("Выбор: ");
                string otvetr = Console.ReadLine();
                try
                {
                    j2 = Convert.ToInt32(otvetr);
                }
                catch
                {
                    Console.WriteLine("Error!");
                    Thread.Sleep(2000);
                    goto st1;
                }
                flag1 = false;
                for (int i1 = 0; i1 < j; ++i1)
                {
                    if (checker3[i1] == j2 && checker3[i1] != 0)
                    {
                        flag1 = true;
                    }
                }

                if (flag1 == false)
                {
                    Console.WriteLine("Error");
                    Thread.Sleep(2000);
                    goto st1;
                }

                if (flag1 == true)
                {
                    File.Delete(path11 + @"\Данные\Сотрудники\" + nums2[j2]);
                    Console.WriteLine("Филиал удалён!");
                    Console.WriteLine("Внимание! У сотрудников, которые были в данном филиале, необходимо повторно указать другой филиал или уволить!");
                    Console.WriteLine("Данным видом работ занимается сотрудник отдела кадров!");
                    Thread.Sleep(5000);
                    Admin_panel();
                }
            }
        }
    }
}


