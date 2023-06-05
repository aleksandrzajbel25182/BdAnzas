using Anzas.DAL;
using BdAnzas.Base;
using BdAnzas.Commands;
using BdAnzas.Content.Windows;
using Egor92.MvvmNavigation;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace BdAnzas.Content
{
    internal class Info_DrilModel : ViewModelBase
    {
        private NavigationManager navigationManager;
        private AnzasContext dbcontext;


        private ObservableCollection<InfoDrill> _infodrill;
        /// <summary>
        /// Информаиция по скважинам
        /// </summary>
        public ObservableCollection<InfoDrill> InfoDrills
        {
            get => _infodrill;
            set => Set(ref _infodrill, value);
           
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

        public Info_DrilModel(NavigationManager navigationManager, AnzasContext db)
        {
            this.navigationManager = navigationManager;
            this.dbcontext = db;
          
            InfoDrills = new ObservableCollection<InfoDrill>();

            InfoDrills = dbcontext.InfoDrills
                .Include(p => p.PlaceSiteNavigation)
                .Include(item => item.TypeLcodeNavigation)
                .Include(item => item.GeologNavigation)
                .AsNoTracking().ToObservableCollection();

           


            DeleteCommand = new LamdaCommand(OnDeleteCommandExcuted, DeleteCommandExecute);
            AddWindowCommand = new LamdaCommand(OnAddWindowCommandExcuted, AddWindowCommandExecute);
            EditCommand = new LamdaCommand(OnEditCommandExcuted, EditCommandExecute);
           
           
        }
        #region команды
        /// <summary>
        /// Команда добавление новых элементов 
        /// </summary>
        public ICommand AddWindowCommand { get; }
        private bool AddWindowCommandExecute(object p) => true;
        private void OnAddWindowCommandExcuted(object p)
        {
           
            AddWindow window = new AddWindow();
            window.DataContext = new AddEditWindowViewModel("Информация по скважинам", true);
            if (window.ShowDialog() == false)
            {
                Refrash();
            }

        }
        /// <summary>
        /// Командра разрешения редактирования
        /// </summary>
        public ICommand EditCommand { get; }
        private bool EditCommandExecute(object p) => true;
        private void OnEditCommandExcuted(object p)
        {
            AddWindow window = new AddWindow();
            if (Selected_InfoDrill != null)
            {
                window.DataContext = new AddEditWindowViewModel("Информация по скважинам", false, Selected_InfoDrill.Uid);
                if (window.ShowDialog() == false)
                {
                    Refrash();
                }
            }
            else
            {
                MessageBox.Show("Необходимо выбрать из таблицы один элемент", "Ошибка редактирования", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                if (Selected_InfoDrill != null)
                {

                    InfoDrill infodrills = dbcontext.InfoDrills.FirstOrDefault(i => i.HoleId == Selected_InfoDrill.HoleId);
                    if (infodrills is not null)
                    {
                        var strmessege = @$"Вы действительно хотите удалить скважину: {Selected_InfoDrill.HoleId}";
                     var result = MessageBox.Show(strmessege, "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes) 
                        {

                            MessageBox.Show("Удаление прошло успешно");
                            dbcontext.InfoDrills.Remove(infodrills);
                            dbcontext.SaveChanges();
                            //InfoDrills.Remove(infodrills);
                            Refrash();
                        }
                    }


                    
                }
                else
                {
                    MessageBox.Show("Не выбран объект для удаления","Удаление",MessageBoxButton.OK,MessageBoxImage.Warning);
                }

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



    }
}
