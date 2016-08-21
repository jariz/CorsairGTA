using GTA;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Gradients;
using System.Drawing;
using CUE.NET.Brushes;

namespace CorsairGTA
{
    class UnderwaterLighting : Lighting
    {
        public override string Name()
        {
            return "Water";
        }

        public override string Description()
        {
            return "Indicates when you're underwater or not\nwith a nice ocean animation on your\nkeyboard.";
        }

        public UnderwaterLighting()
        {
            Tick += UnderwaterLighting_Tick;
        }

        private void UnderwaterLighting_Tick(CorsairKeyboard keyboard)
        {
            UsedBrushes.Clear();
            if(Game.Player.Character.IsSwimmingUnderWater)
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
                    new GradientStop(0f, Color.Blue),
                    new GradientStop(pos, Color.Cyan),
                    new GradientStop(1f, Color.Blue)
                };

                LinearGradient blueToCyanGradient = new LinearGradient(gradient);
                PointF startPoint = new PointF(0f, 0f);
                PointF endPoint = new PointF(1f, 0f);
                LinearGradientBrush linearBrush = new LinearGradientBrush(startPoint, endPoint, blueToCyanGradient);
                keyboard.Brush = linearBrush;
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
