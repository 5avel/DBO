using System.Threading.Tasks;
using System.Windows.Input;

namespace DBO.ViewModel.MVVMLib
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}