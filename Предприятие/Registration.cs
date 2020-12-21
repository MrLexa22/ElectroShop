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
    class Registration
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
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public void Regist()
        {
            string path11 = Directory.GetCurrentDirectory();
        st1: Console.Clear();
            Console.Write("Введите Email: ");
            string email = Console.ReadLine();
            bool chek = IsValidEmail(email);
            if (chek == false)
            {
                Console.WriteLine("Неккоректный Email!");
                Thread.Sleep(2000);
                goto st1;
            }

            DirectoryInfo dir3 = new DirectoryInfo(path11 + @"\Данные\Покупатели");
            foreach (var item in dir3.GetFiles())
            {
                using (BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Покупатели\" + item.Name, FileMode.Open)))
                {
                    string em = "";
                    int h = 1;
                    while (reader.PeekChar() > -1)
                    {
                        string name1 = reader.ReadString();
                        if (h == 2)
                        {
                            if (name1 == email)
                            {
                                Console.WriteLine("Данный EMAIL уже зарегистрирован!");
                                Thread.Sleep(2000);
                                goto st1;
                            }
                        }
                        h++;
                    }
                }
            }

        r1:
            Console.WriteLine();
            Console.Write("Введите логин: ");
            string st_login = Console.ReadLine();
            DirectoryInfo dir2 = new DirectoryInfo(path11 + @"\Данные\Сотрудники\Бухгалтер");
            if (st_login == "" || st_login == " " || st_login == null || Regex.IsMatch(st_login, @"^[а-яА-Я]+$") || st_login.Contains(" ") || st_login.Length < 2)
            {
                Console.WriteLine("Error!");
                Thread.Sleep(2000);
                goto r1;
            }
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
            try
            {
                BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Покупатели\" + st_login + ".login", FileMode.Create));
                writer.Write("Test");
                writer.Close();
                Thread.Sleep(100);
                File.Delete(path11 + @"\Данные\Покупатели\" + st_login + ".login");
            }
            catch
            {
                Console.WriteLine("Не удовлетворяет системным требованиям!");
                goto r1;
            }
            Console.WriteLine();
        r2: Console.Write("Введите пароль: ");
            string password = ReadPasswordLine();
            if (password.Length < 8)
            {
                Console.WriteLine();
                Console.WriteLine("Длина пароля не соответствует требованиям безопасности");
                Thread.Sleep(2000);
                goto r2;
            }
            int g1 = 0, g2 = 0, g3 = 0, g4 = 0, g5 = 0, g6 = 0, g7 = 0 ;
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
            Console.Clear();
            Random rnd = new Random();
            int value = rnd.Next(12345, 54321);

            MailAddress from = new MailAddress("zamyanovskaya.shkola@bk.ru", "Предприятие на C#");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Подтверждение регистрации покупателя на предприятии";
            m.Body = "<h2>Введите следующий код для подтверждения: </h2>"+value;
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
                Console.WriteLine("Произошла непредвиденная ошибка при отправлении письма регистрации. Повторите попытку регистрации позже!");
                Thread.Sleep(5000);
                Console.Clear();
                MenuVibor lol = new MenuVibor();
                lol.Vibor();
            }

            int check = 4;
            Console.WriteLine("Для подтверждения регистрации необходимо ввести код из псиьма, отправленного на указанную почту!");
            mailagain: Console.Write("Введите код: ");
            string kod = Console.ReadLine();
            int kod1 = 0;
            if (check == 1)
            {
                Console.WriteLine("Регистрация отклонена! Неверный код из Email");
                Thread.Sleep(2000);
                MenuVibor lol = new MenuVibor();
                lol.Vibor();
            }
            if (check != 0)
            {
                try
                {
                    kod1 = Convert.ToInt32(kod);
                    if (value != kod1)
                    {
                        check = check - 1;
                        Console.WriteLine("Ошибка, неверный код! Осталось " + check + " попытки");
                        Thread.Sleep(1000);
                        Console.WriteLine();
                        goto mailagain;
                    }
                    if (value == kod1)
                    {
                        Console.Clear();
                        Console.WriteLine("Успешная регистрация!");
                        Console.WriteLine("Данные с регистрационными данными будут направлены Вам на почту");
                        Directory.CreateDirectory(path11 + @"\Данные\Покупатели\" + st_login);
                        BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Покупатели\" + st_login + ".login", FileMode.OpenOrCreate));
                        writer.Write(st_login);
                        writer.Write(email);
                        writer.Write(password);
                        writer.Close();

                        from = new MailAddress("zamyanovskaya.shkola@bk.ru", "Предприятие на C#");
                        to = new MailAddress(email);
                        m = new MailMessage(from, to);
                        m.Subject = "Регистрационные данные";
                        m.Body = "<h2>Логин: " + st_login+ "</h2>"+ "<h2>Пароль: " + password + "</h2>";
                        m.IsBodyHtml = true;
                        smtp = new SmtpClient("smtp.mail.ru", 25);
                        smtp.Credentials = new NetworkCredential("zamyanovskaya.shkola@bk.ru", "MPTmailfrom");
                        smtp.EnableSsl = true;
                        try
                        {
                            smtp.Send(m);
                        }
                        catch
                        {
                            Console.WriteLine("Произошла непредвиденная ошибка при отправлении письма с рег. данными.");
                            Thread.Sleep(5000);
                            Console.Clear();
                            MenuVibor lol1 = new MenuVibor();
                            lol1.Vibor();
                        }
                        Thread.Sleep(5000);
                        Console.Clear();
                        MenuVibor lol = new MenuVibor();
                        lol.Vibor();
                    }
                }
                catch
                {
                    check = check - 1;
                    Console.WriteLine("Ошибка, неверный код! Осталось " + check + " попытки");
                    Thread.Sleep(1000);
                    Console.WriteLine();
                    goto mailagain;
                }
            }

        }
    }
}
