using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using static GameOfLife.FastConsole.Kernel32;

namespace GameOfLife.FastConsole
{
    public static class QuickWrite
    {
        private static void WriteWithBuffer(CharInfo[] buffer, short size)
        {
            var conout = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
            if (conout.IsInvalid) return;
            var rect = new SmallRect { Left = 0, Top = 0, Right = size, Bottom = size };
            WriteConsoleOutput(conout, buffer, new Coord { X = size, Y = size }, new Coord { X = 0, Y = 0 }, ref rect);
        }

        public static void Write(IEnumerable<CharInfo> bufferSeq, short size) =>
            WriteWithBuffer(bufferSeq.ToArray(), size);

        public static void Write(string text, short size, ConsoleColor color = ConsoleColor.White) =>
            WriteWithBuffer(GenerateBuffer(text, color), size);

        private static CharInfo[] GenerateBuffer(string text, ConsoleColor color = ConsoleColor.White) =>
            text.Replace(Environment.NewLine, "").Select(c => ProjectChar(c, color)).ToArray();

        private static CharInfo ProjectChar(char charToCheck, ConsoleColor color) =>
            charToCheck == ' '
                ? new CharInfo()
                : new CharInfo {Attributes = (short) color, Char = new CharUnion {UnicodeChar = charToCheck}};
    }
}