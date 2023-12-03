using System;

namespace Core.Server.Util
{
    public static class Logger
    {
        public static void Info(string message)
        {
            var header = $"{CurrentTimeToString()} [INFO]";

            Console.WriteLine($"{header} {message}");
        }

        public static void Warnning(string message)
        {
            var header = $"{CurrentTimeToString()} [WARN]";

            ColorWrite($"{header} {message}", ConsoleColor.Red);
        }

        public static void Error(string message)
        {
            var header = $"{CurrentTimeToString()} [ERROR]";

            ColorWrite($"{header} {message}", ConsoleColor.Red);
        }

        private static string CurrentTimeToString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private static void ColorWrite(string message, ConsoleColor color)
        {
            Console.BackgroundColor = color;

            Console.WriteLine(message);

            Console.ResetColor();
        }
    }
}
