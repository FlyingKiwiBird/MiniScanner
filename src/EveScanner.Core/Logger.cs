//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="Logger.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Core
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// Provides a simple logger without external dependencies.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Used for thread safety.
        /// </summary>
        private static object lockIt = new object();

        /// <summary>
        /// Writes a log message with a specified logging level to a file. This method actually writes to the file, all other methods call this one.
        /// </summary>
        /// <param name="level">Logging level</param>
        /// <param name="message">Message to log</param>
        public static void Log(string level, string message)
        {
            string currentLevel = ConfigHelper.Instance.DebugLevel.ToUpper(CultureInfo.InvariantCulture);
            if (level != "ERROR" && level != "FATAL")
            {
                if (currentLevel == "NONE")
                {
                    return;
                }

                if (currentLevel == "RESULTS" && level != currentLevel)
                {
                    return;
                }

                if (currentLevel == "SCANS" && level == "DEBUG")
                {
                    return;
                }
            }

            string format = "[{0}] - {1} - {2}";
            string output = string.Format(CultureInfo.InvariantCulture, format, DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss", CultureInfo.InvariantCulture), level, message);

            lock (lockIt)
            {
                File.AppendAllLines(ConfigHelper.Instance.LogFile, new string[] { output });
            }
        }

        /// <summary>
        /// Writes a log message with a specified logging level to a file with a parameterized message string.
        /// </summary>
        /// <param name="level">Logging level</param>
        /// <param name="message">Message to log</param>
        /// <param name="args">Message arguments</param>
        public static void Log(string level, string message, params string[] args)
        {
            Logger.Log(level, string.Format(CultureInfo.InvariantCulture, message, args));
        }

        /// <summary>
        /// Writes a log message at the results level.
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Result(string message)
        {
            Logger.Log("results", message);
        }

        /// <summary>
        /// Writes a log message at the results level with optional parameters.
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="args">Message arguments</param>
        public static void Result(string message, params string[] args)
        {
            Logger.Result(string.Format(CultureInfo.InvariantCulture, message, args));
        }

        /// <summary>
        /// Writes a log message at the scan level.
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Scan(string message)
        {
            Logger.Log("scans", message);
        }

        /// <summary>
        /// Writes a log message at the scan level with optional parameters.
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="args">Message arguments</param>
        public static void Scan(string message, params string[] args)
        {
            Logger.Scan(string.Format(CultureInfo.InvariantCulture, message, args));
        }

        /// <summary>
        /// Writes a log message at the debug level.
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Debug(string message)
        {
            Logger.Log("debug", message);
        }

        /// <summary>
        /// Writes a log message at the debug level with optional parameters.
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="args">Message arguments</param>
        public static void Debug(string message, params string[] args)
        {
            Logger.Debug(string.Format(CultureInfo.InvariantCulture, message, args));
        }

        /// <summary>
        /// Writes a log message at the error level. These should be non-fatal errors. This method will present a dialog to the user asking if they wish to continue.
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Error(string message)
        {
            Logger.Error(message, false);
        }

        /// <summary>
        /// Writes a log message at the error level. These should be non-fatal errors. This method will present a dialog to the user asking if they wish to continue.
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="skipDialog">Whether the dialog should be skipped.</param>
        public static void Error(string message, bool skipDialog)
        {
            Logger.Log("error", message);

            if (!skipDialog)
            {
                DialogResult result = MessageBox.Show("An error occurred, do you wish to continue?" + Environment.NewLine + message, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
                if (result == DialogResult.No)
                {
                    Logger.Fatal("User selected No option, exiting.");
                    Application.Exit();
                }
                else
                {
                    Logger.Error("User selected Yes option, continuing.", true);
                }
            }
        }

        /// <summary>
        /// Writes a log message at the error level with optional parameters.
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="args">Message arguments</param>
        public static void Error(string message, params string[] args)
        {
            Logger.Error(string.Format(CultureInfo.InvariantCulture, message, args));
        }

        /// <summary>
        /// Writes a log message at the fatal level.
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Fatal(string message)
        {
            Logger.Log("fatal", message);
        }

        /// <summary>
        /// Writes a log message at the fatal level with optional parameters.
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="args">Message arguments</param>
        public static void Fatal(string message, params string[] args)
        {
            Logger.Fatal(string.Format(CultureInfo.InvariantCulture, message, args));
        }
    }
}
