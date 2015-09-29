//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="Program.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner
{
    using System;
    using System.Windows.Forms;

    using EveOnlineApi;
    using EveOnlineApi.Interfaces;
    using EveOnlineApi.Interfaces.Xml;

    using EveScanner.Core;
    using EveScanner.Interfaces;
    using EveScanner.IoC;    
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

            // Configure XML API Injections
            Injector.Register<IAllianceXmlDataProvider>(typeof(FileBackedEveOnlineXmlApi));
            Injector.Register<ICharacterXmlDataProvider>(typeof(FileBackedEveOnlineXmlApi));
            Injector.Register<ICorporationXmlDataProvider>(typeof(FileBackedEveOnlineXmlApi));

            // Configure other API Entity Injections
            Injector.Register<IAllianceDataProvider>(typeof(XmlBackedEveOnlineApi));
            Injector.Register<ICharacterDataProvider>(typeof(XmlBackedEveOnlineApi));
            Injector.Register<ICorporationDataProvider>(typeof(XmlBackedEveOnlineApi));
            
            Injector.Register<IImageDataProvider>(typeof(FileBackedImageDataProvider));

            // Configure EveScanner Injections
            Injector.Register<IAppraisalService>(typeof(Evepraisal));
            Injector.Register<IScanHistory>(typeof(ListScanHistory));

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
