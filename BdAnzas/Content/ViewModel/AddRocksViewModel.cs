using Anzas.DAL;
using Anzas.DAL.Services;
using BdAnzas.Base;
using BdAnzas.Commands;
using BdAnzas.Constants;
using BdAnzas.Infrastructure;
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

namespace BdAnzas.Content.ViewModel
{
    internal class AddRocksViewModel : ViewModelBase
    {

        private readonly RockRepository _rockRepository;
        private DialogManager _dialogManager;
        private bool _editflag; // флаг для понимания редактировать или обновлять данные

        private Rock _rockrow = new Rock();
        public Rock Rockrow
        {
            get => _rockrow;
            set => Set(ref _rockrow, value);
        }

        #region Свойства



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
        public double? From { get => _from; set => SetProperty(ref _from, value); }
        private double? _from;
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
        #endregion


        public AddRocksViewModel()
        {
            _editflag = false;
            AnzasContext anzasContext = new AnzasContext();
            _rockRepository = new RockRepository(anzasContext);
            _dialogManager = new DialogManager();

            RockCodes = new ObservableCollection<RockCode>();
            Persons = new ObservableCollection<Person>();
            InfoDrills = new ObservableCollection<InfoDrill>();
            using (AnzasContext db = new AnzasContext())
            {

                RockCodes = db.RockCodes.AsNoTracking().ToObservableCollection();
                Persons = db.People.AsNoTracking().ToObservableCollection();
                InfoDrills = db.InfoDrills.AsNoTracking().ToObservableCollection();
            }

        }


        public AddRocksViewModel(int id)
        {
            _editflag = true;
            AnzasContext anzasContext = new AnzasContext();
            _rockRepository = new RockRepository(anzasContext);
            _dialogManager = new DialogManager();

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
            Rockrow = _rockRepository.GetById(id);

            From = Rockrow.From;
            To = Rockrow.To;
            Length = Rockrow.Length;
            Kernm = Rockrow.Kernm;
            Kernpc = Rockrow.Kernpc;
            Description = Rockrow.Description;
            Petro = Rockrow.Petro;
            Mineral = Rockrow.Mineral;
            NotesCommentsText = Rockrow.NotesCommentsText;


            SelectedPersons = Persons[(int)Rockrow.Geolog - 1];
            SelectedInfoDrill = InfoDrills[(int)Rockrow.HoleId - 1];
            SelectedRockCode = RockCodes[(int)Rockrow.RockCode - 1];


        }
        #region Команды

        /// <summary>
        /// Команда сохранения
        /// </summary>
        private ICommand _saveRockCommand;
        public ICommand SaveRockCommand
        {
            get
            {
                if (_saveRockCommand == null)
                {
                    if (_editflag)
                        _saveRockCommand = new RelayCommand(param => EditRock());
                    else
                        _saveRockCommand = new RelayCommand(param => AddRock());

                }
                return _saveRockCommand;
            }
        }

        #endregion


        /// <summary>
        /// Метод Добавления новых данных по Rock
        /// </summary>
        private async void AddRock()
        {
            try
            {
                var maxid = _rockRepository.Max();
                Rock rock = new Rock
                {
                    Uid = maxid+1,
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

                await _rockRepository.AddAsync(rock).ConfigureAwait(false);
                _dialogManager.ShowMessage("Новая запись в базу данных успешно добавлена", "Запись в базу данных", InfoMessege.OK, InfoMessege.Information);
            }
            catch (Exception ex)
            {
                _dialogManager.ShowMessage($"Ошибка добавления данных в базу: {ex.Message}", "Запись в базу данных", InfoMessege.OK, InfoMessege.Error);
            }

        }
        /// <summary>
        /// Метод обновления данных по Rock
        /// </summary>
        private async void EditRock()
        {
            try
            {

                if (Rockrow != null)
                {

                    Rockrow.From = From;
                    Rockrow.To = To;
                    Rockrow.Length = Length;
                    Rockrow.Kernm = Kernm;
                    Rockrow.Kernpc = Kernpc;
                    Rockrow.Description = Description;
                    Rockrow.Petro = Petro;
                    Rockrow.Mineral = Mineral;
                    Rockrow.NotesCommentsText = NotesCommentsText;
                    Rockrow.Geolog = SelectedPersons.Uid;
                    Rockrow.HoleId = SelectedInfoDrill.Uid;
                    Rockrow.RockCode = SelectedRockCode.Uid;
                };
                var result = _dialogManager.ShowMessageResult("Были внесены изминения, вы хотите их сохранить?"
                                                        , "Обновление записи в базу данных"
                                                        , InfoMessege.YesNoCancel, InfoMessege.Information);
                if (result == MessageBoxResult.Yes)
                    await _rockRepository.UpdateAsync(Rockrow).ConfigureAwait(false);
                _dialogManager.ShowMessage($"Данные успешно обновлены", "Обновление записи в базу данных", InfoMessege.OK, InfoMessege.Information);
            }
            catch (Exception ex)
            {
                _dialogManager.ShowMessage($"Ошибка добавления данных в базу: {ex.Message}", "Запись в базу данных", InfoMessege.OK, InfoMessege.Error);
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
