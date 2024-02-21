using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace JuegoCuadrados
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private Button botonDiferente;
        private int tiempoLimite = 2500; // Tiempo límite predeterminado en milisegundos
        private readonly Random rnd = new Random();
        private bool resul = false;
        private readonly byte rojo1 = 0x63;
        private readonly byte verde1 = 0x2B;
        private readonly byte azul1 = 0x30;

        private readonly byte rojo2 = 0x22;
        private readonly byte verde2 = 0x74;
        private readonly byte azul2 = 0xA5;

        public MainWindow()
        {
            InitializeComponent();
            InicializarJuego();
        }

        private void InicializarJuego()
        {
            // Elegir un botón al azar para ser el diferente
            var botones = grid.Children.OfType<Button>().ToList();
            botonDiferente = botones[rnd.Next(0, botones.Count)];

            Color miColorPersonalizado = Color.FromRgb(rojo1, verde1, azul1);

            SolidColorBrush miPincelPersonalizado = new SolidColorBrush(miColorPersonalizado);
            botonDiferente.Background = miPincelPersonalizado; // Establecer el color del botón diferente

            // Asociar el evento MouseEnter a todos los botones
            foreach (var control in grid.Children)
            {
                if (control is Button boton)
                {
                    boton.MouseEnter += Button_MouseEnter;
                    boton.Click += Button_Click;
                }
            }            
        }

        private void TimeLimitComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)timeLimitComboBox.SelectedItem;
            if (selectedItem != null)
            {
                string content = selectedItem.Content.ToString();
                string mododeJuego = content.Split(' ')[0]; // Obtener el número del tiempo seleccionado
                if (mododeJuego.Equals("Facil"))
                {
                    tiempoLimite = 2500;
                }
                else if (mododeJuego.Equals("Normal"))
                {
                    tiempoLimite = 1500;
                }else if (mododeJuego.Equals("Dificil"))
                {
                    tiempoLimite = 500;
                }
            }
        }

        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button botonClickeado = sender as Button;
            if (botonClickeado == botonDiferente)
            {
                if (timer == null || !timer.IsEnabled) // Verificar si el timer no existe o no está activo
                {
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromMilliseconds(tiempoLimite);
                    timer.Tick += Timer_Tick;
                    timer.Start();
                }
            }
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            if (resul==false)
            {
                MessageBox.Show("Has perdido");
                ReiniciarJuego();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button botonClickeado = sender as Button;
            if (botonClickeado == botonDiferente) {
                if (timer != null)
                {
                    timer.Stop();
                }
                MessageBox.Show("Has ganado");
                resul = true;
                ReiniciarJuego();
            }
        }



        private void ReiniciarJuego()
        {
            resul = false;
            // Restablecer los colores de los botones
            foreach (var control in grid.Children)
            {
                if (control is Button boton)
                {
                    Color miColorPersonalizado1 = Color.FromRgb(rojo2, verde2, azul2);

                    SolidColorBrush miPincelPersonalizado1 = new SolidColorBrush(miColorPersonalizado1);
                    boton.Background = miPincelPersonalizado1; // Establecer el color del botón diferente}
                }
            }

            // Elegir un nuevo botón diferente
            var botones = grid.Children.OfType<Button>().ToList();
            botonDiferente = botones[rnd.Next(0, botones.Count)];
            Color miColorPersonalizado = Color.FromRgb(rojo1, verde1, azul1);

            SolidColorBrush miPincelPersonalizado = new SolidColorBrush(miColorPersonalizado);
            botonDiferente.Background = miPincelPersonalizado; // Establecer el color del botón diferente}
        }
    }
}