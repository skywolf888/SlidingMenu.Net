//package com.jeremyfeinstein.slidingmenu.example;
//import android.os.Bundle;
//import android.view.KeyEvent;
//import android.view.View;
//import android.view.ViewGroup;
//import com.jeremyfeinstein.slidingmenu.example.fragments.ColorFragment;
//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu;
//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu.OnClosedListener;
//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu.OnOpenedListener;


using Android.OS;
using SSlidingMenu=Com.Jeremyfeinstein.SlidingMenu.Lib.SlidingMenu;
 
using Android.App;

namespace Com.Jeremyfeinstein.SlidingMenu.Example
{
    [Activity(Label = "LeftAndRightActivity", Theme = "@style/ExampleTheme")]
    public class LeftAndRightActivity : BaseActivity
    {

        public LeftAndRightActivity()
            : base(Resource.String.left_and_right)

        { }

        //@Override
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            getSlidingMenu().setMode(SSlidingMenu.LEFT_RIGHT);
            getSlidingMenu().setTouchModeAbove(SSlidingMenu.TOUCHMODE_FULLSCREEN);

            SetContentView(Resource.Layout.content_frame);
            SupportFragmentManager
            .BeginTransaction()
            .Replace(Resource.Id.content_frame, new SampleListFragment())
            .Commit();

            getSlidingMenu().setSecondaryMenu(Resource.Layout.menu_frame_two);
            getSlidingMenu().setSecondaryShadowDrawable(Resource.Drawable.shadowright);
            SupportFragmentManager
            .BeginTransaction()
            .Replace(Resource.Id.menu_frame_two, new SampleListFragment())
            .Commit();
        }
    }
}