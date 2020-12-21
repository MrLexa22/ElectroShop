using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;

namespace Предприятие
{
    class Prodavec
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

            DirectoryInfo dir = new DirectoryInfo(path11 + @"\Данные\Сотрудники\Кассир-продавец");
            foreach (var item in dir.GetFiles())
            {
                if (login + ".login" == item.Name)
                {
                    Console.Write("Введите пароль: ");
                    string parol = ReadPasswordLine();
                    string log = "", pas = "", em = ""; ;
                    int i = 0;
                    BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\Кассир-продавец\" + login + ".login", FileMode.Open));
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
                        Console.WriteLine("Вы вошли как продавец-кассир!");
                        string fam = "", ima = "", otch = "";
                        path11 = Directory.GetCurrentDirectory();
                        reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\Кассир-продавец\" + login + ".login", FileMode.Open));
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
                        Prodavec_menu(login);
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
            Buhgalter pre = new Buhgalter();
            pre.Authentication(login);
        }
        public void Prodavec_menu(string login)
        {
            Console.Clear();
            string path11 = Directory.GetCurrentDirectory();
            string filial = "";
            int g = 0;
            if (File.Exists(path11 + @"\Данные\Сотрудники\Кассир-продавец\" + login + @"\filial.fil"))
            {
                BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\Кассир-продавец\" + login + @"\filial.fil", FileMode.Open));
                while (reader.PeekChar() > -1)
                {
                    string name = reader.ReadString();
                    if (g == 0)
                        filial = name;
                }
                reader.Close();
                if (File.Exists(path11 + @"\Данные\Сотрудники\" + filial + @".filial"))
                {
                    goto hgk;
                }
                if (!File.Exists(path11 + @"\Данные\Сотрудники\" + filial + @".filial"))
                {
                    Console.WriteLine("Ваш филиал не найден в базе. Авторизация невозможна! Обратитесь в отдел кадров и администрацию!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    MenuVibor lol = new MenuVibor();
                    lol.Vibor();
                }
            }
            if (!File.Exists(path11 + @"\Данные\Сотрудники\Кассир-продавец\" + login + @"\filial.fil"))
            {
                Console.WriteLine("У вас не указан филиал! Авторизация невозможна! Обратитесь в отдел кадров и администрацию!");
                Thread.Sleep(3000);
                Console.Clear();
                MenuVibor lol = new MenuVibor();
                lol.Vibor();
            }    

            hgk:
            string[] menuText = { "Выберите\n", "  1)Просмотр НЕподтверждённых заказов\n", "  2)Подтвердить/отменить заказ\n", "  3)Просмотр подтверждённых заказов\n", "  4)Просмотреть свои сведения о заработной плате\n", "  5)Выход" };
            Authorization auth = new Authorization();
            while (true)
            {

                int chek = ViewMenu(menuText);

                switch (chek)
                {
                    case 1:
                        LookNonPodt(login);
                        break;
                    case 2:
                        PodtZakaz(login);
                        break;
                    case 3:
                        LookPodt(login);
                        break;
                    case 4:
                        LookZP(login);
                        break;
                    case 5:
                        MenuVibor lol = new MenuVibor();
                        lol.Vibor();
                        break;
                }
            }
        }
        public void LookZP(string login)
        {
            Console.Clear();
            string path11 = Directory.GetCurrentDirectory();
            BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\Кассир-продавец\zarplata.zrp", FileMode.Open));
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
            Thread.Sleep(13000);
            Console.Clear();
            Prodavec_menu(login);
        }
        public void LookNonPodt(string login)
        {
            Console.Clear();
            string path11 = Directory.GetCurrentDirectory();

            string filial = "", gorod = "";
            int g = 0;
            BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\Кассир-продавец\" + login + @"\filial.fil", FileMode.Open));
            while (reader.PeekChar() > -1)
            {
                string name = reader.ReadString();
                if (g==0)
                   filial = name;
            }
            reader.Close();

            BinaryReader reader1 = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + filial + @".filial", FileMode.Open));
            g = 0;
            while (reader1.PeekChar() > -1)
            {
                string name = reader1.ReadString();
                if (g==1)
                   gorod = name;
                g++;
            }
            reader1.Close();

            Console.WriteLine("Ваш филиал: " + filial + " (" + gorod + ")");
            Console.WriteLine();
            Console.WriteLine("Список заказов вашего филиала: ");

            if (!Directory.Exists(path11 + @"\Данные\Заказы\Неподтверждённые\" + filial))
            {
                Console.WriteLine("Заказов в вашем филиале нет!");
                Thread.Sleep(2000);
                Console.Clear();
                Prodavec_menu(login);
            }

            string[] nums2 = new string[10000];
            int[] checker3 = new int[1000];
            int j = 1;
            DirectoryInfo dir = new DirectoryInfo(path11 + @"\Данные\Заказы\Неподтверждённые\"+filial);
            foreach (var item in dir.GetDirectories())
            {
                nums2[j] = item.Name;
                Console.WriteLine(j + ") " + item.Name);
                checker3[j] = j;
                j++;
            }
            if (j==1)
            {
                Console.WriteLine("Заказов нет!");
                Thread.Sleep(2000);
                Console.Clear();
                Prodavec_menu(login);
            }

            st1: Console.Write("Введите номер: ");
            int j2 = 0;
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
            bool flag1 = false;
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
            Console.Clear();
            Console.WriteLine("Вы выбрали: " + nums2[j2]);
            Console.WriteLine();

            dir = new DirectoryInfo(path11 + @"\Данные\Заказы\Неподтверждённые\" + filial+@"\"+nums2[j2]);
            g = 0;
            string email = "";
            foreach (var item in dir.GetFiles())
            {
                string jkl = item.Name.Substring(item.Name.Length - 6);
                if (jkl == ".login")
                {
                    reader = new BinaryReader(File.Open(path11 + @"\Данные\Заказы\Неподтверждённые\" + filial + @"\" + nums2[j2] + @"\"+item.Name, FileMode.Open));
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (g == 0)
                            email = name;
                        if (g == 1)
                            email = name;
                        g++;
                    }
                    reader.Close();
                }
            }
            Console.WriteLine("Email покупателя: " + email);

            Console.WriteLine();
            double itog_all = 0;
            string nazv_porkup = "", kolvo_pokup = "", cena1_pokup = "", cenaitog_pokup = "", categ_pokup = "";
            j = 1;
            foreach (var item in dir.GetFiles())
            {
                g = 0;
                string jkl = item.Name.Substring(item.Name.Length - 5);
                if (jkl == ".cart")
                {
                    reader = new BinaryReader(File.Open(path11 + @"\Данные\Заказы\Неподтверждённые\" + filial + @"\" + nums2[j2] + @"\" + item.Name, FileMode.Open));
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (g == 0)
                            nazv_porkup = name;
                        if (g == 1)
                            kolvo_pokup = name;
                        if (g == 2)
                            cena1_pokup = name;
                        if (g == 3)
                        {
                            cenaitog_pokup = name;
                            itog_all = itog_all + Convert.ToDouble(cenaitog_pokup);
                        }
                        if (g == 4)
                            categ_pokup = name;
                        g++;
                    }
                    Console.WriteLine(j + ") " + nazv_porkup + " (кол-во: "+ kolvo_pokup+") (цена за 1 шт.: "+ cena1_pokup+") (итог. цена: "+ cenaitog_pokup+")");
                    j++;
                    reader.Close();
                }
            }
            Console.WriteLine();
            Console.WriteLine("Дата и время создания заказа: " + System.IO.File.GetCreationTime(path11 + @"\Данные\Заказы\Неподтверждённые\" + filial + @"\" + nums2[j2] + @"\" + nazv_porkup+".cart").ToString());
            Console.WriteLine();
            Console.WriteLine("Общая сумма заказа: " + itog_all);
            Console.WriteLine();
            Console.WriteLine("Нажмите Enter для возврата в меню");
            Console.ReadKey();
        }

        public void PodtZakaz(string login)
        {
            Console.Clear();
            string path11 = Directory.GetCurrentDirectory();

            string filial = "", gorod = "";
            int g = 0;
            BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\Кассир-продавец\" + login + @"\filial.fil", FileMode.Open));
            while (reader.PeekChar() > -1)
            {
                string name = reader.ReadString();
                if (g == 0)
                    filial = name;
            }
            reader.Close();

            BinaryReader reader1 = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + filial + @".filial", FileMode.Open));
            g = 0;
            while (reader1.PeekChar() > -1)
            {
                string name = reader1.ReadString();
                if (g == 1)
                    gorod = name;
                g++;
            }
            reader1.Close();

            Console.WriteLine("Ваш филиал: " + filial + " (" + gorod + ")");
            Console.WriteLine();
            Console.WriteLine("Список заказов вашего филиала: ");

            if (!Directory.Exists(path11 + @"\Данные\Заказы\Неподтверждённые\" + filial))
            {
                Console.WriteLine("Заказов в вашем филиале нет!");
                Thread.Sleep(2000);
                Console.Clear();
                Prodavec_menu(login);
            }

            string[] nums2 = new string[10000];
            int[] checker3 = new int[1000];
            int j = 1;
            DirectoryInfo dir = new DirectoryInfo(path11 + @"\Данные\Заказы\Неподтверждённые\" + filial);
            foreach (var item in dir.GetDirectories())
            {
                nums2[j] = item.Name;
                Console.WriteLine(j + ") " + item.Name);
                checker3[j] = j;
                j++;
            }
            if (j == 1)
            {
                Console.WriteLine("Заказов нет!");
                Thread.Sleep(2000);
                Console.Clear();
                Prodavec_menu(login);
            }

            Console.WriteLine();
            Console.WriteLine("Продолжить - Enter");
            Console.WriteLine("Возврат в меню - F3");
            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.F3)
                {
                    Prodavec_menu(login);
                }
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    goto st1;
                }
            }

        st1: Console.Write("Введите номер: ");
            int j2 = 0;
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
            bool flag1 = false;
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
            Console.Clear();
            Console.WriteLine("Вы выбрали: " + nums2[j2]);
            Console.WriteLine();

            dir = new DirectoryInfo(path11 + @"\Данные\Заказы\Неподтверждённые\" + filial + @"\" + nums2[j2]);
            g = 0;
            string email = "";
            foreach (var item in dir.GetFiles())
            {
                string jkl = item.Name.Substring(item.Name.Length - 6);
                if (jkl == ".login")
                {
                    reader = new BinaryReader(File.Open(path11 + @"\Данные\Заказы\Неподтверждённые\" + filial + @"\" + nums2[j2] + @"\" + item.Name, FileMode.Open));
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (g == 0)
                            email = name;
                        if (g == 1)
                            email = name;
                        g++;
                    }
                    reader.Close();
                }
            }
            Console.WriteLine("Email покупателя: " + email);

            Console.WriteLine();
            double itog_all = 0;
            string[] korzina = new string[10000];
            int kl = 0;
            string nazv_porkup = "", kolvo_pokup = "", cena1_pokup = "", cenaitog_pokup = "", categ_pokup = "";
            j = 1;
            foreach (var item in dir.GetFiles())
            {
                g = 0;
                string jkl = item.Name.Substring(item.Name.Length - 5);
                if (jkl == ".cart")
                {
                    reader = new BinaryReader(File.Open(path11 + @"\Данные\Заказы\Неподтверждённые\" + filial + @"\" + nums2[j2] + @"\" + item.Name, FileMode.Open));
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (g == 0)
                            nazv_porkup = name;
                        if (g == 1)
                            kolvo_pokup = name;
                        if (g == 2)
                            cena1_pokup = name;
                        if (g == 3)
                        {
                            cenaitog_pokup = name;
                            itog_all = itog_all + Convert.ToDouble(cenaitog_pokup);
                        }
                        if (g == 4)
                            categ_pokup = name;
                        g++;
                    }
                    reader.Close();
                    Console.WriteLine(j + ") " + nazv_porkup + " (кол-во: " + kolvo_pokup + ") (цена за 1 шт.: " + cena1_pokup + ") (итог. цена: " + cenaitog_pokup + ")");
                    korzina[kl] = j + ") " + nazv_porkup + "(кол - во: " + kolvo_pokup + ")(цена за 1 шт.: " + cena1_pokup + ")(итог.цена: " + cenaitog_pokup + ")";
                    kl++;
                    if (File.Exists(path11 + @"\Данные\Товары\" + categ_pokup + @"\" + nazv_porkup + ".tovar"))
                    {
                        string nazvanie = "", kolvo1 = "", chena1 = "", data = "";
                        reader = new BinaryReader(File.Open(path11 + @"\Данные\Товары\" + categ_pokup + @"\" + nazv_porkup + ".tovar", FileMode.Open));
                        while (reader.PeekChar() > -1)
                        {
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
                        }
                        Console.WriteLine("   Наличие на складе: ещё есть (" + kolvo1 + " штук) (цена на складе: " + chena1 + ")");
                        reader.Close();
                    }
                    if (!File.Exists(path11 + @"\Данные\Товары\" + categ_pokup + @"\" + nazv_porkup + ".tovar"))
                    {
                        Console.WriteLine("   Наличие на складе: полностью зарезервировано покупателям");
                    }
                    j++;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Дата и время создания заказа: " + System.IO.File.GetCreationTime(path11 + @"\Данные\Заказы\Неподтверждённые\" + filial + @"\" + nums2[j2] + @"\" + nazv_porkup + ".cart").ToString());
            Console.WriteLine();
            Console.WriteLine("Общая сумма заказа: " + itog_all);
            Console.WriteLine();
            Console.WriteLine();
        viba:
            string vih = "";
            Console.WriteLine("Для отмены - F3");
            Console.WriteLine("Продолжить - Enter");
            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.F3)
                {
                    Prodavec_menu(login);
                }
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    goto jfdg;
                }
            }
            jfdg:
            Console.WriteLine("Выберите: ");
            Console.WriteLine("1. Подтвердить заказ");
            Console.WriteLine("2. Отменить заказ");
            Console.Write("Выбор: ");
            vih = Console.ReadLine();

            if (vih == "1")
            {
                if (!Directory.Exists(path11 + @"\Данные\Заказы\Подтверждённые\" + filial))
                {
                    Directory.CreateDirectory(path11 + @"\Данные\Заказы\Подтверждённые\" + filial);
                }
                Directory.Move(path11 + @"\Данные\Заказы\Неподтверждённые\" + filial + @"\" + nums2[j2], path11 + @"\Данные\Заказы\Подтверждённые\" + filial + @"\" + nums2[j2]);

                if (!Directory.Exists(path11 + @"\Данные\Бухгалтерия\Заказы\"))
                {
                    Directory.CreateDirectory(path11 + @"\Данные\Бухгалтерия\Заказы\");
                }
                BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Бухгалтерия\Заказы\" + nums2[j2] + ".zakaz", FileMode.OpenOrCreate));
                writer.Write(Convert.ToString(itog_all));
                DateTime dt = DateTime.Now;
                string curDate = dt.ToShortDateString();
                writer.Write(curDate);
                writer.Close();
                Console.Clear();
                Console.WriteLine("Заказ подтверждён!");

                double budzhet_firma = 0;
                reader = new BinaryReader(File.Open(path11 + @"\Данные\Бухгалтерия\budzhet.dengi", FileMode.Open));
                while (reader.PeekChar() > -1)
                {
                    int h = 1;
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (h == 1)
                            budzhet_firma = Convert.ToDouble(name);
                        h++;
                    }
                }
                reader.Close();

                budzhet_firma = budzhet_firma + itog_all;
                writer = new BinaryWriter(File.Open(path11 + @"\Данные\Бухгалтерия\budzhet.dengi", FileMode.OpenOrCreate));
                writer.Write(Convert.ToString(budzhet_firma));
                writer.Close();

                MailAddress from = new MailAddress("zamyanovskaya.shkola@bk.ru", "Предприятие на C#");
                MailAddress to = new MailAddress(email);
                MailMessage m = new MailMessage(from, to);
                m.Subject = nums2[j2]+" подтверждён";
                string text_filial = "Вы можете забрать заказ из филиала " + filial+ "<br />";
                string text_itog = "Сумма заказа: " + itog_all+ "<br />";
                string text_pisma = "<h2>ЗАКАЗ ПОДТВЕРЖДЁН!</h2>" + text_filial + text_itog+ "<br />";
                for (int i=0; i<kl;i++)
                {
                    text_pisma = text_pisma + korzina[i] + "<br />";
                }
                m.Body = text_pisma;
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 25);
                smtp.Credentials = new NetworkCredential("zamyanovskaya.shkola@bk.ru", "MPTmailfrom");
                smtp.EnableSsl = true;
                try
                {
                    smtp.Send(m);
                }
                catch
                {
                    Console.WriteLine("Произошла непредвиденная ошибка при отправлении письма подтверждения.");
                    Thread.Sleep(5000);
                    Console.Clear();
                    Prodavec_menu(login);
                }

                Thread.Sleep(2000);
                Prodavec_menu(login);
            }
            
            else if (vih == "2")
            {
                Console.Clear();
                Console.WriteLine("Заказ отменён!");
                dir = new DirectoryInfo(path11 + @"\Данные\Заказы\Неподтверждённые\" + filial+@"\"+ nums2[j2]);
                foreach (var item in dir.GetFiles())
                {
                    g = 0;
                    string jkl = item.Name.Substring(item.Name.Length - 5);
                    if (jkl == ".cart")
                    {
                        reader = new BinaryReader(File.Open(path11 + @"\Данные\Заказы\Неподтверждённые\" + filial + @"\" + nums2[j2] + @"\" + item.Name, FileMode.Open));
                        while (reader.PeekChar() > -1)
                        {
                            string name = reader.ReadString();
                            if (g == 0)
                                nazv_porkup = name;
                            if (g == 1)
                                kolvo_pokup = name;
                            if (g == 2)
                                cena1_pokup = name;
                            if (g == 3)
                            {
                                cenaitog_pokup = name;
                                itog_all = itog_all + Convert.ToDouble(cenaitog_pokup);
                            }
                            if (g == 4)
                                categ_pokup = name;
                            g++;
                        }
                        reader.Close();
                        if (!File.Exists(path11 + @"\Данные\Товары\" + categ_pokup + @"\" + nazv_porkup + ".tovar"))
                        {
                            Console.WriteLine("Товар [" + nazv_porkup + "] отсутствует в базе данных. Для повторной записи товара обратитесь к кладовщику!");
                        }
                        else if (File.Exists(path11 + @"\Данные\Товары\" + categ_pokup + @"\" + nazv_porkup + ".tovar"))
                        {
                            string nazvanie = "", kolvo1 = "", chena1 = "", data = "";
                            reader = new BinaryReader(File.Open(path11 + @"\Данные\Товары\" + categ_pokup + @"\" + nazv_porkup + ".tovar", FileMode.Open));
                            while (reader.PeekChar() > -1)
                            {
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
                            }
                            reader.Close();
                            int kolvo_to = 0;
                            kolvo_to = Convert.ToInt32(kolvo1) + Convert.ToInt32(kolvo_pokup);

                            File.Delete(path11 + @"\Данные\Товары\" + categ_pokup + @"\" + nazv_porkup + ".tovar");

                            BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Товары\" + categ_pokup + @"\" + nazv_porkup + ".tovar", FileMode.OpenOrCreate));
                            writer.Write(nazvanie);
                            writer.Write(Convert.ToString(kolvo_to));
                            writer.Write(chena1);
                            writer.Write(data);
                            writer.Close();
                        }
                    }
                }

                Directory.Delete(path11 + @"\Данные\Заказы\Неподтверждённые\" + filial + @"\" + nums2[j2],true);

                MailAddress from = new MailAddress("zamyanovskaya.shkola@bk.ru", "Предприятие на C#");
                MailAddress to = new MailAddress(email);
                MailMessage m = new MailMessage(from, to);
                m.Subject = nums2[j2] + " ОТМЕНЁН";
                string text_itog = "Сумма заказа: " + itog_all + "<br />";
                string text_pisma = "<h2>ЗАКАЗ ОТМЕНЁН!</h2>" + text_itog + "<br />";
                for (int i = 0; i < kl; i++)
                {
                    text_pisma = text_pisma + korzina[i] + "<br />";
                }
                m.Body = text_pisma;
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 25);
                smtp.Credentials = new NetworkCredential("zamyanovskaya.shkola@bk.ru", "MPTmailfrom");
                smtp.EnableSsl = true;
                try
                {
                    smtp.Send(m);
                }
                catch
                {
                    Console.WriteLine("Произошла непредвиденная ошибка при отправлении письма подтверждения.");
                    Thread.Sleep(5000);
                    Console.Clear();
                    Prodavec_menu(login);
                }
                Console.WriteLine("Письмо на почту покупателя отправлено!");
                Thread.Sleep(5000);
                Prodavec_menu(login);
            }

            else if (vih != "1" || vih != "2")
            {
                Console.WriteLine("Error!");
                Console.WriteLine();
                Thread.Sleep(2000);
                goto viba;
            }
        }
        public void LookPodt(string login)
        {
            Console.Clear();
            string path11 = Directory.GetCurrentDirectory();

            string filial = "", gorod = "";
            int g = 0;
            BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\Кассир-продавец\" + login + @"\filial.fil", FileMode.Open));
            while (reader.PeekChar() > -1)
            {
                string name = reader.ReadString();
                if (g == 0)
                    filial = name;
            }
            reader.Close();

            BinaryReader reader1 = new BinaryReader(File.Open(path11 + @"\Данные\Сотрудники\" + filial + @".filial", FileMode.Open));
            g = 0;
            while (reader1.PeekChar() > -1)
            {
                string name = reader1.ReadString();
                if (g == 1)
                    gorod = name;
                g++;
            }
            reader1.Close();

            Console.WriteLine("Ваш филиал: " + filial + " (" + gorod + ")");
            Console.WriteLine();
            Console.WriteLine("Список заказов вашего филиала: ");

            if (!Directory.Exists(path11 + @"\Данные\Заказы\Подтверждённые\" + filial))
            {
                Console.WriteLine("Заказов в вашем филиале нет!");
                Thread.Sleep(2000);
                Console.Clear();
                Prodavec_menu(login);
            }

            string[] nums2 = new string[10000];
            int[] checker3 = new int[1000];
            int j = 1;
            DirectoryInfo dir = new DirectoryInfo(path11 + @"\Данные\Заказы\Подтверждённые\" + filial);
            foreach (var item in dir.GetDirectories())
            {
                nums2[j] = item.Name;
                Console.WriteLine(j + ") " + item.Name);
                checker3[j] = j;
                j++;
            }
            if (j == 1)
            {
                Console.WriteLine("Заказов нет!");
                Thread.Sleep(2000);
                Console.Clear();
                Prodavec_menu(login);
            }

        st1: Console.Write("Введите номер: ");
            int j2 = 0;
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
            bool flag1 = false;
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
            Console.Clear();
            Console.WriteLine("Вы выбрали: " + nums2[j2]);
            Console.WriteLine();

            dir = new DirectoryInfo(path11 + @"\Данные\Заказы\Подтверждённые\" + filial + @"\" + nums2[j2]);
            g = 0;
            string email = "";
            foreach (var item in dir.GetFiles())
            {
                string jkl = item.Name.Substring(item.Name.Length - 6);
                if (jkl == ".login")
                {
                    reader = new BinaryReader(File.Open(path11 + @"\Данные\Заказы\Подтверждённые\" + filial + @"\" + nums2[j2] + @"\" + item.Name, FileMode.Open));
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (g == 0)
                            email = name;
                        if (g == 1)
                            email = name;
                        g++;
                    }
                    reader.Close();
                }
            }
            Console.WriteLine("Email покупателя: " + email);

            Console.WriteLine();
            double itog_all = 0;
            string nazv_porkup = "", kolvo_pokup = "", cena1_pokup = "", cenaitog_pokup = "", categ_pokup = "";
            j = 1;
            foreach (var item in dir.GetFiles())
            {
                g = 0;
                string jkl = item.Name.Substring(item.Name.Length - 5);
                if (jkl == ".cart")
                {
                    reader = new BinaryReader(File.Open(path11 + @"\Данные\Заказы\Подтверждённые\" + filial + @"\" + nums2[j2] + @"\" + item.Name, FileMode.Open));
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (g == 0)
                            nazv_porkup = name;
                        if (g == 1)
                            kolvo_pokup = name;
                        if (g == 2)
                            cena1_pokup = name;
                        if (g == 3)
                        {
                            cenaitog_pokup = name;
                            itog_all = itog_all + Convert.ToDouble(cenaitog_pokup);
                        }
                        if (g == 4)
                            categ_pokup = name;
                        g++;
                    }
                    Console.WriteLine(j + ") " + nazv_porkup + " (кол-во: " + kolvo_pokup + ") (цена за 1 шт.: " + cena1_pokup + ") (итог. цена: " + cenaitog_pokup + ")");
                    j++;
                    reader.Close();
                }
            } 
            Console.WriteLine();
            Console.WriteLine("Дата и время создания заказа: " + System.IO.File.GetCreationTime(path11 + @"\Данные\Заказы\Подтверждённые\" + filial + @"\" + nums2[j2] + @"\" + nazv_porkup + ".cart").ToString());
            Console.WriteLine();
            Console.WriteLine("Общая сумма заказа: " + itog_all);
            Console.WriteLine();
            Console.WriteLine("Нажмите Enter для возврата в меню");
            Console.ReadKey();
        }
    }
}
