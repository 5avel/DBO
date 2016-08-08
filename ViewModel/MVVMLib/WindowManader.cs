using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBO.ViewModel.MVVMLib
{
    public class WindowManader
    {
        private static WindowManader instance;

        public delegate void ShowWindowHandler(ViewModelBase vm, string view);
        public event ShowWindowHandler ShowWindowEvent;
        protected virtual void OnShowWindowEvent(ViewModelBase vm, string view)
        {
            ShowWindowHandler handler = ShowWindowEvent;
            if (handler != null)
                handler(vm, view);
        }
        private WindowManader(){}

        public static WindowManader Instance
        {
            get
            {
                lock (typeof(WindowManader))
                {
                    if (instance == null)
                        instance = new WindowManader();
                }
                return instance;
            }
        }
    }
}
