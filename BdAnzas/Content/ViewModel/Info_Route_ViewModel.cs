using Anzas.DAL;
using Anzas.DAL.Services;
using BdAnzas.Base;
using BdAnzas.Commands;
using BdAnzas.Constants;
using BdAnzas.Content.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BdAnzas.Content.ViewModel
{
    internal  class Info_Route_ViewModel:ViewModelBase
    {
        private InfoRouteRepository _infoRouteRepository;


        #region Свойства

        /// <summary>
        /// Информация по маршрутам
        /// </summary>
        private ObservableCollection<InfoRoute> _routes;
        public ObservableCollection<InfoRoute> Routes
        {
            get => _routes;
            set => Set(ref _routes, value);
        }

        /// <summary>
        /// Выделенный элемент инф. по маршрутам
        /// </summary>
        private InfoRoute _selectedRoute;
        public  InfoRoute SelectedRoute
        {
            get => _selectedRoute;
            set => Set(ref _selectedRoute, value);
        }
        


        #endregion


        public Info_Route_ViewModel()
        {
            AnzasContext anzasContext = new AnzasContext();
            _infoRouteRepository = new InfoRouteRepository(anzasContext);
            Routes = new ObservableCollection<InfoRoute>(_infoRouteRepository.GetAll());


        }

        #region Команды


        private ICommand _editItemCommand;
        public ICommand EditItemCommand
        {
            get
            {
                if (_editItemCommand == null)
                {
                    _editItemCommand = new RelayCommand(param => OpenWindowEdit());
                }
                return _editItemCommand;
            }
        }


        private ICommand _addItemCommand;
        public ICommand AddItemCommand
        {
            get
            {
                if (_addItemCommand == null)
                {

                    _addItemCommand = new RelayCommand(param => OpenWindowAdd());
                }
                return _addItemCommand;
            }
        }


        #endregion

        /// <summary>
        /// Метод для редактирования
        /// </summary>
        private void OpenWindowEdit()
        {
            AddWindow window = new AddWindow();
            if (SelectedRoute != null)
            {
                window.DataContext = new AddEditWindowViewModel(NavigationKeys.InfoRoutehKey, SelectedRoute.Uid);
                if (window.ShowDialog() == false)
                {
                    Routes.Clear();
                    Routes = new ObservableCollection<InfoRoute>(_infoRouteRepository.GetAll());
                }
            }

        }
        /// <summary>
        /// Метод для добавления нового элемента
        /// </summary>
        private void OpenWindowAdd()
        {
            AddWindow window = new AddWindow();

            window.DataContext = new AddEditWindowViewModel(NavigationKeys.InfoRoutehKey);
            if (window.ShowDialog() == false)
            {
                Routes.Clear();
                Routes = new ObservableCollection<InfoRoute>(_infoRouteRepository.GetAll());
            }

        }

    }
}
