using BdAnzas.Base;
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

namespace BdAnzas.Content.Windows
{
    internal class AddWindowViewModel : ViewModelBase, INavigatedToAware
    {
        NavigationManager _navigationmaneger;


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
            set => Set(ref _selectedTable, value);
        }




        public AddWindowViewModel()
        {
            ContentControl = new AddInfoDrill_View();
            //1. Create navigation manager
            _navigationmaneger = new NavigationManager(ContentControl);
            //Регистрируем 
            _navigationmaneger.Register<AddInfoDrill_View>("Add_Dril", () => new AddInfoDrill_ViewModel(_navigationmaneger));
            _navigationmaneger.Navigate("Add_Dril");

            Tables = new ObservableCollection<Tables>()
            {
                new Tables(){Title = "Информация по скважинам (Info_Drill)"},
                 new Tables(){Title = "Информация по канавам (Info_Trench)" },
                  new Tables(){Title = "Информация по маршрутам (Info_Route)"}
            };
            OpenFileDialogCommand = new LamdaCommand(OnOpenFileDialogCommandExcuted, OpenFileDialogExecute);
            ImportCommand = new LamdaCommand(OnImportCommandExcuted, ImportCommandExecute);
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
            ImportFile(PathFile);
        }
        #endregion

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
                PathFile = @""+Path.GetFullPath(openFileDialog.FileName);
            }
        }


        private void ImportFile(string fileNames)
        {
            try
            {


                Excel ex = new Excel(@"C:\Users\ZAI\Desktop\Shablon.xlsx", "Лист 1");

               

                List<InfoDrill> list = new List<InfoDrill>();

                //list = ex.ReadExcelToDataTable();
                //ex.ImportFile(list)






                using (AnzasContext db = new AnzasContext())
                {



                    //db.InfoDrills.Add()

                    //db.Add(dt);



                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }



        public void OnNavigatedTo(object arg)
        {

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
