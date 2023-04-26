using BdAnzas.Base;
using Egor92.MvvmNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdAnzas.Content
{
    internal class Info_DrilModel: ViewModelBase
    {
        private NavigationManager navigationManager;

        public Info_DrilModel()
        {
        }

        public Info_DrilModel(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
        }
    }
}
