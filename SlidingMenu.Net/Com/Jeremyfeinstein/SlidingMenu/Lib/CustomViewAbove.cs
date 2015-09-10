//package com.jeremyfeinstein.slidingmenu.lib;

//import java.util.ArrayList;
//import java.util.List;

//import android.content.Context;
//import android.graphics.Canvas;
//import android.graphics.Rect;
//import android.os.Build;
//import android.support.v4.view.KeyEventCompat;
//import android.support.v4.view.MotionEventCompat;
//import android.support.v4.view.VelocityTrackerCompat;
//import android.support.v4.view.ViewCompat;
//import android.support.v4.view.ViewConfigurationCompat;
//import android.util.AttributeSet;
//import android.util.FloatMath;
//import android.util.Log;
//import android.view.FocusFinder;
//import android.view.KeyEvent;
//import android.view.MotionEvent;
//import android.view.SoundEffectConstants;
//import android.view.VelocityTracker;
//import android.view.View;
//import android.view.ViewConfiguration;
//import android.view.ViewGroup;
//import android.view.animation.Interpolator;
//import android.widget.Scroller;


//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu.OnClosedListener;
//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu.OnOpenedListener;
//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu.OnCloseListener;
//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu.OnOpenListener;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using System;
using System.Collections.Generic;


namespace Com.Jeremyfeinstein.SlidingMenu.Lib
{
    /**
         * Callback interface for responding to changing state of the selected page.
         */
    public interface IOnPageChangeListener
    {

        /**
         * This method will be invoked when the current page is scrolled, either as part
         * of a programmatically initiated smooth scroll or a user initiated touch scroll.
         *
         * @param position Position index of the first page currently being displayed.
         *                 Page position+1 will be visible if positionOffset is nonzero.
         * @param positionOffset Value from [0, 1) indicating the offset from the page at position.
         * @param positionOffsetPixels Value in pixels indicating the offset from position.
         */
        void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels);

