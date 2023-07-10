using Anzas.DAL;
using Anzas.DAL.Services;
using BdAnzas.Base;
using BdAnzas.Commands;
using BdAnzas.Constants;
using BdAnzas.Content.Windows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BdAnzas.Content.ViewModel
{
    internal class Syrvey_ViewModel : ViewModelBase
    {

        #region Свойства

       
        private ObservableCollection<Survey> _syrveyDrill;
        /// <summary>
        /// Инклинометрия скважин
        /// </summary>
        public ObservableCollection<Survey> SyrveyDrill
        {
            get { return _syrveyDrill; }
            set { _syrveyDrill = value; }
        }

        private Survey _selectedSyrveyDrill;
        /// <summary>
        /// Выбранная инклинометрия скважин
        /// </summary>
        public Survey SelectedSyrveyDrill
        {
            get { return  _selectedSyrveyDrill; }
            set { _selectedSyrveyDrill = value; }
        }



        #endregion

        public Syrvey_ViewModel()
        {
            _syrveyDrill = new ObservableCollection<Survey>();
            using (AnzasContext db = new AnzasContext())
            {
                SyrveyDrill = db.Surveys.Include(h => h.Hole).AsNoTracking().ToObservableCollection();
            }

        }


        private ICommand _editItemCommand;
        public ICommand EditItemCommand
        {
            get
            {
                if (_editItemCommand == null)
                {
                    _editItemCommand = new RelayCommand(param => OpenWindowEdit());
                }
                return _editItemCommand;
            }
        }


        private ICommand _addItemCommand;
        public ICommand AddItemCommand
        {
            get
            {
                if (_addItemCommand == null)
                {

                    _addItemCommand = new RelayCommand(param => OpenWindowAdd());
                }
                return _addItemCommand;
            }
        }



        /// <summary>
        /// Метод для редактирования
        /// </summary>
        private void OpenWindowEdit()
        {
            AddWindow window = new AddWindow();
            if (SelectedSyrveyDrill != null)
            {
                window.DataContext = new AddEditWindowViewModel(NavigationKeys.SurveyhKey, SelectedSyrveyDrill.Uid);
                if (window.ShowDialog() == false)
                {
                    //SyrveyDrill.Clear();
                    //SyrveyDrill = new ObservableCollection<Survey>(_infoRouteRepository.GetAll());
                }
            }

        }
        /// <summary>
        /// Метод для добавления нового элемента
        /// </summary>
        private void OpenWindowAdd()
        {
            AddWindow window = new AddWindow();

            window.DataContext = new AddEditWindowViewModel(NavigationKeys.SurveyhKey);
            if (window.ShowDialog() == false)
            {
                //SyrveyDrill.Clear();
                //SyrveyDrill = new ObservableCollection<Survey>(_infoRouteRepository.GetAll());
            }

        }
    }
}
