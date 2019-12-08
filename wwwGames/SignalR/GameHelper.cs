using System;
using System.Linq;

namespace wwwGames.SignalR
{
    public class GameHelper
    {
        static public string getNewKey()
        {
            Random rnd = new Random();
            int digit;
            string result = "";
            while (result.Count() != 4)
            {
                while (true)
                {
                    digit = rnd.Next(9);
                    if (!result.Contains(digit.ToString()))
                    {
                        result += digit;
                        break;
                    }
                }
            }
            return result;
        }

        static public int[] checkValue(string value, string key)
        {
            int[] result = { 0, 0 };
            foreach (var v in value)
            {
                if (key.Contains(v))
                {
                    result[1]++;
                }
            }
            for(int i = 0; i < 4; i++)
            {
                if (value[i] == key[i])
                {
                    result[0]++;
                    result[1]--;
                }
            }
            return result;
        }
    }
}
