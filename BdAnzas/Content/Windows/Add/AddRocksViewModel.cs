using Anzas.DAL;
using BdAnzas.Base;
using BdAnzas.Commands;
using Egor92.MvvmNavigation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BdAnzas.Content.Windows.Add
{
    internal class AddRocksViewModel : ViewModelBase
    {
        


        private ObservableCollection<InfoDrill> _infoDrills;
        public ObservableCollection<InfoDrill> InfoDrills
        {
            get => _infoDrills;
            set => Set(ref _infoDrills, value);
        }
        private InfoDrill _selectedInfoDrill;
        public InfoDrill SelectedInfoDrill
        {
            get => _selectedInfoDrill;

            set
            {
                _selectedInfoDrill = value;
                OnPropertyChanged("SelectedInfoDrill");
                OnPropertyChanged("SubmitEnabled");
            }
        }


        /// <summary>
        /// От
        /// </summary>
        public double From { get => _from; set => SetProperty(ref _from, value); }
        private double _from;
        /// <summary>
        /// До
        /// </summary>
        public double? To { get => _to; set => SetProperty(ref _to, value); }
        private double? _to;

        /// <summary>
        /// Длина интервала
        /// </summary>
        public double? Length { get => _length; set => SetProperty(ref _length, value); }

        private double? _length;
        /// <summary>
        /// Выход керна, м
        /// </summary>
        public double? Kernm { get => _kernm; set => SetProperty(ref _kernm, value); }
        private double? _kernm;
        /// <summary>
        /// Выход керна, %
        /// </summary>
        public double? Kernpc
        {
            get => _kernpc; set => SetProperty(ref _kernpc, value);
        }
        private double? _kernpc;
        /// <summary>
        /// Код породы
        /// </summary>
        public ObservableCollection<RockCode> RockCodes { get => _rockCode; set => Set(ref _rockCode, value); }
        private ObservableCollection<RockCode> _rockCode;

        private RockCode _selectedRockCode;
        public RockCode SelectedRockCode
        {
            get => _selectedRockCode;
            set
            {
                _selectedRockCode = value;
                OnPropertyChanged("SelectedRockCode");
                OnPropertyChanged("SubmitEnabled");
            }
        }

        /// <summary>
        /// Описание керна
        /// </summary>
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
                OnPropertyChanged("SubmitEnabled");
            }
        }
        private string _description;
        /// <summary>
        /// Геолог
        /// </summary>
        public ObservableCollection<Person> Persons { get => _persons; set => Set(ref _persons, value); }
        private ObservableCollection<Person> _persons = new ObservableCollection<Person>();

        /// <summary>
        /// Выбранный элемент Геолог
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
        /// Описание шлифов
        /// </summary>
        public string? Petro { get => _petro; set => Set(ref _petro, value); }
        private string? _petro;
        /// <summary>
        /// Описание аншлифов
        /// </summary>
        public string? Mineral { get => _mineral; set => Set(ref _mineral, value); }
        private string? _mineral;
        /// <summary>
        /// Примечания
        /// </summary>
        public string? NotesCommentsText { get => _notesCommentsText; set => Set(ref _notesCommentsText, value); }
        private string? _notesCommentsText;


        public AddRocksViewModel( )
        {
            RockCodes = new ObservableCollection<RockCode>();
            Persons = new ObservableCollection<Person>();
            InfoDrills = new ObservableCollection<InfoDrill>();
            using (AnzasContext db = new AnzasContext())
            {

                RockCodes = db.RockCodes.AsNoTracking().ToObservableCollection();
                Persons = db.People.AsNoTracking().ToObservableCollection();
                InfoDrills = db.InfoDrills.AsNoTracking().ToObservableCollection();

                //var a = db.InfoDrills.Select(u => u.HoleId).ToObservableCollection();
            }
            SaveCommand = new LamdaCommand(OnSaveCommandExcuted, SaveCommandExecute);
        }


        public AddRocksViewModel(int id)
        {


            RockCodes = new ObservableCollection<RockCode>();
            Persons = new ObservableCollection<Person>();
            InfoDrills = new ObservableCollection<InfoDrill>();

            using (AnzasContext db = new AnzasContext())
            {

                RockCodes = db.RockCodes.AsNoTracking().ToObservableCollection();
                Persons = db.People.AsNoTracking().ToObservableCollection();
                InfoDrills = db.InfoDrills.AsNoTracking().ToObservableCollection();

                //var a = db.InfoDrills.Select(u => u.HoleId).ToObservableCollection();
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
                    Rock rock = new Rock
                    {
                        Uid = db.Rocks.Max(x => x.Uid) + 1,
                        HoleId = SelectedInfoDrill.Uid,
                        Profile = SelectedInfoDrill.Uid,
                        From = From,
                        To = To,
                        Length = Length,
                        Kernm = Kernm,
                        Kernpc = Kernpc,
                        RockCode = SelectedRockCode.Uid,
                        Description = Description,
                        Mineral = Mineral,
                        Petro = Petro,
                        NotesCommentsText = NotesCommentsText,
                        Geolog = SelectedPersons.Uid
                    };

                    var message = "Вы точно хотите сохранить?";
                    var result = MessageBox.Show(message, "Запись в базу данных",
                             MessageBoxButton.YesNo,
                             MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes) ;
                    {
                        db.Rocks.Add(rock);
                        db.SaveChanges();
                        MessageBox.Show("Сохранение прошло успешно");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }


        public bool SubmitEnabled
        {
            get
            {
                if (SelectedPersons is not null
                    && SelectedInfoDrill is not null
                    && SelectedRockCode is not null
                    && !String.IsNullOrEmpty(Description)
                    )
                {
                   
                    return true;
                }
                else return false;
            }
        }


        protected override void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
        {
            base.PropertyNewValue(ref fieldProperty, newValue, propertyName);


            Length = To - From;
            Kernpc = 100 - ((Length - Kernm) / (Length / 100));




        }




    }
}
