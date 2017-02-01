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
using TK = Xceed.Wpf.Toolkit;
using System.Windows.Input;

namespace ExtendedDateTimePicker
{
    public class ExtendedDateTimePicker : TK.DateTimePicker
    {

        #region Static

        #region Properties

        /// <summary>
        /// ДатаВремя, сохраняемое на клиенте.
        /// </summary>
        public static DependencyProperty ValueToSaveProperty;
        /// <summary>
        /// Команда сброса времени.
        /// </summary>
        public static DependencyProperty SetValueToSaveCommandProperty;

        #endregion Properties

        #region Constructors

        static ExtendedDateTimePicker()
        {
            // предоставление стиля по умолчанию:
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ExtendedDateTimePicker),
                new FrameworkPropertyMetadata(typeof(ExtendedDateTimePicker)));

            // регистрация сво
            ValueToSaveProperty = DependencyProperty.Register(
                "ValueToSave", 
                typeof(DateTime), 
                typeof(ExtendedDateTimePicker));

            SetValueToSaveCommandProperty = DependencyProperty.Register(
                "SetValueToSaveCommand",
                typeof(ICommand),
                typeof(ExtendedDateTimePicker));
        }
        #endregion Constructors

        #endregion Static

        #region Fields

        private DateTime _actualDateTime;

        /// <summary>
        /// Таймер обновления отображаемого времени.
        /// </summary>
        private Timer.Timer _timeUpdater;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Команда сброса даты-времени в актуальное состояние.
        /// </summary>
        public ICommand SetValueToSaveCommand
        {
            get
            {
                return (ICommand)GetValue(SetValueToSaveCommandProperty);
            }
            set
            {
                SetValue(SetValueToSaveCommandProperty, value);
            }
        }
        /// <summary>
        /// Дата-время, для сохранения на клиенте.
        /// </summary>
        public DateTime ValueToSave
        {
            get 
            { 
                return (DateTime)GetValue(ValueToSaveProperty); 
            }
            set 
            { 
                SetValue(ValueToSaveProperty, value); 
            }
        }

        #endregion Properties

        #region Constructors
        public ExtendedDateTimePicker() : base()
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
                this.ValueToSave = this.Value.Value;
                this._ResetTime();
            }
        }

        private void _UpdateTime()
        {
            this.Dispatcher.BeginInvoke(
                new ThreadStart(
                    () =>
                    {
                        //this.SetCurrentValue(ValueProperty, DateTime.Now);
                        this.Value = DateTime.Now;
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

            this.SetValueToSaveCommand = new RelayCommand(
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
