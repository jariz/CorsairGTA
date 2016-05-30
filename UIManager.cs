using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;

namespace CorsairGTA
{
    static class UIManager
    {
        static Menu menu;

        static bool f3AlreadyPressed = false;
        static bool numpad5AlreadyPressed = false;
        static int numpad8Tick = 0;
        static int numpad2Tick = 0;

        static bool showMenu = false;
        public static void Init()
        {
            var items = new List<IMenuItem>();
            
            foreach(Lighting lighting in LightingManager.GetLightings())
            {
                MenuButton button = new MenuButton(lighting.Name() + " lighting\t\t[ON]");
                button.Activated += (object sender, EventArgs e) =>
                {
                    if(lighting.Enabled)
                    {
                        lighting.Disable();
                    }
                    else
                    {
                        lighting.Enable();
                    }
                    button.Caption = lighting.Name() + " lighting\t\t[" + (lighting.Enabled ? "ON" : "OFF") + "]";
                };
                items.Add(button);
            }


            menu = new Menu("CorsairGTA", items.ToArray());
            //menu.SelectedItemColor = System.Drawing.Color.Red;
            //menu.UnselectedItemColor = System.Drawing.Color.DarkRed;
           
            menu.HeaderFont = Font.ChaletComprimeCologne;
            menu.ItemTextCentered = false;
            menu.HasFooter = false;

            menu.Width = 210;

            menu.HeaderColor = System.Drawing.Color.Red;
            menu.HeaderTextScale = 0.67f;
            menu.Initialize();
        }

        public static void Tick()
        {
            if(Game.IsKeyPressed(System.Windows.Forms.Keys.F3))
            {
                if(!f3AlreadyPressed)
                {
                    showMenu = !showMenu;
                }
                
                f3AlreadyPressed = true;
            }
            else
            {
                f3AlreadyPressed = false;
            }

            if(Game.IsKeyPressed(System.Windows.Forms.Keys.NumPad2))
            {
                if(numpad2Tick == 0)
                {
                    if (menu.SelectedIndex < menu.Items.Count - 1)
                    {
                        menu.SelectedIndex += 1;
                    }
                    else
                    {
                        menu.SelectedIndex = 0;
                    }
                }

                numpad2Tick += 1;

                if (numpad2Tick > 10)
                {
                    numpad2Tick = 0;
                }
            }
            else
            {
                numpad2Tick = 0;
            }

            if (Game.IsKeyPressed(System.Windows.Forms.Keys.NumPad8))
            {
                if (numpad8Tick == 0)
                {
                    if (menu.SelectedIndex > 0)
                    {
                        menu.SelectedIndex -= 1;
                    }
                    else
                    {
                        menu.SelectedIndex = menu.Items.Count - 1;
                    }
                }

                numpad8Tick += 1;

                if (numpad8Tick > 10)
                {
                    numpad8Tick = 0;
                }
            }
            else
            {
                numpad8Tick = 0;
            }

            if(Game.IsKeyPressed(System.Windows.Forms.Keys.NumPad5))
            {
                if(!numpad5AlreadyPressed)
                {
                    menu.Items[menu.SelectedIndex].Activate();
                }

                numpad5AlreadyPressed = true;
            }
            else
            {
                numpad5AlreadyPressed = false;
            }

            if (showMenu)
            {
                var size = new System.Drawing.Size(new System.Drawing.Point(20, 20));
                menu.Draw(size);
            }
            
        }


    }
}
