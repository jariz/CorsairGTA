using GTA;
using System;
using CUE.NET;
using CUE.NET.Exceptions;
using CUE.NET.Devices.Keyboard;
using System.Diagnostics;
using CUE.NET.Devices.Generic.Enums;
using System.Drawing;
using CUE.NET.Devices.Keyboard.Enums;
using CUE.NET.Gradients;
using CUE.NET.Brushes;
using CUE.NET.Devices.Keyboard.Keys;
using System.Collections;
using CUE.NET.Devices.Generic;
using System.Collections.Generic;

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

                    UI.Notify("CorsairGTA successfully initialized (" + CueSDK.LoadedArchitecture + "-SDK)", true);
                }
                catch (CUEException ex)
                {
                    UI.Notify("CorsairGTA failed to initialize. (CUE Error)");
                    UI.Notify("CUE error code: " + Enum.GetName(typeof(CorsairError), ex.Error));
                    return;
                }
                catch (WrapperException ex)
                {
                    UI.Notify("CorsairGTA failed to initialize. (Wrapper Error)");
                    UI.Notify(ex.Message);
                    return;
                }
                catch (Exception ex)
                {
                    UI.Notify("CorsairGTA failed to initialize. (Unknown Error)");
                    UI.Notify(ex.Message);
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

