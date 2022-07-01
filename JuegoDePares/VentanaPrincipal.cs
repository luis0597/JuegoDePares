using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JuegoDePares
{
    public partial class VentanaPrincipal : Form
    {
        Random random = new Random();

        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z",
            "U", "U", "G", "G"
        };

        Label primerClick = null, segundoClick = null;

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            primerClick.ForeColor = primerClick.BackColor;
            segundoClick.ForeColor = segundoClick.BackColor;

            primerClick = null;
            segundoClick = null;
        }

        private void asignarIconos()
        {
            //Llena la tabla con elementos de la lista al azar
            foreach (Control icono in tabla.Controls)
            {
                Label iconoLabel = icono as Label;
                if (iconoLabel != null)
                {
                    int randomNum = random.Next(icons.Count);
                    iconoLabel.Text = icons[randomNum];
                    icons.RemoveAt(randomNum);
                }
            }
        }

        private void ganar()
        {
            //Recorre la tabla hasta encontrar un icono oculto
            foreach (Control icono in tabla.Controls)
            {
                Label iconLabel = icono as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            //Si no queda ningun icono oculto, el juego termina
            MessageBox.Show("¡Completaste el juego!", "Felicidades");
            Close();
        }

        public VentanaPrincipal()
        {
            InitializeComponent();
            asignarIconos();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //Ignora clicks si el timer esta corriendo
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                //Si el icono ya se esta mostrando no hace nada
                if (clickedLabel.ForeColor == Color.Black)
                    return;
                //clickedLabel.ForeColor = Color.Black;

                //Muestra el primer icono
                if (primerClick == null)
                {
                    primerClick = clickedLabel;
                    primerClick.ForeColor = Color.Black;
                    return;
                }

                //Muestra el segundo icono
                segundoClick = clickedLabel;
                segundoClick.ForeColor = Color.Black;

                //Comprueba si el juego termina
                ganar();

                //Comprueba si son pares
                if (primerClick.Text == segundoClick.Text)
                {
                    primerClick = null;
                    segundoClick = null;
                    return;
                }

                //Si no son pares los oculta
                timer1.Start();
                  
            }
        }
    }
}
