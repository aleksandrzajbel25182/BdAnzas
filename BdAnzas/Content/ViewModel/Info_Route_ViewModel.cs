using Anzas.DAL;
using Anzas.DAL.Services;
using BdAnzas.Base;
using BdAnzas.Commands;
using BdAnzas.Constants;
using BdAnzas.Content.Windows;
using BdAnzas.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BdAnzas.Content.ViewModel
{
    internal class Info_Route_ViewModel : ViewModelBase
    {
        private InfoRouteRepository _infoRouteRepository;
        private SearchManager searchManager;

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
        public InfoRoute SelectedRoute
        {
            get => _selectedRoute;
            set => Set(ref _selectedRoute, value);
        }

        #region фильтр свойства
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { Set(ref _searchText, value); }
        }

        public ObservableCollection<GroupColumn> _columnNames;
        public ObservableCollection<GroupColumn> ColumnNames
        {
            get => _columnNames;
            set => Set(ref _columnNames, value);
        }
        public GroupColumn _selectedColumn;
        public GroupColumn SelectedColumn
        {
            get => _selectedColumn;
            set => Set(ref _selectedColumn, value);
        }
        #endregion

        #endregion


        public Info_Route_ViewModel()
        {
            searchManager = new SearchManager();

            ColumnNames = new ObservableCollection<GroupColumn>()
            {
                     new GroupColumn{ Id = "Uid", NameColumn = "ID" },
                     new GroupColumn{ Id= "HoleId", NameColumn = "№ Маршрута"},
                     new GroupColumn{ Id = "TypeLcode", NameColumn = "Тип выработки" },
                     new GroupColumn{ Id= "PlaceSite", NameColumn = "Название участка"},
                     new GroupColumn{ Id = "Easting1", NameColumn = "Долгота (начало)" },
                     new GroupColumn{ Id= "Northing1", NameColumn = "Широта (начало)"},
                     new GroupColumn{ Id = "Elevation1", NameColumn = "Абс. отм. (начало)" },
                     new GroupColumn{ Id= "Easting2", NameColumn = "Долгота (конец)"},
                     new GroupColumn{ Id = "Northing2", NameColumn = "Широта (конец)" },
                     new GroupColumn{ Id= "Elevation2", NameColumn = "Абс. отм. (конец)"},
                     new GroupColumn{ Id= "Length", NameColumn = "Длина маршрута"},
                     new GroupColumn{ Id= "Date", NameColumn = "Дата"},                    
                     new GroupColumn{ Id= "Geolog", NameColumn = "Геолог"},
                     new GroupColumn{ Id= "NotesCommentsText", NameColumn = "Примечания"},
            };

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


        private void Refrash()
        {
            Routes.Clear();
            Routes = new ObservableCollection<InfoRoute>(_infoRouteRepository.GetAll());
        }

        private ICommand _refrashCommand;
        public ICommand RefrashCommand
        {
            get
            {
                if (_refrashCommand == null)
                {
                    _refrashCommand = new RelayCommand(param => Refrash());
                }
                return _refrashCommand;
            }
        }
        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(param => Routes = searchManager.SearchFilter(Routes, SearchText, SelectedColumn.Id));
                }
                return _searchCommand;
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
