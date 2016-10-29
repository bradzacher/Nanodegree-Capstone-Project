using Android.Content;
using Android.Preferences;

namespace Zacher.Model
{
    public class SettingsModel
    {
        public const string PreferenceKeyNotificationTime = "prefNotificationTime";
        public const string PreferenceKeyMeasurementUnits = "prefMeasurementUnits";

        /// <summary>
        /// The default measurement unit
        /// </summary>
        public string MeasurementUnitsDefault => this._ctx.GetString(Resource.String.pref_default_measurement_units);
        /// <summary>
        /// The default notification time
        /// </summary>
        public string NotificationTimeDefault => this._ctx.GetString(Resource.String.pref_default_notification_time);

        private readonly ISharedPreferences _preferences;
        private readonly Context _ctx;

        public SettingsModel(Context ctx)
        {
            this._ctx = ctx;
            this._preferences = PreferenceManager.GetDefaultSharedPreferences(ctx);
        }

        private string Metric => this._ctx.GetString(Resource.String.pref_units_metric);
        
        /// <summary>
        /// True if the setting is set to metric, false otherwise
        /// </summary>
        public bool IsMetric => this._preferences.GetString(PreferenceKeyMeasurementUnits, this.MeasurementUnitsDefault) == this.Metric;
        /// <summary>
        /// A string representing the 24hr time setting
        /// </summary>
        public string NotificationTime => this._preferences.GetString(PreferenceKeyNotificationTime, this.NotificationTimeDefault);
    }
}