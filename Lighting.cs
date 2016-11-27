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
        public Lighting()
        {
            Log.Write($"Lighting {this.Name()} loaded.");
        }

        public abstract string Name();
        public abstract string Description();

        public virtual void updateBrush(IBrush brush, CorsairKeyboard keyboard)
        {

        }

        public List<IBrush> UsedBrushes = new List<IBrush>();

        public event LightingHandler Tick;


        public bool _isActive = false;
        public bool isActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                if(_isActive != value)
                    Log.Write($"Lighting {this.Name()} " + (value ? "became active" : "became inactive"));
                _isActive = value;
            }
        }

        bool FadeInRunning = false;
        bool FadeOutRunning = false;

        public bool _enabled = true;
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                if(_enabled != value)
                    Log.Write($"Lighting {this.Name()} " + (value ? "enabled" : "disabled"));
                _enabled = value;
            }
        }

        public int TickNum = 0;
        public int FadeTickNum = 0;

        public void Disable()
        {
            Enabled = false;
            isActive = false;
        }

        public void Enable()
        {
            Enabled = true;
        }

        public void DoTick(CorsairKeyboard keyboard)
        {
            if (!Enabled)
            {
                CorsairGTA.ClearKeyboard(keyboard);
                return;
            }

            TickNum++;
            
            var wasActive = isActive;

            Tick(keyboard);

            if (isActive && !wasActive)
            {
                FadeTickNum = 0;
                FadeInRunning = true;

                Log.Write($"Starting fadein for {this.Name()} w/{UsedBrushes.Count} brushes");
            }
            if (!isActive && wasActive)
            {
                FadeTickNum = 0;
                FadeOutRunning = true;

                Log.Write($"Starting fadeout for {this.Name()} w/{UsedBrushes.Count} brushes");
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

            UsedBrushes.ForEach(brush => updateBrush(brush, keyboard));
        }
    }
}
