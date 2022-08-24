using System;
using System.Drawing;

namespace CyclicalAutomata {
    public partial class CyclicalAutomata {
        private partial class Cell {
            private Cell(Point loc) {
                Properties = new Props(0);
                Neighbors = null;
                _location = loc;
                LastPaintedProps = null;
            }
            public Cell(int x, int y) : this(new Point(x, y)) {
            }
            //public Cell(Cell orig, int state) : this(orig._location) {
            //   Properties.State = state;
            //   Neighbors = orig.Neighbors;
            //}
            public int Age {
                get => Properties.Age;
                set => Properties.Age = value;
            }
            public Props LastPaintedProps { get; set; }
            public Cell[] Neighbors { get; set; }
            public Props NextProps { get; set; }
            public Props Properties { get; private set; }
            public int State {
                get => Properties.State;
                set => Properties.State = value;
            }
            public int[] CountNeighborsWithStates(params int[] states) {
                int[] counts = new int[states.Length];
                foreach (var cell in Neighbors) {
                    if (cell == null) continue;
                    for (int i = 0; i < states.Length; ++i) {
                        if (cell.Properties.State == states[i]) ++counts[i];
                    }
                }
                return counts;
            }
            public int[] CountNeighborsWithStates(bool diagonal, params int[] states) {
                int[] counts = new int[states.Length];
                int[] indexes = diagonal ? new[] { 0, 2, 5, 7 } : new[] { 1, 3, 4, 6 };
                foreach (int i in indexes) {
                    for (int s = 0; s < states.Length; ++s) {
                        if (Neighbors[i] != null && Neighbors[i].Properties.State == states[s]) ++counts[s];
                    }
                }
                return counts;
            }
            public int[] CountNeighborsWithStates(Point dir, bool include, params int[] states) {
                int[] counts = new int[states.Length];
                int[] indexes;
                if (include) {
                    indexes = new[] { DirectionPointToIndex(dir) };
                }
                else {
                    int avoid = DirectionPointToIndex(dir);
                    indexes = new int[7];
                    int ii = 0;
                    for (int i = 0; i < 8; ++i) {
                        if (i == avoid) continue;
                        indexes[ii] = i;
                        ++ii;
                    }
                }
                foreach (var j in indexes) {
                    if (Neighbors[j] == null) continue;
                    for (int i = 0; i < states.Length; ++i)
                        if (Neighbors[j].Properties.State == states[i])
                            ++counts[i];
                }
                return counts;
            }
            public bool HasNeighbor(int state, int threshold = 1, int high = 1000) {
                int count = 0;
                foreach (var cell in Neighbors) {
                    if (cell != null && cell.State == state) {
                        ++count;
                        if (count >= high) return false;
                    }
                }
                return count >= threshold && count < high;
            }
            public void Iterate() {
                if (NextProps != null) {
                    Properties = NextProps;
                    Properties.Age = 0;
                }
                else Properties.Age++;
                NextProps = null;
            }
            public override string ToString() {
                return "{" + _location + ", " + Properties.State + ", " + Properties.Age + "}";
            }
            private static int DirectionPointToIndex(Point p) {
                switch (p.Y) {
                    case -1:
                        return p.X == -1 ? 0 : p.X == 0 ? 1 : 2;
                    case 0:
                        return p.X == -1 ? 3 : 4;
                    case 1:
                        return p.X == -1 ? 5 : p.X == 0 ? 6 : 7;
                    default:
                        throw new Exception("Invalid direction.");
                }
            }
            private readonly Point _location;
        }
    }
}