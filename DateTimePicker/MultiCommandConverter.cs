using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace ExtendedDateTimePicker
{
    public class MultiCommandConverter : IMultiValueConverter
    {
        private List<RelayCommand> _values = new List<RelayCommand>();

        private Action<object> GetCompoundExecute()
        {
            return (parameter) =>
                    {
                        foreach (RelayCommand command in _values)
                        {
                            if (command != null)
                                command.Execute(parameter);
                        }
                    };
        }

        public object Convert(
            object[] values, 
            Type targetType, 
            object parameter, 
            System.Globalization.CultureInfo culture)
        {
            foreach(object obj in values)
            {
                RelayCommand rc = obj as RelayCommand;
                this._values.Add(rc);
            }
            RelayCommand multiCommand = new RelayCommand(
                GetCompoundExecute(),
                (obj) => true);

            return multiCommand;
        }


        public object[] ConvertBack(
            object value, 
            Type[] targetTypes, 
            object parameter, 
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException(
                "Реализации метода обратной конвертации не предусмотрено.");
        }
    }
}
