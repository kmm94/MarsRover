using MarsRover.Model.Interface;
using MarsRover.Model;
using MarsRover.Model.DataType;
using MarsRover.Properties;
using System;
using System.Drawing;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using MarsRover.Controller;
using MarsRover.Utils;

namespace MarsRover.View
{
    public partial class MainWindow : Window
    {
        private readonly int gridSize = 50;
        private readonly int worldSize = 20;
        private readonly DispatcherTimer timer = new DispatcherTimer();
        private readonly ResourceSet resources;
        private readonly IRoverController controller;

        public MainWindow()
        {
            resources = new ResourceSet(typeof(Resources).Assembly.GetManifestResourceStream("MarsRover.Properties.Resources.resources"));

            controller = new RoverController(worldSize);

            InitializeComponent();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 16);
            timer.Tick += Update;
            timer.Start();

            CreateImageFields(controller.GetWorld());

            Rover.Instance.image.Source = BitmapUtil.BitmapToImageSource(resources.GetObject("Curiousity2") as Bitmap);
            Rover.Instance.image.Width = 50;
            Rover.Instance.image.Height = 50;
            WorldMap.Children.Add(Rover.Instance.image);
            Canvas.SetTop(Rover.Instance.image, Rover.Instance.x * gridSize);
            Canvas.SetLeft(Rover.Instance.image, Rover.Instance.y * gridSize);
            Rover.Instance.image = Rover.Instance.image;

            KeyDown += Keyboard;         
        }

        void Keyboard(object sender, KeyEventArgs e)
        {
            controller.KeyboardHandler(e);
        }

        private void CreateImageFields(World world)
        {
            for (var i=0; i<world.size; i++)
            {
                for(var j=0; j<world.size; j++)
                {
                    switch (world.GetField(i, j).type)
                    {
                        case FieldType.DIRT:
                            world.GetField(i, j).image.Source = BitmapUtil.BitmapToImageSource(resources.GetObject("Dirt") as Bitmap);
                            break;
                        case FieldType.STONE:
                            world.GetField(i, j).image.Source = BitmapUtil.BitmapToImageSource(resources.GetObject("Stone") as Bitmap);
                            break;
                        case FieldType.EXIT:
                            world.GetField(i, j).image.Source = BitmapUtil.BitmapToImageSource(resources.GetObject("Exit") as Bitmap);
                            break;
                    }
                    WorldMap.Children.Add(world.GetField(i, j).image);
                    Canvas.SetTop(world.GetField(i, j).image, j * gridSize);
                    Canvas.SetLeft(world.GetField(i, j).image, i * gridSize);
                }
            }            
        }

        private void Update(object sender, EventArgs e)
        {            
            Canvas.SetTop(Rover.Instance.image, Rover.Instance.y * gridSize);
            Canvas.SetLeft(Rover.Instance.image, Rover.Instance.x * gridSize);
        }
    }
}
