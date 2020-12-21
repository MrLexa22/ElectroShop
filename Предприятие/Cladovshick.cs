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
    class Cladovshick
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

            DirectoryInfo dir = new DirectoryInfo(path11 + @"\Данные\Сотрудники\Кладовщик");
            foreach (var item in dir.GetFiles())
            {
                if (login + ".login" == item.Name)
                {
                    Console.Write("Введите пароль: ");
                    string parol = ReadPasswordLine();
                    string log = "", pas = "";
                    int i = 0;
                    BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\Кладовщик\" + login + ".login", FileMode.Open));
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
                        Console.WriteLine("Вы вошли как кладовщик!");
                        Thread.Sleep(1500);
                        Clad_Menu();
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
            Pokupatel pre = new Pokupatel();
            pre.Authentication(login);
        }
        public void Clad_Menu()
        {
            Console.Clear();
            string[] menuText = { "Выберите\n", "  1)Добавление категорий товаров\n", "  2)Оформить поставку товаров (занести товары в базу данных)\n", "  3)Списать товар\n", "  4)Изменить категорию товара\n", "  5)Просмотреть свои сведения о заработной плате\n", "  6)Удаление категории товаров\n", "  7)Просмотреть категории товаров\n", "  8)Просмотреть сведения о товаре\n", "  9)Изменить сведения о товаре (цена, кол-во на складе, срок годности)\n", "  10)Выход\n" };
            Authorization auth = new Authorization();
            while (true)
            {

                int chek = ViewMenu(menuText);

                switch (chek)
                {
                    case 1:
                        AddCategory();
                        break;
                    case 2:
                        Oformlenie();
                        break;
                    case 3:
                        Spisanie();
                        break;
                    case 4:
                        ChangeCategory();
                        break;
                    case 5:
                        LookZP();
                        break;
                    case 6:
                        DelCategory();
                        break;
                    case 7:
                        LookCagery();
                        break;
                    case 8:
                        LookTovary();
                        break;
                    case 9:
                        ChangeTovary();
                        break;
                    case 10:
                        MenuVibor lol = new MenuVibor();
                        lol.Vibor();
                        break;
                }
            }
        }
        public void AddCategory()
        {
        ag1: string path11 = Directory.GetCurrentDirectory();
            Console.Clear();
            Console.Write("Введите название категории: ");
            string nazvanie = Console.ReadLine();
            if (nazvanie == "" || nazvanie == " " || nazvanie == "  " || nazvanie.Length<2)
            {
                Console.WriteLine("Название не должно быть пустым! Длина названия не меньше 2 символов");
                Thread.Sleep(3000);
                goto ag1;
            }
            if (Directory.Exists(path11 + @"\Данные\Товары\" + nazvanie))
            {
                Console.WriteLine("Данная категория уже существует!");
                Thread.Sleep(3000);
                goto ag1;
            }
            if (!Directory.Exists(path11 + @"\Данные\Товары\" + nazvanie))
            {
                try
                {
                    Console.WriteLine("Категория успешно создана");
                    Directory.CreateDirectory(path11 + @"\Данные\Товары\" + nazvanie);
                    Thread.Sleep(3000);
                    Clad_Menu();
                }
                catch
                {
                    Console.WriteLine("ОТМЕНА! Название не удовлетворяет системным требованиям!");
                    Thread.Sleep(3000);
                    goto ag1;
                }
            }
        }
        public void Oformlenie()
        {
            st1: Console.Clear();
            Console.WriteLine("Выберите категорию добавляемых товаров: ");
            Console.WriteLine();
            string path11 = Directory.GetCurrentDirectory();
            string[] nums2 = new string[10000];
            int[] checker3 = new int[1000];
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Товары\");
            int j = 1;
            foreach (var item in dir1.GetDirectories())
            {
                nums2[j] = item.Name;
                Console.WriteLine(j + ") " + Path.GetFileNameWithoutExtension(nums2[j]));
                checker3[j] = j;
                j++;
            }
            if (j==1)
            {
                Console.WriteLine("Невозможно оформить поставку!");
                Console.WriteLine("Добавьте сначала категории товаров!");
                Thread.Sleep(5000);
                Clad_Menu();
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
                    Console.Clear();
                    Console.WriteLine("Вы выбрали категорию товаров: " + nums2[j2]);
                    Console.WriteLine();
                    goto tyu1;
                }
            }
            tyu1:
            Console.Write("Введите кол-во товаров: ");
            string kolvo1 = Console.ReadLine();
            int kolvo_chislo = 0;
            try
            {
                kolvo_chislo = Convert.ToInt32(kolvo1);
                if (kolvo_chislo == 0)
                {
                    Console.WriteLine("Кол-во товаров не может быть нулём!");
                    Console.WriteLine();
                    goto tyu1;
                }
            }
            catch
            {
                Console.WriteLine("Не число!");
                Thread.Sleep(2000);
                goto tyu1;
            }
            int[] kolvo = new int[kolvo_chislo];

            string[] tovary_nazvanie = new string[10000];
            string[] tovary_kolvo = new string[10000];
            string[] tovary_cena = new string[10000];
            string[] tovary_data = new string[10000];

            for (int i = 0; i < kolvo_chislo; i++)
            {
            ag1: Console.Write("Введите название товара [" + (i + 1) + "]: ");
                string nazvanie = Console.ReadLine();
                if (nazvanie == "" || nazvanie == " " || nazvanie == "  " || nazvanie.Length < 2)
                {
                    Console.WriteLine("Название не должно быть пустым! Длина названия не меньше 2 символов");
                    Thread.Sleep(3000);
                    goto ag1;
                }
                if (File.Exists(path11 + @"\Данные\Товары\" + nums2[j2] + @"\" + nazvanie + @".tovar"))
                {
                    Console.WriteLine("Данный товар в данной категории уже существует!");
                    Thread.Sleep(3000);
                    goto ag1;
                }
                if (!File.Exists(path11 + @"\Данные\Товары\" + nums2[j2] + @"\" + nazvanie + @".tovar"))
                {
                    try
                    {
                        using (File.Create(path11 + @"\Данные\Товары\" + nums2[j2] + @"\" + nazvanie + @".tovar")) ;
                        goto tyu11;
                    }
                    catch
                    {
                        Console.WriteLine("ОТМЕНА! Название не удовлетворяет системным требованиям!");
                        Thread.Sleep(3000);
                        goto ag1;
                    }
                }

            tyu11: Console.WriteLine();
                Console.Write("Введите кол-во товара: ");
                string kolvo11 = Console.ReadLine();
                int kloichestvo = 0;
                try
                {
                    kloichestvo = Convert.ToInt32(kolvo11);
                    if (kloichestvo == 0)
                    {
                        Console.WriteLine("Кол-во товаров не может быть нулём!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                        goto tyu11;
                    }
                }
                catch
                {
                    Console.WriteLine("Не целое число!");
                    Thread.Sleep(2000);
                    goto tyu11;
                }

            tyu12: Console.WriteLine();
                Console.Write("Введите цену товара (например, 666,66): ");
                string chena1 = Console.ReadLine();
                double chena = 0;
                try
                {
                    chena = Convert.ToDouble(chena1);
                    if (chena == 0)
                    {
                        Console.WriteLine("Цена товара не может быть нулём!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                        goto tyu12;
                    }
                }
                catch
                {
                    Console.WriteLine("Не число!");
                    Thread.Sleep(2000);
                    goto tyu12;
                }

            tyu13:
                Console.WriteLine();
                string data = "";
                Console.WriteLine("Есть ли срок годности у товара?");
                Console.WriteLine("1. Да");
                Console.WriteLine("2. Нет");
                Console.Write("Выбор: ");
                string vih = "";
                vih = Console.ReadLine();
                if (vih == "1" || vih == "2")
                {
                    goto tim1;
                }
                else
                {
                    Console.WriteLine("Error!");
                    Thread.Sleep(2000);
                    goto tyu13;
                }

                tim1:
                if (vih == "2")
                {
                    data = " ";
                    goto tuy15;
                }
                if (vih == "1")
                {
                tyu14:
                    DateTime data1 = new DateTime(2001, 1, 1);
                    Console.Write("Введите дату, когда истекает срок годности (день.месяц.год:01.01.2001): ");
                    data = Console.ReadLine();
                    try
                    {
                        data1 = Convert.ToDateTime(data);
                        data = data1.ToString("dd.MM.yyyy");
                        if (data1.Year == DateTime.Now.Year)
                        {
                            if (data1.Month > DateTime.Now.Month)
                            {
                                goto tuy15;
                            }
                            if (data1.Month < DateTime.Now.Month)
                            {
                                Console.WriteLine("Не удволетворяет условиям!");
                                Thread.Sleep(2000);
                                goto tyu14;
                            }
                            if (data1.Month == DateTime.Now.Month)
                            {
                                if (data1.Day <= DateTime.Now.Day)
                                {
                                    Console.WriteLine("Не удволетворяет условиям!");
                                    Thread.Sleep(2000);
                                    goto tyu14;
                                }
                                if (data1.Day > DateTime.Now.Day)
                                {
                                    goto tuy15;
                                }
                            }
                        }
                        if (data1.Year > DateTime.Now.Year)
                        {
                            goto tuy15;
                        }
                        if (data1.Year < DateTime.Now.Year)
                        {
                            Console.WriteLine("Не удволетворяет условиям!");
                            Thread.Sleep(2000);
                            goto tyu14;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Не удовлетворяет заданным условиям!");
                        Thread.Sleep(3000);
                        goto tyu13;
                    }
                }

            tuy15:
                Console.WriteLine("Товар создан!");
                File.Delete(path11 + @"\Данные\Товары\" + nums2[j2] + @"\" + nazvanie + @".tovar");
                BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Товары\" + nums2[j2] + @"\" + nazvanie + @".tovar", FileMode.OpenOrCreate));
                writer.Write(nazvanie);
                writer.Write(kolvo11);
                writer.Write(chena1);
                writer.Write(data);
                writer.Close();
                Thread.Sleep(3000);
                Console.Clear();
                //Clad_Menu();
            }
        }

        public void Spisanie()
        {
        st1: Console.Clear();
            Console.WriteLine("Выберите категорию товара, которые желаете списать: ");
            Console.WriteLine();
            string path11 = Directory.GetCurrentDirectory();
            string[] nums2 = new string[10000];
            int[] checker3 = new int[1000];
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Товары\");
            int j = 1;
            foreach (var item in dir1.GetDirectories())
            {
                nums2[j] = item.Name;
                Console.WriteLine(j + ") " + Path.GetFileNameWithoutExtension(nums2[j]));
                checker3[j] = j;
                j++;
            }
            if (j == 1)
            {
                Console.WriteLine("Нет категорий, нет товаров");
                Console.WriteLine("Добавьте сначала категории товаров!");
                Thread.Sleep(5000);
                Clad_Menu();
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
                    Console.Clear();
                    Console.WriteLine("Вы выбрали категорию товаров: " + nums2[j2]);
                    Console.WriteLine();
                    goto tyu1;
                }
            }
        tyu1:
            Console.WriteLine("Выберите товар, которые желаете списать:");
            path11 = Directory.GetCurrentDirectory();
            string[] tovar = new string[10000];
            checker3 = new int[1000];
            dir1 = new DirectoryInfo(path11 + @"\Данные\Товары\" + nums2[j2]);
            j = 1;
            foreach (var item in dir1.GetFiles())
            {
                tovar[j] = item.Name;
                Console.WriteLine(j + ") " + Path.GetFileNameWithoutExtension(tovar[j]));
                checker3[j] = j;
                j++;
            }
            if (j == 1)
            {
                Console.WriteLine("Нет товаров в данной категории");
                Thread.Sleep(5000);
                Clad_Menu();
            }
            int j22 = 0;
            flag1 = true;
            if (j > 1)
            {
                Console.Write("Выбор: ");
                string otvetr = Console.ReadLine();
                try
                {
                    j22 = Convert.ToInt32(otvetr);
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
                    if (checker3[i1] == j22 && checker3[i1] != 0)
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
                    Console.WriteLine("Вы выбрали товар: " + Path.GetFileNameWithoutExtension(tovar[j22]));
                    Console.WriteLine();
                    Console.WriteLine("Товар был списан!");
                    File.Delete(path11 + @"\Данные\Товары\" + nums2[j2] + @"\" + tovar[j22]);
                    Thread.Sleep(2000);
                    Clad_Menu();
                }
            }
        }
        public void ChangeCategory()
        {
        st1: Console.Clear();
            Console.WriteLine("Выберите категорию товара, который желаете переместить в другую категорию: ");
            Console.WriteLine();
            string path11 = Directory.GetCurrentDirectory();
            string[] nums2 = new string[10000];
            int[] checker3 = new int[1000];
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Товары\");
            int j = 1;
            foreach (var item in dir1.GetDirectories())
            {
                nums2[j] = item.Name;
                Console.WriteLine(j + ") " + Path.GetFileNameWithoutExtension(nums2[j]));
                checker3[j] = j;
                j++;
            }
            if (j == 1)
            {
                Console.WriteLine("Нет категорий, нет товаров");
                Console.WriteLine("Добавьте сначала категории товаров!");
                Thread.Sleep(5000);
                Clad_Menu();
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
                    Console.Clear();
                    Console.WriteLine("Вы выбрали категорию товаров: " + nums2[j2]);
                    Console.WriteLine();
                    goto tyu1;
                }
            }
        tyu1:
            Console.WriteLine("Выберите товар, которые желаете переместить в другую категорию:");
            path11 = Directory.GetCurrentDirectory();
            string[] tovar = new string[10000];
            checker3 = new int[1000];
            dir1 = new DirectoryInfo(path11 + @"\Данные\Товары\" + nums2[j2]);
            j = 1;
            foreach (var item in dir1.GetFiles())
            {
                tovar[j] = item.Name;
                Console.WriteLine(j + ") " + Path.GetFileNameWithoutExtension(tovar[j]));
                checker3[j] = j;
                j++;
            }
            if (j == 1)
            {
                Console.WriteLine("Нет товаров в данной категории");
                Thread.Sleep(5000);
                Clad_Menu();
            }
            int j22 = 0;
            flag1 = true;
            if (j > 1)
            {
                Console.Write("Выбор: ");
                string otvetr = Console.ReadLine();
                try
                {
                    j22 = Convert.ToInt32(otvetr);
                }
                catch
                {
                    Console.WriteLine("Error!");
                    Thread.Sleep(2000);
                    goto tyu1;
                }

                flag1 = false;
                for (int i1 = 0; i1 < j; ++i1)
                {
                    if (checker3[i1] == j22 && checker3[i1] != 0)
                    {
                        flag1 = true;
                    }
                }

                if (flag1 == false)
                {
                    Console.WriteLine("Error");
                    Thread.Sleep(2000);
                    goto tyu1;
                }
                if (flag1 == true)
                {
                    Console.Clear();
                    Console.WriteLine("Вы выбрали товар: " + Path.GetFileNameWithoutExtension(tovar[j22]));
                    Console.WriteLine();
                    Thread.Sleep(2000);
                    goto st11;
                }
            }
            st11:
            Console.Clear();
            Console.WriteLine("Выберите категорию, куда желаете переместить товар: ");
            Console.WriteLine();
            path11 = Directory.GetCurrentDirectory();
            string[] nums23 = new string[10000];
            int[] checker33 = new int[1000];
            dir1 = new DirectoryInfo(path11 + @"\Данные\Товары\");
            j = 1;
            foreach (var item in dir1.GetDirectories())
            {
                if (item.Name != nums2[j2])
                {
                    nums23[j] = item.Name;
                    Console.WriteLine(j + ") " + Path.GetFileNameWithoutExtension(nums23[j]));
                    checker33[j] = j;
                    j++;
                }
            }
            if (j == 1)
            {
                Console.WriteLine("Нет категорий, нет товаров");
                Console.WriteLine("Добавьте сначала категории товаров!");
                Thread.Sleep(5000);
                Clad_Menu();
            }
            int j23 = 0;
            flag1 = true;
            if (j > 1)
            {
                Console.Write("Выбор: ");
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

                flag1 = false;
                for (int i1 = 0; i1 < j; ++i1)
                {
                    if (checker33[i1] == j23 && checker33[i1] != 0)
                    {
                        flag1 = true;
                    }
                }

                if (flag1 == false)
                {
                    Console.WriteLine("Error");
                    Thread.Sleep(2000);
                    goto st11;
                }
                if (flag1 == true)
                {
                    Console.Clear();
                    Console.WriteLine("Вы выбрали категорию товаров: " + nums23[j23]);
                    Console.WriteLine();
                    goto tyu11;
                }
                tyu11:
                if(File.Exists(path11+ @"\Данные\Товары\"+ nums23[j23]+@"\"+ tovar[j22]))
                {
                    Console.WriteLine("Ошибка! Данный товар уже существует в этой категории!");
                    Thread.Sleep(5000);
                    Clad_Menu();
                }
                if (!File.Exists(path11 + @"\Данные\Товары\" + nums23[j23] + @"\" + tovar[j22]))
                {
                    Console.WriteLine("Товар был перемещён в другую категорию");
                    File.Move(path11 + @"\Данные\Товары\" + nums2[j2] + @"\" + tovar[j22], path11 + @"\Данные\Товары\" + nums23[j23] + @"\" + tovar[j22]);
                    Thread.Sleep(5000);
                    Clad_Menu();
                }
            }
        }
        public void LookZP()
        {
            Console.Clear();
            string path11 = Directory.GetCurrentDirectory();
            BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\Кладовщик\zarplata.zrp", FileMode.Open));
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
            Clad_Menu();
        }
        public void DelCategory()
        {
        st1: Console.Clear();
            Console.WriteLine("ВНИМАНИЕ! ПРИ УДАЛЕНИИ КАТЕГОРИИ ВСЕ ТОВАРЫ ЭТОЙ КАТЕГОРИИ БУДУТ УДАЛЕНЫ!");
            Console.WriteLine("ДЛЯ ВЫХОДА ИЗ МЕНЮ НАЖМИТЕ КЛАВИШУ ^F3^");
            Console.WriteLine("Выберите категорию товаров, которую желаете удалить: ");
            Console.WriteLine();
            string path11 = Directory.GetCurrentDirectory();
            string[] nums2 = new string[10000];
            int[] checker3 = new int[1000];
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Товары\");
            int j = 1;
            foreach (var item in dir1.GetDirectories())
            {
                nums2[j] = item.Name;
                Console.WriteLine(j + ") " + Path.GetFileNameWithoutExtension(nums2[j]));
                checker3[j] = j;
                j++;
            }
            if (j == 1)
            {
                Console.WriteLine("Нет категорий, нет товаров");
                Console.WriteLine("Добавьте сначала категории товаров!");
                Thread.Sleep(5000);
                Clad_Menu();
            }
            int j2 = 0;
            bool flag1 = true;
            if (j > 1)
            {
                while (true)
                {
                    Console.WriteLine("Для выхода - F3");
                    Console.WriteLine("Продолжить? Нажми Enter!");
                    if (Console.ReadKey(true).Key == ConsoleKey.F3)
                    {
                        Clad_Menu();
                    }
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        goto dfg;
                    }
                    dfg: Console.WriteLine();
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
                        Console.Clear();
                        Console.WriteLine("Вы выбрали категорию товаров: " + nums2[j2]);
                        Console.WriteLine();
                        Console.WriteLine("Категория удалена!");
                        Directory.Delete(path11 + @"\Данные\Товары\" + nums2[j2], true);
                        Thread.Sleep(2000);
                        Clad_Menu();
                    }
                }
            }

        }
        public void LookCagery()
        {
            Console.Clear();
            Console.WriteLine("Записанные категории товаров:");
            string path11 = Directory.GetCurrentDirectory();
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Товары\");
            int j = 1;
            foreach (var item in dir1.GetDirectories())
            {
                Console.WriteLine(j + ") " + Path.GetFileNameWithoutExtension(item.Name));
                j++;
            }
            if (j == 1)
            {
                Console.WriteLine("Нет категорий, нет товаров");
                Console.WriteLine("Добавьте сначала категории товаров!");
                Thread.Sleep(5000);
                Clad_Menu();
            }
            Console.WriteLine();
            Console.WriteLine("Для возврата в меню нажимте Enter");
            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    Clad_Menu();
                }
            }
        }
        public void LookTovary()
        {
            st1: Console.Clear();
            Console.WriteLine("Выберите категорию товаров: ");
            Console.WriteLine();
            string path11 = Directory.GetCurrentDirectory();
            string[] nums2 = new string[10000];
            int[] checker3 = new int[1000];
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Товары\");
            int j = 1;
            foreach (var item in dir1.GetDirectories())
            {
                nums2[j] = item.Name;
                Console.WriteLine(j + ") " + Path.GetFileNameWithoutExtension(nums2[j]));
                checker3[j] = j;
                j++;
            }
            if (j == 1)
            {
                Console.WriteLine("Нет категорий, нет товаров");
                Console.WriteLine("Добавьте сначала категории товаров!");
                Thread.Sleep(5000);
                Clad_Menu();
            }
            int j2 = 0;
            bool flag1 = true;
            if (j > 1)
            {
            dfg: Console.WriteLine();
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
                    Console.Clear();
                    Console.WriteLine("Вы выбрали категорию товаров: " + nums2[j2]);
                    goto jkl;
                }
            }
            jkl:
            Console.WriteLine("Выберите товар:");
            path11 = Directory.GetCurrentDirectory();
            string[] tovar = new string[10000];
            checker3 = new int[1000];
            dir1 = new DirectoryInfo(path11 + @"\Данные\Товары\" + nums2[j2]);
            j = 1;
            foreach (var item in dir1.GetFiles())
            {
                tovar[j] = item.Name;
                Console.WriteLine(j + ") " + Path.GetFileNameWithoutExtension(tovar[j]));
                checker3[j] = j;
                j++;
            }
            if (j == 1)
            {
                Console.WriteLine("Нет товаров в данной категории");
                Thread.Sleep(5000);
                Clad_Menu();
            }
            int j22 = 0;
            flag1 = true;
            if (j > 1)
            {
                Console.Write("Выбор: ");
                string otvetr = Console.ReadLine();
                try
                {
                    j22 = Convert.ToInt32(otvetr);
                }
                catch
                {
                    Console.WriteLine("Error!");
                    Thread.Sleep(2000);
                    goto jkl;
                }

                flag1 = false;
                for (int i1 = 0; i1 < j; ++i1)
                {
                    if (checker3[i1] == j22 && checker3[i1] != 0)
                    {
                        flag1 = true;
                    }
                }

                if (flag1 == false)
                {
                    Console.WriteLine("Error");
                    Thread.Sleep(2000);
                    goto jkl;
                }
                if (flag1 == true)
                {
                    Console.Clear();
                    Console.WriteLine("Вы выбрали товар: " + Path.GetFileNameWithoutExtension(tovar[j22]));
                    Console.WriteLine();
                    Thread.Sleep(2000);
                    goto st11;
                }
            }
        st11:
            Console.Clear();
            Console.WriteLine("Сведения о товаре [" + Path.GetFileNameWithoutExtension(tovar[j22]) + "]");
            BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Товары\" + nums2[j2] + @"\" + tovar[j22], FileMode.OpenOrCreate));
            string nazvanie = "", kolvo1 = "", chena1 = "", data = "";
            int h = 1;
            while (reader.PeekChar() > -1)
            {
                string name = reader.ReadString();
                if (h == 1)
                    nazvanie = name;
                if (h == 2)
                    kolvo1 = name;
                if (h == 3)
                    chena1 = name;
                if (h == 4)
                    data = name;
                h++;
            }
            reader.Close();
            Console.WriteLine();
            Console.WriteLine("Наименование: "+nazvanie);
            Console.WriteLine("Категория: " + nums2[j2]);
            Console.WriteLine("Количество на складе: "+kolvo1);
            Console.WriteLine("Цена за 1 штуку: "+chena1);
            Console.WriteLine("Срок годности: "+data);
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("Для выхода в меню - F3");
                Console.WriteLine("Выбрать другой товар для просмотра - Нажми Enter!");
                if (Console.ReadKey(true).Key == ConsoleKey.F3)
                {
                    Clad_Menu();
                }
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    goto st1;
                }
            }
        }
        public void ChangeTovary()
        {
        st1: Console.Clear();
            Console.WriteLine("Выберите категорию товаров: ");
            Console.WriteLine();
            string path11 = Directory.GetCurrentDirectory();
            string[] nums2 = new string[10000];
            int[] checker3 = new int[1000];
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Товары\");
            int j = 1;
            foreach (var item in dir1.GetDirectories())
            {
                nums2[j] = item.Name;
                Console.WriteLine(j + ") " + Path.GetFileNameWithoutExtension(nums2[j]));
                checker3[j] = j;
                j++;
            }
            if (j == 1)
            {
                Console.WriteLine("Нет категорий, нет товаров");
                Console.WriteLine("Добавьте сначала категории товаров!");
                Thread.Sleep(5000);
                Clad_Menu();
            }
            int j2 = 0;
            bool flag1 = true;
            if (j > 1)
            {
            dfg: Console.WriteLine();
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
                    Console.Clear();
                    Console.WriteLine("Вы выбрали категорию товаров: " + nums2[j2]);
                    goto jkl;
                }
            }
        jkl:
            Console.WriteLine("Выберите товар, которые желаете изменить:");
            path11 = Directory.GetCurrentDirectory();
            string[] tovar = new string[10000];
            checker3 = new int[1000];
            dir1 = new DirectoryInfo(path11 + @"\Данные\Товары\" + nums2[j2]);
            j = 1;
            foreach (var item in dir1.GetFiles())
            {
                tovar[j] = item.Name;
                Console.WriteLine(j + ") " + Path.GetFileNameWithoutExtension(tovar[j]));
                checker3[j] = j;
                j++;
            }
            if (j == 1)
            {
                Console.WriteLine("Нет товаров в данной категории");
                Thread.Sleep(5000);
                Clad_Menu();
            }
            int j22 = 0;
            flag1 = true;
            if (j > 1)
            {
                Console.Write("Выбор: ");
                string otvetr = Console.ReadLine();
                try
                {
                    j22 = Convert.ToInt32(otvetr);
                }
                catch
                {
                    Console.WriteLine("Error!");
                    Thread.Sleep(2000);
                    goto jkl;
                }

                flag1 = false;
                for (int i1 = 0; i1 < j; ++i1)
                {
                    if (checker3[i1] == j22 && checker3[i1] != 0)
                    {
                        flag1 = true;
                    }
                }

                if (flag1 == false)
                {
                    Console.WriteLine("Error");
                    Thread.Sleep(2000);
                    goto jkl;
                }
                if (flag1 == true)
                {
                    Console.Clear();
                    goto jhd;
                }
            }
        jhd:
            Console.Clear();
            //Console.WriteLine("Сведения о товаре [" + Path.GetFileNameWithoutExtension(tovar[j22]) + "]");
            BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Товары\" + nums2[j2] + @"\" + tovar[j22], FileMode.OpenOrCreate));
            string nazvanie = "", kolvo1 = "", chena1 = "", data = "";
            int h = 1;
            while (reader.PeekChar() > -1)
            {
                string name = reader.ReadString();
                if (h == 1)
                    nazvanie = name;
                if (h == 2)
                    kolvo1 = name;
                if (h == 3)
                    chena1 = name;
                if (h == 4)
                    data = name;
                h++;
            }
            reader.Close();
            /*Console.WriteLine();
            Console.WriteLine("Наименование: " + nazvanie);
            Console.WriteLine("Категория: " + nums2[j2]);
            Console.WriteLine("Количество на складе: " + kolvo1);
            Console.WriteLine("Цена за 1 штуку: " + chena1);
            Console.WriteLine("Срок годности: " + data);
            Console.WriteLine();*/
            string[] menuText = { "Выберите что изменить\n", "  1)Наименование (" + nazvanie + ")\n", "  2)Количество на складе (" + kolvo1 + ")\n", "  3)Цену за 1 штуку (" + chena1 + ")\n", "  4)Срок годности (" + data + ")\n", "  5)Ничего (возврат в меню)\n" };
            Authorization auth = new Authorization();
            while (true)
            {

                int chek = ViewMenu(menuText);

                switch (chek)
                {
                    case 1:
                        goto naimenovanie;
                        break;
                    case 2:
                        goto chang_kolvo;
                        break;
                    case 3:
                        goto chang_cena;
                        break;
                    case 4:
                        goto chang_data;
                        break;
                    case 5:
                        Clad_Menu();
                        break;
                }
            }

        naimenovanie:
        ag1: Console.Clear();
            Console.Write("Введите новое название товара: ");
            nazvanie = Console.ReadLine();
            if (nazvanie == "" || nazvanie == " " || nazvanie == "  " || nazvanie.Length < 2)
            {
                Console.WriteLine("Название не должно быть пустым! Длина названия не меньше 2 символов");
                Thread.Sleep(3000);
                goto ag1;
            }
            if (File.Exists(path11 + @"\Данные\Товары\" + nums2[j2] + @"\" + nazvanie + @".tovar"))
            {
                Console.WriteLine("Данное название уже есть в данной категории!");
                Thread.Sleep(2000);
                goto ag1;
            }
            if (!File.Exists(path11 + @"\Данные\Товары\" + nums2[j2] + @"\" + nazvanie + @".tovar"))
            {
                try
                {
                    File.Delete(path11 + @"\Данные\Товары\" + nums2[j2] + @"\" + tovar[j22]);
                    Console.WriteLine("Название товара изменено!");
                    BinaryWriter writer1 = new BinaryWriter(File.Open(path11 + @"\Данные\Товары\" + nums2[j2] + @"\" + nazvanie + @".tovar", FileMode.OpenOrCreate));
                    writer1.Write(nazvanie);
                    writer1.Write(kolvo1);
                    writer1.Write(chena1);
                    writer1.Write(data);
                    writer1.Close();
                    Thread.Sleep(3000);
                    Console.Clear();
                    Clad_Menu();
                }
                catch
                {
                    Console.WriteLine("ОТМЕНА! Название не удовлетворяет системным требованиям!");
                    Thread.Sleep(3000);
                    goto ag1;
                }
            }

            chang_kolvo:
            Console.Clear();
            Console.Write("Введите кол-во товара: ");
            string kolvo11 = Console.ReadLine();
            int kloichestvo = 0;
            try
            {
                kloichestvo = Convert.ToInt32(kolvo11);
                if (kloichestvo == 0)
                {
                    Console.WriteLine("Кол-во товаров не может быть нулём!");
                    Thread.Sleep(2000);
                    Console.WriteLine();
                    goto chang_kolvo;
                }
            }
            catch
            {
                Console.WriteLine("Не целое число!");
                Thread.Sleep(2000);
                goto chang_kolvo;
            }
            File.Delete(path11 + @"\Данные\Товары\" + nums2[j2] + @"\" + tovar[j22]);
            Console.WriteLine("Количество товара изменено!");
            BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Товары\" + nums2[j2] + @"\" + nazvanie + @".tovar", FileMode.OpenOrCreate));
            writer.Write(nazvanie);
            writer.Write(kolvo11);
            writer.Write(chena1);
            writer.Write(data);
            writer.Close();
            Thread.Sleep(3000);
            Console.Clear();
            Clad_Menu();

            chang_cena:
            Console.Clear();
            Console.Write("Введите цену товара (например, 666,66): ");
            string chena11 = Console.ReadLine();
            double chena = 0;
            try
            {
                chena = Convert.ToDouble(chena11);
                if (chena == 0)
                {
                    Console.WriteLine("Цена товара не может быть нулём!");
                    Thread.Sleep(2000);
                    Console.WriteLine();
                    goto chang_cena;
                }
            }
            catch
            {
                Console.WriteLine("Не число!");
                Thread.Sleep(2000);
                goto chang_cena;
            }
            File.Delete(path11 + @"\Данные\Товары\" + nums2[j2] + @"\" + tovar[j22]);
            Console.WriteLine("Цена товара изменена!");
            BinaryWriter writer2 = new BinaryWriter(File.Open(path11 + @"\Данные\Товары\" + nums2[j2] + @"\" + nazvanie + @".tovar", FileMode.OpenOrCreate));
            writer2.Write(nazvanie);
            writer2.Write(kolvo1);
            writer2.Write(chena11);
            writer2.Write(data);
            writer2.Close();
            Thread.Sleep(3000);
            Console.Clear();
            Clad_Menu();

            chang_data:
            Console.Clear();
            DateTime data1 = new DateTime(2001, 1, 1);
            Console.Write("Введите дату, когда истекает срок годности (день.месяц.год:01.01.2001): ");
            data = Console.ReadLine();
            try
            {
                data1 = Convert.ToDateTime(data);
                data = data1.ToString("dd.MM.yyyy");
                if (data1.Year == DateTime.Now.Year)
                {
                    if (data1.Month > DateTime.Now.Month)
                    {
                        goto chang_data1;
                    }
                    if (data1.Month < DateTime.Now.Month)
                    {
                        Console.WriteLine("Не удволетворяет условиям!");
                        Thread.Sleep(2000);
                        goto chang_data;
                    }
                    if (data1.Month == DateTime.Now.Month)
                    {
                        if (data1.Day <= DateTime.Now.Day)
                        {
                            Console.WriteLine("Не удволетворяет условиям!");
                            Thread.Sleep(2000);
                            goto chang_data;
                        }
                        if (data1.Day > DateTime.Now.Day)
                        {
                            goto chang_data1;
                        }
                    }
                }
                if (data1.Year > DateTime.Now.Year)
                {
                    goto chang_data1;
                }
                if (data1.Year < DateTime.Now.Year)
                {
                    Console.WriteLine("Не удволетворяет условиям!");
                    Thread.Sleep(2000);
                    goto chang_data;
                }
            }
            catch
            {
                Console.WriteLine("Не удовлетворяет заданным условиям!");
                Thread.Sleep(3000);
                goto chang_data;
            }
            chang_data1:
            File.Delete(path11 + @"\Данные\Товары\" + nums2[j2] + @"\" + tovar[j22]);
            Console.WriteLine("Срок годности товара изменена!");
            BinaryWriter writer3 = new BinaryWriter(File.Open(path11 + @"\Данные\Товары\" + nums2[j2] + @"\" + nazvanie + @".tovar", FileMode.OpenOrCreate));
            writer3.Write(nazvanie);
            writer3.Write(kolvo1);
            writer3.Write(chena1);
            writer3.Write(data);
            writer3.Close();
            Thread.Sleep(3000);
            Console.Clear();
            Clad_Menu();
        }
    }
}
