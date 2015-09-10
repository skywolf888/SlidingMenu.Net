//package com.jeremyfeinstein.slidingmenu.lib;

//import android.content.Context;
//import android.graphics.Bitmap;
//import android.graphics.Canvas;
//import android.graphics.Color;
//import android.graphics.Paint;
//import android.graphics.drawable.Drawable;
//import android.util.AttributeSet;
//import android.util.Log;
//import android.util.TypedValue;
//import android.view.MotionEvent;
//import android.view.View;
//import android.view.ViewGroup;

using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu.CanvasTransformer;
using Android.Views;
using System;

namespace Com.Jeremyfeinstein.SlidingMenu.Lib
{
    public class CustomViewBehind : ViewGroup
    {

        private static readonly string TAG = "CustomViewBehind";

        private static readonly int MARGIN_THRESHOLD = 48; // dips
        private int mTouchMode = SlidingMenu.TOUCHMODE_MARGIN;

        private CustomViewAbove mViewAbove;

        private View mContent;
        private View mSecondaryContent;
        private int mMarginThreshold;
        private int mWidthOffset;
        private ICanvasTransformer mTransformer;
        private bool mChildrenEnabled ;

        public CustomViewBehind(Context context)
            : this(context, null)
        {

        }

        public CustomViewBehind(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {

            mMarginThreshold = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip,
                    MARGIN_THRESHOLD, Resources.DisplayMetrics);
        }

        public void setCustomViewAbove(CustomViewAbove customViewAbove)
        {
            mViewAbove = customViewAbove;
        }

        public void setCanvasTransformer(ICanvasTransformer t)
        {
            mTransformer = t;
        }

        public void setWidthOffset(int i)
        {
            mWidthOffset = i;
            RequestLayout();
        }

        public void setMarginThreshold(int marginThreshold)
        {
            mMarginThreshold = marginThreshold;
        }

        public int getMarginThreshold()
        {
            return mMarginThreshold;
        }

        public int getBehindWidth()
        {
            return mContent.Width;
        }

        public void setContent(View v)
        {
            if (mContent != null)
                RemoveView(mContent);
            mContent = v;

            AddView(mContent);
        }

        public View getContent()
        {
            return mContent;
        }

        /**
         * Sets the secondary (right) menu for use when setMode is called with SlidingMenu.LEFT_RIGHT.
         * @param v the right menu
         */
        public void setSecondaryContent(View v)
        {
            if (mSecondaryContent != null)
                RemoveView(mSecondaryContent);
            mSecondaryContent = v;
            AddView(mSecondaryContent);
        }

        public View getSecondaryContent()
        {
            return mSecondaryContent;
        }

        public void setChildrenEnabled(bool enabled)
        {
            mChildrenEnabled = enabled;
        }

        //@Override
        public override void ScrollTo(int x, int y)
        {
            base.ScrollTo(x, y);
            if (mTransformer != null)
                Invalidate();
        }

        //@Override
        public override bool OnInterceptTouchEvent(MotionEvent e)
        {
            return !mChildrenEnabled;
        }

        //@Override
        public override bool OnTouchEvent(MotionEvent e)
        {
            return !mChildrenEnabled;
        }

        //@Override
        protected override void DispatchDraw(Canvas canvas)
        {
            if (mTransformer != null)
            {
                canvas.Save();
                mTransformer.transformCanvas(canvas, mViewAbove.getPercentOpen());
                base.DispatchDraw(canvas);
                canvas.Restore();
            }
            else
                base.DispatchDraw(canvas);
        }

        //@Override
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            int width = r - l;
            int height = b - t;
            mContent.Layout(0, 0, width - mWidthOffset, height);
            if (mSecondaryContent != null)
                mSecondaryContent.Layout(0, 0, width - mWidthOffset, height);

        }

