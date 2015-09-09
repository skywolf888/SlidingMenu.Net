//package com.jeremyfeinstein.slidingmenu.lib;

//import android.graphics.Canvas;
//import android.graphics.drawable.Drawable;
//import android.view.View;


using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;

namespace Com.Jeremyfeinstein.SlidingMenu.Lib
{
    public interface IMenuInterface
    {

        void scrollBehindTo(int x, int y,
              CustomViewBehind cvb, float scrollScale);

        int getMenuLeft(CustomViewBehind cvb, View content);

        int getAbsLeftBound(CustomViewBehind cvb, View content);

        int getAbsRightBound(CustomViewBehind cvb, View content);

        bool marginTouchAllowed(View content, int x, int threshold);

        bool menuOpenTouchAllowed(View content, int currPage, int x);

        bool menuTouchInQuickReturn(View content, int currPage, int x);

        bool menuClosedSlideAllowed(int x);

        bool menuOpenSlideAllowed(int x);

        void drawShadow(Canvas canvas, Drawable shadow, int width);

        void drawFade(Canvas canvas, int alpha,
              CustomViewBehind cvb, View content);

        void drawSelector(View content, Canvas canvas, float percentOpen);

    }
}