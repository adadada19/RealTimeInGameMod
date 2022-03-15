using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace RealTimeInGameMod.Time
{
    class TimeConverter : ModWorld
    {
        public double RealTime = new double();
        public bool isTimeConverted2 = true;
        public override void PreUpdate()
        {
            base.PreUpdate();
            string curTimeLong = DateTime.Now.ToLongTimeString();
            string[] times = curTimeLong.Split(new char[] { ':' });
            string hours = times[0];
            string minutes = times[1];
            string seconds = times[2];
            double h = Convert.ToDouble(hours);
            double m = Convert.ToDouble(minutes);
            double s = Convert.ToDouble(seconds);
            double convertedTime = (h * 60 + m) * 60 + s;
            if (Main.fastForwardTime == false && isTimeConverted2 == true) // TODO Make transition more smooth
            {
                if (convertedTime >= 70200 && convertedTime < 86400)
                {
                    Main.dayTime = false;
                    RealTime = convertedTime - 70200;
                }
                else if (convertedTime >= 0 && convertedTime < 16200)
                {
                    Main.dayTime = false;
                    RealTime = convertedTime + 16200;
                }
                else if (convertedTime >= 16200 && convertedTime < 70200)
                {
                    Main.dayTime = true;
                    RealTime = convertedTime - 16200;
                }
                //RealTime = ((h * 60 + m) * 60) + s; // 0 - 86399(86400) тиков реального
                //Main.NewText(RealTime);
                Main.time = RealTime; // Last converter step
            }

        }
        public void TimeConvert(bool isTimeConverted)
        {
            isTimeConverted2 = isTimeConverted;
        }
    }
}