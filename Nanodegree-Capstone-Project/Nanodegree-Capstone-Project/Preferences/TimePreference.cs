using System;
using Android.Content;
using Android.Content.Res;
using Android.Preferences;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using JetBrains.Annotations;
using Object = Java.Lang.Object;

namespace Zacher.Preferences
{
    /// <summary>
    /// Time picker for the preference screen
    /// Adapted from http://stackoverflow.com/a/5533295/3736051
    /// </summary>
    [UsedImplicitly]
    public sealed class TimePreference : DialogPreference
    {
        private int _lastHour;
        private int _lastMinute;
        private TimePicker _picker;

        /// <summary>
        /// workaround for a bug in xamarin when invoking classes with virtual methods http://stackoverflow.com/a/10593438/3736051
        /// </summary>
        public TimePreference(IntPtr a, JniHandleOwnership b)
            : base(a,b) {}

        public TimePreference(Context ctx, IAttributeSet attrs)
            : base(ctx, attrs)
        {
            this.SetPositiveButtonText(Resource.String.pref_time_positive_response);
            this.SetNegativeButtonText(Resource.String.pref_time_negative_response);
        }
        
        /// <inheritdoc />
        protected override View OnCreateDialogView()
        {
            this._picker = new TimePicker(this.Context);

            return (this._picker);
        }

        /// <inheritdoc />
        protected override void OnBindDialogView(View v)
        {
            base.OnBindDialogView(v);

            this._picker.Hour = this._lastHour;
            this._picker.Minute = this._lastMinute;
        }

        /// <inheritdoc />
        protected override void OnDialogClosed(bool positiveResult)
        {
            base.OnDialogClosed(positiveResult);

            if (!positiveResult)
            {
                return;
            }

            this._lastHour = this._picker.Hour;
            this._lastMinute = this._picker.Minute;

            string time = $"{this._lastHour:0}:{this._lastMinute:00}";

            if (this.CallChangeListener(time))
            {
                this.PersistString(time);
            }
        }

        /// <inheritdoc />
        protected override Object OnGetDefaultValue(TypedArray a, int index)
        {
            return a.GetString(index);
        }

        private string GetDefaultValue()
        {
            return this.Context.GetString(Resource.String.pref_default_notification_time);
        }

        /// <inheritdoc />
        protected override void OnSetInitialValue(bool restoreValue, Object defaultValue)
        {
            string time;

            if (restoreValue)
            {
                time = this.GetPersistedString(defaultValue?.ToString() ?? this.GetDefaultValue());
            }
            else
            {
                time = defaultValue.ToString();
            }

            string[] pieces = time.Split(':');
            this._lastHour = int.Parse(pieces[0]);
            this._lastMinute = int.Parse(pieces[1]);
        }

        /// <summary>
        /// Returns the currently selected value
        /// </summary>
        /// <returns>The selected time formatted as a 24-hr time</returns>
        public string GetValue()
        {
            return this.GetPersistedString(this.GetDefaultValue());
        }
    }
}