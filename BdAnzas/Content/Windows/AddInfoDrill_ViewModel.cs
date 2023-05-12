using Anzas.DAL;
using BdAnzas.Base;
using BdAnzas.Commands;
using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BdAnzas.Content.Windows
{
    internal class AddInfoDrill_ViewModel : ViewModelBase, INavigatedToAware
    {

        private NavigationManager navigationmaneger;


        #region Свойства


        /// <summary>
        /// № скважины
        /// </summary>
        public string HoleId
        {
            get => _holeId;
            set
            {
                _holeId = value;
                OnPropertyChanged("HoleId");
                OnPropertyChanged("SubmitEnabled");
            }
        }
        private string? _holeId;

        /// <summary>
        /// Тип выработки
        /// </summary>
        public ObservableCollection<Mine> Mines { get => _mines; set => Set(ref _mines, value); }
        private ObservableCollection<Mine> _mines = new ObservableCollection<Mine>();


        /// <summary>
        /// Выбранный элемент Типа выработки
        /// </summary>
        public Mine SelectedMines
        {
            get => _selectedMines;
            set
            {
                _selectedMines = value;
                OnPropertyChanged("SelectedMines");
                OnPropertyChanged("SubmitEnabled");
            }
        }
        private Mine _selectedMines;

        /// <summary>
        /// Название участка
        /// </summary>
        public ObservableCollection<Place> Places { get => _places; set => Set(ref _places, value); }
        private ObservableCollection<Place> _places = new ObservableCollection<Place>();

        /// <summary>
        /// Выбранный элемент Название участка
        /// </summary>
        public Place SelectedPlaces
        {
            get => _selectedPlaces;
            set
            {
                _selectedPlaces = value;
                OnPropertyChanged("SelectedPlaces");
                OnPropertyChanged("SubmitEnabled");
            }
        }
        private Place _selectedPlaces;


        /// <summary>
        /// Номер ПЛ
        /// </summary>
        public double Profile { get => _profile; set => Set(ref _profile, value); }
        private double _profile;

        /// <summary>
        /// Долгота
        /// </summary>
        public double Easting
        {
            get => _easting;
            set
            {
                _easting = value;
                OnPropertyChanged("Easting");
                OnPropertyChanged("SubmitEnabled");
            }
        }
        private double _easting;

        /// <summary>
        /// Широта
        /// </summary>
        public double Northing
        {
            get => _northing;
            set
            {
                _northing = value;
                OnPropertyChanged("Northing");
                OnPropertyChanged("SubmitEnabled");
            }
        }
        private double _northing;

        /// <summary>
        /// Абс. отм.
        /// </summary>
        public double Elevation { get => _elevation; set => SetProperty(ref _elevation, value); }
        private double _elevation;

        /// <summary>
        /// Диаметр бурения, мм
        /// </summary>
        public double Diam
        {
            get => _diam;
            set
            {
                _diam = value;
                OnPropertyChanged("Diam");
                OnPropertyChanged("SubmitEnabled");
            }
        }
        private double _diam;

        /// <summary>
        /// Азимут ист., °
        /// </summary>
        public double Azimuth { get => _azimuth; set => Set(ref _azimuth, value); }
        private double _azimuth;

        /// <summary>
        /// Угол наклона от горизонта, °
        /// </summary>
        public double Dip { get => _dip; set => Set(ref _dip, value); }
        private double _dip;

        /// <summary>
        /// Глубина скважины,м
        /// </summary>
        public double Depth
        {
            get => _depth;

            set
            {
                _depth = value;
                OnPropertyChanged("Depth");
                OnPropertyChanged("SubmitEnabled");
            }
        }
        private double _depth;

        /// <summary>
        /// Уровень ПВ, м
        /// </summary>
        public double Uroven { get => _uroven; set => SetProperty(ref _uroven, value); }
        private double _uroven;

        /// <summary>
        /// Абс. отм. уровня, м
        /// 
        /// </summary>
        public double UrAbs
        {
            get => _urAbs;
            set => SetProperty(ref _urAbs, value);
        }
        private double _urAbs;

        /// <summary>
        /// Начало бурения
        /// </summary>
        public DateTime StartDate { get => _startDate; set => Set(ref _startDate, value); }
        private DateTime _startDate;

        /// <summary>
        /// Окончание бурения
        /// </summary>
        public DateTime EndDate { get => _endDate; set => Set(ref _endDate, value); }
        private DateTime _endDate;

        /// <summary>
        /// Геолог
        /// </summary>
        public ObservableCollection<Person> Persons { get => _persons; set => Set(ref _persons, value); }
        private ObservableCollection<Person> _persons = new ObservableCollection<Person>();

        /// <summary>
        /// Выбранный элемент Название участка
        /// </summary>
        public Person SelectedPersons
        {
            get => _selectedPersons;
            set
            {
                _selectedPersons = value;
                OnPropertyChanged("SelectedPersons");
                OnPropertyChanged("SubmitEnabled");
            }
        }
        private Person _selectedPersons;

        /// <summary>
        /// Примечания
        /// </summary>
        public string NotesCommentsText { get => _notesCommentsText; set => Set(ref _notesCommentsText, value); }
        private string _notesCommentsText;

        public bool SubmitEnabled
        {
            get
            {
                if (
                    SelectedMines is not null
                    && SelectedPersons is not null
                    && SelectedPlaces is not null
                    && !String.IsNullOrEmpty(HoleId)
                    && Easting != 0
                    && Northing != 0
                    && Diam != 0
                    && Depth != 0
                    )
                {

                    return true;
                }
                else return false;
            }
        }


        #endregion


        public AddInfoDrill_ViewModel(NavigationManager navigationmaneger)
        {
            this.navigationmaneger = navigationmaneger;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;

            using (AnzasContext db = new AnzasContext())
            {
                Persons = db.People.AsNoTracking().ToObservableCollection();
                Mines = db.Mines.AsNoTracking().ToObservableCollection();
                Places = db.Places.AsNoTracking().ToObservableCollection();
            }
            SaveCommand = new LamdaCommand(OnSaveCommandExcuted, SaveCommandExecute);

        }

        public ICommand SaveCommand { get; }
        private bool SaveCommandExecute(object p) => true;
        private void OnSaveCommandExcuted(object p)
        {
            try
            {
                using (AnzasContext db = new AnzasContext())
                {
                    InfoDrill infoDrill = new InfoDrill
                    {
                        Uid = db.InfoDrills.Max(x => x.Uid) + 1,
                        HoleId = HoleId,
                        TypeLcode = SelectedMines.Uid,
                        PlaceSite = SelectedPlaces.Uid,
                        Profile = Profile,
                        Easting = Easting,
                        Northing = Northing,
                        Elevation = Elevation,
                        Diam = Diam,
                        Azimuth = Azimuth,
                        Dip = Dip,
                        Depth = Depth,
                        Uroven = Uroven,
                        UrAbs = UrAbs,
                        StartDate = new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day),
                        EndDate = new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day),
                        Geolog = SelectedPersons.Uid,
                        NotesCommentsText = NotesCommentsText,
                        Project = 0
                    };

                    var message = "Вы точно хотите сохранить?";
                    var result = MessageBox.Show(message, "Запись в базу данных",
                             MessageBoxButton.YesNo,
                             MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes) ;
                    {
                        db.InfoDrills.Add(infoDrill);
                        db.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }

        public void OnNavigatedTo(object arg)
        {

        }

        // Получаем сразу значение абс отм уровня
        protected override void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
        {
            base.PropertyNewValue(ref fieldProperty, newValue, propertyName);


            UrAbs = Elevation - Uroven;

        }
    }
}
