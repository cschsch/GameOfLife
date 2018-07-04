using System;
using System.Text;

namespace GameOfLife.Entities
{
    public static class Figures
    {
        public static string Pentadecathlon => new StringBuilder()
            .Append("                  ").Append(Environment.NewLine)
            .Append("                  ").Append(Environment.NewLine)
            .Append("                  ").Append(Environment.NewLine)
            .Append("         X        ").Append(Environment.NewLine)
            .Append("        XXX       ").Append(Environment.NewLine)
            .Append("       XXXXX      ").Append(Environment.NewLine)
            .Append("                  ").Append(Environment.NewLine)
            .Append("                  ").Append(Environment.NewLine)
            .Append("                  ").Append(Environment.NewLine)
            .Append("                  ").Append(Environment.NewLine)
            .Append("                  ").Append(Environment.NewLine)
            .Append("                  ").Append(Environment.NewLine)
            .Append("       XXXXX      ").Append(Environment.NewLine)
            .Append("        XXX       ").Append(Environment.NewLine)
            .Append("         X        ").Append(Environment.NewLine)
            .Append("                  ").Append(Environment.NewLine)
            .Append("                  ").Append(Environment.NewLine)
            .Append("                  ").ToString();

        public static string Pulsar => new StringBuilder()
            .Append("                 ").Append(Environment.NewLine)
            .Append("                 ").Append(Environment.NewLine)
            .Append("    XXX   XXX    ").Append(Environment.NewLine)
            .Append("                 ").Append(Environment.NewLine)
            .Append("  X    X X    X  ").Append(Environment.NewLine)
            .Append("  X    X X    X  ").Append(Environment.NewLine)
            .Append("  X    X X    X  ").Append(Environment.NewLine)
            .Append("    XXX   XXX    ").Append(Environment.NewLine)
            .Append("                 ").Append(Environment.NewLine)
            .Append("    XXX   XXX    ").Append(Environment.NewLine)
            .Append("  X    X X    X  ").Append(Environment.NewLine)
            .Append("  X    X X    X  ").Append(Environment.NewLine)
            .Append("  X    X X    X  ").Append(Environment.NewLine)
            .Append("                 ").Append(Environment.NewLine)
            .Append("    XXX   XXX    ").Append(Environment.NewLine)
            .Append("                 ").Append(Environment.NewLine)
            .Append("                 ").ToString();

        public static string Lwss => new StringBuilder()
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("  X  X         ").Append(Environment.NewLine)
            .Append("      X        ").Append(Environment.NewLine)
            .Append("  X   X        ").Append(Environment.NewLine)
            .Append("   XXXX        ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").ToString();

        public static string Glider => new StringBuilder()
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("  X            ").Append(Environment.NewLine)
            .Append("   X           ").Append(Environment.NewLine)
            .Append(" XXX           ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").ToString();
    }
}