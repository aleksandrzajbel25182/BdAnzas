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
using System.Windows;
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
        public InfoTrench SelectedInfoTrench
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
            DeleteCommand = new LamdaCommand(OnDeleteCommandExcuted, DeleteCommandExecute);
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
                if (SelectedInfoTrench != null)
                {
                    using (AnzasContext dbcontext = new AnzasContext())
                    {
                        InfoTrench row = dbcontext.InfoTrenches.FirstOrDefault(i => i.HoleId == SelectedInfoTrench.HoleId);
                        if (row is not null)
                        {
                            var strmessege = @$"Вы действительно хотите удалить: {SelectedInfoTrench.HoleId}";
                            var result = MessageBox.Show(strmessege, "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                            {

                                MessageBox.Show("Удаление прошло успешно");
                                dbcontext.InfoTrenches.Remove(row);
                                dbcontext.SaveChanges();
                                //InfoDrills.Remove(infodrills);
                                Refrash();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Не выбран объект для удаления", "Удаление", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }



        }
        private void Refrash()
        {
            using (AnzasContext dbcontext = new AnzasContext())
            {
                InfoTrench.Clear();
                InfoTrench = dbcontext.InfoTrenches
                   .Include(p => p.PlaceSiteNavigation)
                   .Include(item => item.TypeLcodeNavigation)
                   .Include(item => item.GeologNavigation)
                   .AsNoTracking().ToObservableCollection();
            }
        }
    }
}
