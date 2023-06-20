using Anzas.DAL;
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
    internal class AddEditRockRoute_ViewModel : ViewModelBase
    {

        private DialogManager _dialogManager;
        private bool _editflag; // флаг для понимания редактировать или обновлять данные


        private RocksRoute _rockRoute;

        #region Свойства      

        /// <summary>
        /// Номер точки наблюдения (т.н.)
        /// </summary>
        public int? Rtn { get => _rtn; set => Set(ref _rtn, value); }
        private int? _rtn;

        public string? Rsample { get => _rsample; set => Set(ref _rsample, value); }
        private string? _rsample;

        /// <summary>
        /// Долгота
        /// </summary>
        public double? Easting { get => _easting; set => Set(ref _easting, value); }
        private double? _easting;

        /// <summary>
        /// Широта
        /// </summary>
        public double? Northing { get => _northing; set => Set(ref _northing, value); }
        private double? _northing;

        /// <summary>
        /// Абс. отм.
        /// </summary>
        public double? Elevation { get => _elevation; set => Set(ref _elevation, value); }
        private double? _elevation;

        /// <summary>
        /// Характеристика места отбора обр., проб
        /// </summary>
        public ObservableCollection<Otbor> TnOtbor { get => _tnOtbor; set => Set(ref _tnOtbor, value); }
        private ObservableCollection<Otbor> _tnOtbor;

        private Otbor _selectedOtbor;
        public Otbor SelectedOtbor { get => _selectedOtbor; set => Set(ref _selectedOtbor, value); }


        /// <summary>
        /// Тип опробуемых отложений
        /// </summary>
        public ObservableCollection<Anzas.DAL.Type> TnType { get => _tnType; set => Set(ref _tnType, value); }
        private ObservableCollection<Anzas.DAL.Type> _tnType;

        private Anzas.DAL.Type _selectedType;
        public Anzas.DAL.Type SelectedType { get => _selectedType; set => Set(ref _selectedType, value); }

        /// <summary>
        /// Код породы
        /// </summary>
        public ObservableCollection<RockCode> RockCode { get => _rockCode; set => Set(ref _rockCode, value); }
        private ObservableCollection<RockCode> _rockCode;

        private RockCode _selectedRockCode;
        public RockCode SelectedRockCode { get => _selectedRockCode; set => Set(ref _selectedRockCode, value); }

        /// <summary>
        /// Описание керна
        /// </summary>
        public string? Description { get => _description; set => Set(ref _description, value); }
        private string? _description;

        /// <summary>
        /// Геолог
        /// </summary>
        public ObservableCollection<Person> Geolog { get => _geolog; set => Set(ref _geolog, value); }
        private ObservableCollection<Person> _geolog;

        private Person _selectedGeolog;
        public Person SelectedGeolog { get => _selectedGeolog; set => Set(ref _selectedGeolog, value); }

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
        #endregion


        public AddEditRockRoute_ViewModel()
        {
            _editflag = false;
            _dialogManager = new DialogManager();

            using (AnzasContext db = new AnzasContext())
            {
                var otbors = db.Otbors.AsNoTracking().ToObservableCollection();
                var tntype = db.Types.AsNoTracking().ToObservableCollection();
                var geolog = db.People.AsNoTracking().ToObservableCollection();
                var rockkode = db.RockCodes.AsNoTracking().ToObservableCollection();

                TnOtbor = new ObservableCollection<Otbor>(otbors);
                TnType = new ObservableCollection<Anzas.DAL.Type>(tntype);
                Geolog = new ObservableCollection<Person>(geolog);
                RockCode = new ObservableCollection<RockCode>();

            }


        }

        public AddEditRockRoute_ViewModel(int id)
        {
            _editflag = true;
            _dialogManager = new DialogManager();
            _rockRoute = new RocksRoute();
            using (AnzasContext db = new AnzasContext())
            {
                var otbors = db.Otbors.AsNoTracking().ToObservableCollection();
                var tntype = db.Types.AsNoTracking().ToObservableCollection();
                var geolog = db.People.AsNoTracking().ToObservableCollection();
                var rockkode = db.RockCodes.AsNoTracking().ToObservableCollection();

                TnOtbor = new ObservableCollection<Otbor>(otbors);
                TnType = new ObservableCollection<Anzas.DAL.Type>(tntype);
                Geolog = new ObservableCollection<Person>(geolog);
                RockCode = new ObservableCollection<RockCode>(rockkode);

                _rockRoute = db.RocksRoutes.SingleOrDefault(i => i.Uid == id);

            }
            Rtn = _rockRoute.Rtn;
            Rsample = _rockRoute.Rsample;
            Elevation = _rockRoute.Elevation;
            Northing = _rockRoute.Northing;
            Easting = _rockRoute.Easting;
            Description = _rockRoute.Description;
            Petro = _rockRoute.Petro;
            Mineral = _rockRoute.Mineral;
            NotesCommentsText = _rockRoute.NotesCommentsText;

            SelectedGeolog = Geolog[(int)_rockRoute.Geolog - 1];
            SelectedOtbor = TnOtbor[(int)_rockRoute.TnOtbor - 1];
            SelectedType = TnType[(int)_rockRoute.TnType - 1];
            SelectedRockCode = RockCode[(int)_rockRoute.RockCode - 1];


        }


        /// <summary>
        /// Команда сохранения
        /// </summary>
        private ICommand _saveRockrouteCommand;
        public ICommand SaveRockRouteCommand
        {
            get
            {
                if (_saveRockrouteCommand == null)
                {
                    if (_editflag)
                        _saveRockrouteCommand = new RelayCommand(param => EditRockRoute());
                    else
                        _saveRockrouteCommand = new RelayCommand(param => AddRockRoute());

                }
                return _saveRockrouteCommand;
            }
        }




        /// <summary>
        /// Метод Добавления новых данных по RockRoute
        /// </summary>
        private async void AddRockRoute()
        {
            try
            {
                using (AnzasContext db = new AnzasContext())
                {
                    int id = db.RocksRoutes.Max(u => u.Uid);
                    _rockRoute = new RocksRoute()
                    {
                        Uid = id + 1,
                        Rtn = Rtn,
                        Rsample = Rsample,
                        Elevation = Elevation,
                        Northing = Northing,
                        Easting = Easting,
                        Description = Description,
                        Petro = Petro,
                        Mineral = Mineral,
                        NotesCommentsText = NotesCommentsText,
                        TnOtbor = SelectedOtbor.Uid,
                        TnType = SelectedType.Uid,
                        Geolog = SelectedGeolog.Uid,
                        RockCode = SelectedRockCode.Uid
                    };

                    db.RocksRoutes.Add(_rockRoute);
                    await db.SaveChangesAsync();
                }
                _dialogManager.ShowMessage("Новая запись в базу данных успешно добавлена", "Запись в базу данных", InfoMessege.OK, InfoMessege.Information);
            }
            catch (Exception ex)
            {
                _dialogManager.ShowMessage($"Ошибка добавления данных в базу: {ex.Message}", "Запись в базу данных", InfoMessege.OK, InfoMessege.Error);
            }

        }
        /// <summary>
        /// Метод обновления данных по RockRoute
        /// </summary>
        private async void EditRockRoute()
        {
            try
            {
                if (_rockRoute != null)
                {

                    _rockRoute.Rtn = Rtn;
                    Rsample = Rsample;
                    _rockRoute.Elevation = Elevation;
                    _rockRoute.Northing = Northing;
                    _rockRoute.Easting = Easting;
                    _rockRoute.Description = Description;
                    _rockRoute.Petro = Petro;
                    _rockRoute.Mineral = Mineral;
                    _rockRoute.NotesCommentsText = NotesCommentsText;
                    _rockRoute.TnOtbor = SelectedOtbor.Uid;
                    _rockRoute.TnType = SelectedType.Uid;
                    _rockRoute.Geolog = SelectedGeolog.Uid;
                    _rockRoute.RockCode = SelectedRockCode.Uid;
                }


                var result = _dialogManager.ShowMessageResult("Были внесены изминения, вы хотите их сохранить?"
                                                        , "Обновление записи в базу данных"
                                                        , InfoMessege.YesNoCancel, InfoMessege.Information);
                if (result == MessageBoxResult.Yes)
                    using (AnzasContext db = new AnzasContext())
                    {
                        db.RocksRoutes.Update(_rockRoute);
                        await db.SaveChangesAsync();
                    }

                _dialogManager.ShowMessage($"Данные успешно обновлены", "Обновление записи в базу данных", InfoMessege.OK, InfoMessege.Information);
            }
            catch (Exception ex)
            {
                _dialogManager.ShowMessage($"Ошибка добавления данных в базу: {ex.Message}", "Запись в базу данных", InfoMessege.OK, InfoMessege.Error);
            }

        }

    }
}
