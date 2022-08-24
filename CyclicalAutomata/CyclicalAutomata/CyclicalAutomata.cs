using System;
using System.Drawing;

namespace CyclicalAutomata {
    public partial class CyclicalAutomata {
        public CyclicalAutomata(Size size) {
            _cells = new CellGrid(size) { InitialCond = InitialCond.Random };
            _allstates = new int[_cells.NumStates];
            for (int i = 0; i < _cells.NumStates; ++i) _allstates[i] = i;
            FavorRotationDirection = -1;
            ThresholdLow = 1;
            ThresholdHigh = 100;
            Reset();
        }
        public AgeEffects AgeEffect {
            get => _cells.AgeEffect;
            set => _cells.AgeEffect = value;
        }
        public int AgeSpeed {
            get => _cells.AgeSpeed;
            set => _cells.AgeSpeed = value;
        }
        public int FavorRotationDirection { get; set; }
        public Bitmap Image => _cells.Bitmap;
        public InitialCond Initial {
            get => _cells.InitialCond;
            set => _cells.InitialCond = value;
        }
        public int Iterations { get; private set; }
        public Rules Rule { get; set; }
        public Size Size {
            get => _cells.Size;
            set => _cells.Size = value;
        }
        public int StateCount {
            get => _cells.NumStates;
            set {
                _cells.NumStates = value;
                _allstates = new int[_cells.NumStates];
                for (int i = 0; i < _cells.NumStates; ++i) _allstates[i] = i;
                Reset();
            }
        }
        public int ThresholdHigh { get; set; }
        public int ThresholdLow { get; set; }
        public void InitializeFromFile(string filename) {
            _cells.InitializeFromFile(filename);
        }
        //public interface IRule {
        //   string Description { get; }
        //   string Name { get; }
        //}
        public void Iterate() {
            _cells.ForEach(cell => {
                               cell.NextProps = null;
                               int[] counts;
                               switch (Rule) {
                                   case Rules.NextOnly:
                                       if (cell.HasNeighbor(NextState(cell.State), ThresholdLow, ThresholdHigh))
                                           cell.NextProps = new Cell.Props(NextState(cell.State));
                                       break;
                                   case Rules.PrevOnly:
                                       if (cell.HasNeighbor(PrevState(cell.State), ThresholdLow, ThresholdHigh))
                                           cell.NextProps = new Cell.Props(PrevState(cell.State));
                                       break;
                                   case Rules.NextIfDouble:
                                   case Rules.NextIfTriple:
                                   case Rules.NextIfQuad:
                                   case Rules.NextIfQuint:
                                       counts = cell.CountNeighborsWithStates(PrevState(cell.State), NextState(cell.State));
                                       if (counts[1] >= counts[0] * (int)Rule) cell.NextProps = new Cell.Props(NextState(cell.State));
                                       break;
                                   case Rules.NextOrPrev:
                                       var hasnext = cell.HasNeighbor(NextState(cell.State), ThresholdHigh);
                                       var hasprev = cell.HasNeighbor(PrevState(cell.State), ThresholdLow);
                                       if ((hasnext && !hasprev) || (hasprev && !hasnext)) {
                                           cell.NextProps = new Cell.Props(hasnext ? NextState(cell.State) : PrevState(cell.State));
                                       }
                                       break;
                                   case Rules.FavorNext:
                                       if (cell.HasNeighbor(NextState(cell.State))) cell.NextProps = new Cell.Props(NextState(cell.State));
                                       else if (cell.HasNeighbor(PrevState(cell.State))) cell.NextProps = new Cell.Props(PrevState(cell.State));
                                       break;
                                   case Rules.NextIfMore:
                                       counts = cell.CountNeighborsWithStates(PrevState(cell.State), NextState(cell.State));
                                       if (counts[1] > counts[0]) cell.NextProps = new Cell.Props(NextState(cell.State));
                                       break;
                                   case Rules.NextIfZero:
                                       counts = cell.CountNeighborsWithStates(NextState(cell.State));
                                       if (counts[0] == 0) cell.NextProps = new Cell.Props(NextState(cell.State));
                                       break;
                                   case Rules.NextProgressive:
                                       counts = cell.CountNeighborsWithStates(_allstates);
                                       int req = 2;
                                       for (int curstate = NextState(cell.State); curstate != cell.State; curstate = NextState(curstate)) {
                                           if (counts[curstate] >= req) {
                                               cell.NextProps = new Cell.Props(curstate);
                                               break;
                                           }
                                           ++req;
                                       }
                                       break;
                                   case Rules.NextFavorDiagonal:
                                       var dcounts = cell.CountNeighborsWithStates(true, NextState(cell.State));
                                       if (dcounts[0] > 0) cell.NextProps = new Cell.Props(NextState(cell.State));
                                       break;
                                   case Rules.NextFavorOrthogonal:
                                       var ocounts = cell.CountNeighborsWithStates(false, NextState(cell.State));
                                       if (ocounts[0] > 0) cell.NextProps = new Cell.Props(NextState(cell.State));
                                       break;
                                   case Rules.NextRotatingFavor:
                                       var favoredcounts = cell.CountNeighborsWithStates(_cells.Favor, true, NextState(cell.State));
                                       var unfavoredcounts = cell.CountNeighborsWithStates(_cells.Favor, false, NextState(cell.State));
                                       if (favoredcounts[0] > 0 || unfavoredcounts[0] > 5) cell.NextProps = new Cell.Props(NextState(cell.State));
                                       break;
                                   default:
                                       throw new ArgumentOutOfRangeException();
                               }
                           });
            _cells.Favor = RotateDirection(_cells.Favor);
            _cells.FinishIteration();
            ++Iterations;
        }
        public void RandomizeColors() {
            _cells.RandomizeColors();
        }
        public void Reset() {
            _cells.Reset();
            Iterations = 0;
        }
        private int NextState(int state) {
            return state == StateCount - 1 ? 0 : state + 1;
        }
        private int PrevState(int state) {
            return state == 0 ? StateCount - 1 : state - 1;
        }
        private Point RotateDirection(Point dir) {
            if (FavorRotationDirection >= 0) {
                for (int i = 0; i < _alldirections.Length; ++i) {
                    if (dir == _alldirections[i]) return _alldirections[i == _alldirections.Length - 1 ? 0 : i + 1];
                }
            }
            else {
                for (int i = _alldirections.Length - 1; i >= 0; --i) {
                    if (dir == _alldirections[i]) return _alldirections[i == 0 ? _alldirections.Length - 1 : i - 1];
                }
            }
            return _alldirections[0];
        }
        private static int ColorDist(Color c1, Color c2) {
            return Math.Abs(c1.R - c2.R) + Math.Abs(c1.G - c2.G) + Math.Abs(c1.B - c2.B);
        }
        private static int NormalizeTowardValue(int value, int target, int amount) {
            if (value == target) return value;
            if (amount < 0) amount *= -1; // make sure amount is positive.
            if (amount > Math.Abs(value - target)) amount = Math.Abs(value - target); // make sure we won't overshoot the target.
            if (value > target) amount *= -1; // ensure amount points in the right direction.
            return value + amount;
        }
        private int[] _allstates;
        private readonly CellGrid _cells;
        //private string _initFilename;
        //private readonly Random _rand = new Random();
        private static readonly Point[] _alldirections = {
                                                             new Point(1, 0), new Point(1, -1), new Point(0, -1), new Point(-1, -1), new Point(-1, 0), new Point(-1, 1),
                                                             new Point(0, 1),
                                                             new Point(1, 1)
                                                         };
        private static readonly object _lockobj = new object();
        //private Bitmap _patternImage;
    }
}