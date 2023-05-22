using Anzas.DAL;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;

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
        List<InfoDrill> info = new List<InfoDrill>();



        public Excel(string FileName, string SheetName)
        {
            this.filename = FileName;
            this.sheetname = SheetName;
           
        }



        //public void ImportFile()
        //{
        //    info = new List<InfoDrill>();

            
        //    for (int i = 2; i <= rows; i++)
        //    {                
        //        for (int j = 1; j <= collumns; j++)
        //        {                   
        //            //write the console
        //            if (excelRange.Cells[i, j].Value == null)
        //                Console.Write("null");
        //            Console.Write(excelRange.Cells[i, j].Value + " | ");
                
        //        }
                
        //    }

        //    //after reading, relaase the excel project
        //    excelApp.Quit();
        //    Marshal.ReleaseComObject(excelApp);

        //    //    return dt;

        //}

        public void ReadExcelToDataTable()
        {
            excelApp = new Application();
           
            if (excelApp == null)
            {
                /// сделать вывод ошибки
                Console.WriteLine("Excel is not installed!!");
                return;
            }
            //отркываем файл
            excelBook = excelApp.Workbooks.Open(filename);
            //открываем лист
            excelSheet = excelBook.Sheets[1];
            excelRange = excelSheet.UsedRange;

            rows = excelRange.Rows.Count;
            collumns = excelRange.Columns.Count;
            
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
            for (int i = 2; i <= rows; i++)
            {
                row = dt.NewRow();
                for (int j = 1; j <= collumns; j++)
                {
                    //write the console
                    if (excelRange.Cells[i, j].Value == null)
                        row[j-1] = null;
                    row[j-1] = excelRange.Cells[i, j].Value2;

                }
                dt.Rows.Add(row);

            }

            
            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp);

            
        }

        


        public List<InfoDrill> ConvertDataTable(System.Data.DataTable tbl)
        {
            List<InfoDrill> results = new List<InfoDrill>();
            results = tbl.AsEnumerable().Select(i=> new InfoDrill()
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
                StartDate = new DateOnly(2023, 5, 10) , // 
                EndDate = new DateOnly(2023, 5, 10), //
                Geolog = 1, // ispraviti
                NotesCommentsText = i.Field<string>("NOTES (COMMENTS, TEXT)")

            }).ToList();
             
            return results;
        }
        









    }
}
