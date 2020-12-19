using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvaders2020
{
    public partial class Game : Form
    {
        private Spaceship spaceship = null;
        private List<Enemy> enemies = new List<Enemy>();
        private Timer mainTimer = null;

        public Game()
        {
            InitializeComponent();
            InitializeGame();
            InitializeMainTimer();
        }

        private void InitializeGame()
        {
            this.KeyDown += Game_KeyDown;
            this.BackColor = Color.Black;
            AddSpaceshipToGame();
            AddEnemyToGame(3, 8);
        }

        private void AddSpaceshipToGame()
        {
            spaceship = new Spaceship(this);
            spaceship.FireCooldown = 450;
            this.Controls.Add(spaceship);
            spaceship.Left = 150;
            spaceship.Top = ClientRectangle.Height - spaceship.Height;
        }

        private void AddEnemyToGame(int rows, int columns)
        {
            Enemy enemy = null;

            for (int rowCounter = 0; rowCounter < rows; rowCounter++)
            {
                for (int colCounter = 0; colCounter < columns; colCounter++)
                {
                    enemy = new Enemy();
                    enemy.Left = 20 + 60 * colCounter;
                    enemy.Top = 20 + 60 * rowCounter;
                    this.Controls.Add(enemy);
                    enemies.Add(enemy);
                }
            }
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                spaceship.Fire();
            }
            else if (e.KeyCode == Keys.A)
            {
                spaceship.MoveLeft();
            }
            else if (e.KeyCode == Keys.D)
            {
                spaceship.MoveRight();
            }
            else if (e.KeyCode == Keys.S)
            {
                spaceship.MoveStop();
            }
        }

        private void InitializeMainTimer()
        {
            mainTimer = new Timer();
            mainTimer.Interval = 10;
            mainTimer.Tick += MainTimer_Tick;
            mainTimer.Start();
        }
        private void MainTimer_Tick(object sender, EventArgs e)
        {
            CheckBulletEnemyCollision();
        }

        private void CheckBulletEnemyCollision()
        {
            foreach (var bullet in spaceship.bullets)
            {
                foreach (var enemy in enemies)
                {
                    if (bullet.Bounds.IntersectsWith(enemy.Bounds))
                    {
                        enemy.Dispose();
                        bullet.Dispose();
                    }
                }
            }
        }
    }
}