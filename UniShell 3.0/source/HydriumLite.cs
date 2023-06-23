using System;
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
        public static void COutputNSys(string outputText, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(outputText);
            Console.ForegroundColor = color;
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
                Console.Write(inputText[symb]);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void ColorPatternOutput(string output, ColorOutputPattern pattern)
        {
            switch(pattern)
            {
                case ColorOutputPattern.Mono:
                    ConsoleColor[] mono = {ConsoleColor.DarkGray, ConsoleColor.Gray,ConsoleColor.White};
                    int currentColor = 0;
                    for(int i = 0; i < output.Length; i++)
                    {
                        try
                        {

                            Console.ForegroundColor = mono[currentColor];
                        }
                        catch
                        {
                            currentColor = 0;
                            Console.ForegroundColor = mono[currentColor];
                        }
                        Console.Write(output[i]);
                        currentColor++;
                    }
                    break;
                case ColorOutputPattern.Fire:
                    ConsoleColor[] fire = { ConsoleColor.DarkRed, ConsoleColor.Red, ConsoleColor.DarkYellow, ConsoleColor.Yellow};
                    currentColor = 0;
                    for (int i = 0; i < output.Length; i++)
                    {
                        try
                        {

                            Console.ForegroundColor = fire[currentColor];
                        }
                        catch
                        {
                            currentColor = 0;
                            Console.ForegroundColor = fire[currentColor];
                        }
                        Console.Write(output[i]);
                        currentColor++;
                    }
                    break;
                case ColorOutputPattern.RGB:
                    ConsoleColor[] rgb = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue };
                    currentColor = 0;
                    for (int i = 0; i < output.Length; i++)
                    {
                        try
                        {

                            Console.ForegroundColor = rgb[currentColor];
                        }
                        catch
                        {
                            currentColor = 0;
                            Console.ForegroundColor = rgb[currentColor];
                        }
                        Console.Write(output[i]);
                        currentColor++;
                    }
                    break;
                case ColorOutputPattern.Sea:
                    ConsoleColor[] sea = { ConsoleColor.DarkBlue, ConsoleColor.Blue, ConsoleColor.Cyan };
                    currentColor = 0;
                    for (int i = 0; i < output.Length; i++)
                    {
                        try
                        {

                            Console.ForegroundColor = sea[currentColor];
                        }
                        catch
                        {
                            currentColor = 0;
                            Console.ForegroundColor = sea[currentColor];
                        }
                        Console.Write(output[i]);
                        currentColor++;
                    }
                    break;
                case ColorOutputPattern.Rainbow:
                    ConsoleColor[] rainbow = { ConsoleColor.Red, ConsoleColor.DarkYellow, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Magenta };
                    currentColor = 0;
                    for (int i = 0; i < output.Length; i++)
                    {
                        try
                        {

                            Console.ForegroundColor = rainbow[currentColor];
                        }
                        catch
                        {
                            currentColor = 0;
                            Console.ForegroundColor = rainbow[currentColor];
                        }
                        Console.Write(output[i]);
                        currentColor++;
                    }
                    break;

            }
        }
        public static string RealtimeInput(string inputText)
        {
            
            InputTextSysOutput(inputText);
            string inputResults = Console.ReadLine();
            return inputResults;
        }
        public static void OutputList(List<string> list)
        {
            for(int li = 0; li < list.Count; li++)
            {
                for(int i = 0; i < list[li].Length; i++)
                {
                    switch (list[li][i])
                    {
                        case '|':
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write(list[li][i]);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(list[li][i]);
                            break;
                    }
                    
                }
                Console.WriteLine();
            }
        }
    }
    public enum ColorOutputPattern
    {
        Mono,
        Fire,
        RGB,
        Sea,
        Rainbow
    }
}
