using Android.App;
using Android.Content.PM;
using Android.Net.Http;
using Android.OS;
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

        protected override int LayoutResource => Resource.Layout.main;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            this._drawerLayout = this.FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            // Set hamburger items menu
            this.SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);

            // setup navigation view
            this._navigationView = this.FindViewById<NavigationView>(Resource.Id.nav_view);

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
        private void ListItemClicked(int itemId = Resource.Id.nav_home_1) // default to the first item
        {
            // this way we don't load twice, but you might want to modify this a bit.
            if (itemId == this._oldItemId)
            {
                return;
            }

            this._oldItemId = itemId;

            Android.Support.V4.App.Fragment fragment = null;
            switch (itemId)
            {
                // todo - add my menu items
                case Resource.Id.nav_home_1:
                    fragment = Fragment1.NewInstance();
                    break;
                case Resource.Id.nav_home_2:
                    fragment = Fragment2.NewInstance();
                    break;

                default:
                    // unexpected selection
                    return;
            }

            this.SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    this._drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}

