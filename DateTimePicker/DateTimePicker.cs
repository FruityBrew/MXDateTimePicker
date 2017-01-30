using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Xceed.Wpf.Toolkit;

namespace DateTimePicker
{
    public class DateTimePicker : DateTimeUpDown
    {
         #region Constants

        //private const string PART_TimeUpDown = "PART_TimeUpDown";

        #endregion Constants

        #region Static

        #region Properties

        public static DependencyProperty CurrentVal;

        #endregion Properties

        #region Constructors

        static DateTimePicker()
        {
            // предоставление стиля по умолчанию:
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(DateTimePicker),
                new FrameworkPropertyMetadata(typeof(DateTimePicker)));

            CurrentVal = DependencyProperty.Register(
                "CurVal", 
                typeof(DateTime), 
                typeof(DateTimePicker));
        }
        #endregion Constructors


        #endregion Static

        #region Fields

        private Timer.Timer _timeUpdater;

        private DateTime _currentDateTime;

        #endregion Fields

        #region Properties

        public DateTime CurVal
        {
            get { return (DateTime)GetValue(CurrentVal); }
            set { SetValue(CurrentVal, value); }
        }
        
        public DateTime CurrentDateTime
        {
            get
            {
                return this._currentDateTime;
            }
            set
            {
                this._currentDateTime = value;
            }
        }

        #endregion Properties

        #region Constructors
        public DateTimePicker() : base()
        {
            this._currentDateTime = DateTime.Now;
            this.CurVal = DateTime.Now;
            this._timeUpdater = new Timer.Timer(1000);
            this._timeUpdater.Elapsed += this._OnTimeEvent;
            this._timeUpdater.Start();

            this.GotKeyboardFocus += this.StopTimer;
            this.KeyDown +=DateTimePicker_KeyDown;
        }

        private void DateTimePicker_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Escape)
            {
                this._timeUpdater.Start();
            }
        }

        private void StopTimer(object sender, EventArgs arg)
        {
            this._timeUpdater.Stop();
        }

        #endregion Construcnors

        #region Utilities
        #endregion Utilities

        #region Methods
        #endregion Methods

        #region Methods overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion Methods overrides


        #region Event handlers
        private void _OnTimeEvent(Object source, Timer.ElapsedEventArgs arg)
        {
            this.Dispatcher.BeginInvoke(
                new ThreadStart(
                    () => 
                    { 
                        this.SetCurrentValue(ValueProperty, DateTime.Now); 
                    }));
        }
        #endregion Event handlers

        #region Events

        #endregion Events
    }
}
