using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

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

        public static CharInfo[] GenerateBuffer(string text, ConsoleColor color = ConsoleColor.White) =>
            text.Replace(Environment.NewLine, "").Select(c => ProjectChar(c, color)).ToArray();

        private static CharInfo ProjectChar(char charToCheck, ConsoleColor color) =>
            charToCheck == ' '
                ? new CharInfo()
                : new CharInfo {Attributes = (short) color, Char = new CharUnion {UnicodeChar = charToCheck}};
    }

    public static class Kernel32
    {
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern bool WriteConsoleOutput(
            SafeFileHandle hConsoleOutput,
            CharInfo[] lpBuffer,
            Coord dwBufferSize,
            Coord dwBufferCoord,
            ref SmallRect lpWriteRegion);

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            public short X;
            public short Y;

            public Coord(short x, short y)
            {
                X = x;
                Y = y;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CharUnion
        {
            [FieldOffset(0)] public char UnicodeChar;
            [FieldOffset(0)] public byte AsciiChar;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CharInfo
        {
            [FieldOffset(0)] public CharUnion Char;
            [FieldOffset(2)] public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SmallRect
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }
    }
}