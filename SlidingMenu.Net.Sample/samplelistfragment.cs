//package com.jeremyfeinstein.slidingmenu.example;

using Android.Content;
using Android.OS;
//import android.content.Context;
//import android.os.Bundle;
//import android.support.v4.app.ListFragment;
//import android.view.LayoutInflater;
//import android.view.View;
//import android.view.ViewGroup;
//import android.widget.ArrayAdapter;
//import android.widget.ImageView;
//import android.widget.TextView;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
 

namespace Com.Jeremyfeinstein.SlidingMenu.Example
{

     
    public class SampleListFragment : ListFragment
    {

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.list, null);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            SampleAdapter adapter = new SampleAdapter(this.Activity);
            for (int i = 0; i < 20; i++)
            {
                adapter.Add(new SampleItem("Sample List", Android.Resource.Drawable.IcMenuSearch));
            }
            ListAdapter = adapter;
        }
        
    }
    public class SampleItem
    {
        public string tag;
        public int iconRes;
        public SampleItem(string tag, int iconRes)
        {
            this.tag = tag;
            this.iconRes = iconRes;
        }
    }
    public class SampleAdapter : ArrayAdapter<SampleItem>
    {
        private Context mContext;


        public SampleAdapter(Context context)
            : base(context, 0)
        {
            mContext = context;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.row, null);
            }
            ImageView icon = (ImageView)convertView.FindViewById(Resource.Id.row_icon);
            icon.SetImageResource(GetItem(position).iconRes);
            TextView title = (TextView)convertView.FindViewById(Resource.Id.row_title);

            title.Text = GetItem(position).tag;

            return convertView;
        }

    }
}
