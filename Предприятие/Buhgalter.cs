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
    class Buhgalter
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

            DirectoryInfo dir = new DirectoryInfo(path11 + @"\Данные\Сотрудники\Бухгалтер");
            foreach (var item in dir.GetFiles())
            {
                if (login + ".login" == item.Name)
                {
                    Console.Write("Введите пароль: ");
                    string parol = ReadPasswordLine();
                    string log = "", pas = "", em = ""; ;
                    int i = 0;
                    BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\Бухгалтер\" + login + ".login", FileMode.Open));
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
                        Console.WriteLine("Вы вошли как бухгалтер!");
                        string fam = "", ima = "", otch = "";
                        path11 = Directory.GetCurrentDirectory();
                        reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\Бухгалтер\" + login + ".login", FileMode.Open));
                        int h = 1;
                        while (reader.PeekChar() > -1)
                        {
                            string name = reader.ReadString();
                            if (h == 3)
                                fam = name;
                            if (h == 4)
                                ima = name;
                            if (h == 5)
                                otch = name;
                            h++;
                        }
                        Console.WriteLine("Добро пожаловать! " + fam + " " + ima + " " + otch);
                        reader.Close();
                        Thread.Sleep(3500);
                        Buhg_menu();
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
            Console.WriteLine("Логин не зарегстрирован / логину сотрудника не присвоена должность!");
            Console.WriteLine("Обратитесь в отдел кадров или администратору!");
            Thread.Sleep(6000);
            Console.Clear();
            auth.Lolkin();
        }
        public void Buhg_menu()
        {
            Console.Clear();
            string path11 = Directory.GetCurrentDirectory();
            if (!File.Exists(path11 + @"\Данные\Бухгалтерия\budzhet.dengi"))
            {
                string budzhet = "500000";
                BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Бухгалтерия\budzhet.dengi", FileMode.OpenOrCreate));
                writer.Write(budzhet);
                writer.Close();
            }
            if (!Directory.Exists(path11 + @"\Данные\Бухгалтерия\Заказы"))
            {
                Directory.CreateDirectory(path11 + @"\Данные\Бухгалтерия\Заказы");
            }
            if (!Directory.Exists(path11 + @"\Данные\Бухгалтерия\Зарплаты"))
            {
                Directory.CreateDirectory(path11 + @"\Данные\Бухгалтерия\Зарплаты");
            }
            string[] menuText = { "Выберите\n", "  1)Просмотр зарплаты по должностям\n", "  2)Произвести выплату зарплаты\n", "  3)Просмотр сведений о выплате заработной плате\n", "  4)Просмотр сведений о прибыли в фирме\n", "  5)Просмотр бюджета фирмы\n", "  6)Изменить бюджет фирмы\n", "  7)Изменить зарплату по должности\n", "  8)Просмотр операций по счёту\n", "  9)Выход" };
            Authorization auth = new Authorization();
            while (true)
            {

                int chek = ViewMenu(menuText);

                switch (chek)
                {
                    case 1:
                        LookZP();
                        break;
                    case 2:
                        ViplataZP();
                        break;
                    case 3:
                        LookViplaty();
                        break;
                    case 4:
                        LookPribil();
                        break;
                    case 5:
                        LookBudzet();
                        break;
                    case 6:
                        ChangeBudzet();
                        break;
                    case 7:
                        ChangeZP();
                        break;
                    case 8:
                        LookSchet();
                        break;
                    case 9:
                        MenuVibor lol = new MenuVibor();
                        lol.Vibor();
                        break;
                }
            }
        }
        public void LookZP()
        {
            Console.Clear();
            string dolzh = "";
            string[] menuText = { "Выберите должность\n", "  1)Администратор\n", "  2)Бухгалтер\n", "  3)Кассир-продавец\n", "  4)Кладовщик\n", "  5)Отдел кадров\n", "  6)Возврат в меню" };
            while (true)
            {

                int chek = ViewMenu(menuText);

                switch (chek)
                {
                    case 1:
                        dolzh = "Администратор";
                        goto lookj;
                        break;
                    case 2:
                        dolzh = "Бухгалтер";
                        goto lookj;
                        break;
                    case 3:
                        dolzh = "Кассир-продавец";
                        goto lookj;
                        break;
                    case 4:
                        dolzh = "Кладовщик";
                        goto lookj;
                        break;
                    case 5:
                        dolzh = "Отдел кадров";
                        goto lookj;
                        break;
                    case 6:
                        Buhg_menu();
                        break;
                }
            }
            lookj:
            Console.Clear();
            string path11 = Directory.GetCurrentDirectory();
            string zp = "";
            if (dolzh == "Администратор")
            {
                BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\" + dolzh + @"\zarplata.zrp", FileMode.Open));
                while (reader.PeekChar() > -1)
                {
                    zp = reader.ReadString();
                }
                reader.Close();
            }
            if (dolzh != "Администратор")
            {
                BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\zarplata.zrp", FileMode.Open));
                while (reader.PeekChar() > -1)
                {
                    zp = reader.ReadString();
                }
                reader.Close();
            }
            Console.WriteLine("Установленная месячная зарплата ["+dolzh+"]: " + zp + " рублей");
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
            Console.WriteLine("13% НДС из ЗП: " + ndfl);
            Console.WriteLine();
            Console.WriteLine("Работодатель отчисляет: ");
            Console.WriteLine("В ПФР: " + pfr);
            Console.WriteLine("ФФОМС: " + ffoms);
            Console.WriteLine("ФФС: " + fss);
            Console.WriteLine("НСЛ: " + nsl);
            Console.WriteLine("УСНО: " + usno);
            Console.WriteLine();
            Console.WriteLine("С учётом НДС [" + dolzh + "] получает: " + itog);
            Thread.Sleep(16000);
            Console.Clear();
            LookZP();
        }
        public void ViplataZP()
        {
            start: Console.Clear();
            string path11 = Directory.GetCurrentDirectory();
            string dolzh = "";
            string[] menuText = { "Выберите должность\n", "  1)Администратор\n", "  2)Бухгалтер\n", "  3)Кассир-продавец\n", "  4)Кладовщик\n", "  5)Отдел кадров\n", "  6)Возврат в меню" };
            while (true)
            {

                int chek = ViewMenu(menuText);

                switch (chek)
                {
                    case 1:
                        dolzh = "Администратор";
                        goto lookj;
                        break;
                    case 2:
                        dolzh = "Бухгалтер";
                        goto lookj;
                        break;
                    case 3:
                        dolzh = "Кассир-продавец";
                        goto lookj;
                        break;
                    case 4:
                        dolzh = "Кладовщик";
                        goto lookj;
                        break;
                    case 5:
                        dolzh = "Отдел кадров";
                        goto lookj;
                        break;
                    case 6:
                        Buhg_menu();
                        break;
                }
            }

        lookj:
            Console.Clear();
            string[] menuText1 = { "Вы желаете выплатить зарплату всем сотрудникам данной должности?\n", "  1)Да\n", "  2)Нет\n", "  3)Вернуться в меню\n" };
            while (true)
            {

                int chek = ViewMenu(menuText1);

                switch (chek)
                {
                    case 1:
                        goto da;
                        break;
                    case 2:
                        goto net;
                        break;
                    case 3:
                        Buhg_menu();
                        break;
                }
            }

        da:
            Console.Clear();
            string god = "";
            string[] menuText2 = { "Выберите год\n", "  1)2020\n", "  2)2019\n", "  3)Назад\n" };
            while (true)
            {

                int chek = ViewMenu(menuText2);

                switch (chek)
                {
                    case 1:
                        god = "2020";
                        goto ljk;
                        break;
                    case 2:
                        god = "2019";
                        goto ljk;
                        break;
                    case 3:
                        goto lookj;
                        break;
                }
            }
            if (!Directory.Exists(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god))
            {
                Directory.CreateDirectory(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god);
            }

        ljk:
            Console.Clear();
            string mes = "";
            string[] menuText3 = { "Выберите месяц\n", "  1)Январь\n", "  2)Февраль\n", "  3)Март\n", "  4)Апрель\n", "  5)Май\n", "  6)Июнь\n", "  7)Июль\n", "  8)Август\n", "  9)Сентябрь\n", "  10)Октябрь\n", "  11)Ноябрь\n", "  12)Декабрь\n", "  13)Назад\n" };
            while (true)
            {

                int chek = ViewMenu(menuText3);

                switch (chek)
                {
                    case 1:
                        mes = "Январь";
                        goto kim;
                        break;
                    case 2:
                        mes = "Февраль";
                        goto kim;
                        break;
                    case 3:
                        mes = "Март";
                        goto kim;
                        break;
                    case 4:
                        mes = "Апрель";
                        goto kim;
                        break;
                    case 5:
                        mes = "Май";
                        goto kim;
                        break;
                    case 6:
                        mes = "Июнь";
                        goto kim;
                        break;
                    case 7:
                        mes = "Июль";
                        goto kim;
                        break;
                    case 8:
                        mes = "Август";
                        goto kim;
                        break;
                    case 9:
                        mes = "Сентябрь";
                        goto kim;
                        break;
                    case 10:
                        mes = "Октябрь";
                        goto kim;
                        break;
                    case 11:
                        mes = "Ноябрь";
                        goto kim;
                        break;
                    case 12:
                        mes = "Декабрь";
                        goto kim;
                        break;
                    case 13:
                        goto da;
                        break;
                }
            }
        kim:
            if (!Directory.Exists(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes))
            {
                Directory.CreateDirectory(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes);
            }
            if (!Directory.Exists(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes + @"\" + dolzh))
            {
                Directory.CreateDirectory(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes + @"\" + dolzh);
            }
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\" + dolzh + @"\");
            string fam = "", ima = "", otch = "";
            string zp = "";
            BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Бухгалтерия\budzhet.dengi", FileMode.Open));
            reader.Close();
            if (dolzh == "Администратор")
            {
                reader = new BinaryReader(File.Open(path11 + @"\Данные\" + dolzh + @"\zarplata.zrp", FileMode.Open));
                while (reader.PeekChar() > -1)
                {
                    zp = reader.ReadString();
                }
                reader.Close();
            }
            else
            {
                reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\zarplata.zrp", FileMode.Open));
                while (reader.PeekChar() > -1)
                {
                    zp = reader.ReadString();
                }
                reader.Close();
            }
            double ndfl = 0, pfr = 0, ffoms = 0, fss = 0, nsl = 0, usno = 0, zarp = 0, itog = 0;
            double ndfl1 = 0, pfr1 = 0, ffoms1 = 0, fss1 = 0, nsl1 = 0, usno1 = 0, zarp1 = 0, itog1 = 0;
            zarp = Double.Parse(zp);
            int schet_sotr = 0;
            double all_itog = 0;
            double budz = 0;

            reader = new BinaryReader(File.Open(path11 + @"\Данные\Бухгалтерия\budzhet.dengi", FileMode.Open));
            int h1 = 1;
            while (reader.PeekChar() > -1)
            {
                string name = reader.ReadString();
                if (h1 == 1)
                    budz = Convert.ToDouble(name);
                h1++;
            }
            reader.Close();
            if (dolzh == "Администратор")
            {
                if (File.Exists(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes + @"\" + dolzh + @"\Администратор" + ".zrp"))
                {
                    Console.WriteLine("Сотрудник АДМИНИСТРАТОР"+" УЖЕ получил зарплату за данный период!");
                    Thread.Sleep(1000);
                }
                if (!File.Exists(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes + @"\" + dolzh + @"\Администратор" + ".zrp"))
                {
                    schet_sotr++;

                    ndfl = (zarp * 13.00) / 100.00;
                    pfr = (zarp * 22.00) / 100.00;
                    ffoms = (zarp * 5.10) / 100.00;
                    fss = (zarp * 2.90) / 100.00;
                    nsl = (zarp * 0.20) / 100.00;
                    usno = (zarp * 6.00) / 100.00;
                    itog = zarp - ndfl;
                    all_itog = all_itog + zarp + pfr + ffoms + fss + nsl + usno - ndfl;

                    if (all_itog > budz)
                    {
                        Console.WriteLine("Бюджет фирмы не позволяет выплатить зарплату Администратору");
                        Thread.Sleep(2000);
                        schet_sotr--;
                        goto ujkl;
                    }

                    if (all_itog < budz)
                    {
                        BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes + @"\" + dolzh + @"\Администратор" + ".zrp", FileMode.OpenOrCreate));
                        writer.Write(zp);
                        writer.Close();
                        budz = budz - (zarp + pfr + ffoms + fss + nsl + usno - ndfl);
                        itog1 = itog1 + itog;
                        pfr1 = pfr1 + pfr;
                        ffoms1 = ffoms1 + ffoms;
                        fss1 = fss1 + fss;
                        nsl1 = nsl1 + nsl;
                        usno1 = usno1 + usno;
                        ndfl1 = ndfl1 + ndfl;
                    }
                }
            }

            if (dolzh != "Администратор")
            {
                foreach (var item in dir1.GetFiles())
                {
                    if (item.Name != "zarplata.zrp")
                    {
                        reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + item.Name, FileMode.Open));
                        int h = 1;
                        while (reader.PeekChar() > -1)
                        {
                            string name = reader.ReadString();
                            if (h == 3)
                                fam = name;
                            if (h == 4)
                                ima = name;
                            if (h == 5)
                                otch = name;
                            h++;
                        }
                        reader.Close();
                        if (File.Exists(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes + @"\" + dolzh + @"\" + Path.GetFileNameWithoutExtension(item.Name) + ".zrp"))
                        {
                            Console.WriteLine("Сотрудник " + fam + " " + ima + " УЖЕ получил зарплату за данный период!");
                            Thread.Sleep(1000);
                        }

                        if (!File.Exists(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes + @"\" + dolzh + @"\" + Path.GetFileNameWithoutExtension(item.Name) + ".zrp"))
                        {

                            schet_sotr++;

                            ndfl = (zarp * 13.00) / 100.00;
                            pfr = (zarp * 22.00) / 100.00;
                            ffoms = (zarp * 5.10) / 100.00;
                            fss = (zarp * 2.90) / 100.00;
                            nsl = (zarp * 0.20) / 100.00;
                            usno = (zarp * 6.00) / 100.00;
                            itog = zarp - ndfl;
                            all_itog = all_itog + zarp + pfr + ffoms + fss + nsl + usno - ndfl;

                            if (all_itog > budz)
                            {
                                Console.WriteLine("Бюджет фирмы не позволяет выплатить зарплату " + fam + " " + ima);
                                Thread.Sleep(2000);
                                schet_sotr--;
                                goto ujkl;
                            }

                            if (all_itog < budz)
                            {
                                BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes + @"\" + dolzh + @"\" + Path.GetFileNameWithoutExtension(item.Name) + ".zrp", FileMode.OpenOrCreate));
                                writer.Write(zp);
                                writer.Close();
                                budz = budz - (zarp + pfr + ffoms + fss + nsl + usno - ndfl);
                                itog1 = itog1 + itog;
                                pfr1 = pfr1 + pfr;
                                ffoms1 = ffoms1 + ffoms;
                                fss1 = fss1 + fss;
                                nsl1 = nsl1 + nsl;
                                usno1 = usno1 + usno;
                                ndfl1 = ndfl1 + ndfl;
                            }
                        }
                    }
                }
            }
            ujkl: Console.Clear();

            Console.WriteLine("Зарплату получили: " + schet_sotr + " сотрудников");
            Console.WriteLine("Начислено сотрудникам (с учётом вычета НДС): " + (itog1));
            Console.WriteLine("Отчислено в ПФР: " + pfr1);
            Console.WriteLine("Отчислено в ФФОМС: " + ffoms1);
            Console.WriteLine("Отчислено в ФСС: " + fss1);
            Console.WriteLine("Отчислено в НСЛ: " + nsl1);
            Console.WriteLine("Отчислено в УСНО: " + usno1);
            Console.WriteLine("Отчислено в ФНС: " + ndfl1);
            Console.WriteLine();
            if (schet_sotr != 0)
            {
                File.Delete(path11 + @"\Данные\Бухгалтерия\budzhet.dengi");
                BinaryWriter writer11 = new BinaryWriter(File.Open(path11 + @"\Данные\Бухгалтерия\budzhet.dengi", FileMode.OpenOrCreate));
                writer11.Write(Convert.ToString(budz));
                writer11.Close();
                Console.WriteLine("Остаток на фирме: " + budz);
            }
            Thread.Sleep(10000);
            Buhg_menu();





        net:
            Console.Clear();
            dir1 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\" + dolzh + @"\");
            int j = 1;
            string[] nums2 = new string[10000];
            int[] checker3 = new int[1000];
            fam = ""; ima = ""; otch = "";
            if (dolzh == "Администратор")
                goto start;
            foreach (var item in dir1.GetFiles())
            {
                if (item.Name != "zarplata.zrp")
                {
                    nums2[j] = item.Name;
                    checker3[j] = j;
                    reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\"+dolzh+@"\"+item.Name, FileMode.Open));
                    int h = 1;
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
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
                    j++;
                }
            }
            if (j == 1)
            {
                Console.WriteLine("Нет сотрудников");
                Thread.Sleep(2000);
                Buhg_menu();
            }
            int j2 = 0;
            bool flag1 = true;
            if (j > 1)
            {
            st1: Console.Write("Выбор: ");
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
                    goto elfgfs;
                }
            }
        elfgfs:
            Console.Clear();
            god = "";
            string[] menuText22 = { "Выберите год\n", "  1)2020\n", "  2)2019\n", "  3)Назад\n" };
            while (true)
            {

                int chek = ViewMenu(menuText22);

                switch (chek)
                {
                    case 1:
                        god = "2020";
                        goto ljk2;
                        break;
                    case 2:
                        god = "2019";
                        goto ljk2;
                        break;
                    case 3:
                        Console.Clear();
                        goto net;
                        break;
                }
            }
            if (!Directory.Exists(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god))
            {
                Directory.CreateDirectory(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god);
            }

        ljk2:
            Console.Clear();
            mes = "";
            string[] menuText33 = { "Выберите месяц\n", "  1)Январь\n", "  2)Февраль\n", "  3)Март\n", "  4)Апрель\n", "  5)Май\n", "  6)Июнь\n", "  7)Июль\n", "  8)Август\n", "  9)Сентябрь\n", "  10)Октябрь\n", "  11)Ноябрь\n", "  12)Декабрь\n", "  13)Назад\n" };
            while (true)
            {

                int chek = ViewMenu(menuText33);

                switch (chek)
                {
                    case 1:
                        mes = "Январь";
                        goto kim1;
                        break;
                    case 2:
                        mes = "Февраль";
                        goto kim1;
                        break;
                    case 3:
                        mes = "Март";
                        goto kim1;
                        break;
                    case 4:
                        mes = "Апрель";
                        goto kim1;
                        break;
                    case 5:
                        mes = "Май";
                        goto kim1;
                        break;
                    case 6:
                        mes = "Июнь";
                        goto kim1;
                        break;
                    case 7:
                        mes = "Июль";
                        goto kim1;
                        break;
                    case 8:
                        mes = "Август";
                        goto kim1;
                        break;
                    case 9:
                        mes = "Сентябрь";
                        goto kim1;
                        break;
                    case 10:
                        mes = "Октябрь";
                        goto kim1;
                        break;
                    case 11:
                        mes = "Ноябрь";
                        goto kim1;
                        break;
                    case 12:
                        mes = "Декабрь";
                        goto kim1;
                        break;
                    case 13:
                        goto net;
                        break;
                }
            }
        kim1:
            if (!Directory.Exists(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes))
            {
                Directory.CreateDirectory(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes);
            }
            if (!Directory.Exists(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes + @"\" + dolzh))
            {
                Directory.CreateDirectory(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes + @"\" + dolzh);
            }
            if (File.Exists(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes + @"\" + dolzh + @"\" + Path.GetFileNameWithoutExtension(nums2[j2]) + ".zrp"))
            {
                Console.WriteLine("Сотрудник " + fam + " " + ima + " УЖЕ получил зарплату за данный период!");
                Thread.Sleep(2000);
                Buhg_menu();
            }
            ndfl = 0; pfr = 0; ffoms = 0; fss = 0; nsl = 0; usno = 0; zarp = 0; itog = 0;
            ndfl1 = 0; pfr1 = 0; ffoms1 = 0; fss1 = 0; nsl1 = 0; usno1 = 0; zarp1 = 0; itog1 = 0;
            schet_sotr = 0;
            all_itog = 0;
            budz = 0;

            reader = new BinaryReader(File.Open(path11 + @"\Данные\Бухгалтерия\budzhet.dengi", FileMode.Open));
            h1 = 1;
            while (reader.PeekChar() > -1)
            {
                string name = reader.ReadString();
                if (h1 == 1)
                    budz = Convert.ToDouble(name);
                h1++;
            }
            reader.Close();

            reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\zarplata.zrp", FileMode.Open));
            zp = "";
            while (reader.PeekChar() > -1)
            {
                zp = reader.ReadString();
            }
            reader.Close();
            zarp = Double.Parse(zp);

            if (!File.Exists(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes + @"\" + dolzh + @"\" + Path.GetFileNameWithoutExtension(nums2[j2]) + ".zrp"))
            {
                schet_sotr++;

                ndfl = (zarp * 13.00) / 100.00;
                pfr = (zarp * 22.00) / 100.00;
                ffoms = (zarp * 5.10) / 100.00;
                fss = (zarp * 2.90) / 100.00;
                nsl = (zarp * 0.20) / 100.00;
                usno = (zarp * 6.00) / 100.00;
                itog = zarp - ndfl;
                all_itog = all_itog + zarp + pfr + ffoms + fss + nsl + usno - ndfl;

                if (all_itog > budz)
                {
                    Console.WriteLine("Бюджет фирмы не позволяет выплатить зарплату " + fam + " " + ima);
                    Thread.Sleep(2000);
                    schet_sotr--;
                    goto jhuj;
                }
                if (all_itog < budz)
                {
                    BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes + @"\" + dolzh + @"\" + Path.GetFileNameWithoutExtension(nums2[j2]) + ".zrp", FileMode.OpenOrCreate));
                    writer.Write(zp);
                    writer.Close();
                    budz = budz - (zarp + pfr + ffoms + fss + nsl + usno - ndfl);
                    itog1 = itog1 + itog;
                    pfr1 = pfr1 + pfr;
                    ffoms1 = ffoms1 + ffoms;
                    fss1 = fss1 + fss;
                    nsl1 = nsl1 + nsl;
                    usno1 = usno1 + usno;
                    ndfl1 = ndfl1 + ndfl;
                }
                Console.WriteLine();
            }
            jhuj: Console.Clear();

            Console.WriteLine("Зарплату получили: " + schet_sotr + " сотрудников");
            Console.WriteLine("Начислено сотрудникам (с учётом вычета НДС): " + (itog1));
            Console.WriteLine("Отчислено в ПФР: " + pfr1);
            Console.WriteLine("Отчислено в ФФОМС: " + ffoms1);
            Console.WriteLine("Отчислено в ФСС: " + fss1);
            Console.WriteLine("Отчислено в НСЛ: " + nsl1);
            Console.WriteLine("Отчислено в УСНО: " + usno1);
            Console.WriteLine("Отчислено в ФНС: " + ndfl1);
            Console.WriteLine();
            if (schet_sotr != 0)
            {
                File.Delete(path11 + @"\Данные\Бухгалтерия\budzhet.dengi");
                BinaryWriter writer1 = new BinaryWriter(File.Open(path11 + @"\Данные\Бухгалтерия\budzhet.dengi", FileMode.OpenOrCreate));
                writer1.Write(Convert.ToString(budz));
                writer1.Close();
                Console.WriteLine("Остаток на фирме: " + budz);
            }
            Thread.Sleep(10000);
            Buhg_menu();
        }
        public void LookViplaty()
        {
        elfgfs:
            Console.Clear();
            string path11 = Directory.GetCurrentDirectory();
            string god = "";
            string[] menuText22 = { "Выберите год\n", "  1)2020\n", "  2)2019\n", "  3)Меню\n" };
            while (true)
            {

                int chek = ViewMenu(menuText22);

                switch (chek)
                {
                    case 1:
                        god = "2020";
                        goto ljk2;
                        break;
                    case 2:
                        god = "2019";
                        goto ljk2;
                        break;
                    case 3:
                        Console.Clear();
                        Buhg_menu();
                        break;
                }
            }
            if (!Directory.Exists(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god))
            {
                Console.Clear();
                Console.WriteLine("Выплат за данный год не производилось!");
                Thread.Sleep(5000);
                goto elfgfs;
            }
        ljk2:
            Console.Clear();
            string mes = "";
            string[] menuText33 = { "Выберите месяц\n", "  1)Январь\n", "  2)Февраль\n", "  3)Март\n", "  4)Апрель\n", "  5)Май\n", "  6)Июнь\n", "  7)Июль\n", "  8)Август\n", "  9)Сентябрь\n", "  10)Октябрь\n", "  11)Ноябрь\n", "  12)Декабрь\n", "  13)Назад\n" };
            while (true)
            {

                int chek = ViewMenu(menuText33);

                switch (chek)
                {
                    case 1:
                        mes = "Январь";
                        goto kim1;
                        break;
                    case 2:
                        mes = "Февраль";
                        goto kim1;
                        break;
                    case 3:
                        mes = "Март";
                        goto kim1;
                        break;
                    case 4:
                        mes = "Апрель";
                        goto kim1;
                        break;
                    case 5:
                        mes = "Май";
                        goto kim1;
                        break;
                    case 6:
                        mes = "Июнь";
                        goto kim1;
                        break;
                    case 7:
                        mes = "Июль";
                        goto kim1;
                        break;
                    case 8:
                        mes = "Август";
                        goto kim1;
                        break;
                    case 9:
                        mes = "Сентябрь";
                        goto kim1;
                        break;
                    case 10:
                        mes = "Октябрь";
                        goto kim1;
                        break;
                    case 11:
                        mes = "Ноябрь";
                        goto kim1;
                        break;
                    case 12:
                        mes = "Декабрь";
                        goto kim1;
                        break;
                    case 13:
                        goto elfgfs;
                        break;
                }
            }
        kim1:
            if (!Directory.Exists(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes))
            {
                Console.Clear();
                Console.WriteLine("Выплат за данный месяц не производилось!");
                Thread.Sleep(5000);
                goto ljk2;
            }
            string dolzh = "";
            int j = 1;
            string[] nums2 = new string[10000];
            int[] checker3 = new int[1000];
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes);
            foreach (var item in dir1.GetDirectories())
            {
                nums2[j] = item.Name;
                checker3[j] = j;
                Console.WriteLine(j + ") " + item.Name);
                j++;

            }
            if (j == 1)
            {
                Console.WriteLine("Нет выплат");
                Thread.Sleep(2000);
                goto ljk2;
            }
            int j2 = 0;
            bool flag1 = true;
            if (j > 1)
            {
            st1: Console.Write("Выбор: ");
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
                    dolzh = nums2[j2];
                    goto elfgfs1;
                }
            }
        elfgfs1:
            Console.Clear();
            dir1 = new DirectoryInfo(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes + @"\" + dolzh);
            j = 1;
            nums2 = new string[10000];
            checker3 = new int[1000];
            string fam = "", ima = "", otch = "";
            foreach (var item in dir1.GetFiles())
            {
                nums2[j] = item.Name;
                if (dolzh == "Администратор")
                {
                    j++;
                }
                if (File.Exists(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + Path.GetFileNameWithoutExtension(nums2[j]) + ".login"))
                {
                    BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\" + Path.GetFileNameWithoutExtension(nums2[j]) + ".login", FileMode.Open));
                    int h1 = 1;
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (h1 == 3)
                            fam = name;
                        if (h1 == 4)
                            ima = name;
                        if (h1 == 5)
                            otch = name;
                        h1++;
                    }
                    reader.Close();
                    checker3[j] = j;
                    Console.WriteLine(j + ") " + fam+" "+ima+" "+otch);
                    j++;
                }
            }
            if (j == 1)
            {
                Console.WriteLine("Нет выплат / сотрудники,котороым производились выплаты - уволены");
                Thread.Sleep(2000);
                goto ljk2;
            }
            j2 = 0;
            flag1 = true;
            if (j > 1)
            {
                if (dolzh == "Администратор")
                {
                    flag1 = true;
                    goto rur;
                }
            st1: Console.Write("Выбор: ");
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

                rur: if (flag1 == true)
                {
                    Console.Clear();
                    Console.WriteLine("За " + mes + " " + god + " года выбранный сотрудник получил:");
                    BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Администратор\zarplata.zrp", FileMode.Open));
                    reader.Close();
                    if (dolzh == "Администратор")
                    {
                        reader = new BinaryReader(File.Open(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes + @"\" + dolzh + @"\Администратор.zrp", FileMode.Open));
                    }
                    else
                    {
                        reader = new BinaryReader(File.Open(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god + @"\" + mes + @"\" + dolzh + @"\" + nums2[j2], FileMode.Open));
                    }
                    string zp = "";
                    while (reader.PeekChar() > -1)
                    {
                        zp = reader.ReadString();
                    }
                    reader.Close();
                    double zarp = Convert.ToDouble(zp);
                    double ndfl = (zarp * 13.00) / 100.00;
                    double pfr = (zarp * 22.00) / 100.00;
                    double ffoms = (zarp * 5.10) / 100.00;
                    double fss = (zarp * 2.90) / 100.00;
                    double nsl = (zarp * 0.20) / 100.00;
                    double usno = (zarp * 6.00) / 100.00;
                    double itog = zarp - ndfl;
                    double ubitok = pfr + ffoms + fss + nsl + usno + itog;
                    Console.WriteLine("Начислено сотруднику (с учётом вычета НДС): " + (itog));
                    Console.WriteLine("Отчислено в ПФР: " + pfr);
                    Console.WriteLine("Отчислено в ФФОМС: " + ffoms);
                    Console.WriteLine("Отчислено в ФСС: " + fss);
                    Console.WriteLine("Отчислено в НСЛ: " + nsl);
                    Console.WriteLine("Отчислено в УСНО: " + usno);
                    Console.WriteLine("Отчислено в ФНС: " + ndfl);
                    Console.WriteLine();
                    Console.WriteLine("Убыток из фирмы на зарплату сотрудника: " + ubitok);
                    Thread.Sleep(5000);
                    goto elfgfs;
                }
            }
        }
        public void LookPribil()
        {
            Console.Clear();
        elfgfs:
            Console.Clear();
            string path11 = Directory.GetCurrentDirectory();
            string god = "";
            string[] menuText22 = { "Выберите год\n", "  1)2020\n", "  2)2019\n", "  3)Меню\n" };
            while (true)
            {

                int chek = ViewMenu(menuText22);

                switch (chek)
                {
                    case 1:
                        god = "2020";
                        goto ljk2;
                        break;
                    case 2:
                        god = "2019";
                        goto ljk2;
                        break;
                    case 3:
                        Console.Clear();
                        Buhg_menu();
                        break;
                }
            }
            if (!Directory.Exists(path11 + @"\Данные\Бухгалтерия\Зарплаты\" + god))
            {
                Console.Clear();
                Console.WriteLine("Выплат за данный год не производилось!");
                Thread.Sleep(5000);
                goto elfgfs;
            }
        ljk2:
            Console.Clear();
            string mes = "";
            string[] menuText33 = { "Выберите месяц\n", "  1)Январь\n", "  2)Февраль\n", "  3)Март\n", "  4)Апрель\n", "  5)Май\n", "  6)Июнь\n", "  7)Июль\n", "  8)Август\n", "  9)Сентябрь\n", "  10)Октябрь\n", "  11)Ноябрь\n", "  12)Декабрь\n", "  13)Назад\n" };
            while (true)
            {

                int chek = ViewMenu(menuText33);

                switch (chek)
                {
                    case 1:
                        mes = "1";
                        goto kim1;
                        break;
                    case 2:
                        mes = "2";
                        goto kim1;
                        break;
                    case 3:
                        mes = "3";
                        goto kim1;
                        break;
                    case 4:
                        mes = "4";
                        goto kim1;
                        break;
                    case 5:
                        mes = "5";
                        goto kim1;
                        break;
                    case 6:
                        mes = "6";
                        goto kim1;
                        break;
                    case 7:
                        mes = "7";
                        goto kim1;
                        break;
                    case 8:
                        mes = "8";
                        goto kim1;
                        break;
                    case 9:
                        mes = "9";
                        goto kim1;
                        break;
                    case 10:
                        mes = "10";
                        goto kim1;
                        break;
                    case 11:
                        mes = "11";
                        goto kim1;
                        break;
                    case 12:
                        mes = "12";
                        goto kim1;
                        break;
                    case 13:
                        goto elfgfs;
                        break;
                }
            }
        kim1:
            Console.Clear();
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Бухгалтерия\Заказы");
            string summa = "", date = "";
            double itog = 0;
            int j = 1;
            foreach (var item in dir1.GetFiles())
            {
                BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Бухгалтерия\Заказы\"+item.Name, FileMode.Open));
                int h1 = 1;
                while (reader.PeekChar() > -1)
                {
                    string name = reader.ReadString();
                    if (h1 == 1)
                        summa = name;
                    if (h1 == 2)
                        date = name;
                    h1++;
                }
                reader.Close();
                DateTime data = Convert.ToDateTime(date);
                string mes_dat = data.Month.ToString();
                string god_dat = data.Year.ToString();
                if (mes_dat == mes && god == god_dat)
                {
                    j++;
                    Console.WriteLine(Path.GetFileNameWithoutExtension(item.Name)+": ");
                    Console.WriteLine("  Прибыль: "+summa);
                    itog = itog + Convert.ToDouble(summa);
                }
            }
            if (j==1)
            {
                Console.WriteLine("Прибыли в текущем месяце нет!");
                Thread.Sleep(2000);
                Buhg_menu();
            }
            if (j>1)
            {
                Console.WriteLine();
                Console.WriteLine("Прибыль: "+itog);
                Thread.Sleep(10000);
                Buhg_menu();
            }
        }
        public void LookBudzet()
        {
            Console.Clear();
            string path11 = Directory.GetCurrentDirectory();
            BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Бухгалтерия\budzhet.dengi", FileMode.Open));
            int h1 = 1;
            double budz = 0;
            while (reader.PeekChar() > -1)
            {
                string name = reader.ReadString();
                if (h1 == 1)
                    budz = Convert.ToDouble(name);
                h1++;
            }
            reader.Close();
            Console.WriteLine("Бюджет фирмы: " + budz);
            Console.WriteLine();
            Console.WriteLine("Нажми Enter - вернуться в меню");
            Console.ReadKey();
        }
        public void ChangeBudzet()
        {
            Console.Clear();
            string path11 = Directory.GetCurrentDirectory();
            BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Бухгалтерия\budzhet.dengi", FileMode.Open));
            int h1 = 1;
            double budz = 0;
            while (reader.PeekChar() > -1)
            {
                string name = reader.ReadString();
                if (h1 == 1)
                    budz = Convert.ToDouble(name);
                h1++;
            }
            reader.Close();
            Console.WriteLine("Бюджет фирмы: " + budz);
            Console.WriteLine();
            agvib: Console.WriteLine("Выберите, что вы желаете сделать:");
            Console.WriteLine("1. Перевод денег");
            Console.WriteLine("2. Пополнение бюджета");
            Console.Write("Выбор: ");
            string vibor = Console.ReadLine();
            if (!Directory.Exists(path11 + @"\Данные\Бухгалтерия\Операции по счёту"))
                Directory.CreateDirectory(path11 + @"\Данные\Бухгалтерия\Операции по счёту");
            if (!Directory.Exists(path11 + @"\Данные\Бухгалтерия\Операции по счёту\Переводы"))
                Directory.CreateDirectory(path11 + @"\Данные\Бухгалтерия\Операции по счёту\Переводы");
            if (!Directory.Exists(path11 + @"\Данные\Бухгалтерия\Операции по счёту\Пополнения"))
                Directory.CreateDirectory(path11 + @"\Данные\Бухгалтерия\Операции по счёту\Пополнения");

            if (vibor == "1")
            {
                Console.Clear();
                double perevod = 0;
                per: Console.Write("Введите сумму перевода: ");
                try
                {
                    perevod = Convert.ToDouble(Console.ReadLine());
                    if (perevod > budz || perevod <= 0)
                    {
                        Console.WriteLine("Сумма перевода не может превышать баланс бюджета!");
                        Thread.Sleep(2000);
                        goto per;
                    }
                }
                catch
                {
                    Console.WriteLine("Неккоректно введена сумма перевода");
                    Thread.Sleep(2000);
                    goto per;
                }
                Random rnd = new Random();

                jkhfd: int nomer_perevoda = rnd.Next(54321, 99999);
                if (File.Exists(path11 + @"\Данные\Бухгалтерия\Операции по счёту\Переводы\Перевод " + nomer_perevoda + ".perevod"))
                    goto jkhfd;

                budz = budz - perevod;
                File.Delete(path11 + @"\Данные\Бухгалтерия\budzhet.dengi");
                BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Бухгалтерия\budzhet.dengi", FileMode.OpenOrCreate));
                writer.Write(Convert.ToString(budz));
                writer.Close();

                writer = new BinaryWriter(File.Open(path11+@"\Данные\Бухгалтерия\Операции по счёту\Переводы\Перевод "+nomer_perevoda+".perevod", FileMode.OpenOrCreate));
                writer.Write(Convert.ToString(perevod));
                writer.Write(Convert.ToString(DateTime.Now.ToShortDateString()));
                writer.Close();

                Console.Clear();
                Console.WriteLine("Перевод " + nomer_perevoda + " успешно выполнен!");
                Console.WriteLine("Остаток на счёте: " + budz);
                Thread.Sleep(5000);
                Buhg_menu();
            }
            else if (vibor == "2")
            {
                Console.Clear();
                double popolnenie = 0;
            per: Console.Write("Введите сумму пополнения: ");
                try
                {
                    popolnenie = Convert.ToDouble(Console.ReadLine());
                    if (popolnenie <= 0)
                    {
                        Console.WriteLine("Не может быть меньше 0 и равно ему");
                        Thread.Sleep(2000);
                        goto per;
                    }
                }
                catch
                {
                    Console.WriteLine("Неккоректно введена сумма пополнения");
                    Thread.Sleep(2000);
                    goto per;
                }
                Random rnd = new Random();

            jkhfd: int nomer_perevoda = rnd.Next(54321, 99999);
                if (File.Exists(path11 + @"\Данные\Бухгалтерия\Операции по счёту\Пополнения\Пополнение " + nomer_perevoda + ".perevod"))
                    goto jkhfd;

                budz = budz + popolnenie;
                File.Delete(path11 + @"\Данные\Бухгалтерия\budzhet.dengi");
                BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Бухгалтерия\budzhet.dengi", FileMode.OpenOrCreate));
                writer.Write(Convert.ToString(budz));
                writer.Close();

                writer = new BinaryWriter(File.Open(path11+@"\Данные\Бухгалтерия\Операции по счёту\Пополнения\Пополнение " + nomer_perevoda + ".perevod", FileMode.OpenOrCreate));
                writer.Write(Convert.ToString(popolnenie));
                writer.Write(Convert.ToString(DateTime.Now.ToShortDateString()));
                writer.Close();

                Console.Clear();
                Console.WriteLine("Пополнение " + nomer_perevoda + " успешно выполнен!");
                Console.WriteLine("Остаток на счёте: " + budz);
                Thread.Sleep(5000);
                Buhg_menu();

            }
            else if (vibor != "1" || vibor != "2")
            {
                Console.WriteLine("Error");
                Thread.Sleep(3000);
                goto agvib;
            }
        }
        public void ChangeZP()
        {
            Console.Clear();
            string dolzh = "";
            string[] menuText33 = { "Выберите должность\n", "  1)Администратор\n", "  2)Бухгалтер\n", "  3)Кассир-продавец\n", "  4)Кладовщик\n", "  5)Отдел-кадров\n","  6)Меню\n" };
            while (true)
            {

                int chek = ViewMenu(menuText33);

                switch (chek)
                {
                    case 1:
                        dolzh = "Администратор";
                        goto kim1;
                        break;
                    case 2:
                        dolzh = "Бухгалтер";
                        goto kim1;
                        break;
                    case 3:
                        dolzh = "Кассир-продавец";
                        goto kim1;
                        break;
                    case 4:
                        dolzh = "Кладовщик";
                        goto kim1;
                        break;
                    case 5:
                        dolzh = "Отдел-кадров";
                        goto kim1;
                        break;
                    case 6:
                        Buhg_menu();
                        break;
                }
            }
            kim1:
            Console.Clear();
            string path11 = Directory.GetCurrentDirectory();
            string zp = "";
            if (dolzh == "Администратор")
            {
                BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\" + dolzh + @"\zarplata.zrp", FileMode.Open));
                while (reader.PeekChar() > -1)
                {
                    zp = reader.ReadString();
                }
                reader.Close();
            }
            if (dolzh != "Администратор")
            {
                BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\zarplata.zrp", FileMode.Open));
                while (reader.PeekChar() > -1)
                {
                    zp = reader.ReadString();
                }
                reader.Close();
            }
            Console.WriteLine("Установленная месячная зарплата [" + dolzh + "]: " + zp + " рублей");
            agd: Console.Write("Введите новую зарплату: ");
            double zp1 = 0;
            try
            {
                zp1 = Convert.ToDouble(Console.ReadLine());
                if (zp1 < 9000)
                {
                    Console.WriteLine("Не может быть меньше 9000 рублей!");
                    Thread.Sleep(2000);
                    goto agd;
                }    
            }
            catch
            {
                Console.WriteLine("Хм, не похоже на число");
                Thread.Sleep(2000);
                goto agd;
            }
            double ndfl = 0, pfr = 0, ffoms = 0, fss = 0, nsl = 0, usno = 0, zarp = 0, itog = 0;
            Console.Clear();
            Console.WriteLine("Новая установленная зарплата [" + dolzh + "]: " + zp1);
            if (dolzh != "Администратор")
            {
                File.Delete(path11 + @"\Данные\Сотрудники\" + dolzh + @"\zarplata.zrp");
                BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\zarplata.zrp", FileMode.OpenOrCreate));
                writer.Write(Convert.ToString(zp1));
                writer.Close();
            }
            if (dolzh == "Администратор")
            {
                File.Delete(path11 + @"\Данные\" + dolzh + @"\zarplata.zrp");
                BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\" + dolzh + @"\zarplata.zrp", FileMode.OpenOrCreate));
                writer.Write(Convert.ToString(zp1));
                writer.Close();
            }

            zarp = zp1;
            ndfl = (zarp * 13.00) / 100.00;
            pfr = (zarp * 22.00) / 100.00;
            ffoms = (zarp * 5.10) / 100.00;
            fss = (zarp * 2.90) / 100.00;
            nsl = (zarp * 0.20) / 100.00;
            usno = (zarp * 6.00) / 100.00;
            itog = zarp - ndfl;

            Console.WriteLine("Из этой зарплаты: ");
            Console.WriteLine("13% НДС из ЗП: " + ndfl);
            Console.WriteLine();
            Console.WriteLine("Работодатель отчисляет: ");
            Console.WriteLine("В ПФР: " + pfr);
            Console.WriteLine("ФФОМС: " + ffoms);
            Console.WriteLine("ФФС: " + fss);
            Console.WriteLine("НСЛ: " + nsl);
            Console.WriteLine("УСНО: " + usno);
            Console.WriteLine();
            Console.WriteLine("С учётом НДС [" + dolzh + "] получает: " + itog);
            Thread.Sleep(15000);
            Console.Clear();
            Buhg_menu();
        }
        public void LookSchet()
        {
        again: Console.Clear();
            Console.Clear();
            string dolzh = "";
            string[] menuText33 = { "Выберите тип операции\n", "  1)Пополнения\n", "  2)Переводы\n", "  3)Меню\n" };
            while (true)
            {

                int chek = ViewMenu(menuText33);

                switch (chek)
                {
                    case 1:
                        dolzh = "Пополнения";
                        goto kim1;
                        break;
                    case 2:
                        dolzh = "Переводы";
                        goto kim1;
                        break;
                    case 3:
                        Buhg_menu();
                        break;
                }
            }
        kim1:
            Console.Clear();
            string period = "";
            string[] menuText31 = { "Выберите период\n", "  1)Текущий год\n", "  2)Текущий месяц\n", "  3)Назад\n" };
            while (true)
            {

                int chek = ViewMenu(menuText31);

                switch (chek)
                {
                    case 1:
                        goto god;
                        break;
                    case 2:
                        goto mes;
                        break;
                    case 3:
                        goto again;
                        break;
                }
            }
        god:
            Console.Clear();
            string path11 = Directory.GetCurrentDirectory();
            string god = DateTime.Now.Year.ToString();
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Бухгалтерия\Операции по счёту\" + dolzh);
            string data = "", summa = "";
            string god_file = "";
            double itog = 0;
            int j = 0;
            foreach (var item in dir1.GetFiles())
            {
                BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Бухгалтерия\Операции по счёту\" + dolzh + @"\" + item.Name, FileMode.Open));
                int h = 1;
                while (reader.PeekChar() > -1)
                {
                    string name = reader.ReadString();
                    if (h == 1)
                        summa = name;
                    if (h == 2)
                        data = name;
                    h++;
                }
                reader.Close();
                DateTime date = new DateTime();
                date = Convert.ToDateTime(data);
                god_file = date.Year.ToString();
                if (god_file == god)
                {
                    Console.WriteLine(Path.GetFileNameWithoutExtension(item.Name) + ": сумма: " + summa + "; Дата: " + data);
                    itog = itog + Convert.ToDouble(summa);
                    j++;
                }
            }
            if (j==0)
            {
                Console.WriteLine("Операций за данный год нет!");
                Thread.Sleep(4000);
            }
            if (j > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Итог за год: " + itog);
                Thread.Sleep(8000);
            }

        mes:
            Console.Clear();
            path11 = Directory.GetCurrentDirectory();
            god = DateTime.Now.Year.ToString();
            string mesac = DateTime.Now.Month.ToString();
            dir1 = new DirectoryInfo(path11 + @"\Данные\Бухгалтерия\Операции по счёту\" + dolzh);
            data = ""; summa = "";
            god_file = "";
            string mesac_file = "";
            itog = 0;
            j = 0;
            foreach (var item in dir1.GetFiles())
            {
                BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Бухгалтерия\Операции по счёту\" + dolzh + @"\" + item.Name, FileMode.Open));
                int h = 1;
                while (reader.PeekChar() > -1)
                {
                    string name = reader.ReadString();
                    if (h == 1)
                        summa = name;
                    if (h == 2)
                        data = name;
                    h++;
                }
                reader.Close();
                DateTime date = new DateTime();
                date = Convert.ToDateTime(data);
                god_file = date.Year.ToString();
                mesac_file = date.Month.ToString();
                if (god_file == god && mesac==mesac_file)
                {
                    Console.WriteLine(Path.GetFileNameWithoutExtension(item.Name) + ": сумма: " + summa + "; Дата: " + data);
                    itog = itog + Convert.ToDouble(summa);
                    j++;
                }
            }
            if (j == 0)
            {
                Console.WriteLine("Операций за данный месяц нет!");
                Thread.Sleep(4000);
            }
            if (j > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Итог за месяц: " + itog);
                Thread.Sleep(8000);
            }
        }
    }
}
