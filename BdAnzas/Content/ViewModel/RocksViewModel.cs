using Anzas.DAL;
using Anzas.DAL.Services;
using BdAnzas.Base;
using BdAnzas.Commands;
using BdAnzas.Constants;
using BdAnzas.Content.Windows;
using BdAnzas.Infrastructure;
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
    internal class RocksViewModel : ViewModelBase
    {
        RockRepository _rockRepository;
        private ObservableCollection<Rock> _rocks;
        public ObservableCollection<Rock> Rocks
        {
            get => _rocks;
            set
            {
                _rocks = value;
                OnPropertyChanged("Rocks");

            }
        }

        private Rock _selectedRocks;
        public Rock SelectedRocks
        {
            get => _selectedRocks;
            set => Set(ref _selectedRocks, value);
        }


        private ObservableCollection<RocksRoute> _rocksroute;
        public ObservableCollection<RocksRoute> Rocksroute
        {
            get => _rocksroute;
            set
            {
                _rocksroute = value;
                OnPropertyChanged("Rocksroute");

            }
        }

        private RocksRoute _selectedRocksRoute;
        public RocksRoute SelectedRocksRoute
        {
            get => _selectedRocksRoute;
            set => Set(ref _selectedRocksRoute, value);
        }


        public RocksViewModel()
        {

            AnzasContext anzasContext = new AnzasContext();
            _rockRepository = new RockRepository(anzasContext);
            Rocks = new ObservableCollection<Rock>(_rockRepository.GetAll());

            using (AnzasContext db = new AnzasContext())
            {
                var _rocksroute = db.RocksRoutes
               
                    .Include(item => item.GeologNavigation)
                    .Include(item=>item.TnOtborNavigation)
                    .Include(item=>item.TnTypeNavigation)
                    .Include(item => item.RockCodeNavigation).AsNoTracking().ToObservableCollection();
                Rocksroute = new ObservableCollection<RocksRoute>(_rocksroute);
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


        private void Test()
        {
            DialogManager dialogManager = new DialogManager();

            string message = null;
            if(SelectedRocks!= null)
            {
                 message = SelectedRocks.Hole.HoleId.ToString() + " скважина";
            }
            else if (SelectedRocksRoute!= null)
            {
                message = SelectedRocksRoute.HoleId.ToString() + " маршруты";
            }
            
            dialogManager.ShowMessage(message, "test", InfoMessege.OK, InfoMessege.Information);

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
           
            if (SelectedRocks != null)
            {
                AddWindow window = new AddWindow();
                window.DataContext = new AddEditWindowViewModel(NavigationKeys.RocksKey, SelectedRocks.Uid);
                if (window.ShowDialog() == false)
                {
                    Rocks.Clear();
                    Rocks = new ObservableCollection<Rock>(_rockRepository.GetAll());
                }
            }
            if (SelectedRocksRoute != null)
            {
                AddWindow window = new AddWindow();
                window.DataContext = new AddEditWindowViewModel(NavigationKeys.RocksRouteKey, SelectedRocksRoute.Uid);
                if (window.ShowDialog() == false)
                {
                    Rocksroute.Clear();
                    RefrashRockRoute();
                }
            }

        }
        /// <summary>
        /// Метод для добавления нового элемента
        /// </summary>
        private void OpenWindowAdd()
        {
            AddWindow window = new AddWindow();

            window.DataContext = new AddEditWindowViewModel(NavigationKeys.RocksKey);
            if (window.ShowDialog() == false)
            {
                Rocks.Clear();
                Rocks = new ObservableCollection<Rock>(_rockRepository.GetAll());
            }

        }
      
        private void RefrashRockRoute()
        {
            using (AnzasContext db = new AnzasContext())
            {
                var _rocksroute = db.RocksRoutes

                    .Include(item => item.GeologNavigation)
                    .Include(item => item.TnOtborNavigation)
                    .Include(item => item.TnTypeNavigation)
                    .Include(item => item.RockCodeNavigation).AsNoTracking().ToObservableCollection();
                Rocksroute = new ObservableCollection<RocksRoute>(_rocksroute);
            }
        }

    }
}
