//package com.jeremyfeinstein.slidingmenu.example.anim;

//import android.os.Bundle;
//import android.view.Menu;

//import com.jeremyfeinstein.slidingmenu.example.BaseActivity;
//import com.jeremyfeinstein.slidingmenu.example.R;
//import com.jeremyfeinstein.slidingmenu.example.SampleListFragment;
//import com.jeremyfeinstein.slidingmenu.example.R.id;
//import com.jeremyfeinstein.slidingmenu.example.R.layout;
//import com.jeremyfeinstein.slidingmenu.example.R.menu;
//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu;
//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu.CanvasTransformer;

using Android.OS;
using Com.Jeremyfeinstein.SlidingMenu.Lib;
 

namespace Com.Jeremyfeinstein.SlidingMenu.Example.anim
{
    public abstract class CustomAnimation : BaseActivity
    {

        private CanvasTransformer mTransformer;

        public CustomAnimation(int titleRes, CanvasTransformer transformer)
            : base(titleRes)
        {

            mTransformer = transformer;
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

            SlidingMenu.Lib.SlidingMenu sm = getSlidingMenu();
            setSlidingActionBarEnabled(true);
            sm.setBehindScrollScale(0.0f);
            sm.setBehindCanvasTransformer(mTransformer);
        }

    }
}