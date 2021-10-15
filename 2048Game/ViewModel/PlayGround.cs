using _2048Game.Model;
using _2048Game.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _2048Game.ViewModel
{
    class PlayGround
    {
        private Tile[,] tiles;

        private Random rand;
        private MainWindow mainWindow;

        private bool GameFinish;

        public int Score { get; set; }
        public int MaxScore { get; set; }

        public PlayGround(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            rand = new Random();
            tiles = new Tile[4, 4];
        }

        public void ReloadGame()
        {
            CreateTiles();

            Score = 0;

            DrawGame();
        }

        private void CreateTiles()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == 1 && j == 0)
                    {
                        tiles[i, j] = new Tile(2);
                    }
                    else if (i == 1 && j == 2)
                    {
                        tiles[i, j] = new Tile(4);
                    }
                    else
                    {
                        tiles[i, j] = new Tile();
                    }
                }
            }
        }

        private void DoGameOver()
        {
            DrawGame();

            GameFinish = true;

            var result = MessageBox.Show("Вы проиграли!\nЖелаете начать игру заново?", "Game Over", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
                ReloadGame();
            else
                mainWindow.Close();
        }

        private void DoWin()
        {
            DrawGame();

            GameFinish = true;

            var result = MessageBox.Show("Вы выйграли!\nЖелаете начать игру заново?", "Win!", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
                ReloadGame();
            else
                mainWindow.Close();
        }

        private void DrawGame()
        {
            mainWindow.textBlockHighScore.Text = MaxScore.ToString();
            mainWindow.textBlockScore.Text = Score.ToString();

            for (int col = 0; col < 4; col++)
            {
                for (int row = 0; row < 4; row++)
                {
                    if (tiles[col, row].Value > 0)
                    {
                        Border border = mainWindow.FindName($"Tile{col}{row}") as Border;
                        TextBlock textBlock = border.FindName($"Text{col}{row}") as TextBlock;

                        border.Background = tiles[col, row].TileColor;
                        textBlock.Text = tiles[col, row].Value.ToString();
                        textBlock.Foreground = tiles[col, row].TextColor;
                    }
                    else
                    {
                        Border border = mainWindow.FindName($"Tile{col}{row}") as Border;
                        TextBlock textBlock = border.FindName($"Text{col}{row}") as TextBlock;

                        border.Background = tiles[col, row].TileColor;
                        textBlock.Text = "";
                        textBlock.Foreground = tiles[col, row].TextColor;
                    }
                }
            }
        }

        private void SpawnTile()
        {
            if (IsGameOver())
                DoGameOver();

            int col;
            int row;

            int count = 0;

            do
            {
                count++;
                col = rand.Next(0, 4);
                row = rand.Next(0, 4);
            } while (tiles[col, row].Value != 0 && count < 15);

            if (count < 15)
            {
                if (rand.Next(1, 100) % 10 == 0)
                    tiles[col, row].Value = 4;
                else
                    tiles[col, row].Value = 2;
            }

            if (Score > MaxScore)
                MaxScore = Score;

            DrawGame();
        }

        private bool IsGameOver()
        {

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i - 1 >= 0)
                    {
                        if (tiles[i - 1, j].Value == tiles[i, j].Value)
                        {
                            return false;
                        }
                    }

                    if (i + 1 < 4)
                    {
                        if (tiles[i + 1, j].Value == tiles[i, j].Value)
                        {
                            return false;
                        }
                    }

                    if (j - 1 >= 0)
                    {
                        if (tiles[i, j - 1].Value == tiles[i, j].Value)
                        {
                            return false;
                        }
                    }

                    if (j + 1 < 4)
                    {
                        if (tiles[i, j + 1].Value == tiles[i, j].Value)
                        {
                            return false;
                        }
                    }

                    if (tiles[i, j].Value == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void MoveRight()
        {
            if (GameFinish)
                return;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 3; j >= 0; j--)
                {
                    for (int k = j - 1; k >= 0; k--)
                    {
                        if (tiles[i, k].Value == 0)
                        {
                            continue;
                        }
                        else if (tiles[i, k].Value == tiles[i, j].Value)
                        {
                            tiles[i, j].Value *= 2;
                            tiles[i, k].Value = 0;
                            Score += tiles[i, j].Value;

                            if (tiles[i, j].Value == 2048)
                                DoWin();

                            break;
                        }
                        else
                        {
                            if (tiles[i, j].Value == 0 && tiles[i, k].Value != 0)
                            {
                                tiles[i, j].Value = tiles[i, k].Value;
                                tiles[i, k].Value = 0;
                                j++;
                                break;
                            }
                            else if (tiles[i, j].Value != 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            SpawnTile();
        }

        public void MoveLeft()
        {
            if (GameFinish)
                return;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = j + 1; k < 4; k++)
                    {
                        if (tiles[i, k].Value == 0)
                        {
                            continue;
                        }
                        else if (tiles[i, k].Value == tiles[i, j].Value)
                        {
                            tiles[i, j].Value *= 2;
                            tiles[i, k].Value = 0;
                            Score += tiles[i, j].Value;

                            if (tiles[i, j].Value == 2048)
                                DoWin();

                            break;
                        }
                        else
                        {
                            if (tiles[i, j].Value == 0 && tiles[i, k].Value != 0)
                            {
                                tiles[i, j].Value = tiles[i, k].Value;
                                tiles[i, k].Value = 0;
                                j--;
                                break;
                            }
                            else if (tiles[i, j].Value != 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            SpawnTile();
        }

        public void MoveUp()
        {
            if (GameFinish)
                return;

            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int k = i + 1; k < 4; k++)
                    {
                        if (tiles[k, j].Value == 0)
                        {
                            continue;
                        }
                        else if (tiles[k, j].Value == tiles[i, j].Value)
                        {
                            tiles[i, j].Value *= 2;
                            tiles[k, j].Value = 0;
                            Score += tiles[i, j].Value;

                            if (tiles[i, j].Value == 2048)
                                DoWin();

                            break;
                        }
                        else
                        {
                            if (tiles[i, j].Value == 0 && tiles[k, j].Value != 0)
                            {
                                tiles[i, j].Value = tiles[k, j].Value;
                                tiles[k, j].Value = 0;
                                i--;
                                break;
                            }
                            else if (tiles[i, j].Value != 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            SpawnTile();
        }

        public void MoveDown()
        {
            if (GameFinish)
                return;

            for (int j = 0; j < 4; j++)
            {
                for (int i = 3; i >= 0; i--)
                {
                    for (int k = i - 1; k >= 0; k--)
                    {
                        if (tiles[k, j].Value == 0)
                        {
                            continue;
                        }
                        else if (tiles[k, j].Value == tiles[i, j].Value)
                        {
                            tiles[i, j].Value *= 2;
                            tiles[k, j].Value = 0;
                            Score += tiles[i, j].Value;

                            if (tiles[i, j].Value == 2048)
                                DoWin();

                            break;
                        }
                        else
                        {
                            if (tiles[i, j].Value == 0 && tiles[k, j].Value != 0)
                            {
                                tiles[i, j].Value = tiles[k, j].Value;
                                tiles[k, j].Value = 0;
                                i++;
                                break;
                            }
                            else if (tiles[i, j].Value != 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            SpawnTile();
        }
    }
}
