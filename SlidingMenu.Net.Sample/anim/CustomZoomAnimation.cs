//package com.jeremyfeinstein.slidingmenu.example.anim;

//import android.graphics.Canvas;

//import com.jeremyfeinstein.slidingmenu.example.R;
//import com.jeremyfeinstein.slidingmenu.example.Resource.String;
//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu.CanvasTransformer;


using Android.App;
using Android.Graphics;
using Android.Views.Animations;
using Com.Jeremyfeinstein.SlidingMenu.Lib;
 


namespace Com.Jeremyfeinstein.SlidingMenu.Example.anim
{
     [Activity(Label = "CustomZoomAnimation", Theme = "@style/ExampleTheme")]
    public class CustomZoomAnimation : CustomAnimation
    {


        class ZoomClass : Java.Lang.Object, ICanvasTransformer
        {

            public void transformCanvas(Canvas canvas, float percentOpen)
            {
                float scale = (float)(percentOpen * 0.25 + 0.75);
                canvas.Scale(scale, scale, canvas.Width / 2, canvas.Height / 2);
            }
        }
        public CustomZoomAnimation()
            : base(Resource.String.anim_zoom, new ZoomClass())
        {
            // see the class CustomAnimation for how to attach 
            // the CanvasTransformer to the SlidingMenu
            //super(Resource.String.anim_zoom, new CanvasTransformer() {
            //    @Override
            //    public void transformCanvas(Canvas canvas, float percentOpen) {
            //        float scale = (float) (percentOpen*0.25 + 0.75);
            //        canvas.scale(scale, scale, canvas.getWidth()/2, canvas.getHeight()/2);
            //    }
            //});
        }

    }
}