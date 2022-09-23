using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFAlytalo
{
    public class Sauna
    {
        public Boolean Switched { get; set; }
        public int Heat { get; set; }

        public int SaunaTemp = 20;       //asetetaam saunalle peruslämpötila

        public int MaxSaunaTemp = 100;


        public void TurnOn() // toimii kuin kiihdytä nappi ajastimen kanssa
        {
            Switched = true;
            //Heat++;
        }
        public void TurnOff()
        {
            Switched = false;
            //Heat--;
        }


        //metodi, jolla saunan lämpötilaa kasvatetaan asetettuun maksimilämpöön
        public void SaunaTempUp()
        {
            if (SaunaTemp < MaxSaunaTemp)
            {
                SaunaTemp++;
            }
            else if (SaunaTemp == MaxSaunaTemp)
            {
                SaunaTemp = MaxSaunaTemp;
            }
        }

        public void SaunaTempDown()
        {
            SaunaTemp--;
        }
    }

}




        