using Android.OS;
using Android.Support.V4.App;
using Android.Views;

namespace Zacher.Fragments
{
    public class RadarFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        // ReSharper disable once UnusedMember.Global
        public static RadarFragment NewInstance()
        {
            return new RadarFragment { Arguments = new Bundle() };
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.radar_fragment, null);
        }
    }
}