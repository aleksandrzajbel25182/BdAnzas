using BdAnzas.Base;
using Egor92.MvvmNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdAnzas.Content.Windows.Add
{
    internal class AddRocksViewModel : ViewModelBase
    {
        private NavigationManager navigationmaneger;




        public AddRocksViewModel(NavigationManager navigationmaneger)
        {
            this.navigationmaneger = navigationmaneger;
        }
    }
}
