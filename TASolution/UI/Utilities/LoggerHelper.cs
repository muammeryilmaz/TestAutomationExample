using System;
using System.IO;

namespace UI.Utilities
{
    /// <summary>
    /// Provides logging functionality for the application.
    /// </summary>
    public static class LoggerHelper
    {
        private static readonly string logFilePath = "UI_Test_Log.txt";

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void LogInfo(string message)
        {
            WriteLog("INFO", message);
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        public static void LogError(string message)
        {
            WriteLog("ERROR", message);
        }

        private static void WriteLog(string logType, string message)
        {
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logType}] {message}";
            Console.WriteLine(logMessage);
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
    }
}
