using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VAME
{
    public partial class MainWindow : Window
    {
        private bool showNavMesh = false;
        private List<Polygon> navMeshPolygons = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadMapButton_Click(object sender, RoutedEventArgs e)
        {
            string o2Path = "maps/111.o2";
            string nvmPath = "maps/nv_1421.nvm";

            if (File.Exists(nvmPath))
            {
                var triangles = NvmParser.LoadNavMesh(nvmPath);
                DrawNavMesh(triangles);
            }

            RegionInfoText.Text = $"Yüklendi: {o2Path}\nNavMesh: {nvmPath}";
        }

        private void DrawNavMesh(List<float[]> triangles)
        {
            foreach (var tri in triangles)
            {
                Polygon poly = new()
                {
                    Stroke = Brushes.Lime,
                    StrokeThickness = 0.5,
                    Fill = showNavMesh ? Brushes.LimeGreen : Brushes.Transparent,
                    Opacity = 0.3
                };

                for (int i = 0; i < 3; i++)
                {
                    poly.Points.Add(new Point(tri[i * 3], tri[i * 3 + 1]));
                }
                navMeshPolygons.Add(poly);
                MapCanvas.Children.Add(poly);
            }
        }

        private void NavMeshToggle_Checked(object sender, RoutedEventArgs e)
        {
            showNavMesh = true;
            foreach (var poly in navMeshPolygons)
            {
                poly.Fill = Brushes.LimeGreen;
            }
        }

        private void NavMeshToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            showNavMesh = false;
            foreach (var poly in navMeshPolygons)
            {
                poly.Fill = Brushes.Transparent;
            }
        }

        private void ClearMapButton_Click(object sender, RoutedEventArgs e)
        {
            MapCanvas.Children.Clear();
            navMeshPolygons.Clear();
            RegionInfoText.Text = "Harita temizlendi.";
        }

        private void AddObjectButton_Click(object sender, RoutedEventArgs e)
        {
            Ellipse mob = new()
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(mob, 100);
            Canvas.SetTop(mob, 100);
            MapCanvas.Children.Add(mob);
        }

        private void MapCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point click = e.GetPosition(MapCanvas);
            int regionX = (int)(click.X / 192);
            int regionY = (int)(click.Y / 192);
            int regionId = regionX * 256 + regionY;
            RegionInfoText.Text = $"X: {click.X:0}, Y: {click.Y:0}\nRegionX: {regionX}, RegionY: {regionY}\nRegionID: {regionId}";
        }
    }
}
