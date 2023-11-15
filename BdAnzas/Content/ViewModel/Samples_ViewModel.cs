using Anzas.DAL;
using BdAnzas.Base;
using BdAnzas.Commands;
using BdAnzas.Infrastructure;
using MathCore.CSV;
using MathCore.PE;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static MathCore.Values.CSV;

namespace BdAnzas.Content.ViewModel
{
    internal class Samples_ViewModel : ViewModelBase
    {
        private SearchManager searchManager;

        private ObservableCollection<ISample> _samples;

        public ObservableCollection<ISample> Samples
        {
            get => _samples;
            set => Set(ref _samples, value);
        }

        private ISample _selectedSamples;

        public ISample SelectedSamples
        {
            get => _selectedSamples;
            set => Set(ref _selectedSamples, value);
        }


        //Конструктор
        public Samples_ViewModel()
        {
            searchManager = new SearchManager();
            //ColumnNames = new ObservableCollection<GroupColumn>( new GroupColumn
            //{

            //})
            //{
            //     "Uid",
            //    "HoleId",
            //    "From",
            //    "To",
            //    "Length",
            //    "GeoSample",
            //    "RockCode",
            //    "Weight",
            //    "SampleType",
            //    "Geolog",
            //    "NotesCommentsText",
            //    "TypeHole",
            //    "Profile",
            //};
            ColumnNames = new ObservableCollection<GroupColumn>()
            {
                     new GroupColumn{ Id = "Uid", NameColumn = "ID" },
                     new GroupColumn{ Id= "HoleId", NameColumn = "№ Выработки"},
                     new GroupColumn{ Id = "From", NameColumn = "От" },
                     new GroupColumn{ Id= "To", NameColumn = "До"},
                     new GroupColumn{ Id = "Length", NameColumn = "Длина интервала" },
                     new GroupColumn{ Id= "GeoSample", NameColumn = "Геологический номер"},
                     new GroupColumn{ Id = "RockCode", NameColumn = "Порода" },
                     new GroupColumn{ Id= "Weight", NameColumn = "Вес пробы"},
                     new GroupColumn{ Id = "SampleType", NameColumn = "Тип пробы" },
                     new GroupColumn{ Id= "Geolog", NameColumn = "Геолог"},
                     new GroupColumn{ Id= "NotesCommentsText", NameColumn = "Комментарии"},
                     new GroupColumn{ Id= "TypeHole", NameColumn = "Тип выработки"},
                     new GroupColumn{ Id= "Profile", NameColumn = "Профиль"},
            };


            _samples = new ObservableCollection<ISample>();
            using (AnzasContext db = new AnzasContext())
            {
                List<ISample> VrList = new List<ISample>();

                var collection = db.Samples.AsNoTracking().ToObservableCollection();

                foreach (var item in collection)
                {
                    if (item.TypeHole == 1)
                    {
                        VrList.Add(new ISample
                        {
                            Uid = item.Uid,
                            HoleId = db.InfoDrills.Where(x => x.Uid == item.HoleId).Select(h => h.HoleId).SingleOrDefault(),
                            Profile = db.InfoDrills.Where(x => x.Uid == item.HoleId).Select(p => p.Profile).SingleOrDefault(),
                            From = item.From,
                            To = item.To,
                            Length = item.Length,
                            GeoSample = item.GeoSample,
                            RockCode = db.RockCodes.Where(x => x.Uid == item.RockCode).Select(n => n.RockCode1).SingleOrDefault(),
                            Weight = item.Weight,
                            SampleType = db.SampleTypes.Where(s => s.Uid == item.SampleType).Select(s => s.TypeLcode).SingleOrDefault(),
                            Geolog = db.People.Where(p => p.Uid == item.Geolog).Select(p => p.Surname).SingleOrDefault(),
                            NotesCommentsText = item.NotesCommentsText
                        });
                    }
                    else if (item.TypeHole == 2)
                    {
                        VrList.Add(new ISample
                        {
                            Uid = item.Uid,
                            HoleId = db.InfoTrenches.Where(x => x.Uid == item.HoleId).Select(h => h.HoleId).SingleOrDefault(),
                            Profile = db.InfoTrenches.Where(x => x.Uid == item.HoleId).Select(p => p.Profile).SingleOrDefault(),
                            From = item.From,
                            To = item.To,
                            Length = item.Length,
                            GeoSample = item.GeoSample,
                            RockCode = db.RockCodes.Where(x => x.Uid == item.RockCode).Select(n => n.RockCode1).SingleOrDefault(),
                            Weight = item.Weight,
                            SampleType = db.SampleTypes.Where(s => s.Uid == item.SampleType).Select(s => s.TypeLcode).SingleOrDefault(),
                            Geolog = db.People.Where(p => p.Uid == item.Geolog).Select(p => p.Surname).SingleOrDefault(),
                            NotesCommentsText = item.NotesCommentsText
                        });
                    }
                    else if (item.TypeHole == 3)
                    {
                        VrList.Add(new ISample
                        {
                            Uid = item.Uid,
                            HoleId = db.InfoRoutes.Where(x => x.Uid == item.HoleId).Select(h => h.HoleId).SingleOrDefault(),

                            From = item.From,
                            To = item.To,
                            Length = item.Length,
                            GeoSample = item.GeoSample,
                            RockCode = db.RockCodes.Where(x => x.Uid == item.RockCode).Select(n => n.RockCode1).SingleOrDefault(),
                            Weight = item.Weight,
                            SampleType = db.SampleTypes.Where(s => s.Uid == item.SampleType).Select(s => s.TypeLcode).SingleOrDefault(),
                            Geolog = db.People.Where(p => p.Uid == item.Geolog).Select(p => p.Surname).SingleOrDefault(),
                            NotesCommentsText = item.NotesCommentsText
                        });
                    }
                }
                Samples = VrList.ToObservableCollection();
            }

        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { Set(ref _searchText, value); }
        }


