/**
 * Sirens!!!
**/

using CUE.NET.Devices.Keyboard;
using GTA;
using CUE.NET.Gradients;
using System.Drawing;
using CUE.NET.Brushes;
using System.Collections.Generic;

namespace CorsairGTA
{
    class WantedLighting : Lighting
    {
        public override string Name()
        {
            return "Wanted";
        }

        public override string Description()
        {
            return "Indicates whether or not you are wanted\nwith a animation of a siren on your\nkeyboard.";
        }

        public WantedLighting()
        {
            Tick += WantedLighting_Tick;
        }

        private void WantedLighting_Tick(CorsairKeyboard keyboard)
        {
            if (Game.Player.WantedLevel > 0 && !Game.Player.Character.IsInjured)
            {
                isActive = true;

                int modifier = 50;

                float pos = (float)TickNum / (float)modifier;

                if (pos == 0.0f) pos = 0.01f;

                if (TickNum > modifier)
                {
                    pos = 2f + (pos * -1);
                }


                if (TickNum >= (modifier * 2))
                {
                    TickNum = 0;
                }

                GradientStop[] gradient =
                {
                    new GradientStop(0f, Color.Red),
                    new GradientStop(pos, Color.Blue),
                    new GradientStop(1f, Color.Red)
                };

                LinearGradient blueToRedGradient = new LinearGradient(gradient);
                PointF startPoint = new PointF(0f, 0f);
                PointF endPoint = new PointF(1f, 0f);
                LinearGradientBrush linearBrush = new LinearGradientBrush(startPoint, endPoint, blueToRedGradient);
                keyboard.Brush = linearBrush;
                UsedBrushes = new List<IBrush>();
                UsedBrushes.Add(keyboard.Brush);
            }
            else
            {
                if(isActive)
                {
                    isActive = false;
                    CorsairGTA.ClearKeyboard(keyboard);
                }
            }
        }

    }
}
