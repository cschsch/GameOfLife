using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Graphics.Console.FastConsole
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
    }
}