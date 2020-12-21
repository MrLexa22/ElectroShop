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
    class MenuVibor
    {
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
        public void Vibor()
        {
            string path11 = Directory.GetCurrentDirectory();
        st: Console.Clear();

            if (!Directory.Exists(path11 + @"\Данные"))
            {
                Directory.CreateDirectory(path11 + @"\Данные");
                Directory.CreateDirectory(path11 + @"\Данные\Администратор");
                Directory.CreateDirectory(path11 + @"\Данные\Бухгалтерия");
                Directory.CreateDirectory(path11 + @"\Данные\Заказы");
                Directory.CreateDirectory(path11 + @"\Данные\Заказы\Подтверждённые");
                Directory.CreateDirectory(path11 + @"\Данные\Заказы\Неподтверждённые");
                Directory.CreateDirectory(path11 + @"\Данные\Покупатели");
                Directory.CreateDirectory(path11 + @"\Данные\Сотрудники");
                Directory.CreateDirectory(path11 + @"\Данные\Сотрудники\Бухгалтер");
                Directory.CreateDirectory(path11 + @"\Данные\Сотрудники\Кассир-продавец");
                Directory.CreateDirectory(path11 + @"\Данные\Сотрудники\Кладовщик");
                Directory.CreateDirectory(path11 + @"\Данные\Сотрудники\Отдел кадров");
                Directory.CreateDirectory(path11 + @"\Данные\Товары");
                Directory.CreateDirectory(path11 + @"\Данные\Бухгалтерия\Операции по счёту");
                Directory.CreateDirectory(path11 + @"\Данные\Бухгалтерия\Операции по счёту\Переводы");
                Directory.CreateDirectory(path11 + @"\Данные\Бухгалтерия\Операции по счёту\Пополнения");
                Directory.CreateDirectory(path11 + @"\Данные\Бухгалтерия\Зарплаты");
                Directory.CreateDirectory(path11 + @"\Данные\Бухгалтерия\Заказы");
                string dolzh = "", zp="";
                for (int i=0; i<=3; i++)
                {   
                    if (i == 0)
                    {
                        dolzh = "Бухгалтер";
                        zp = "90000";
                        if (!File.Exists(path11 + @"\Данные\Сотрудники\" + dolzh + @"\zarplata.zrp"))
                        {
                            BinaryWriter reader22 = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\zarplata.zrp", FileMode.OpenOrCreate));
                            reader22.Write(zp);
                            reader22.Close();
                        }
                    }
                    if (i == 1)
                    {
                        dolzh = "Кассир-продавец";
                        zp = "76000";
                        if (!File.Exists(path11 + @"\Данные\Сотрудники\" + dolzh + @"\zarplata.zrp"))
                        {
                            BinaryWriter reader22 = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\zarplata.zrp", FileMode.OpenOrCreate));
                            reader22.Write(zp);
                            reader22.Close();
                        }

                    }
                    if (i == 2)
                    {
                        dolzh = "Кладовщик";
                        zp = "85000";
                        if (!File.Exists(path11 + @"\Данные\Сотрудники\" + dolzh + @"\zarplata.zrp"))
                        {
                            BinaryWriter reader22 = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\zarplata.zrp", FileMode.OpenOrCreate));
                            reader22.Write(zp);
                            reader22.Close();
                        }
                    }
                    if (i == 3)
                    {
                        dolzh = "Отдел кадров";
                        zp = "80000";
                        if (!File.Exists(path11 + @"\Данные\Сотрудники\" + dolzh + @"\zarplata.zrp"))
                        {
                            BinaryWriter reader22 = new BinaryWriter(File.Open(path11 + @"\Данные\Сотрудники\" + dolzh + @"\zarplata.zrp", FileMode.OpenOrCreate));
                            reader22.Write(zp);
                            reader22.Close();
                        }
                    }
                    if (!File.Exists(path11 + @"\Данные\Сотрудники\Администратор\zarplata.zrp"))
                    {
                        zp = "100000";
                        if (!File.Exists(path11 + @"\Данные\Сотрудники\Администратор\zarplata.zrp"))
                        {
                            BinaryWriter reader22 = new BinaryWriter(File.Open(path11 + @"\Данные\Администратор\zarplata.zrp", FileMode.OpenOrCreate));
                            reader22.Write(zp);
                            reader22.Close();
                        }
                    }
                    if (!File.Exists(path11 + @"\Данные\Бухгалтерия\budzhet.dengi"))
                    {
                        string budzhet = "500000";
                        BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Бухгалтерия\budzhet.dengi", FileMode.OpenOrCreate));
                        writer.Write(budzhet);
                        writer.Close();
                    }
                }
            }


            if (!File.Exists(path11 + @"\Данные\Администратор\password.passw"))
            {
                Console.WriteLine("Внимание! Администратору не задан пароль!");
                Console.Write("Введите пароль: ");

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

                if (password.Length < 8)
                {
                    Console.WriteLine();
                    Console.WriteLine("Длина пароля не соответствует требованиям безопасности");
                    Thread.Sleep(2000);
                    goto st;
                }
                int g1 = 0, g2 = 0, g3 = 0, g4 = 0, g5 = 0, g6 = 0, g7=0;
                for (int i = 0; i < password.Length; i++)
                {
                    if ((password[i] >= 'A') && (password[i] <= 'Z'))
                        g1++;
                    if (password[i] == '!' || password[i] == '@' || password[i] == '#' || password[i] == '$' || password[i] == '%' || password[i] == '&' || password[i] == '*')
                        g2++;
                    if (password[i] >= '0' && password[i] <= '9')
                        g3++;
                    if (((password[i] >= 'А') && (password[i] <= 'Я')) || ((password[i] >= 'а') && (password[i] <= 'я')) || password[i] == 'ё')
                        g4++;
                    if (password == null || password == "")
                        g5++;
                    if (password.Contains(" "))
                        g6++;
                    if (password[i] >= 'A' && password[i++] <= 'Z')
                        g7++;
                }
                if (g1 < 3)
                {
                    Console.WriteLine();
                    Console.WriteLine("Минимум 3 заглавных буквы!");
                    Thread.Sleep(2000);
                    goto st;
                }
                if (g2 < 2)
                {
                    Console.WriteLine();
                    Console.WriteLine("Минимум 2 спец. символа!");
                    Thread.Sleep(2000);
                    goto st;
                }
                if (g3 < 3)
                {
                    Console.WriteLine();
                    Console.WriteLine("Минимум 3 цифры!");
                    Thread.Sleep(2000);
                    goto st;
                }
                if (g4>0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Никаких русских символов!");
                    Thread.Sleep(2000);
                    goto st;
                }
                if (g5 > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Вы ввели пустой пароль");
                    Thread.Sleep(2000);
                    goto st;
                }
                if (g6 > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Пароль не должен содержать пробелов");
                    Thread.Sleep(2000);
                    goto st;
                }
                if (g7 < 3)
                {
                    Console.WriteLine();
                    Console.WriteLine("Минимум 3 заглавных буквы подряд!");
                    Thread.Sleep(2000);
                    goto st;
                }
                BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Администратор\password.passw", FileMode.OpenOrCreate));
                writer.Write(password);
                writer.Close();

                Console.WriteLine();
                Console.WriteLine("Вы успешно записали пароль Администратора!");
                Console.WriteLine();
                Console.WriteLine("Логин: admin");
                Console.WriteLine("Пароль: " + password);
                Thread.Sleep(5000);
            }

                string[] menuText = {"Menu\n", "  1)Авторизация\n", "  2)Выход\n", "  3)Регистрация в магазине\n" };
                Authorization auth = new Authorization();
            Registration reg = new Registration();
            while (true)
                {

                    int chek = ViewMenu(menuText);

                    switch (chek)
                    {
                        case 1:
                            auth.Lolkin();
                            break;
                        case 2:
                            Environment.Exit(0);
                            break;
                        case 3:
                           reg.Regist();
                           break;
                }
                }
            }
        }
    }
