//v1
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Mi.Common
{


    /// <summary>
    /// info about the app, also unseful directories
    /// </summary>
    public class ApplicationInfo
    {

        public ApplicationInfo()
        {
            // Environment.CurrentDirectory
        }

        public static bool IsDev { get; set; } = false;
        public static string APP_DEV_NAME = "InfoLense"; //used in creating app data directory and such

        //these fields are to be displayed to the user 
        public static string APP_TITLE { get;  } = "InfoLense";
        public static string APP_SUB_TITLE { get;  } = "-todo-";
        public static string APP_SHORT_DESCRIPTION { get; } = "Cover parts of the screen";
        public static string APP_VERSION_NOTE { get; } = "";

        public static string APP_VERSION { get; } = "0.1.0";
        public static string APP_VERSION_FRIENDLY { get; } = $"{APP_VERSION} " + (IsDev ? " [dev]" : "(29-08-2022)");
        public static string APP_DEVELOPER_NAME { get; set; } = "YassinMi";
        public static string APP_GUI_DESIGNER_NAME { get; set; } = "YassinMi";
        //public static string APP_GITHUB_URL { get; set; } = "https://github.com/yassinMi";
        //public static string APP_DEVELOPER_EMAIL { get; set; } = "DIR16CAT17@gmail.com";
    
        /// <summary>
        /// the absolute path where the exe lives
        /// </summary>
        public static readonly string MAIN_PATH = Path.GetDirectoryName(
           System.Reflection.Assembly.GetExecutingAssembly().Location);

        public static readonly string APP_DATA = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Mi\\"+APP_DEV_NAME;
        public static readonly string APP_CONFIG_FILE = APP_DATA + "\\" + APP_DEV_NAME + ".config.xml";
        public static readonly string USER_SESSIONS_DIR = APP_DATA + "\\" + "session-data";
        internal static readonly string ERRORS_LOG_FILE = APP_DATA + @"\Errors.log";
        /// <summary>
        /// under MyDocuments/MiScraper/Plugins
        /// </summary>
        public static string PLUGINS_GLOBAL_FOLDER_AT_MY_DOCUMENTS = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+ @"\MiScraper\Plugins");
        public static string PLUGINS_GLOBAL_FOLDER_AT_INSTLLATION = MAIN_PATH + @"\Plugins";

        internal static void OnAppStartup()
        {
            if (Directory.Exists(APP_DATA) == false)
            {
                Directory.CreateDirectory(APP_DATA);
            }
        }
    }





}
