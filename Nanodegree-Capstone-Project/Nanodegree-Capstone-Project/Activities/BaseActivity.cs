using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;

namespace Zacher.Activities
{
    public abstract class BaseActivity : AppCompatActivity
    {
        public Toolbar Toolbar { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(this.LayoutResource);
            this.Toolbar = this.FindViewById<Toolbar>(Resource.Id.Toolbar);
            if (this.Toolbar != null)
            {
                this.SetSupportActionBar(this.Toolbar);
                this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                this.SupportActionBar.SetHomeButtonEnabled(true);
            }
        }

        protected abstract int LayoutResource
        {
            get;
        }

        protected int ActionBarIcon
        {
            set { this.Toolbar.SetNavigationIcon(value); }
        }
    }


}

