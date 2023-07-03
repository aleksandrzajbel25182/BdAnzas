using Anzas.DAL;
using BdAnzas.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