        //@Override
        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            int width = GetDefaultSize(0, widthMeasureSpec);
            int height = GetDefaultSize(0, heightMeasureSpec);
            SetMeasuredDimension(width, height);
            int contentWidth = GetChildMeasureSpec(widthMeasureSpec, 0, width - mWidthOffset);
            int contentHeight = GetChildMeasureSpec(heightMeasureSpec, 0, height);
            mContent.Measure(contentWidth, contentHeight);
            if (mSecondaryContent != null)
                mSecondaryContent.Measure(contentWidth, contentHeight);
        }

        private SlidingMenuMode mMode;
        private bool mFadeEnabled;
        private readonly Paint mFadePaint = new Paint();
        private float mScrollScale;
        private Drawable mShadowDrawable;
        private Drawable mSecondaryShadowDrawable;
        private int mShadowWidth;
        private float mFadeDegree;


        //public void setMode(SlidingMenuMode mode)
        //{
        //    if (mode == SlidingMenuMode.LEFT || mode == SlidingMenuMode.RIGHT)
        //    {
        //        if (mContent != null)
        //            mContent.Visibility = ViewStates.Visible;
        //        if (mSecondaryContent != null)
        //            mSecondaryContent.Visibility = ViewStates.Invisible;
        //    }
        //    mMode = mode;
        //}

        //public SlidingMenuMode getMode()
        //{
        //    return mMode;
        //}

        public SlidingMenuMode Mode
        {
            get { return mMode; }
            set
            {
                if (value == SlidingMenuMode.LEFT || value == SlidingMenuMode.RIGHT)
                {
                    if (mContent != null)
                        mContent.Visibility = ViewStates.Visible;
                    if (mSecondaryContent != null)
                        mSecondaryContent.Visibility = ViewStates.Invisible;
                }
                mMode = value;
            }
        }

        public void setScrollScale(float scrollScale)
        {
            mScrollScale = scrollScale;
        }

        public float getScrollScale()
        {
            return mScrollScale;
        }

        public void setShadowDrawable(Drawable shadow)
        {
            mShadowDrawable = shadow;
            Invalidate();
        }

        public void setSecondaryShadowDrawable(Drawable shadow)
        {
            mSecondaryShadowDrawable = shadow;
            Invalidate();
        }

        public void setShadowWidth(int width)
        {
            mShadowWidth = width;
            Invalidate();
        }

        public void setFadeEnabled(bool b)
        {
            mFadeEnabled = b;
        }

        public void setFadeDegree(float degree)
        {
            if (degree > 1.0f || degree < 0.0f)
                throw new Java.Lang.IllegalStateException("The BehindFadeDegree must be between 0.0f and 1.0f");
            mFadeDegree = degree;
        }

        public int getMenuPage(int page)
        {
            page = (page > 1) ? 2 : ((page < 1) ? 0 : page);
            if (mMode == SlidingMenuMode.LEFT && page > 1)
            {
                return 0;
            }
            else if (mMode == SlidingMenuMode.RIGHT && page < 1)
            {
                return 2;
            }
            else
            {
                return page;
            }
        }

        public void scrollBehindTo(View content, int x, int y)
        {
            ViewStates vis = ViewStates.Visible;
            if (mMode == SlidingMenuMode.LEFT)
            {
                if (x >= content.Left) vis = ViewStates.Invisible;
                ScrollTo((int)((x + getBehindWidth()) * mScrollScale), y);
            }
            else if (mMode == SlidingMenuMode.RIGHT)
            {
                if (x <= content.Left) vis = ViewStates.Invisible;
                ScrollTo((int)(getBehindWidth() - Width +
                        (x - getBehindWidth()) * mScrollScale), y);
            }
            else if (mMode == SlidingMenuMode.LEFT_RIGHT)
            {
                mContent.Visibility = x >= content.Left ? ViewStates.Invisible : ViewStates.Visible;
                mSecondaryContent.Visibility = x <= content.Left ? ViewStates.Invisible : ViewStates.Visible;
                vis = x == 0 ? ViewStates.Invisible : ViewStates.Visible;
                if (x <= content.Left)
                {
                    ScrollTo((int)((x + getBehindWidth()) * mScrollScale), y);
                }
                else
                {
                    ScrollTo((int)(getBehindWidth() - Width +
                            (x - getBehindWidth()) * mScrollScale), y);
                }
            }
            if (vis == ViewStates.Invisible)
                Log.Verbose(TAG, "behind INVISIBLE");
            Visibility = vis;
        }

        public int getMenuLeft(View content, int page)
        {
            if (mMode == SlidingMenuMode.LEFT)
            {
                switch (page)
                {
                    case 0:
                        return content.Left - getBehindWidth();
                    case 2:
                        return content.Left;
                }
            }
            else if (mMode == SlidingMenuMode.RIGHT)
            {
                switch (page)
                {
                    case 0:
                        return content.Left;
                    case 2:
                        return content.Left + getBehindWidth();
                }
            }
            else if (mMode == SlidingMenuMode.LEFT_RIGHT)
            {
                switch (page)
                {
                    case 0:
                        return content.Left - getBehindWidth();
                    case 2:
                        return content.Left + getBehindWidth();
                }
            }
            return content.Left;
        }

        public int getAbsLeftBound(View content)
        {
            if (mMode == SlidingMenuMode.LEFT || mMode == SlidingMenuMode.LEFT_RIGHT)
            {
                return content.Left - getBehindWidth();
            }
            else if (mMode == SlidingMenuMode.RIGHT)
            {
                return content.Left;
            }
            return 0;
        }

        public int getAbsRightBound(View content)
        {
            if (mMode == SlidingMenuMode.LEFT)
            {
                return content.Left;
            }
            else if (mMode == SlidingMenuMode.RIGHT || mMode == SlidingMenuMode.LEFT_RIGHT)
            {
                return content.Left + getBehindWidth();
            }
            return 0;
        }

        public bool marginTouchAllowed(View content, int x)
        {
            int left = content.Left;
            int right = content.Right;
            if (mMode == SlidingMenuMode.LEFT)
            {
                return (x >= left && x <= mMarginThreshold + left);
            }
            else if (mMode == SlidingMenuMode.RIGHT)
            {
                return (x <= right && x >= right - mMarginThreshold);
            }
            else if (mMode == SlidingMenuMode.LEFT_RIGHT)
            {
                return (x >= left && x <= mMarginThreshold + left) ||
                        (x <= right && x >= right - mMarginThreshold);
            }
            return false;
        }

        public void setTouchMode(int i)
        {
            mTouchMode = i;
        }

        public bool menuOpenTouchAllowed(View content, int currPage, float x)
        {
            switch (mTouchMode)
            {
                case SlidingMenu.TOUCHMODE_FULLSCREEN:
                    return true;
                case SlidingMenu.TOUCHMODE_MARGIN:
                    return menuTouchInQuickReturn(content, currPage, x);
            }
            return false;
        }

        public bool menuTouchInQuickReturn(View content, int currPage, float x)
        {
            if (mMode == SlidingMenuMode.LEFT || (mMode == SlidingMenuMode.LEFT_RIGHT && currPage == 0))
            {

                return x >= content.Left;
            }
            else if (mMode == SlidingMenuMode.RIGHT || (mMode == SlidingMenuMode.LEFT_RIGHT && currPage == 2))
            {
                return x <= content.Right;
            }
            return false;
        }

        public bool menuClosedSlideAllowed(float dx)
        {
            if (mMode == SlidingMenuMode.LEFT)
            {
                return dx > 0;
            }
            else if (mMode == SlidingMenuMode.RIGHT)
            {
                return dx < 0;
            }
            else if (mMode == SlidingMenuMode.LEFT_RIGHT)
            {
                return true;
            }
            return false;
        }

        public bool menuOpenSlideAllowed(float dx)
        {
            if (mMode == SlidingMenuMode.LEFT)
            {
                return dx < 0;
            }
            else if (mMode == SlidingMenuMode.RIGHT)
            {
                return dx > 0;
            }
            else if (mMode == SlidingMenuMode.LEFT_RIGHT)
            {
                return true;
            }
            return false;
        }

        public void drawShadow(View content, Canvas canvas)
        {
            if (mShadowDrawable == null || mShadowWidth <= 0) return;
            int left = 0;
            if (mMode == SlidingMenuMode.LEFT)
            {
                left = content.Left - mShadowWidth;
            }
            else if (mMode == SlidingMenuMode.RIGHT)
            {
                left = content.Right;
            }
            else if (mMode == SlidingMenuMode.LEFT_RIGHT)
            {
                if (mSecondaryShadowDrawable != null)
                {
                    left = content.Right;
                    mSecondaryShadowDrawable.SetBounds(left, 0, left + mShadowWidth, Height);
                    mSecondaryShadowDrawable.Draw(canvas);
                }
                left = content.Left - mShadowWidth;
            }
            mShadowDrawable.SetBounds(left, 0, left + mShadowWidth, Height);
            mShadowDrawable.Draw(canvas);
        }

        public void drawFade(View content, Canvas canvas, float openPercent)
        {
            if (!mFadeEnabled) return;
            int alpha = (int)(mFadeDegree * 255 * Math.Abs(1 - openPercent));
            mFadePaint.Color = Color.Argb(alpha, 0, 0, 0);
            int left = 0;
            int right = 0;
            if (mMode == SlidingMenuMode.LEFT)
            {
                left = content.Left - getBehindWidth();
                right = content.Left;
            }
            else if (mMode == SlidingMenuMode.RIGHT)
            {
                left = content.Right;
                right = content.Right + getBehindWidth();
            }
            else if (mMode == SlidingMenuMode.LEFT_RIGHT)
            {
                left = content.Left - getBehindWidth();
                right = content.Left;
                canvas.DrawRect(left, 0, right, Height, mFadePaint);
                left = content.Right;
                right = content.Right + getBehindWidth();
            }
            canvas.DrawRect(left, 0, right, Height, mFadePaint);
        }

        private bool mSelectorEnabled = true;
        private Bitmap mSelectorDrawable;
        private View mSelectedView;

        public void drawSelector(View content, Canvas canvas, float openPercent)
        {
            if (!mSelectorEnabled) return;
            if (mSelectorDrawable != null && mSelectedView != null)
            {
                string tag = (string)mSelectedView.GetTag(Resource.Id.selected_view);
                if (tag.Equals(TAG + "SelectedView"))
                {
                    canvas.Save();
                    int left, right, offset;
                    offset = (int)(mSelectorDrawable.Width * openPercent);
                    if (mMode == SlidingMenuMode.LEFT)
                    {
                        right = content.Left;
                        left = right - offset;
                        canvas.ClipRect(left, 0, right, Height);
                        canvas.DrawBitmap(mSelectorDrawable, left, getSelectorTop(), null);
                    }
                    else if (mMode == SlidingMenuMode.RIGHT)
                    {
                        left = content.Right;
                        right = left + offset;
                        canvas.ClipRect(left, 0, right, Height);
                        canvas.DrawBitmap(mSelectorDrawable, right - mSelectorDrawable.Width, getSelectorTop(), null);
                    }
                    canvas.Restore();
                }
            }
        }

        public void setSelectorEnabled(bool b)
        {
            mSelectorEnabled = b;
        }

        public void setSelectedView(View v)
        {
            if (mSelectedView != null)
            {
                mSelectedView.SetTag(Resource.Id.selected_view, null);
                mSelectedView = null;
            }
            if (v != null && v.Parent != null)
            {
                mSelectedView = v;
                mSelectedView.SetTag(Resource.Id.selected_view, TAG + "SelectedView");
                Invalidate();
            }
        }

        private int getSelectorTop()
        {
            int y = mSelectedView.Top;
            y += (mSelectedView.Height - mSelectorDrawable.Height) / 2;
            return y;
        }

        public void setSelectorBitmap(Bitmap b)
        {
            mSelectorDrawable = b;
            RefreshDrawableState();
        }

    }
}