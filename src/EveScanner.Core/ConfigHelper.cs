//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ConfigHelper.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Globalization;

    /// <summary>
    /// Holds the application configuration and provides helper methods for
    /// accessing items stored in the application configuration.
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// Holds the instance of the EveScannerConfig so we don't parse the config all the time.
        /// </summary>
        private static ConfigHelper instance = null;

        /// <summary>
        /// Prevents a default instance of the <see cref="ConfigHelper"/> class from being created.
        /// </summary>
        private ConfigHelper()
        {
        }

        /// <summary>
        /// Gets the instance of the Scanner Configuration class.
        /// </summary>
        public static ConfigHelper Instance
        {
            get
            {
                if (ConfigHelper.instance == null)
                {
                    ConfigHelper.instance = new ConfigHelper();
                    ConfigHelper.instance.Load();
                }

                return ConfigHelper.instance;
            }
        }

        /// <summary>
        /// Gets or sets the Width of the application window.
        /// </summary>
        public int AppWidth { get; set; }

        /// <summary>
        /// Gets or sets the Height of the application window.
        /// </summary>
        public int AppHeight { get; set; }

        /// <summary>
        /// Gets or sets the horizontal portion of the coordinate of the top left of the application window.
        /// </summary>
        public int WindowPositionX { get; set; }

        /// <summary>
        /// Gets or sets the vertical portion of the coordinate of the top left of the application window.
        /// </summary>
        public int WindowPositionY { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether extra form controls are shown.
        /// </summary>
        public bool ShowExtra { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the application should care about clipboard content changes.
        /// </summary>
        public bool CaptureClipboard { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the application should stay "Always on Top"
        /// </summary>
        public bool AlwaysOnTop { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we should keep the location checked between scans.
        /// </summary>
        public bool KeepLocation { get; set; }

        /// <summary>
        /// Gets or sets the logging level for the application.
        /// </summary>
        public string DebugLevel { get; set; }

        /// <summary>
        /// Gets or sets the path to the log file.
        /// </summary>
        public string LogFile { get; set; }

        /// <summary>
        /// Gets or sets the first user enterable location.
        /// </summary>
        public string Location1 { get; set; }

        /// <summary>
        /// Gets or sets the second user enterable location.
        /// </summary>
        public string Location2 { get; set; }

        /// <summary>
        /// Gets or sets the third user enterable location.
        /// </summary>
        public string Location3 { get; set; }

        /// <summary>
        /// Gets or sets the source of our scan data.
        /// </summary>
        public string ScanSource { get; set; }

        /// <summary>
        /// Gets the name value collection containing Image Group Id -> Physical Image Location
        /// </summary>
        public NameValueCollection ImageGroups { get; private set; }

        /// <summary>
        /// Gets the name value collection containing Item Name -> Image Group Id
        /// </summary>
        public NameValueCollection ImageItems { get; private set; }

        /// <summary>
        /// Gets the name value collection containing Image Group Id -> Group Name
        /// </summary>
        public NameValueCollection ImageNames { get; private set; }

        /// <summary>
        /// Gets the name value collection containing Ship Description -> Scan Output Name
        /// </summary>
        public NameValueCollection ShipTypes { get; private set; }

        /// <summary>
        /// Gets the name value collection containing Interface -> Implementation
        /// </summary>
        public NameValueCollection Implementations { get; private set; }

        /// <summary>
        /// Saves the current state of the application to the configuration file.
        /// </summary>
        public void Save()
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            ConfigHelper.SetAppConfigValue(cfg, "AppWidth", this.AppWidth.ToString(CultureInfo.InvariantCulture));
            ConfigHelper.SetAppConfigValue(cfg, "AppHeight", this.AppHeight.ToString(CultureInfo.InvariantCulture));
            ConfigHelper.SetAppConfigValue(cfg, "WindowPosX", this.WindowPositionX.ToString(CultureInfo.InvariantCulture));
            ConfigHelper.SetAppConfigValue(cfg, "WindowPosY", this.WindowPositionY.ToString(CultureInfo.InvariantCulture));
            ConfigHelper.SetAppConfigValue(cfg, "ShowExtra", this.ShowExtra ? "true" : "false");
            ConfigHelper.SetAppConfigValue(cfg, "CaptureClipboard", this.CaptureClipboard ? "true" : "false");
            ConfigHelper.SetAppConfigValue(cfg, "AlwaysOnTop", this.AlwaysOnTop ? "true" : "false");
            ConfigHelper.SetAppConfigValue(cfg, "KeepLocation", this.KeepLocation ? "true" : "false");
            ConfigHelper.SetAppConfigValue(cfg, "DebugLevel", this.DebugLevel);
            ConfigHelper.SetAppConfigValue(cfg, "LogFile", this.LogFile);
            ConfigHelper.SetAppConfigValue(cfg, "Location1", this.Location1);
            ConfigHelper.SetAppConfigValue(cfg, "Location2", this.Location2);
            ConfigHelper.SetAppConfigValue(cfg, "Location3", this.Location3);
            ConfigHelper.SetAppConfigValue(cfg, "ScanSource", this.ScanSource);

            cfg.Save(ConfigurationSaveMode.Modified);
        }

        /// <summary>
        /// Finds the index of the image which should be displayed given a list of items to search.
        /// </summary>
        /// <param name="items">Array of item names</param>
        /// <returns>Index to image or null if no matching image found</returns>
        public IEnumerable<int> FindImagesToDisplay(string[] items)
        {
            List<int> imagesSeen = new List<int>();

            if (items == null || items.Length == 0)
            {
                yield break;
            }

            foreach (string item in items)
            {
                if (!string.IsNullOrEmpty(this.ImageItems[item]))
                {
                    int curImageIndex = ConfigHelper.ConvertToInt(this.ImageItems[item], int.MaxValue);

                    if (!imagesSeen.Contains(curImageIndex))
                    {
                        imagesSeen.Add(curImageIndex);
                        yield return curImageIndex;
                    }
                }
            }

            yield break;
        }

        /// <summary>
        /// Gets an implementation of an interface as defined in the configuration file. This is not high-performance, so, don't use it a lot.
        /// </summary>
        /// <typeparam name="T">Interface Type</typeparam>
        /// <returns>Instantiated Object</returns>
        internal static T GetImplementation<T>()
        {
            string objType = ConfigHelper.Instance.Implementations[typeof(T).Name];

            if (string.IsNullOrEmpty(objType))
            {
                return default(T);
            }

            return (T)Activator.CreateInstance(Type.GetType(objType));
        }

        /// <summary>
        /// Gets the Application Configuration Value for the Specified Key
        /// </summary>
        /// <param name="cfg">Configuration object</param>
        /// <param name="key">Key to requested configuration element</param>
        /// <param name="defaultValue">Default value if the element is missing or empty</param>
        /// <param name="throwExceptionIfMissing">A value indicating whether an exception should be thrown if the key isn't present</param>
        /// <returns>Configured Value</returns>
        private static string GetAppConfigValue(Configuration cfg, string key, string defaultValue = null, bool throwExceptionIfMissing = false)
        {
            var v = cfg.AppSettings.Settings[key];

            if (throwExceptionIfMissing && (v == null || string.IsNullOrEmpty(v.Value)))
            {
                throw new ConfigurationErrorsException("missing app config key");
            }

            return v != null ? v.Value : defaultValue;
        }

        /// <summary>
        /// Sets a key-value pair in the configuration file.
        /// </summary>
        /// <param name="cfg">Configuration object</param>
        /// <param name="key">Configuration key</param>
        /// <param name="value">Configuration value</param>
        private static void SetAppConfigValue(Configuration cfg, string key, string value)
        {
            if (cfg.AppSettings.Settings[key] != null)
            {
                cfg.AppSettings.Settings.Remove(key);
            }

            cfg.AppSettings.Settings.Add(key, value);
        }

        /// <summary>
        /// Converts a string to an integer
        /// </summary>
        /// <param name="input">Value to convert</param>
        /// <param name="defaultValue">Default value if conversion fails</param>
        /// <returns>Integer version of input if conversion succeeds, defaultValue otherwise</returns>
        private static int ConvertToInt(string input, int defaultValue)
        {
            int output;

            if (!int.TryParse(input, out output))
            {
                return defaultValue;
            }

            return output;
        }

        /// <summary>
        /// Loads configuration data from the config file into this instance.
        /// </summary>
        private void Load()
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            this.AppWidth = ConfigHelper.ConvertToInt(ConfigHelper.GetAppConfigValue(cfg, "AppWidth"), 590);
            this.AppHeight = ConfigHelper.ConvertToInt(ConfigHelper.GetAppConfigValue(cfg, "AppHeight"), 574);
            this.WindowPositionX = ConfigHelper.ConvertToInt(ConfigHelper.GetAppConfigValue(cfg, "WindowPosX"), -1);
            this.WindowPositionY = ConfigHelper.ConvertToInt(ConfigHelper.GetAppConfigValue(cfg, "WindowPosY"), -1);
            this.ShowExtra = ConfigHelper.GetAppConfigValue(cfg, "ShowExtra") != "false";
            this.CaptureClipboard = ConfigHelper.GetAppConfigValue(cfg, "CaptureClipboard") != "false";
            this.AlwaysOnTop = ConfigHelper.GetAppConfigValue(cfg, "AlwaysOnTop") == "true";
            this.KeepLocation = ConfigHelper.GetAppConfigValue(cfg, "KeepLocation") == "true";
            this.DebugLevel = ConfigHelper.GetAppConfigValue(cfg, "DebugLevel", "none");
            this.LogFile = ConfigHelper.GetAppConfigValue(cfg, "LogFile", "evescanner.log");
            this.Location1 = ConfigHelper.GetAppConfigValue(cfg, "Location1", "Perimeter -> Urlen");
            this.Location2 = ConfigHelper.GetAppConfigValue(cfg, "Location2", "Ashab -> Madirmilire");
            this.Location3 = ConfigHelper.GetAppConfigValue(cfg, "Location3", "Hatakani -> Sivala");
            this.ScanSource = ConfigHelper.GetAppConfigValue(cfg, "ScanSource", "Evepraisal");

            this.ImageGroups = ConfigurationManager.GetSection("imageGroups") as NameValueCollection;
            this.ImageItems = ConfigurationManager.GetSection("imageItems") as NameValueCollection;
            this.ImageNames = ConfigurationManager.GetSection("imageNames") as NameValueCollection;
            this.ShipTypes = ConfigurationManager.GetSection("shipTypes") as NameValueCollection;
            this.Implementations = ConfigurationManager.GetSection("implementations") as NameValueCollection;
        }
    }
}
