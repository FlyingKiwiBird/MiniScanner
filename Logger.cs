using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EveScanner
{
    public class Logger
    {
        public static void Log(string level, string message)
        {
            string currentLevel = EveScannerConfig.Instance.DebugLevel.ToLower();
            if (level != "error" && level != "fatal")
            {
                if (currentLevel == "none") return;

                if (currentLevel == "results" && level != currentLevel) return;

                if (currentLevel == "scans" && level == "debug") return;
            }

            string format = "[{0}] - {1} - {2}";
            string output = string.Format(format, DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"), level, message);

            File.AppendAllLines(EveScannerConfig.Instance.LogFile, new string[] { output });
        }

        public static void Log(string level, string message, params string[] args)
        {
            Logger.Log(level, string.Format(message, args));
        }

        public static void Result(string message)
        {
            Logger.Log("results", message);
        }

        public static void Result(string message, params string[] args)
        {
            Logger.Result(string.Format(message, args));
        }

        public static void Scan(string message)
        {
            Logger.Log("scans", message);
        }

        public static void Scan(string message, params string[] args)
        {
            Logger.Scan(string.Format(message, args));
        }

        public static void Debug(string message)
        {
            Logger.Log("debug", message);
        }

        public static void Debug(string message, params string[] args)
        {
            Logger.Debug(string.Format(message, args));
        }
        
        public static void Error(string message)
        {
            Logger.Log("error", message);

            DialogResult result = MessageBox.Show("An error occurred, do you wish to continue?" + Environment.NewLine + message, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.No)
            {
                Logger.Fatal("User selected No option, exiting.");
                Application.Exit();
            }
            else
            {
                Logger.Error("User selected Yes option, continuing.");
            }
        }

        public static void Error(string message, params string[] args)
        {
            Logger.Error(string.Format(message, args));
        }

        public static void Fatal(string message)
        {
            Logger.Log("fatal", message);
        }

        public static void Fatal(string message, params string[] args)
        {
            Logger.Fatal(string.Format(message, args));
        }
    }
}
