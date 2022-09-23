using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading; // lisätään using -lause Timeria varten

namespace WPFAlytalo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Lights kitchen = new Lights(); // luodaan uusi olio Lights - luokkaan
        Lights livingroom = new Lights(); // luodaan uusi olio Lights - luokkaan
        Thermostat GoalTemperature = new Thermostat(); // luodaan uusi olio Thermostat -luokkaan
        Sauna sauna = new Sauna(); // luodaan uusi olio Sauna -luokkaan 
        public DispatcherTimer Heating = new DispatcherTimer(); //luodaan ajastin lämmitykselle
        public DispatcherTimer Cooling = new DispatcherTimer(); // luodaan ajastin jäähdytykselle

        public MainWindow()
        {
            InitializeComponent();
            Heating.Tick += Heating_Tick;               // kutsutaan ajastimen tick -rutiinia
            Heating.Interval = new TimeSpan(0, 0, 0, 1); // määritetään ajastin laukemaan kerran sekunnissa
            Cooling.Tick += Cooling_Tick;                 // en saanut tätä jäähdystysajastinta toimimaan vaikka olen yrittänyt koko viime viikon ratkoa ongelmaa
            Cooling.Interval = new TimeSpan(0, 0, 0, 1);
        }
        private void Heating_Tick(object sender, EventArgs e)
        {
            //txtSaunaHeat.Text = GoalTemperature.GetGoalTemperature().ToString(); // määritetään lämpötilan alkamaan talon tämänhetkisestä lämpötilasta, tätä en saanut toimimaan eli ilmeisesti virheellinen koodi?
            btnSaunaOn_Click(btnSaunaOn, new RoutedEventArgs(Button.ClickEvent));
            //tämä lisätty:
            sauna.SaunaTempUp();
        }
        private void Cooling_Tick(object sender, EventArgs e)
        {
            btnSaunaOff_Click(btnSaunaOff, new RoutedEventArgs(Button.ClickEvent));
            if (sauna.SaunaTemp > GoalTemperature.Temperature)
            {
                sauna.SaunaTempDown();
            }

            //txtSaunaHeat.Text = sauna.Heat.ToString();
        }
        private void btnValotPaalleKeittio_Click(object sender, RoutedEventArgs e) // määritetään mitä tapahtuu kun keittioön valot päälle -nappia klikataan
        {
                kitchen.PutLightsOn(); //kutsutaan luotua metodia
                if (kitchen.Switched == true)
            {
                    btnIndicatorKitchen.Background = Brushes.LightGoldenrodYellow; // jos ehto toteutuu, keittiön väri-indikaattori "syttyy" keltaiseksi
                }
        }

        private void btnLightsOnLivingroom_Click(object sender, RoutedEventArgs e) //määritetään mitä tapahtuu kun klikataan olohuoneen valot päälle -nappia
        {
            livingroom.PutLightsOn(); // kutsutaan luotua metodia
            if (livingroom.Switched == true)
            {
                btnIndicatorLivingroom.Background = Brushes.LightGoldenrodYellow; // jos ehto toteutuu, olohuoneen väri-indikaattori "syttyy" keltaiseksi
            }
        }

        private void sldDimmerKitchen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) // määritetään dimmerin arvonmuutos
        {
            var dimmerKitchen = sender as Slider;
            double value = dimmerKitchen.Value;
            txtKitchenLight.Text = value.ToString("0.0") + "/" + dimmerKitchen.Maximum;
        }

        private void sldDimmerLivingroom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var dimmerLivingroom = sender as Slider;
            double value = dimmerLivingroom.Value;
            txtLivingroomLight.Text = value.ToString("0.0") + "/" + dimmerLivingroom.Maximum;
        }

        private void btnLightsOffKitchen_Click(object sender, RoutedEventArgs e) // määritetään mitä tapahtuu kun painetaan valot pois -nappia
        {
            kitchen.TurnLightsOff(); // kutsutaan luotua metodia
            if (kitchen.Switched == false)
            {
                btnIndicatorKitchen.Background = Brushes.Black; // valo "sammuu" mustaksi
                sldDimmerKitchen.Value = 0; // dimmerin arvo nollautuu
            }
        }

        private void btnLightsOffLivingroom_Click(object sender, RoutedEventArgs e) // määritetään mitä tapahtuu kun painetaan olohuoneen valot pois -nappia
        {
            livingroom.TurnLightsOff(); // kutsutaan luotua metodia
            if (livingroom.Switched == false)
            {
                btnIndicatorLivingroom.Background = Brushes.Black; // valo "sammuu" mustaksi
                sldDimmerLivingroom.Value = 0; // dimmerin arvo nollautuu
            }
        }

        private void btnSetNewTemperature_Click(object sender, RoutedEventArgs e) // määritetään mitä tapahtuu kun painetaan Aeta lämpötila -nappia
        {
            GoalTemperature.SetGoalTemperature(int.Parse(txtGoalTemperature.Text)); // kutsutaan luotua metodia ja talletetaan annettu tavoitelämpötila
            txtTemperatureNow.Text = GoalTemperature.GetGoalTemperature().ToString(); // kutsutaan luotua metodia ja haetaan tallenettu tieto sekä näytetään se Tämänhetkinen lämpötila- kentässä
            txtGoalTemperature.Text = ""; // tyhjennetään Tavoitelämpötila - kenttä
            txtGoalTemperature.Focus(); // asetetaan focus Tavoitelämpötila -kenttään
        }

        private void btnSaunaOn_Click(object sender, RoutedEventArgs e)
        {
            if (!Heating.IsEnabled)  //kun nappia painetaan, myös lämmitysajastin käynnistyy
            {
                Heating.Start();
            }
            sauna.TurnOn();         //kutsutaan luotua metodia
            Cooling.Stop();
            if (sauna.Switched == true)
            {
                txtSauna.Text = "SAUNA PÄÄLLÄ";
            }
            txtSaunaHeat.Text = sauna.SaunaTemp.ToString();

        }
        private void btnSaunaOff_Click(object sender, RoutedEventArgs e)
        {
            if (!Cooling.IsEnabled)
            {
                Cooling.Start();
            }
            Heating.Stop();
            sauna.TurnOff();
            txtSaunaHeat.Text = sauna.SaunaTemp.ToString();
            txtSauna.Text = "";

            //if (sauna.Switched == false)
            //{
            //    txtSauna.Text = " ";
            //}

        }
    } 
}
