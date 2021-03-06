//package com.jeremyfeinstein.slidingmenu.lib;

//import java.lang.reflect.Method;

//import android.annotation.SuppressLint;
//import android.annotation.TargetApi;
//import android.app.Activity;
//import android.content.Context;
//import android.content.res.TypedArray;
//import android.graphics.Bitmap;
//import android.graphics.BitmapFactory;
//import android.graphics.Canvas;
//import android.graphics.Point;
//import android.graphics.Rect;
//import android.graphics.drawable.Drawable;
//import android.os.Build;
//import android.os.Handler;
//import android.os.Parcel;
//import android.os.Parcelable;
//import android.util.AttributeSet;
//import android.util.Log;
//import android.view.Display;
//import android.view.LayoutInflater;
//import android.view.View;
//import android.view.ViewGroup;
//import android.view.WindowManager;
//import android.widget.FrameLayout;
//import android.widget.RelativeLayout;

using Android.Annotation;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Util;
using Android.Views;
//import com.jeremyfeinstein.slidingmenu.lib.CustomViewAbove.OnPageChangeListener;
using Android.Widget;
using Java.Lang;

namespace Com.Jeremyfeinstein.SlidingMenu.Lib
{
    /**
         * The listener interface for receiving onOpen events.
         * The class that is interested in processing a onOpen
         * event implements this interface, and the object created
         * with that class is registered with a component using the
         * component's <code>addOnOpenListener<code> method. When
         * the onOpen event occurs, that object's appropriate
         * method is invoked
         */
    public interface IOnOpenListener
    {

        /**
         * On open.
         */
        void onOpen();
    }

    /**
     * The listener interface for receiving onOpened events.
     * The class that is interested in processing a onOpened
     * event implements this interface, and the object created
     * with that class is registered with a component using the
     * component's <code>addOnOpenedListener<code> method. When
     * the onOpened event occurs, that object's appropriate
     * method is invoked.
     *
     * @see OnOpenedEvent
     */
    public interface IOnOpenedListener
    {

        /**
         * On opened.
         */
        void onOpened();
    }

    /**
     * The listener interface for receiving onClose events.
     * The class that is interested in processing a onClose
     * event implements this interface, and the object created
     * with that class is registered with a component using the
     * component's <code>addOnCloseListener<code> method. When
     * the onClose event occurs, that object's appropriate
     * method is invoked.
     *
     * @see OnCloseEvent
     */
    public interface IOnCloseListener
    {

        /**
         * On close.
         */
        void onClose();
    }

    /**
     * The listener interface for receiving onClosed events.
     * The class that is interested in processing a onClosed
     * event implements this interface, and the object created
     * with that class is registered with a component using the
     * component's <code>addOnClosedListener<code> method. When
     * the onClosed event occurs, that object's appropriate
     * method is invoked.
     *
     * @see OnClosedEvent
     */
    public interface IOnClosedListener
    {

        /**
         * On closed.
         */
        void onClosed();
    }

    /**
     * The Interface CanvasTransformer.
     */
    public interface ICanvasTransformer
    {

        /**
         * Transform canvas.
         *
         * @param canvas the canvas
         * @param percentOpen the percent open
         */
        void transformCanvas(Canvas canvas, float percentOpen);
    }

    public enum SlidingMenuMode
    {
        LEFT=0,
        RIGHT=1,
        LEFT_RIGHT = 2
    }

    public class SlidingMenu : RelativeLayout
    {

        private static readonly string TAG = typeof(SlidingMenu).Name;//SlidingMenu.class.getSimpleName();

        public const int SLIDING_WINDOW = 0;
        public const int SLIDING_CONTENT = 1;
        private bool mActionbarOverlay = false;

        /** Constant value for use with setTouchModeAbove(). Allows the SlidingMenu to be opened with a swipe
         * gesture on the screen's margin
         */
        public const int TOUCHMODE_MARGIN = 0;

        /** Constant value for use with setTouchModeAbove(). Allows the SlidingMenu to be opened with a swipe
         * gesture anywhere on the screen
         */
        public const int TOUCHMODE_FULLSCREEN = 1;

        /** Constant value for use with setTouchModeAbove(). Denies the SlidingMenu to be opened with a swipe
         * gesture
         */
        public const int TOUCHMODE_NONE = 2;

        ///** Constant value for use with setMode(). Puts the menu to the left of the content.
        // */
        //public static readonly int LEFT = 0;

        ///** Constant value for use with setMode(). Puts the menu to the right of the content.
        // */
        //public static readonly int RIGHT = 1;

