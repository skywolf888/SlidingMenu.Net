//package com.jeremyfeinstein.slidingmenu.example.fragments;

//import android.app.AlertDialog;
//import android.content.Intent;
//import android.os.Bundle;
//import android.os.Handler;
//import android.support.v4.app.Fragment;
//import android.view.View;

//import com.actionbarsherlock.view.MenuItem;
//import com.jeremyfeinstein.slidingmenu.example.R;
//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu;
//import com.jeremyfeinstein.slidingmenu.lib.app.SlidingFragmentActivity;

/**
 * This activity is an example of a responsive Android UI.
 * On phones, the SlidingMenu will be enabled only in portrait mode.
 * In landscape mode, it will present itself as a dual pane layout.
 * On tablets, it will will do the same general thing. In portrait
 * mode, it will enable the SlidingMenu, and in landscape mode, it
 * will be a dual pane layout.
 * 
 * @author jeremy
 *
 */

using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Com.Jeremyfeinstein.SlidingMenu.Lib.app;
 
using SSlidingMenu=Com.Jeremyfeinstein.SlidingMenu.Lib.SlidingMenu;


namespace Com.Jeremyfeinstein.SlidingMenu.Example.fragments
{
    [Activity(Label = "ResponsiveUIActivity", Theme = "@style/ExampleTheme")]
    public class ResponsiveUIActivity : SlidingFragmentActivity
    {

        private Android.Support.V4.App.Fragment mContent;

        //@Override
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetTitle(Resource.String.responsive_ui);

            SetContentView(Resource.Layout.responsive_content_frame);

            // check if the content frame contains the menu frame
            if (FindViewById(Resource.Id.menu_frame) == null)
            {
                setBehindContentView(Resource.Layout.menu_frame);
                getSlidingMenu().setSlidingEnabled(true);
                getSlidingMenu().setTouchModeAbove(SSlidingMenu.TOUCHMODE_FULLSCREEN);
                // show home as up so we can toggle

                SupportActionBar.SetDefaultDisplayHomeAsUpEnabled(true);

            }
            else
            {
                // add a dummy view
                View v = new View(this);
                setBehindContentView(v);
                getSlidingMenu().setSlidingEnabled(false);
                getSlidingMenu().setTouchModeAbove(SSlidingMenu.TOUCHMODE_NONE);
            }

            // set the Above View Fragment
            if (savedInstanceState != null)
                mContent = SupportFragmentManager.GetFragment(savedInstanceState, "mContent");
            if (mContent == null)
                mContent = new BirdGridFragment(0);
            SupportFragmentManager
            .BeginTransaction()
            .Replace(Resource.Id.content_frame, mContent)
            .Commit();

            // set the Behind View Fragment
            SupportFragmentManager
            .BeginTransaction()
            .Replace(Resource.Id.menu_frame, new BirdMenuFragment())
            .Commit();

            // customize the SlidingMenu
            SSlidingMenu sm = getSlidingMenu();
            sm.setBehindOffsetRes(Resource.Dimension.slidingmenu_offset);
            sm.setShadowWidthRes(Resource.Dimension.shadow_width);
            sm.setShadowDrawable(Resource.Drawable.shadow);
            sm.setBehindScrollScale(0.25f);
            sm.setFadeDegree(0.25f);

            // show the explanation dialog
            if (savedInstanceState == null)
                new AlertDialog.Builder(this)
                .SetTitle(Resource.String.what_is_this)
                .SetMessage(Resource.String.responsive_explanation)
                .Show();
        }

        //@Override
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    toggle();
                    break;
            }
            return base.OnOptionsItemSelected(item);
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
            Handler h = new Handler();
            //h.postDelayed(new Runnable() {
            //    public void run() {
            //        getSlidingMenu().showContent();
            //    }
            //}, 50);
        }

        public void onBirdPressed(int pos)
        {
            Android.Content.Intent intent = BirdActivity.newInstance(this, pos);
            StartActivity(intent);
        }

    }
}