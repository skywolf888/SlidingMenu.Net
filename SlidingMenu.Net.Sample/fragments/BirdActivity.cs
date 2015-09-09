//package com.jeremyfeinstein.slidingmenu.example.fragments;

//import android.app.Activity;
//import android.content.Intent;
//import android.content.res.TypedArray;
//import android.graphics.Color;
//import android.graphics.drawable.ColorDrawable;
//import android.os.Bundle;
//import android.os.Handler;
//import android.view.View;
//import android.view.View.OnClickListener;
//import android.view.Window;
//import android.widget.ImageView;
//import android.widget.ImageView.ScaleType;

//import com.actionbarsherlock.app.SherlockActivity;
//import com.actionbarsherlock.view.MenuItem;
//import com.jeremyfeinstein.slidingmenu.example.R;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
 

namespace Com.Jeremyfeinstein.SlidingMenu.Example.fragments
{

    [Activity(Label = "BirdActivity", Theme = "@style/ExampleTheme")]
    public class BirdActivity : AppCompatActivity
    {

        private Handler mHandler;

        public static Intent newInstance(Activity activity, int pos)
        {
            Intent intent = new Intent(activity, typeof(BirdActivity));
            intent.PutExtra("pos", pos);
            return intent;
        }

        //@Override
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Window.RequestFeature(WindowFeatures.ActionBarOverlay);
            int pos = 0;
            if (Intent.Extras != null)
            {
                pos = Intent.Extras.GetInt("pos");
            }

            string[] birds = Resources.GetStringArray(Resource.Array.birds);
            TypedArray imgs = Resources.ObtainTypedArray(Resource.Array.birds_img);
            int resId = imgs.GetResourceId(pos, -1);

            Title = birds[pos];
            //Window.RequestFeature(WindowFeatures.ActionBarOverlay);
            ColorDrawable color = new ColorDrawable(Color.Black);
            color.SetAlpha(128);

            SupportActionBar.SetBackgroundDrawable(color);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            mHandler = new Handler();

            ImageView imageView = new ImageView(this);
            imageView.SetScaleType(Android.Widget.ImageView.ScaleType.CenterInside);
            imageView.SetImageResource(resId);
            //imageView.setOnClickListener(new OnClickListener() {
            //    public void onClick(View v) {
            //        getSupportActionBar().show();
            //        hideActionBarDelayed(mHandler);
            //    }
            //});
            imageView.Click += delegate
            {
                SupportActionBar.Show();
                hideActionBarDelayed(mHandler);
            };

            SetContentView(imageView);
            this.Window.SetBackgroundDrawableResource(Android.Resource.Color.Black);
        }

        //@Override
        protected override void OnResume()
        {
            base.OnResume();
            SupportActionBar.Show();
            hideActionBarDelayed(mHandler);
        }

        //@Override
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        class postclass : Java.Lang.Object, IRunnable
        {
            private BirdActivity birdActivity;

            public postclass(BirdActivity birdActivity)
            {
                // TODO: Complete member initialization
                this.birdActivity = birdActivity;
            }

			
            public void Run()
            {
                birdActivity.SupportActionBar.Hide();
            }
        }

        private void hideActionBarDelayed(Handler handler)
        {

            handler.PostDelayed(new postclass(this),2000);

            //handler.postDelayed(new Runnable() {
            //    public void run() {
            //        getSupportActionBar().hide();
            //    }
            //}, 2000);
        }
         
    }
}