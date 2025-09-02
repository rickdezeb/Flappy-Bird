using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBirdWindowsForms
{
    public partial class game : Form
    {
        int pipeSpeed = 8;
        int gravity = 7;
        int score = 0;
        int highScore = 0;
        public game()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(gamekeyisdown);
            this.KeyUp += new KeyEventHandler(gamekeyisup);
            this.KeyPreview = true;
            resetGame();
        }
        private void gameTimerEvent(object sender, EventArgs e)
        {
            flappyBird.Top += gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            scoreText.Text = "Score: " + score;
            ground.Left -= pipeSpeed;

            if (ground.Left <= -ground.Width + this.ClientSize.Width)
            {
                ground.Left = 0;
            }
            if (pipeBottom.Left < -150)
            {
                pipeBottom.Left = 500;
                score++;
            }
            if (pipeTop.Left < -180)
            {
                pipeTop.Left = 600;
                score++;
            }
            if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                flappyBird.Bounds.IntersectsWith(ground.Bounds) ||
                flappyBird.Top < -25)
            {
                endGame();
            }

            pipeSpeed = 8 + (score / 5);
        }

        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                gravity = -7;
            }

        }

        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 7;
            }
        }
        private void endGame()
        {
            gameTimer.Stop();
            gameOverText.Visible = true;
            restartGameButton.Visible = true;

            if (score > highScore)
            {
                highScore = score;
                highScoreText.Text = "High Score: " + highScore;
            }
        }
        private void resetGame()
        {
            score = 0;
            pipeBottom.Left = 500;
            pipeTop.Left = 800;
            flappyBird.Top = 126;
            gameOverText.Visible = false;
            restartGameButton.Visible = false;
            gameTimer.Start();
        }
        private void restartGameButton_Click(object sender, EventArgs e)
        {
            if (gameTimer.Enabled == false)
                resetGame();
        }
    }
}
