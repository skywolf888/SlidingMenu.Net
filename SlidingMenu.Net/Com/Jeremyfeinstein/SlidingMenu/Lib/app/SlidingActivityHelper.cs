//package com.jeremyfeinstein.slidingmenu.lib.app;

//import android.app.Activity;
//import android.os.Bundle;
//import android.os.Handler;
//import android.view.KeyEvent;
//import android.view.LayoutInflater;
//import android.view.View;
//import android.view.ViewGroup.LayoutParams;

//import com.jeremyfeinstein.slidingmenu.lib.R;
//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu;
using Android.App;
using Android.OS;
using Android.Views;
using Java.Lang;



namespace Com.Jeremyfeinstein.SlidingMenu.Lib.app
{
    public class SlidingActivityHelper
    {

        private Activity mActivity;

        private SlidingMenu mSlidingMenu;

        private View mViewAbove;

        private View mViewBehind;

        private bool mBroadcasting = false;

        private bool mOnPostCreateCalled = false;

        private bool mEnableSlide = true;

        /**
         * Instantiates a new SlidingActivityHelper.
         *
         * @param activity the associated activity
         */
        public SlidingActivityHelper(Activity activity)
        {
            mActivity = activity;
        }

        /**
         * Sets mSlidingMenu as a newly inflated SlidingMenu. Should be called within the activitiy's onCreate()
         *
         * @param savedInstanceState the saved instance state (unused)
         */
        public void OnCreate(Bundle savedInstanceState)
        {
            mSlidingMenu = (SlidingMenu)LayoutInflater.From(mActivity).Inflate(Resource.Layout.slidingmenumain, null);
        }

        /**
         * Further SlidingMenu initialization. Should be called within the activitiy's onPostCreate()
         *
         * @param savedInstanceState the saved instance state (unused)
         */
        public void OnPostCreate(Bundle savedInstanceState)
        {
            if (mViewBehind == null || mViewAbove == null)
            {
                throw new Java.Lang.IllegalStateException("Both setBehindContentView must be called " +
                        "in onCreate in addition to setContentView.");
            }

            mOnPostCreateCalled = true;

            mSlidingMenu.attachToActivity(mActivity,
                    mEnableSlide ? SlidingMenu.SLIDING_WINDOW : SlidingMenu.SLIDING_CONTENT);

            bool open;
            bool secondary;
            if (savedInstanceState != null)
            {
                open = savedInstanceState.GetBoolean("SlidingActivityHelper.open");
                secondary = savedInstanceState.GetBoolean("SlidingActivityHelper.secondary");
            }
            else
            {
                open = false;
                secondary = false;
            }
            
            //new Handler().post(new Runnable() {
            //    public void run() {
            //        if (open) {
            //            if (secondary) {
            //                mSlidingMenu.showSecondaryMenu(false);
            //            } else {
            //                mSlidingMenu.showMenu(false);
            //            }
            //        } else {
            //            mSlidingMenu.showContent(false);					
            //        }
            //    }
            //});

             
            new Handler().Post(new runclass(open, secondary, mSlidingMenu));
        }

        class runclass : Java.Lang.Object, IRunnable {
            private bool open;
            private bool secondary;
            private SlidingMenu mSlidingMenu;

            public runclass(bool open, bool secondary, SlidingMenu mSlidingMenu)
            {
                // TODO: Complete member initialization
                this.open = open;
                this.secondary = secondary;
                this.mSlidingMenu = mSlidingMenu;
            }
             

            public void Run()
            {
                if (open)
                {
                     if (secondary)
                     {
                         mSlidingMenu.showSecondaryMenu(false);
                     }
                     else
                     {
                         mSlidingMenu.showMenu(false);
                     }
                 }
                 else
                 {
                     mSlidingMenu.showContent(false);
                 }
            }
        }

