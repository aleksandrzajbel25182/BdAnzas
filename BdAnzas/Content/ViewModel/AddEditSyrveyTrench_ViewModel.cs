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
using System.Windows.Documents;
using System.Windows.Input;

namespace BdAnzas.Content.ViewModel
{
    internal class AddEditSyrveyTrench_ViewModel : ViewModelBase
    {
        private DialogManager _dialogManager;
        private bool _editflag; // флаг для понимания редактировать или обновлять данные

        private SurveyTrench syrveyThrench;

        #region Свойства
        /// <summary>
        /// Номер точки поворота
        /// </summary>
        private int? _numTp;
        public int? NumTp { get => _numTp; set => Set(ref _numTp, value); }
        /// <summary>
        /// Длина канавы,м
        /// </summary>
        private double? _length;
        public double? Length { get => _length; set => Set(ref _length, value); }
        /// <summary>
        /// Азимут магн. полевой, °
        /// </summary>
        private double? _azMagn;
        public double? AzMagn { get => _azMagn; set => Set(ref _azMagn, value); }
        /// <summary>
        /// Глубина канавы,м
        /// </summary>
        private double? _depth;
        public double? Depth { get => _depth; set => Set(ref _depth, value); }
        /// <summary>
        /// Долгота
        /// </summary>
        private double? _easting;
        public double? Easting { get => _easting; set => Set(ref _easting, value); }
        /// <summary>
        /// Широта
        /// </summary>
        private double? _northing;
        public double? Northing { get => _northing; set => Set(ref _northing, value); }
        /// <summary>
        /// Абс. отм.
        /// </summary>
        private double? _elevation;
        public double? Elevation { get => _elevation; set => Set(ref _elevation, value); }

        private ObservableCollection<InfoTrench> _hole;
        public ObservableCollection<InfoTrench> Hole { get => _hole; set => Set(ref _hole, value); }

        private InfoTrench _selectedhole;
        public InfoTrench SelectedHole { get => _selectedhole; set => Set(ref _selectedhole, value); }

        #endregion


        public AddEditSyrveyTrench_ViewModel()
        {
            _editflag = true;
            _dialogManager = new DialogManager();
            syrveyThrench = new SurveyTrench();

        }

        public AddEditSyrveyTrench_ViewModel(int id)
        {
            _editflag = true;
            _dialogManager = new DialogManager();
            syrveyThrench = new SurveyTrench();
            using (AnzasContext db = new AnzasContext())
            {
                syrveyThrench = db.SurveyTrenches.SingleOrDefault(i => i.Uid == id);
                NumTp = syrveyThrench.NumTp;
                Length = syrveyThrench.Length;
                AzMagn = syrveyThrench.AzMagn;
                Depth = syrveyThrench.Depth;
                Easting = syrveyThrench.Easting;
                Northing = syrveyThrench.Northing;
                Elevation = syrveyThrench.Elevation;
                Hole = db.InfoTrenches.Select(x => new InfoTrench { Uid = x.Uid, HoleId = x.HoleId }).AsNoTracking().ToObservableCollection();

            }

        }


        /// <summary>
        /// Команда сохранения
        /// </summary>
        private ICommand _saveSurveyTrenchCommand;
        public ICommand SaveSurveyTrenchCommand
        {
            get
            {
                if (_saveSurveyTrenchCommand == null)
                {
                    if (_editflag)
                        _saveSurveyTrenchCommand = new RelayCommand(param => EditSurveyTrench());
                    else
                        _saveSurveyTrenchCommand = new RelayCommand(param => AddSurveyTrench());

                }
                return _saveSurveyTrenchCommand;
            }
        }




        /// <summary>
        /// Метод Добавления новых данных по Survey
        /// </summary>
        private async void AddSurveyTrench()
        {
            try
            {
                using (AnzasContext db = new AnzasContext())
                {
                    int id = db.SurveyTrenches.Max(u => u.Uid);
                    syrveyThrench = new SurveyTrench()
                    {
                        Uid = id + 1,
                         Easting= Easting,
                        Elevation= Elevation,
                        AzMagn= AzMagn,
                        Depth = Depth,
                        Northing = Northing,
                        NumTp = NumTp,
                        Length = Length,
                        HoleId = SelectedHole.Uid
                    };

                    db.SurveyTrenches.Add(syrveyThrench);
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
        /// Метод обновления данных по Survey
        /// </summary>
        private async void EditSurveyTrench()
        {
            try
            {
                if (syrveyThrench != null)
                {

                    syrveyThrench.Depth = Depth;
                    syrveyThrench.NumTp = NumTp;
                    syrveyThrench.AzMagn = AzMagn;
                    syrveyThrench.Elevation= Elevation;
                    syrveyThrench.Northing= Northing;
                    syrveyThrench.Length = Length;
                    syrveyThrench.Easting = Easting;
                }
                var result = _dialogManager.ShowMessageResult("Были внесены изминения, вы хотите их сохранить?"
                                                        , "Обновление записи в базу данных"
                                                        , InfoMessege.YesNoCancel, InfoMessege.Information);
                if (result == MessageBoxResult.Yes)
                    using (AnzasContext db = new AnzasContext())
                    {
                        db.SurveyTrenches.Update(syrveyThrench);
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
