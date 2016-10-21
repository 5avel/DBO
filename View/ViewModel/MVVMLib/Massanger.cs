using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBO.ViewModel.MVVMLib
{
    class Massanger
    {
        private static Massanger instance; // Объект класса Massanger
        private static object syncRoot = new Object(); // Объект для синхронизации доступа из разных потоков
      
        public delegate void AccountStateHandler(string message);// Объявляем делегат
        public event AccountStateHandler MessageReceivedEvent; // Событие при получении сообщения

        private Massanger() { } // Закрытый конструктор класса Massanger

        /// <summary>
        /// Возвращает объект класса Massanger
        /// </summary>
        /// <returns>Massanger Instance</returns>
        public static Massanger getInstance() 
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new Massanger();
                }
            }
            return instance;
        }

        public void SendMessage(string message)
        {
            MessageReceivedEvent?.Invoke(message);
        }
    }
}
