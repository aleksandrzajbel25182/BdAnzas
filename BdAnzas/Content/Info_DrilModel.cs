using Anzas.DAL;
using Anzas.DAL.Services;
using BdAnzas.Base;
using BdAnzas.Commands;
using BdAnzas.Content.Windows;
using Egor92.MvvmNavigation;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace BdAnzas.Content
{
    internal class Info_DrilModel : ViewModelBase
    {
        private readonly InfodrillRepository _infodrillRepository;
        private NavigationManager navigationManager;

        //private AnzasContext dbcontext;


        //private ObservableCollection<InfoDrill> _infodrill = new ObservableCollection<InfoDrill>();
        ///// <summary>
        ///// Информаиция по скважинам
        ///// </summary>
        //public ObservableCollection<InfoDrill> InfoDrills
        //{
        //    get => _infodrill;
        //    set => Set(ref _infodrill, value);
        //}

        ///// <summary>
        ///// Выбранный элемент скважин
        ///// </summary>
        //private InfoDrill? _selectedInfodril;
        //public InfoDrill? Selected_InfoDrill
        //{
        //    get => _selectedInfodril;
        //    set => Set(ref _selectedInfodril, value);
        //}

        private ObservableCollection<InfoDrill> _infodrill = new ObservableCollection<InfoDrill>();
        public ObservableCollection<InfoDrill> InfoDrills
        {
            get => _infodrill;
            set => SetProperty(ref _infodrill, value);
        }

        /// <summary>
        /// Выбранный элемент скважин
        /// </summary>
        private InfoDrill? _selectedInfodril;
        public InfoDrill? Selected_InfoDrill
        {
            get => _selectedInfodril;
            set => Set(ref _selectedInfodril, value);
        }

        public Info_DrilModel()
        {
        }
        public async Task LoadInfoDrillAsync()
        {
            var people = await Task.Run(()=>_infodrillRepository.GetAll());
            // Заполняем коллекцию People данными из базы данных
            InfoDrills = new ObservableCollection<InfoDrill>(people);
        }

        public Info_DrilModel(NavigationManager navigationManager, AnzasContext db)
        {
            _infodrillRepository = new InfodrillRepository(db);
            LoadInfoDrillAsync();
            //this.navigationManager = navigationManager;
            //this.dbcontext = db;

            //InfoDrills = dbcontext.InfoDrills
            //    .Include(p => p.PlaceSiteNavigation)
            //    .Include(item => item.TypeLcodeNavigation)
            //    .Include(item => item.GeologNavigation)
            //    .AsNoTracking().ToObservableCollection();

            //DeleteCommand = new LamdaCommand(OnDeleteCommandExcuted, DeleteCommandExecute);
            //AddWindowCommand = new LamdaCommand(OnAddWindowCommandExcuted, AddWindowCommandExecute);
        }
        public ICommand OpenItemCommand => new RelayCommand(OpenItem);

        private void OpenItem(object parameter)
        {
            // Получение выбранного элемента из параметра команды
            var selectedItem = parameter as MyModel;

            // Создание нового окна и передача выбранного элемента
            var window = new MyWindow(selectedItem);
            window.ShowDialog();
        }

        /*
        #region
        /// <summary>
        /// Команда удаления 
        /// </summary>
        public ICommand AddWindowCommand { get; }
        private bool AddWindowCommandExecute(object p) => true;
        private void OnAddWindowCommandExcuted(object p)
        {
            AddWindow window = new AddWindow();
            window.DataContext = new AddWindowViewModel();
            if (window.ShowDialog() == false)
            {
                Refrash();
            }
        }

       /// <summary>
       /// Команда удаления 
       /// </summary>
        public ICommand DeleteCommand { get; }
        private bool DeleteCommandExecute(object p) => true;
        private void OnDeleteCommandExcuted(object p)
        {

            try
            {
                InfoDrill infodrills = dbcontext.InfoDrills.FirstOrDefault(i => i.HoleId == Selected_InfoDrill.HoleId);
                if (infodrills is not null)
                {
                    dbcontext.InfoDrills.Remove(infodrills);
                    dbcontext.SaveChanges();
                    InfoDrills.Remove(infodrills);
                }             
               
                Refrash();
                MessageBox.Show("Удаление прошло успешно");

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
          


        }

        #endregion
        /// <summary>
        /// Очистка коллекции и заново чтение из базы данных.
        /// </summary>
        private void Refrash()
        {
            InfoDrills.Clear();
            InfoDrills = dbcontext.InfoDrills
               .Include(p => p.PlaceSiteNavigation)
               .Include(item => item.TypeLcodeNavigation)
               .Include(item => item.GeologNavigation)
               .AsNoTracking().ToObservableCollection();
        }
        */


    }
}
