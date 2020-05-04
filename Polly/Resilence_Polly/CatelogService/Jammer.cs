using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherService
{
    public static class Jammer
    {
        private static int NoOfAttempts { get; set; }


        public static bool IsJam()
        {
            NoOfAttempts += 1;
            
            if (NoOfAttempts > 7) // close jamm
            {
                NoOfAttempts = 0;
            }

            if (NoOfAttempts > 3) // apply jam
            {
               // NoOfAttempts = 0;
                return true;
            }

           

            return false;
        }
    }
}
