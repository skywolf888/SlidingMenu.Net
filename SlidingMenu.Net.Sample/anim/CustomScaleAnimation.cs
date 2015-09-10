//package com.jeremyfeinstein.slidingmenu.example.anim;

//import android.graphics.Canvas;

using Android.App;
using Android.Graphics;
//import com.jeremyfeinstein.slidingmenu.example.R;
//import com.jeremyfeinstein.slidingmenu.example.Resource.String;
//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu.CanvasTransformer;
using Com.Jeremyfeinstein.SlidingMenu.Lib;
 

namespace Com.Jeremyfeinstein.SlidingMenu.Example.anim
{
     [Activity(Label = "CustomScaleAnimation", Theme = "@style/ExampleTheme")]
    public class CustomScaleAnimation : CustomAnimation
    {

        class ScaleClass : Java.Lang.Object, ICanvasTransformer
        {

            public void transformCanvas(Canvas canvas, float percentOpen)
            {
                canvas.Scale(percentOpen, 1, 0, 0);
            }
        }

        public CustomScaleAnimation()
            : base(Resource.String.anim_scale, new ScaleClass())
        {
            //super(Resource.String.anim_scale, new CanvasTransformer() {
            //    @Override
            //    public void transformCanvas(Canvas canvas, float percentOpen) {
            //        canvas.scale(percentOpen, 1, 0, 0);
            //    }			
            //});
        }

    }
}