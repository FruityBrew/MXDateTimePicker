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
using System.Windows.Input;

namespace DateTimePicker
{
    public class DateTimePicker : DateTimeUpDown
    {

        #region Static

        #region Properties

        /// <summary>
        /// ДатаВремя, устанавливаемое вручную.
        /// </summary>
        public static DependencyProperty ManuallySettedValueProperty;
        /// <summary>
        /// Команда сброса отображаемого времени в актуальное.
        /// </summary>
        public static DependencyProperty SetManuallyTimeCommandProperty;

        #endregion Properties

        #region Constructors

        static DateTimePicker()
        {
            // предоставление стиля по умолчанию:
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(DateTimePicker),
                new FrameworkPropertyMetadata(typeof(DateTimePicker)));

            // регистрация сво
            ManuallySettedValueProperty = DependencyProperty.Register(
                "ManuallySettedValue", 
                typeof(DateTime), 
                typeof(DateTimePicker));

            SetManuallyTimeCommandProperty = DependencyProperty.Register(
                "SetManuallyTimeCommand",
                typeof(ICommand),
                typeof(DateTimePicker));
        }
        #endregion Constructors

        #endregion Static

        #region Fields

        /// <summary>
        /// Таймер обновления отображаемого времени.
        /// </summary>
        private Timer.Timer _timeUpdater;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Команда сброса даты-времени в актуальное состояние.
        /// </summary>
        public ICommand SetManuallyTimeCommand
        {
            get
            {
                return (ICommand)GetValue(SetManuallyTimeCommandProperty);
            }
            set
            {
                SetValue(SetManuallyTimeCommandProperty, value);
            }
        }
        /// <summary>
        /// Дата-время, установленные вручную.
        /// </summary>
        public DateTime ManuallySettedValue
        {
            get 
            { 
                return (DateTime)GetValue(ManuallySettedValueProperty); 
            }
            set 
            { 
                SetValue(ManuallySettedValueProperty, value); 
            }
        }

        #endregion Properties

        #region Constructors
        public DateTimePicker() : base()
        {
        }

        #endregion Constructors

        #region Utilities

        private void _ResetTime()
        {
            this._OnTimeEvent(this, null);
            Keyboard.ClearFocus();
            this._timeUpdater.Start();
        }

        private void _SetManuallyValue(object parameter)
        {
            if (this.Value.HasValue)
            {
                this.ManuallySettedValue = this.Value.Value;
                this._ResetTime();
            }
        }

        private void _UpdateTime()
        {
            this.Dispatcher.BeginInvoke(
                new ThreadStart(
                    () =>
                    {
                        this.SetCurrentValue(ValueProperty, DateTime.Now);
                    }));
        }

        private void _StopTimer(object sender, EventArgs arg)
        {
            this._timeUpdater.Stop();
        }

        #endregion Utilities

        #region Methods overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // начальная установка свойств:
            this._UpdateTime();

            // Создание и запуск таймера:

            this._timeUpdater = new Timer.Timer(1000);
            this._timeUpdater.Elapsed += this._OnTimeEvent;
            this._timeUpdater.Start();

            // Подписка остановки таймера на захват фокуса клавиатуры:
            this.GotKeyboardFocus += this._StopTimer;
            // Подписка на нажатие клавиши клавиатуры:
            this.KeyDown += DateTimePicker_KeyDown;

            this.SetManuallyTimeCommand = new RelayCommand(
                this._SetManuallyValue,
                (obj) => true);  
        }


        #endregion Methods overrides


        #region Event handlers

        private void DateTimePicker_KeyDown(object sender, KeyEventArgs e)
        {
            // если esc - сбросить в текущее
            if (e.Key == Key.Escape)
            {
                this._ResetTime();
            }
        }

        private void _OnTimeEvent(Object source, Timer.ElapsedEventArgs arg)
        {
            this._UpdateTime();
        }
        #endregion Event handlers
    }
}
