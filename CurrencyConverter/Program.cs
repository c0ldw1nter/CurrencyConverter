using System;
using System.Collections.Generic;
using System.IO;

namespace CurrencyConverter
{
    public class Program
    {
        static Dictionary<string, decimal> currencyValues;
        static void Main(string[] args)
        {
            currencyValues = ReadCurrencyValues("valueData.csv");
            if(currencyValues==null)
            {
                Console.WriteLine("Error reading values file.");
                Console.ReadLine();
                Environment.Exit(0);
            }

            while (true)
            {
            RetryAmount:
                Console.WriteLine("Enter the amount to convert:");
                if (!decimal.TryParse(Console.ReadLine(), out decimal inputAmount))
                {
                    Console.WriteLine("Invalid input.");
                    goto RetryAmount;
                }

            RetryStartCurrency:
                Console.WriteLine("Enter the currency to convert from:");
                string startCurrency = Console.ReadLine().ToUpper();

                if (!currencyValues.ContainsKey(startCurrency))
                {
                    Console.WriteLine("No such currency.");
                    goto RetryStartCurrency;
                }

            RetryEndCurrency:
                Console.WriteLine("Enter the currency to convert to:");
                string endCurrency = Console.ReadLine().ToUpper();

                if (!currencyValues.ContainsKey(endCurrency))
                {
                    Console.WriteLine("No such currency.");
                    goto RetryEndCurrency;
                }

                var result = string.Format("{0:0.000000000000000000}", ConvertCurrency(inputAmount, currencyValues[startCurrency], currencyValues[endCurrency]));

                Console.WriteLine($"{inputAmount} {startCurrency} = {result} {endCurrency}");

                Console.WriteLine("\nWould you like to do another operation? [Y/N]");
                if (Console.ReadLine().ToUpper() != "Y")
                {
                    break;
                }
            }
        }

        public static decimal ConvertCurrency(decimal amount, decimal startCurr, decimal endCurr)
        {
            if (endCurr == 0)
                return -1;

            return (amount * startCurr) / endCurr;
        }

        public static Dictionary<string,decimal> ReadCurrencyValues(string path)
        {
            try
            {
                StreamReader sr = new StreamReader(path);
                var ret = new Dictionary<string, decimal>();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    var split = line.Split(',');
                    if (split.Length != 2)
                    {
                        return null;
                    }
                    else
                    {
                        if (!decimal.TryParse(split[1], out decimal value))
                        {
                            return null;
                        }
                        ret.Add(split[0], value);
                    }
                }
                sr.Close();
                return ret;
            }catch(Exception)
            {
                return null;
            }
        }
    }
}
