using Anzas.DAL;
using Anzas.DAL.Services;
using BdAnzas.Base;
using BdAnzas.Commands;
using BdAnzas.Constants;
using BdAnzas.Content.Windows;
using BdAnzas.Infrastructure;
using Egor92.MvvmNavigation;
using MathCore.WPF.Converters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace BdAnzas.Content.ViewModel
{
    internal class Info_DrilModel : ViewModelBase
    {
        private readonly InfodrillRepository _infodrillRepository;
        private AnzasContext db;
        private SearchManager searchManager;
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { Set(ref _searchText, value); }
        }



        private ObservableCollection<InfoDrill> _infodrill = new ObservableCollection<InfoDrill>();
        public ObservableCollection<InfoDrill> InfoDrills
        {
            get => _infodrill;
            set => SetProperty(ref _infodrill, value);

        }

        /// <summary>
        /// Выбранный элемент скважин
        /// </summary>
        private InfoDrill _selectedInfodril;
        public InfoDrill Selected_InfoDrill
        {
            get => _selectedInfodril;
            set => Set(ref _selectedInfodril, value);
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

        public Info_DrilModel()
        {
        }


        public Info_DrilModel(AnzasContext _db)
        {
            searchManager = new SearchManager();

            ColumnNames = new ObservableCollection<GroupColumn>()
            {
                     new GroupColumn{ Id = "Uid", NameColumn = "ID" },
                     new GroupColumn{ Id= "HoleId", NameColumn = "№ скважины"},
                     new GroupColumn{ Id = "TypeLcode", NameColumn = "Тип выработки" },
                     new GroupColumn{ Id= "PlaceSite", NameColumn = "Название участка"},
                     new GroupColumn{ Id = "Profile", NameColumn = "Номер ПЛ" },
                     new GroupColumn{ Id= "Easting", NameColumn = "Долгота"},
                     new GroupColumn{ Id = "Northing", NameColumn = "Широта" },
                     new GroupColumn{ Id= "Elevation", NameColumn = "Абс. отм."},
                     new GroupColumn{ Id = "Diam", NameColumn = "Диаметр бурения, мм" },
                     new GroupColumn{ Id= "Azimuth", NameColumn = "Азимут ист., °"},
                     new GroupColumn{ Id= "Dip", NameColumn = "Угол наклона от горизонта, °"},
                     new GroupColumn{ Id= "Depth", NameColumn = "Глубина скважины,м"},
                     new GroupColumn{ Id= "Uroven", NameColumn = "Уровень ПВ, м"},
                     new GroupColumn{ Id= "UrAbs", NameColumn = "Абс. отм. уровня, м"},
                     new GroupColumn{ Id= "StartDate", NameColumn = "Начало бурения"},
                     new GroupColumn{ Id= "EndDate", NameColumn = "Окончание бурения"},
                     new GroupColumn{ Id= "Geolog", NameColumn = "Геолог"},
                     new GroupColumn{ Id= "NotesCommentsText", NameColumn = "Примечания"},
            };

            db = _db;
            _infodrillRepository = new InfodrillRepository(db);
            InfoDrills = new ObservableCollection<InfoDrill>(_infodrillRepository.GetAll());
          

        }

       


        private void Refrash()
        {
            InfoDrills.Clear();
            InfoDrills = new ObservableCollection<InfoDrill>(_infodrillRepository.GetAll());
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


        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(param => InfoDrills = searchManager.SearchFilter(InfoDrills, SearchText, SelectedColumn.Id));
                }
                return _searchCommand;
            }
        }

        /// <summary>
        /// Метод для редактирования
        /// </summary>
        private void OpenWindowEdit()
        {
            AddWindow window = new AddWindow();
            if (Selected_InfoDrill != null)
            {
                window.DataContext = new AddEditWindowViewModel(NavigationKeys.InfoDrillKey, Selected_InfoDrill.Uid);
                if (window.ShowDialog() == false)
                {
                    InfoDrills.Clear();
                    InfoDrills = new ObservableCollection<InfoDrill>(_infodrillRepository.GetAll());
                }
            }

        }

        /// <summary>
        /// Метод для добавления нового элемента
        /// </summary>
        private void OpenWindowAdd()
        {
            AddWindow window = new AddWindow();

            window.DataContext = new AddEditWindowViewModel(NavigationKeys.InfoDrillKey);
            if (window.ShowDialog() == false)
            {
                InfoDrills.Clear();
                InfoDrills = new ObservableCollection<InfoDrill>(_infodrillRepository.GetAll());
            }

        }

    }


}

