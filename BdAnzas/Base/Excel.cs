using Anzas.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BdAnzas.Base
{
    internal class Excel
    {
        List<InfoDrill> infoDrill;

        private Application excelApp;
        private Workbook excelBook;
        private _Worksheet excelSheet;
        private Microsoft.Office.Interop.Excel.Range excelRange;
        private string filename;
        /// <summary>
        /// Имя листа
        /// </summary>
        private string sheetname;
        /// <summary>
        /// Строки
        /// </summary>
        private int rows;
        /// <summary>
        /// Колонки
        /// </summary>
        private int collumns;
        private int numbersheets;
        Dictionary<int, string> sheetsdictory = new Dictionary<int, string>()
        {
            [1] = "InfoDril",
            [2] = "Info_Trench",
            [3] = "Info_Route",
            [4] = "Survey",
            [5] = "Survey_Trench",
            [6] = "Rocks",
            [7] = "Rocks_Route",
            [8] = "Samples",

        };

        /// <summary>
        /// идентификатор скважины для литологии.
        /// </summary>
        private int holeidDrill;


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="FileName">Путь к файлу</param>
        /// <param name="SheetName">Необходимы лист в файле Excel</param>
        public Excel(string FileName, string SheetName)
        {
            this.filename = FileName;
            this.sheetname = SheetName;

        }

        /// <summary>
        /// Конструктор если получаем идентификтатор по скважине
        /// </summary>
        /// <param name="FileName">Путь к файлу</param>
        /// <param name="SheetName">Необходимы лист в файле Excel</param>
        /// <param name="id">Идентификатор скважины</param>
        public Excel(string FileName, string SheetName, int id)
        {
            this.filename = FileName;
            this.sheetname = SheetName;
            this.holeidDrill = id;
        }

        /// <summary>
        /// Читаем Excel-файл записываем данные DataTable
        /// </summary>
        public System.Data.DataTable ReadExcelToDataTable()
        {
            try
            {
                if (excelApp == null)
                {
                    excelApp = new Application();


                    //отркываем файл
                    excelBook = excelApp.Workbooks.Open(filename);
                   
                    //Из словаря получаем ключ который внесем потом в лист для его открытия
                    foreach (var item in sheetsdictory)
                    {
                        if (item.Value == sheetname)
                            numbersheets = item.Key;
                    }
                    //открываем лист
                    excelSheet = excelBook.Sheets[numbersheets];
                    excelRange = excelSheet.UsedRange;
                    //Получаем количество строк
                    rows = excelRange.Rows.Count;
                    //Получаем количество колонок
                    collumns = excelRange.Columns.Count;
                    //В данном цикле заголовки выступают в качестве название колонок в DataTable
                    System.Data.DataTable dt = new System.Data.DataTable();
                    for (int i = 1; i <= 1; i++)
                    {
                        for (int j = 1; j <= collumns; j++)
                        {
                            //write the console
                            dt.Columns.Add(excelRange.Cells[i, j].Value2.ToString());

                        }

                    }
                    DataRow row;
                    //Загружаем в Datatable строки из excel
                    for (int i = 2; i <= rows; i++)
                    {
                        row = dt.NewRow();
                        for (int j = 1; j <= collumns; j++)
                        {
                            //write the console
                            if (excelRange.Cells[i, j].Value == null)
                                row[j - 1] = null;
                            row[j - 1] = excelRange.Cells[i, j].Value2;
                        }
                        dt.Rows.Add(row);
                    }
                    // ЗАкрываем книгу и ексель
                    Marshal.ReleaseComObject(excelBook);
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                    return dt;
                }

            }
            catch (Exception)
            {
                return null;
                throw;
            }
           
            return null;
        }



        /// <summary>
        /// Конвертируем из DataTable в список класса InfoDrill
        /// </summary>
        /// <param name="tbl">Получаем DataTable</param>
        /// <returns>Возвращаем готовый список с данными по скважинам</returns>
        public List<InfoDrill> ConvertDT_InfoDrill(System.Data.DataTable tbl)
        {
            List<InfoDrill> results = new List<InfoDrill>();
            results = tbl.AsEnumerable().Select(i => new InfoDrill()
            {
                HoleId = i.Field<string>("HoleID"),
                TypeLcode = 1,// исправить
                PlaceSite = 1,// исправить
                Profile = Convert.ToDouble(i.Field<string>("Profile")),
                Easting = Convert.ToDouble(i.Field<string>("Easting")),
                Northing = Convert.ToDouble(i.Field<string>("Northing")),
                Elevation = Convert.ToDouble(i.Field<string>("Elevation")),
                Diam = Convert.ToDouble(i.Field<string>("Diam")),
                Azimuth = Convert.ToDouble(i.Field<string>("Azimuth")),
                Dip = Convert.ToDouble(i.Field<string>("Dip")),
                Depth = Convert.ToDouble(i.Field<string>("Depth")),
                Uroven = Convert.ToDouble(i.Field<string>("Uroven")),
                UrAbs = Convert.ToDouble(i.Field<string>("UR_ABS")),
                StartDate = new DateOnly(2023, 5, 10), // 
                EndDate = new DateOnly(2023, 5, 10), //
                Geolog = 1, // ispraviti
                NotesCommentsText = i.Field<string>("NOTES (COMMENTS, TEXT)")

            }).ToList();

            return results;
        }


        public bool SaveBase()
        {
            using (AnzasContext db = new AnzasContext())
            {

                switch (sheetname)
                {
                    case "Rocks":

                        foreach (var item in ConvertDT_Rock(ReadExcelToDataTable(), holeidDrill))
                        {
                            db.Rocks.Add(item);
                        }
                        break;
                    default:
                        return false;
                }               
                db.SaveChanges();
            }
            return true;
        }

      

        public List<Rock> ConvertDT_Rock(System.Data.DataTable tbl, int holeid)
        {
            List<Rock> results = new List<Rock>();
            List<RockCode> rockCodes = new List<RockCode>();

            results = tbl.AsEnumerable().Select(i => new Rock()
            {

                HoleId = holeid,
                Profile = holeid,
                From = Convert.ToDouble(i.Field<string>("From")),
                To = Convert.ToDouble(i.Field<string>("To")),
                Length = Convert.ToDouble(i.Field<string>("Length")),
                Kernm = Convert.ToDouble(i.Field<string>("KERNM")),
                Kernpc = Convert.ToDouble(i.Field<string>("KERNPC")),
                RockCode = 1, ///ПРИДУМАТЬ
                Description = i.Field<string>("Description"),
                Geolog = 1,//ПРИДУМАТЬ
                Petro = i.Field<string>("PETRO"),
                Mineral = i.Field<string>("MINERAL"),
                NotesCommentsText = i.Field<string>("NOTES (COMMENTS, TEXT)"),


            }).ToList();
            //доделать
            using (AnzasContext db = new AnzasContext())
            {
                var maxuid = db.Rocks.Max(u => u.Uid);
                foreach (var item in results)
                {
                    item.Uid = maxuid + 1;
                    maxuid++;
                }
            }

            return results;
        }
       








    }
}
