﻿using BdAnzas.Base;
using BdAnzas.Commands;
using Egor92.MvvmNavigation;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Egor92.MvvmNavigation.Abstractions;
using Anzas.DAL;
using System.IO;
using System.Collections.Generic;
using BdAnzas.Content.Windows.Add;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BdAnzas.Content.Windows
{
    internal class AddEditWindowViewModel : ViewModelBase
    {
        NavigationManager _navigationmaneger;

        /// <summary>
        /// Включаем отключаем видимость
        /// </summary>
        private bool _isVisibility;
        public bool IsVisibility
        {
            get => _isVisibility;
            set => Set(ref _isVisibility, value);
        }

        /// <summary>
        /// Шапка окна
        /// </summary>
        private string _title;
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        private string _pathFile;
        public string PathFile
        {
            get => _pathFile;
            set => Set(ref _pathFile, value);
        }

        /// <summary>
        /// Контрол для отображения страниц
        /// </summary>
        private ContentControl _contentcontrol;
        public ContentControl ContentControl
        {
            get => _contentcontrol;
            set
            {
                _contentcontrol = value;
                OnPropertyChanged("ContentControl");
            }
        }

        private ObservableCollection<Tables> _tables;
        public ObservableCollection<Tables> Tables
        {
            get => _tables;
            set => Set(ref _tables, value);
        }

        private Tables _selectedTable;

        public Tables SeletedTable
        {
            get => _selectedTable;
            set
            {
                _selectedTable = value;
                OnPropertyChanged("SeletedTable");
                OnPropertyChanged("SubmitEnabled");
            }
        }

        private ObservableCollection<Skvagins> _skvagina;
        /// <summary>
        /// Для выпадающего списка Скважин.
        /// </summary>
        public ObservableCollection<Skvagins> Skvagina
        {
            get => _skvagina;
            set => Set(ref _skvagina, value);
        }
        private Skvagins _selectedSkvagina;
        public Skvagins SelectedSkvagina
        {
            get => _selectedSkvagina;
            set => Set(ref _selectedSkvagina, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">Страница которую нужно вывести</param>
        /// <param name="edit">Режим редактирования/Добавления. True - добавление. False-редактирование</param>
        public AddEditWindowViewModel(string page, bool edit)
        {
            ContentControl = new AddInfoDrill_View();

            _navigationmaneger = new NavigationManager(ContentControl);
            //Регистрируем 
            _navigationmaneger.Register<AddInfoDrill_View>("Add_Dril", () => new AddIEditnfoDrill_ViewModel());
            _navigationmaneger.Register<AddRocksView>("Add_Rock", () => new AddRocksViewModel());

            IsVisibility = edit;
            Title = page;

            NavigatePage(page);

            Skvagina = new ObservableCollection<Skvagins>();
            using (AnzasContext db = new AnzasContext())
            {
                var drills = db.InfoDrills.AsNoTracking().ToObservableCollection();
                foreach (var item in drills)
                {
                    Skvagina.Add(new Skvagins { Uid = item.Uid, Title = item.HoleId });
                }

            }


            Tables = new ObservableCollection<Tables>()
            {
                new Tables(){Title = "Информация по скважинам (Info_Drill)"},
                 new Tables(){Title = "Информация по канавам (Info_Trench)" },
                  new Tables(){Title = "Информация по маршрутам (Info_Route)"},
                   new Tables(){Title = "Литология(Rocks)"}
            };

            OpenFileDialogCommand = new LamdaCommand(OnOpenFileDialogCommandExcuted, OpenFileDialogExecute);
            ImportCommand = new LamdaCommand(OnImportCommandExcuted, ImportCommandExecute);



        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">Страница которую нужно вывести</param>
        /// <param name="edit">Режим редактирования/Добавления. True - добавление. False-редактирование</param>
        /// <param name="id">Идентификатор таблицы</param>
        public AddEditWindowViewModel(string page, bool edit , int id)
        {
            ContentControl = new AddInfoDrill_View();

            _navigationmaneger = new NavigationManager(ContentControl);
            //Регистрируем 
            _navigationmaneger.Register<AddInfoDrill_View>("Add_Dril", () => new AddIEditnfoDrill_ViewModel(id));
            _navigationmaneger.Register<AddRocksView>("Add_Rock", () => new AddRocksViewModel(id));

            IsVisibility = edit;
            Title = page;

            NavigatePage(page);



        }
        #region Commands

      
        /// <summary>
        /// Команда выбора файла для импорта 
        /// </summary>
        public ICommand OpenFileDialogCommand { get; }
        private bool OpenFileDialogExecute(object p) => true;
        private void OnOpenFileDialogCommandExcuted(object p)
        {
            OpenFile();
        }

        /// <summary>
        /// Команда импорта
        /// </summary>
        public ICommand ImportCommand { get; }
        private bool ImportCommandExecute(object p) => true;
        private void OnImportCommandExcuted(object p)
        {

            if (ImportFile())
            {
                MessageBox.Show("Импорт прошел без ошибок");
            }

        }
        #endregion

        private void NavigatePage(string _page)
        {
            switch (_page)
            {
                case "Информация по скважинам":
                    _navigationmaneger.Navigate("Add_Dril");

                    break;

                case "Литология":
                    _navigationmaneger.Navigate("Add_Rock");
                    break;

                default:
                    break;
            }
        }


        /// <summary>
        /// Метод проверяющий была ли выбрана таблица Rocks
        /// </summary>
        public bool SubmitEnabled
        {
            get
            {
                if (SeletedTable != null)
                {
                    if (SeletedTable.Title == "Литология(Rocks)")
                    {
                        return true;
                    }
                }
                else return false;
                return false;
            }
        }


        /// <summary>
        /// Функция выбора файла для импорта
        /// </summary>
        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Книга Excel(*.xlsx)|*.xlsx|Книга Excel 97-2003(*.xls)|.xls";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                PathFile = @"" + Path.GetFullPath(openFileDialog.FileName);
            }
        }


        private bool ImportFile()
        {
            try
            {


                switch (SeletedTable.Title)
                {
                    case "Информация по скважинам(Info_Drill)":
                        MessageBox.Show("В разарботке");
                        break;
                    case "Информация по канавам (Info_Trench)":
                        MessageBox.Show("В разарботке");
                        break;
                    case "Информация по маршрутам (Info_Route)":
                        MessageBox.Show("В разарботке");
                        break;
                    case "Литология(Rocks)":
                        Excel ex = new Excel(PathFile, "Rocks", SelectedSkvagina.Uid);
                        ex.SaveBase();
                        return true;
                        break;

                    default:
                        return false;

                }

                return false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }
            return false;

        }

    }

    public class Skvagins
    {
        private int uid;

        public int Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

    }

    public class Tables
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

    }
}
