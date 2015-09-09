//package com.jeremyfeinstein.slidingmenu.example;

//import android.os.Bundle;
//import android.support.v4.view.ViewPager;

using Android.App;
using Android.OS;
 

namespace Com.Jeremyfeinstein.SlidingMenu.Example
{
        [Activity(Label = "SlidingTitleBar", Theme = "@style/ExampleTheme")]
    public class SlidingTitleBar : BaseActivity
    {

        public SlidingTitleBar()
            : base(Resource.String.title_bar_slide)
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

            setSlidingActionBarEnabled(true);
        }

    }
}