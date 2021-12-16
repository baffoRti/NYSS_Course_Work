using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course
{
    public class VigenereCipher
    {
        public static string Alph { get; private set; } = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        static char EncSymbol(char symbol, char key) => Alph[(Alph.Length + Alph.IndexOf(symbol) + Alph.IndexOf(key)) % Alph.Length];
        static char DecSymbol(char symbol, char key) => Alph[(Alph.Length + Alph.IndexOf(symbol) - Alph.IndexOf(key)) % Alph.Length];

        public static string Crypt(string mess, string key, CryptMode mode)
        {
            char[] chars = mess.ToLower().ToCharArray();
            key = key.ToLower();
            int keyIndex = 0;
            for (int i = 0; i < mess.Length; i++)
            {
                if (!Alph.Contains(chars[i]))
                    continue;
                switch (mode)
                {
                    case CryptMode.Encrypt:
                        chars[i] = EncSymbol(chars[i], key[keyIndex++]);
                        break;
                    case CryptMode.Decrypt:
                        chars[i] = DecSymbol(chars[i], key[keyIndex++]);
                        break;
                }
                keyIndex %= key.Length;
            }
            for (int i = 0; i < mess.Length; i++)
            {
                if (char.IsUpper(mess[i]))
                    chars[i] = char.ToUpper(chars[i]);
            }
            return new string(chars);
        }
    }
    public enum CryptMode
    {
        Encrypt,
        Decrypt
    }
}
