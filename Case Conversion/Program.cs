using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case_Conversion
{
    class Program
    {
        static void Main(string[] args)
        {
            string startingString = "This is my starting string.";
            int iterations = 1_000_000;

            //original code
            DateTime start = DateTime.Now;
            for (int x = 0; x < iterations; x++)
            {
                string asMixed = ToMixedCase(startingString);
            }
            Console.WriteLine($"Finished {nameof(ToMixedCase)} with {iterations} iterations in {(DateTime.Now - start).ToString()} seconds.");
            Console.WriteLine($"Output: {ToMixedCase(startingString)}");

            //optimized original code
            start = DateTime.Now;
            for (int x = 0; x < iterations; x++)
            {
                string asMixed = ToMixedCase_Optimized(startingString);
            }
            Console.WriteLine($"Finished {nameof(ToMixedCase_Optimized)} with {iterations} iterations in {(DateTime.Now - start).ToString()} seconds.");
            Console.WriteLine($"Output: {ToMixedCase_Optimized(startingString)}");

            //byte decrement method
            start = DateTime.Now;
            for (int x = 0; x < iterations; x++)
            {
                string asMixed = ToMixedCase_ASCII(startingString);
            }
            Console.WriteLine($"Finished {nameof(ToMixedCase_ASCII)} with {iterations} iterations in {(DateTime.Now - start).ToString()} seconds.");
            Console.WriteLine($"Output: {ToMixedCase_ASCII(startingString)}");
            Console.ReadKey();            
        }

        static string ToMixedCase(string text)
        {
            string textmixedcase = "";
            int i = 0;

            foreach (char ch in text)
            {
                i++;
                if (i % 2 == 0)
                {
                    textmixedcase += ch.ToString().ToUpper();
                }
                else
                {
                    textmixedcase += ch.ToString().ToLower();
                }
            }
            return textmixedcase;
        }

        static string ToMixedCase_Optimized(string text)
        {
            char[] mixedCase = new char[text.Length];
            bool toUpper = false; //first char is upper case
            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];
                mixedCase[i] = toUpper ? char.ToUpper(ch) : char.ToLower(ch);
                toUpper = !toUpper;
            }
            return new string(mixedCase);
        }

        const int lower_a = 97; //ASCII "a"
        const int lower_z = 122; //ASCII "z"
        const int upper_A = 65; //ASCII "A"
        const int upper_Z = 90; //ASCII "Z"
        const int upper_offset = 32; //amount to decrement to turn an upper case to lower in ASCII
        static string ToMixedCase_ASCII(string text)
        {
            var bytes = Encoding.ASCII.GetBytes(text);
            bool toUpper = false; //first char is upper case
            for (int x = 0; x < bytes.Length; x++)
            {
                if (toUpper && bytes[x] <= lower_z && bytes[x] >= lower_a)
                    bytes[x] -= upper_offset;
                else if (!toUpper && bytes[x] <= upper_Z && bytes[x] >= upper_A)
                    bytes[x] += upper_offset;
                toUpper = !toUpper;
            }
            return Encoding.ASCII.GetString(bytes);
        }
    }
}
