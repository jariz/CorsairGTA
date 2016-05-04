/**
 * Sirens!!!
**/

using CUE.NET.Devices.Keyboard;
using GTA;
using CUE.NET.Gradients;
using System.Drawing;
using CUE.NET.Brushes;

namespace CorsairGTA
{
    class WantedLighting : Lighting
    {
        public override string Name()
        {
            return "Wanted";
        }

        GradientStop[] first =
        {
            new GradientStop(0f, Color.Blue),
            new GradientStop(1f, Color.Red)
        };

        GradientStop[] second =
        {
            new GradientStop(1f, Color.Blue),
            new GradientStop(0f, Color.Red)
        };

        int TickNum = 0;

        public WantedLighting()
        {
            Tick += WantedLighting_Tick;
        }

        private void WantedLighting_Tick(CorsairKeyboard keyboard)
        {
            if (Game.Player.WantedLevel > 0 && !Game.Player.Character.IsInjured)
            {
                isActive = true;
                TickNum++;

                if (TickNum > 120) TickNum = 0;

                GradientStop[] gradient;
                if (TickNum > 60) gradient = second;
                else gradient = first;

                LinearGradient blueToRedGradient = new LinearGradient(gradient);
                PointF startPoint = new PointF(0f, 0f);
                PointF endPoint = new PointF(1f, 0f);
                LinearGradientBrush linearBrush = new LinearGradientBrush(startPoint, endPoint, blueToRedGradient);
                keyboard.Brush = linearBrush;
            }
            else
            {
                if(isActive)
                {
                    isActive = false;
                    CorsairGTA.ClearKeyboard(keyboard);
                    TickNum = 0;
                }
            }
        }

    }
}
