//package com.jeremyfeinstein.slidingmenu.example.anim;

//import android.graphics.Canvas;
//import android.view.animation.Interpolator;

//import com.jeremyfeinstein.slidingmenu.example.R;
//import com.jeremyfeinstein.slidingmenu.example.Resource.String;
//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu.CanvasTransformer;

using Android.App;
using Android.Graphics;
using Android.Views.Animations;
using Com.Jeremyfeinstein.SlidingMenu.Lib;
 

namespace Com.Jeremyfeinstein.SlidingMenu.Example.anim
{
    [Activity(Label = "CustomSlideAnimation", Theme = "@style/ExampleTheme")]
    public class CustomSlideAnimation : CustomAnimation
    {

        class interpclass : Java.Lang.Object, IInterpolator
        {

            public float GetInterpolation(float t)
            {
                t -= 1.0f;
                return t * t * t + 1.0f;
            }
        }

        private static IInterpolator interp = new interpclass();

        class SlideClass : Java.Lang.Object, ICanvasTransformer
        {

            public void transformCanvas(Canvas canvas, float percentOpen)
            {
                canvas.Translate(0, canvas.Height * (1 - CustomSlideAnimation.interp.GetInterpolation(percentOpen)));
            }
        }

        public CustomSlideAnimation()
            : base(Resource.String.anim_slide, new SlideClass())
        {
            // see the class CustomAnimation for how to attach 
            // the CanvasTransformer to the SlidingMenu
            //super(Resource.String.anim_slide, new CanvasTransformer() {
            //    @Override
            //    public void transformCanvas(Canvas canvas, float percentOpen) {
            //        canvas.translate(0, canvas.getHeight()*(1-interp.getInterpolation(percentOpen)));
            //    }			
            //});
        }

    }
}