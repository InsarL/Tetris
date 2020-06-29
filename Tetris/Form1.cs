using System;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Tetris : Form
    {
        private Game game;
        public Tetris()
        {
            InitializeComponent();
            game = new Game();
            game.Defeat += OnDefeat;
            game.Restart();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            game.Draw(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            game.Update();
            labelScore.Text = "Счёт:" + game.Score;
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
            game.Restart();
            timer1.Start();
        }
    }
}
