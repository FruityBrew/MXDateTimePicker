using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TestExtendedWPFToolkit
{
    public class ViewModel : INotifyPropertyChanged
    {
        private DateTime _dateTime;

        private Timer _timer;

        public DateTime DT 
        {
            get
            {
                return this._dateTime;
            }
            set
            {
                this._dateTime = value;
                this.OnPropertyChanged("DT");
            }
        }

        public ViewModel()
        {
            this._dateTime = DateTime.Now;
            this._timer = new Timer(1000);
            this._timer.Elapsed += this._OnTimeEvent;
            this._timer.Start();
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(
                    this,
                    new PropertyChangedEventArgs(propertyName));
        }

        private void _OnTimeEvent(Object source, ElapsedEventArgs arg)
        {
            this.DT = DateTime.Now;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
