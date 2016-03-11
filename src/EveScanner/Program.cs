//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="Program.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner
{
    using System;
    using System.Windows.Forms;

    using EveScanner.Core;
    using EveScanner.UI;

    /// <summary>
    /// Entry class for the application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Register all implementations in case we want them later, then configure the ones we want as the defaults.
            RegistrationService rx = new RegistrationService();
            rx.ScanFolder();
            rx.SetDefaultImplementations();

            // Run Application
            Application.Run(new Form1());
        }

        /// <summary>
        /// This method is called when the application encounters an unhandled exception (hopefully never)
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Unhandled Exception Event Arguments</param>
        internal static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Fatal(e.ExceptionObject.ToString());
        }
    }
}
