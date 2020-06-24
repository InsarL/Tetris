using System;
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
            game.Defeat += OnDefeat;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            game.Draw(e.Graphics);
            label1.Text = "Очки:" + game.Score;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            game.Update();
            pictureBox1.Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            game.MoveFigure(e.KeyCode);
            pictureBox1.Refresh();
        }

        private void OnDefeat()
        {
            timer1.Stop();
            pictureBox1.Refresh();
            MessageBox.Show("Game Over");
            timer1.Start();
        }
    }
}
