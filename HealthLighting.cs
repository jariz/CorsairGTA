﻿using System;
using GTA;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Brushes;
using System.Drawing;
using CUE.NET.Devices.Keyboard.Keys;
using CUE.NET.Devices.Keyboard.Enums;

namespace CorsairGTA
{
    class HealthLighting : Lighting
    {
        public override string Name()
        {
            return "Health";
        }

        public HealthLighting()
        {
            Tick += HealthLighting_Tick;
        }
        

        int lastHealth = -1;
        int healthTickNum = 0;

        private void HealthLighting_Tick(CorsairKeyboard keyboard)
        {
            if (lastHealth == -1) lastHealth = Game.Player.Character.Health;

            if (Game.Player.Character.Health < lastHealth)
            {
                healthTickNum = 0;
                isActive = true;
            }

            lastHealth = Game.Player.Character.Health;

            if (isActive)
            {
                healthTickNum++;
                ListKeyGroup group = new ListKeyGroup(keyboard);

                group.AddKey(new CorsairKey[] {
                    keyboard[CorsairKeyboardKeyId.D7],
                    keyboard[CorsairKeyboardKeyId.D0],

                    keyboard['Y'],
                    keyboard['U'],
                    keyboard['O'],
                    keyboard['I'],
                    keyboard['P'],

                    keyboard['H'],
                    keyboard['J'],
                    keyboard['K'],
                    keyboard['L'],

                    keyboard['N'],
                    keyboard['M'],
                    keyboard[CorsairKeyboardKeyId.CommaAndLessThan]
                });
                
                group.Brush = new SolidColorBrush(Color.Red);
                UsedBrushes.Add(group.Brush);
            }

            if (healthTickNum > 100)
            {
                isActive = false;
            }
        }
    }
}
