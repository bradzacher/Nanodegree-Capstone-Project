using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Nanodegree_Capstone_Project
{
    [Activity(Label = "Nanodegree_Capstone_Project", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private int _count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            this.SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            var button = this.FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = $"{this._count++} clicks!"; };
        }
    }
}

