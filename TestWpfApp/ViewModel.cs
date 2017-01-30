using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestWpfApp
{
    /// <summary>
    /// Тестовая вьюмодель.
    /// </summary>
    public class ViewModel
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
            }
        }

        public ViewModel()
        {
            this.SaveChangesCommand = new XCommand(this.SaveChanges);
        }

        public ICommand SaveChangesCommand { get; set; }

        public void SaveChanges()
        {
            // сохранить дату где-то:
            DateTime fakeDate = this.DateTimeForBuisnesLogic;
        }

    }
}
