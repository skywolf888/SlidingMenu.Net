//package com.jeremyfeinstein.slidingmenu.example;

//import java.net.URLEncoder;

//import android.app.AlertDialog;
//import android.content.Context;
//import android.content.Intent;
//import android.net.Uri;
//import android.os.Bundle;
//import android.preference.Preference;
//import android.preference.PreferenceScreen;
//import android.text.Html;
//import android.view.View;
//import android.view.ViewGroup;
//import android.widget.ArrayAdapter;
//import android.widget.ImageView;
//import android.widget.ListView;
//import android.widget.TextView;
//import android.widget.Toast;

//import com.actionbarsherlock.app.SherlockPreferenceActivity;
//import com.actionbarsherlock.view.Menu;
//import com.actionbarsherlock.view.MenuItem;
//import com.crittercism.app.Crittercism;
//import com.jeremyfeinstein.slidingmenu.example.anim.CustomScaleAnimation;
//import com.jeremyfeinstein.slidingmenu.example.anim.CustomSlideAnimation;
//import com.jeremyfeinstein.slidingmenu.example.anim.CustomZoomAnimation;
//import com.jeremyfeinstein.slidingmenu.example.fragments.FragmentChangeActivity;
//import com.jeremyfeinstein.slidingmenu.example.fragments.ResponsiveUIActivity;



using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Text;
using Android.Views;
using Android.Widget;
using Com.Jeremyfeinstein.SlidingMenu.Example.anim;
using Com.Jeremyfeinstein.SlidingMenu.Example.fragments;
using Java.Net;
using SlidingMenu.Net.Sample;
using System;
 

namespace Com.Jeremyfeinstein.SlidingMenu.Example
{

	[Activity(Label = "SlidingMenu.Net.Sample", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class ExampleListActivity : PreferenceActivity
    {

        //@Override
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetTitle(Resource.String.app_name);

            //		Crittercism.init(getApplicationContext(), "508ab27601ed857a20000003");
            this.AddPreferencesFromResource(Resource.Xml.main);
        }

        //@Override
        public override bool OnPreferenceTreeClick(PreferenceScreen screen, Preference pref)
        {
            Type cls = null;

            //Class<?> cls = null;
            string title = pref.Title.ToString();
            if (title.Equals(GetString(Resource.String.properties))) {
                cls = typeof(PropertiesActivity);	
            } else if (title.Equals(GetString(Resource.String.attach))) {
                cls = typeof(AttachExample);
            } else if (title.Equals(GetString(Resource.String.changing_fragments))) {
                cls = typeof(FragmentChangeActivity);
            } else if (title.Equals(GetString(Resource.String.left_and_right))) {
                cls = typeof(LeftAndRightActivity);
            } else if (title.Equals(GetString(Resource.String.responsive_ui))) {
                cls = typeof(ResponsiveUIActivity);
            } else if (title.Equals(GetString(Resource.String.viewpager))) {
                cls = typeof(ViewPagerActivity);
            } else if (title.Equals(GetString(Resource.String.title_bar_slide))) {
                cls = typeof(SlidingTitleBar);
            } else if (title.Equals(GetString(Resource.String.title_bar_content))) {
                cls = typeof(SlidingContent);
            } else if (title.Equals(GetString(Resource.String.anim_zoom))) {
                cls = typeof(CustomZoomAnimation);
            } else if (title.Equals(GetString(Resource.String.anim_scale))) {
                cls = typeof(CustomScaleAnimation);
            } else if (title.Equals(GetString(Resource.String.anim_slide))) {
                cls = typeof(CustomSlideAnimation);
            }
            Android.Content.Intent intent = new Intent(this, cls);
            StartActivity(intent);
            return true;
        }

        //@Override
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId) {
            case Resource.Id.github:
                Util.goToGitHub(this);
                return true;
            case Resource.Id.about:
                new AlertDialog.Builder(this)
                .SetTitle(Resource.String.about)
                .SetMessage(Html.FromHtml(GetString(Resource.String.about_msg)))
                .Show();
                break;
            case Resource.Id.licenses:
                new AlertDialog.Builder(this)
                .SetTitle(Resource.String.licenses)
                .SetMessage(Html.FromHtml(GetString(Resource.String.apache_license)))
                .Show();
                break;
            case Resource.Id.contact:
                Intent email = new Intent(Android.Content.Intent.ActionSendto);
                String uriText = "mailto:jfeinstein10@gmail.com" +
                        "?subject=" + URLEncoder.Encode("SlidingMenu Demos Feedback"); 
                email.SetData(Android.Net.Uri.Parse(uriText));
                try {
                    StartActivity(email);
                } catch (Exception ex) {
                    Toast.MakeText(this, Resource.String.no_email, ToastLength.Short).Show();
                }
                break;
            }
            return base.OnOptionsItemSelected(item);
        }

        //@Override
        public override bool OnCreateOptionsMenu(IMenu menu)
        {

            //getSupportMenuInflater().inflate(Resource.Menu.example_list, menu);
            
            MenuInflater.Inflate(Resource.Menu.example_list, menu);
            return true;
        }

    }
}