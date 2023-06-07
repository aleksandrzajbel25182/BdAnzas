using Anzas.DAL;
using BdAnzas.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BdAnzas.Content.ViewModel
{
    internal class AddEditInfoTrench_ViewModel : ViewModelBase
    {

        // <summary>
        /// Редактируем или нет?
        /// </summary>
        private bool _ediFlag;
        /// <summary>
        /// Храним id для обновления
        /// </summary>
        private int _id;

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
        /// Конструктор при котором будет происходить добавления в базу 
        /// </summary>
        public AddEditInfoTrench_ViewModel()
        {
            _ediFlag = false;
            //StartDate = DateTime.Now;
            //EndDate = DateTime.Now;

            using (AnzasContext db = new AnzasContext())
            {
                Persons = db.People.AsNoTracking().ToObservableCollection();
                Mines = db.Mines.AsNoTracking().ToObservableCollection();
                Places = db.Places.AsNoTracking().ToObservableCollection();
            }
            //SaveCommand = new LamdaCommand(OnSaveCommandExcuted, SaveCommandExecute);

        }

        /// <summary>
        /// Конструктор при котором будет происходить обновления с базой 
        /// </summary>
        public AddEditInfoTrench_ViewModel(int id)
        {
            _ediFlag = true;
            //StartDate = DateTime.Now;
            //EndDate = DateTime.Now;

            using (AnzasContext db = new AnzasContext())
            {

                var list = db.InfoTrenches.FirstOrDefault(i => i.Uid == id);



                Persons = db.People.AsNoTracking().ToObservableCollection();
                Mines = db.Mines.AsNoTracking().ToObservableCollection();
                Places = db.Places.AsNoTracking().ToObservableCollection();

                SelectedPersons = Persons[list.Geolog - 1];
                SelectedMines = Mines[(int)list.TypeLcode - 1];
                SelectedPlaces = Places[(int)list.PlaceSite - 1];

            }


            //SaveCommand = new LamdaCommand(OnSaveCommandExcuted, SaveCommandExecute);

        }



        /// <summary>
        /// ФУнкция обновления данных 
        /// </summary>
        /// <returns></returns>
        private bool UpdataInfodrill()
        {
            try
            {
                using (AnzasContext db = new AnzasContext())
                {
                    //InfoDrill infoDrill = new InfoDrill
                    //{
                    //    Uid = _id,
                    //    HoleId = HoleId,
                    //    TypeLcode = SelectedMines.Uid,
                    //    PlaceSite = SelectedPlaces.Uid,
                    //    Profile = Profile,
                    //    Easting = Easting,
                    //    Northing = Northing,
                    //    Elevation = Elevation,
                    //    Diam = Diam,
                    //    Azimuth = Azimuth,
                    //    Dip = Dip,
                    //    Depth = Depth,
                    //    Uroven = Uroven,
                    //    UrAbs = UrAbs,
                    //    StartDate = new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day),
                    //    EndDate = new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day),
                    //    Geolog = SelectedPersons.Uid,
                    //    NotesCommentsText = NotesCommentsText
                    //};

                    //var message = "Были внесены измения. Вы хотите их сохранить?";
                    //var result = MessageBox.Show(message, "Изменения",
                    //         MessageBoxButton.YesNo,
                    //         MessageBoxImage.Question);

                    //if (result == MessageBoxResult.Yes)
                    //{
                    //    db.Update(infoDrill);
                    //    db.SaveChanges();
                    //    return true;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            return true;
        }

        /// <summary>
        /// Функция  сохранения 
        /// </summary>
        /// <returns></returns>
        private bool SaveInfodrill()
        {
            try
            {
                //using (AnzasContext db = new AnzasContext())
                //{
                //    InfoDrill infoDrill = new InfoDrill
                //    {
                //        Uid = db.InfoDrills.Max(x => x.Uid) + 1,
                //        HoleId = HoleId,
                //        TypeLcode = SelectedMines.Uid,
                //        PlaceSite = SelectedPlaces.Uid,
                //        Profile = Profile,
                //        Easting = Easting,
                //        Northing = Northing,
                //        Elevation = Elevation,
                //        Diam = Diam,
                //        Azimuth = Azimuth,
                //        Dip = Dip,
                //        Depth = Depth,
                //        Uroven = Uroven,
                //        UrAbs = UrAbs,
                //        StartDate = new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day),
                //        EndDate = new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day),
                //        Geolog = SelectedPersons.Uid,
                //        NotesCommentsText = NotesCommentsText,
                //        Project = 0
                //    };

                //    var message = "Вы точно хотите сохранить?";
                //    var result = MessageBox.Show(message, "Запись в базу данных",
                //             MessageBoxButton.YesNo,
                //             MessageBoxImage.Question);

                //    if (result == MessageBoxResult.Yes) ;
                //    {
                //        db.InfoDrills.Add(infoDrill);
                //        db.SaveChanges();
                //        return true;
                //    }

                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
                throw;
            }
            return true;
        }

    }
}
