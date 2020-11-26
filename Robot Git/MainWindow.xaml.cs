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
using System.Windows.Threading;

namespace Robot_Git
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Direccions
        private enum Direccions
        {
            NORD = 0,
            SUD = 1,
            EST = 2,
            OEST = 3
        };
        private int direccio = (int)Direccions.NORD;

        //Parametres
        private const int TAMANY_ELEMENTS = 50;

        //Random
        private Random random = new Random();

        //Posicions
        private Point posicioRobot;
        private Point posicioTresor;

        //Imatge tresor
        private ImageBrush imgTresor = new ImageBrush();
        private string rutaTresor = "tresor.jpg";

        //Imatge robot
        private ImageBrush imgRobot = new ImageBrush();
        private string rutaRobot = "robot.jpg";

        public MainWindow()
        {
            InitializeComponent();
            //Temporitzador
            DispatcherTimer temporitzador = new DispatcherTimer();
            temporitzador.Tick += new EventHandler(temporitzador_Tick);
            temporitzador.Interval = new TimeSpan(10000);


            //Indicar la imatge als ImageBrush
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

            //Iniciar temporitzador
            temporitzador.Start();
        }

        private void temporitzador_Tick(object sender, EventArgs e)
        {
            int dau = random.Next(0, 3);
            if(dau > 0)
            {

            }
            if(dau > 1)
            {
                switch (direccio)
                {
                    case (int)Direccions.NORD:
                        posicioRobot.Y -= TAMANY_ELEMENTS;
                        pintaRobot(1);
                        break;
                    case (int)Direccions.SUD:
                        posicioRobot.Y += TAMANY_ELEMENTS;
                        pintaRobot(1);
                        break;
                    case (int)Direccions.OEST:
                        posicioRobot.X -= TAMANY_ELEMENTS;
                        pintaRobot(1);
                        break;
                    case (int)Direccions.EST:
                        posicioRobot.X += TAMANY_ELEMENTS;
                        pintaRobot(1);
                        break;
                }
            }
        }

        private void pintaRobot(int index)
        {
            //Crear robot
            Ellipse robot = new Ellipse();
            robot.Width = TAMANY_ELEMENTS;
            robot.Height = TAMANY_ELEMENTS;
            robot.Fill = imgRobot;

            //Pintar robot
            Canvas.SetTop(robot, posicioRobot.Y);
            Canvas.SetLeft(robot, posicioRobot.X);
            canvasRobot.Children.Insert(index, robot);

            if (canvasRobot.Children.Count > 2) canvasRobot.Children.RemoveAt(2);
        }

        private void pintaTresor(int index)
        {
            //Crear tresor
            Ellipse tresor = new Ellipse();
            tresor.Width = TAMANY_ELEMENTS;
            tresor.Height = TAMANY_ELEMENTS;
            tresor.Fill = imgTresor;

            //Pintar tresor
            Canvas.SetTop(tresor, posicioTresor.Y);
            Canvas.SetLeft(tresor, posicioTresor.X);
            canvasRobot.Children.Insert(index, tresor);
        }
    }
}
