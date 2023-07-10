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
    internal class AddEditSyrveyDrill_ViewModel: ViewModelBase
    {
        /// <summary>
        /// Глубина
        /// </summary>
        private string _depth;
        public string Depth { get => _depth; set => Set(ref _depth, value); }

        /// <summary>
        /// Угол наклона от вертикали
        /// </summary>
        private double _inclin;
        public double Inclin { get=>_inclin; set=>Set(ref _inclin , value); }

        // <summary>
        /// Аз_магн_исходный
        /// </summary>
        private double? _azMagn;
        public double? AzMagn { get=> _azMagn; set=>Set(ref _azMagn , value); }

        /// <summary>
        /// Аз_мг_принятый
        /// </summary>
        private double? _azimPr;
        public double? AzimPr { get=> _azimPr; set=>Set(ref _azimPr , value); }

        /// <summary>
        /// Аз_ист_принятый
        /// </summary>
        private double? _azimIst;
        public double? AzimIst { get=> _azimIst; set=>Set(ref _azimIst , value); }

        private ObservableCollection<InfoDrill> _hole;
        public ObservableCollection<InfoDrill> Hole { get=>_hole; set=>Set(ref _hole, value); }



        public AddEditSyrveyDrill_ViewModel()
        {
            using(AnzasContext db = new AnzasContext())
            {
                Hole = db.InfoDrills.Select(x => new InfoDrill{ Uid = x.Uid, HoleId =  x.HoleId}).AsNoTracking().ToObservableCollection();
            }
        }
        public AddEditSyrveyDrill_ViewModel(int id)
        {

        }




    }
}
