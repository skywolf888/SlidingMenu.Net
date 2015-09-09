//package com.jeremyfeinstein.slidingmenu.example;

using Android.App;
//import android.os.Bundle;
using Android.OS;
 

namespace Com.Jeremyfeinstein.SlidingMenu.Example
{
    [Activity(Label = "SlidingContent", Theme = "@style/ExampleTheme")]
    public class SlidingContent : BaseActivity
    {

        public SlidingContent()
            : base(Resource.String.title_bar_content)
        {

        }

        //@Override
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // set the Above View
            SetContentView(Resource.Layout.content_frame);
            SupportFragmentManager
            .BeginTransaction()
            .Replace(Resource.Id.content_frame, new SampleListFragment())
            .Commit();

            setSlidingActionBarEnabled(false);
        }

    }
}