using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CyclicalAutomata {
   public partial class CyclicalAutomata {
      private class CellGrid {
         public CellGrid(Size size) {
            _size = size;
            _bmp = new Bitmap(_size.Width, _size.Height);
            CreateCellArray();
            Reset();
         }
         /// <summary>
         ///    Gets the Cell specified at (x, y).
         /// </summary>
         public Cell this[int x, int y] {
            get { return _cells[y * _size.Width + x]; }
         }
         /// <summary>
         ///    Gets or sets a value that determines how a cell's age is represented in the bitmap image.
         /// </summary>
         public AgeEffects AgeEffect { get; set; }
         /// <summary>
         ///    Sets the speed at which cells age.  The default is 1.
         /// </summary>
         public int AgeSpeed {
            get { return _ageSpeed; }
            set {
               if (value < 1) value = 1;
               _ageSpeed = value;
            }
         }
         private Bitmap _bmp;
         /// <summary>
         ///    Gets an image representation of the grid for display.
         /// </summary>
         public Bitmap Bitmap {
            get {
               lock (_lockobj) {
                  for (int y = 0; y < _size.Height; ++y) {
                     for (int x = 0; x < _size.Width; ++x) {
                        var cell = this[x, y];
                        if (cell.LastPaintedProps == cell.Properties) continue;
                        cell.LastPaintedProps = new Cell.Props(cell.Properties);
                        var color = _colors[cell.State];
                        var ageAmount = cell.Age * AgeSpeed;
                        if (ageAmount > 0) {
                           switch (AgeEffect) {
                              case AgeEffects.Gray:
                                 color = Color.FromArgb(NormalizeTowardValue(color.R, 128, ageAmount),
                                                        NormalizeTowardValue(color.G, 128, ageAmount),
                                                        NormalizeTowardValue(color.B, 128, ageAmount));
                                 break;
                              case AgeEffects.Black:
                                 color = Color.FromArgb(NormalizeTowardValue(color.R, 0, ageAmount),
                                                        NormalizeTowardValue(color.G, 0, ageAmount),
                                                        NormalizeTowardValue(color.B, 0, ageAmount));
                                 break;
                              case AgeEffects.White:
                                 color = Color.FromArgb(NormalizeTowardValue(color.R, 255, ageAmount),
                                                        NormalizeTowardValue(color.G, 255, ageAmount),
                                                        NormalizeTowardValue(color.B, 255, ageAmount));
                                 break;
                           }
                        }
                        _bmp.SetPixel(x, y, color);
                     }
                  }
                  return _bmp;
               }
            }
         }
         public Point Favor { get; set; }
         public string InitFilename { get; set; }
         public InitialCond InitialCond { get; set; }
         /// <summary>
         ///    Gets or sets the number of states.
         /// </summary>
         public int NumStates {
            get { return _numStates; }
            set {
               lock (_lockobj) {
                  if (value < 2) value = 2;
                  else if (value > MAX_STATES) value = MAX_STATES;
                  if (value == _numStates) return;
                  if (value < _numStates) {
                     foreach (var cell in _cells) if (cell.State >= value) cell.State = value - 1;
                  }
                  _numStates = value;
                  int inc = MAX_STATES / _numStates;
                  _colors = new Color[_numStates];
                  for (int i = 0, c = 0; i < MAX_STATES && c < _numStates; i += inc, ++c) {
                     _colors[c] = GetColorByIndex(i);
                  }
               }
            }
         }
         /// <summary>
         ///    Gets or sets the size of the grid.
         /// </summary>
         public Size Size {
            get { return _size; }
            set {
               if (value == _size) return;
               lock (_lockobj) {
                  var oldrect = new Rectangle(0, 0, _size.Width, _size.Height);
                  var newrect = new Rectangle(0, 0, value.Width, value.Height);
                  var intersect = Rectangle.Intersect(oldrect, newrect);
                  var newcells = new Cell[value.Width * value.Height];
                  for (int i = 0; i < newcells.Length; ++i) newcells[i] = new Cell(i / _size.Width, i % _size.Width);
                  for (int y = 0; y < intersect.Height; ++y) {
                     for (int x = 0; x < intersect.Width; ++x) {
                        newcells[y * value.Width + x] = _cells[y * _size.Width + x];
                     }
                  }
                  _cells = newcells;
                  _size = value;
                  _bmp = new Bitmap(_size.Width, _size.Height);
               }
            }
         }
         public void FinishIteration() {
            lock (_lockobj) {
               foreach (Cell cell in _cells) cell.Iterate();
            }
         }
         public void ForEach(Action<Cell> action) {
            lock (_lockobj) {
               for (int y = 0; y < _size.Height; ++y) {
                  for (int x = 0; x < _size.Width; ++x) {
                     action(_cells[y * _size.Width + x]);
                  }
               }
            }
         }
         public void InitializeFromFile(string fileName) {
            if (fileName == null) return;
            InitFilename = fileName;
            using (var bmp = System.Drawing.Image.FromFile(fileName)) {
               using (var scaled = new Bitmap(bmp, _size)) {
                  List<Color> colorlist = new List<Color>();
                  // Get a list of all the unique colors in the scaled image.
                  for (int y = 0; y < _size.Height; ++y) {
                     for (int x = 0; x < _size.Width; ++x) {
                        var color = scaled.GetPixel(x, y);
                        if (colorlist.All(c => ColorDist(c, color) > 1)) colorlist.Add(color);
                     }
                  }
                  // Reduce the color list down to the number of states.
                  if (colorlist.Count < _numStates) {
                     _colors = new Color[_numStates];
                     for (int i = 0; i < _numStates; ++i) _colors[i] = colorlist[i % colorlist.Count];
                  }
                  else {
                     _colors = ReduceColors(colorlist, _numStates);
                  }
                  // For each cell, set its state to the one most closely matching the color.
                  for (int y = 0; y < _size.Height; ++y) {
                     for (int x = 0; x < _size.Width; ++x) {
                        var color = scaled.GetPixel(x, y);
                        int mindist = int.MaxValue;
                        int minindex = 0;
                        for (int i = 0; i < _numStates; ++i) {
                           int dist = ColorDist(color, _colors[i]);
                           if (dist < mindist) {
                              mindist = dist;
                              minindex = i;
                              if (mindist == 0) break;
                           }
                        }
                        var cell = _cells[y * _size.Width + x];
                        cell.Age = 0;
                        cell.State = minindex;
                     }
                  }
               }
            }
         }
         public void RandomizeColors() {
            for (int i = 0; i < _colors.Length; ++i) {
               var c = _colors[i];
               int r = _rand.Next(_colors.Length);
               if (r == i) continue;
               _colors[i] = _colors[r];
               _colors[r] = c;
            }
         }
         public void Reset() {
            switch (InitialCond) {
               case InitialCond.Random:
                  foreach (Cell cell in _cells) {
                     cell.State = _rand.Next(_numStates);
                     cell.Age = 0;
                  }
                  break;
               case InitialCond.Ordered:
                  for (int i = 0; i < _cells.Length; ++i) {
                     _cells[i].State = i % _numStates;
                     _cells[i].Age = 0;
                  }
                  break;
               case InitialCond.FromFile:
                  InitializeFromFile(InitFilename);
                  break;
               default:
                  throw new ArgumentOutOfRangeException();
            }
         }
         private void CreateCellArray() {
            _cells = new Cell[_size.Width * _size.Height];
            for (int i = 0; i < _cells.Length; ++i) _cells[i] = new Cell(i / _size.Width, i % _size.Width);
            for (int y = 0; y < _size.Height; ++y) {
               for (int x = 0; x < _size.Width; ++x) {
                  var cell = _cells[y * _size.Width + x];
                  cell.Neighbors = new Cell[8];
                  int n = -1;
                  for (int addy = -1; addy <= 1; ++addy) {
                     for (int addx = -1; addx <= 1; ++addx) {
                        if (addx == 0 && addy == 0) continue;
                        ++n;
                        int X = x + addx;
                        int Y = y + addy;
                        if (X < 0 || Y < 0 || X >= _size.Width || Y >= _size.Height) continue;
                        cell.Neighbors[n] = _cells[Y * _size.Width + X];
                     }
                  }
               }
            }
         }
         private static int CompareB(Color c1, Color c2) {
            return c1.B < c2.B ? -1 : c1.B > c2.B ? 1 : 0;
         }
         private static int CompareG(Color c1, Color c2) {
            return c1.G < c2.G ? -1 : c1.G > c2.G ? 1 : 0;
         }
         private static int CompareR(Color c1, Color c2) {
            return c1.R < c2.R ? -1 : c1.R > c2.R ? 1 : 0;
         }
         private static int CompareRGB(Color c1, Color c2) {
            int i1 = c1.R + c1.G + c1.B;
            int i2 = c2.R + c2.G + c2.B;
            return i1 < i2 ? -1 : i1 > i2 ? 1 : 0;
         }
         /// <summary>
         ///    Gets a color indexed along a continuous rainbow of 1536 colors.
         /// </summary>
         private static Color GetColorByIndex(int i) {
            //int original = i;
            if (i < 0) throw new Exception("Invalid color index");
            if (i < 256) return Color.FromArgb(255, i, 0);
            i -= 256;
            if (i < 256) return Color.FromArgb(255 - i, 255, 0);
            i -= 256;
            if (i < 256) return Color.FromArgb(0, 255, i);
            i -= 256;
            if (i < 256) return Color.FromArgb(0, 255 - i, 255);
            i -= 256;
            if (i < 256) return Color.FromArgb(i, 0, 255);
            i -= 256;
            if (i < 256) return Color.FromArgb(255, 0, 255 - i);
            throw new Exception("Color index too large");
         }
         private static Color[] ReduceColors(List<Color> colorlist, int max) {
            // Reduce the color list until we have one color for each state.
            while (colorlist.Count > max) {
               // Merge the two closest reds.
               int index = -1;
               int mindist = int.MaxValue;
               colorlist.Sort(CompareR);
               for (int i = 0; i < colorlist.Count - 1; ++i) {
                  int dist = colorlist[i + 1].R - colorlist[i].R;
                  if (dist > 0 && dist < mindist) {
                     mindist = dist;
                     index = i;
                     if (mindist == 0) break;
                  }
               }
               if (index >= 0 && mindist > 0) {
                  int r = (colorlist[index].R + colorlist[index + 1].R) / 2;
                  var c = colorlist[index];
                  colorlist[index] = Color.FromArgb(r, c.G, c.B);
                  c = colorlist[index + 1];
                  colorlist[index + 1] = Color.FromArgb(r, c.G, c.B);
               }
               // Merge the two closest greens.
               index = -1;
               mindist = int.MaxValue;
               colorlist.Sort(CompareG);
               for (int i = 0; i < colorlist.Count - 1; ++i) {
                  int dist = colorlist[i + 1].G - colorlist[i].G;
                  if (dist > 0 && dist < mindist) {
                     mindist = dist;
                     index = i;
                     if (mindist == 0) break;
                  }
               }
               if (index >= 0 && mindist > 0) {
                  int g = (colorlist[index].G + colorlist[index + 1].G) / 2;
                  var c = colorlist[index];
                  colorlist[index] = Color.FromArgb(c.R, g, c.B);
                  c = colorlist[index + 1];
                  colorlist[index + 1] = Color.FromArgb(c.R, g, c.B);
               }
               // Merge the two closest blues.
               index = -1;
               mindist = int.MaxValue;
               colorlist.Sort(CompareB);
               for (int i = 0; i < colorlist.Count - 1; ++i) {
                  int dist = colorlist[i + 1].B - colorlist[i].B;
                  if (dist > 0 && dist < mindist) {
                     mindist = dist;
                     index = i;
                     if (mindist == 0) break;
                  }
               }
               if (index >= 0 && mindist > 0) {
                  int b = (colorlist[index].B + colorlist[index + 1].B) / 2;
                  var c = colorlist[index];
                  colorlist[index] = Color.FromArgb(c.R, c.G, b);
                  c = colorlist[index + 1];
                  colorlist[index + 1] = Color.FromArgb(c.R, c.G, b);
               }
               // Filter out any close colors.
               colorlist.Sort(CompareRGB);
               for (int i = 0; i < colorlist.Count - 1; ++i) {
                  if (ColorDist(colorlist[i], colorlist[i + 1]) < 10) {
                     colorlist.RemoveAt(i);
                     if (colorlist.Count == max) break;
                     switch (i) {
                        case 0:
                           break;
                        case 1:
                           i--;
                           break;
                        default:
                           i -= 2;
                           break;
                     }
                  }
               }
            }
            return colorlist.ToArray();
         }
         private int _ageSpeed;
         private Cell[] _cells;
         private Color[] _colors = {
                                      Color.FromArgb(255, 0, 0), Color.FromArgb(255, 128, 0), Color.FromArgb(255, 255, 0), Color.FromArgb(128, 255, 0),
                                      Color.FromArgb(0, 255, 0),
                                      Color.FromArgb(0, 255, 128), Color.FromArgb(0, 255, 255), Color.FromArgb(0, 128, 255), Color.FromArgb(0, 0, 255),
                                      Color.FromArgb(128, 0, 255),
                                      Color.FromArgb(255, 0, 255), Color.FromArgb(255, 0, 128)
                                   };
         private int _numStates = 12;
         private readonly Random _rand = new Random();
         private Size _size;
         private const int MAX_STATES = 1535;
      }
   }
}