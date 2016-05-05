/**
 * CorsairGTA's default lighting when nothing else of interest is happening.
 * Uses the character's color with a low brightness. (franklin = green, trevor = brown, etc)

 * !!!BUG!!! hook keeps reporting the color back which we used to start with, not the actual color being used right now
**/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUE.NET.Devices.Keyboard;
using GTA;
using GTA.Native;
using CUE.NET.Brushes;
using System.Drawing;


namespace CorsairGTA
{
    class PlayerLighting : Lighting
    {
        public override string Name()
        {
            return "Player";
        }

        public PlayerLighting()
        {
            Tick += PlayerLighting_Tick;
        }
        
        private void PlayerLighting_Tick(CorsairKeyboard keyboard)
        {
            UsedBrushes.Clear();

            //todo only check every 30 ticks to save perf?
            bool imTheOnlyActiveLighting = true;
            foreach(Lighting lighting in LightingManager.GetActiveLightings())
            {
                if (lighting.Name() == Name()) continue;
                imTheOnlyActiveLighting = false;
            }
            
            if(imTheOnlyActiveLighting)
            {
                isActive = true;

                //was hoping to use Game.Player.Color but it appears to keep the same color :(
                //so use model instead for now until this is fixed (https://github.com/crosire/scripthookvdotnet/issues/455)
                //SolidColorBrush playerBrush = new SolidColorBrush(Game.Player.Color);

                Color color = Color.Black;
                switch(Game.Player.Character.Model.Hash)
                {
                    case unchecked((int)PedHash.Franklin):
                        color = ColorTranslator.FromHtml("#AAEDAA");
                        break;

                    case unchecked((int)PedHash.Michael):
                        color = ColorTranslator.FromHtml("#64B3D3");
                        break;

                    case unchecked((int)PedHash.Trevor):
                        color = ColorTranslator.FromHtml("#FEA256");
                        break;
                }

                //if (color == Color.Black) return;

                SolidColorBrush playerBrush = new SolidColorBrush(color);
                //playerBrush.Brightness = 0.6f;
                keyboard.Brush = playerBrush;
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
