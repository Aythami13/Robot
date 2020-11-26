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
        private string rutaTresor = "tresor23.jpg";

        //Imatge robot
        private ImageBrush imgRobot = new ImageBrush();
        private string rutaRobot = "robot.jpg";


        //Imatge flecha
        private ImageBrush imgFlecha = new ImageBrush();


        //Numero de moviments
        private int nMoviments = 0;

        public MainWindow()
        {
            InitializeComponent();
            //Temporitzador
            DispatcherTimer temporitzador = new DispatcherTimer();
            temporitzador.Tick += new EventHandler(temporitzador_Tick);
            temporitzador.Interval = new TimeSpan(100000);



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
            pintaFlecha(2);

            //Iniciar temporitzador
            temporitzador.Start();
        }

        private void pintaFlecha(int index)
        {
            //Pintar flecha
            Ellipse flecha = new Ellipse();
            flecha.Width = TAMANY_ELEMENTS/3;
            flecha.Height = TAMANY_ELEMENTS/3;
            flecha.Fill = imgFlecha;

            Canvas.SetTop(flecha, posicioRobot.Y + TAMANY_ELEMENTS/4);
            Canvas.SetLeft(flecha, posicioRobot.X + TAMANY_ELEMENTS / 4);
            canvasRobot.Children.Insert(index, flecha);

            //Direccio de la flecha
            if (direccio == (int)Direccions.NORD) imgFlecha.ImageSource = new BitmapImage(new Uri("flecha_nord.png", UriKind.Relative));
            else if (direccio == (int)Direccions.EST) imgFlecha.ImageSource = new BitmapImage(new Uri("flecha_est.png", UriKind.Relative));
            else if (direccio == (int)Direccions.OEST) imgFlecha.ImageSource = new BitmapImage(new Uri("flecha_oest.png", UriKind.Relative));
            else imgFlecha.ImageSource = new BitmapImage(new Uri("flecha_sud.png", UriKind.Relative));


            //Eliminar l'anterior flecha
            if (canvasRobot.Children.Count > 3)
            {
                canvasRobot.Children.RemoveAt(3);
            }
        }

        private void temporitzador_Tick(object sender, EventArgs e)
        {
            GiraRobot();
            MoureRobot();
            Guanya();
        }

        private void Guanya()
        {
            if(posicioRobot.X == posicioTresor.X && posicioRobot.Y == posicioTresor.Y)
            {
                MessageBox.Show("Has guanyat en " + nMoviments + " moviments!");
                this.Close();
            }
            
        }

        private void GiraRobot()
        {
            int dau = random.Next(0, 3);
            if(dau < 2)
            {
                switch (direccio)
                {
                    case (int)Direccions.NORD:
                        if (dau == 0) direccio = (int)Direccions.OEST;
                        else if (dau == 1) direccio = (int)Direccions.EST;
                        break;
                    case (int)Direccions.SUD:
                        if (dau == 0) direccio = (int)Direccions.EST;
                        else if (dau == 1) direccio = (int)Direccions.OEST;
                        break;
                    case (int)Direccions.EST:
                        if (dau == 0) direccio = (int)Direccions.NORD;
                        else if (dau == 1) direccio = (int)Direccions.SUD;
                        break;
                    case (int)Direccions.OEST:
                        if (dau == 0) direccio = (int)Direccions.SUD;
                        else if (dau == 1) direccio = (int)Direccions.NORD;
                        break;
                }
                nMoviments++;
            }
            
        }

        private void MoureRobot()
        {
            int dau = random.Next(0, 4);
            if (dau == 0)
            {
                switch (direccio)
                {
                    case (int)Direccions.NORD:
                        posicioRobot.Y -= TAMANY_ELEMENTS;
                        break;
                    case (int)Direccions.SUD:
                        posicioRobot.Y += TAMANY_ELEMENTS;
                        break;
                    case (int)Direccions.OEST:
                        posicioRobot.X -= TAMANY_ELEMENTS;
                        break;
                    case (int)Direccions.EST:
                        posicioRobot.X += TAMANY_ELEMENTS;
                        break;
                }            
                nMoviments++;
            }
            if (posicioRobot.X < 0 && direccio == (int)Direccions.OEST) posicioRobot.X = 500 - TAMANY_ELEMENTS;
            else if (posicioRobot.X > 500 - TAMANY_ELEMENTS && direccio == (int)Direccions.EST) posicioRobot.X = 0;
            else if (posicioRobot.Y < 0 && direccio == (int)Direccions.NORD) posicioRobot.Y = 500 - TAMANY_ELEMENTS;
            else if (posicioRobot.Y > 500 - TAMANY_ELEMENTS && direccio == (int)Direccions.SUD) posicioRobot.Y = 0;
            pintaRobot(1);
            pintaFlecha(2);
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

            //Eliminar l'anterior robot
            if (canvasRobot.Children.Count > 3)
            {
                canvasRobot.Children.RemoveAt(2);
            }
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
