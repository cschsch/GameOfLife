using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameOfLife.Renderer.FastConsole
{
    public static class QuickWrite
    {
        private static void WriteWithBuffer(Kernel32.CharInfo[] buffer, short size)
        {
            var conout = Kernel32.CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
            if (conout.IsInvalid) return;
            var rect = new Kernel32.SmallRect { Left = 0, Top = 0, Right = size, Bottom = size };
            Kernel32.WriteConsoleOutput(conout, buffer, new Kernel32.Coord { X = size, Y = size }, new Kernel32.Coord { X = 0, Y = 0 }, ref rect);
        }

        public static void Write(IEnumerable<Kernel32.CharInfo> bufferSeq, short size) =>
            WriteWithBuffer(bufferSeq.ToArray(), size);

        public static void Write(string text, short size, ConsoleColor color = ConsoleColor.White) =>
            WriteWithBuffer(GenerateBuffer(text, color), size);

        private static Kernel32.CharInfo[] GenerateBuffer(string text, ConsoleColor color = ConsoleColor.White) =>
            text.Replace(Environment.NewLine, "").Select(c => ProjectChar(c, color)).ToArray();

        private static Kernel32.CharInfo ProjectChar(char charToCheck, ConsoleColor color) =>
            charToCheck == ' '
                ? new Kernel32.CharInfo()
                : new Kernel32.CharInfo {Attributes = (short) color, Char = new Kernel32.CharUnion {UnicodeChar = charToCheck}};
    }
}