        /**
         * Controls whether the ActionBar slides along with the above view when the menu is opened,
         * or if it stays in place.
         *
         * @param slidingActionBarEnabled True if you want the ActionBar to slide along with the SlidingMenu,
         * false if you want the ActionBar to stay in place
         */
        public void setSlidingActionBarEnabled(bool slidingActionBarEnabled)
        {
            if (mOnPostCreateCalled)
                throw new Java.Lang.IllegalStateException("enableSlidingActionBar must be called in onCreate.");
            mEnableSlide = slidingActionBarEnabled;
        }

        /**
         * Finds a view that was identified by the id attribute from the XML that was processed in onCreate(Bundle).
         * 
         * @param id the resource id of the desired view
         * @return The view if found or null otherwise.
         */
        public View FindViewById(int id)
        {
            View v;
            if (mSlidingMenu != null)
            {
                v = mSlidingMenu.FindViewById(id);
                if (v != null)
                    return v;
            }
            return null;
        }

        /**
         * Called to retrieve per-instance state from an activity before being killed so that the state can be
         * restored in onCreate(Bundle) or onRestoreInstanceState(Bundle) (the Bundle populated by this method
         * will be passed to both). 
         *
         * @param outState Bundle in which to place your saved state.
         */
        public void onSaveInstanceState(Bundle outState)
        {
            outState.PutBoolean("SlidingActivityHelper.open", mSlidingMenu.isMenuShowing());
            outState.PutBoolean("SlidingActivityHelper.secondary", mSlidingMenu.isSecondaryMenuShowing());
        }

        /**
         * Register the above content view.
         *
         * @param v the above content view to register
         * @param params LayoutParams for that view (unused)
         */
        public void registerAboveContentView(View v, ViewGroup.LayoutParams lparams)
        {
            if (!mBroadcasting)
                mViewAbove = v;
        }

        /**
         * Set the activity content to an explicit view. This view is placed directly into the activity's view
         * hierarchy. It can itself be a complex view hierarchy. When calling this method, the layout parameters
         * of the specified view are ignored. Both the width and the height of the view are set by default to
         * MATCH_PARENT. To use your own layout parameters, invoke setContentView(android.view.View,
         * android.view.ViewGroup.LayoutParams) instead.
         *
         * @param v The desired content to display.
         */
        public void setContentView(View v)
        {
            mBroadcasting = true;
            mActivity.SetContentView(v);
        }

        /**
         * Set the behind view content to an explicit view. This view is placed directly into the behind view 's view hierarchy.
         * It can itself be a complex view hierarchy.
         *
         * @param view The desired content to display.
         * @param layoutParams Layout parameters for the view. (unused)
         */
        public void setBehindContentView(View view, ViewGroup.LayoutParams layoutParams)
        {
            mViewBehind = view;
            mSlidingMenu.setMenu(mViewBehind);
        }

        /**
         * Gets the SlidingMenu associated with this activity.
         *
         * @return the SlidingMenu associated with this activity.
         */
        public SlidingMenu getSlidingMenu()
        {
            return mSlidingMenu;
        }

        /**
         * Toggle the SlidingMenu. If it is open, it will be closed, and vice versa.
         */
        public void toggle()
        {
            mSlidingMenu.toggle();
        }

        /**
         * Close the SlidingMenu and show the content view.
         */
        public void showContent()
        {
            mSlidingMenu.showContent();
        }

        /**
         * Open the SlidingMenu and show the menu view.
         */
        public void showMenu()
        {
            mSlidingMenu.showMenu();
        }

        /**
         * Open the SlidingMenu and show the secondary menu view. Will default to the regular menu
         * if there is only one.
         */
        public void showSecondaryMenu()
        {
            mSlidingMenu.showSecondaryMenu();
        }

        /**
         * On key up.
         *
         * @param keyCode the key code
         * @param event the event
         * @return true, if successful
         */
        public bool OnKeyUp(Keycode keyCode, KeyEvent ev)
        {
            if (keyCode == Keycode.Back && mSlidingMenu.isMenuShowing())
            {
                showContent();
                return true;
            }
            return false;
        }

    }
}