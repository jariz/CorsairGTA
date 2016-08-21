using System;
using System.Collections.Generic;
using System.Drawing;
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
                button.Description = lighting.Description();
                items.Add(button);
            }


            menu = new Menu("CorsairGTA", items.ToArray());


            menu.Width = 310;
            menu.ItemTextCentered = false;

            menu.HeaderFont = GTA.Font.ChaletComprimeCologne;
            menu.HeaderColor = CorsairGTA.MakeTransparent(ColorTranslator.FromHtml("#2ECC71"));
            menu.HeaderTextScale = 0.67f;

            menu.SelectedItemColor = CorsairGTA.MakeTransparent(ColorTranslator.FromHtml("#90C695"));
            menu.UnselectedItemColor = CorsairGTA.MakeTransparent(ColorTranslator.FromHtml("#b7CCB9"));

            menu.FooterColor = CorsairGTA.MakeTransparent(ColorTranslator.FromHtml("#94d09a"));

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

            if (showMenu)
            {
                if (Game.IsKeyPressed(System.Windows.Forms.Keys.NumPad2))
                {
                    if (numpad2Tick == 0)
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

                if (Game.IsKeyPressed(System.Windows.Forms.Keys.NumPad5))
                {
                    if (!numpad5AlreadyPressed)
                    {
                        menu.Items[menu.SelectedIndex].Activate();
                    }

                    numpad5AlreadyPressed = true;
                }
                else
                {
                    numpad5AlreadyPressed = false;
                }

                var size = new System.Drawing.Size(new System.Drawing.Point(10, 10));
                menu.Draw(size);
            }
        }
    }
}
