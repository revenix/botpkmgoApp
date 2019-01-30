using System; 
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS; 
using Android.Views;
using SupportFragment = Android.Support.V4.App.Fragment;


namespace botpkmgoApp.droid.Fragments
{
    public class MapPkmFragment : SupportFragment, IOnMapReadyCallback
    {
         
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            
            View view = inflater.Inflate(Resource.Layout.MapPkmnFragment, container, false);

            SetUpMap();

            return view;
             
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            LatLng location = new LatLng(-12.087605, -77.013882);

            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(18);
            builder.Bearing(155);
            builder.Tilt(65);

            CameraPosition cameraPosition = builder.Build();

            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

            googleMap.MoveCamera(cameraUpdate);
        }

        public void SetUpMap() {

            var mapFragment = (SupportMapFragment)ChildFragmentManager.FindFragmentById(Resource.Id.mapGoogle);
            mapFragment.GetMapAsync(this);

        }

    }
}