//package com.jeremyfeinstein.slidingmenu.example;

//import android.os.Bundle;
//import android.view.View;
//import android.view.View.OnClickListener;
//import android.widget.Button;
//import android.widget.CheckBox;
//import android.widget.CompoundButton;
//import android.widget.RadioGroup;
//import android.widget.RadioGroup.OnCheckedChangeListener;
//import android.widget.SeekBar;
//import android.widget.SeekBar.OnSeekBarChangeListener;

//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu;



using Android.App;
using Android.OS;
using Android.Widget;
using SSlidingMenu = Com.Jeremyfeinstein.SlidingMenu.Lib.SlidingMenu;
using R = Com.Jeremyfeinstein.SlidingMenu.Example.Resource;
using Com.Jeremyfeinstein.SlidingMenu.Lib;


namespace Com.Jeremyfeinstein.SlidingMenu.Example
{
    [Activity(Label = "PropertiesActivity.Net.Sample", Theme = "@style/ExampleTheme")]
    public class PropertiesActivity : BaseActivity, Android.Widget.RadioGroup.IOnCheckedChangeListener
    {

        public PropertiesActivity()
            : base(Resource.String.properties)
        {

        }        

        //@Override
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            setSlidingActionBarEnabled(true);

            SetContentView(Resource.Layout.properties);

            // left and right modes
            RadioGroup mode = (RadioGroup)FindViewById(Resource.Id.mode);
            mode.Check(Resource.Id.left);
            mode.SetOnCheckedChangeListener(this); 
            //mode.setOnCheckedChangeListener(new OnCheckedChangeListener() {
            //    @Override
            //    public void onCheckedChanged(RadioGroup group, int checkedId) {
            //        SlidingMenu sm = getSlidingMenu();
            //        switch (checkedId) {
            //        case R.id.left:
            //            sm.setMode(SlidingMenu.LEFT);
            //            sm.setShadowDrawable(R.drawable.shadow);
            //            break;
            //        case R.id.right:
            //            sm.setMode(SlidingMenu.RIGHT);
            //            sm.setShadowDrawable(R.drawable.shadowright);
            //            break;
            //        case R.id.left_right:
            //            sm.setMode(SlidingMenu.LEFT_RIGHT);
            //            sm.setSecondaryMenu(R.layout.menu_frame_two);
            //            getSupportFragmentManager()
            //            .beginTransaction()
            //            .replace(R.id.menu_frame_two, new SampleListFragment())
            //            .commit();					
            //            sm.setSecondaryShadowDrawable(R.drawable.shadowright);
            //            sm.setShadowDrawable(R.drawable.shadow);
            //        }
            //    }			
            //});

            // touch mode stuff
            RadioGroup touchAbove = (RadioGroup)FindViewById(Resource.Id.touch_above);
            touchAbove.Check(Resource.Id.touch_above_full);
            touchAbove.SetOnCheckedChangeListener(this);
            //touchAbove.setOnCheckedChangeListener(new OnCheckedChangeListener() {
            //    @Override
            //    public void onCheckedChanged(RadioGroup group, int checkedId) {
            //        switch (checkedId) {
            //        case R.id.touch_above_full:
            //            getSlidingMenu().setTouchModeAbove(SlidingMenu.TOUCHMODE_FULLSCREEN);
            //            break;
            //        case R.id.touch_above_margin:
            //            getSlidingMenu().setTouchModeAbove(SlidingMenu.TOUCHMODE_MARGIN);
            //            break;
            //        case R.id.touch_above_none:
            //            getSlidingMenu().setTouchModeAbove(SlidingMenu.TOUCHMODE_NONE);
            //            break;
            //        }
            //    }
            //});

            // scroll scale stuff
            SeekBar scrollScale = (SeekBar)FindViewById(Resource.Id.scroll_scale);
            scrollScale.Max = 1000;
            scrollScale.Progress = 333;
            scrollScale.SetOnSeekBarChangeListener(new ScaleChangeListener(this.getSlidingMenu()));
            //scrollScale.setOnSeekBarChangeListener(new OnSeekBarChangeListener() {
            //    @Override
            //    public void onProgressChanged(SeekBar seekBar, int progress,
            //            boolean fromUser) { }
            //    @Override
            //    public void onStartTrackingTouch(SeekBar seekBar) { }
            //    @Override
            //    public void onStopTrackingTouch(SeekBar seekBar) {
            //        getSlidingMenu().setBehindScrollScale((float) seekBar.getProgress()/seekBar.getMax());
            //    }
            //});


