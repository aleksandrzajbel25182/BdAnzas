using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BdAnzas.Base
{
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? PropertyName = null)
        {
            // Если значение поля которое хотим обновить уже соответсвует тому значение которое мы передали возвращаем ложь
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        #region Виртуальные защищённые методы для изменения значений свойств
        /// <summary>Виртуальный метод определяющий изменения в значении поля значения свойства</summary>
        /// <param name="fieldProperty">Ссылка на поле значения свойства</param>
        /// <param name="newValue">Новое значение</param>
        /// <param name="propertyName">Название свойства</param>
        protected virtual void SetProperty<T>(ref T fieldProperty, T newValue, [CallerMemberName] string propertyName = "")
        {
            if ((fieldProperty != null && !fieldProperty.Equals(newValue)) || (fieldProperty == null && newValue != null))
                PropertyNewValue(ref fieldProperty, newValue, propertyName);
        }

        /// <summary>Виртуальный метод изменяющий значение поля значения свойства</summary>
        /// <param name="fieldProperty">Ссылка на поле значения свойства</param>
        /// <param name="newValue">Новое значение</param>
        /// <param name="propertyName">Название свойства</param>
        protected virtual void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
        {
            fieldProperty = newValue;
            OnPropertyChanged(propertyName);
        }
        #endregion


    }
}