        /**
         * This method will be invoked when a new page becomes selected. Animation is not
         * necessarily complete.
         *
         * @param position Position index of the new selected page.
         */
        void OnPageSelected(int position);

    }

    public class CustomViewAbove : ViewGroup
    {

        private static readonly string TAG = "CustomViewAbove";
        private static readonly bool DEBUG = true;

        private static readonly bool USE_CACHE = false;

        private static readonly int MAX_SETTLE_DURATION = 600; // ms
        private static readonly int MIN_DISTANCE_FOR_FLING = 25; // dips
        private static readonly IInterpolator sInterpolator = new IInterpolator1();
        //private static readonly IInterpolator sInterpolator = new Interpolator() {
        //    public float getInterpolation(float t) {
        //        t -= 1.0f;
        //        return t * t * t * t * t + 1.0f;
        //    }
        //};

        class IInterpolator1 : Java.Lang.Object, IInterpolator {

            public float GetInterpolation(float t)
            {
                t -= 1.0f;
                return t * t * t * t * t + 1.0f;
            }
        }

        private View mContent;

        private int mCurItem;
        private Scroller mScroller;

        private bool mScrollingCacheEnabled;

        private bool mScrolling;

        private bool mIsBeingDragged;
        private bool mIsUnableToDrag;
        private int mTouchSlop;
        private float mInitialMotionX;
        /**
         * Position of the last motion event.
         */
        private float mLastMotionX;
        private float mLastMotionY;
        /**
         * ID of the active pointer. This is used to retain consistency during
         * drags/flings if multiple pointers are used.
         */
        protected int mActivePointerId = INVALID_POINTER;
        /**
         * Sentinel value for no current active pointer.
         * Used by {@link #mActivePointerId}.
         */
        private static readonly int INVALID_POINTER = -1;

        /**
         * Determines speed during touch scrolling
         */
        protected VelocityTracker mVelocityTracker;
        private int mMinimumVelocity;
        protected int mMaximumVelocity;
        private int mFlingDistance;

        private CustomViewBehind mViewBehind;
        //	private int mMode;
        private bool mEnabled = true;

        private IOnPageChangeListener mOnPageChangeListener;
        private IOnPageChangeListener mInternalPageChangeListener;

        //	private OnCloseListener mCloseListener;
        //	private OnOpenListener mOpenListener;
        private IOnClosedListener mClosedListener;
        private IOnOpenedListener mOpenedListener;

        private List<View> mIgnoredViews = new List<View>();

        //	private int mScrollState = SCROLL_STATE_IDLE;

        

        /**
         * Simple implementation of the {@link OnPageChangeListener} interface with stub
         * implementations of each method. Extend this if you do not intend to override
         * every method of {@link OnPageChangeListener}.
         */
        public class SimpleOnPageChangeListener : Java.Lang.Object,IOnPageChangeListener
        {

            public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
            {
                // This space for rent
            }

            public virtual void OnPageSelected(int position)
            {
                // This space for rent
            }

            public void OnPageScrollStateChanged(int state)
            {
                // This space for rent
            }

        }

        public class InternalPageChangeListener : SimpleOnPageChangeListener
        {
            private CustomViewBehind mViewBehind;

            public InternalPageChangeListener(CustomViewBehind mViewBehind)
            {
                // TODO: Complete member initialization
                this.mViewBehind = mViewBehind;
            }
            public override void OnPageSelected(int position)
            {
                if (mViewBehind != null) {
                    switch (position)
                    {
                        case 0:
                        case 2:
                            mViewBehind.setChildrenEnabled(true);
                            break;
                        case 1:
                            mViewBehind.setChildrenEnabled(false);
                            break;
                    }
                }
            }
        }

        public CustomViewAbove(Context context)
            : this(context, null)
        {

        }

        public CustomViewAbove(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {

            initCustomViewAbove();
        }

        void initCustomViewAbove()
        {
            SetWillNotDraw(false);
            //setDescendantFocusability(FOCUS_AFTER_DESCENDANTS);
            DescendantFocusability = DescendantFocusability.AfterDescendants;

            Focusable = true;
            Context context = Context;
            mScroller = new Scroller(context, sInterpolator);
            ViewConfiguration configuration = ViewConfiguration.Get(context);
            mTouchSlop = ViewConfigurationCompat.GetScaledPagingTouchSlop(configuration);
            mMinimumVelocity = configuration.ScaledMinimumFlingVelocity;
            mMaximumVelocity = configuration.ScaledMaximumFlingVelocity;
             
            //setInternalPageChangeListener(new SimpleOnPageChangeListener() {
            //    public void onPageSelected(int position) {
            //        if (mViewBehind != null) {
            //            switch (position) {
            //            case 0:
            //            case 2:
            //                mViewBehind.setChildrenEnabled(true);
            //                break;
            //            case 1:
            //                mViewBehind.setChildrenEnabled(false);
            //                break;
            //            }
            //        }
            //    }
            //});
            setInternalPageChangeListener(new InternalPageChangeListener(mViewBehind));
            float density = context.Resources.DisplayMetrics.Density;
            mFlingDistance = (int)(MIN_DISTANCE_FOR_FLING * density);
        }

        /**
         * Set the currently selected page. If the CustomViewPager has already been through its first
         * layout there will be a smooth animated transition between the current item and the
         * specified item.
         *
         * @param item Item index to select
         */
        public void setCurrentItem(int item)
        {
            setCurrentItemInternal(item, true, false);
        }

        /**
         * Set the currently selected page.
         *
         * @param item Item index to select
         * @param smoothScroll True to smoothly scroll to the new item, false to transition immediately
         */
        public void setCurrentItem(int item, bool smoothScroll)
        {
            setCurrentItemInternal(item, smoothScroll, false);
        }

        public int getCurrentItem()
        {
            return mCurItem;
        }

        void setCurrentItemInternal(int item, bool smoothScroll, bool always)
        {
            setCurrentItemInternal(item, smoothScroll, always, 0);
        }

        void setCurrentItemInternal(int item, bool smoothScroll, bool always, int velocity)
        {
            if (!always && mCurItem == item)
            {
                setScrollingCacheEnabled(false);
                return;
            }

            item = mViewBehind.getMenuPage(item);

            bool dispatchSelected = mCurItem != item;
            mCurItem = item;
            int destX = getDestScrollX(mCurItem);
            if (dispatchSelected && mOnPageChangeListener != null)
            {
                mOnPageChangeListener.OnPageSelected(item);
            }
            if (dispatchSelected && mInternalPageChangeListener != null)
            {
                mInternalPageChangeListener.OnPageSelected(item);
            }
            if (smoothScroll)
            {
                smoothScrollTo(destX, 0, velocity);
            }
            else
            {
                completeScroll();
                ScrollTo(destX, 0);
            }
        }

        /**
         * Set a listener that will be invoked whenever the page changes or is incrementally
         * scrolled. See {@link OnPageChangeListener}.
         *
         * @param listener Listener to set
         */
        public void setOnPageChangeListener(IOnPageChangeListener listener)
        {
            mOnPageChangeListener = listener;
        }
        /*
        public void setOnOpenListener(OnOpenListener l) {
            mOpenListener = l;
        }

        public void setOnCloseListener(OnCloseListener l) {
            mCloseListener = l;
        }
         */
        public void setOnOpenedListener(IOnOpenedListener l)
        {
            mOpenedListener = l;
        }

        public void setOnClosedListener(IOnClosedListener l)
        {
            mClosedListener = l;
        }

        /**
         * Set a separate OnPageChangeListener for internal use by the support library.
         *
         * @param listener Listener to set
         * @return The old listener that was set, if any.
         */
        IOnPageChangeListener setInternalPageChangeListener(IOnPageChangeListener listener)
        {
            IOnPageChangeListener oldListener = mInternalPageChangeListener;
            mInternalPageChangeListener = listener;
            return oldListener;
        }

        public void addIgnoredView(View v)
        {
            if (!mIgnoredViews.Contains(v))
            {
                mIgnoredViews.Add(v);
            }
        }

        public void removeIgnoredView(View v)
        {
            mIgnoredViews.Remove(v);
        }

        public void clearIgnoredViews()
        {
            mIgnoredViews.Clear();
        }

        // We want the duration of the page snap animation to be influenced by the distance that
        // the screen has to travel, however, we don't want this duration to be effected in a
        // purely linear fashion. Instead, we use this method to moderate the effect that the distance
        // of travel has on the overall snap duration.
        float distanceInfluenceForSnapDuration(float f)
        {
            f -= 0.5f; // center the values about 0.
            f *= Convert.ToSingle(0.3f * Math.PI / 2.0f);
            return (float)FloatMath.Sin(f);
        }

        public int getDestScrollX(int page)
        {
            switch (page)
            {
                case 0:
                case 2:
                    return mViewBehind.getMenuLeft(mContent, page);
                case 1:
                    return mContent.Left;
            }
            return 0;
        }

        private int getLeftBound()
        {
            return mViewBehind.getAbsLeftBound(mContent);
        }

        private int getRightBound()
        {
            return mViewBehind.getAbsRightBound(mContent);
        }

        public int getContentLeft()
        {
            return mContent.Left + mContent.PaddingLeft;
        }

        public bool isMenuOpen()
        {
            return mCurItem == 0 || mCurItem == 2;
        }

        private bool isInIgnoredView(MotionEvent ev)
        {
            Rect rect = new Rect();
            foreach (View v in mIgnoredViews)
            {
                v.GetHitRect(rect);
                if (rect.Contains((int)ev.GetX(), (int)ev.GetY())) return true;
            }
            return false;
        }

        public int getBehindWidth()
        {
            if (mViewBehind == null)
            {
                return 0;
            }
            else
            {
                return mViewBehind.getBehindWidth();
            }
        }

        public int getChildWidth(int i)
        {
            switch (i)
            {
                case 0:
                    return getBehindWidth();
                case 1:
                    return mContent.Width;
                default:
                    return 0;
            }
        }

        public bool isSlidingEnabled()
        {
            return mEnabled;
        }

        public void setSlidingEnabled(bool b)
        {
            mEnabled = b;
        }

        /**
         * Like {@link View#scrollBy}, but scroll smoothly instead of immediately.
         *
         * @param x the number of pixels to scroll by on the X axis
         * @param y the number of pixels to scroll by on the Y axis
         */
        void smoothScrollTo(int x, int y)
        {
            smoothScrollTo(x, y, 0);
        }

        /**
         * Like {@link View#scrollBy}, but scroll smoothly instead of immediately.
         *
         * @param x the number of pixels to scroll by on the X axis
         * @param y the number of pixels to scroll by on the Y axis
         * @param velocity the velocity associated with a fling, if applicable. (0 otherwise)
         */
        void smoothScrollTo(int x, int y, int velocity)
        {
            if (ChildCount == 0)
            {
                // Nothing to do.
                setScrollingCacheEnabled(false);
                return;
            }
            int sx = ScrollX;
            int sy = ScrollY;
            int dx = x - sx;
            int dy = y - sy;
            if (dx == 0 && dy == 0)
            {
                completeScroll();
                if (isMenuOpen())
                {
                    if (mOpenedListener != null)
                        mOpenedListener.onOpened();
                }
                else
                {
                    if (mClosedListener != null)
                        mClosedListener.onClosed();
                }
                return;
            }

            setScrollingCacheEnabled(true);
            mScrolling = true;

            int width = getBehindWidth();
            int halfWidth = width / 2;
            float distanceRatio = Math.Min(1f, 1.0f * Math.Abs(dx) / width);
            float distance = halfWidth + halfWidth *
                   distanceInfluenceForSnapDuration(distanceRatio);

            int duration = 0;
            velocity = Math.Abs(velocity);
            if (velocity > 0)
            {
                duration = Convert.ToInt32(4 * Math.Round(1000 * Math.Abs(distance / velocity)));
            }
            else
            {
                float pageDelta = (float)Math.Abs(dx) / width;
                duration = (int)((pageDelta + 1) * 100);
                duration = MAX_SETTLE_DURATION;
            }
            duration = Math.Min(duration, MAX_SETTLE_DURATION);

            mScroller.StartScroll(sx, sy, dx, dy, duration);
            Invalidate();
        }

        public void setContent(View v)
        {
            if (mContent != null)
                this.RemoveView(mContent);
            mContent = v;
            AddView(mContent);
        }

        public View getContent()
        {
            return mContent;
        }

        public void setCustomViewBehind(CustomViewBehind cvb)
        {
            mViewBehind = cvb;
            setInternalPageChangeListener(new InternalPageChangeListener(mViewBehind));
        }

        //@Override
        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {

            int width = GetDefaultSize(0, widthMeasureSpec);
            int height = GetDefaultSize(0, heightMeasureSpec);
            SetMeasuredDimension(width, height);

            int contentWidth = GetChildMeasureSpec(widthMeasureSpec, 0, width);
            int contentHeight = GetChildMeasureSpec(heightMeasureSpec, 0, height);
            mContent.Measure(contentWidth, contentHeight);
        }

        //@Override
        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            // Make sure scroll position is set correctly.
            if (w != oldw)
            {
                // [ChrisJ] - This fixes the onConfiguration change for orientation issue..
                // maybe worth having a look why the recomputeScroll pos is screwing
                // up?
                completeScroll();
                ScrollTo(getDestScrollX(mCurItem), ScrollY);
            }
        }

        //@Override
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            int width = r - l;
            int height = b - t;
            mContent.Layout(0, 0, width, height);
        }

        public void setAboveOffset(int i)
        {
            //		RelativeLayout.LayoutParams params = ((RelativeLayout.LayoutParams)mContent.getLayoutParams());
            //		params.setMargins(i, params.topMargin, params.rightMargin, params.bottomMargin);
            mContent.SetPadding(i, mContent.PaddingTop,
                    mContent.PaddingRight, mContent.PaddingBottom);
        }


        //@Override
        public override void ComputeScroll()
        {
            
            if (!mScroller.IsFinished)
            {
                if (mScroller.ComputeScrollOffset())
                {
                    int oldX = ScrollX;
                    int oldY = ScrollY;
                    int x = mScroller.CurrX;
                    int y = mScroller.CurrY;

                    if (oldX != x || oldY != y)
                    {
                        ScrollTo(x, y);
                        pageScrolled(x);
                    }

                    // Keep on drawing until the animation has finished.
                    Invalidate();
                    return;
                }
            }

            // Done with scroll, clean up state.
            completeScroll();
        }

        private void pageScrolled(int xpos)
        {
            int widthWithMargin = Width;
            int position = xpos / widthWithMargin;
            int offsetPixels = xpos % widthWithMargin;
            float offset = (float)offsetPixels / widthWithMargin;

            onPageScrolled(position, offset, offsetPixels);
        }

        /**
         * This method will be invoked when the current page is scrolled, either as part
         * of a programmatically initiated smooth scroll or a user initiated touch scroll.
         * If you override this method you must call through to the superclass implementation
         * (e.g. super.onPageScrolled(position, offset, offsetPixels)) before onPageScrolled
         * returns.
         *
         * @param position Position index of the first page currently being displayed.
         *                 Page position+1 will be visible if positionOffset is nonzero.
         * @param offset Value from [0, 1) indicating the offset from the page at position.
         * @param offsetPixels Value in pixels indicating the offset from position.
         */
        protected void onPageScrolled(int position, float offset, int offsetPixels)
        {
            if (mOnPageChangeListener != null)
            {
                mOnPageChangeListener.OnPageScrolled(position, offset, offsetPixels);
            }
            if (mInternalPageChangeListener != null)
            {
                mInternalPageChangeListener.OnPageScrolled(position, offset, offsetPixels);
            }
        }

        private void completeScroll()
        {
            bool needPopulate = mScrolling;
            if (needPopulate)
            {
                // Done with scroll, no longer want to cache view drawing.
                setScrollingCacheEnabled(false);
                mScroller.AbortAnimation();
                int oldX = ScrollX;
                int oldY = ScrollY;
                int x = mScroller.CurrX;
                int y = mScroller.CurrY;
                if (oldX != x || oldY != y)
                {
                    ScrollTo(x, y);
                }
                if (isMenuOpen())
                {
                    if (mOpenedListener != null)
                        mOpenedListener.onOpened();
                }
                else
                {
                    if (mClosedListener != null)
                        mClosedListener.onClosed();
                }
            }
            mScrolling = false;
        }

        protected int mTouchMode = SlidingMenu.TOUCHMODE_MARGIN;

        public void setTouchMode(int i)
        {
            mTouchMode = i;
        }

        public int getTouchMode()
        {
            return mTouchMode;
        }

        private bool thisTouchAllowed(MotionEvent ev)
        {
            int x = (int)(ev.GetX() + mScrollX);
            if (isMenuOpen())
            {
                return mViewBehind.menuOpenTouchAllowed(mContent, mCurItem, x);
            }
            else
            {
                switch (mTouchMode)
                {
                    case SlidingMenu.TOUCHMODE_FULLSCREEN:
                        return !isInIgnoredView(ev);
                    case SlidingMenu.TOUCHMODE_NONE:
                        return false;
                    case SlidingMenu.TOUCHMODE_MARGIN:
                        return mViewBehind.marginTouchAllowed(mContent, x);
                }
            }
            return false;
        }

        private bool thisSlideAllowed(float dx)
        {
            bool allowed = false;
            if (isMenuOpen())
            {
                allowed = mViewBehind.menuOpenSlideAllowed(dx);
            }
            else
            {
                allowed = mViewBehind.menuClosedSlideAllowed(dx);
            }
            if (DEBUG)
                Log.Verbose(TAG, "this slide allowed " + allowed + " dx: " + dx);
            return allowed;
        }

        private int getPointerIndex(MotionEvent ev, int id)
        {
            int activePointerIndex = MotionEventCompat.FindPointerIndex(ev, id);
            if (activePointerIndex == -1)
                mActivePointerId = INVALID_POINTER;
            return activePointerIndex;
        }

        private bool mQuickReturn = false;

        //@Override
        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {     
        
            if (!mEnabled)
                return false;

            MotionEventActions action =ev.Action & (MotionEventActions)MotionEventCompat.ActionMask;

            if (DEBUG)
                if (action == MotionEventActions.Down)
                    Log.Verbose(TAG, "Received ACTION_DOWN");

            if (action == MotionEventActions.Cancel || action == MotionEventActions.Up
                    || (action != MotionEventActions.Down && mIsUnableToDrag))
            {
                endDrag();
                return false;
            }

            switch (action)
            {
                case MotionEventActions.Move:
                    determineDrag(ev);
                    break;
                case MotionEventActions.Down:
                    int index = MotionEventCompat.GetActionIndex(ev);
                    mActivePointerId = MotionEventCompat.GetPointerId(ev, index);
                    if (mActivePointerId == INVALID_POINTER)
                        break;
                    
                    mLastMotionX = MotionEventCompat.GetX(ev, index);
                    mInitialMotionX = mLastMotionX;
                    mLastMotionY = MotionEventCompat.GetY(ev, index);
                    if (thisTouchAllowed(ev))
                    {
                        mIsBeingDragged = false;
                        mIsUnableToDrag = false;
                        if (isMenuOpen() && mViewBehind.menuTouchInQuickReturn(mContent, mCurItem, ev.GetX() + mScrollX))
                        {
                            mQuickReturn = true;
                        }
                    }
                    else
                    {
                        mIsUnableToDrag = true;
                    }
                    break;
                case (MotionEventActions)MotionEventCompat.ActionPointerUp:
                    onSecondaryPointerUp(ev);
                    break;
            }

            if (!mIsBeingDragged)
            {
                if (mVelocityTracker == null)
                {
                    mVelocityTracker = VelocityTracker.Obtain();
                }
                mVelocityTracker.AddMovement(ev);
            }             
            return mIsBeingDragged || mQuickReturn;
        }


        //@Override
        public override bool OnTouchEvent(MotionEvent ev)
        {

            if (!mEnabled)
                return false;

            if (!mIsBeingDragged && !thisTouchAllowed(ev))
                return false;

            //		if (!mIsBeingDragged && !mQuickReturn)
            //			return false;

            MotionEventActions action = ev.Action;

            if (mVelocityTracker == null)
            {
                mVelocityTracker = VelocityTracker.Obtain();
            }
            mVelocityTracker.AddMovement(ev);

            switch (action & (MotionEventActions)MotionEventCompat.ActionMask)
            {
                case MotionEventActions.Down:
                    /*
                     * If being flinged and user touches, stop the fling. isFinished
                     * will be false if being flinged.
                     */
                    completeScroll();

                    // Remember where the motion event started
                    int index = MotionEventCompat.GetActionIndex(ev);
                    mActivePointerId = MotionEventCompat.GetPointerId(ev, index);
                    mLastMotionX = mInitialMotionX = ev.GetX();
                    break;
                case MotionEventActions.Move:
                    if (!mIsBeingDragged)
                    {
                        determineDrag(ev);
                        if (mIsUnableToDrag)
                            return false;
                    }
                    if (mIsBeingDragged)
                    {
                        // Scroll to follow the motion event
                        int activePointerIndex = getPointerIndex(ev, mActivePointerId);
                        if (mActivePointerId == INVALID_POINTER)
                            break;
                        float x = MotionEventCompat.GetX(ev, activePointerIndex);
                        float deltaX = mLastMotionX - x;
                        mLastMotionX = x;
                        float oldScrollX = ScrollX;
                        float scrollX = oldScrollX + deltaX;
                        float leftBound = getLeftBound();
                        float rightBound = getRightBound();
                        if (scrollX < leftBound)
                        {
                            scrollX = leftBound;
                        }
                        else if (scrollX > rightBound)
                        {
                            scrollX = rightBound;
                        }
                        // Don't lose the rounded component
                        mLastMotionX += scrollX - (int)scrollX;
                        ScrollTo((int)scrollX, ScrollY);
                        pageScrolled((int)scrollX);
                    }
                    break;
                case MotionEventActions.Up:
                    if (mIsBeingDragged)
                    {
                        VelocityTracker velocityTracker = mVelocityTracker;
                        velocityTracker.ComputeCurrentVelocity(1000, mMaximumVelocity);
                        int initialVelocity = (int)VelocityTrackerCompat.GetXVelocity(
                                velocityTracker, mActivePointerId);
                        int scrollX = ScrollX;
                        float pageOffset = (float)(scrollX - getDestScrollX(mCurItem)) / getBehindWidth();
                        int activePointerIndex = getPointerIndex(ev, mActivePointerId);
                        if (mActivePointerId != INVALID_POINTER)
                        {
                            float x = MotionEventCompat.GetX(ev, activePointerIndex);
                            int totalDelta = (int)(x - mInitialMotionX);
                            int nextPage = determineTargetPage(pageOffset, initialVelocity, totalDelta);
                            setCurrentItemInternal(nextPage, true, true, initialVelocity);
                        }
                        else
                        {
                            setCurrentItemInternal(mCurItem, true, true, initialVelocity);
                        }
                        mActivePointerId = INVALID_POINTER;
                        endDrag();
                    }
                    else if (mQuickReturn && mViewBehind.menuTouchInQuickReturn(mContent, mCurItem, ev.GetX() + mScrollX))
                    {
                        // close the menu
                        setCurrentItem(1);
                        endDrag();
                    }
                    break;
                case MotionEventActions.Cancel:
                    if (mIsBeingDragged)
                    {
                        setCurrentItemInternal(mCurItem, true, true);
                        mActivePointerId = INVALID_POINTER;
                        endDrag();
                    }
                    break;
                case (MotionEventActions)MotionEventCompat.ActionPointerDown:
                    {
                        int indexx = MotionEventCompat.GetActionIndex(ev);
                        mLastMotionX = MotionEventCompat.GetX(ev, indexx);
                        mActivePointerId = MotionEventCompat.GetPointerId(ev, indexx);
                        break;
                    }
                case (MotionEventActions)MotionEventCompat.ActionPointerUp:
                    onSecondaryPointerUp(ev);
                    int pointerIndex = getPointerIndex(ev, mActivePointerId);
                    if (mActivePointerId == INVALID_POINTER)
                        break;
                    mLastMotionX = MotionEventCompat.GetX(ev, pointerIndex);
                    break;
            }
            return true;
        }

        private void determineDrag(MotionEvent ev)
        {
            int activePointerId = mActivePointerId;
            int pointerIndex = getPointerIndex(ev, activePointerId);
            if (activePointerId == INVALID_POINTER || pointerIndex == INVALID_POINTER)
                return;
            float x = MotionEventCompat.GetX(ev, pointerIndex);
            float dx = x - mLastMotionX;
            float xDiff = Math.Abs(dx);
            float y = MotionEventCompat.GetY(ev, pointerIndex);
            float dy = y - mLastMotionY;
            float yDiff = Math.Abs(dy);
            if (xDiff > (isMenuOpen() ? mTouchSlop / 2 : mTouchSlop) && xDiff > yDiff && thisSlideAllowed(dx))
            {
                startDrag();
                mLastMotionX = x;
                mLastMotionY = y;
                setScrollingCacheEnabled(true);
                // TODO add back in touch slop check
            }
            else if (xDiff > mTouchSlop)
            {
                mIsUnableToDrag = true;
            }
        }

        //@Override
        public override void ScrollTo(int x, int y)
        {
            base.ScrollTo(x, y);
            mScrollX = x;
            mViewBehind.scrollBehindTo(mContent, x, y);
            ((SlidingMenu)Parent).manageLayers(getPercentOpen());
        }

        private int determineTargetPage(float pageOffset, int velocity, int deltaX)
        {
            int targetPage = mCurItem;
            if (Math.Abs(deltaX) > mFlingDistance && Math.Abs(velocity) > mMinimumVelocity)
            {
                if (velocity > 0 && deltaX > 0)
                {
                    targetPage -= 1;
                }
                else if (velocity < 0 && deltaX < 0)
                {
                    targetPage += 1;
                }
            }
            else
            {
                targetPage = (int)Math.Round(mCurItem + pageOffset);
            }
            return targetPage;
        }

        public float getPercentOpen()
        {
            return Math.Abs(mScrollX - mContent.Left) / getBehindWidth();
        }

        //@Override
        protected override void DispatchDraw(Canvas canvas)
        {
            base.DispatchDraw(canvas);
            // Draw the margin drawable if needed.
            mViewBehind.drawShadow(mContent, canvas);
            mViewBehind.drawFade(mContent, canvas, getPercentOpen());
            mViewBehind.drawSelector(mContent, canvas, getPercentOpen());
        }

        // variables for drawing
        private float mScrollX = 0.0f;

        private void onSecondaryPointerUp(MotionEvent ev)
        {
            if (DEBUG) Log.Verbose(TAG, "onSecondaryPointerUp called");
            int pointerIndex = MotionEventCompat.GetActionIndex(ev);
            int pointerId = MotionEventCompat.GetPointerId(ev, pointerIndex);
            if (pointerId == mActivePointerId)
            {
                // This was our active pointer going up. Choose a new
                // active pointer and adjust accordingly.
                int newPointerIndex = pointerIndex == 0 ? 1 : 0;
                mLastMotionX = MotionEventCompat.GetX(ev, newPointerIndex);
                mActivePointerId = MotionEventCompat.GetPointerId(ev, newPointerIndex);
                if (mVelocityTracker != null)
                {
                    mVelocityTracker.Clear();
                }
            }
        }

        private void startDrag()
        {
            mIsBeingDragged = true;
            mQuickReturn = false;
        }

        private void endDrag()
        {
            mQuickReturn = false;
            mIsBeingDragged = false;
            mIsUnableToDrag = false;
            mActivePointerId = INVALID_POINTER;

            if (mVelocityTracker != null)
            {
                mVelocityTracker.Recycle();
                mVelocityTracker = null;
            }
        }

        private void setScrollingCacheEnabled(bool enabled)
        {
            if (mScrollingCacheEnabled != enabled)
            {
                mScrollingCacheEnabled = enabled;
                if (USE_CACHE)
                {
                    int size = ChildCount;
                    for (int i = 0; i < size; ++i)
                    {
                        View child = GetChildAt(i);
                        if (child.Visibility != ViewStates.Gone)
                        {
                            child.DrawingCacheEnabled = enabled;
                        }
                    }
                }
            }
        }

        /**
         * Tests scrollability within child views of v given a delta of dx.
         *
         * @param v View to test for horizontal scrollability
         * @param checkV Whether the view v passed should itself be checked for scrollability (true),
         *               or just its children (false).
         * @param dx Delta scrolled in pixels
         * @param x X coordinate of the active touch point
         * @param y Y coordinate of the active touch point
         * @return true if child views of v can be scrolled by delta of dx.
         */
        protected bool canScroll(View v, bool checkV, int dx, int x, int y)
        {
            if (v is ViewGroup)
            {
                ViewGroup group = (ViewGroup)v;
                int scrollX = v.ScrollX;
                int scrollY = v.ScrollY;
                int count = group.ChildCount;
                // Count backwards - let topmost views consume scroll distance first.
                for (int i = count - 1; i >= 0; i--)
                {
                    View child = group.GetChildAt(i);
                    if (x + scrollX >= child.Left && x + scrollX < child.Right &&
                            y + scrollY >= child.Top && y + scrollY < child.Bottom &&
                            canScroll(child, true, dx, x + scrollX - child.Left,
                                    y + scrollY - child.Top))
                    {
                        return true;
                    }
                }
            }

            return checkV && ViewCompat.CanScrollHorizontally(v, -dx);
        }


        //@Override
        public override bool DispatchKeyEvent(KeyEvent ev)
        {
            // Let the focused view and/or our descendants get the key first
            return base.DispatchKeyEvent(ev) || executeKeyEvent(ev);
        }

        /**
         * You can call this function yourself to have the scroll view perform
         * scrolling from a key event, just as if the event had been dispatched to
         * it by the view hierarchy.
         *
         * @param event The key event to execute.
         * @return Return true if the event was handled, else false.
         */
        public bool executeKeyEvent(KeyEvent ev)
        {
            bool handled = false;
            
            if (ev.Action == KeyEventActions.Down)
            {
                
                switch (ev.KeyCode)
                {
                    case Keycode.DpadLeft:
                        handled = arrowScroll(FocusSearchDirection.Left);
                        break;
                    case Keycode.DpadRight:
                        handled = arrowScroll(FocusSearchDirection.Right);
                        break;
                    case Keycode.Tab:
                        if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb)
                        {
                            // The focus finder had a bug handling FOCUS_FORWARD and FOCUS_BACKWARD
                            // before Android 3.0. Ignore the tab key on those devices.
                            if (KeyEventCompat.HasNoModifiers(ev))
                            {
                                handled = arrowScroll(FocusSearchDirection.Forward);
                            }
                            else if (KeyEventCompat.HasModifiers(ev, (int)MetaKeyStates.ShiftOn))
                            { 
                                handled = arrowScroll(FocusSearchDirection.Backward);
                            }
                        }
                        break;
                }
            }
            return handled;
        }

        public bool arrowScroll(FocusSearchDirection direction)
        {
            View currentFocused = FindFocus();
            if (currentFocused == this) currentFocused = null;

            bool handled = false;

            View nextFocused = FocusFinder.Instance.FindNextFocus(this, currentFocused,
                    direction);
            if (nextFocused != null && nextFocused != currentFocused)
            {

                if (direction == FocusSearchDirection.Left)
                {
                    handled = nextFocused.RequestFocus();
                }
                else if (direction == FocusSearchDirection.Right)
                {
                    // If there is nothing to the right, or this is causing us to
                    // jump to the left, then what we really want to do is page right.
                    if (currentFocused != null && nextFocused.Left <= currentFocused.Left)
                    {
                        handled = pageRight();
                    }
                    else
                    {
                        handled = nextFocused.RequestFocus();
                    }
                }
            }
            else if (direction == FocusSearchDirection.Left || direction == FocusSearchDirection.Backward)
            {
                // Trying to move left and nothing there; try to page.
                handled = pageLeft();
            }
            else if (direction == FocusSearchDirection.Right || direction == FocusSearchDirection.Forward)
            {
                // Trying to move right and nothing there; try to page.
                handled = pageRight();
            }
            if (handled)
            {
                PlaySoundEffect(SoundEffectConstants.GetContantForFocusDirection(direction));
            }
            return handled;
        }

        bool pageLeft()
        {
            if (mCurItem > 0)
            {
                setCurrentItem(mCurItem - 1, true);
                return true;
            }
            return false;
        }

        bool pageRight()
        {
            if (mCurItem < 1)
            {
                setCurrentItem(mCurItem + 1, true);
                return true;
            }
            return false;
        }

    }
}