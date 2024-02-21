using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JuegoCuadrados
{
    public partial class MainWindow : Window
    {
        private Button botonDiferente;
        private readonly int tiempoLimite = 3000; // Tiempo límite predeterminado en milisegundos
        private readonly Random rnd = new Random();
        private Boolean resul = false;
        byte rojo1 = 0x63;
        byte verde1 = 0x2B;
        byte azul1 = 0x30;

        byte rojo2 = 0x22;
        byte verde2 = 0x74;
        byte azul2 = 0xA5;

        public MainWindow()
        {
            InitializeComponent();
            InicializarJuego();
        }

        private void InicializarJuego()
        {
            // Elegir un botón al azar para ser el diferente
            botonDiferente = (Button)grid.Children[rnd.Next(0, grid.Children.Count)];

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

        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button botonClickeado = sender as Button;
            if (botonClickeado == botonDiferente)
            {
                Thread.Sleep(tiempoLimite);
                if (resul == true) {
                } else {
                    MessageBox.Show("Has perdido");
                  
                }
                ReiniciarJuego();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button botonClickeado = sender as Button;
            if (botonClickeado == botonDiferente) {
                MessageBox.Show("Has ganado");
                resul = true;
            }
        }


        private void ReiniciarJuego()
        {
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
            botonDiferente = (Button)grid.Children[rnd.Next(0, grid.Children.Count)];
            Color miColorPersonalizado = Color.FromRgb(rojo1, verde1, azul1);

            SolidColorBrush miPincelPersonalizado = new SolidColorBrush(miColorPersonalizado);
            botonDiferente.Background = miPincelPersonalizado; // Establecer el color del botón diferente        }
        }

       
    }
}