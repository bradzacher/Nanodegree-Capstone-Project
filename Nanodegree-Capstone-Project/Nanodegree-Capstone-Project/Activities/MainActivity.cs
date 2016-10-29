using Android.App;
using Android.Content.PM;
using Android.Net.Http;
using Android.OS;
using Android.Preferences;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Util;
using Android.Views;
using Java.IO;
using Zacher.Fragments;

namespace Zacher.Activities
{
    [Activity(Label = "Home", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, Icon = "@drawable/Icon")]
    // ReSharper disable once UnusedMember.Global
    public class MainActivity : BaseActivity
    {
        private const long HttpCacheSize = 10 * 1024 * 1024; // 10 MiB

        private DrawerLayout _drawerLayout;
        private NavigationView _navigationView;

        protected override int LayoutResource => Resource.Layout.main_activity;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
         
            // init the settings
            PreferenceManager.SetDefaultValues(this, Resource.Xml.settings, false);
               
            this._drawerLayout = this.FindViewById<DrawerLayout>(Resource.Id.DrawerLayout);

            // Set hamburger items menu
            this.SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);

            // setup navigation view
            this._navigationView = this.FindViewById<NavigationView>(Resource.Id.NavView);

            // handle navigation
            this._navigationView.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);

                this.ListItemClicked(e.MenuItem.ItemId);
                
                this._drawerLayout.CloseDrawers();
            };
            
            // if first time you will want to go ahead and click first item.
            if (savedInstanceState == null)
            {
                this.ListItemClicked();
            }

            // install the HTTP cache
            try
            {
                var httpCacheDir = new File(this.GetExternalCacheDirs()[0], "http");
                HttpResponseCache.Install(httpCacheDir, HttpCacheSize);
            }
            catch (IOException e)
            {
                Log.Info(this.GetString(Resource.String.LogTag), e, "HTTP response cache installation failed");
            }
        }

        private int _oldItemId = -1;
        private void ListItemClicked(int itemId = Resource.Id.NavItemForecast) // default to the first item
        {
            // this way we don't load twice, but you might want to modify this a bit.
            if (itemId == this._oldItemId)
            {
                return;
            }

            this._oldItemId = itemId;
            int title;

            Fragment fragment;
            switch (itemId)
            {
                case Resource.Id.NavItemForecast:
                    fragment = ForecastFragment.NewInstance();
                    title = ForecastFragment.Title;
                    break;
                case Resource.Id.NavItemRadar:
                    fragment = RadarFragment.NewInstance();
                    title = RadarFragment.Title;
                    break;
                case Resource.Id.NavItemSettings:
                    fragment = SettingsFragment.NewInstance();
                    title = SettingsFragment.Title;
                    break;

                default:
                    // unexpected selection
                    return;
            }

            // set the title
            this.Title = this.GetString(title);

            // inject the selected fragment
            this.FragmentManager.BeginTransaction()
                .Replace(Resource.Id.ContentFrame, fragment)
                .Commit();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                this._drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}

