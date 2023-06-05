using Anzas.DAL;
using BdAnzas.Base;
using BdAnzas.Commands;
using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BdAnzas.Content
{
    internal class Info_TrenchModel : ViewModelBase
    {

        private ObservableCollection<InfoTrench> _infoTrench;
        /// <summary>
        /// Информаиция по Канавам
        /// </summary>
        public ObservableCollection<InfoTrench> InfoTrench
        {
            get => _infoTrench;
            set => Set(ref _infoTrench, value);

        }

        private InfoTrench _selectedInfoTrench;
        private InfoTrench SelectedInfoTrench
        {
            get => _selectedInfoTrench;
            set => Set(ref _selectedInfoTrench, value);

        }

        public Info_TrenchModel()
        {
            using (AnzasContext db = new AnzasContext())
            {
                InfoTrench = db.InfoTrenches
                   .Include(p => p.PlaceSiteNavigation)
                   .Include(item => item.TypeLcodeNavigation)
                   .Include(item => item.GeologNavigation)
                   .AsNoTracking().ToObservableCollection();
            }

        }

    }
}
