using Anzas.DAL;
using Anzas.DAL.Services;
using BdAnzas.Base;
using BdAnzas.Commands;
using BdAnzas.Constants;
using BdAnzas.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BdAnzas.Content.ViewModel
{
    internal class AddEditInfoTrench_ViewModel : ViewModelBase
    {
        private readonly InfoTrenchRepository _infoTrenchRepository;
        private DialogManager _dialogManager;
        private bool _editflag; // флаг для понимания редактировать или обновлять данные
     

        #region Свойства

        private InfoTrench _trenchs = new InfoTrench();
        public InfoTrench Trenchs
        {
            get => _trenchs;
            set => Set(ref _trenchs, value);
        }

        /// <summary>
        /// № выработки
        /// </summary>
        private string _holeId;
        public string HoleId { get => _holeId; set=>Set(ref _holeId,value); }      

        /// <summary>
        /// Название участка
        /// </summary>
        public int? PlaceSite { get => _placeSite; set => Set(ref _placeSite, value); }
        private int? _placeSite;
        /// <summary>
        /// Номер ПЛ
        /// </summary>
        public double? Profile { get => _profile; set => Set(ref _profile, value); }
        private double? _profile;
        /// <summary>
        /// Долгота (начало)
        /// </summary>
        public double? Easting1 { get => _easting1; set => Set(ref _easting1, value); }
        private double? _easting1;
        /// <summary>
        /// Широта (начало)
        /// </summary>
        public double? Northing1 { get => _northing1; set => Set(ref _northing1, value); }
        private double? _northing1;
        /// <summary>
        /// Абс. отм. (начало)
        /// </summary>
        public double? Elevation1 { get => _elevation1; set => Set(ref _elevation1, value); }
        private double? _elevation1;
        /// <summary>
        /// Долгота (конец)
        /// </summary>
        public double? Easting2 { get => _easting2; set => Set(ref _easting2, value); }
        private double? _easting2;
        /// <summary>
        /// Широта (конец)
        /// </summary>
        public double? Northing2 { get => _northing2; set => Set(ref _northing2, value); }
        private double? _northing2;
        /// <summary>
        /// Абс. отм. (конец)
        /// </summary>
        public double? Elevation2 { get => _elevation2; set => Set(ref _elevation2, value); }
        private double? _elevation2;
        /// <summary>
        /// Азимут ист., °
        /// </summary>
        public double? Azimuth { get => _azimuth; set => Set(ref _azimuth, value); }
        private double? _azimuth;
        /// <summary>
        /// Длина канавы,м
        /// </summary>
        public double? Length { get => _length; set => Set(ref _length, value); }
        private double? _length;
        /// <summary>
        /// Глубина канавы,м
        /// </summary>
        public double? Depth { get => _depth; set => Set(ref _depth, value); }
        private double? _depth;
        /// <summary>
        /// Ширина канавы,м
        /// </summary>
        public double? Width { get => _width; set => Set(ref _width, value); }
        private double? _width;
        /// <summary>
        /// Начало проходки
        /// </summary>
        public DateTime StartDate { get => _startDate; set => Set(ref _startDate, value); }
        private DateTime _startDate;
        /// <summary>
        /// Окончание проходки
        /// </summary>
        public DateTime EndDate { get => _endDate; set => Set(ref _endDate, value); }
        private DateTime _endDate;

        /// <summary>
        /// Примечания
        /// </summary>
        public string? NotesCommentsText { get => _notesCommentsText; set => Set(ref _notesCommentsText, value); }
        public string? _notesCommentsText;

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
        /// Разрешение на кнопку сохранить
        /// </summary>
        public bool SubmitEnabled
        {
            get
            {
                if (
                    SelectedMines is not null
                    && SelectedPersons is not null
                    && SelectedPlaces is not null
                    && !string.IsNullOrEmpty(HoleId)
                    //&& Easting != 0
                    //&& Northing != 0
                    //&& Diam != 0
                    //&& Depth != 0
                    )
                {

                    return true;
                }
                else return false;
            }
        }


        #endregion

        /// <summary>
        /// Конструктор при котором будет происходить добавления в базу 
        /// </summary>
        public AddEditInfoTrench_ViewModel()
        {
            _editflag = false;
            _dialogManager = new DialogManager();
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
            _editflag = true;
            //StartDate = DateTime.Now;
            //EndDate = DateTime.Now;
            AnzasContext anzasContext = new AnzasContext();
            _infoTrenchRepository = new InfoTrenchRepository(anzasContext);
            _dialogManager = new DialogManager();
            Trenchs = _infoTrenchRepository.GetById(id);

            using (AnzasContext db = new AnzasContext())
            {
                Persons = db.People.AsNoTracking().ToObservableCollection();
                Mines = db.Mines.AsNoTracking().ToObservableCollection();
                Places = db.Places.AsNoTracking().ToObservableCollection();
            }
            SelectedPersons = Persons[Trenchs.Geolog - 1];
            SelectedMines = Mines[(int)Trenchs.TypeLcode - 1];
            SelectedPlaces = Places[(int)Trenchs.PlaceSite - 1];

            HoleId = Trenchs.HoleId;
            Profile = Trenchs.Profile;
            Easting1 = Trenchs.Easting1;
            Easting2 = Trenchs.Easting2;
            Northing1 = Trenchs.Northing1;
            Northing2 = Trenchs.Northing2;
            Elevation1 = Trenchs.Elevation1;
            Elevation2 = Trenchs.Elevation2;
            Azimuth = Trenchs.Azimuth;
            Length = Trenchs.Length;
            Width = Trenchs.Width;
            Depth = Trenchs.Width;
            NotesCommentsText = Trenchs.NotesCommentsText;
            if (Trenchs.StartDate != null)
                StartDate = new DateTime(Trenchs.StartDate.Value.Year, Trenchs.StartDate.Value.Month, Trenchs.StartDate.Value.Day);
            if (Trenchs.EndDate != null)
                EndDate = new DateTime(Trenchs.EndDate.Value.Year, Trenchs.EndDate.Value.Month, Trenchs.EndDate.Value.Day);


        }


        #region Команды

        /// <summary>
        /// Команда сохранения
        /// </summary>
        private ICommand _saveTrenchCommand;
        public ICommand SaveTrenchCommand
        {
            get
            {
                if (_saveTrenchCommand == null)
                {
                    if (_editflag)
                        _saveTrenchCommand = new RelayCommand(param => EditTrenchs());
                    else
                        _saveTrenchCommand = new RelayCommand(param => AddInfoTrenchs());

                }
                return _saveTrenchCommand;
            }
        }

        #endregion

        /// <summary>
        /// Метод обновления данных по Infodril
        /// </summary>
        private async void EditTrenchs()
        {
            try
            {

                if (Trenchs != null)
                {

                    Trenchs.HoleId = HoleId;
                    Trenchs.Profile = Profile;
                    Trenchs.Easting1 = Easting1;
                    Trenchs.Easting2 = Easting2;
                    Trenchs.Northing1 = Northing1;
                    Trenchs.Northing2 = Northing2;
                    Trenchs.Elevation1 = Elevation1;
                    Trenchs.Elevation2 = Elevation2;
                    Trenchs.Azimuth = Azimuth;
                    Trenchs.Length = Length;
                    Trenchs.Width = Width;
                    Trenchs.Depth = Depth;
                    Trenchs.NotesCommentsText = NotesCommentsText;
                    Trenchs.StartDate = new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day);
                    Trenchs.EndDate = new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day);
                    Trenchs.Geolog = SelectedPersons.Uid;
                    Trenchs.TypeLcode = SelectedMines.Uid;
                    Trenchs.PlaceSite = SelectedPlaces.Uid;
                };
                var result = _dialogManager.ShowMessageResult("Были внесены изминения, вы хотите их сохранить?"
                                                        , "Обновление записи в базу данных"
                                                        , InfoMessege.YesNoCancel, InfoMessege.Information);
                if (result == MessageBoxResult.Yes)
                    await _infoTrenchRepository.UpdateAsync(Trenchs).ConfigureAwait(false);
                _dialogManager.ShowMessage($"Данные успешно обновлены", "Обновление записи в базу данных", InfoMessege.OK, InfoMessege.Information);
            }
            catch (Exception ex)
            {
                _dialogManager.ShowMessage($"Ошибка добавления данных в базу: {ex.Message}", "Запись в базу данных", InfoMessege.OK, InfoMessege.Error);
            }

        }

        /// <summary>
        /// Метод Добавления новых данных по InfoTrench
        /// </summary>
        private async void AddInfoTrenchs()
        {
            try
            {

                var id = _infoTrenchRepository.Max();
                // заполните нужные поля в infoDrill
                Trenchs = new InfoTrench()
                {
                    Uid = id,
                    HoleId = HoleId,
                    Profile = Profile,
                    Easting1 = Easting1,
                    Easting2 = Easting2,
                    Northing1 = Northing1,
                    Northing2 = Northing2,
                    Elevation1 = Elevation1,
                    Elevation2 = Elevation2,
                    Azimuth = Azimuth,
                    Length = Length,
                    Width = Width,
                    Depth = Depth,
                    NotesCommentsText = NotesCommentsText,
                    StartDate = new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day),
                    EndDate = new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day),
                    Geolog = SelectedPersons.Uid,
                    TypeLcode = SelectedMines.Uid,
                    PlaceSite = SelectedPlaces.Uid
                };

                await _infoTrenchRepository.AddAsync(Trenchs).ConfigureAwait(false);
                _dialogManager.ShowMessage("Новая запись в базу данных успешно добавлена", "Запись в базу данных", InfoMessege.OK, InfoMessege.Information);
            }
            catch (Exception ex)
            {
                _dialogManager.ShowMessage($"Ошибка добавления данных в базу: {ex.Message}", "Запись в базу данных", InfoMessege.OK, InfoMessege.Error);
            }
        }
    }
}
