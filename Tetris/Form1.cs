using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        private Game game;
        public Form1()
        {
            InitializeComponent();

            game = new Game();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            game.Draw(e.Graphics);
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            game.Update();
            pictureBox1.Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            game.MovementFigure(e.KeyCode);
            label1.Text = "Очки:" + game.Score;
            pictureBox1.Refresh();
        }

    }
}
