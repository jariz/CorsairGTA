using CUE.NET.Brushes;
using CUE.NET.Devices.Keyboard;
using GTA;
using System.Collections.Generic;

namespace CorsairGTA
{
    class FireLighting : Lighting
    {
        public override string Name()
        {
            return "Fire   ";
        }

        public override string Description()
        {
            return "Indicates whether or not the player is on\nfire with a nice fire animation.";
        }

        public FireLighting()
        {
            Tick += FireLighting_Tick;
        }

        private void FireLighting_Tick(CorsairKeyboard keyboard)
        {
            if(Game.Player.Character.IsOnFire)
            {
                isActive = true;

                var brush = new SolidColorBrush(System.Drawing.ColorTranslator.FromHtml("#F9690E"));

                int modifier = 50;

                brush.Brightness = (float)TickNum / (float)modifier;


                if (TickNum > modifier)
                {
                    brush.Brightness = 2f + (brush.Brightness * -1);
                }


                if (TickNum > (modifier * 2))
                {
                    TickNum = 0;
                }
                keyboard.Brush = brush;
                UsedBrushes = new List<IBrush>();
                UsedBrushes.Add(keyboard.Brush);
            }
            else
            {
                if (isActive)
                {
                    isActive = false;
                    CorsairGTA.ClearKeyboard(keyboard);
                }
            }
        }
    }
}
