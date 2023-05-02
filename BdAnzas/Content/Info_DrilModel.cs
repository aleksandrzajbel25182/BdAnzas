using Anzas.DAL;
using BdAnzas.Base;
using Egor92.MvvmNavigation;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace BdAnzas.Content
{
    internal class Info_DrilModel : ViewModelBase
    {
        private NavigationManager navigationManager;
        private AnzasContext dbcontext;


        private ObservableCollection<InfoDrill> _infodrill = new ObservableCollection<InfoDrill>();
        /// <summary>
        /// Информаиция по скважинам
        /// </summary>
        public ObservableCollection<InfoDrill> InfoDrill
        {
            get => _infodrill;
            set => Set(ref _infodrill, value);
        }

        private InfoDrill? _selectedInfodril;
        public InfoDrill? Selected_InfoDrill
        {
            get => _selectedInfodril;
            set => Set(ref _selectedInfodril, value);
        }



        public Info_DrilModel()
        {
        }

        public Info_DrilModel(NavigationManager navigationManager, AnzasContext db)
        {
            this.navigationManager = navigationManager;
            this.dbcontext = db;

            InfoDrill = dbcontext.InfoDrills
                .Include(p => p.PlaceSiteNavigation)
                .Include(item => item.TypeLcodeNavigation)
                .Include(item => item.GeologNavigation)
                .AsNoTracking().ToObservableCollection();


            //DatacolRouDistcrit = db.Routes.Where(r => r.IdDistrictNavigation.IdDistrict == PassedParameter).Include(us => us.User).AsNoTracking().ToObservableCollection();
        }



    }
}
