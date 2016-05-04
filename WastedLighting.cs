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

        int TickNum = 0;

        private void HealthLighting_Tick(CorsairKeyboard keyboard)
        {
            //debugText.Draw();

            if(Game.Player.Character.IsInjured) 
            {
                isActive = true;
                TickNum++;

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
                

            }
            else
            {
                if(isActive)
                {
                    TickNum = 0;
                    isActive = false;
                    CorsairGTA.ClearKeyboard(keyboard);
                }
            }
        }

    }
}
