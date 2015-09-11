//package com.jeremyfeinstein.slidingmenu.example;

//import android.content.Context;
//import android.content.Intent;
//import android.net.Uri;
using Android.Content;
using Android.Net;


namespace Com.Jeremyfeinstein.SlidingMenu.Example
{
    public class Util
    {

        public static void goToGitHub(Context context)
        {
			//
            //Uri uriUrl = Uri.Parse("http://github.com/jfeinstein10/slidingmenu");
			Uri uriUrl = Uri.Parse("https://github.com/skywolf888/SlidingMenu.Net");
            Intent launchBrowser = new Intent(Intent.ActionView, uriUrl);
            context.StartActivity(launchBrowser);
        }

    }
}