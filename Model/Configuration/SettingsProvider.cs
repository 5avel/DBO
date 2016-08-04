using System;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace Model.Configuration
{
    public struct Settings
    {
        public bool isEseyProtocol;
        public string port;
        public string user;
        public string pwd;
        public string licKey;
    }

    public struct license
    {
        public string key;
    }

    class SettingsProvider
    {

        // Лишаем возможности создавать объект этого класса
        private SettingsProvider() { }

        public static void SaveSettings(object s)
        {
            XmlSerializer myXmlSer = new XmlSerializer(s.GetType());
            StreamWriter myWriter = new StreamWriter(Environment.CurrentDirectory + @"\settings.cfg");
            myXmlSer.Serialize(myWriter, s);
            myWriter.Close();
        }

        public static void GetSettings(ref Settings s)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "settings.cfg"))
            {
                XmlSerializer myXmlSer = new XmlSerializer(typeof(Settings));
                FileStream mySet = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "settings.cfg", FileMode.Open);
                s = (Settings)myXmlSer.Deserialize(mySet);
                mySet.Close();
            }
            else
            {
                MessageBox.Show("Файл: " + AppDomain.CurrentDomain.BaseDirectory + "settings.cfg" + " Необнаружен!");
            }
        }

        public static void Getlicense(ref license l)
        {


            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "lic.cfg"))
            {
                XmlSerializer myXmlSer = new XmlSerializer(typeof(license));
                FileStream mySet = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "lic.cfg", FileMode.Open);
                l = (license)myXmlSer.Deserialize(mySet);
                mySet.Close();
            }
            else
            {
                MessageBox.Show("Файл: " + AppDomain.CurrentDomain.BaseDirectory + "lic.cfg Необнаружен!");
            }
        }
    }
}
