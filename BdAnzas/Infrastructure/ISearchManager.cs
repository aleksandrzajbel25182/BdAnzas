using BdAnzas.Content.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BdAnzas.Infrastructure
{
    public interface ISearchManager
    {
        string GetColumnValue<T>(T item, string columnName);
        ObservableCollection<T> SearchFilter<T>(ObservableCollection<T> collection ,string searchText, string selectedColumn);
    }
}
