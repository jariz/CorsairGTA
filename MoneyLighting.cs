using System;
using GTA;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Brushes;
using System.Drawing;

namespace CorsairGTA
{
    class MoneyLighting : Lighting
    {
        public override string Name()
        {
            return "Money";
        }


        public override string Description()
        {
            return "Indicates a increase in player money\nwith a green flash on the keyboard.";
        }

        public MoneyLighting()
        {
            Tick += MoneyLighting_Tick;
        }

        int lastMoney = -1;
        int moneyTickNum = 0;

        private void MoneyLighting_Tick(CorsairKeyboard keyboard)
        {
            UsedBrushes.Clear();

            if (lastMoney == -1) lastMoney = Game.Player.Money;

            if(Game.Player.Money > lastMoney)
            {
                moneyTickNum = 0;
                isActive = true;
            }
            
            lastMoney = Game.Player.Money;

            if (isActive)
            {
                moneyTickNum++;
                keyboard.Brush = new SolidColorBrush(Color.Green);
                UsedBrushes.Add(keyboard.Brush);
            }

            if(moneyTickNum > 100)
            {
                isActive = false;
            }
        }
    }
}