        ///** Constant value for use with setMode(). Puts menus to the left and right of the content.
        // */
        //public static readonly int LEFT_RIGHT = 2;

        private CustomViewAbove mViewAbove;

        private CustomViewBehind mViewBehind;

        private IOnOpenListener mOpenListener;

        private IOnOpenListener mSecondaryOpenListner;

        private IOnCloseListener mCloseListener;



        /**
         * Instantiates a new SlidingMenu.
         *
         * @param context the associated Context
         */
        public SlidingMenu(Context context)
            : this(context, null)
        {

        }

        /**
         * Instantiates a new SlidingMenu and attach to Activity.
         *
         * @param activity the activity to attach slidingmenu
         * @param slideStyle the slidingmenu style
         */
        public SlidingMenu(Activity activity, int slideStyle)
            : this(activity, null)
        {

            this.attachToActivity(activity, slideStyle);
        }

        /**
         * Instantiates a new SlidingMenu.
         *
         * @param context the associated Context
         * @param attrs the attrs
         */
        public SlidingMenu(Context context, IAttributeSet attrs)
            : this(context, attrs, 0)
        {

        }

        class PageChangeClass : Java.Lang.Object, IOnPageChangeListener
        {
            public const int POSITION_OPEN = 0;
            public const int POSITION_CLOSE = 1;
            public const int POSITION_SECONDARY_OPEN = 2;
            private IOnOpenListener mOpenListener;
            private IOnCloseListener mCloseListener;
            private IOnOpenListener mSecondaryOpenListner;

            public PageChangeClass(IOnOpenListener mOpenListener, IOnCloseListener mCloseListener, IOnOpenListener mSecondaryOpenListner)
            {
                // TODO: Complete member initialization
                this.mOpenListener = mOpenListener;
                this.mCloseListener = mCloseListener;
                this.mSecondaryOpenListner = mSecondaryOpenListner;
            }
            public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
            {
                 
            }

            public void OnPageSelected(int position)
            {
                if (position == POSITION_OPEN && mOpenListener != null)
                {
                    mOpenListener.onOpen();
                }
                else if (position == POSITION_CLOSE && mCloseListener != null)
                {
                    mCloseListener.onClose();
                }
                else if (position == POSITION_SECONDARY_OPEN && mSecondaryOpenListner != null)
                {
                    mSecondaryOpenListner.onOpen();
                }
            }
        }

