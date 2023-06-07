using BdAnzas.Base;
using BdAnzas.Commands;
using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BdAnzas.Content.ViewModel
{
    internal class Info_TrenchModel : ViewModelBase
    {
        private NavigationManager navigationmaneger;

        public Info_TrenchModel(NavigationManager navigationmaneger)
        {
            this.navigationmaneger = navigationmaneger;

        }

    }
}
