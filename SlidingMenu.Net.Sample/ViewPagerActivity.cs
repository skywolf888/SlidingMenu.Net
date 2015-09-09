//package com.jeremyfeinstein.slidingmenu.example;

//import java.util.ArrayList;

//import android.os.Bundle;
//import android.support.v4.app.Fragment;
//import android.support.v4.app.FragmentManager;
//import android.support.v4.app.FragmentPagerAdapter;
//import android.support.v4.view.ViewPager;
//import android.support.v4.view.ViewPager.OnPageChangeListener;

//import com.jeremyfeinstein.slidingmenu.example.fragments.ColorFragment;
//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu;

using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Com.Jeremyfeinstein.SlidingMenu.Example.fragments;
using System.Collections.Generic;
 
using SSlidingMenu = Com.Jeremyfeinstein.SlidingMenu.Lib.SlidingMenu;


namespace Com.Jeremyfeinstein.SlidingMenu.Example
{

    [Activity(Label = "ViewPagerActivity", Theme = "@style/ExampleTheme")]
    public class ViewPagerActivity : BaseActivity
    {

        public ViewPagerActivity()
            : base(Resource.String.viewpager)
        {

        }

        class PageChangeClass : Java.Lang.Object, ViewPager.IOnPageChangeListener
        {
            private ViewPagerActivity viewPagerActivity;

            public PageChangeClass(ViewPagerActivity viewPagerActivity)
            {
                // TODO: Complete member initialization
                this.viewPagerActivity = viewPagerActivity;
            }

			
            public void OnPageScrollStateChanged(int state)
            {
                throw new System.NotImplementedException();
            }

            public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
            {
                throw new System.NotImplementedException();
            }

            public void OnPageSelected(int position)
            {
                switch (position) {
                    case 0:
                        this.viewPagerActivity.getSlidingMenu().setTouchModeAbove(SSlidingMenu.TOUCHMODE_FULLSCREEN);
                        break;
                    default:
                        this.viewPagerActivity.getSlidingMenu().setTouchModeAbove(SSlidingMenu.TOUCHMODE_MARGIN);
                        break;
                }
            }
        }

        //@Override
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ViewPager vp = new ViewPager(this);
            vp.Id = "VP".GetHashCode();

            vp.Adapter=new ColorPagerAdapter(this.SupportFragmentManager);
            SetContentView(vp);

            //vp.setOnPageChangeListener(new OnPageChangeListener() {
            //    @Override
            //    public void onPageScrollStateChanged(int arg0) { }

            //    @Override
            //    public void onPageScrolled(int arg0, float arg1, int arg2) { }

            //    @Override
            //    public void onPageSelected(int position) {
            //        switch (position) {
            //        case 0:
            //            getSlidingMenu().setTouchModeAbove(SlidingMenu.TOUCHMODE_FULLSCREEN);
            //            break;
            //        default:
            //            getSlidingMenu().setTouchModeAbove(SlidingMenu.TOUCHMODE_MARGIN);
            //            break;
            //        }
            //    }

            //});

            vp.SetOnPageChangeListener(new PageChangeClass(this));
            vp.SetCurrentItem(0,true);
            getSlidingMenu().setTouchModeAbove(SSlidingMenu.TOUCHMODE_FULLSCREEN);
        }

        public class ColorPagerAdapter : FragmentPagerAdapter
        {

            private List<Android.Support.V4.App.Fragment> mFragments;

            private int[] COLORS = new int[] {
			Resource.Color.red,
			Resource.Color.green,
			Resource.Color.blue,
			Resource.Color.white,
			Resource.Color.black
		};

            public ColorPagerAdapter(Android.Support.V4.App.FragmentManager fm)
                : base(fm)
            {
                //super(fm);
                mFragments = new List<Android.Support.V4.App.Fragment>();
                foreach (int color in COLORS)
                    mFragments.Add(new ColorFragment(color));
            }

            //@Override
            //public int getCount() {
            //    return mFragments.size();
            //}

            public override int Count
            {
                get { return mFragments.Count; }
            }

            //@Override
            public override Android.Support.V4.App.Fragment GetItem(int position)
            {
                return mFragments[position];
            }

        }

    }
}
