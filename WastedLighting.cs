using CUE.NET.Devices.Keyboard;
using GTA;
using System.Drawing;
using CUE.NET.Brushes;
using System;

namespace CorsairGTA
{
    class WastedLighting : Lighting
    {
        public override string Name()
        {
            return "Wasted";
        }

        //UIText debugText = new UIText("INIT!!!!!", new Point(10, 10), 0.4f, Color.WhiteSmoke, 0, false);

        public WastedLighting()
        {
            Tick += HealthLighting_Tick;
        }

        private void HealthLighting_Tick(CorsairKeyboard keyboard)
        {
            UsedBrushes.Clear();
            //debugText.Draw();

            if (Game.Player.Character.IsInjured) 
            {
                isActive = true;

                var brush = new SolidColorBrush(Color.Red);

                int modifier = 50;
                
                brush.Brightness = (float)TickNum / (float)modifier;
                

                if(TickNum > modifier)
                {
                    brush.Brightness = 2f + (brush.Brightness * -1);
                }

                
                if(TickNum > (modifier * 2))
                {
                    TickNum = 0;
                }

                //debugText.Caption = "brightness: " + Convert.ToString(brush.Brightness) + " tick: " + TickNum;

                keyboard.Brush = brush;
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
