using Anzas.DAL;
using BdAnzas.Base;
using BdAnzas.Commands;
using BdAnzas.Content;
using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BdAnzas
{
    internal class MainViewModel: ViewModelBase , INavigatedToAware
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

            
            _navigationmaneger.Register<Info_Dril_View>("Info_Dril", () => new Info_DrilModel(_navigationmaneger , dbcontext));
            _navigationmaneger.Register<Info_TrenchView>("Info_Trench", () => new Info_TrenchModel());
            _navigationmaneger.Register<RocksView>("Rocks", () => new RocksViewModel(_navigationmaneger));


            _navigationmaneger.Navigate("Info_Dril");




            GoInfoTrenchCommand = new LamdaCommand(OnGoInfoTrenchCommandCommandExcuted, CanGoInfoTrenchCommandExecute);
            GoInfo_DrilCommand = new LamdaCommand(OnInfo_DrilCommandCommandExcuted, CanInfo_DrilCommandExecute);
            GoRockCommand = new LamdaCommand(OnGoRockCommandExcuted, CanGoRockCommandExecute);

        }

        #region Info_Trench
        public ICommand GoInfoTrenchCommand { get; }
        private bool CanGoInfoTrenchCommandExecute(object p) => true;
        private void OnGoInfoTrenchCommandCommandExcuted(object p)
        {
            _navigationmaneger.Navigate("Info_Trench");
        }
        #endregion

        #region Info_Dril
        public ICommand GoInfo_DrilCommand { get; }
        private bool CanInfo_DrilCommandExecute(object p) => true;
        private void OnInfo_DrilCommandCommandExcuted(object p)
        {
            _navigationmaneger.Navigate("Info_Dril");
        }
        #endregion


        #region Rock
        public ICommand GoRockCommand { get; }
        private bool CanGoRockCommandExecute(object p) => true;
        private void OnGoRockCommandExcuted(object p)
        {
            _navigationmaneger.Navigate("Rocks");
        }
        #endregion

        public void OnNavigatedTo(object arg)
        {
            if (!(arg is int))
                return;

            PassedParameter = (int)arg; ;
        }
    }
}
