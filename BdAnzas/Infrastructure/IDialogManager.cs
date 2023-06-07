using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BdAnzas.Infrastructure
{
    public interface IDialogManager
    {

        void ShowMessage(string message, string titlemes , MessageBoxButton button , MessageBoxImage image);
    }
}
