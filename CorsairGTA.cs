using GTA;
using System;
using CUE.NET;
using CUE.NET.Exceptions;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Devices.Generic.Enums;
using System.Drawing;
using CUE.NET.Brushes;

namespace CorsairGTA
{
    public class CorsairGTA : Script
    {
        public CorsairGTA()
        {
            Tick += CorsairGTA_Tick;
        }

        public static void ClearKeyboard(CorsairKeyboard keyboard)
        {
            keyboard.Brush = new SolidColorBrush(Color.Black);
        }

        public static Color MakeTransparent(Color color)
        {
            return Color.FromArgb(210, color.R, color.G, color.B);
        }

        bool isInitialized = false;
        CorsairKeyboard keyboard;

        private void CorsairGTA_Tick(object sender, EventArgs e)
        {
            if (!Game.IsLoading && !isInitialized)
            {
                isInitialized = true;

                try
                {
                    CueSDK.Initialize();

                    keyboard = CueSDK.KeyboardSDK;
                    if (keyboard == null)
                        throw new WrapperException("No keyboard found. ");

                    //make keyboard automatically update
                    keyboard.UpdateMode = UpdateMode.Continuous;

                    //load lightings
                    LightingManager.Load(keyboard);

                    //load UI
                    UIManager.Init();

                    //UI.Notify("CorsairGTA successfully initialized (" + CueSDK.LoadedArchitecture + "-SDK)", true);
                    Log.Write("CorsairGTA successfully initialized (" + CueSDK.LoadedArchitecture + "-SDK)");
                }
                catch (CUEException ex)
                {
                    UI.Notify("CorsairGTA failed to initialize. (CUE Error)");
                    UI.Notify("CUE error code: " + Enum.GetName(typeof(CorsairError), ex.Error));

                    Log.Write("CorsairGTA failed to initialize. (CUE Error)");
                    Log.Write("CUE error code: " + Enum.GetName(typeof(CorsairError), ex.Error));
                    Log.Write("Full stack:");
                    Log.Write(ex.ToString());
                    return;
                }
                catch (WrapperException ex)
                {
                    UI.Notify("CorsairGTA failed to initialize. (Wrapper Error)");
                    UI.Notify(ex.Message);

                    Log.Write("CorsairGTA failed to initialize. (Wrapper Error)");
                    Log.Write(ex.Message);
                    Log.Write("Full stack:");
                    Log.Write(ex.ToString());
                    return;
                }
                catch (Exception ex)
                {
                    UI.Notify("CorsairGTA failed to initialize. (Unknown Error)");
                    UI.Notify(ex.Message);

                    Log.Write("CorsairGTA failed to initialize. (Unknown Error)");
                    Log.Write(ex.Message);
                    Log.Write("Full stack:");
                    Log.Write(ex.ToString());
                    return;
                }
                
                //reset keyboard on init..
                ClearKeyboard(keyboard);
            }

            LightingManager.Tick();
            UIManager.Tick();
        }
    }
}

