using CUE.NET.Brushes;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Devices.Keyboard.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorsairGTA
{
    public delegate void LightingHandler(CorsairKeyboard keyboard);
    abstract class Lighting
    {
        public abstract string Name();
        public List<IBrush> UsedBrushes = new List<IBrush>();

        public event LightingHandler Tick;
        public bool isActive = false;

        bool FadeInRunning = false;
        bool FadeOutRunning = false;

        public int TickNum = 0;
        public int FadeTickNum = 0;

        public void DoTick(CorsairKeyboard keyboard)
        {
            TickNum++;

            var wasActive = isActive;

            Tick(keyboard);

            if (isActive && !wasActive)
            {
                FadeTickNum = 0;
                FadeInRunning = true;
            }
            if (!isActive && wasActive)
            {
                FadeTickNum = 0;
                FadeOutRunning = true;
            }


            if (FadeInRunning)
            {
                FadeTickNum++;
                UsedBrushes.ForEach(brush => brush.Opacity = (float)FadeTickNum / 100);
            }
            else if(FadeOutRunning)
            {
                FadeTickNum++;
                UsedBrushes.ForEach(brush => brush.Opacity = 2f + (float)((FadeTickNum / 100) * -1));
            }

            if (FadeTickNum > 100)
            {
                FadeInRunning = false;
                FadeOutRunning = false;
                FadeTickNum = 0;
            }
        }
    }
}
