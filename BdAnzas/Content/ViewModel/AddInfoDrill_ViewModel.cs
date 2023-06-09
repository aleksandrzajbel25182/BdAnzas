using Anzas.DAL;
using Anzas.DAL.Services;
using BdAnzas.Base;
using BdAnzas.Commands;
using BdAnzas.Constants;
using BdAnzas.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace BdAnzas.Content.ViewModel
{
    internal class AddInfoDrill_ViewModel : ViewModelBase
    {
        private readonly InfodrillRepository _infodrillRepository;
        private DialogManager _dialogManager;
        private bool _editflag; // флаг для понимания редактировать или обновлять данные
      
        private InfoDrill _drill = new InfoDrill();
        public InfoDrill Drill
        {
            get => _drill;
            set => Set(ref _drill, value);
        }


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
        private string _holeId;

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
        public double? Profile { get => _profile; set => Set(ref _profile, value); }
        private double? _profile;

        /// <summary>
        /// Долгота
        /// </summary>
        public double? Easting
        {
            get => _easting;
            set
            {
                _easting = value;
                OnPropertyChanged("Easting");
                OnPropertyChanged("SubmitEnabled");
            }
        }
        private double? _easting;

        /// <summary>
        /// Широта
        /// </summary>
        public double? Northing
        {
            get => _northing;
            set
            {
                _northing = value;
                OnPropertyChanged("Northing");
                OnPropertyChanged("SubmitEnabled");
            }
        }
        private double? _northing;

        /// <summary>
        /// Абс. отм.
        /// </summary>
        public double? Elevation { get => _elevation; set => SetProperty(ref _elevation, value); }
        private double? _elevation;

        /// <summary>
        /// Диаметр бурения, мм
        /// </summary>
        public double? Diam
        {
            get => _diam;
            set
            {
                _diam = value;
                OnPropertyChanged("Diam");
                OnPropertyChanged("SubmitEnabled");
            }
        }
        private double? _diam;

        /// <summary>
        /// Азимут ист., °
        /// </summary>
        public double? Azimuth { get => _azimuth; set => Set(ref _azimuth, value); }
        private double? _azimuth;

        /// <summary>
        /// Угол наклона от горизонта, °
        /// </summary>
        public double? Dip { get => _dip; set => Set(ref _dip, value); }
        private double? _dip;

        /// <summary>
        /// Глубина скважины,м
        /// </summary>
        public double? Depth
        {
            get => _depth;

            set
            {
                _depth = value;
                OnPropertyChanged("Depth");
                OnPropertyChanged("SubmitEnabled");
            }
        }
        private double? _depth;

        /// <summary>
        /// Уровень ПВ, м
        /// </summary>
        public double? Uroven { get => _uroven; set => SetProperty(ref _uroven, value); }
        private double? _uroven;

        /// <summary>
        /// Абс. отм. уровня, м
        /// 
        /// </summary>
        public double? UrAbs
        {
            get => _urAbs;
            set => SetProperty(ref _urAbs, value);
        }
        private double? _urAbs;

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

        #region Конструкторы 
        /// <summary>
        /// Конструктор для реализации добавления новых данных
        /// </summary
        public AddInfoDrill_ViewModel()
        {
            _editflag = false;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            _dialogManager = new DialogManager();
            _infodrillRepository = new InfodrillRepository(new AnzasContext());
            using (AnzasContext context = new AnzasContext())
            {
                Persons = context.People.AsNoTracking().ToObservableCollection();
                Mines = context.Mines.AsNoTracking().ToObservableCollection();
                Places = context.Places.AsNoTracking().ToObservableCollection();
            }
            Drill = new InfoDrill();
        }
        /// <summary>
        /// Конструктор для реализации обнвоелния данных. Служит для поиска по идентификтору и заполнение полей 
        /// </summary>
        /// <param name="uid">Идентификтор скважины</param>
        public AddInfoDrill_ViewModel(int uid)
        {
            _editflag = true;           

            AnzasContext anzasContext = new AnzasContext();
            _infodrillRepository = new InfodrillRepository(anzasContext);
            _dialogManager = new DialogManager();

            Drill = _infodrillRepository.GetById(uid);

            HoleId = Drill.HoleId;
            Profile = Drill.Profile;
            Easting = Drill.Easting;
            Northing = Drill.Northing;
            Elevation = Drill.Elevation;
            Diam = Drill.Diam;
            Azimuth = Drill.Azimuth;
            Dip = Drill.Dip;
            Depth = Drill.Depth;
            Uroven = Drill.Uroven;
            UrAbs = Drill.UrAbs;
            if (Drill.StartDate != null)
                StartDate = new DateTime(Drill.StartDate.Value.Year, Drill.StartDate.Value.Month, Drill.StartDate.Value.Day);
            if (Drill.EndDate != null)
                EndDate = new DateTime(Drill.EndDate.Value.Year, Drill.EndDate.Value.Month, Drill.EndDate.Value.Day);

            using (AnzasContext context = new AnzasContext())
            {
                Persons = context.People.AsNoTracking().ToObservableCollection();
                Mines = context.Mines.AsNoTracking().ToObservableCollection();
                Places = context.Places.AsNoTracking().ToObservableCollection();
                context.Dispose();
            }

            SelectedPersons = Persons[(int)Drill.Geolog - 1];
            SelectedMines = Mines[(int)Drill.TypeLcode - 1];
            SelectedPlaces = Places[(int)Drill.PlaceSite - 1];

        }
        #endregion
        #region Команды

        /// <summary>
        /// Команда сохранения
        /// </summary>
        private ICommand _saveInfoDrillCommand;
        public ICommand SaveInfoDrillCommand
        {
            get
            {
                if (_saveInfoDrillCommand == null)
                {
                    if (_editflag)
                        _saveInfoDrillCommand = new RelayCommand(param => EditInfoDrill());
                    else
                        _saveInfoDrillCommand = new RelayCommand(param => AddInfoDrill());

                }
                return _saveInfoDrillCommand;
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Метод обновления данных по Infodril
        /// </summary>
        private async void EditInfoDrill()
        {
            try
            {

                if (Drill != null)
                {

                    Drill.HoleId = HoleId;
                    Drill.TypeLcode = SelectedMines.Uid;
                    Drill.PlaceSite = SelectedPlaces.Uid;
                    Drill.Profile = Profile;
                    Drill.Easting = Easting;
                    Drill.Northing = Northing;
                    Drill.Elevation = Elevation;
                    Drill.Diam = Diam;
                    Drill.Azimuth = Azimuth;
                    Drill.Dip = Dip;
                    Drill.Depth = Depth;
                    Drill.Uroven = Uroven;
                    Drill.UrAbs = UrAbs;
                    Drill.StartDate = new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day);
                    Drill.EndDate = new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day);
                    Drill.Geolog = SelectedPersons.Uid;
                    Drill.NotesCommentsText = NotesCommentsText;
                    Drill.Project = 0;
                };
                var result = _dialogManager.ShowMessageResult("Были внесены изминения, вы хотите их сохранить?"
                                                        , "Обновление записи в базу данных"
                                                        , InfoMessege.YesNoCancel, InfoMessege.Information);
                if (result == MessageBoxResult.Yes)
                    await _infodrillRepository.UpdateAsync(Drill).ConfigureAwait(false);
                _dialogManager.ShowMessage($"Данные успешно обновлены", "Обновление записи в базу данных", InfoMessege.OK, InfoMessege.Information);
            }
            catch (Exception ex)
            {
                _dialogManager.ShowMessage($"Ошибка добавления данных в базу: {ex.Message}", "Запись в базу данных", InfoMessege.OK, InfoMessege.Error);
            }

        }

        /// <summary>
        /// Метод Добавления новых данных по Infodril
        /// </summary>
        private async void AddInfoDrill()
        {
            try
            {

                var id = _infodrillRepository.Max();
                // заполните нужные поля в infoDrill
                Drill = new InfoDrill()
                {
                    Uid = id + 1,
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

                await _infodrillRepository.AddAsync(Drill).ConfigureAwait(false);
                _dialogManager.ShowMessage("Новая запись в базу данных успешно добавлена", "Запись в базу данных", InfoMessege.OK, InfoMessege.Information);
            }
            catch (Exception ex)
            {
                _dialogManager.ShowMessage($"Ошибка добавления данных в базу: {ex.Message}", "Запись в базу данных", InfoMessege.OK, InfoMessege.Error);
            }
        }
        #endregion
        // Получаем сразу значение абс отм уровня
        protected override void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
        {
            base.PropertyNewValue(ref fieldProperty, newValue, propertyName);


            UrAbs = Elevation - Uroven;

        }
    }
}
