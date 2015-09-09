//package com.jeremyfeinstein.slidingmenu.example.fragments;

//import com.jeremyfeinstein.slidingmenu.example.R;

//import android.os.Bundle;
//import android.support.v4.app.Fragment;
//import android.view.LayoutInflater;
//import android.view.View;
//import android.view.ViewGroup;
//import android.widget.RelativeLayout;

using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
 


namespace Com.Jeremyfeinstein.SlidingMenu.Example.fragments
{
    public class ColorFragment : Fragment
    {

        private int mColorRes = -1;

        public ColorFragment()
            : this(Resource.Color.white)
        {

        }

        public ColorFragment(int colorRes)
        {
            mColorRes = colorRes;
            RetainInstance=true;
        }

        //@Override
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (savedInstanceState != null)
                mColorRes = savedInstanceState.GetInt("mColorRes");
            Color color = Resources.GetColor(mColorRes);
            // construct the RelativeLayout
            RelativeLayout v = new RelativeLayout(this.Activity);
            v.SetBackgroundColor(color);
            return v;
        }

        //@Override
        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("mColorRes", mColorRes);
        }

    }
}