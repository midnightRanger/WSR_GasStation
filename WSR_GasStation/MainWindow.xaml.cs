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
using Microsoft.EntityFrameworkCore;
using WSR_GasStation.Domain.Models;

namespace WSR_GasStation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StationDbContext db;  


        public MainWindow()
        {
            InitializeComponent();

            db = new StationDbContext();

            db.Station.Load();

            //stations.ItemsSource = db.Station.ToList();
            //stations.SelectedValuePath = "ID_Station";
            //stations.CanUserAddRows = false;
            //stations.CanUserDeleteRows = false;
            //stations.SelectionMode = DataGridSelectionMode.Single;

        }

        private void receiveDataBTN_Click(object sender, RoutedEventArgs e) 
        {
            int id = 0; 
            try
            {
                id = Convert.ToInt32(stationIdTB.Text);
            }
            catch (Exception ex) {
                MessageBox.Show($"Что-то пошло не так. Ошибка: {ex.Message}" , "Ошибка!", MessageBoxButton.OK);
                stationIdTB.Text = "";
            }

            if (id > 99 || id < 1) {
                MessageBox.Show($"Значение ID должно быть от 1 до 99", "Ошибка!", MessageBoxButton.OK);
            }

            Station station = db.Station.Include(s => s.StationInfos).FirstOrDefault(s => s.ID_Station == id);

            if (station != null) { fillTb(station); }
            
            


            

        }

        public void fillTb(Station station) {
            
            foreach (var info in station.StationInfos) {
                switch (info.Name) {

                    case "92": {
                            ai92CostTB.Text = info.Price.ToString();
                            ai92OstatokTB.Text = info.AmountOfFuel.ToString();
                        } break;

                    case "95":
                        {
                            ai95CostTb.Text = info.Price.ToString();
                            ai95OstatokTB.Text = info.AmountOfFuel.ToString();
                        }
                        break;

                    case "98":
                        {
                            ai98CostTb.Text = info.Price.ToString();
                            ai98OstatokTB.Text = info.AmountOfFuel.ToString();
                        }
                        break;
                }

            }
        }

    
    }
}
