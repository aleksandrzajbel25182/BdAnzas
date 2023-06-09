using Anzas.DAL;
using Anzas.DAL.Services;
using BdAnzas.Base;
using BdAnzas.Commands;
using BdAnzas.Constants;
using BdAnzas.Content.Windows;
using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BdAnzas.Content.ViewModel
{
    internal class Info_TrenchModel : ViewModelBase
    {
        private readonly InfoTrenchRepository _infoTrenchRepository;

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
            AnzasContext anzasContext = new AnzasContext();
            _infoTrenchRepository = new InfoTrenchRepository(anzasContext);
            InfoTrench = new ObservableCollection<InfoTrench>(_infoTrenchRepository.GetAll());

            

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
            if (SelectedInfoTrench != null)
            {
                window.DataContext = new AddEditWindowViewModel(NavigationKeys.InfoTrenchKey, SelectedInfoTrench.Uid);
                if (window.ShowDialog() == false)
                {
                    InfoTrench.Clear();
                    InfoTrench = new ObservableCollection<InfoTrench>(_infoTrenchRepository.GetAll());
                }
            }

        }
                /// <summary>
        /// Метод для добавления нового элемента
        /// </summary>
        private void OpenWindowAdd()
        {
            AddWindow window = new AddWindow();

            window.DataContext = new AddEditWindowViewModel(NavigationKeys.InfoTrenchKey);
            if (window.ShowDialog() == false)
            {
                InfoTrench.Clear();
                InfoTrench = new ObservableCollection<InfoTrench>(_infoTrenchRepository.GetAll());
            }

        }        

    }
}