        public ObservableCollection<GroupColumn> _columnNames;
        public ObservableCollection<GroupColumn> ColumnNames
        {
            get => _columnNames;
            set => Set(ref _columnNames, value);
        }
        public GroupColumn _selectedColumn;
        public GroupColumn SelectedColumn
        {
            get => _selectedColumn;
            set => Set(ref _selectedColumn, value);
        }


        private void Refrash()
        {
            _samples.Clear();
            using (AnzasContext db = new AnzasContext())
            {
                List<ISample> VrList = new List<ISample>();

                var collection = db.Samples.AsNoTracking().ToObservableCollection();

                foreach (var item in collection)
                {
                    if (item.TypeHole == 1)
                    {
                        VrList.Add(new ISample
                        {
                            Uid = item.Uid,
                            HoleId = db.InfoDrills.Where(x => x.Uid == item.HoleId).Select(h => h.HoleId).SingleOrDefault(),
                            Profile = db.InfoDrills.Where(x => x.Uid == item.HoleId).Select(p => p.Profile).SingleOrDefault(),
                            From = item.From,
                            To = item.To,
                            Length = item.Length,
                            GeoSample = item.GeoSample,
                            RockCode = db.RockCodes.Where(x => x.Uid == item.RockCode).Select(n => n.RockCode1).SingleOrDefault(),
                            Weight = item.Weight,
                            SampleType = db.SampleTypes.Where(s => s.Uid == item.SampleType).Select(s => s.TypeLcode).SingleOrDefault(),
                            Geolog = db.People.Where(p => p.Uid == item.Geolog).Select(p => p.Surname).SingleOrDefault(),
                            NotesCommentsText = item.NotesCommentsText
                        });
                    }
                    else if (item.TypeHole == 2)
                    {
                        VrList.Add(new ISample
                        {
                            Uid = item.Uid,
                            HoleId = db.InfoTrenches.Where(x => x.Uid == item.HoleId).Select(h => h.HoleId).SingleOrDefault(),
                            Profile = db.InfoTrenches.Where(x => x.Uid == item.HoleId).Select(p => p.Profile).SingleOrDefault(),
                            From = item.From,
                            To = item.To,
                            Length = item.Length,
                            GeoSample = item.GeoSample,
                            RockCode = db.RockCodes.Where(x => x.Uid == item.RockCode).Select(n => n.RockCode1).SingleOrDefault(),
                            Weight = item.Weight,
                            SampleType = db.SampleTypes.Where(s => s.Uid == item.SampleType).Select(s => s.TypeLcode).SingleOrDefault(),
                            Geolog = db.People.Where(p => p.Uid == item.Geolog).Select(p => p.Surname).SingleOrDefault(),
                            NotesCommentsText = item.NotesCommentsText
                        });
                    }
                    else if (item.TypeHole == 3)
                    {
                        VrList.Add(new ISample
                        {
                            Uid = item.Uid,
                            HoleId = db.InfoRoutes.Where(x => x.Uid == item.HoleId).Select(h => h.HoleId).SingleOrDefault(),

                            From = item.From,
                            To = item.To,
                            Length = item.Length,
                            GeoSample = item.GeoSample,
                            RockCode = db.RockCodes.Where(x => x.Uid == item.RockCode).Select(n => n.RockCode1).SingleOrDefault(),
                            Weight = item.Weight,
                            SampleType = db.SampleTypes.Where(s => s.Uid == item.SampleType).Select(s => s.TypeLcode).SingleOrDefault(),
                            Geolog = db.People.Where(p => p.Uid == item.Geolog).Select(p => p.Surname).SingleOrDefault(),
                            NotesCommentsText = item.NotesCommentsText
                        });
                    }
                }
                Samples = VrList.ToObservableCollection();
            }

        }


