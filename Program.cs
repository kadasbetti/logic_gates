using System;
using System.Collections.Generic;

namespace homework910
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] firstline = Console.ReadLine().Split(" ");

            string line = "";
            List<Signal> alldata = new List<Signal>();
            int numOfData = 0;

            do
            {
                line = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] parts = line.Split(' ');
                    if (char.Parse(parts[0])>= 'A' && char.Parse(parts[0])<= 'Z' && numOfData < 100)
                    {
                    Signal data = new Signal(char.Parse(parts[0]), Converter(parts[1]));
                    alldata.Add(data);
                    numOfData++;
                    }
                }
            }
            while (!string.IsNullOrWhiteSpace(line));

            List<string> result = new List<string>();
            int numOfGates = 0;

            for (int i = firstline.Length - 1; i >= 0; i--)
            {
                if (firstline[i].Length == 1)
                {
                    result.Add(ValueOf(firstline[i], alldata));
                }

                if (firstline[i].Length > 1 && numOfGates<=100)
                {
                    if (firstline[i] == "AND")
                    {
                        string input1 = result[result.Count - 1];
                        string input2 = result[result.Count - 2];
                        result.RemoveAt(result.Count - 1);
                        result.RemoveAt(result.Count - 1);
                        result.Add(AND(input1, input2));
                    }
                    if (firstline[i] == "OR")
                    {
                        string input1 = result[result.Count - 1];
                        string input2 = result[result.Count - 2];
                        result.RemoveAt(result.Count - 1);
                        result.RemoveAt(result.Count - 1);
                        result.Add(OR(input1, input2));
                    }
                    if (firstline[i] == "NOT")
                    {
                        string input1 = result[result.Count - 1];
                        result.RemoveAt(result.Count - 1);
                        result.Add(NOT(input1));
                    }
                    if (firstline[i] == "NAND")
                    {
                        string input1 = result[result.Count - 1];
                        string input2 = result[result.Count - 2];
                        result.RemoveAt(result.Count - 1);
                        result.RemoveAt(result.Count - 1);
                        result.Add(NAND(input1, input2));
                    }
                    if (firstline[i] == "NOR")
                    {
                        string input1 = result[result.Count - 1];
                        string input2 = result[result.Count - 2];
                        result.RemoveAt(result.Count - 1);
                        result.RemoveAt(result.Count - 1);
                        result.Add(NOR(input1, input2));
                    }
                    if (firstline[i] == "XOR")
                    {
                        string input1 = result[result.Count - 1];
                        string input2 = result[result.Count - 2];
                        result.RemoveAt(result.Count - 1);
                        result.RemoveAt(result.Count - 1);
                        result.Add(XOR(input1, input2));
                    }
                    if (firstline[i] == "XNOR")
                    {
                        string input1 = result[result.Count - 1];
                        string input2 = result[result.Count - 2];
                        result.RemoveAt(result.Count - 1);
                        result.RemoveAt(result.Count - 1);
                        result.Add(XNOR(input1, input2));
                    }
                    numOfGates++;
                }
            }

            Console.WriteLine(result[0]);
        }

        static string Converter(string input)
        {
            string x = input;
            string b = "";

            for (int i = 0; i < x.Length; i += 2)
            {
                string vl = x.Substring(i, 2);
                double v = double.Parse(vl) / 10.0;

                if(v>= 0.0 && v <= 5.0)
                { 

                        if (v >= 0.0 && v <= 0.8)
                        {
                            b += "0";
                        }
                        else if (v >= 2.7 && v <= 5)
                        {
                            b += "1";
                        }
                        else
                        {
                            b += "E";
                        }

                }
            }
            return b;
        }

        static string ValueOf(string character, List<Signal> allsignals)
        {
            string value = "";
            foreach (var line in allsignals)
            {
                if (Char.Parse(character) == line.Character)
                {
                    value = line.Value;
                }
            }
            return value;
        }

        static string AND(string input1, string input2)
        {
            string result = "";

            for (int i = 0; i < input1.Length; i++)
            {
                if (input1[i] == '1' && input2[i] == '1')
                {
                    result += "1";
                }
                else if (input2[i] == 'E' || input1[i] == 'E')
                {
                    result += "E";
                }
                else
                {
                    result += "0";
                }
            }

            return result;
        }

        static string OR(string input1, string input2)
        {
            string result = "";
            for (int i = 0; i < input1.Length; i++)
            {
                if (input1[i] == '0' && input2[i] == '0')
                {
                    result += "0";
                }
                else if (input2[i] == 'E' || input1[i] == 'E')
                {
                    result += "E";
                }
                else
                {
                    result += "1";
                }
            }
            return result;
        }

        static string NOT(string input1)
        {
            string result = "";
            for (int i = 0; i < input1.Length; i++)
            {
                if (input1[i] == '0')
                {
                    result += "1";
                }
                else if (input1[i] == '1')
                {
                    result += "0";
                }
                else
                {
                    result += "E";
                }
            }
            return result;
        }

        static string NAND(string input1, string input2)
        {
            string result = AND(input1, input2);

            return NOT(result);
        }

        static string NOR(string input1, string input2)
        {
            string result = OR(input1, input2);

            return NOT(result);
        }

        static string XOR(string input1, string input2)
        {
            string result = "";
            for (int i = 0; i < input1.Length; i++)
            {
                if (input1[i] == '0' && input2[i] == '0' || input1[i] == '1' && input2[i] == '1')
                {
                    result += "0";
                }
                else if (input2[i] == 'E' || input1[i] == 'E')
                {
                    result += "E";
                }
                else
                {
                    result += "1";
                }
            }
            return result;
        }

        static string XNOR(string input1, string input2)
        {
            string result = XOR(input1, input2);

            return NOT(result);
        }
    }

    internal class Signal
    {
        char character;
        string value;

        public Signal(char character, string value)
        {
            this.character = character;
            this.value = value;
        }

        public char Character { get => character; }
        public string Value { get => value; }
    }
}
