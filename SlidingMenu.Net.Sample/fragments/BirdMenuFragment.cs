//package com.jeremyfeinstein.slidingmenu.example.fragments;

//import android.os.Bundle;
//import android.support.v4.app.Fragment;
//import android.support.v4.app.ListFragment;
//import android.view.LayoutInflater;
//import android.view.View;
//import android.view.ViewGroup;
//import android.widget.ArrayAdapter;
//import android.widget.ListView;

//import com.jeremyfeinstein.slidingmenu.example.R;

using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
 

namespace Com.Jeremyfeinstein.SlidingMenu.Example.fragments
{
    public class BirdMenuFragment : ListFragment
    {

        //@Override
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.list, null);
        }

        //@Override
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            string[] birds = Resources.GetStringArray(Resource.Array.birds);
            ArrayAdapter<string> colorAdapter = new ArrayAdapter<string>(this.Activity,
                    Android.Resource.Layout.SimpleListItem1, Android.Resource.Id.Text1, birds);
            ListAdapter = colorAdapter;
        }

         

        //@Override
        public override void OnListItemClick(ListView lv, View v, int position, long id)
        {
            Fragment newContent = new BirdGridFragment(position);
            if (newContent != null)
                switchFragment(newContent);
        }

        // the meat of switching the above fragment
        private void switchFragment(Fragment fragment)
        {
            if (this.Activity == null)
                return;

            if (this.Activity is ResponsiveUIActivity)
            {
                ResponsiveUIActivity ra = (ResponsiveUIActivity)this.Activity;
                ra.switchContent(fragment);
            }
        }
    }
}
