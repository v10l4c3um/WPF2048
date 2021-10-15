using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _2048Game.Model
{
    class Tile
    {
        public Tile(int value = 0)
        {
            Value = value;
        }

        public int Value { get; set; }

        public SolidColorBrush TextColor
        {
            get
            {
                return TileBrushes.GetTextColor(this);
            }
        }

        public SolidColorBrush TileColor
        {
            get
            {
                return TileBrushes.GetTileColor(this);
            }
        }
    }
}
