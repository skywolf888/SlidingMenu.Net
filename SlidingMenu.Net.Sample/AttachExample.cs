//package com.jeremyfeinstein.slidingmenu.example;

//import android.os.Bundle;
//import android.support.v4.app.FragmentActivity;

//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu;


using Android.OS;
using Android.Support.V4.App;
using SSlidingMenu=Com.Jeremyfeinstein.SlidingMenu.Lib.SlidingMenu;
using SlidingMenu.Net.Sample;
using Android.App;


namespace Com.Jeremyfeinstein.SlidingMenu.Example
{
    [Activity(Label = "AttachExample", Theme = "@style/ExampleTheme")]
    public class AttachExample : FragmentActivity
    {

        private SSlidingMenu menu;

        //@Override
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetTitle(Resource.String.attach);

            // set the Above View
            SetContentView(Resource.Layout.content_frame);
            SupportFragmentManager
            .BeginTransaction()
            .Replace(Resource.Id.content_frame, new SampleListFragment())
            .Commit();

            // configure the SlidingMenu
            menu = new SSlidingMenu(this);
            menu.setTouchModeAbove(SSlidingMenu.TOUCHMODE_FULLSCREEN);
            menu.setShadowWidthRes(Resource.Dimension.shadow_width);
            menu.setShadowDrawable(Resource.Drawable.shadow);
            menu.setBehindOffsetRes(Resource.Dimension.slidingmenu_offset);
            menu.setFadeDegree(0.35f);
            menu.attachToActivity(this, SSlidingMenu.SLIDING_CONTENT);
            menu.setMenu(Resource.Layout.menu_frame);
             
            SupportFragmentManager
            .BeginTransaction()
            .Replace(Resource.Id.menu_frame, new SampleListFragment())
            .Commit();
        }

        //@Override
        public override void OnBackPressed()
        {
            if (menu.isMenuShowing())
            {
                menu.showContent();
            }
            else
            {
                base.OnBackPressed();
            }
        }

    }
}
