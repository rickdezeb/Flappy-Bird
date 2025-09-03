using System;

namespace FlappyBirdWindowsForms
{
    public class GameLogic
    {
        public int PipeSpeed { get; private set; } = 8;
        public int Score { get; private set; } = 0;
        public int HighScore { get; private set; } = 0;
        public float BirdVelocity { get; set; } = 0;
        public float BirdGravity { get; } = 0.5f;
        public float BirdJumpStrength { get; } = -8f;
        public float BirdRotation { get; private set; } = 0f;

        public void UpdateBird()
        {
            BirdVelocity += BirdGravity;

            if (BirdVelocity < 0)
                BirdRotation = -30f;
            else if (BirdVelocity > 8)
                BirdRotation = 45f;
            else
                BirdRotation = BirdVelocity * 5;
        }

        public void Jump()
        {
            BirdVelocity = BirdJumpStrength;
        }

        public void Reset()
        {
            Score = 0;
            BirdVelocity = 0;
            BirdRotation = 0;
            PipeSpeed = 8;
        }

        public void PipePassed()
        {
            Score++;
            PipeSpeed = 8 + (Score / 5);
        }

        public void UpdateHighScore()
        {
            if (Score > HighScore)
                HighScore = Score;
        }
    }
}