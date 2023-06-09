using Anzas.DAL;
using BdAnzas.Base;
using BdAnzas.Commands;
using BdAnzas.Constants;
using BdAnzas.Content;
using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BdAnzas.Content.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {

        AnzasContext dbcontext = new AnzasContext();
        NavigationManager _navigationmaneger;
        /// <summary>
        /// Переменная хранимая данные переданные из другой страницы
        /// </summary>
        private int _passedParameter;
        public int PassedParameter
        {
            get => _passedParameter;
            set => Set(ref _passedParameter, value);
        }

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

        public MainViewModel()
        {
            ContentControl = new Info_Dril_View();
            //1. Create navigation manager
            _navigationmaneger = new NavigationManager(ContentControl);

            _navigationmaneger.Register<Info_Dril_View>(NavigationKeys.InfoDrillKey, () => new Info_DrilModel(dbcontext));
            _navigationmaneger.Register<Info_TrenchView>(NavigationKeys.InfoTrenchKey, () => new Info_TrenchModel());
            _navigationmaneger.Register<RocksView>(NavigationKeys.RocksKey, () => new RocksViewModel());
            _navigationmaneger.Navigate(NavigationKeys.InfoDrillKey);
        }

        private ICommand _navigateCommand;
        public ICommand NavigateCommand
        {
            get
            {
                if (_navigateCommand == null)
                {
                    _navigateCommand = new RelayCommand(param => DoParameterisedCommand(param));
                }
                return _navigateCommand;
            }
        }
        private void DoParameterisedCommand(object parameter)
        {
            _navigationmaneger.Navigate(parameter.ToString());
        }

    }
}