            // behind width stuff
            SeekBar behindWidth = (SeekBar)FindViewById(Resource.Id.behind_width);
            behindWidth.Max = 1000;
            behindWidth.Progress = 750;
            behindWidth.SetOnSeekBarChangeListener(new BehindChangeListener(this.getSlidingMenu()));
            //behindWidth.setOnSeekBarChangeListener(new OnSeekBarChangeListener() {
            //    @Override
            //    public void onProgressChanged(SeekBar seekBar, int progress,
            //            boolean fromUser) { }
            //    @Override
            //    public void onStartTrackingTouch(SeekBar seekBar) { }
            //    @Override
            //    public void onStopTrackingTouch(SeekBar seekBar) {
            //        float percent = (float) seekBar.getProgress()/seekBar.getMax();
            //        getSlidingMenu().setBehindWidth((int) (percent * getSlidingMenu().getWidth()));
            //        getSlidingMenu().requestLayout();
            //    }
            //});

            // shadow stuff
            CheckBox shadowEnabled = (CheckBox)FindViewById(Resource.Id.shadow_enabled);
            shadowEnabled.Checked = true;
            shadowEnabled.SetOnCheckedChangeListener(new ShadowCheckedChangeListener(this.getSlidingMenu()));
            //shadowEnabled.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            //    public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
            //        if (isChecked)
            //            getSlidingMenu().setShadowDrawable(
            //                    getSlidingMenu().getMode() == SlidingMenu.LEFT ? 
            //                            R.drawable.shadow : R.drawable.shadowright);
            //        else
            //            getSlidingMenu().setShadowDrawable(null);
            //    }
            //});
            SeekBar shadowWidth = (SeekBar)FindViewById(Resource.Id.shadow_width);
            shadowWidth.Max = 1000;
            shadowWidth.Progress = 75;
            shadowWidth.SetOnSeekBarChangeListener(new ShadowChangeListener(this.getSlidingMenu()));
            //shadowWidth.setOnSeekBarChangeListener(new OnSeekBarChangeListener() {
            //    @Override
            //    public void onProgressChanged(SeekBar arg0, int arg1, boolean arg2) { }
            //    @Override
            //    public void onStartTrackingTouch(SeekBar seekBar) { }
            //    @Override
            //    public void onStopTrackingTouch(SeekBar seekBar) {
            //        float percent = (float) seekBar.getProgress()/ (float) seekBar.getMax();
            //        int width = (int) (percent * (float) getSlidingMenu().getWidth());
            //        getSlidingMenu().setShadowWidth(width);
            //        getSlidingMenu().invalidate();
            //    }
            //});

            // fading stuff
            CheckBox fadeEnabled = (CheckBox)FindViewById(Resource.Id.fade_enabled);
            fadeEnabled.Checked = true;
            fadeEnabled.SetOnCheckedChangeListener(new FadeCheckedChangeListener(this.getSlidingMenu()));
            //fadeEnabled.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            //    @Override
            //    public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
            //        getSlidingMenu().setFadeEnabled(isChecked);
            //    }			
            //});
            SeekBar fadeDeg = (SeekBar)FindViewById(Resource.Id.fade_degree);
            fadeDeg.Max = 1000;
            fadeDeg.Progress = 666;
            fadeDeg.SetOnSeekBarChangeListener(new FadeChangeListener(this.getSlidingMenu()));
            //fadeDeg.setOnSeekBarChangeListener(new OnSeekBarChangeListener() {
            //    @Override
            //    public void onProgressChanged(SeekBar seekBar, int progress,
            //            boolean fromUser) { }
            //    @Override
            //    public void onStartTrackingTouch(SeekBar seekBar) { }
            //    @Override
            //    public void onStopTrackingTouch(SeekBar seekBar) {
            //        getSlidingMenu().setFadeDegree((float) seekBar.getProgress()/seekBar.getMax());
            //    }			
            //});
        }

        class ShadowCheckedChangeListener : Java.Lang.Object, CompoundButton.IOnCheckedChangeListener
        {

            private SSlidingMenu slidingMenu;

            public ShadowCheckedChangeListener(SSlidingMenu slidingMenu)
            {
                // TODO: Complete member initialization
                this.slidingMenu = slidingMenu;
            }

            public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
            {
                if (isChecked)
                    slidingMenu.setShadowDrawable(
                            slidingMenu.Mode == SlidingMenuMode.LEFT ?
                                    R.Drawable.shadow : R.Drawable.shadowright);
                else
                    slidingMenu.setShadowDrawable(null);
            }
        }

        class FadeCheckedChangeListener : Java.Lang.Object, CompoundButton.IOnCheckedChangeListener
        {

