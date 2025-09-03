using System;
using System.Drawing;
using System.Windows.Forms;

namespace FlappyBirdWindowsForms
{
    public partial class Game : Form
    {
        private const int PipeBottomResetX = 500;
        private const int PipeTopResetX = 800;
        private const int BirdStartTop = 126;
        private Image flappyBirdOriginalImage;
        private GameLogic logic;

        public Game()
        {
            InitializeComponent();
            flappyBirdOriginalImage = flappyBird.Image;
            logic = new GameLogic();
            this.KeyDown += new KeyEventHandler(gamekeyisdown);
            this.KeyPreview = true;
            resetGame();
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            logic.UpdateBird();
            flappyBird.Top += (int)logic.BirdVelocity;
            flappyBird.Image = RotateImage(flappyBirdOriginalImage, logic.BirdRotation);

            pipeBottom.Left -= logic.PipeSpeed;
            pipeTop.Left -= logic.PipeSpeed;
            scoreText.Text = $"Score: {logic.Score}";
            ground.Left -= logic.PipeSpeed;

            if (ground.Left <= -ground.Width + this.ClientSize.Width)
                ground.Left = 0;

            if (pipeBottom.Left < -150)
            {
                pipeBottom.Left = PipeBottomResetX;
                logic.PipePassed();
            }
            if (pipeTop.Left < -180)
            {
                pipeTop.Left = PipeTopResetX;
                logic.PipePassed();
            }
            if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                flappyBird.Bounds.IntersectsWith(ground.Bounds) ||
                flappyBird.Top < -25)
            {
                endGame();
            }
        }

        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                logic.Jump();
        }

        private void endGame()
        {
            gameTimer.Stop();
            gameOverText.Visible = true;
            restartGameButton.Visible = true;
            logic.UpdateHighScore();
            highScoreText.Text = $"High Score: {logic.HighScore}";
        }

        private Image RotateImage(Image image, float angle)
        {
            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);
            using (Graphics g = Graphics.FromImage(rotatedBmp))
            {
                g.TranslateTransform((float)image.Width / 2, (float)image.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-(float)image.Width / 2, -(float)image.Height / 2);
                g.DrawImage(image, new Point(0, 0));
            }
            return rotatedBmp;
        }

        private void resetGame()
        {
            logic.Reset();
            pipeBottom.Left = PipeBottomResetX;
            pipeTop.Left = PipeTopResetX;
            flappyBird.Top = BirdStartTop;
            flappyBird.Image = flappyBirdOriginalImage;
            gameOverText.Visible = false;
            restartGameButton.Visible = false;
            gameTimer.Start();
        }

        private void restartGameButton_Click(object sender, EventArgs e)
        {
            if (!gameTimer.Enabled)
                resetGame();
        }
    }
}