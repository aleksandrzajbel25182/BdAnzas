using Anzas.DAL;
using Anzas.DAL.Services;
using BdAnzas.Base;
using BdAnzas.Commands;
using BdAnzas.Constants;
using BdAnzas.Content.Windows;
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





        public RocksViewModel()
        {

            AnzasContext anzasContext = new AnzasContext();
              _rockRepository= new RockRepository(anzasContext);
            Rocks = new ObservableCollection<Rock>(_rockRepository.GetAll());

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
            if (SelectedRocks != null)
            {
                window.DataContext = new AddEditWindowViewModel(NavigationKeys.RocksKey, SelectedRocks.Uid);
                if (window.ShowDialog() == false)
                {
                    Rocks.Clear();
                    Rocks = new ObservableCollection<Rock>(_rockRepository.GetAll());
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


    }
}
