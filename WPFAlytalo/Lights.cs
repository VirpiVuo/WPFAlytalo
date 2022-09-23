using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFAlytalo
{
    public class Lights //luodaan valoja varten uusi luokka
    {
        public Boolean Switched { get; set; } // Luodaan valojen päälle laittoa/ sulkua varten uusi ominaisuus
        //public string Dimmer { get; set; } määrittelin himmennyksen määrän suoraan XAMLissa, onhan tämä ok?
        public void PutLightsOn() // luodaan metodi valojen päälle laittamiseksi
        {
            Switched = true;
        }
        public void TurnLightsOff() // luodaan metodi valojen sammuttamiseksi
        {
            Switched = false;
        }
    }
}
