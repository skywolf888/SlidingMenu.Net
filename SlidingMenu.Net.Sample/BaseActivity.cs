//package com.jeremyfeinstein.slidingmenu.example;

//import java.util.ArrayList;
//import java.util.List;

//import android.os.Bundle;
//import android.support.v4.app.Fragment;
//import android.support.v4.app.FragmentManager;
//import android.support.v4.app.FragmentPagerAdapter;
//import android.support.v4.app.FragmentTransaction;
//import android.support.v4.app.ListFragment;
//import android.support.v4.view.ViewPager;

//import com.actionbarsherlock.view.Menu;
//import com.actionbarsherlock.view.MenuItem;
//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu;
//import com.jeremyfeinstein.slidingmenu.lib.app.SlidingFragmentActivity;


 
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Com.Jeremyfeinstein.SlidingMenu.Lib.app;
 
using SSlidingMenu=Com.Jeremyfeinstein.SlidingMenu.Lib.SlidingMenu;


namespace Com.Jeremyfeinstein.SlidingMenu.Example
{

    public class BaseActivity : SlidingFragmentActivity
    {

        private int mTitleRes;
        protected ListFragment mFrag;

        public BaseActivity(int titleRes)
        {
            mTitleRes = titleRes;
        }

        //@Override
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetTitle(mTitleRes);

            // set the Behind View
            setBehindContentView(Resource.Layout.menu_frame);
            if (savedInstanceState == null)
            {
                FragmentTransaction t = this.SupportFragmentManager.BeginTransaction();
                mFrag = new SampleListFragment();
                t.Replace(Resource.Id.menu_frame, mFrag);
                t.Commit();
            }
            else
            {
                mFrag = (ListFragment)this.SupportFragmentManager.FindFragmentById(Resource.Id.menu_frame);
            }

            // customize the SlidingMenu

            SSlidingMenu sm = getSlidingMenu();
            sm.setShadowWidthRes(Resource.Dimension.shadow_width);
            sm.setShadowDrawable(Resource.Drawable.shadow);
            sm.setBehindOffsetRes(Resource.Dimension.slidingmenu_offset);
            sm.setFadeDegree(0.35f);
            sm.setTouchModeAbove(SSlidingMenu.TOUCHMODE_FULLSCREEN);
            SupportActionBar.SetDefaultDisplayHomeAsUpEnabled(true);            
        }

        //@Override
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    toggle();
                    return true;
                case Resource.Id.github:
                    Util.goToGitHub(this);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        //@Override
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //SupportMenuInflater.inflate(R.Menu.main, menu);
            MenuInflater.Inflate(Resource.Menu.main, menu);
            return true;
        }
    }
}