using CUE.NET.Brushes;
using CUE.NET.Devices.Keyboard;
using GTA;
using System.Drawing;

namespace CorsairGTA
{
    class CarLighting : Lighting
    {
        //UIText debugText = new UIText("INIT!!!!!", new Point(10, 10), 0.4f, Color.WhiteSmoke, 0, false);

        public override string Name()
        {
            return "Car";
        }

        public CarLighting()
        {
            Tick += CarLighting_Tick;
        }

        private void CarLighting_Tick(CorsairKeyboard keyboard)
        {
            //debugText.Draw();
            var color = Color.FromName(Game.Player.LastVehicle.PrimaryColor.ToString());
            //debugText.Caption = color.R + " " + color.B + " " + color.A;
            keyboard.Brush = new SolidColorBrush(color);
            UsedBrushes.Add(keyboard.Brush);
            isActive = true;
        }
    }
}
