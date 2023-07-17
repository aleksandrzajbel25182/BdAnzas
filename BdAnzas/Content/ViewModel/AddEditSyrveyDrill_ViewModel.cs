using Anzas.DAL;
using BdAnzas.Base;
using BdAnzas.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anzas.DAL;
using BdAnzas.Commands;
using BdAnzas.Constants;
using MaterialDesignThemes.Wpf;
using MathCore.Functions.Differentiable;
using System.Windows.Input;
using System.Windows;

namespace BdAnzas.Content.ViewModel
{
    internal class AddEditSyrveyDrill_ViewModel : ViewModelBase
    {

        private DialogManager _dialogManager;
        private bool _editflag; // флаг для понимания редактировать или обновлять данные

        private Survey syrvey;

        /// <summary>
        /// Глубина
        /// </summary>
        private double? _depth;
        public double? Depth { get => _depth; set => Set(ref _depth, value); }

        /// <summary>
        /// Угол наклона от вертикали
        /// </summary>
        private double? _inclin;
        public double? Inclin { get => _inclin; set => Set(ref _inclin, value); }

        // <summary>
        /// Аз_магн_исходный
        /// </summary>
        private double? _azMagn;
        public double? AzMagn { get => _azMagn; set => Set(ref _azMagn, value); }

        /// <summary>
        /// Аз_мг_принятый
        /// </summary>
        private double? _azimPr;
        public double? AzimPr { get => _azimPr; set => Set(ref _azimPr, value); }

        /// <summary>
        /// Аз_ист_принятый
        /// </summary>
        private double? _azimIst;
        public double? AzimIst { get => _azimIst; set => Set(ref _azimIst, value); }

        private ObservableCollection<InfoDrill> _hole;
        public ObservableCollection<InfoDrill> Hole { get => _hole; set => Set(ref _hole, value); }

        private InfoDrill _selectedhole;
        public InfoDrill SelectedHole { get => _selectedhole; set => Set(ref _selectedhole, value); }


        public AddEditSyrveyDrill_ViewModel()
        {
            _editflag = false;
            _dialogManager = new DialogManager();
            using (AnzasContext db = new AnzasContext())
            {
                Hole = db.InfoDrills.Select(x => new InfoDrill { Uid = x.Uid, HoleId = x.HoleId }).AsNoTracking().ToObservableCollection();
            }
        }
        public AddEditSyrveyDrill_ViewModel(int id)
        {
            _editflag = true;
            _dialogManager = new DialogManager();
            syrvey = new Survey();
            using (AnzasContext db = new AnzasContext())
            {
                syrvey = db.Surveys.SingleOrDefault(i => i.Uid == id);
                Depth = syrvey.Depth;
                Inclin = syrvey.Inclin;
                AzMagn = syrvey.AzMagn;
                AzimPr = syrvey.AzimPr;
                AzimIst = syrvey.AzimIst;
                Hole = db.InfoDrills.Select(x => new InfoDrill { Uid = x.Uid, HoleId = x.HoleId }).AsNoTracking().ToObservableCollection();
                SelectedHole = Hole[(int)syrvey.HoleId - 1];
            }

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
                        _saveRockrouteCommand = new RelayCommand(param => EditSurvey());
                    else
                        _saveRockrouteCommand = new RelayCommand(param => AddSurveyDrill());

                }
                return _saveRockrouteCommand;
            }
        }




        /// <summary>
        /// Метод Добавления новых данных по Survey
        /// </summary>
        private async void AddSurveyDrill()
        {
            try
            {
                using (AnzasContext db = new AnzasContext())
                {
                    int id = db.Surveys.Max(u => u.Uid);
                    syrvey = new Survey()
                    {
                        Uid = id + 1, 
                        AzimIst = AzimIst,
                        AzimPr = AzimPr,
                        AzMagn = AzMagn,
                        Depth = Depth,
                        Inclin = Inclin,
                        HoleId = SelectedHole.Uid
                    };

                    db.Surveys.Add(syrvey);
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
        private async void EditSurvey()
        {
            try
            {
                if (syrvey != null)
                {

                    syrvey.Depth = Depth;
                    syrvey.Inclin = Inclin;
                    syrvey.AzMagn = AzMagn;
                    syrvey.AzimPr = AzimPr;
                    syrvey.AzimIst = AzimIst;
                }
                var result = _dialogManager.ShowMessageResult("Были внесены изминения, вы хотите их сохранить?"
                                                        , "Обновление записи в базу данных"
                                                        , InfoMessege.YesNoCancel, InfoMessege.Information);
                if (result == MessageBoxResult.Yes)
                    using (AnzasContext db = new AnzasContext())
                    {
                        db.Surveys.Update(syrvey);
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
