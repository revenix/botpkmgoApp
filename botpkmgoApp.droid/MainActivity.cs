using Android.App;
using Android.OS;
using Android.Support.V7.App; 
using Android.Support.V4.Widget;
using SupportFragment = Android.Support.V4.App.Fragment;
using SupportFragmentManager = Android.Support.V4.App.FragmentManager;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using SupportActionBar = Android.Support.V7.App.ActionBar;

using Android.Views;
using Android.Support.Design.Widget; 
using Android.Support.V4.View;
using Android.Support.V4.App;
using System.Collections.Generic;
using Java.Lang;
using botpkmgoApp.droid.Fragments;

namespace botpkmgoApp.droid
{
    [Activity(Theme = "@style/Theme.DesignMenu", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        private DrawerLayout mDrawerLayout;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            ////MENU
            SupportToolbar toolBar = FindViewById<SupportToolbar>(Resource.Id.toolBar);
            SetSupportActionBar(toolBar);

            SupportActionBar ab = SupportActionBar;
            ab.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            ab.SetDisplayHomeAsUpEnabled(true);

            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            if (navigationView != null)
            {
                SetUpDrawerContent(navigationView);
            }

            TabLayout tabs = FindViewById<TabLayout>(Resource.Id.tabs);

            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);

            SetUpViewPager(viewPager);

            tabs.SetupWithViewPager(viewPager);
            /*Boton Flotante*/
            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);

            fab.Click += (o, e) =>
            {
                View anchor = o as View;

                Snackbar.Make(anchor, "QR", Snackbar.LengthLong)
                        .SetAction("SCAN", v =>
                        {
                            //add async  
                            //do something here 
                        })
                        .Show();
            };
            /*Boton Flotante*/

            //MENU
        }

        //MENU
        private void SetUpViewPager(ViewPager viewPager)
        {
            //tabs agregar tabs 
            TabAdapter adapter = new TabAdapter(SupportFragmentManager);
            adapter.AddFragment(new MapPkmFragment(), "GPS");
            //adapter.AddFragment(new Fragment2(), "Participantes");
            //adapter.AddFragment(new Fragment3(), "Modalidades");

            viewPager.Adapter = adapter;
        }

        private void SetUpDrawerContent(NavigationView navigationView)
        {
            navigationView.NavigationItemSelected += (object sender, NavigationView.NavigationItemSelectedEventArgs e) =>
            { 
                ////menu option
                //switch (e.MenuItem.ItemId)
                //{
                //    case Resource.Id.nav_:
                //        //var inten = new Intent(this, typeof(infoEquipo));
                //        //inten.PutExtra("idparticipante", id_participante);
                //        //StartActivity(inten);
                //        break;
                //    case Resource.Id.nav_:

                //        //var activity2 = new Intent(this, typeof(InfoParticipanteActivity));
                //        //activity2.PutExtra("idparticipante", id_participante);
                //        //StartActivity(activity2);

                //        break;

                //    default: 
                //        //Intent loginPageIntent = new Intent(this, typeof(Login));
                //        ////logout
                //        //loginPageIntent.AddFlags(ActivityFlags.ClearTop);
                //        //StartActivity(loginPageIntent); 
                //        break;
                // }
                ////menu option

                e.MenuItem.SetChecked(true);
                mDrawerLayout.CloseDrawers();
            };
        }
        //Evento de botones del Menu


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    mDrawerLayout.OpenDrawer((int)GravityFlags.Left);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        //TABS
        public class TabAdapter : FragmentPagerAdapter
        {
            public List<SupportFragment> Fragments { get; set; }
            public List<string> FragmentNames { get; set; }

            public TabAdapter(SupportFragmentManager sfm) : base(sfm)
            {
                Fragments = new List<SupportFragment>();
                FragmentNames = new List<string>();
            }

            public void AddFragment(SupportFragment fragment, string name)
            {
                Fragments.Add(fragment);
                FragmentNames.Add(name);
            }

            public override int Count
            {
                get
                {
                    return Fragments.Count;
                }
            }

            public override SupportFragment GetItem(int position)
            {
                return Fragments[position];
            }

            public override ICharSequence GetPageTitleFormatted(int position)
            {
                return new Java.Lang.String(FragmentNames[position]);
            }
        }
        //TABS


    }
}