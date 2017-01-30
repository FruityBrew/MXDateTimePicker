using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using Xceed.Wpf.Toolkit;

namespace DateTimePicker
{
    [TemplatePart(Name = PART_TimeUpDown, Type = typeof(DateTimeUpDown))]
    public class ExtendedDateTimeUpDown : DateTimeUpDown
    {
        #region Constants

        private const string PART_TimeUpDown = "PART_TimeUpDown";

        #endregion Constants

        #region Static

        #region Constructors

        static ExtendedDateTimeUpDown()
        {
            // предоставление нового стиля:
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ExtendedDateTimeUpDown),
                new FrameworkPropertyMetadata(typeof(ExtendedDateTimeUpDown)));
        }
        #endregion Constructors


        #endregion Static

        #region Fields

        private DateTimeUpDown _dateTimeUpDown;

        private Timer _timeUpdater;

        private DateTime _currentDateTime;

        #endregion Fields

        #region Properties
        
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
        public ExtendedDateTimeUpDown()
        {
            this._currentDateTime = DateTime.Now;
            this._timeUpdater = new Timer(1000);
            this._timeUpdater.Elapsed += this._OnTimeEvent;
            this._timeUpdater.Start();
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
        private void _OnTimeEvent(Object source, ElapsedEventArgs arg)
        {
            this.CurrentDateTime = DateTime.Now;
        }
        #endregion Event handlers

        #region Events
        #endregion Events
    }
}
