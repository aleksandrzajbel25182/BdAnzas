using Anzas.DAL;
using BdAnzas.Base;
using BdAnzas.Content.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdAnzas.Infrastructure
{
    class SearchManager : ViewModelBase, ISearchManager
    {
        

        /// <summary>
        /// Метод получения значения свойства по имени колонки
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string GetColumnValue<T>(T item, string columnName)
        {
            // Используем рефлексию для получения значения свойства по имени колонки
            var property = item.GetType().GetProperty(columnName);

            if (property != null)
            {
                var value = property.GetValue(item, null);
                if (value != null)
                {
                    return value.ToString();
                }
            }
            // Возвращаем пустую строку в случае, если свойство не найдено или значение null
            return string.Empty;
        }

        public ObservableCollection<T> SearchFilter<T>(ObservableCollection<T> collection, string searchText, string selectedColumn)
        {

            var FilterColler = collection.Where(item => GetColumnValue(item, selectedColumn).Contains(searchText, StringComparison.OrdinalIgnoreCase));

            return FilterColler.ToObservableCollection();
        }
    }


}

