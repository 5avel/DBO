using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBO.ViewModel.MVVMLib
{
    class Massanger
    {
        private static Massanger instance;
        private static object syncRoot = new Object();

        private Massanger() {  }

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
    }
}
