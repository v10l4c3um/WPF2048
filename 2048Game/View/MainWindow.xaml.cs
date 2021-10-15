using _2048Game.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace _2048Game.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PlayGround playGround;

        public MainWindow()
        {
            InitializeComponent();

            playGround = new PlayGround(this);

            playGround.ReloadGame();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    playGround.MoveLeft();
                    break;
                case Key.Right:
                    playGround.MoveRight();
                    break;
                case Key.Up:
                    playGround.MoveUp();
                    break;
                case Key.Down:
                    playGround.MoveDown();
                    break;
                default:
                    break;
            }
        }
    }
}
