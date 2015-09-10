using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.Jeremyfeinstein.SlidingMenu.Example;
using Android.Support.V4.App;
using Com.Jeremyfeinstein.SlidingMenu.Example.fragments;

namespace SlidingMenu.Net.Sample
{
    [Activity(Label = "SlidingMenu.Net.Sample", MainLauncher = false, Icon = "@drawable/icon")]
    public class MainActivity : FragmentActivity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.menu_frame);

            // Get our button from the layout resource,
            // and attach an event to it
            //Button button = FindViewById<Button>(Resource.Id.MyButton);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

                SupportFragmentManager
            .BeginTransaction()
            .Replace(Resource.Id.menu_frame, new BirdMenuFragment())
            .Commit();

        }

        

         
    }
}

