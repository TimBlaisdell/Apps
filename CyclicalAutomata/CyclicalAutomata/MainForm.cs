using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using CyclicalAutomata.Properties;

namespace CyclicalAutomata {
   public partial class MainForm : Form {
      public MainForm() {
         InitializeComponent();
         var size = Settings.Default.AutomataSize == new Size(0, 0) ? ClientRectangle.Size : Settings.Default.AutomataSize;
         _automata = new CyclicalAutomata(size) {
                                                   Rule = (CyclicalAutomata.Rules) Settings.Default.Rule,
                                                   StateCount = Settings.Default.StateCount == 0 ? 16 : Settings.Default.StateCount
                                                };
         if (Settings.Default.RandomizeColors) _automata.RandomizeColors();
         Size = Settings.Default.WindowSize == new Size(0, 0) ? Size : Settings.Default.WindowSize;
         Delay = Settings.Default.Delay;
      }
      public CyclicalAutomata Automata {
         get { return _automata; }
      }
      public int Delay {
         get { return _delay; }
         set {
            _delay = value;
            Settings.Default.Delay = value;
            Settings.Default.Save();
         }
      }
      public bool Running { get; private set; }
      private void bwWorker_DoWork(object sender, DoWorkEventArgs e) {
         while (!bwWorker.CancellationPending) {
            _automata.Iterate();
            Thread.Sleep(Delay);
         }
      }
      private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
         if (Running) bwWorker.CancelAsync();
         _controlForm.Close();
      }
      private void Form1_MouseDown(object sender, MouseEventArgs e) {
         _moving = true;
         _lastMousePoint = MousePosition;
      }
      private void Form1_MouseMove(object sender, MouseEventArgs e) {
         if (!_moving) return;
         var mousepoint = MousePosition;
         Location = new Point(Location.X + mousepoint.X - _lastMousePoint.X, Location.Y + mousepoint.Y - _lastMousePoint.Y);
         _lastMousePoint = mousepoint;
      }
      private void Form1_MouseUp(object sender, MouseEventArgs e) {
         _moving = false;
      }
      private void Form1_Shown(object sender, EventArgs e) {
         //bwWorker.RunWorkerAsync();
         _controlForm = new Control(this);
         _controlForm.Show();
      }
      private void MainForm_Activated(object sender, EventArgs e) {
         //if (_controlForm != null) _controlForm.Activate();
      }
      private void MainForm_LocationChanged(object sender, EventArgs e) {
         if (_controlForm != null) _controlForm.Location = new Point(Right, Top);
      }
      private void MainForm_SizeChanged(object sender, EventArgs e) {
         if (_controlForm != null) _controlForm.Location = new Point(Right, Top);
         Settings.Default.WindowSize = Size;
         Settings.Default.Save();
      }
      private void timer_Tick(object sender, EventArgs e) {
         Invalidate();
         _controlForm.lblIterations.Text = _automata.Iterations.ToString();
      }
      public void Start() {
         if (Running) return;
         Running = true;
         bwWorker.RunWorkerAsync();
      }
      public void Stop() {
         if (!Running) return;
         Running = false;
         bwWorker.CancelAsync();
      }
      protected override void OnPaint(PaintEventArgs e) {
         using (Bitmap bmp = new Bitmap(_automata.Image, ClientRectangle.Size)) {
            e.Graphics.DrawImage(bmp, new Point(0, 0));
         }
      }
      protected override void OnPaintBackground(PaintEventArgs e) {
      }
      private readonly CyclicalAutomata _automata;
      private Control _controlForm;
      private int _delay;
      private Point _lastMousePoint;
      private bool _moving;
   }
}