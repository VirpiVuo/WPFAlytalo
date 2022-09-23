using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace WPFAlytalo.Tests
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        //public void TestMethod1()
        //{
        //throw new Exception("Testi epäonnistui!");
    //}
    [TestMethod]
    public void TestValotPaalle()
    {
        Lights Testivalo = new Lights();
            Testivalo.PutLightsOn();
            if (Testivalo.Switched == false)
            {
                Assert.Fail("Valot eivät menneet päälle!");
            }

     }
        
        [TestMethod]
        public void TestTalonLampotila() //testataan saunan maksimilämpötilaa
        {
            Thermostat TestiLampotila = new Thermostat();
            TestiLampotila.SetGoalTemperature(20);
            try
            {
                TestiLampotila.SetGoalTemperature(35);
                Assert.Fail("Poikkeuksen nosto epäonnistui!");
            }
            catch (ArgumentOutOfRangeException)
            {

            }

        }
    }
}
