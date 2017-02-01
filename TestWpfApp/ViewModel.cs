using DateTimePicker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestWpfApp
{
    /// <summary>
    /// Тестовая вьюмодель.
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        private DateTime _dateTimeForBuisnesLogic;

        public DateTime DateTimeForBuisnesLogic
        {
            get
            {
                return this._dateTimeForBuisnesLogic;
            }
            set
            {
                this._dateTimeForBuisnesLogic = value;
                this._OnPropertyChanged("DateTimeForBuisnesLogic");
                this._OnPropertyChanged("DateTimeForBuisnesLogicTxt");
            }
        }

        public String DateTimeForBuisnesLogicTxt
        {
            get
            {
                return this._dateTimeForBuisnesLogic.ToString(
                    "dd.MM.yyyy HH:mm:ss");
            }
        }

        public ViewModel()
        {
            this.SaveChangesCommand = new RelayCommand(
                this.SaveChanges,
                (obj) => true);
        }

        public ICommand SaveChangesCommand { get; set; }

        public void SaveChanges(object param)
        {
            // сохранить дату где-то:
            DateTime fakeDate = this.DateTimeForBuisnesLogic;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void _OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
