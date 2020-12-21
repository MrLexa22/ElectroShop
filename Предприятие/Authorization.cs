using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Предприятие
{
    class Authorization
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

        public string login;
        public void Lolkin()
        {
            string path11 = Directory.GetCurrentDirectory();
        again: Console.Clear();
            Console.Write("Введите логин: ");
            login = Console.ReadLine();

            if (login != "admin")
            {
                Cadri stud = new Cadri();
                stud.Authentication(login);
            }

            if (login == "admin")
            {
                string name = "";
                BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Администратор\password.passw", FileMode.Open));
                while (reader.PeekChar() > -1)
                {
                    name = reader.ReadString();
                }
                Console.Write("Введите пароль: ");
                string parol = ReadPasswordLine();

                if (parol == name)
                {
                    Console.WriteLine();
                    Console.WriteLine("Вы вошли как Администратор!");
                    reader.Close();
                    Admin adm = new Admin();
                    adm.Admin_panel();
                }
                if (parol != name)
                {
                    Console.WriteLine();
                    Console.WriteLine("Вы ввели неверный пароль Администратора, повторите ввод");
                    reader.Close();
                    Thread.Sleep(2000);
                    goto again;
                }
            }
        }
    }
}
