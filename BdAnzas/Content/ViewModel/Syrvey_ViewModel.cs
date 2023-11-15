using Anzas.DAL;
using Anzas.DAL.Services;
using BdAnzas.Base;
using BdAnzas.Commands;
using BdAnzas.Constants;
using BdAnzas.Content.Windows;
using BdAnzas.Infrastructure;
using MathCore.WPF.Converters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BdAnzas.Content.ViewModel
{
    internal class Syrvey_ViewModel : ViewModelBase
    {
        private SearchManager searchManager;

        #region Свойства

        #region SyrveyDrill
        private ObservableCollection<Survey> _syrveyDrill;
        /// <summary>
        /// Инклинометрия скважин
        /// </summary>
        public ObservableCollection<Survey> SyrveyDrill
        {
            get { return _syrveyDrill; }
            set => Set(ref _syrveyDrill, value);
        }

        private Survey _selectedSyrveyDrill;
        /// <summary>
        /// Выбранная инклинометрия скважин
        /// </summary>
        public Survey SelectedSyrveyDrill
        {
            get { return _selectedSyrveyDrill; }
            set => Set(ref _selectedSyrveyDrill, value);
        }

        #endregion

        #region SyrveyTrench
        private ObservableCollection<SurveyTrench> _syrveyTrench;
        /// <summary>
        /// Инклинометрия скважин
        /// </summary>
        public ObservableCollection<SurveyTrench> SyrveyTrench
        {
            get { return _syrveyTrench; }
            set { _syrveyTrench = value; }
        }

        private SurveyTrench _selectedSyrveyTrenchl;
        /// <summary>
        /// Выбранная инклинометрия скважин
        /// </summary>
        public SurveyTrench SelectedSyrveyTrench
        {
            get { return _selectedSyrveyTrenchl; }
            set { _selectedSyrveyTrenchl = value; }
        }

        #endregion


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
        /// <summary>
        /// Индекс выбранной вкладки
        /// </summary>
        private int _selected;
        public int Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged("Selected");
            }
        }


        public Syrvey_ViewModel()
        {
            searchManager = new SearchManager();

            ColumnNames = new ObservableCollection<GroupColumn>()
            {
                     
                     new GroupColumn{ Id= "HoleId", NameColumn = "№ скважины"},
                     new GroupColumn{ Id= "Depth", NameColumn = "Глубина"},

            };


            _syrveyDrill = new ObservableCollection<Survey>();
            _syrveyTrench = new ObservableCollection<SurveyTrench>();
            using (AnzasContext db = new AnzasContext())
            {
                SyrveyDrill = db.Surveys.Include(h => h.Hole).AsNoTracking().ToObservableCollection();
                SyrveyTrench = db.SurveyTrenches.Include(h => h.Hole).AsNoTracking().ToObservableCollection();
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

        private void Refrash()
        {
            using (AnzasContext db = new AnzasContext())
            {


                if (Selected == 0)
                {
                    SyrveyDrill.Clear();
                    SyrveyDrill = db.Surveys.Include(h => h.Hole).AsNoTracking().ToObservableCollection();
                }
                else
                {
                    SyrveyTrench.Clear();
                    SyrveyTrench = db.SurveyTrenches.Include(h => h.Hole).AsNoTracking().ToObservableCollection();
                }
            }


        }

        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    if (Selected == 0)
                    {
                        
                        _searchCommand = new RelayCommand(param => SyrveyDrill = searchManager.SearchFilter(SyrveyDrill, SearchText, SelectedColumn.Id));
                        return _searchCommand;
                    }
                    else
                    {
                        _searchCommand = new RelayCommand(param => SyrveyTrench = searchManager.SearchFilter(SyrveyTrench, SearchText, SelectedColumn.Id));
                        return _searchCommand;
                    }
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

        /// <summary>
        /// Метод для редактирования
        /// </summary>
        private void OpenWindowEdit()
        {
            AddWindow window = new AddWindow();
            if (Selected == 0)
            {
                if (SelectedSyrveyDrill != null)
                {
                    window.DataContext = new AddEditWindowViewModel(NavigationKeys.SurveyhKey, SelectedSyrveyDrill.Uid);
                    if (window.ShowDialog() == false)
                    {
                        //SyrveyDrill.Clear();
                        //SyrveyDrill = new ObservableCollection<Survey>(_infoRouteRepository.GetAll());
                    }
                }
            }
            else
            {
                if (SelectedSyrveyTrench != null)
                {
                    window.DataContext = new AddEditWindowViewModel(NavigationKeys.InfoSyrveyTrenchKey, SelectedSyrveyTrench.Uid);
                    if (window.ShowDialog() == false)
                    {
                        //SyrveyDrill.Clear();
                        //SyrveyDrill = new ObservableCollection<Survey>(_infoRouteRepository.GetAll());
                    }
                }

            }


        }
        /// <summary>
        /// Метод для добавления нового элемента
        /// </summary>
        private void OpenWindowAdd()
        {
            AddWindow window = new AddWindow();
            if (Selected == 0)
            {
                window.DataContext = new AddEditWindowViewModel(NavigationKeys.SurveyhKey);

                if (window.ShowDialog() == false)
                {
                    //SyrveyDrill.Clear();
                    //SyrveyDrill = new ObservableCollection<Survey>(_infoRouteRepository.GetAll());
                }
            }
            else
            {
                window.DataContext = new AddEditWindowViewModel(NavigationKeys.InfoSyrveyTrenchKey);

                if (window.ShowDialog() == false)
                {
                    //SyrveyDrill.Clear();
                    //SyrveyDrill = new ObservableCollection<Survey>(_infoRouteRepository.GetAll());
                }
            }


        }
    }
}