            private SSlidingMenu slidingMenu;

            public FadeCheckedChangeListener(SSlidingMenu slidingMenu)
            {
                // TODO: Complete member initialization
                this.slidingMenu = slidingMenu;
            }

            public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
            {
                slidingMenu.setFadeEnabled(isChecked);
            }
        }
        class ScaleChangeListener : Java.Lang.Object, Android.Widget.SeekBar.IOnSeekBarChangeListener
        {
            private SSlidingMenu slidingMenu;

            public ScaleChangeListener(SSlidingMenu slidingMenu)
            {
                // TODO: Complete member initialization
                this.slidingMenu = slidingMenu;
            }

            public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
            {
                 
            }

            public void OnStartTrackingTouch(SeekBar seekBar)
            {
                 
            }

            public void OnStopTrackingTouch(SeekBar seekBar)
            {
                slidingMenu.setBehindScrollScale((float)seekBar.Progress / seekBar.Max);
            }
        }
        
        
        class BehindChangeListener : Java.Lang.Object, Android.Widget.SeekBar.IOnSeekBarChangeListener
        {
            private SSlidingMenu slidingMenu;

            public BehindChangeListener(SSlidingMenu slidingMenu)
            {
                // TODO: Complete member initialization
                this.slidingMenu = slidingMenu;
            }

            public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
            {
                 
            }

            public void OnStartTrackingTouch(SeekBar seekBar)
            {
                 
            }

            public void OnStopTrackingTouch(SeekBar seekBar)
            {
                float percent = (float) seekBar.Progress/seekBar.Max;
                    slidingMenu.setBehindWidth((int) (percent * slidingMenu.Width));
                    slidingMenu.RequestLayout();
            }
        }

        class ShadowChangeListener : Java.Lang.Object, Android.Widget.SeekBar.IOnSeekBarChangeListener
        {
            private SSlidingMenu slidingMenu;

            public ShadowChangeListener(SSlidingMenu slidingMenu)
            {
                // TODO: Complete member initialization
                this.slidingMenu = slidingMenu;
            }

            public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
            {

            }

            public void OnStartTrackingTouch(SeekBar seekBar)
            {

            }

            public void OnStopTrackingTouch(SeekBar seekBar)
            {
                float percent = (float) seekBar.Progress/ (float) seekBar.Max;
                    int width = (int) (percent * (float) slidingMenu.Width);
                    slidingMenu.setShadowWidth(width);
                    slidingMenu.Invalidate();
            }
        }

        class FadeChangeListener : Java.Lang.Object, Android.Widget.SeekBar.IOnSeekBarChangeListener
        {
            private SSlidingMenu slidingMenu;

            public FadeChangeListener(SSlidingMenu slidingMenu)
            {
                // TODO: Complete member initialization
                this.slidingMenu = slidingMenu;
            }

            public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
            {

            }

            public void OnStartTrackingTouch(SeekBar seekBar)
            {

            }

            public void OnStopTrackingTouch(SeekBar seekBar)
            {
                slidingMenu.setFadeDegree((float)seekBar.Progress / seekBar.Max);
            }
        }
        public void OnCheckedChanged(RadioGroup group, int checkedId)
        {
            SSlidingMenu sm = getSlidingMenu();
            switch (checkedId)
            {
                case Resource.Id.left:
                    sm.Mode=SlidingMenuMode.LEFT;
                    sm.setShadowDrawable(Resource.Drawable.shadow);
                    break;
                case Resource.Id.right:
                    sm.Mode=SlidingMenuMode.RIGHT;
                    sm.setShadowDrawable(Resource.Drawable.shadowright);
                    break;
                case Resource.Id.left_right:
                    sm.Mode=SlidingMenuMode.LEFT_RIGHT;
                    sm.setSecondaryMenu(Resource.Layout.menu_frame_two);
                    SupportFragmentManager
                    .BeginTransaction()
                    .Replace(R.Id.menu_frame_two, new SampleListFragment())
                    .Commit();
                    sm.setSecondaryShadowDrawable(R.Drawable.shadowright);
                    sm.setShadowDrawable(R.Drawable.shadow);
                    break;
                case R.Id.touch_above_full:
                    sm.setTouchModeAbove(SSlidingMenu.TOUCHMODE_FULLSCREEN);
                    break;
                case R.Id.touch_above_margin:
                    sm.setTouchModeAbove(SSlidingMenu.TOUCHMODE_MARGIN);
                    break;
                case R.Id.touch_above_none:
                    sm.setTouchModeAbove(SSlidingMenu.TOUCHMODE_NONE);
                    break;
            }
        }
    }
}