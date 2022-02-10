using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Text_File_Encryptor
{
    class Program
    {
        public static Encrypt crypt = new Encrypt();

        static void Main(string[] args)
        {
            if (args.Length < 1 || args.Length > 1)
            {
                Console.WriteLine("Drop a .txt file on the executable to use");
                Console.ReadKey();
                Environment.Exit(0);
            }
            if (args[0].ElementAt(args[0].Length - 1).ToString().ToLower() != "t" || args[0].ElementAt(args[0].Length - 2).ToString().ToLower() != "x" || args[0].ElementAt(args[0].Length - 3).ToString().ToLower() != "t" || args[0].ElementAt(args[0].Length - 4).ToString().ToLower() != ".")
            {
                Console.WriteLine("Only accepts .txt files");
                Console.ReadKey();
                Environment.Exit(0);
            }
            if (File.ReadAllText(args[0]) == "")
            {
                Console.WriteLine("File is empty");
                Console.ReadKey();
                Environment.Exit(0);
            }

            if (File.ReadAllLines(args[0])[0] == "ETF")
            {
            A:
                Console.WriteLine("Enter password for text file");
                string password = Console.ReadLine();
                if (password.Length < 8 || password.Length > 30)
                {
                    Console.Clear();
                    Console.WriteLine("Password length must be 8 characters long");
                    Console.ReadKey();
                    Console.Clear();
                    goto A;
                }
                Console.Clear();
            B:
                Console.WriteLine("Enter secret for text file");
                string secret = Console.ReadLine();
                if (secret.Length != password.Length)
                {
                    Console.Clear();
                    Console.WriteLine("Secret length must be 8 characters long");
                    Console.ReadKey();
                    Console.Clear();
                    goto B;
                }
                string text = "";
                string[] TextInFile = File.ReadAllLines(args[0]);
                for (int i = 1; i < TextInFile.Length; i++)
                {
                    text += TextInFile[i];
                }
                string DecryptedText = crypt.DecryptText(text, password, secret);
                string NameForFile = "";
                for (int i = 0; i < Path.GetFileName(args[0]).Length - 4; i++)
                {
                    NameForFile += Path.GetFileName(args[0]).ElementAt(i);
                }
                if (!File.Exists(Directory.GetCurrentDirectory() + "\\" + NameForFile + " (Decrypted).txt"))
                {
                    File.Create(Directory.GetCurrentDirectory() + "\\" + NameForFile + " (Decrypted).txt").Close();
                    File.WriteAllText(Directory.GetCurrentDirectory() + "\\" + NameForFile + " (Decrypted).txt", DecryptedText);
                }
                Environment.Exit(0);
            }
            else
            {
            A:
                Console.WriteLine("Enter password for file (8 characters)");

                string password = Console.ReadLine();
                if (password.Length < 8 || password.Length > 8)
                {
                    Console.Clear();
                    Console.WriteLine("Password length must be at least 8 characters and at most 30 characters");
                    Console.ReadKey();
                    Console.Clear();
                    goto A;
                }
                Console.Clear();
                Console.WriteLine("Are you sure you want \"{0}\" as your password? (Y/N)", password);
                if (Console.ReadKey().Key.ToString() != "Y")
                {
                    Console.Clear();
                    goto A;
                }
                Console.Clear();
            B:
                Console.WriteLine("Enter your secret for your encryption (8 characters long)");
                string secret = Console.ReadLine();
                if (secret.Length != password.Length)
                {
                    Console.Clear();
                    Console.WriteLine("Secret length must be 8 characters long");
                    Console.ReadKey();
                    Console.Clear();
                    goto B;
                }
                Console.Clear();
                Console.WriteLine("Are you sure you want \"{0}\" as your secret? (Y/N)", secret);
                if (Console.ReadKey().Key.ToString() != "Y")
                {
                    Console.Clear();
                    goto B;
                }
                Console.Clear();
                
                string EncryptedFileText = crypt.EncryptText(File.ReadAllText(args[0]), password, secret);
                string NameForFile = "";
                for (int i = 0; i < Path.GetFileName(args[0]).Length - 4; i++)
                {
                    NameForFile += Path.GetFileName(args[0]).ElementAt(i);
                }
                if (!File.Exists(Directory.GetCurrentDirectory() + "\\" + NameForFile + " (Encrypted).txt"))
                {
                    File.Create(Directory.GetCurrentDirectory() + "\\" + NameForFile + " (Encrypted).txt").Close();
                    File.WriteAllText(Directory.GetCurrentDirectory() + "\\" + NameForFile + " (Encrypted).txt", "ETF\n" + EncryptedFileText);
                }
                Environment.Exit(0);
            }
        }
    }
}