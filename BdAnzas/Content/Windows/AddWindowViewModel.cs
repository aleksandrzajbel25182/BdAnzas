using BdAnzas.Base;
using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BdAnzas.Content.Windows
{
    internal class AddWindowViewModel : ViewModelBase, INavigatedToAware
    {
        NavigationManager _navigationmaneger;

        /// <summary>
        /// Контрол для отображения страниц
        /// </summary>
        private ContentControl _contentcontrol;

        public ContentControl ContentControl
        {
            get => _contentcontrol;
            set
            {
                _contentcontrol = value;
                OnPropertyChanged("ContentControl");
            }
        }

        public AddWindowViewModel()
        {
            ContentControl = new AddInfoDrill_View();
            //1. Create navigation manager
            _navigationmaneger = new NavigationManager(ContentControl);
            //Регистрируем 
            _navigationmaneger.Register<AddInfoDrill_View>("Add_Dril", () => new AddInfoDrill_ViewModel(_navigationmaneger));
            _navigationmaneger.Navigate("Add_Dril");

        }

        public void OnNavigatedTo(object arg)
        {

        }
    }
}
