using Anzas.DAL;
using Anzas.DAL.Services;
using BdAnzas.Base;
using BdAnzas.Commands;
using BdAnzas.Constants;
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

namespace BdAnzas.Content.ViewModel
{
    internal class Info_DrilModel : ViewModelBase
    {
        private readonly InfodrillRepository _infodrillRepository;
        private AnzasContext db;


        private ObservableCollection<InfoDrill> _infodrill = new ObservableCollection<InfoDrill>();
        public ObservableCollection<InfoDrill> InfoDrills
        {
            get => _infodrill;
            set => SetProperty(ref _infodrill, value);
        }

        /// <summary>
        /// Выбранный элемент скважин
        /// </summary>
        private InfoDrill _selectedInfodril;
        public InfoDrill Selected_InfoDrill
        {
            get => _selectedInfodril;
            set => Set(ref _selectedInfodril, value);
        }

        public Info_DrilModel()
        {
        }


        public Info_DrilModel(AnzasContext _db)
        {
            db = _db;
            _infodrillRepository = new InfodrillRepository(db);
            InfoDrills = new ObservableCollection<InfoDrill>(_infodrillRepository.GetAll());

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
            if (Selected_InfoDrill != null)
            {
                window.DataContext = new AddEditWindowViewModel(NavigationKeys.InfoDrillKey, Selected_InfoDrill.Uid);
                if (window.ShowDialog() == false)
                {
                    InfoDrills.Clear();
                    InfoDrills = new ObservableCollection<InfoDrill>(_infodrillRepository.GetAll());
                }
            }

        }

        /// <summary>
        /// Метод для добавления нового элемента
        /// </summary>
        private void OpenWindowAdd()
        {
            AddWindow window = new AddWindow();

            window.DataContext = new AddEditWindowViewModel(NavigationKeys.InfoDrillKey);
            if (window.ShowDialog() == false)
            {
                InfoDrills.Clear();
                InfoDrills = new ObservableCollection<InfoDrill>(_infodrillRepository.GetAll());
            }

        }

    }


}

