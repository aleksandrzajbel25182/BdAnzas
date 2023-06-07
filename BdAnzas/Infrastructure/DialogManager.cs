using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BdAnzas.Infrastructure
{
    public class DialogManager : IDialogManager
    {
      
        public void ShowMessage(string message, string titlemes, MessageBoxButton button, MessageBoxImage image)
        {
            MessageBox.Show(message, titlemes, button, image);
        }

        public MessageBoxResult ShowMessageResult(string message, string titlemes, MessageBoxButton button, MessageBoxImage image)
        {
            var result = MessageBox.Show(message, titlemes, button, image);
            return result;
        }
    }
}
