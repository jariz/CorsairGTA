using CUE.NET.Devices.Keyboard;
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

        public event LightingHandler Tick;
        public bool isActive = false;

        public void DoTick(CorsairKeyboard keyboard)
        {
            Tick(keyboard);
        }
    }
}
