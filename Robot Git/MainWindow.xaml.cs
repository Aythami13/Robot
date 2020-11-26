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

namespace Robot_Git
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int TAMANY_ELEMENTS = 50;

        private Random random = new Random();
        private Point posicioRobot;
        private Point posicioTresor;

        private ImageBrush imgTresor = new ImageBrush();
        private string rutaTresor = "tresor.jpg";

        private ImageBrush imgRobot = new ImageBrush();
        private string rutaRobot = "robot.jpg";

        public MainWindow()
        {
            InitializeComponent();

            imgTresor.ImageSource = new BitmapImage(new Uri(rutaTresor, UriKind.Relative));
            imgRobot.ImageSource = new BitmapImage(new Uri(rutaRobot, UriKind.Relative));

            //Determinar la posicio inicial dels elements
            posicioRobot = new Point(random.Next(0, (int)canvasRobot.Width / TAMANY_ELEMENTS)*TAMANY_ELEMENTS,
                random.Next(0, (int)canvasRobot.Height / TAMANY_ELEMENTS) * TAMANY_ELEMENTS);
            posicioTresor = new Point(random.Next(0, (int)canvasRobot.Width / TAMANY_ELEMENTS) * TAMANY_ELEMENTS,
                random.Next(0, (int)canvasRobot.Height / TAMANY_ELEMENTS) * TAMANY_ELEMENTS);

            //Pintar els elements
            pintaTresor(0);
            pintaRobot(1);
        }

        private void pintaRobot(int index)
        {
            Ellipse robot = new Ellipse();
            robot.Width = TAMANY_ELEMENTS;
            robot.Height = TAMANY_ELEMENTS;
            robot.Fill = imgRobot;

            Canvas.SetTop(robot, posicioRobot.Y);
            Canvas.SetLeft(robot, posicioRobot.X);
            canvasRobot.Children.Insert(index, robot);

        }

        private void pintaTresor(int index)
        {
            Ellipse tresor = new Ellipse();
            tresor.Width = TAMANY_ELEMENTS;
            tresor.Height = TAMANY_ELEMENTS;
            tresor.Fill = imgTresor;

            Canvas.SetTop(tresor, posicioTresor.Y);
            Canvas.SetLeft(tresor, posicioTresor.X);
            canvasRobot.Children.Insert(index, tresor);
        }
    }
}
