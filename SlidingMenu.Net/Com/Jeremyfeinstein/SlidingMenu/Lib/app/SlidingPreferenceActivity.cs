//package com.jeremyfeinstein.slidingmenu.lib.app;

//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu;

//import android.os.Bundle;
//import android.preference.PreferenceActivity;
//import android.view.KeyEvent;
//import android.view.View;
//import android.view.ViewGroup.LayoutParams;

using Android.App;
using Android.OS;
using Android.Preferences;
using Android.Views;
using LayoutParams=Android.Views.ViewGroup.LayoutParams;

namespace Com.Jeremyfeinstein.SlidingMenu.Lib.app
{
    public class SlidingPreferenceActivity : PreferenceActivity, SlidingActivityBase
    {

        private SlidingActivityHelper mHelper;

        /* (non-Javadoc)
         * @see android.app.Activity#onCreate(android.os.Bundle)
         */
        //@Override
        protected override void OnCreate(Bundle savedInstanceState)
        {
            mHelper = new SlidingActivityHelper(this);
            base.OnCreate(savedInstanceState);
            mHelper.onCreate(savedInstanceState);
        }

        /* (non-Javadoc)
         * @see android.app.Activity#onPostCreate(android.os.Bundle)
         */
        //@Override
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mHelper.onPostCreate(savedInstanceState);
        }

        /* (non-Javadoc)
         * @see android.app.Activity#findViewById(int)
         */
        //@Override
        public override View FindViewById(int id)
        {
            View v = base.FindViewById(id);
            if (v != null)
                return v;
            return mHelper.findViewById(id);
        }

        /* (non-Javadoc)
         * @see android.app.Activity#onSaveInstanceState(android.os.Bundle)
         */
        //@Override
        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            mHelper.onSaveInstanceState(outState);
        }

        /* (non-Javadoc)
         * @see android.app.Activity#setContentView(int)
         */
        //@Override
        public override void SetContentView(int id)
        {
            SetContentView(LayoutInflater.Inflate(id, null));
        }

        /* (non-Javadoc)
         * @see android.app.Activity#setContentView(android.view.View)
         */
        //@Override
        public override void SetContentView(View v)
        {
            SetContentView(v, new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent));
        }

        /* (non-Javadoc)
         * @see android.app.Activity#setContentView(android.view.View, android.view.ViewGroup.LayoutParams)
         */
        //@Override
        public override void SetContentView(View v, LayoutParams lparams)
        {
            base.SetContentView(v, lparams);
            mHelper.registerAboveContentView(v, lparams);
        }

        /* (non-Javadoc)
         * @see com.jeremyfeinstein.slidingmenu.lib.app.SlidingActivityBase#setBehindContentView(int)
         */
        public void setBehindContentView(int id)
        {
            setBehindContentView(LayoutInflater.Inflate(id, null));
        }

        /* (non-Javadoc)
         * @see com.jeremyfeinstein.slidingmenu.lib.app.SlidingActivityBase#setBehindContentView(android.view.View)
         */
        public void setBehindContentView(View v)
        {
            setBehindContentView(v, new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent));
        }

        /* (non-Javadoc)
         * @see com.jeremyfeinstein.slidingmenu.lib.app.SlidingActivityBase#setBehindContentView(android.view.View, android.view.ViewGroup.LayoutParams)
         */
        public void setBehindContentView(View v, LayoutParams lparams)
        {
            mHelper.setBehindContentView(v, lparams);
        }

        /* (non-Javadoc)
         * @see com.jeremyfeinstein.slidingmenu.lib.app.SlidingActivityBase#getSlidingMenu()
         */
        public SlidingMenu getSlidingMenu()
        {
            return mHelper.getSlidingMenu();
        }

        /* (non-Javadoc)
         * @see com.jeremyfeinstein.slidingmenu.lib.app.SlidingActivityBase#toggle()
         */
        public void toggle()
        {
            mHelper.toggle();
        }

        /* (non-Javadoc)
         * @see com.jeremyfeinstein.slidingmenu.lib.app.SlidingActivityBase#showAbove()
         */
        public void showContent()
        {
            mHelper.showContent();
        }

        /* (non-Javadoc)
         * @see com.jeremyfeinstein.slidingmenu.lib.app.SlidingActivityBase#showBehind()
         */
        public void showMenu()
        {
            mHelper.showMenu();
        }

        /* (non-Javadoc)
         * @see com.jeremyfeinstein.slidingmenu.lib.app.SlidingActivityBase#showSecondaryMenu()
         */
        public void showSecondaryMenu()
        {
            mHelper.showSecondaryMenu();
        }

        /* (non-Javadoc)
         * @see com.jeremyfeinstein.slidingmenu.lib.app.SlidingActivityBase#setSlidingActionBarEnabled(bool)
         */
        public void setSlidingActionBarEnabled(bool b)
        {
            mHelper.setSlidingActionBarEnabled(b);
        }

        /* (non-Javadoc)
         * @see android.app.Activity#onKeyUp(int, android.view.KeyEvent)
         */
        //@Override
        public override bool OnKeyUp(Keycode keyCode, KeyEvent ev)
        {
            bool b = mHelper.OnKeyUp(keyCode, ev);
            if (b) return b;
            return base.OnKeyUp(keyCode, ev);
        }
    }
}
