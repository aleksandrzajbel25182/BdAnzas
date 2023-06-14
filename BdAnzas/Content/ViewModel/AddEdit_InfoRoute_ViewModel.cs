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
    internal class AddEdit_InfoRoute_ViewModel : ViewModelBase
    {
        private readonly InfoRouteRepository _infoRouteRepository;
        private DialogManager _dialogManager;
        private bool _editflag; // флаг для понимания редактировать или обновлять данные
        private InfoRoute route;

        #region Свойства


        /// <summary>
        /// № маршрута
        /// </summary>
        public string HoleId
        {
            get => _holeid; set
            {
                _holeid = value;
                OnPropertyChanged("HoleId");
                OnPropertyChanged("SubmitEnabled");
            }
        }
        private string _holeid;


        /// <summary>
        /// Долгота (начало)
        /// </summary>
        public double? Easting1 { get => _easting1; set => Set(ref _easting1, value); }
        public double? _easting1;
        /// <summary>
        /// Долгота (начало)
        /// </summary>
        public double? Northing1 { get => _northing1; set => Set(ref _northing1, value); }
        public double? _northing1;
        /// <summary>
        /// Абс. отм. (начало)
        /// </summary>
        public double? Elevation1 { get => _elevation1; set => Set(ref _elevation1, value); }
        public double? _elevation1;

        /// <summary>
        /// Долгота (конец)
        /// </summary>
        public double? Easting2 { get => _easting2; set => Set(ref _easting2, value); }
        public double? _easting2;

        /// <summary>
        /// Широта (конец)
        /// </summary>
        public double? Northing2 { get => _northing2; set => Set(ref _northing2, value); }
        public double? _northing2;
        /// <summary>
        /// Абс. отм. (конец)
        /// </summary>
        public double? Elevation2 { get => _elevation2; set => Set(ref _elevation2, value); }
        public double? _elevation2;
        /// <summary>
        /// Длина маршрута
        /// </summary>
        public double? Length { get => _length; set => Set(ref _length, value); }
        public double? _length;
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime? Date { get => _date; set => Set(ref _date, value); }
        public DateTime? _date;
        /// <summary>
        /// Примечание
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



        public AddEdit_InfoRoute_ViewModel()
        {
            AnzasContext anzasConxtet = new AnzasContext();
            _infoRouteRepository = new InfoRouteRepository(anzasConxtet);
            _editflag = false;
            _dialogManager = new DialogManager();
            route = new InfoRoute();
            using (AnzasContext db = new AnzasContext())
            {
                Persons = db.People.AsNoTracking().ToObservableCollection();
                Mines = db.Mines.AsNoTracking().ToObservableCollection();
                Places = db.Places.AsNoTracking().ToObservableCollection();
            }

        }

        public AddEdit_InfoRoute_ViewModel(int id)
        {
            _editflag = true;
            _dialogManager = new DialogManager();
            AnzasContext anzasConxtet = new AnzasContext();
            _infoRouteRepository = new InfoRouteRepository(anzasConxtet);
            route = new InfoRoute();
            route = _infoRouteRepository.GetById(id);

            HoleId = route.HoleId;
            Easting1 = route.Easting1;
            Easting2 = route.Easting2;
            Northing1 = route.Northing1;
            Northing2 = route.Northing2;
            Elevation1 = route.Elevation1;
            Elevation2 = route.Elevation2;
            Length = route.Length;
            NotesCommentsText = route.NotesCommentsText;
            if (route.Date != null)
                Date = new DateTime(route.Date.Value.Year, route.Date.Value.Month, route.Date.Value.Day);

            using (AnzasContext db = new AnzasContext())
            {
                Persons = db.People.AsNoTracking().ToObservableCollection();
                Mines = db.Mines.AsNoTracking().ToObservableCollection();
                Places = db.Places.AsNoTracking().ToObservableCollection();
            }
            SelectedPersons = Persons[route.Geolog - 1];
            SelectedMines = Mines[(int)route.TypeLcode - 1];
            SelectedPlaces = Places[(int)route.PlaceSite - 1];
        }

        #region Команды

        /// <summary>
        /// Команда сохранения
        /// </summary>
        private ICommand _saveInfoRouteCommand;
        public ICommand SaveInfoRouteCommand
        {
            get
            {
                if (_saveInfoRouteCommand == null)
                {
                    if (_editflag)
                        _saveInfoRouteCommand = new RelayCommand(param => EditInfoRoute());
                    else
                        _saveInfoRouteCommand = new RelayCommand(param => AddInfoRoute());

                }
                return _saveInfoRouteCommand;
            }
        }

        #endregion

        /// <summary>
        /// Метод обновления данных по Route
        /// </summary>
        private async void EditInfoRoute()
        {
            try
            {

                if (route != null)
                {
                    route.HoleId = HoleId;
                    route.Easting1 = Easting1;
                    route.Easting2 = Easting2;
                    route.Northing1 = Northing1;
                    route.Northing2 = Northing2;
                    route.Elevation1 = Elevation1;
                    route.Elevation2 = Elevation2;
                    route.Length = Length;
                    route.NotesCommentsText = NotesCommentsText;
                    route.Date = new DateOnly(Date.Value.Year, Date.Value.Month, Date.Value.Day);
                    route.Geolog = SelectedPersons.Uid;
                    route.TypeLcode = SelectedMines.Uid;
                    route.PlaceSite = SelectedPlaces.Uid;
                };
                var result = _dialogManager.ShowMessageResult("Были внесены изминения, вы хотите их сохранить?"
                                                        , "Обновление записи в базу данных"
                                                        , InfoMessege.YesNoCancel, InfoMessege.Information);
                if (result == MessageBoxResult.Yes)
                    await _infoRouteRepository.UpdateAsync(route).ConfigureAwait(false);
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
        private async void AddInfoRoute()
        {
            try
            {

                var id = _infoRouteRepository.Max();
                // заполните нужные поля в infoDrill
                route = new InfoRoute()
                {
                    Uid = id+1,
                    HoleId = HoleId,
                    Easting1 = Easting1,
                    Easting2 = Easting2,
                    Northing1 = Northing1,
                    Northing2 = Northing2,
                    Elevation1 = Elevation1,
                    Elevation2 = Elevation2,
                    Length = Length,
                    NotesCommentsText = NotesCommentsText,
                    Date = new DateOnly(Date.Value.Year, Date.Value.Month, Date.Value.Day),
                    Geolog = SelectedPersons.Uid,
                    TypeLcode = SelectedMines.Uid,
                    PlaceSite = SelectedPlaces.Uid
                };

                await _infoRouteRepository.AddAsync(route).ConfigureAwait(false);
                _dialogManager.ShowMessage("Новая запись в базу данных успешно добавлена", "Запись в базу данных", InfoMessege.OK, InfoMessege.Information);
            }
            catch (Exception ex)
            {
                _dialogManager.ShowMessage($"Ошибка добавления данных в базу: {ex.Message}", "Запись в базу данных", InfoMessege.OK, InfoMessege.Error);
            }
        }
    }
}
