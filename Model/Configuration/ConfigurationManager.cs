
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace DBO.Model.Configuration
{
    class ConfigurationManager
    {
        private static ConfigurationManager configManager;

        private ConfigurationManager()
        {
           // license l = new license();
           // SettingsProvider.Getlicense(ref l);

            //if (!Test(l)) { MessageBox.Show("Ошибка лицензии!"); App.Current.MainWindow.Close(); }
        }

        public static ConfigurationManager GetInstance()
        {
            // для исключения возможности создания двух объектов 
            // при многопоточном приложении
            if (configManager == null)
            {
                lock (typeof(ConfigurationManager))
                {
                    if (configManager == null)
                        configManager = new ConfigurationManager();
                }
            }

            return configManager;
        }


        #region Методы для проверки лицензий
        private static bool Test(license l)
        {
            if (GetHashString2(GetHashString()) == l.key) return true;
            else return false;
        }

        private static string GetHashString()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Product, SerialNumber, Manufacturer FROM Win32_BaseBoard");

            ManagementObjectCollection information = searcher.Get();

            string s = "";
            foreach (ManagementObject obj in information)
            {
                foreach (PropertyData data in obj.Properties)
                {
                    //Console.WriteLine(string.Format("{0} = {1}", data.Name, data.Value));
                    s += data.Value;
                }

            }
            s += "omyr;@#$";
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }

        private static string GetHashString2(string s)
        {
            s += "ubrwk~!@";
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }

        #endregion
    }
}
