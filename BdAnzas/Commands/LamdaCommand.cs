using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdAnzas.Commands
{
    internal class LamdaCommand : Command
    {
        private readonly Action<object> _Exucete;
        private readonly Func<object, bool> _CanExecute;

        public LamdaCommand(Action<object> Exucete, Func<object, bool> CanExecute = null)
        {
            _Exucete = Exucete ?? throw new ArgumentNullException(nameof(Exucete));
            _CanExecute = CanExecute;
        }

        public override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => _Exucete(parameter);
    }
}
