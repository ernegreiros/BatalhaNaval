using System;
using System.Text;

namespace Battleship
{
    public static class Helper
    {
        public static string KeyGenerator()
        {
            return ConvertToKey(Guid.NewGuid());
        }

        public static string ConvertToKey(Guid guid)
        {
            var guidArray = guid.ToByteArray();
            var letters = new int[8];
            var bytes = new byte[8];

            for (var i = 0; i < guidArray.Length; i++)
            {
                var index = i / 2;
                letters[index] += guidArray[i]; // byte + byte can be a value greater than byte
            }

            for (int i = 0; i < letters.Length; i++)
            {
                bytes[i] = ToLetter(letters[i]);
            }

            return Encoding.ASCII.GetString(bytes);
        }

        private static byte ToLetter(int value)
        {
            var x = byte.Parse(((value % 26) + 'A').ToString());
            return x;
        }
    }
}
