using System;
using System.IO;
using System.Windows;

namespace Secdev.Workstation
{
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += (s, e) =>
            {
                try
                {
                    string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "crash.log");
                    File.WriteAllText(logPath, e.Exception.ToString());
                }
                catch { }
            };
        }
    }
}
