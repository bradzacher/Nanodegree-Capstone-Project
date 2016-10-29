using Android.OS;
using Android.App;
using Android.Views;

namespace Zacher.Fragments
{
    public class ForecastFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        // ReSharper disable once UnusedMember.Global
        public static ForecastFragment NewInstance()
        {
            return new ForecastFragment { Arguments = new Bundle() };
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.forecast_fragment, null);
        }
    }
}