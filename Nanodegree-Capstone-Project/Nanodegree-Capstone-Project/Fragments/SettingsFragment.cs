using Android.Content;
using Android.OS;
using Android.Preferences;
using Zacher.Model;
using Zacher.Preferences;

namespace Zacher.Fragments
{
    public class SettingsFragment : PreferenceFragment, ISharedPreferencesOnSharedPreferenceChangeListener
    {
        public const int Title = Resource.String.fragment_title_settings;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            this.AddPreferencesFromResource(Resource.Xml.settings);
            this.OnSharedPreferenceChanged(this.PreferenceScreen.SharedPreferences, SettingsModel.PreferenceKeyMeasurementUnits);
            this.OnSharedPreferenceChanged(this.PreferenceScreen.SharedPreferences, SettingsModel.PreferenceKeyNotificationTime);
        }

        public override void OnResume()
        {
            base.OnResume();

            this.PreferenceScreen.SharedPreferences.RegisterOnSharedPreferenceChangeListener(this);
        }

        public override void OnPause()
        {
            base.OnPause();

            this.PreferenceScreen.SharedPreferences.UnregisterOnSharedPreferenceChangeListener(this);
        }

        // ReSharper disable once UnusedMember.Global
        public static SettingsFragment NewInstance()
        {
            return new SettingsFragment { Arguments = new Bundle() };
        }

        public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
        {
            // make the selected measurement unit value visible
            if (key == SettingsModel.PreferenceKeyMeasurementUnits)
            {
                var pref = (ListPreference) this.FindPreference(key);
                pref.Summary = pref.Entry;
            }
            else if (key == SettingsModel.PreferenceKeyNotificationTime)
            {
                var pref = (TimePreference) this.FindPreference(key);
                string currentVal = pref.GetValue();
                pref.Summary = this.GetString(Resource.String.pref_notification_time_summary_selected, currentVal);
            }
        }
    }
}