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

            stations.ItemsSource = db.Station.ToList();
            stations.SelectedValuePath = "ID_Station";
            stations.CanUserAddRows = false;
            stations.CanUserDeleteRows = false;
            stations.SelectionMode = DataGridSelectionMode.Single;

        }


    }
}