        /**
         * Instantiates a new SlidingMenu.
         *
         * @param context the associated Context
         * @param attrs the attrs
         * @param defStyle the def style
         */
        public SlidingMenu(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {


            LayoutParams behindParams = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
            mViewBehind = new CustomViewBehind(context);
            AddView(mViewBehind, behindParams);
            LayoutParams aboveParams = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
            mViewAbove = new CustomViewAbove(context);
            AddView(mViewAbove, aboveParams);
            // register the CustomViewBehind with the CustomViewAbove
            mViewAbove.setCustomViewBehind(mViewBehind);
            mViewBehind.setCustomViewAbove(mViewAbove);

            mViewAbove.setOnPageChangeListener(new PageChangeClass(mOpenListener, mCloseListener, mSecondaryOpenListner));
            //mViewAbove.setOnPageChangeListener(new OnPageChangeListener() {
            //    public static readonly int POSITION_OPEN = 0;
            //    public static readonly int POSITION_CLOSE = 1;
            //    public static readonly int POSITION_SECONDARY_OPEN = 2;

            //    public void onPageScrolled(int position, float positionOffset,
            //            int positionOffsetPixels) { }

            //    public void onPageSelected(int position) {
            //        if (position == POSITION_OPEN && mOpenListener != null) {
            //            mOpenListener.onOpen();
            //        } else if (position == POSITION_CLOSE && mCloseListener != null) {
            //            mCloseListener.onClose();
            //        } else if (position == POSITION_SECONDARY_OPEN && mSecondaryOpenListner != null ) {
            //            mSecondaryOpenListner.onOpen();
            //        }
            //    }
            //});

            // now style everything!
            TypedArray ta = context.ObtainStyledAttributes(attrs, Resource.Styleable.SlidingMenu);
            // set the above and behind views if defined in xml
            SlidingMenuMode mode = (SlidingMenuMode)ta.GetInt(Resource.Styleable.SlidingMenu_mode, (int)SlidingMenuMode.LEFT);
            this.Mode=mode;
            int viewAbove = ta.GetResourceId(Resource.Styleable.SlidingMenu_viewAbove, -1);
            if (viewAbove != -1)
            {
                setContent(viewAbove);
            }
            else
            {
                setContent(new FrameLayout(context));
            }
            int viewBehind = ta.GetResourceId(Resource.Styleable.SlidingMenu_viewBehind, -1);
            if (viewBehind != -1)
            {
                setMenu(viewBehind);
            }
            else
            {
                setMenu(new FrameLayout(context));
            }
            int touchModeAbove = ta.GetInt(Resource.Styleable.SlidingMenu_touchModeAbove, TOUCHMODE_MARGIN);
            setTouchModeAbove(touchModeAbove);
            int touchModeBehind = ta.GetInt(Resource.Styleable.SlidingMenu_touchModeBehind, TOUCHMODE_MARGIN);
            setTouchModeBehind(touchModeBehind);

            int offsetBehind = (int)ta.GetDimension(Resource.Styleable.SlidingMenu_behindOffset, -1);
            int widthBehind = (int)ta.GetDimension(Resource.Styleable.SlidingMenu_behindWidth, -1);
            if (offsetBehind != -1 && widthBehind != -1)
                throw new Java.Lang.IllegalStateException("Cannot set both behindOffset and behindWidth for a SlidingMenu");
            else if (offsetBehind != -1)
                setBehindOffset(offsetBehind);
            else if (widthBehind != -1)
                setBehindWidth(widthBehind);
            else
                setBehindOffset(0);
            float scrollOffsetBehind = ta.GetFloat(Resource.Styleable.SlidingMenu_behindScrollScale, 0.33f);
            setBehindScrollScale(scrollOffsetBehind);
            int shadowRes = ta.GetResourceId(Resource.Styleable.SlidingMenu_shadowDrawable, -1);
            if (shadowRes != -1)
            {
                setShadowDrawable(shadowRes);
            }
            int shadowWidth = (int)ta.GetDimension(Resource.Styleable.SlidingMenu_shadowWidth, 0);
            setShadowWidth(shadowWidth);
            bool fadeEnabled = ta.GetBoolean(Resource.Styleable.SlidingMenu_fadeEnabled, true);
            setFadeEnabled(fadeEnabled);
            float fadeDeg = ta.GetFloat(Resource.Styleable.SlidingMenu_fadeDegree, 0.33f);
            setFadeDegree(fadeDeg);
            bool selectorEnabled = ta.GetBoolean(Resource.Styleable.SlidingMenu_selectorEnabled, false);
            setSelectorEnabled(selectorEnabled);
            int selectorRes = ta.GetResourceId(Resource.Styleable.SlidingMenu_selectorDrawable, -1);
            if (selectorRes != -1)
                setSelectorDrawable(selectorRes);
            ta.Recycle();
        }

        /**
         * Attaches the SlidingMenu to an entire Activity
         * 
         * @param activity the Activity
         * @param slideStyle either SLIDING_CONTENT or SLIDING_WINDOW
         */
        public void attachToActivity(Activity activity, int slideStyle)
        {
            attachToActivity(activity, slideStyle, false);
        }

        /**
         * Attaches the SlidingMenu to an entire Activity
         * 
         * @param activity the Activity
         * @param slideStyle either SLIDING_CONTENT or SLIDING_WINDOW
         * @param actionbarOverlay whether or not the ActionBar is overlaid
         */
        public void attachToActivity(Activity activity, int slideStyle, bool actionbarOverlay)
        {
            if (slideStyle != SLIDING_WINDOW && slideStyle != SLIDING_CONTENT)
                throw new Java.Lang.IllegalArgumentException("slideStyle must be either SLIDING_WINDOW or SLIDING_CONTENT");

            if (Parent != null)
                throw new Java.Lang.IllegalStateException("This SlidingMenu appears to already be attached");

            // get the window background
            TypedArray a = activity.Theme.ObtainStyledAttributes(new int[] { Android.Resource.Attribute.WindowBackground });
            int background = a.GetResourceId(0, 0);
            a.Recycle();

            switch (slideStyle)
            {
                case SLIDING_WINDOW:
                    mActionbarOverlay = false;
                    ViewGroup decor = (ViewGroup)activity.Window.DecorView;
                    ViewGroup decorChild = (ViewGroup)decor.GetChildAt(0);
                    // save ActionBar themes that have transparent assets
                    decorChild.SetBackgroundResource(background);
                    decor.RemoveView(decorChild);
                    decor.AddView(this);
                    setContent(decorChild);
                    break;
                case SLIDING_CONTENT:
                    mActionbarOverlay = actionbarOverlay;
                    // take the above view out of
                    ViewGroup contentParent = (ViewGroup)activity.FindViewById(Android.Resource.Id.Content);
                    View content = contentParent.GetChildAt(0);
                    contentParent.RemoveView(content);
                    contentParent.AddView(this);
                    setContent(content);
                    // save people from having transparent backgrounds
                    if (content.Background == null)
                        content.SetBackgroundResource(background);
                    break;
            }
        }

        /**
         * Set the above view content from a layout resource. The resource will be inflated, adding all top-level views
         * to the above view.
         *
         * @param res the new content
         */
        public void setContent(int res)
        {
            setContent(LayoutInflater.From(Context).Inflate(res, null));
        }

        /**
         * Set the above view content to the given View.
         *
         * @param view The desired content to display.
         */
        public void setContent(View view)
        {
            mViewAbove.setContent(view);
            showContent();
        }

        /**
         * Retrieves the current content.
         * @return the current content
         */
        public View getContent()
        {
            return mViewAbove.getContent();
        }

        /**
         * Set the behind view (menu) content from a layout resource. The resource will be inflated, adding all top-level views
         * to the behind view.
         *
         * @param res the new content
         */
        public void setMenu(int res)
        {
            setMenu(LayoutInflater.From(Context).Inflate(res, null));
        }

        /**
         * Set the behind view (menu) content to the given View.
         *
         * @param view The desired content to display.
         */
        public void setMenu(View v)
        {
            mViewBehind.setContent(v);
        }

        /**
         * Retrieves the main menu.
         * @return the main menu
         */
        public View getMenu()
        {
            return mViewBehind.getContent();
        }

        /**
         * Set the secondary behind view (right menu) content from a layout resource. The resource will be inflated, adding all top-level views
         * to the behind view.
         *
         * @param res the new content
         */
        public void setSecondaryMenu(int res)
        {
            setSecondaryMenu(LayoutInflater.From(Context).Inflate(res, null));
        }

        /**
         * Set the secondary behind view (right menu) content to the given View.
         *
         * @param view The desired content to display.
         */
        public void setSecondaryMenu(View v)
        {
            mViewBehind.setSecondaryContent(v);
            //		mViewBehind.invalidate();
        }

        /**
         * Retrieves the current secondary menu (right).
         * @return the current menu
         */
        public View getSecondaryMenu()
        {
            return mViewBehind.getSecondaryContent();
        }


        /**
         * Sets the sliding enabled.
         *
         * @param b true to enable sliding, false to disable it.
         */
        public void setSlidingEnabled(bool b)
        {
            mViewAbove.setSlidingEnabled(b);
        }

        /**
         * Checks if is sliding enabled.
         *
         * @return true, if is sliding enabled
         */
        public bool isSlidingEnabled()
        {
            return mViewAbove.isSlidingEnabled();
        }

        ///**
        // * Sets which side the SlidingMenu should appear on.
        // * @param mode must be either SlidingMenu.LEFT or SlidingMenu.RIGHT
        // */
        //public void setMode(SlidingMenuMode mode)
        //{
        //    if (mode != SlidingMenuMode.LEFT && mode != SlidingMenuMode.RIGHT && mode != SlidingMenuMode.LEFT_RIGHT)
        //    {
        //        throw new Java.Lang.IllegalStateException("SlidingMenu mode must be LEFT, RIGHT, or LEFT_RIGHT");
        //    }
        //    mViewBehind.setMode(mode);
        //}

        ///**
        // * Returns the current side that the SlidingMenu is on.
        // * @return the current mode, either SlidingMenu.LEFT or SlidingMenu.RIGHT
        // */
        //public SlidingMenuMode getMode()
        //{
        //    return mViewBehind.getMode();
        //}

        public SlidingMenuMode Mode {
            get { return mViewBehind.Mode; }
            set
            {
                if (value != SlidingMenuMode.LEFT && value != SlidingMenuMode.RIGHT && value != SlidingMenuMode.LEFT_RIGHT)
                {
                    throw new Java.Lang.IllegalStateException("SlidingMenu mode must be LEFT, RIGHT, or LEFT_RIGHT");
                }
                mViewBehind.Mode=value;
            }
        }

        /**
         * Sets whether or not the SlidingMenu is in static mode (i.e. nothing is moving and everything is showing)
         *
         * @param b true to set static mode, false to disable static mode.
         */
        public void setStatic(bool b)
        {
            if (b)
            {
                setSlidingEnabled(false);
                mViewAbove.setCustomViewBehind(null);
                mViewAbove.setCurrentItem(1);
                //			mViewBehind.setCurrentItem(0);	
            }
            else
            {
                mViewAbove.setCurrentItem(1);
                //			mViewBehind.setCurrentItem(1);
                mViewAbove.setCustomViewBehind(mViewBehind);
                setSlidingEnabled(true);
            }
        }

        /**
         * Opens the menu and shows the menu view.
         */
        public void showMenu()
        {
            showMenu(true);
        }

        /**
         * Opens the menu and shows the menu view.
         *
         * @param animate true to animate the transition, false to ignore animation
         */
        public void showMenu(bool animate)
        {
            mViewAbove.setCurrentItem(0, animate);
        }

        /**
         * Opens the menu and shows the secondary menu view. Will default to the regular menu
         * if there is only one.
         */
        public void showSecondaryMenu()
        {
            showSecondaryMenu(true);
        }

        /**
         * Opens the menu and shows the secondary (right) menu view. Will default to the regular menu
         * if there is only one.
         *
         * @param animate true to animate the transition, false to ignore animation
         */
        public void showSecondaryMenu(bool animate)
        {
            mViewAbove.setCurrentItem(2, animate);
        }

        /**
         * Closes the menu and shows the above view.
         */
        public void showContent()
        {
            showContent(true);
        }

        /**
         * Closes the menu and shows the above view.
         *
         * @param animate true to animate the transition, false to ignore animation
         */
        public void showContent(bool animate)
        {
            mViewAbove.setCurrentItem(1, animate);
        }

        /**
         * Toggle the SlidingMenu. If it is open, it will be closed, and vice versa.
         */
        public void toggle()
        {
            toggle(true);
        }

        /**
         * Toggle the SlidingMenu. If it is open, it will be closed, and vice versa.
         *
         * @param animate true to animate the transition, false to ignore animation
         */
        public void toggle(bool animate)
        {
            if (isMenuShowing())
            {
                showContent(animate);
            }
            else
            {
                showMenu(animate);
            }
        }

        /**
         * Checks if is the behind view showing.
         *
         * @return Whether or not the behind view is showing
         */
        public bool isMenuShowing()
        {
            return mViewAbove.getCurrentItem() == 0 || mViewAbove.getCurrentItem() == 2;
        }

        /**
         * Checks if is the behind view showing.
         *
         * @return Whether or not the behind view is showing
         */
        public bool isSecondaryMenuShowing()
        {
            return mViewAbove.getCurrentItem() == 2;
        }

        /**
         * Gets the behind offset.
         *
         * @return The margin on the right of the screen that the behind view scrolls to
         */
        public int getBehindOffset()
        {
            return ((RelativeLayout.LayoutParams)mViewBehind.LayoutParameters).RightMargin;
        }

        /**
         * Sets the behind offset.
         *
         * @param i The margin, in pixels, on the right of the screen that the behind view scrolls to.
         */
        public void setBehindOffset(int i)
        {
            //		RelativeLayout.LayoutParams params = ((RelativeLayout.LayoutParams)mViewBehind.getLayoutParams());
            //		int bottom = params.bottomMargin;
            //		int top = params.topMargin;
            //		int left = params.leftMargin;
            //		params.setMargins(left, top, i, bottom);
            mViewBehind.setWidthOffset(i);
        }

        /**
         * Sets the behind offset.
         *
         * @param resID The dimension resource id to be set as the behind offset.
         * The menu, when open, will leave this width margin on the right of the screen.
         */
        public void setBehindOffsetRes(int resID)
        {
            int i = (int)Context.Resources.GetDimension(resID);
            setBehindOffset(i);
        }

        /**
         * Sets the above offset.
         *
         * @param i the new above offset, in pixels
         */
        public void setAboveOffset(int i)
        {
            mViewAbove.setAboveOffset(i);
        }

        /**
         * Sets the above offset.
         *
         * @param resID The dimension resource id to be set as the above offset.
         */
        public void setAboveOffsetRes(int resID)
        {
            int i = (int)Context.Resources.GetDimension(resID);
            setAboveOffset(i);
        }

        /**
         * Sets the behind width.
         *
         * @param i The width the Sliding Menu will open to, in pixels
         */
        //@SuppressWarnings("deprecation")
        [SuppressWarnings(Value = new string[] { "deprecation" })]
        public void setBehindWidth(int i)
        {
            int width = 0;
            Display display = ((IWindowManager)Context.GetSystemService(Context.WindowService))
                    .DefaultDisplay;


            try
            {
                //Class<?> cls = Display.class;
                //Class<?>[] parameterTypes = {Point.class};
                //Point parameter = new Point();
                //Method method = cls.getMethod("getSize", parameterTypes);
                //method.invoke(display, parameter);
                //width = parameter.x;
                Point parameter = new Point();
                display.GetSize(parameter);
                width = parameter.X;
            }
            catch (Exception e)
            {
                //width = display.getWidth();
            }
            setBehindOffset(width - i);
        }

        /**
         * Sets the behind width.
         *
         * @param res The dimension resource id to be set as the behind width offset.
         * The menu, when open, will open this wide.
         */
        public void setBehindWidthRes(int res)
        {
            int i = (int)Context.Resources.GetDimension(res);
            setBehindWidth(i);
        }

        /**
         * Gets the behind scroll scale.
         *
         * @return The scale of the parallax scroll
         */
        public float getBehindScrollScale()
        {
            return mViewBehind.getScrollScale();
        }

        /**
         * Gets the touch mode margin threshold
         * @return the touch mode margin threshold
         */
        public int getTouchmodeMarginThreshold()
        {
            return mViewBehind.getMarginThreshold();
        }

        /**
         * Set the touch mode margin threshold
         * @param touchmodeMarginThreshold
         */
        public void setTouchmodeMarginThreshold(int touchmodeMarginThreshold)
        {
            mViewBehind.setMarginThreshold(touchmodeMarginThreshold);
        }

        /**
         * Sets the behind scroll scale.
         *
         * @param f The scale of the parallax scroll (i.e. 1.0f scrolls 1 pixel for every
         * 1 pixel that the above view scrolls and 0.0f scrolls 0 pixels)
         */
        public void setBehindScrollScale(float f)
        {
            if (f < 0 && f > 1)
                throw new IllegalStateException("ScrollScale must be between 0 and 1");
            mViewBehind.setScrollScale(f);
        }

        /**
         * Sets the behind canvas transformer.
         *
         * @param t the new behind canvas transformer
         */
        public void setBehindCanvasTransformer(ICanvasTransformer t)
        {
            mViewBehind.setCanvasTransformer(t);
        }

        /**
         * Gets the touch mode above.
         *
         * @return the touch mode above
         */
        public int getTouchModeAbove()
        {
            return mViewAbove.getTouchMode();
        }

        /**
         * Controls whether the SlidingMenu can be opened with a swipe gesture.
         * Options are {@link #TOUCHMODE_MARGIN TOUCHMODE_MARGIN}, {@link #TOUCHMODE_FULLSCREEN TOUCHMODE_FULLSCREEN},
         * or {@link #TOUCHMODE_NONE TOUCHMODE_NONE}
         *
         * @param i the new touch mode
         */
        public void setTouchModeAbove(int i)
        {
            if (i != TOUCHMODE_FULLSCREEN && i != TOUCHMODE_MARGIN
                    && i != TOUCHMODE_NONE)
            {
                throw new IllegalStateException("TouchMode must be set to either" +
                        "TOUCHMODE_FULLSCREEN or TOUCHMODE_MARGIN or TOUCHMODE_NONE.");
            }
            mViewAbove.setTouchMode(i);
        }

        /**
         * Controls whether the SlidingMenu can be opened with a swipe gesture.
         * Options are {@link #TOUCHMODE_MARGIN TOUCHMODE_MARGIN}, {@link #TOUCHMODE_FULLSCREEN TOUCHMODE_FULLSCREEN},
         * or {@link #TOUCHMODE_NONE TOUCHMODE_NONE}
         *
         * @param i the new touch mode
         */
        public void setTouchModeBehind(int i)
        {
            if (i != TOUCHMODE_FULLSCREEN && i != TOUCHMODE_MARGIN
                    && i != TOUCHMODE_NONE)
            {
                throw new IllegalStateException("TouchMode must be set to either" +
                        "TOUCHMODE_FULLSCREEN or TOUCHMODE_MARGIN or TOUCHMODE_NONE.");
            }
            mViewBehind.setTouchMode(i);
        }

        /**
         * Sets the shadow drawable.
         *
         * @param resId the resource ID of the new shadow drawable
         */
        public void setShadowDrawable(int resId)
        {
            setShadowDrawable(Context.Resources.GetDrawable(resId));
        }

        /**
         * Sets the shadow drawable.
         *
         * @param d the new shadow drawable
         */
        public void setShadowDrawable(Drawable d)
        {
            mViewBehind.setShadowDrawable(d);
        }

        /**
         * Sets the secondary (right) shadow drawable.
         *
         * @param resId the resource ID of the new shadow drawable
         */
        public void setSecondaryShadowDrawable(int resId)
        {
            setSecondaryShadowDrawable(Context.Resources.GetDrawable(resId));
        }

        /**
         * Sets the secondary (right) shadow drawable.
         *
         * @param d the new shadow drawable
         */
        public void setSecondaryShadowDrawable(Drawable d)
        {
            mViewBehind.setSecondaryShadowDrawable(d);
        }

        /**
         * Sets the shadow width.
         *
         * @param resId The dimension resource id to be set as the shadow width.
         */
        public void setShadowWidthRes(int resId)
        {
            setShadowWidth((int)Resources.GetDimension(resId));
        }

        /**
         * Sets the shadow width.
         *
         * @param pixels the new shadow width, in pixels
         */
        public void setShadowWidth(int pixels)
        {
            mViewBehind.setShadowWidth(pixels);
        }

        /**
         * Enables or disables the SlidingMenu's fade in and out
         *
         * @param b true to enable fade, false to disable it
         */
        public void setFadeEnabled(bool b)
        {
            mViewBehind.setFadeEnabled(b);
        }

        /**
         * Sets how much the SlidingMenu fades in and out. Fade must be enabled, see
         * {@link #setFadeEnabled(bool) setFadeEnabled(bool)}
         *
         * @param f the new fade degree, between 0.0f and 1.0f
         */
        public void setFadeDegree(float f)
        {
            mViewBehind.setFadeDegree(f);
        }

        /**
         * Enables or disables whether the selector is drawn
         *
         * @param b true to draw the selector, false to not draw the selector
         */
        public void setSelectorEnabled(bool b)
        {
            mViewBehind.setSelectorEnabled(true);
        }

        /**
         * Sets the selected view. The selector will be drawn here
         *
         * @param v the new selected view
         */
        public void setSelectedView(View v)
        {
            mViewBehind.setSelectedView(v);
        }

        /**
         * Sets the selector drawable.
         *
         * @param res a resource ID for the selector drawable
         */
        public void setSelectorDrawable(int res)
        {
            mViewBehind.setSelectorBitmap(BitmapFactory.DecodeResource(this.Resources, res));
        }

        /**
         * Sets the selector drawable.
         *
         * @param b the new selector bitmap
         */
        public void setSelectorBitmap(Bitmap b)
        {
            mViewBehind.setSelectorBitmap(b);
        }

        /**
         * Add a View ignored by the Touch Down event when mode is Fullscreen
         *
         * @param v a view to be ignored
         */
        public void addIgnoredView(View v)
        {
            mViewAbove.addIgnoredView(v);
        }

        /**
         * Remove a View ignored by the Touch Down event when mode is Fullscreen
         *
         * @param v a view not wanted to be ignored anymore
         */
        public void removeIgnoredView(View v)
        {
            mViewAbove.removeIgnoredView(v);
        }

        /**
         * Clear the list of Views ignored by the Touch Down event when mode is Fullscreen
         */
        public void clearIgnoredViews()
        {
            mViewAbove.clearIgnoredViews();
        }

        /**
         * Sets the OnOpenListener. {@link OnOpenListener#onOpen() OnOpenListener.onOpen()} will be called when the SlidingMenu is opened
         *
         * @param listener the new OnOpenListener
         */
        public void setOnOpenListener(IOnOpenListener listener)
        {
            //mViewAbove.setOnOpenListener(listener);
            mOpenListener = listener;
        }


        /**
         * Sets the OnOpenListner for secondary menu  {@link OnOpenListener#onOpen() OnOpenListener.onOpen()} will be called when the secondary SlidingMenu is opened
         * 
         * @param listener the new OnOpenListener
         */

        public void setSecondaryOnOpenListner(IOnOpenListener listener)
        {
            mSecondaryOpenListner = listener;
        }

        /**
         * Sets the OnCloseListener. {@link OnCloseListener#onClose() OnCloseListener.onClose()} will be called when any one of the SlidingMenu is closed
         *
         * @param listener the new setOnCloseListener
         */
        public void setOnCloseListener(IOnCloseListener listener)
        {
            //mViewAbove.setOnCloseListener(listener);
            mCloseListener = listener;
        }

        /**
         * Sets the OnOpenedListener. {@link OnOpenedListener#onOpened() OnOpenedListener.onOpened()} will be called after the SlidingMenu is opened
         *
         * @param listener the new OnOpenedListener
         */
        public void setOnOpenedListener(IOnOpenedListener listener)
        {
            mViewAbove.setOnOpenedListener(listener);
        }

        /**
         * Sets the OnClosedListener. {@link OnClosedListener#onClosed() OnClosedListener.onClosed()} will be called after the SlidingMenu is closed
         *
         * @param listener the new OnClosedListener
         */
        public void setOnClosedListener(IOnClosedListener listener)
        {
            mViewAbove.setOnClosedListener(listener);
        }

        public class SavedState : BaseSavedState
        {

            private readonly int mItem;

            public SavedState(IParcelable superState, int item)
                : base(superState)
            {

                mItem = item;
            }

            private SavedState(Parcel pin)
                : base(pin)
            {

                mItem = pin.ReadInt();
            }

            public int getItem()
            {
                return mItem;
            }

            /* (non-Javadoc)
             * @see android.view.AbsSavedState#writeToParcel(android.os.Parcel, int)
             */
            public override void WriteToParcel(Parcel pout, ParcelableWriteFlags flags)
            {
                base.WriteToParcel(pout, flags);
                pout.WriteInt(mItem);
            }

            public static readonly IParcelableCreator CREATOR=new ParcelableCreatorImpl();

            class ParcelableCreatorImpl : Java.Lang.Object, IParcelableCreator
            {

                public Object CreateFromParcel(Parcel source)
                {
                    return new SavedState(source);
                }

                public Object[] NewArray(int size)
                {
                    return new SavedState[size];
                }
            }

            //public static readonly Parcelable.Creator<SavedState> CREATOR =
            //        new Parcelable.Creator<SavedState>() {
            //    public SavedState createFromParcel(Parcel in) {
            //        return new SavedState(in);
            //    }

            //    public SavedState[] newArray(int size) {
            //        return new SavedState[size];
            //    }
            //};

        }

        /* (non-Javadoc)
         * @see android.view.View#onSaveInstanceState()
         */
        //@Override
        protected override IParcelable OnSaveInstanceState()
        {
            IParcelable superState = base.OnSaveInstanceState();
            SavedState ss = new SavedState(superState, mViewAbove.getCurrentItem());
            return ss;
        }

        /* (non-Javadoc)
         * @see android.view.View#onRestoreInstanceState(android.os.Parcelable)
         */
        //@Override
        protected override void OnRestoreInstanceState(IParcelable state)
        {
            SavedState ss = (SavedState)state;

            base.OnRestoreInstanceState(ss.SuperState);
            mViewAbove.setCurrentItem(ss.getItem());
        }

        /* (non-Javadoc)
         * @see android.view.ViewGroup#fitSystemWindows(android.graphics.Rect)
         */
        //@SuppressLint("NewApi")
        //@Override
        protected override bool FitSystemWindows(Rect insets)
        {
            int leftPadding = insets.Left;
            int rightPadding = insets.Right;
            int topPadding = insets.Top;
            int bottomPadding = insets.Bottom;
            if (!mActionbarOverlay)
            {
                Log.Verbose(TAG, "setting padding!");
                SetPadding(leftPadding, topPadding, rightPadding, bottomPadding);
            }
            return true;
        }

        //@TargetApi(Build.VERSION_CODES.HONEYCOMB)
        [TargetApi(Value = (int)BuildVersionCodes.Honeycomb)]
        public void manageLayers(float percentOpen)
        {

            if (Build.VERSION.SdkInt < BuildVersionCodes.Honeycomb) return;

            bool layer = percentOpen > 0.0f && percentOpen < 1.0f;

            LayerType layerType = layer ? LayerType.Hardware : LayerType.None;
            //throw new Java.Lang.Exception("not impl");
            if (layerType != getContent().LayerType)
            {
                //getHandler().post(new Runnable() {
                //    public void run() {
                //        Log.v(TAG, "changing layerType. hardware? " + (layerType == View.LAYER_TYPE_HARDWARE));
                //        getContent().setLayerType(layerType, null);
                //        getMenu().setLayerType(layerType, null);
                //        if (getSecondaryMenu() != null) {
                //            getSecondaryMenu().setLayerType(layerType, null);
                //        }
                //    }
                //});
                Handler.Post(new ManageLayersRunnable(this, layerType));
            }
        }

        class ManageLayersRunnable : Java.Lang.Object, IRunnable
        {
            private SlidingMenu slidingMenu;
            private LayerType layerType;



            public ManageLayersRunnable(SlidingMenu slidingMenu, LayerType layerType)
            {
                // TODO: Complete member initialization
                this.slidingMenu = slidingMenu;
                this.layerType = layerType;
            }



            public void Run()
            {
                Log.Verbose(TAG, "changing layerType. hardware? " + (layerType == LayerType.Hardware));
                slidingMenu.getContent().SetLayerType(layerType, null);
                slidingMenu.getMenu().SetLayerType(layerType, null);
                if (slidingMenu.getSecondaryMenu() != null)
                {
                    slidingMenu.getSecondaryMenu().SetLayerType(layerType, null);
                }

            }
        }

    }
}