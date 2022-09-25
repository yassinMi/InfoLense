using Mi.Common;
using Mi.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace InfoLens
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private const string AppName = "InfoLens-10d2e625ce7b4e98079d4597";
        private const string UniqueEventName = "InfoLens-2e0189630cf3914522e87657";
        private EventWaitHandle eventWaitHandle;

        private Mutex _mutex;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {

            ApplicationInfo.OnAppStartup();
           

            bool isOnwned;

            this._mutex = new Mutex(true, AppName, out isOnwned);
            this.eventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, UniqueEventName);
            GC.KeepAlive(this._mutex);

            if (isOnwned)
            {
                var thread = new Thread(() => {
                    while (this.eventWaitHandle.WaitOne())
                    {
                        Current.Dispatcher.BeginInvoke((Action)(() => {
                            ShowMainWindow();
                        }));
                    }
                });
                thread.IsBackground = true;
                thread.Start();
                ShowMainWindow();

                //WatchingService.Instance.RunWorkerAsync();



                return;

            }

            this.eventWaitHandle.Set();
            this.Shutdown();





        }


        public static Window FindOpenWindowByType(Type targetType)
        {
            foreach (var item in Application.Current.Windows)
            {
                if (item.GetType() == targetType)
                {
                    return item as Window;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ShowMainWindow()
        {
            MainWindow maybeMainWindowRef = (MainWindow)FindOpenWindowByType(typeof(MainWindow));
            if (maybeMainWindowRef != null)
            {
                if (maybeMainWindowRef.WindowState == System.Windows.WindowState.Minimized)
                    maybeMainWindowRef.WindowState = System.Windows.WindowState.Normal;
                maybeMainWindowRef.Activate();
                maybeMainWindowRef.Topmost = true;
                maybeMainWindowRef.Topmost = false;
                maybeMainWindowRef.Focus();
            }
            else
            {
                MainWindow newMainWindow = new MainWindow();
                Application.Current.MainWindow = newMainWindow;
                newMainWindow.Show();
            }

        }


    }
}
