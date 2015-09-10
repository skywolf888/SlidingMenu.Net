//package com.jeremyfeinstein.slidingmenu.lib;

//import android.graphics.Canvas;
//import android.view.animation.Interpolator;

//import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu.CanvasTransformer;


using Android.Graphics;
using Android.Views.Animations;


namespace Com.Jeremyfeinstein.SlidingMenu.Lib
{
    public class CanvasTransformerBuilder
    {

        private ICanvasTransformer mTrans;

        class Interpolator1 : Java.Lang.Object, IInterpolator
        {

            public float GetInterpolation(float input)
            {
                return input;
            }
        }

        //private static IInterpolator lin = new Interpolator() {
        //    public float getInterpolation(float t) {
        //        return t;
        //    }
        //};

        private static IInterpolator lin = new Interpolator1();


        private class mTransClass : Java.Lang.Object, ICanvasTransformer
        {

            public void transformCanvas(Canvas canvas, float percentOpen)
            {

            }
        }

        private void initTransformer()
        {
            if (mTrans == null)
                mTrans = new mTransClass();
        }

        public ICanvasTransformer zoom(int openedX, int closedX,
                  int openedY, int closedY,
                  int px, int py)
        {
            return zoom(openedX, closedX, openedY, closedY, px, py, lin);
        }

        class mTrans2 : Java.Lang.Object, ICanvasTransformer
        {
            private ICanvasTransformer mtrans;
            private IInterpolator minterp;
            private Canvas mcanvas;
            int mopenedX;
            int mclosedX;
            int mopenedY;
            int mclosedY;
            int mpx;
            int mpy;
            public mTrans2(ICanvasTransformer trans, IInterpolator interp, int openedX, int closedX,
                  int openedY, int closedY,
                  int px, int py)
            {
                mtrans = trans;
                minterp = interp;
                mopenedX = openedX;
                mclosedX = closedX;
                mopenedY = openedY;
                mclosedY = closedY;
                mpx = px;
                mpy = py;

            }
            public void transformCanvas(Canvas canvas, float percentOpen)
            {
                mtrans.transformCanvas(canvas, percentOpen);
                float f = minterp.GetInterpolation(percentOpen);
                canvas.Scale((mopenedX - mclosedX) * f + mclosedX,
                        (mopenedY - mclosedY) * f + mclosedY, mpx, mpy);
            }
        }

        public ICanvasTransformer zoom(int openedX, int closedX,
                  int openedY, int closedY,
                  int px, int py, IInterpolator interp)
        {
            initTransformer();
            //mTrans = new CanvasTransformer() {
            //    public void transformCanvas(Canvas canvas, float percentOpen) {
            //        mTrans.transformCanvas(canvas, percentOpen);
            //        float f = interp.getInterpolation(percentOpen);
            //        canvas.scale((openedX - closedX) * f + closedX,
            //                (openedY - closedY) * f + closedY, px, py);
            //    }			
            //};
            mTrans = new mTrans2(mTrans, interp, openedX, closedX, openedY, closedY, px, py);
            return mTrans;
        }

        public ICanvasTransformer rotate(int openedDeg, int closedDeg,
                  int px, int py)
        {
            return rotate(openedDeg, closedDeg, px, py, lin);
        }

        class mTrans3 : Java.Lang.Object, ICanvasTransformer
        {
            private ICanvasTransformer mTrans;
            private IInterpolator interp;
            private int openedDeg;
            private int closedDeg;
            private int px;
            private int py;

            public mTrans3(ICanvasTransformer mTrans, IInterpolator interp, int openedDeg, int closedDeg, int px, int py)
            {
                // TODO: Complete member initialization
                this.mTrans = mTrans;
                this.interp = interp;
                this.openedDeg = openedDeg;
                this.closedDeg = closedDeg;
                this.px = px;
                this.py = py;
            }
            public void transformCanvas(Canvas canvas, float percentOpen)
            {
                mTrans.transformCanvas(canvas, percentOpen);
                float f = interp.GetInterpolation(percentOpen);
                canvas.Rotate((openedDeg - closedDeg) * f + closedDeg,
                        px, py);
            }
        }

        public ICanvasTransformer rotate(int openedDeg, int closedDeg,
                  int px, int py, IInterpolator interp)
        {
            initTransformer();
            //mTrans = new CanvasTransformer() {
            //    public void transformCanvas(Canvas canvas, float percentOpen) {
            //        mTrans.transformCanvas(canvas, percentOpen);
            //        float f = interp.getInterpolation(percentOpen);
            //        canvas.rotate((openedDeg - closedDeg) * f + closedDeg, 
            //                px, py);
            //    }			
            //};
            mTrans = new mTrans3(mTrans, interp, openedDeg, closedDeg, px, py);
            return mTrans;
        }

        public ICanvasTransformer translate(int openedX, int closedX,
                  int openedY, int closedY)
        {
            return translate(openedX, closedX, openedY, closedY, lin);
        }


        class mTrans4 : Java.Lang.Object, ICanvasTransformer
        {
            private ICanvasTransformer mTrans;
            private IInterpolator interp;
            private int openedX;
            private int closedX;
            private int openedY;
            private int closedY;

            public mTrans4(ICanvasTransformer mTrans, IInterpolator interp, int openedX, int closedX, int openedY, int closedY)
            {
                // TODO: Complete member initialization
                this.mTrans = mTrans;
                this.interp = interp;
                this.openedX = openedX;
                this.closedX = closedX;
                this.openedY = openedY;
                this.closedY = closedY;
            }
            public void transformCanvas(Canvas canvas, float percentOpen)
            {
                mTrans.transformCanvas(canvas, percentOpen);
                float f = interp.GetInterpolation(percentOpen);
                canvas.Translate((openedX - closedX) * f + closedX,
                        (openedY - closedY) * f + closedY);
            }

        }

        public ICanvasTransformer translate(int openedX, int closedX,
                  int openedY, int closedY, IInterpolator interp)
        {
            initTransformer();
            //mTrans = new CanvasTransformer() {
            //    public void transformCanvas(Canvas canvas, float percentOpen) {
            //        mTrans.transformCanvas(canvas, percentOpen);
            //        float f = interp.getInterpolation(percentOpen);
            //        canvas.translate((openedX - closedX) * f + closedX,
            //                (openedY - closedY) * f + closedY);
            //    }			
            //};
            mTrans = new mTrans4(mTrans, interp, openedX, closedX, openedY, closedY);
            return mTrans;
        }

        class mTrans5 : Java.Lang.Object, ICanvasTransformer
        {
            private ICanvasTransformer mTrans;
            private ICanvasTransformer t;

            public mTrans5(ICanvasTransformer mTrans, ICanvasTransformer t)
            {
                // TODO: Complete member initialization
                this.mTrans = mTrans;
                this.t = t;
            }

            public void transformCanvas(Canvas canvas, float percentOpen)
            {
                mTrans.transformCanvas(canvas, percentOpen);
                t.transformCanvas(canvas, percentOpen);
            }
        }

        public ICanvasTransformer concatTransformer(ICanvasTransformer t)
        {
            initTransformer();
            //mTrans = new CanvasTransformer() {
            //    public void transformCanvas(Canvas canvas, float percentOpen) {
            //        mTrans.transformCanvas(canvas, percentOpen);
            //        t.transformCanvas(canvas, percentOpen);
            //    }			
            //};
            mTrans = new mTrans5(mTrans, t);
            return mTrans;
        }

    }
}
