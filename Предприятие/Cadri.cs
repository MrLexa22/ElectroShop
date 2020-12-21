using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace Предприятие
{
    class Cadri
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

        public void Authentication(string login)
        {
            string path11 = Directory.GetCurrentDirectory();
            Authorization auth = new Authorization();

            DirectoryInfo dir = new DirectoryInfo(path11 + @"\Данные\Сотрудники\Отдел кадров");
            foreach (var item in dir.GetFiles())
            {
                if (login + ".login" == item.Name)
                {
                    Console.Write("Введите пароль: ");
                    string parol = ReadPasswordLine();
                    string log = "", pas = "";
                    int i = 0;
                    BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\Отдел кадров\" + login + ".login", FileMode.Open));
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (i == 0)
                            log = name;
                        if (i == 1)
                            pas = name;
                        i++;
                    }
                    reader.Close();
                    if (parol == pas)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Вы вошли как сотрудник отдела кадров!");
                        Thread.Sleep(1500);
                        Cadr();
                    }
                    if (parol != pas)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Пароль неверный!");
                        Thread.Sleep(1500);
                        auth.Lolkin();
                    }
                }
            }
            Cladovshick pre = new Cladovshick();
            pre.Authentication(login);
        }

        public void Cadr()
        {
        st2: Console.Clear();
            string path11 = Directory.GetCurrentDirectory();
            DirectoryInfo dir = new DirectoryInfo(path11 + @"\Данные\Сотрудники\");
            string[] nums2 = new string[10000];
            int[] checker3 = new int[1000];
            string fam = "", ima = "", otch = "", pas = "", log = "";
            int j = 1;
            foreach (var item in dir.GetFiles())
            {
                if (item.Name != "zarplata.zrp")
                {
                    string lol = item.Name;
                    string check = lol.Substring(lol.Length - 6);
                    nums2[j] = item.Name;
                    BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + item.Name, FileMode.Open));
                    int h = 1;
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (h == 1)
                            log = name;
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

                    if (check == ".login")
                    {
                        Console.WriteLine(j + ") " + fam + " " + ima + " " + otch);
                        checker3[j] = j;
                        j++;
                    }
                }
            }

            if (j == 1)
            {
                Console.WriteLine("Сотрудников без должностей и личных данных нет!");
                Console.WriteLine("Загрузка меню...");
                Thread.Sleep(5000);
                Panel_Cadri();
            }
            int j2 = 0;
            bool flag1 = true;
            if (j > 1)
            {
                Console.WriteLine();
                Console.WriteLine("Внимание!! Есть сотрудник(и), у которых не указана должность и личный данные!");
                Console.WriteLine("Желаете ли Вы указать должность и данные сотрудникам?");
                Console.WriteLine("1. ДА");
                Console.WriteLine("2. НЕТ");
                Console.Write("Выбор: ");
                string vih = Console.ReadLine();

                if (vih == "1")
                {
                    Console.WriteLine();
                st1: Console.Write("Выберите номер сотрудника: ");
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
                        Console.Clear();
                        BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + nums2[j2], FileMode.Open));
                        int h = 1;
                        while (reader.PeekChar() > -1)
                        {
                            string name = reader.ReadString();
                            if (h == 1)
                                log = name;
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
                    fik: Console.WriteLine("Данные: ");
                        Console.WriteLine("ФИО: " + fam + " " + ima + " " + otch);
                        Console.WriteLine("Логин: " + log);
                        Console.WriteLine();
                        Console.WriteLine("1. Бухгалтер");
                        Console.WriteLine("2. Кассир-продавец");
                        Console.WriteLine("3. Кладовщик");
                        Console.WriteLine("4. Сотрудник отдела кадров");
                        Console.Write("Выберите должность: ");
                        string vic = Console.ReadLine();
                        string dolzh = "", zp = "";
                        if (vic == "1")
                        {
                            dolzh = "Бухгалтер";
                            zp = "90000";
                            goto cont;
                        }
                        if (vic == "2")
                        {
                            dolzh = "Кассир-продавец";
                            zp = "76000";
                            goto cont;
                        }
                        if (vic == "3")
                        {
                            dolzh = "Кладовщик";
                            zp = "85000";
                            goto cont;
                        }
                        if (vic == "4")
                        {
                            dolzh = "Отдел кадров";
                            zp = "80000";
                            goto cont;
                        }
                        else
                        {
                            Console.WriteLine("Error");
                            Thread.Sleep(2000);
                            Console.Clear();
                            goto fik;
                        }

                    cont:
                        if (!File.Exists(path11 + @"\Данные\Сотрудники\" + dolzh + @"\zarplata.zrp"))
                        {
                            BinaryWriter reader221 = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\zarplata.zrp", FileMode.OpenOrCreate));
                            reader221.Write(zp);
                            reader221.Close();
                        }

                        string dr = "", obr = "", opt = "", filial = "";
                        Console.Clear();
                        Console.Write("Введите образование: ");
                        obr = Console.ReadLine();

                        Console.WriteLine();
                    opyt: Console.Write("Введите опыт работы (число целое): ");
                        opt = Console.ReadLine();
                        if (opt != "" || opt != " " || opt != null)
                        {
                            try
                            {
                                int chek = 0;
                                chek = Convert.ToInt32(opt);
                            }
                            catch
                            {
                                Console.WriteLine("Error");
                                Thread.Sleep(2000);
                                Console.Clear();
                                goto opyt;
                            }
                        }

                    dra: Console.WriteLine();
                        Console.Write("Введите год рождения: ");
                        dr = Console.ReadLine();
                        if (dr == "" || dr == " " || dr == null)
                        {
                            Console.WriteLine("Обязательное поле!");
                            Thread.Sleep(2000);
                            goto dra;
                        }
                        if (dr != "" || dr != " " || dr != null)
                        {
                            try
                            {
                                int chek = 0;
                                chek = Convert.ToInt32(dr);

                                int chek1 = DateTime.Now.Year - 18;
                                if (chek > chek1)
                                {
                                    Console.WriteLine("Возраст должен быть больше 18 лет!");
                                    Thread.Sleep(3000);
                                    Console.Clear();
                                    goto dra;
                                }
                                if (chek < 1960)
                                {
                                    Console.WriteLine("Все сотрудники должны быть младше 1960 г.р.");
                                    Thread.Sleep(3000);
                                    Console.Clear();
                                    goto dra;
                                }
                                else
                                    goto rgh1;
                            }
                            catch
                            {
                                Console.WriteLine("Error");
                                Thread.Sleep(2000);
                                Console.Clear();
                                goto dra;
                            }
                        }

                    rgh1: Console.Clear();
                        string filo = "", gorop = "";
                        string[] nums22 = new string[10000];
                        int[] checker33 = new int[1000];
                        j = 1;
                        Console.WriteLine("Укажите филиал!");
                        dir = new DirectoryInfo(path11 + @"\Данные\Сотрудники\");
                        foreach (var item1 in dir.GetFiles())
                        {
                            string lol1 = item1.Name;
                            string check1 = lol1.Substring(lol1.Length - 7);
                            nums22[j] = item1.Name;
                            reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + item1.Name, FileMode.Open));
                            h = 1;
                            while (reader.PeekChar() > -1)
                            {
                                string name = reader.ReadString();
                                if (h == 1)
                                    filo = name;
                                if (h == 2)
                                    gorop = name;
                                h++;
                            }
                            reader.Close();
                            if (check1 == ".filial")
                            {
                                Console.WriteLine(j + ") " + filo + " (" + gorop + ") ");
                                checker33[j] = j;
                                j++;
                            }
                        }
                        if (j == 1)
                        {
                            Console.WriteLine("Филиалов нет! Необходимо создать филиал!!");
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
                                goto rgh1;
                            }
                            catch
                            {
                                Console.WriteLine("Название не удовлетворяет текущим требованиям!");
                                Thread.Sleep(5000);
                                Console.Clear();
                                goto r1;
                            }
                        }
                        int j22 = 0;
                        bool flag11 = true;
                        if (j > 1)
                        {
                            Console.WriteLine();
                        st11: Console.Write("Выберите номер филиала: ");
                            otvetr = Console.ReadLine();
                            try
                            {
                                j22 = Convert.ToInt32(otvetr);
                            }
                            catch
                            {
                                Console.WriteLine("Error!");
                                Thread.Sleep(2000);
                                goto st11;
                            }
                            flag11 = false;
                            for (int i1 = 0; i1 < j; ++i1)
                            {
                                if (checker33[i1] == j22 && checker33[i1] != 0)
                                {
                                    flag11 = true;
                                }
                            }

                            if (flag11 == false)
                            {
                                Console.WriteLine("Error");
                                Thread.Sleep(2000);
                                goto st11;
                            }

                            if (flag11 == true)
                            {
                                filial = Path.GetFileNameWithoutExtension(nums22[j22]);
                                Console.Clear();
                                Console.WriteLine("Пользователь имеет следующие данные:");
                                Console.WriteLine("Данные: ");
                                Console.WriteLine("ФИО: " + fam + " " + ima + " " + otch);
                                Console.WriteLine("Логин: " + log);
                                Console.WriteLine("Должность: " + dolzh);
                                if (zp == "")
                                    Console.WriteLine("Зарплата: не указана");
                                if (zp != "")
                                    Console.WriteLine("Зарплата: " + zp + " рублей");
                                if (dr == "")
                                    Console.WriteLine("Год рожденя: не указана");
                                if (dr != "")
                                    Console.WriteLine("Год рождения: " + dr + " год");
                                if (dr == "")
                                    Console.WriteLine("Возраст: не указан год рождения");
                                if (dr != "")
                                {
                                    int vozr1 = 0;
                                    int vozr = 0;
                                    try
                                    {
                                        vozr1 = int.Parse(dr);
                                        vozr = DateTime.Now.Year - vozr1;
                                        Console.WriteLine("Возраст: " + vozr);
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Возраст: ошибка");
                                    }
                                }
                                if (obr == "")
                                    Console.WriteLine("Образование: не указана");
                                if (obr != "")
                                    Console.WriteLine("Образование: " + obr);
                                if (opt == "")
                                    Console.WriteLine("Опыт: не указана");
                                if (opt != "")
                                    Console.WriteLine("Опыт: " + opt + " лет");
                                if (filial == "")
                                    Console.WriteLine("Филиал: не указана");
                                if (filial != "")
                                    Console.WriteLine("Филиал: " + filial);

                                File.Move(path11 + @"\Данные\Сотрудники\" + log + ".login", path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + log + ".login");
                                File.Delete(path11 + @"\Данные\Сотрудники\" + log + ".login");
                                Directory.CreateDirectory(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + log);
                                BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + log + @"\zarplata.zp", FileMode.OpenOrCreate));
                                writer.Write(zp);
                                writer.Close();
                                writer = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + log + @"\denrozh.dr", FileMode.OpenOrCreate));
                                writer.Write(dr);
                                writer.Close();
                                writer = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + log + @"\obrazovanie.obr", FileMode.OpenOrCreate));
                                writer.Write(obr);
                                writer.Close();
                                writer = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + log + @"\opit.opit", FileMode.OpenOrCreate));
                                writer.Write(opt);
                                writer.Close();
                                writer = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + log + @"\filial.fil", FileMode.OpenOrCreate));
                                writer.Write(filial);
                                writer.Close();
                                Thread.Sleep(8000);
                                goto st2;
                            }
                        }
                    }
                }
                if (vih == "2")
                {
                    Panel_Cadri();
                }
                if (vih != "1" || vih != "2")
                {
                    Console.WriteLine("Ошибка!");
                    Thread.Sleep(2000);
                    goto st2;
                }
            }
        }

        public void Panel_Cadri()
        {
            Console.Clear();
            string[] menuText = { "Выберите\n", "  1)Заполнить личные данные сотрудников БЕЗ ДОЛЖНОСТЕЙ\n", "  2)Просмотр данных сотрудников\n", "  3)Редактирование личных данных сотрудников\n", "  4)Уволить сотрудника\n", "  5)Просмотреть свои сведения о заработной плате\n", "  6)Выход" };
            Authorization auth = new Authorization();
            while (true)
            {

                int chek = ViewMenu(menuText);

                switch (chek)
                {
                    case 1:
                        Cadr();
                        break;
                    case 2:
                        LookDanie();
                        break;
                    case 3:
                        Redact();
                        break;
                    case 4:
                        Uval();
                        break;
                    case 5:
                        LookZP();
                        break;
                    case 6:
                        MenuVibor lol = new MenuVibor();
                        lol.Vibor();
                        break;
                }
            }
        }

        public void LookDanie()
        {
            Console.Clear();
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
                Panel_Cadri();
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
                string zp = "", dr = "", obr = "", opt = "", filial = "";
                if (flag1 == true)
                {
                    int ind = nums2[j2].Length - 6;
                    string jkl = nums2[j2].Remove(ind);
                    dir1 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + jkl);
                    foreach (var item in dir1.GetFiles())
                    {
                        nums2[j] = item.Name;
                        BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + jkl + @"\" + item.Name, FileMode.Open));
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
                    }

                    Console.Clear();
                    Console.WriteLine("Данные сотрудника: ");
                    Console.WriteLine("ФИО: " + fam + " " + ima + " " + otch);
                    Console.WriteLine("Логин: " + login);
                    if (zp == "")
                        Console.WriteLine("Зарплата: не указана");
                    if (zp != "")
                        Console.WriteLine("Зарплата: " + zp + " рублей");
                    if (dr == "")
                        Console.WriteLine("Год рожденя: не указана");
                    if (dr != "")
                        Console.WriteLine("Год рождения: " + dr + " год");
                    if (dr == "")
                        Console.WriteLine("Возраст: не указан год рождения");
                    if (dr != "")
                    {
                        int vozr1 = 0;
                        int vozr = 0;
                        try
                        {
                            vozr1 = int.Parse(dr);
                            vozr = DateTime.Now.Year - vozr1;
                            Console.WriteLine("Возраст: " + vozr);
                        }
                        catch
                        {
                            Console.WriteLine("Возраст: ошибка");
                        }
                    }
                    if (obr == "")
                        Console.WriteLine("Образование: не указана");
                    if (obr != "")
                        Console.WriteLine("Образование: " + obr);
                    if (opt == "")
                        Console.WriteLine("Опыт: не указана");
                    if (opt != "")
                        Console.WriteLine("Опыт: " + opt + " лет");
                    if (filial == "")
                        Console.WriteLine("Филиал: не указана");
                    if (filial != "")
                        Console.WriteLine("Филиал: " + filial);
                    Thread.Sleep(8000);
                    Panel_Cadri();
                }
            }
        }
        public void Redact()
        {
            Console.Clear();
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
                Panel_Cadri();
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
                string zp = "", dr = "", obr = "", opt = "", filial = "";
                if (flag1 == true)
                {
                    int ind = nums2[j2].Length - 6;
                    string jkl = nums2[j2].Remove(ind);
                    dir1 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + jkl);
                    foreach (var item in dir1.GetFiles())
                    {
                        nums2[j] = item.Name;
                        BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + jkl + @"\" + item.Name, FileMode.Open));
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
                    }

                    Console.Clear();
                    Console.WriteLine("Данные сотрудника: ");
                    Console.WriteLine("ФИО: " + fam + " " + ima + " " + otch);
                    Console.WriteLine("Логин: " + login);
                    if (zp == "")
                        Console.WriteLine("Зарплата: не указана");
                    if (zp != "")
                        Console.WriteLine("Зарплата: " + zp + " рублей");
                    if (dr == "")
                        Console.WriteLine("Год рожденя: не указана");
                    if (dr != "")
                        Console.WriteLine("Год рождения: " + dr + " год");
                    if (dr == "")
                        Console.WriteLine("Возраст: не указан год рождения");
                    if (dr != "")
                    {
                        int vozr1 = 0;
                        int vozr = 0;
                        try
                        {
                            vozr1 = int.Parse(dr);
                            vozr = DateTime.Now.Year - vozr1;
                            Console.WriteLine("Возраст: " + vozr);
                        }
                        catch
                        {
                            Console.WriteLine("Возраст: ошибка");
                        }
                    }
                    if (obr == "")
                        Console.WriteLine("Образование: не указана");
                    if (obr != "")
                        Console.WriteLine("Образование: " + obr);
                    if (opt == "")
                        Console.WriteLine("Опыт: не указана");
                    if (opt != "")
                        Console.WriteLine("Опыт: " + opt + " лет");
                    if (filial == "")
                        Console.WriteLine("Филиал: не указана");
                    if (filial != "")
                        Console.WriteLine("Филиал: " + filial);
                    Thread.Sleep(5000);

                    Console.Clear();
                    string[] menuText1 = { "Выберите что нужно изменить:\n", "  1)ФИО (" + fam + " " + ima + " " + otch + ")\n", "  2)Год рождения (" + dr + ")\n", "  3)Образование (" + obr + ")\n", "  4)Опыт (" + opt + ")\n", "  5)Должность (" + dolzh + ")\n", "  6)Филиал (" + filial + ")\n" };
                    while (true)
                    {
                        int chek = ViewMenu(menuText1);
                        switch (chek)
                        {
                            case 1:
                                goto redactfio;
                                break;
                            case 2:
                                goto redactdr;
                                break;
                            case 3:
                                goto redactobr;
                                break;
                            case 4:
                                goto redactopyt;
                                break;
                            case 5:
                                goto redactdolzh;
                                break;
                            case 6:
                                goto redactfilial;
                                break;
                        }
                    }
                redactfio:
                    Console.Clear();
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
                        goto redactfio;
                    }
                    if (File.Exists(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + nums2[j2]))
                        File.Delete(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + nums2[j2]);
                    BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + nums2[j2], FileMode.OpenOrCreate));
                    writer.Write(login);
                    writer.Write(pas);
                    writer.Write(fam);
                    writer.Write(ima);
                    writer.Write(otch);
                    writer.Close();
                    Console.WriteLine("Новые ФИО записаны!");
                    Thread.Sleep(2000);
                    Panel_Cadri();

                redactdr:
                    Console.Clear();
                    Console.Write("Введите год рождения: ");
                    dr = Console.ReadLine();
                    if (dr == "" || dr == " " || dr == null)
                    {
                        Console.WriteLine("Обязательное поле!");
                        Thread.Sleep(2000);
                        goto redactdr;
                    }
                    if (dr != "" || dr != " " || dr != null)
                    {
                        try
                        {
                            int chek = 0;
                            chek = Convert.ToInt32(dr);

                            int chek1 = DateTime.Now.Year - 18;
                            if (chek > chek1)
                            {
                                Console.WriteLine("Возраст должен быть больше 18 лет!");
                                Thread.Sleep(3000);
                                Console.Clear();
                                goto redactdr;
                            }
                            if (chek < 1960)
                            {
                                Console.WriteLine("Все сотрудники должны быть младше 1960 г.р.");
                                Thread.Sleep(3000);
                                Console.Clear();
                                goto redactdr;
                            }
                            else
                            {
                                Console.WriteLine("Год рождения изменён!");
                                if (File.Exists(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + jkl + @"\denrozh.dr"))
                                    File.Delete(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + jkl + @"\denrozh.dr");
                                writer = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + jkl + @"\denrozh.dr", FileMode.OpenOrCreate));
                                writer.Write(dr);
                                writer.Close();
                                Thread.Sleep(2000);
                                Panel_Cadri();
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Error");
                            Thread.Sleep(2000);
                            Console.Clear();
                            goto redactdr;
                        }
                    }

                redactobr:
                    Console.Clear();
                    Console.Write("Введите образование сотрудника: ");
                    obr = Console.ReadLine();
                    Console.WriteLine("Уровень образования изменён!");
                    if (File.Exists(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + jkl + @"\obrazovanie.obr"))
                        File.Delete(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + jkl + @"\obrazovanie.obr");
                    writer = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + jkl + @"\obrazovanie.obr", FileMode.OpenOrCreate));
                    writer.Write(obr);
                    writer.Close();
                    Thread.Sleep(2000);
                    Panel_Cadri();

                redactopyt:
                    Console.Clear();
                    Console.Write("Введите опыт работы (число целое): ");
                    opt = Console.ReadLine();
                    if (opt != "" || opt != " " || opt != null)
                    {
                        try
                        {
                            int chek = 0;
                            chek = Convert.ToInt32(opt);
                        }
                        catch
                        {
                            Console.WriteLine("Error");
                            Thread.Sleep(2000);
                            Console.Clear();
                            goto redactopyt;
                        }
                    }
                    Console.WriteLine("Опыт изменён!");
                    if (File.Exists(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + jkl + @"\opit.opit"))
                        File.Delete(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + jkl + @"\opit.opit");
                    writer = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + jkl + @"\opit.opit", FileMode.OpenOrCreate));
                    writer.Write(opt);
                    writer.Close();
                    Thread.Sleep(2000);
                    Panel_Cadri();

                redactdolzh:
                    Console.Clear();
                    j = 1;
                    DirectoryInfo dir11 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\");
                    string[] nums22 = new string[10000];
                    foreach (var item22 in dir11.GetDirectories())
                    {
                        string gfg = item22.Name;
                        if (gfg != dolzh)
                        {
                            nums22[j] = item22.Name;
                            Console.WriteLine(j + ") " + item22.Name);
                            checker3[j] = j;
                            j++;
                        }
                    }
                    int j22 = 0;
                    bool flag11 = true;
                    if (j > 1)
                    {
                        Console.WriteLine();
                    st11: Console.Write("Выбор: ");
                        string otvetr1 = Console.ReadLine();
                        try
                        {
                            j2 = Convert.ToInt32(otvetr1);
                        }
                        catch
                        {
                            Console.WriteLine("Error!");
                            Thread.Sleep(2000);
                            goto st11;
                        }

                        flag11 = false;
                        for (int i1 = 0; i1 < j; ++i1)
                        {
                            if (checker3[i1] == j22 && checker3[i1] != 0)
                            {
                                flag11 = true;
                            }
                        }

                        if (flag11 == false)
                        {
                            Console.WriteLine("Error");
                            Thread.Sleep(2000);
                            goto st11;
                        }

                        if (flag11 == true)
                        {
                            Console.WriteLine("Вы изменили должность!");
                            File.Move(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + login + ".login", path11 + @"\Данные\Сотрудники\" + nums22[j22] + @"\" + login + ".login");
                            File.Delete(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + login + ".login");
                            new DirectoryInfo(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + login).MoveTo(path11 + @"\Данные\Сотрудники\" + nums22[j22] + @"\" + login);
                            File.Delete(path11 + @"\Данные\Сотрудники\" + nums22[j22] + @"\" + login + @"\" + @"\zarplata.zp");
                            File.Copy(path11 + @"\Данные\Сотрудники\" + nums22[j22] + @"\zarplata.zrp", path11 + @"\Данные\Сотрудники\" + nums22[j22] + @"\" + login + @"\zarplata.zp");
                            Thread.Sleep(2000);
                            Panel_Cadri();
                        }
                    }

                redactfilial:
                    Console.Clear();
                    string filo = "", gorop = "";
                    nums22 = new string[10000];
                    int[] checker33 = new int[1000];
                    j = 1;
                    Console.WriteLine("Укажите филиал!");
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
                            string name = reader.ReadString();
                            if (h == 1)
                                filo = name;
                            if (h == 2)
                                gorop = name;
                            h++;
                        }
                        reader.Close();
                        if (check1 == ".filial")
                        {
                            Console.WriteLine(j + ") " + filo + " (" + gorop + ") ");
                            checker33[j] = j;
                            j++;
                        }
                    }
                    if (j == 1)
                    {
                        Console.WriteLine("Филиалов нет! Необходимо создать филиал!!");
                    r1:
                        Console.Write("Введите название филиала: ");
                        string name = Console.ReadLine();
                        Console.Write("Введите город филиала '" + name + "': ");
                        string gorod = Console.ReadLine();

                        try
                        {
                            dir1 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\");
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
                            writer = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + name + ".filial", FileMode.OpenOrCreate));
                            writer.Write(name);
                            writer.Write(gorod);
                            writer.Close();
                            Console.WriteLine("Филиал успешно создан!");
                            Thread.Sleep(3000);
                            goto redactfilial;
                        }
                        catch
                        {
                            Console.WriteLine("Название не удовлетворяет текущим требованиям!");
                            Thread.Sleep(5000);
                            Console.Clear();
                            goto r1;
                        }
                    }
                    j22 = 0;
                    flag11 = true;
                    if (j > 1)
                    {
                        Console.WriteLine();
                    st11: Console.Write("Выберите номер филиала: ");
                        string otvetr2 = Console.ReadLine();
                        try
                        {
                            j22 = Convert.ToInt32(otvetr2);
                        }
                        catch
                        {
                            Console.WriteLine("Error!");
                            Thread.Sleep(2000);
                            goto st11;
                        }

                        flag11 = false;
                        for (int i1 = 0; i1 < j; ++i1)
                        {
                            if (checker33[i1] == j22 && checker33[i1] != 0)
                            {
                                flag11 = true;
                            }
                        }

                        if (flag11 == false)
                        {
                            Console.WriteLine("Error");
                            Thread.Sleep(2000);
                            goto st11;
                        }

                        if (flag11 == true)
                        {
                            Console.WriteLine("Филиал изменён");
                            if (File.Exists(path11 + @"\Данные\Сотрудники\" + dolzh + @"\filial.fil"))
                                File.Delete(path11 + @"\Данные\Сотрудники\" + dolzh + @"\filial.fil");
                            filial = Path.GetFileNameWithoutExtension(nums22[j22]);
                            writer = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + jkl + @"\filial.fil", FileMode.OpenOrCreate));
                            writer.Write(filial);
                            writer.Close();
                            Thread.Sleep(2000);
                            Panel_Cadri();
                        }
                    }
                }
            }
        }
        public void Uval()
        {
            Console.Clear();
            string path11 = Directory.GetCurrentDirectory();
            Console.Clear();
            string dolzh = "";
            string[] menuText = { "Выберите должность:\n", "  1)Бухгалтер\n", "  2)Кассир-продавец\n", "  3)Кладовщик\n", "  4)Сотрудник отдела кадров\n", "  5)Без должности\n" };
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
                    case 5:
                        goto lolkin1;
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
                Panel_Cadri();
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
                string zp = "", dr = "", obr = "", opt = "", filial = "";
                if (flag1 == true)
                {
                    Console.WriteLine("Сотрудник уволен!");
                    File.Delete(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + nums2[j2]);
                    Directory.Delete(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + Path.GetFileNameWithoutExtension(nums2[j2]), true);
                    Thread.Sleep(3000);
                    Panel_Cadri();
                }
            }
            lolkin1:
            Console.Clear();
            path11 = Directory.GetCurrentDirectory();
            DirectoryInfo dir = new DirectoryInfo(path11 + @"\Данные\Сотрудники\");
            string[] nums23 = new string[10000];
            int[] checker33 = new int[1000];
            string log = "";
            j = 1;
            fam = ""; ima = ""; otch = "";
            foreach (var item in dir.GetFiles())
            {
                if (item.Name != "zarplata.zrp")
                {
                    string lol = item.Name;
                    string check = lol.Substring(lol.Length - 6);
                    nums23[j] = item.Name;
                    BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + item.Name, FileMode.Open));
                    int h = 1;
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (h == 1)
                            log = name;
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

                    if (check == ".login")
                    {
                        Console.WriteLine(j + ") " + fam + " " + ima + " " + otch);
                        checker33[j] = j;
                        j++;
                    }
                }
            }

            if (j == 1)
            {
                Console.WriteLine("Сотрудников без должностей и личных данных нет!");
                Console.WriteLine("Загрузка меню...");
                Thread.Sleep(3000);
                Panel_Cadri();
            }
            int j23 = 0;
            bool flag13 = true;
            if (j > 1)
            {
                Console.WriteLine();
            st11: Console.Write("Выбор: ");
                string otvetr = Console.ReadLine();
                try
                {
                    j23 = Convert.ToInt32(otvetr);
                }
                catch
                {
                    Console.WriteLine("Error!");
                    Thread.Sleep(2000);
                    goto st11;
                }

                flag13 = false;
                for (int i1 = 0; i1 < j; ++i1)
                {
                    if (checker33[i1] == j23 && checker33[i1] != 0)
                    {
                        flag13 = true;
                    }
                }

                if (flag13 == false)
                {
                    Console.WriteLine("Error");
                    Thread.Sleep(2000);
                    goto st11;
                }

                if (flag13 == true)
                {
                    Console.WriteLine("Сотрудник без должности уволен!");
                    File.Delete(path11 + @"\Данные\Сотрудники\" + nums23[j23]);
                    Thread.Sleep(2000);
                    Panel_Cadri();
                }
            }
        }
        public void LookZP()
        {
            Console.Clear();
            string path11 = Directory.GetCurrentDirectory();
            BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\Отдел кадров\zarplata.zrp", FileMode.Open));
            string zp = "";
            while (reader.PeekChar() > -1)
            {
                zp = reader.ReadString();
            }
            reader.Close();
            Console.WriteLine("Ваша установленная месячная зарплата: " + zp + " рублей");
            double ndfl = 0, pfr = 0, ffoms = 0, fss = 0, nsl = 0, usno = 0, zarp = 0, itog = 0;

            zarp = Double.Parse(zp);
            ndfl = (zarp * 13.00) / 100.00;
            pfr = (zarp * 22.00) / 100.00;
            ffoms = (zarp * 5.10) / 100.00;
            fss = (zarp * 2.90) / 100.00;
            nsl = (zarp * 0.20) / 100.00;
            usno = (zarp * 6.00) / 100.00;
            itog = zarp - ndfl;

            Console.WriteLine("Из этой зарплаты: ");
            Console.WriteLine("13% НДФ из вашей ЗП: " + ndfl);
            Console.WriteLine();
            Console.WriteLine("Работодатель отчисляет: ");
            Console.WriteLine("В ПФР: " + pfr);
            Console.WriteLine("ФФОМС: " + ffoms);
            Console.WriteLine("ФФС: " + fss);
            Console.WriteLine("НСЛ: " + nsl);
            Console.WriteLine("УСНО: " + usno);
            Console.WriteLine();
            Console.WriteLine("С учётом НДС вы получаете: " + itog);
            Thread.Sleep(16000);
            Console.Clear();
            Panel_Cadri();
        }
    }
}

