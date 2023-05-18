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
        Station station;


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

            station = db.Station.Include(s => s.StationInfos).FirstOrDefault(s => s.ID_Station == id);

            fillTb(station); 
            

        }

        public void fillTb(Station station)
        {
            if (station != null)
            {
                foreach (var info in station.StationInfos)
                {
                    switch (info.Name)
                    {

                        case "92":
                            {
                                ai92CostTB.Text = info.Price.ToString();
                                ai92OstatokTB.Text = info.AmountOfFuel.ToString();
                            }
                            break;

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

                        case "Disel Fuel":
                            {
                                aiDTCostTB.Text = info.Price.ToString();
                                aiDTOstatokTB.Text = info.AmountOfFuel.ToString();
                            }
                            break;
                    }

                }
            }
            else {
                CleanFields(); 
            }
        }

        public void CleanFields() {
            ai98CostTb.Text = "";
            ai98OstatokTB.Text = "";

            ai95CostTb.Text = "";
            ai95OstatokTB.Text = "";

            ai92CostTB.Text = "";
            ai92OstatokTB.Text = "";

            aiDTCostTB.Text = "";
            aiDTOstatokTB.Text = "";
        }

        private void sendChangesBTN_Click(object sender, RoutedEventArgs e)
        {

            if (station == null)
            {


                Station newStation = new Station()
                {
                    Address = addressTB.Text
                };

                db.Add(newStation); 

                db.SaveChanges();

                db.StationInfo.Add(new StationInfo() { Name = "92", AmountOfFuel = Convert.ToInt32(ai92OstatokTB.Text), Price = Convert.ToInt32(ai92CostTB.Text), StationId = newStation.ID_Station });
                db.StationInfo.Add(new StationInfo() { Name = "95", AmountOfFuel = Convert.ToInt32(ai95OstatokTB.Text), Price = Convert.ToInt32(ai95CostTb.Text), StationId = newStation.ID_Station });
                db.StationInfo.Add(new StationInfo() { Name = "98", AmountOfFuel = Convert.ToInt32(ai98OstatokTB.Text), Price = Convert.ToInt32(ai98CostTb.Text), StationId = newStation.ID_Station });
                db.StationInfo.Add(new StationInfo() { Name = "Disel Fuel", AmountOfFuel = Convert.ToInt32(aiDTOstatokTB.Text), Price = Convert.ToInt32(aiDTCostTB.Text), StationId = newStation.ID_Station });

                db.SaveChanges();
            }
            else {

                foreach (var info in station.StationInfos) {
                    switch (info.Name)
                    {
                        case "92":
                            {
                                info.Price = Convert.ToDouble(ai92CostTB.Text);
                                info.AmountOfFuel = Convert.ToInt32(ai92OstatokTB.Text); 
                            }
                            break;

                        case "95":
                            {
                                info.Price = Convert.ToDouble(ai95CostTb.Text);
                                info.AmountOfFuel = Convert.ToInt32(ai95OstatokTB.Text);
                            }
                            break;

                        case "98":
                            {
                                info.Price = Convert.ToDouble(ai98CostTb.Text);
                                info.AmountOfFuel = Convert.ToInt32(ai98OstatokTB.Text);
                            }
                            break;

                        case "Disel Fuel":
                            {
                                info.Price = Convert.ToDouble(aiDTCostTB.Text);
                                info.AmountOfFuel = Convert.ToInt32(aiDTOstatokTB.Text);
                            }
                            break;
                    }

                    station.Address = addressTB.Text; 

                    db.StationInfo.UpdateRange(station.StationInfos);

                    db.Station.Update(station);
                }
                
               
            }
        }
    }
}
