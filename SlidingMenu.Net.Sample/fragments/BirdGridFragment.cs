//package com.jeremyfeinstein.slidingmenu.example.fragments;

//import android.content.res.TypedArray;
//import android.os.Bundle;
//import android.support.v4.app.Fragment;
//import android.view.LayoutInflater;
//import android.view.View;
//import android.view.ViewGroup;
//import android.widget.AdapterView;
//import android.widget.AdapterView.OnItemClickListener;
//import android.widget.BaseAdapter;
//import android.widget.GridView;
//import android.widget.ImageView;

using Android.Content;
using Android.Content.Res;
//import com.jeremyfeinstein.slidingmenu.example.R;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
 

namespace Com.Jeremyfeinstein.SlidingMenu.Example.fragments
{
    public class BirdGridFragment : Fragment
    {

        private int mPos = -1;
        private static int mImgRes;

        public BirdGridFragment() { }
        public BirdGridFragment(int pos)
        {
            mPos = pos;
        }

        //@Override
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (mPos == -1 && savedInstanceState != null)
                mPos = savedInstanceState.GetInt("mPos");
            TypedArray imgs = Resources.ObtainTypedArray(Resource.Array.birds_img);
            mImgRes = imgs.GetResourceId(mPos, -1);

            GridView gv = (GridView)inflater.Inflate(Resource.Layout.list_grid, null);
            gv.SetBackgroundResource(Android.Resource.Color.Black);
            gv.Adapter=new GridAdapter(this);
            //gv.setOnItemClickListener(new OnItemClickListener() {
            //    @Override
            //    public void onItemClick(AdapterView<?> parent, View view, int position,
            //            long id) {
            //        if (getActivity() == null)
            //            return;
            //        ResponsiveUIActivity activity = (ResponsiveUIActivity) getActivity();
            //        activity.onBirdPressed(mPos);
            //    }			
            //});

            gv.ItemClick += delegate { 
                 if (this.Activity == null)
                     return;
                 ResponsiveUIActivity activity = (ResponsiveUIActivity)this.Activity;
                 activity.onBirdPressed(mPos);
            };
            return gv;
        }

        //@Override
        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("mPos", mPos);
        }

        private class GridAdapter : BaseAdapter
        {
            private Fragment mContext;



            public GridAdapter(Fragment context)
            {
                mContext = context;
            }
            public override int Count
            {
                get { return 30; }
            }


            //@Override
            public override Object GetItem(int position)
            {
                return null;
            }

            //@Override
            public override long GetItemId(int position)
            {
                return position;
            }

            //@Override
            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                if (convertView == null)
                {
                    convertView = mContext.Activity.LayoutInflater.Inflate(Resource.Layout.grid_item, null);
                }
                ImageView img = (ImageView)convertView.FindViewById(Resource.Id.grid_item_img);
                img.SetImageResource(mImgRes);
                return convertView;
            }

        }
    }
}
