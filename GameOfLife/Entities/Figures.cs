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
            .Append("         1        ").Append(Environment.NewLine)
            .Append("        111       ").Append(Environment.NewLine)
            .Append("       11111      ").Append(Environment.NewLine)
            .Append("                  ").Append(Environment.NewLine)
            .Append("                  ").Append(Environment.NewLine)
            .Append("                  ").Append(Environment.NewLine)
            .Append("                  ").Append(Environment.NewLine)
            .Append("                  ").Append(Environment.NewLine)
            .Append("                  ").Append(Environment.NewLine)
            .Append("       11111      ").Append(Environment.NewLine)
            .Append("        111       ").Append(Environment.NewLine)
            .Append("         1        ").Append(Environment.NewLine)
            .Append("                  ").Append(Environment.NewLine)
            .Append("                  ").Append(Environment.NewLine)
            .Append("                  ").ToString();

        public static string Pulsar => new StringBuilder()
            .Append("                 ").Append(Environment.NewLine)
            .Append("                 ").Append(Environment.NewLine)
            .Append("    111   111    ").Append(Environment.NewLine)
            .Append("                 ").Append(Environment.NewLine)
            .Append("  1    1 1    1  ").Append(Environment.NewLine)
            .Append("  1    1 1    1  ").Append(Environment.NewLine)
            .Append("  1    1 1    1  ").Append(Environment.NewLine)
            .Append("    111   111    ").Append(Environment.NewLine)
            .Append("                 ").Append(Environment.NewLine)
            .Append("    111   111    ").Append(Environment.NewLine)
            .Append("  1    1 1    1  ").Append(Environment.NewLine)
            .Append("  1    1 1    1  ").Append(Environment.NewLine)
            .Append("  1    1 1    1  ").Append(Environment.NewLine)
            .Append("                 ").Append(Environment.NewLine)
            .Append("    111   111    ").Append(Environment.NewLine)
            .Append("                 ").Append(Environment.NewLine)
            .Append("                 ").ToString();

        public static string Lwss => new StringBuilder()
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("  1  1         ").Append(Environment.NewLine)
            .Append("      1        ").Append(Environment.NewLine)
            .Append("  1   1        ").Append(Environment.NewLine)
            .Append("   1111        ").Append(Environment.NewLine)
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
            .Append("  1            ").Append(Environment.NewLine)
            .Append("   1           ").Append(Environment.NewLine)
            .Append(" 111           ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").Append(Environment.NewLine)
            .Append("               ").ToString();
    }
}