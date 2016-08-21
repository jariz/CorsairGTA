using CUE.NET.Devices.Keyboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorsairGTA
{
    static class LightingManager
    {
        public static List<Lighting> Lightings = new List<Lighting>();
        static CorsairKeyboard keyboard;

        public static void Load(CorsairKeyboard keyboard)
        {   
            LightingManager.keyboard = keyboard;
            Lightings.Add(new WantedLighting());
            Lightings.Add(new PlayerLighting());
            Lightings.Add(new WastedLighting());
            //Lightings.Add(new PlayerRunningLighting());
            Lightings.Add(new MoneyLighting());
            //Lightings.Add(new CarLighting());
            Lightings.Add(new HealthLighting());
            Lightings.Add(new UnderwaterLighting());
            Lightings.Add(new FireLighting());
        }

        public static void Tick()
        {
            foreach(Lighting lighting in Lightings)
            {
                lighting.DoTick(keyboard);
            }
        }

        public static IEnumerable<Lighting> GetActiveLightings()
        {
            return Lightings.Where(l => l.isActive);
        }

        public static IEnumerable<Lighting> GetInactiveLightings()
        {
            return Lightings.Where(l => !l.isActive);
        }

        public static IEnumerable<Lighting> GetLightings()
        {
            return Lightings;
        }
    }
}
