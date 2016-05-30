using System;
using CUE.NET.Devices.Keyboard;
using GTA;
using CUE.NET.Brushes;
using System.Drawing;

namespace CorsairGTA
{
    class PlayerRunningLighting : Lighting
    {
        public override string Name()
        {
            return "PlayerRunning";
        }

        public PlayerRunningLighting()
        {
            Tick += PlayerRunningLighting_Tick;
        }
        

        UIText debugText = new UIText("INIT!!!!!", new Point(10, 10), 0.4f, Color.WhiteSmoke, 0, false);

        private void PlayerRunningLighting_Tick(CorsairKeyboard keyboard)
        {
            debugText.Draw();
            if (Game.Player.Character.IsRunning || Game.Player.Character.IsSprinting)
            {
                //calculate brightness
                int modifier = 200 * (5 - Game.Player.WantedLevel);

                float brightness = (float)TickNum / (float)modifier;


                if (TickNum > modifier)
                {
                    brightness = 2f + (brightness * -1);
                }


                if (TickNum > (modifier * 2))
                {
                    TickNum = 0;
                }

                debugText.Caption = "brightness: " + Convert.ToString(brightness) + " tick: " + TickNum;

                foreach (Lighting lighting in LightingManager.GetActiveLightings())
                {
                    if (lighting.Name() == Name()) continue;
                    
                    foreach(IBrush brush in lighting.UsedBrushes)
                    {
                        brush.Brightness = brightness;
                    }
                }
            }
        }
    }
}
