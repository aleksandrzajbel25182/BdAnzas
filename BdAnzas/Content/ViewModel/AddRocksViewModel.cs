using BdAnzas.Base;
using Egor92.MvvmNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdAnzas.Content.ViewModel
{
    internal class AddRocksViewModel : ViewModelBase
    {
        private int id;

        public AddRocksViewModel()
        {

        }

        public AddRocksViewModel(int id)
        {
            this.id = id;
        }
    }
}
