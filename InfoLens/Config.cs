using Mi.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Mi.Services
{
    [Serializable]
    /// <summary>
    /// 
    /// singletone
    /// </summary>
    public class Config : INotifyPropertyChanged
    {
        [NonSerialized]
        private static Config _instance;
        public static Config Instance
        {
            get
            {
                if (_instance != null) return _instance;
                else
                {
                    Trace.WriteLine("Config: creating instance");
                    _instance = Config.Load();

                    return _instance;
                }
            }
        }

        [NonSerialized]
        private static Config _factoryInstance;
        public static Config Factory
        {
            get
            {
                if (_factoryInstance == null)
                {
                    _factoryInstance = Config.FactoryConfig();
                }
                return _factoryInstance;
            }
        }

        private Config()
        {


        }






        private Color _CoverColor;
        public Color CoverColor
        {
            set { _CoverColor = value; notif(nameof(CoverColor)); }
            get { return _CoverColor; }
        }


        private int _SplitterThickness;
        public int SplitterThickness
        {
            set { _SplitterThickness = value; notif(nameof(SplitterThickness)); }
            get { return _SplitterThickness; }
        }


        private double _Opacity;
        public double Opacity
        {
            set { _Opacity = value; notif(nameof(Opacity)); }
            get { return _Opacity; }
        }




        private string _HotKey;
        public string HotKey
        {
            set { _HotKey = value; notif(nameof(HotKey)); }
            get { return _HotKey; }
        }



        private int _r0Height;
        public int r0Height
        {
            set { _r0Height = value; notif(nameof(r0Height)); }
            get { return _r0Height; }
        }


        private int _r1Height;
        public int r1Height
        {
            set { _r1Height = value; notif(nameof(r1Height)); }
            get { return _r1Height; }
        }


        private int _c0Width;
        public int c0Width
        {
            set { _c0Width = value; notif(nameof(c0Width)); }
            get { return _c0Width; }
        }


        private int _c1Width;
        public int c1Width
        {
            set { _c1Width = value; notif(nameof(c1Width)); }
            get { return _c1Width; }
        }


        public enum DrawingBrushStyle { Dots=0,Area=1,Lines=2}


        private DrawingBrushStyle _SplittersStyle = DrawingBrushStyle.Area;
        public DrawingBrushStyle SplittersStyle
        {
            set { _SplittersStyle = value; notif(nameof(SplittersStyle)); }
            get { return _SplittersStyle; }
        }


        private DrawingBrushStyle _HandleStyle = DrawingBrushStyle.Dots;
        public DrawingBrushStyle HandleStyle
        {
            set { _HandleStyle = value; notif(nameof(HandleStyle)); }
            get { return _HandleStyle; }
        }









        /// <summary>
        /// ts
        /// </summary>
        /// <param name="saveAS"></param>
        /// <returns></returns>
        public Config Save(string saveAS = null)
        {
            lock (this)
            {
                if (saveAS == null) saveAS = ApplicationInfo.APP_CONFIG_FILE;
                using (var stream = File.Open(saveAS, FileMode.Create))
                {
                    sr.Serialize(stream, this);
                }
                Trace.WriteLine("ConfigsSrvice: saved");
                //MainWindow.ShowMessage("Settings Saved");
                return this;
            }
        }



        /// <summary>
        /// attemps to load the xml config file, if file is missing the factory config is automatically saved and returned
        /// throws unhandled exceptions if file deserialization fails
        /// </summary>
        /// <param name="ConfigFile">if not specified, the MI.APP_CONFIG_FILE_V2 is used  </param>
        /// <returns></returns>
        private static Config Load(string ConfigFile = null)
        {
            if (ConfigFile == null) ConfigFile = ApplicationInfo.APP_CONFIG_FILE;
            if (!File.Exists(ConfigFile))
            {
                Console.WriteLine("No config file, creating from FactoryConfig");
                return Config.FactoryConfig().Save();
            }
            using (var stream = File.OpenRead(ConfigFile))
            {
                return sr.Deserialize(stream) as Config;
            }
        }


        /// <summary>
        /// makes up a new config object, used to reset factory setting when requested by the user or when the xml conf is missing or is corrupt
        /// NOTE: this doesnt change the Instance, but rathr creates and returns a new object
        /// </summary>
        private static Config FactoryConfig()
        {
            var c = new Config();
            c.c0Width = 100;
            c.c1Width = 340;
            c.r0Height = 200;
            c.r1Height = 160;
            c.CoverColor = new Color() { A = 235, R = 24, G = 91, B = 58 };
            c.HotKey = "CTRL+I+L";
            c.Opacity = 92;
            c.SplitterThickness = 8;
            return c;
        }







        static XmlSerializer sr = new XmlSerializer(typeof(Config));

        public event PropertyChangedEventHandler PropertyChanged;
        private void notif(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }



}
