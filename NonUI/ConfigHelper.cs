using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;

namespace EveScanner
{
    public class EveScannerConfig
    {
        private static EveScannerConfig instance = null;

        public int AppWidth { get; set; }
        public int AppHeight { get; set; }
        public int WindowPosX { get; set; }
        public int WindowPosY { get; set; }
        public bool ShowExtra { get; set; }
        public bool CaptureClipboard { get; set; }
        public bool AlwaysOnTop { get; set; }
        public string DebugLevel { get; set; }
        public string LogFile { get; set; }
        public string Location1 { get; set; }
        public string Location3 { get; set; }
        public string Location2 { get; set; }

        public NameValueCollection ImageGroups { get; private set; }
        public NameValueCollection ImageItems { get; private set; }
        public NameValueCollection ImageNames { get; private set; }

        public static EveScannerConfig Instance
        {
            get
            {
                if (EveScannerConfig.instance == null)
                {
                    EveScannerConfig.instance = new EveScannerConfig();
                    EveScannerConfig.instance.Load();
                }

                return EveScannerConfig.instance;
            }
        }

        private EveScannerConfig()
        {
        }

        private void Load()
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            this.AppWidth = EveScannerConfig.ConvertToInt(EveScannerConfig.GetAppConfigValue(cfg, "AppWidth"), 680);
            this.AppHeight = EveScannerConfig.ConvertToInt(EveScannerConfig.GetAppConfigValue(cfg, "AppHeight"), 520);
            this.WindowPosX = EveScannerConfig.ConvertToInt(EveScannerConfig.GetAppConfigValue(cfg, "WindowPosX"), -1);
            this.WindowPosY = EveScannerConfig.ConvertToInt(EveScannerConfig.GetAppConfigValue(cfg, "WindowPosY"), -1);
            this.ShowExtra = EveScannerConfig.GetAppConfigValue(cfg, "ShowExtra") != "false";
            this.CaptureClipboard = EveScannerConfig.GetAppConfigValue(cfg, "CaptureClipboard") != "false";
            this.AlwaysOnTop = EveScannerConfig.GetAppConfigValue(cfg, "AlwaysOnTop") == "true";
            this.DebugLevel = EveScannerConfig.GetAppConfigValue(cfg, "DebugLevel", "none");
            this.LogFile = EveScannerConfig.GetAppConfigValue(cfg, "LogFile", "evescanner.log");
            this.Location1 = EveScannerConfig.GetAppConfigValue(cfg, "Location1", "Perimeter -> Urlen");
            this.Location2 = EveScannerConfig.GetAppConfigValue(cfg, "Location2", "Ashab -> Madirmilire");
            this.Location3 = EveScannerConfig.GetAppConfigValue(cfg, "Location3", "Hatakani -> Sivala");

            this.ImageGroups = ConfigurationManager.GetSection("imageGroups") as NameValueCollection;
            this.ImageItems = ConfigurationManager.GetSection("imageItems") as NameValueCollection;
            this.ImageNames = ConfigurationManager.GetSection("imageNames") as NameValueCollection;
        }

        public void Save()
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            EveScannerConfig.SetAppConfigValue(cfg, "AppWidth", this.AppWidth.ToString());
            EveScannerConfig.SetAppConfigValue(cfg, "AppHeight", this.AppHeight.ToString());
            EveScannerConfig.SetAppConfigValue(cfg, "WindowPosX", this.WindowPosX.ToString());
            EveScannerConfig.SetAppConfigValue(cfg, "WindowPosY", this.WindowPosY.ToString());
            EveScannerConfig.SetAppConfigValue(cfg, "ShowExtra", this.ShowExtra ? "true" : "false");
            EveScannerConfig.SetAppConfigValue(cfg, "CaptureClipboard", this.CaptureClipboard ? "true" : "false");
            EveScannerConfig.SetAppConfigValue(cfg, "AlwaysOnTop", this.AlwaysOnTop ? "true" : "false");
            EveScannerConfig.SetAppConfigValue(cfg, "DebugLevel", this.DebugLevel);
            EveScannerConfig.SetAppConfigValue(cfg, "LogFile", this.LogFile);
            EveScannerConfig.SetAppConfigValue(cfg, "Location1", this.Location1);
            EveScannerConfig.SetAppConfigValue(cfg, "Location2", this.Location2);
            EveScannerConfig.SetAppConfigValue(cfg, "Location3", this.Location3);

            cfg.Save(ConfigurationSaveMode.Modified);
        }

        public int? FindImageToDisplay(string[] items)
        {
            int? minImageIndex = Int32.MaxValue;
            foreach (string item in items)
            {
                if (!string.IsNullOrEmpty(this.ImageItems[item]))
                {
                    int curImageIndex = EveScannerConfig.ConvertToInt(this.ImageItems[item], 99);
                    if (curImageIndex < minImageIndex)
                    {
                        minImageIndex = curImageIndex;
                    }
                }
            }

            return minImageIndex < Int32.MaxValue ? minImageIndex : null;
        }

        private static string GetAppConfigValue(Configuration cfg, string key, string defaultValue = null, bool throwExceptionIfMissing = false)
        {
            var v = cfg.AppSettings.Settings[key];

            if (throwExceptionIfMissing && (v == null || string.IsNullOrEmpty(v.Value)))
            {
                throw new Exception("missing app config key");
            }

            return v != null ? v.Value : defaultValue;
        }

        private static void SetAppConfigValue(Configuration cfg, string key, string value)
        {
            if (cfg.AppSettings.Settings[key] != null)
            {
                cfg.AppSettings.Settings.Remove(key);
            }

            cfg.AppSettings.Settings.Add(key, value);
        }

        private static int ConvertToInt(string input, int defaultValue)
        {
            int output;

            if (!int.TryParse(input, out output))
            {
                return defaultValue;
            }

            return output;
        }
    }
}
