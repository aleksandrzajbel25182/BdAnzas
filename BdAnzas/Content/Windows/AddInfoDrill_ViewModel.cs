using Anzas.DAL;
using BdAnzas.Base;
using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdAnzas.Content.Windows
{
    internal class AddInfoDrill_ViewModel : ViewModelBase, INavigatedToAware
    {




        /// <summary>
        /// № скважины
        /// </summary>
        public string HoleId { get => _holeId;set => Set(ref _holeId, value);}
        private string? _holeId;

        /// <summary>
        /// Тип выработки
        /// </summary>
        public ObservableCollection<Mine> Mines { get => _mines; set => Set(ref _mines, value); }
        private ObservableCollection<Mine> _mines = new ObservableCollection<Mine>();

     
        /// <summary>
        /// Выбранный элемент Типа выработки
        /// </summary>
        public Mine SelectedMines { get => _selectedMines; set => Set(ref _selectedMines ,value); }
        private Mine _selectedMines;
        
        /// <summary>
        /// Название участка
        /// </summary>
        public ObservableCollection<Place> Places { get => _places; set => Set(ref _places, value); }
        private ObservableCollection<Place> _places = new ObservableCollection<Place>();

        /// <summary>
        /// Выбранный элемент Название участка
        /// </summary>
        public Place SelectedPlaces{ get => _selectedPlaces; set => Set(ref _selectedPlaces, value); }
        private Place _selectedPlaces;


        /// <summary>
        /// Номер ПЛ
        /// </summary>
        public double Profile { get => _profile; set => Set(ref _profile, value); }
        private double _profile;

        /// <summary>
        /// Долгота
        /// </summary>
        public double Easting { get => _easting; set => Set(ref _easting, value); }
        private double _easting;

        /// <summary>
        /// Широта
        /// </summary>
        public double Northing { get => _northing; set => Set(ref _northing, value); }
        private double _northing;

        /// <summary>
        /// Абс. отм.
        /// </summary>
        public double Elevation { get => _elevation; set => Set(ref _elevation, value); }
        private double _elevation;

        /// <summary>
        /// Диаметр бурения, мм
        /// </summary>
        public double Diam { get => _diam; set => Set(ref _diam, value); }
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
        public double Depth { get => _depth; set => Set(ref _depth, value); }
        private double _depth;

        /// <summary>
        /// Уровень ПВ, м
        /// </summary>
        public double Uroven { get => _uroven; set => Set(ref _uroven, value); }
        private double _uroven;

        /// <summary>
        /// Абс. отм. уровня, м
        /// 
        /// </summary>
        public double UrAbs { get => _urAbs; set => Set(ref _urAbs, value); }
        private double _urAbs;

        /// <summary>
        /// Начало бурения
        /// </summary>
        public DateOnly StartDate { get => _startDate; set => Set(ref _startDate, value); }
        private DateOnly _startDate;

        /// <summary>
        /// Окончание бурения
        /// </summary>
        public DateOnly EndDate { get => _endDate; set => Set(ref _endDate, value); }
        private DateOnly _endDate;

        /// <summary>
        /// Геолог
        /// </summary>
        public ObservableCollection<Person> Persons { get => _persons; set => Set(ref _persons, value); }
        private ObservableCollection<Person> _persons = new ObservableCollection<Person>();

        /// <summary>
        /// Выбранный элемент Название участка
        /// </summary>
        public Person SelectedPersons { get => _selectedPersons; set => Set(ref _selectedPersons, value); }
        private Person _selectedPersons;

        /// <summary>
        /// Примечания
        /// </summary>
        public string NotesCommentsText { get => _notesCommentsText; set => Set(ref _notesCommentsText, value); }
        private string _notesCommentsText;


        private NavigationManager navigationmaneger;

        public AddInfoDrill_ViewModel(NavigationManager navigationmaneger)
        {
            this.navigationmaneger = navigationmaneger;
            
            using(AnzasContext db = new AnzasContext())
            {
                Persons = db.People.AsNoTracking().ToObservableCollection();
                Mines = db.Mines.AsNoTracking().ToObservableCollection();
                Places = db.Places.AsNoTracking().ToObservableCollection();
            }


        }

        public void OnNavigatedTo(object arg)
        {

        }
    }
}
