//package com.jeremyfeinstein.slidingmenu.example.fragments;

//import android.os.Bundle;
//import android.support.v4.app.Fragment;

//import com.jeremyfeinstein.slidingmenu.example.BaseActivity;
//import com.jeremyfeinstein.slidingmenu.example.R;
//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu;


using Android.App;
using Android.OS;
using Android.Support.V4.App;
 
using SSlidingMenu = Com.Jeremyfeinstein.SlidingMenu.Lib.SlidingMenu;


namespace Com.Jeremyfeinstein.SlidingMenu.Example.fragments
{

    [Activity(Label = "FragmentChangeActivity", Theme = "@style/ExampleTheme")]
    public class FragmentChangeActivity : BaseActivity
    {

        private Android.Support.V4.App.Fragment mContent;


        public FragmentChangeActivity()
            : base(Resource.String.changing_fragments)
        {

        }

        //@Override
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // set the Above View
            if (savedInstanceState != null)
                mContent = SupportFragmentManager.GetFragment(savedInstanceState, "mContent");
            if (mContent == null)
                mContent = new ColorFragment(Resource.Color.red);

            // set the Above View
            SetContentView(Resource.Layout.content_frame);
            SupportFragmentManager
            .BeginTransaction()
            .Replace(Resource.Id.content_frame, mContent)
            .Commit();

            // set the Behind View
            setBehindContentView(Resource.Layout.menu_frame);
            SupportFragmentManager
            .BeginTransaction()
            .Replace(Resource.Id.menu_frame, new ColorMenuFragment())
            .Commit();

            // customize the SlidingMenu
            getSlidingMenu().setTouchModeAbove(SSlidingMenu.TOUCHMODE_FULLSCREEN);
        }

        //@Override
        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            SupportFragmentManager.PutFragment(outState, "mContent", mContent);
        }

        public void switchContent(Android.Support.V4.App.Fragment fragment)
        {
            mContent = fragment;
            SupportFragmentManager
            .BeginTransaction()
            .Replace(Resource.Id.content_frame, fragment)
            .Commit();
            getSlidingMenu().showContent();
        }

    }
}