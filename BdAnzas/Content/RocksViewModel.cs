using Anzas.DAL;
using BdAnzas.Base;
using BdAnzas.Commands;
using BdAnzas.Content.Windows;
using Egor92.MvvmNavigation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BdAnzas.Content
{
    internal class RocksViewModel : ViewModelBase
    {

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
        private NavigationManager navigationmaneger;

        public Rock SelectedRocks
        {
            get => _selectedRocks;
            set => Set(ref _selectedRocks, value);
        }





        public RocksViewModel(NavigationManager navigationmaneger)
        {
            this.navigationmaneger = navigationmaneger;
            using (AnzasContext db = new AnzasContext())
            {
                Rocks = db.Rocks.Include(item => item.GeologNavigation)
                    .Include(item => item.RockCodeNavigation)
                .AsNoTracking().ToObservableCollection();
            }

            DeleteCommand = new LamdaCommand(OnDeleteCommandExcuted, DeleteCommandExecute);
            AddWindowCommand = new LamdaCommand(OnAddWindowCommandExcuted, AddWindowCommandExecute);
        }


        /// <summary>
        /// Команда добавления 
        /// </summary>
        public ICommand AddWindowCommand { get; }
        private bool AddWindowCommandExecute(object p) => true;
        private void OnAddWindowCommandExcuted(object p)
        {
            AddWindow window = new AddWindow();
            window.DataContext = new AddEditWindowViewModel("Литология" , true);
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
            using (AnzasContext db = new AnzasContext())
            {


                try
                {
                    if (SelectedRocks != null)
                    {

                        Rock rocks = db.Rocks.FirstOrDefault(i => i.Uid == SelectedRocks.Uid);
                        if (rocks is not null)
                        {
                            db.Rocks.Remove(rocks);

                        }
                        db.SaveChanges();
                        Refrash();
                        MessageBox.Show("Удаление прошло успешно");
                    }
                    else
                    {
                        MessageBox.Show("Выделите строку для удаления");
                    }

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    throw;
                }
            }
        }


        private void Refrash()
        {
            Rocks.Clear();
            using (AnzasContext db = new AnzasContext())
            {
                Rocks = db.Rocks.Include(item => item.GeologNavigation)
                    .Include(item => item.RockCodeNavigation)
                .AsNoTracking().ToObservableCollection();
            }
        }


    }
}
