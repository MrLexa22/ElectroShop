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
    class Pokupatel
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

            DirectoryInfo dir = new DirectoryInfo(path11 + @"\Данные\Покупатели");
            foreach (var item in dir.GetFiles())
            {
                if (login + ".login" == item.Name)
                {
                    Console.Write("Введите пароль: ");
                    string parol = ReadPasswordLine();
                    string log = "", pas = "", em = ""; ;
                    int i = 0;
                    BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Покупатели\" + login + ".login", FileMode.Open));
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (i == 0)
                            log = name;
                        if (i == 1)
                            em = name;
                        if (i == 2)
                            pas = name;
                        i++;
                    }
                    reader.Close();
                    if (parol == pas)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Вы вошли как покупатель!");
                        Thread.Sleep(1500);
                        Pokupatel_menu(login);
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
            Prodavec pre = new Prodavec();
            pre.Authentication(login);
        }
        public void Pokupatel_menu(string login)
        {
            Console.Clear();
            string[] menuText = { "Выберите\n", "  1)Просмотр товаров по категориям\n", "  2)Добавление товаров в корзину\n", "  3)Просмотреть корзину\n", "  4)Изменить кол-во выбр. товара в корзине\n", "  5)Очистить корзину\n", "  6)Удалить товар из корзины\n", "  7)Оформить заказ\n", "  8)Выход\n" };
            Authorization auth = new Authorization();
            while (true)
            {
                int chek = ViewMenu(menuText);

                switch (chek)
                {
                    case 1:
                        LookTovary(login);
                        break;
                    case 2:
                        AddToCart(login);
                        break;
                    case 3:
                        LookCart(login);
                        break;
                    case 4:
                        ChangeKolvo(login);
                        break;
                    case 5:
                        ClearCart(login);
                        break;
                    case 6:
                        DelTovar(login);
                        break;
                    case 7:
                        Oformlenie(login);
                        break;
                    case 8:
                        MenuVibor lol = new MenuVibor();
                        lol.Vibor();
                        break;
                }
            }
        }
        public void LookTovary(string login)
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
                Pokupatel_menu(login);
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
                Pokupatel_menu(login);
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
                    Thread.Sleep(500);
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
            Console.WriteLine("Наименование: " + nazvanie);
            Console.WriteLine("Категория: " + nums2[j2]);
            Console.WriteLine("Количество на складе: " + kolvo1);
            Console.WriteLine("Цена за 1 штуку: " + chena1);
            Console.WriteLine("Срок годности: " + data);
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("Для выхода в меню - F3");
                Console.WriteLine("Выбрать другой товар для просмотра - Нажми Enter!");
                if (Console.ReadKey(true).Key == ConsoleKey.F3)
                {
                    Pokupatel_menu(login);
                }
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    goto st1;
                }
            }
        }
        public void AddToCart(string login)
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
                Pokupatel_menu(login);
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
                Pokupatel_menu(login);
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
                    Thread.Sleep(500);
                    goto st11;
                }
            }
        st11:
            Console.Clear();
            Console.WriteLine("Товар [" + Path.GetFileNameWithoutExtension(tovar[j22]) + "]");
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

            hdfg:
            Console.WriteLine();
            Console.WriteLine("Наименование: " + nazvanie);
            Console.WriteLine("Категория: " + nums2[j2]);
            Console.WriteLine("Количество на складе: " + kolvo1);

            DateTime dt2 = new DateTime(1000, 10, 10);
            double chena11 = 0;
            if (data == "" || data == " " || data == null)
            {
                Console.WriteLine("Цена за 1 штуку: " + chena1);
                goto ugd;
            }
            else
            {
                {
                    dt2 = Convert.ToDateTime(data);
                }

                DateTime dt1 = DateTime.Now;
                var date2 = dt1.AddDays(14);
                string date3 = date2.ToShortDateString();

                DateTime sr_data1 = new DateTime(1000, 10, 10);
                DateTime sr_data2 = new DateTime(1000, 10, 10);

                sr_data1 = Convert.ToDateTime(data);
                sr_data2 = Convert.ToDateTime(date3);

                if (sr_data1 < DateTime.Now)
                {
                    Console.WriteLine("Срок годности товара истёк!");
                    Console.WriteLine("Купить нельзя!");
                    Console.WriteLine("Срок годности: " + data);
                    Thread.Sleep(2000);
                    Console.Clear();
                    Pokupatel_menu(login);
                }
                else if (sr_data2 >= sr_data1)
                {
                    chena11 = Convert.ToDouble(chena1);
                    chena11 = chena11 / 2;
                    chena1 = Convert.ToString(chena11);
                    Console.WriteLine("Цена за 1 штуку (50% скидки): " + chena11);
                }
                else if (sr_data2 < sr_data1)
                {
                    Console.WriteLine("Цена за 1 штуку: " + chena1);
                }
                Console.WriteLine("Срок годности: " + data);
            }
            ugd: Console.WriteLine();
            while (true)
            {
                Console.WriteLine("Выйти в меню, не добавлять в корзину - F3");
                Console.WriteLine("Добавить товар в корзину - Нажми Enter 2 раза!");
                if (Console.ReadKey(true).Key == ConsoleKey.F3)
                {
                    Pokupatel_menu(login);
                }
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    if (!File.Exists(path11+@"\Данные\Покупатели\"+login+@"\"+nazvanie+@".cart"))
                    {
                        BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Покупатели\" + login + @"\" + nazvanie + @".cart", FileMode.OpenOrCreate));
                        writer.Write(nazvanie);
                        string jkldfg = "1";
                        writer.Write(jkldfg);
                        writer.Write(chena1);
                        writer.Write(chena1);
                        writer.Write(nums2[j2]);
                        writer.Close();
                        Console.WriteLine("Товар (1 шт.) добавлен в корзину!");
                        Thread.Sleep(2000);
                        Pokupatel_menu(login);
                    }
                    if (File.Exists(path11 + @"\Данные\Покупатели\" + login + @"\" + nazvanie + @".cart"))
                    {
                        Console.WriteLine("Данный товар уже есть у вас в корзине!");
                        Thread.Sleep(2000);
                        Console.Clear();
                        Pokupatel_menu(login);
                    }
                }
            }
        }
        public void LookCart(string login)
        {
            string path11 = Directory.GetCurrentDirectory();
            string[] nums2 = new string[10000];
            int[] checker3 = new int[1000];
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Покупатели\" + login);
            int j = 1;
            foreach (var item in dir1.GetFiles())
            {
                nums2[j] = item.Name;
                int h = 1;
                string nazv = "", kolvo = "", ishd_cena = "", itog_cena = "", categ = "";
                using (BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Покупатели\" + login + @"\" + nums2[j], FileMode.Open)))
                {
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (h == 1)
                            nazv = name;
                        if (h == 2)
                            kolvo = name;
                        if (h == 3)
                            ishd_cena = name;
                        if (h == 4)
                            itog_cena = name;
                        if (h == 5)
                        {
                            categ = name;
                            Console.WriteLine(j + ") " + nazv + " | (Кол-во: "+kolvo+") (Исх. цена: "+ishd_cena+") (Итог. цена: "+itog_cena+")");
                        }
                        h++;
                    }

                }
                j++;
            }
            if (j == 1)
            {
                Console.WriteLine("Корзина пуста!");
                Thread.Sleep(1500);
                Pokupatel_menu(login);
            }
            if (j > 1)
            {
                while(true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Для возврата в меню - нажмите F3");
                    if (Console.ReadKey(true).Key == ConsoleKey.F3)
                    {
                        Pokupatel_menu(login);
                    }
                }
            }
        }
        public void ChangeKolvo(string login)
        {
            string path11 = Directory.GetCurrentDirectory();
            string[] nums2 = new string[10000];
            int[] checker3 = new int[1000];
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Покупатели\" + login);
            int j = 1;
            foreach (var item in dir1.GetFiles())
            {
                nums2[j] = item.Name;
                int h = 1;
                string nazv = "", kolvo = "", ishd_cena = "", itog_cena = "", categ = "";
                using (BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Покупатели\" + login + @"\" + nums2[j], FileMode.Open)))
                {
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (h == 1)
                            nazv = name;
                        if (h == 2)
                            kolvo = name;
                        if (h == 3)
                            ishd_cena = name;
                        if (h == 4)
                            itog_cena = name;
                        if (h == 5)
                        {
                            categ = name;
                            Console.WriteLine(j + ") " + nazv + " | (Кол-во: " + kolvo + ") (Исх. цена: " + ishd_cena + ") (Итог. цена: " + itog_cena + ")");
                            checker3[j] = j;
                            j++;
                        }
                        h++;
                    }
                }
            }

            if (j == 1)
            {
                Console.WriteLine("Корзина пуста!");
                Thread.Sleep(1500);
                Pokupatel_menu(login);
            }
            if (j > 1)
            {
                int j2 = 0;
                bool flag1 = true;
                Console.WriteLine();
                st1: Console.Write("Выбор: ");
                j2 = Convert.ToInt32(Console.ReadLine());

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
                    Console.WriteLine("Вы выбрали товар [" + Path.GetFileNameWithoutExtension(nums2[j2]) + "]");
                    Console.WriteLine();
                    int h = 1;
                    string nazv = "", kolvo = "", ishd_cena = "", itog_cena = "", categ = "";
                    using (BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Покупатели\" + login + @"\" + nums2[j2], FileMode.Open)))
                    {
                        while (reader.PeekChar() > -1)
                        {
                            string name = reader.ReadString();
                            if (h == 1)
                                nazv = name;
                            if (h == 2)
                                kolvo = name;
                            if (h == 3)
                                ishd_cena = name;
                            if (h == 4)
                                itog_cena = name;
                            if (h == 5)
                            {
                                categ = name;
                                Console.WriteLine("Текущее кол-во: "+kolvo);
                            }
                            h++;
                        }
                    }
                    string cena = "", data = "", kovo_dostup="", nazv1="";
                    h = 1;
                    using (BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Товары\" + categ + @"\" + Path.GetFileNameWithoutExtension(nums2[j2])+".tovar", FileMode.Open)))
                    {
                        while (reader.PeekChar() > -1)
                        {
                            string name = reader.ReadString();
                            if (h == 1)
                                nazv1 = name;
                            if (h == 2)
                                kovo_dostup = name;
                            if (h == 3)
                                cena = name;
                            if (h == 4)
                            {
                                data = name;
                                Console.WriteLine("Доступно на складе: " + kovo_dostup);
                            }
                            h++;
                        }
                    }
                    Console.WriteLine();
                    int kolov_pokup = 0;
                    int kolov_sklad = 0;
                    kolov_pokup = Convert.ToInt32(kolvo);
                    kolov_sklad = Convert.ToInt32(kovo_dostup);

                    agy: Console.Write("Введите новое кол-во товаров: ");
                    string new_kolvo1 = Console.ReadLine();
                    int new_kolvo = 0;
                    try
                    {
                        new_kolvo = Convert.ToInt32(new_kolvo1);
                        if (new_kolvo == 0)
                        {
                            Console.WriteLine("Кол-во товаров не может быть нулём!");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("НЕ ЦЕЛОЕ ЧИСЛО!");
                        Console.WriteLine();
                        Thread.Sleep(2000);
                        goto agy;
                    }

                    if (new_kolvo > kolov_sklad)
                    {
                        Console.WriteLine("Вы не можете добавить в корзину товаров больше, чем на складе!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                        goto agy;
                    }

                    double itog_cena1 = 0;
                    itog_cena1 = Convert.ToDouble(ishd_cena);
                    itog_cena1 = itog_cena1 * new_kolvo;
                    string itog_cenaa = Convert.ToString(itog_cena1);

                    Console.Clear();
                    Console.WriteLine("Вы изменили кол-во товаров!");
                    Console.WriteLine("Новое кол-во товаров: " + new_kolvo1);
                    Console.WriteLine("Новая итоговая цена: " + itog_cenaa);
                    File.Delete(path11 + @"\Данные\Покупатели\" + login + @"\" + nums2[j2]);
                    BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Покупатели\" + login + @"\" + nums2[j2], FileMode.OpenOrCreate));
                    writer.Write(nazv);
                    writer.Write(new_kolvo1);
                    writer.Write(ishd_cena);
                    writer.Write(itog_cenaa);
                    writer.Write(categ);
                    writer.Close();
                    Thread.Sleep(2000);
                    Pokupatel_menu(login);
                }
            }
        }
        public void ClearCart(string login)
        {
            Console.Clear();
            string[] menuText = { "Вы желаете ПОЛНОСТЬЮ ОЧИСТИТЬ свою корзину?!\n", "  1)Абсолютно ДА\n", "  2)НЕТ, мне надо обратно в меню!\n" };
            Authorization auth = new Authorization();
            while (true)
            {
                int chek = ViewMenu(menuText);

                switch (chek)
                {
                    case 1:
                        goto ghj;
                        break;
                    case 2:
                        Pokupatel_menu(login);
                        break;
                }
            }
            ghj:
            string path11 = Directory.GetCurrentDirectory();
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Покупатели\" + login);
            foreach (var item in dir1.GetFiles())
            {
                File.Delete(path11 + @"\Данные\Покупатели\" + login+@"\"+item.Name);
            }
            Console.WriteLine("Корзина полностью очищена!");
            Thread.Sleep(2000);
            Console.Clear();
            Pokupatel_menu(login);
        }
        public void DelTovar(string login)
        {
            Console.Clear();
            Console.WriteLine("Выберите товар, который желаете удалить:");
            string path11 = Directory.GetCurrentDirectory();
            string[] nums2 = new string[10000];
            int[] checker3 = new int[1000];
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Покупатели\" + login);
            int j = 1;
            foreach (var item in dir1.GetFiles())
            {
                nums2[j] = item.Name;
                int h = 1;
                string nazv = "", kolvo = "", ishd_cena = "", itog_cena = "", categ = "";
                using (BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Покупатели\" + login + @"\" + nums2[j], FileMode.Open)))
                {
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (h == 1)
                            nazv = name;
                        if (h == 2)
                            kolvo = name;
                        if (h == 3)
                            ishd_cena = name;
                        if (h == 4)
                            itog_cena = name;
                        if (h == 5)
                        {
                            categ = name;
                            Console.WriteLine(j + ") " + nazv + " | (Кол-во: " + kolvo + ") (Исх. цена: " + ishd_cena + ") (Итог. цена: " + itog_cena + ")");
                            checker3[j] = j;
                            j++;
                        }
                        h++;
                    }
                }
            }

            if (j == 1)
            {
                Console.WriteLine("Корзина пуста!");
                Thread.Sleep(1500);
                Pokupatel_menu(login);
            }
            if (j > 1)
            {
                int j2 = 0;
                bool flag1 = true;
                Console.WriteLine();
            st1: Console.Write("Выбор: ");
                j2 = Convert.ToInt32(Console.ReadLine());

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
                    Console.WriteLine("Вы выбрали товар [" + Path.GetFileNameWithoutExtension(nums2[j2]) + "]");
                    Console.WriteLine();
                    Console.WriteLine("Товар удалён из вашей корзины!");
                    File.Delete(path11 + @"\Данные\Покупатели\" + login + @"\" + nums2[j2]);
                    Thread.Sleep(2000);
                    Console.Clear();
                    Pokupatel_menu(login);
                }
            }
        }
        public void Oformlenie(string login)
        {
            Console.Clear();
            string path11 = Directory.GetCurrentDirectory();
            string[] nums2 = new string[10000];
            int[] checker3 = new int[1000];
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Покупатели\" + login);
            int j = 1;
            double itogova_cena = 0, conitog = 0; ;
            foreach (var item in dir1.GetFiles())
            {
                nums2[j] = item.Name;
                int h = 1;
                string nazv = "", kolvo = "", ishd_cena = "", itog_cena = "", categ = "";
                using (BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Покупатели\" + login + @"\" + nums2[j], FileMode.Open)))
                {
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (h == 1)
                            nazv = name;
                        if (h == 2)
                            kolvo = name;
                        if (h == 3)
                            ishd_cena = name;
                        if (h == 4)
                            itog_cena = name;
                        if (h == 5)
                        {
                            categ = name;
                            conitog = Convert.ToDouble(itog_cena);
                            itogova_cena = itogova_cena + conitog;
                            Console.WriteLine(j + ") " + nazv + " | (Кол-во: " + kolvo + ") (Исх. цена: " + ishd_cena + ") (Итог. цена: " + itog_cena + ")");
                        }
                        h++;
                    }

                }
                j++;
            }
            if (j == 1)
            {
                Console.WriteLine("Корзина пуста!");
                Thread.Sleep(1500);
                Pokupatel_menu(login);
            }
            if (j > 1)
            {
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Общая сумма заказа: " + itogova_cena);
                    Console.WriteLine();
                    Console.WriteLine("Оформить данный заказ - F3");
                    Console.WriteLine("Вернуться в меню - Enter");
                    if (Console.ReadKey(true).Key == ConsoleKey.F3)
                    {
                        goto oform;
                    }
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        Pokupatel_menu(login);
                    }
                }
            }
            oform:
            Console.Clear();
            foreach (var item in dir1.GetFiles())
            {
                nums2[j] = item.Name;
                int h = 1;
                string nazv = "", kolvo = "", ishd_cena = "", itog_cena = "", categ = "";
                using (BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Покупатели\" + login + @"\" + nums2[j], FileMode.Open)))
                {
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (h == 1)
                            nazv = name;
                        if (h == 2)
                            kolvo = name;
                        if (h == 3)
                            ishd_cena = name;
                        if (h == 4)
                            itog_cena = name;
                        if (h == 5)
                        {
                            categ = name;
                            //conitog = Convert.ToDouble(itog_cena);
                            //itogova_cena = itogova_cena + conitog;
                            //Console.WriteLine(j + ") " + nazv + " | (Кол-во: " + kolvo + ") (Исх. цена: " + ishd_cena + ") (Итог. цена: " + itog_cena + ")");
                        }
                        h++;
                    }
                }
                if (File.Exists(path11 + @"\Данные\Товары\" + categ + @"\" + nazv + @".tovar"))
                {
                    h = 1;
                    int kol_vo_zakaz = Convert.ToInt32(kolvo);
                    string nazv_tovar = "", kolvo_tovar = "", cena_tovar = "", data_tovar = "";
                    using (BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Товары\" + categ + @"\" + nazv + @".tovar", FileMode.Open)))
                    {
                        while (reader.PeekChar() > -1)
                        {
                            string name = reader.ReadString();
                            if (h == 1)
                                nazv_tovar = name;
                            if (h == 2)
                                kolvo_tovar = name;
                            if (h == 3)
                                cena_tovar = name;
                            if (h == 4)
                                data_tovar = name;
                            h++;
                        }
                    }
                    int kol_vo_tovar = Convert.ToInt32(kolvo_tovar);
                    if (nazv != nazv_tovar || kol_vo_zakaz > kol_vo_tovar)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Ошибка! Товар [" + nazv + "] не может быть оформлен, т.к. были измненеия на складе! Товар удалён из корзины, повторите попытку ещё раз");
                        File.Delete(path11 + @"\Данные\Покупатели\" + login + @"\" + nums2[j]);
                        Thread.Sleep(5000);
                        Pokupatel_menu(login);
                    }
                    int ostatok = 0;
                    ostatok = kol_vo_tovar - kol_vo_zakaz;
                    string ostatok_zapis = Convert.ToString(ostatok);
                    File.Delete(path11 + @"\Данные\Товары\" + categ + @"\" + nazv + @".tovar");
                    if (ostatok != 0)
                    {
                        using (BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Товары\" + categ + @"\" + nazv + @".tovar", FileMode.OpenOrCreate)))
                        {
                            writer.Write(nazv_tovar);
                            writer.Write(ostatok_zapis);
                            writer.Write(cena_tovar);
                            writer.Write(data_tovar);
                        }
                    }

                    string filo = "", gorop = "";
                    string[] nums22 = new string[10000];
                    int[] checker33 = new int[1000];
                    j = 1;
                    Console.WriteLine("Необходимо выбрать филиал!");
                    DirectoryInfo dir = new DirectoryInfo(path11 + @"\Данные\Сотрудники\");
                    foreach (var item1 in dir.GetFiles())
                    {
                        string lol1 = item1.Name;
                        string check1 = lol1.Substring(lol1.Length - 7);
                        nums22[j] = item1.Name;
                        BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + item1.Name, FileMode.Open));
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
                    }
                    int j22 = 0;
                    bool flag11 = true;
                    if (j > 1)
                    {
                        Console.WriteLine();
                    st11: Console.Write("Выбор: ");
                        string otvetr = Console.ReadLine();
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
                            Console.WriteLine("Заказ будет доставлен в филиал [" + Path.GetFileNameWithoutExtension(nums22[j22]) + "]");
                            juk:
                            if (!Directory.Exists(path11 + @"\Данные\Заказы\Неподтверждённые" + @"\" + Path.GetFileNameWithoutExtension(nums22[j22])))
                            {
                                Directory.CreateDirectory(path11 + @"\Данные\Заказы\Неподтверждённые" + @"\" + Path.GetFileNameWithoutExtension(nums22[j22]));
                            }

                            rnaag: Random rnd = new Random();
                            int nomer_zakaza = rnd.Next(26423, 99999);
                            string nz_st = Convert.ToString(nomer_zakaza);

                            if (Directory.Exists(path11 + @"\Данные\Заказы\Неподтверждённые" + @"\" + Path.GetFileNameWithoutExtension(nums22[j22]) + @"\Заказ " + nz_st))
                            {
                                goto rnaag;
                            }
                            if (Directory.Exists(path11 + @"\Данные\Заказы\Подтверждённые" + @"\" + Path.GetFileNameWithoutExtension(nums22[j22]) + @"\Заказ " + nz_st))
                            {
                                goto rnaag;
                            }
                            if (!Directory.Exists(path11 + @"\Данные\Заказы\Неподтверждённые" + @"\" + Path.GetFileNameWithoutExtension(nums22[j22]) + @"\Заказ " + nz_st))
                            {
                                Directory.CreateDirectory(path11 + @"\Данные\Заказы\Неподтверждённые" + @"\" + Path.GetFileNameWithoutExtension(nums22[j22]) + @"\Заказ " + nz_st);
                            }

                            string em = "", log = "";
                            using (BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Покупатели\" + login + ".login", FileMode.Open)))
                            {
                                h = 1;
                                while (reader.PeekChar() > -1)
                                {
                                    string name1 = reader.ReadString();
                                    if (h == 1)
                                    {
                                        log = name1;
                                    }
                                    if (h == 2)
                                    {
                                        em = name1;
                                    }
                                    h++;
                                }
                            }
                            using (BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Заказы\Неподтверждённые" + @"\" + Path.GetFileNameWithoutExtension(nums22[j22]) + @"\Заказ " + nz_st + @"\" + log + ".login", FileMode.OpenOrCreate)))
                            {
                                writer.Write(log);
                                writer.Write(em);
                            }
                            dir = new DirectoryInfo(path11 + @"\Данные\Покупатели\" + login + @"\");
                            foreach (var item1 in dir.GetFiles())
                            {
                                File.Move(item1.FullName, path11 + @"\Данные\Заказы\Неподтверждённые" + @"\" + Path.GetFileNameWithoutExtension(nums22[j22]) + @"\Заказ " + nz_st + @"\"+item1.Name);
                            }
                            Console.Clear();
                            Console.WriteLine("Заказ оформлен! Корзина очищена! Как только ваш заказ подтвердят - Вам придёт письмо на почту!");
                            Thread.Sleep(2000);
                            Pokupatel_menu(login);
                        }
                    }
                }

                if (!File.Exists(path11 + @"\Данные\Товары\" + categ + @"\" + nazv + @".tovar"))
                {
                    Console.WriteLine();
                    Console.WriteLine("Ошибка22! Товар [" + nazv + "] не может быть оформлен, т.к. были измненеия на складе! Товар удалён из корзины, повторите попытку ещё раз");
                    File.Delete(path11 + @"\Данные\Покупатели\" + login + @"\" + nums2[j]);
                    Thread.Sleep(5000);
                    Pokupatel_menu(login);
                }
            }
        }
    }
}
