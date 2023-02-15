﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydrium
{
    public class Engine
    {
        public static void OutputSysNE(string outputText)
        {
            outputText = outputText.Replace("[", "<");
            outputText = outputText.Replace("]", ">");
            for (int symb = 0; symb < outputText.Length; symb++)
            {
                if (outputText[symb] == '<')
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                if (symb != 0)
                {
                    if (outputText[symb - 1] == '>')
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                }
                Console.Write(outputText[symb]);

            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void OutputSysWE(string outputText)
        {
            outputText = outputText.Replace("[", "<");
            outputText = outputText.Replace("]", ">");
            for (int symb = 0; symb < outputText.Length; symb++)
            {
                if (outputText[symb] == '<')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                if (symb != 0)
                {
                    if (outputText[symb - 1] == '>')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                }
                Console.Write(outputText[symb]);

            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void OutputSysWW(string outputText)
        {
            outputText = outputText.Replace("[", "<");
            outputText = outputText.Replace("]", ">");
            for (int symb = 0; symb < outputText.Length; symb++)
            {
                if (outputText[symb] == '<')
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                if (symb != 0)
                {
                    if (outputText[symb - 1] == '>')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                }
                Console.Write(outputText[symb]);

            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void OutputNSysNLine(string outputText)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(outputText);
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        public static void OutputNSys(string outputText)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(outputText);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void InputTextSysOutput(string inputText)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int symb = 0; symb < inputText.Length; symb++)
            {
                if (inputText[symb] == '@')
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                if (inputText[symb] == '>')
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (inputText[symb] == ':')
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.Write(inputText[symb]);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static string RealtimeInput(string inputText)
        {
            
            InputTextSysOutput(inputText);
            string inputResults = Console.ReadLine();
            return inputResults;
        }
    }
}
