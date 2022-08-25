using System.Drawing;

namespace Automata {
   public static class ConwaysLife {
      private static bool _running;
      public class Cell {
         public Cell(Point loc, bool alive = false) {
            Location = loc;
            Alive = alive;
            Neighborhood = 0;
         }
         public bool Alive { get; set; }
         public Point Location { get; private set; }
         public int Neighborhood { get; set; }
         public int X {
            get { return Location.X; }
         }
         public int Y {
            get { return Location.Y; }
         }
      }
      public class CellBlock {
         public CellBlock(Point loc, Size size) {
            Cells = new Cell[size.Width, size.Height];
            Size = size;
            Location = loc;
         }
         public Point Location { get; private set; }
         public Size Size { get; private set; }
         private Cell[,] Cells;
      }
   }
}