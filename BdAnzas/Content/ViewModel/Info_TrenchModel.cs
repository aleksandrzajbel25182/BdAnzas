using Anzas.DAL;
using Anzas.DAL.Services;
using BdAnzas.Base;
using BdAnzas.Commands;
using BdAnzas.Constants;
using BdAnzas.Content.Windows;
using BdAnzas.Infrastructure;
using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BdAnzas.Content.ViewModel
{
    internal class Info_TrenchModel : ViewModelBase
    {
        private readonly InfoTrenchRepository _infoTrenchRepository;
        private SearchManager searchManager;
        private ObservableCollection<InfoTrench> _infoTrench;
        /// <summary>
        /// Информаиция по Канавам
        /// </summary>
        public ObservableCollection<InfoTrench> InfoTrench
        {
            get => _infoTrench;
            set => Set(ref _infoTrench, value);

        }

        private InfoTrench _selectedInfoTrench;
        public InfoTrench SelectedInfoTrench
        {
            get => _selectedInfoTrench;
            set => Set(ref _selectedInfoTrench, value);

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


        public Info_TrenchModel()
        {
            AnzasContext anzasContext = new AnzasContext();
            _infoTrenchRepository = new InfoTrenchRepository(anzasContext);
            InfoTrench = new ObservableCollection<InfoTrench>(_infoTrenchRepository.GetAll());

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

        }

        private void Refrash()
        {
            InfoTrench.Clear();
            InfoTrench = new ObservableCollection<InfoTrench>(_infoTrenchRepository.GetAll());
        }
        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(param => InfoTrench = searchManager.SearchFilter(InfoTrench, SearchText, SelectedColumn.Id));
                }
                return _searchCommand;
            }
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

        /// <summary>
        /// Метод для редактирования
        /// </summary>
        private void OpenWindowEdit()
        {
            AddWindow window = new AddWindow();
            if (SelectedInfoTrench != null)
            {
                window.DataContext = new AddEditWindowViewModel(NavigationKeys.InfoTrenchKey, SelectedInfoTrench.Uid);
                if (window.ShowDialog() == false)
                {
                    InfoTrench.Clear();
                    InfoTrench = new ObservableCollection<InfoTrench>(_infoTrenchRepository.GetAll());
                }
            }

        }
                /// <summary>
        /// Метод для добавления нового элемента
        /// </summary>
        private void OpenWindowAdd()
        {
            AddWindow window = new AddWindow();

            window.DataContext = new AddEditWindowViewModel(NavigationKeys.InfoTrenchKey);
            if (window.ShowDialog() == false)
            {
                InfoTrench.Clear();
                InfoTrench = new ObservableCollection<InfoTrench>(_infoTrenchRepository.GetAll());
            }

        }        

    }
}
