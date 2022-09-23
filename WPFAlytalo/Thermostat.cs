using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFAlytalo
{
    public class Thermostat
    {
        public int Temperature { get; set; } // luodaan ominaisuus johon tavoitelämpötila tallennetaan
        public void SetGoalTemperature(int Temperature1) // luodaan metodi jolla asetetaan ja tallennetaan tavoitelämpötila
        {
            
            if (Temperature1 >= 15 && Temperature1 <= 25)
                {
                    Temperature = Temperature1;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
           
        
        public int GetGoalTemperature() // luodaan metodi jolla kutsutaan tallennettua tavoitelämpötilaa
        {
            return Temperature;
        }
    }
}