        #region search 
        //private ObservableCollection<ISample> _filteredItems;
        //public ObservableCollection<ISample> FilteredItems
        //{
        //    get { return _filteredItems; }
        //    set { Set(ref _filteredItems, value); }
        //}

        //private string GetColumnValue(ISample item, string columnName)
        //{
        //    // Используем рефлексию для получения значения свойства по имени колонки
        //    var property = item.GetType().GetProperty(columnName);

        //    if (property != null)
        //    {
        //        var value = property.GetValue(item, null);
        //        if (value != null)
        //        {
        //            return value.ToString();
        //        }
        //    }

        //    // Возвращаем пустую строку в случае, если свойство не найдено или значение null
        //    return string.Empty;
        //}



        //private void Search()
        //{
        //    FilteredItems = new ObservableCollection<ISample>(
        //        _samples.Where(item => GetColumnValue(item, SelectedColumn).Contains(SearchText, StringComparison.OrdinalIgnoreCase))
        //    );


        //    Samples = FilteredItems;
        //}
        #endregion

        private ICommand _refrashCommand;
        public ICommand RefrashCommand
        {
            get
            {
                if (_refrashCommand == null)
                {
                    _refrashCommand = new RelayCommand(param => Refrash());
                }
                return _refrashCommand;
            }
        }

        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(param => Samples = searchManager.SearchFilter(Samples, SearchText, SelectedColumn.Id));
                }
                return _searchCommand;
            }
        }
    }

    public class ISample
    {
        public int Uid { get; set; }

        /// <summary>
        /// № выработки
        /// </summary>
        public string? HoleId { get; set; }

        /// <summary>
        /// От
        /// </summary>
        public double? From { get; set; }

        /// <summary>
        /// До
        /// </summary>
        public double? To { get; set; }

        /// <summary>
        /// Длина интервала
        /// </summary>
        public double? Length { get; set; }

        /// <summary>
        /// Геологический номер
        /// </summary>
        public string? GeoSample { get; set; }

        /// <summary>
        /// Порода
        /// </summary>
        public string? RockCode { get; set; }

        /// <summary>
        /// Вес пробы
        /// </summary>
        public double? Weight { get; set; }

        /// <summary>
        /// Тип пробы
        /// </summary>
        public string? SampleType { get; set; }

        /// <summary>
        /// Геолог
        /// </summary>
        public string? Geolog { get; set; }

        /// <summary>
        /// Комментарии
        /// </summary>
        public string? NotesCommentsText { get; set; }

        /// <summary>
        /// Тип выработки (скважина/траншея/маршрут)
        /// </summary>
        public double TypeHole { get; set; }

        public double? Profile { get; set; }

    }
}
