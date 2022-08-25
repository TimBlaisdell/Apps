using System;
using System.Windows.Forms;

namespace Automata {
   public partial class DisplayForm : Form {
      public DisplayForm() {
         InitializeComponent();
      }
      public ControlForm ControlForm { get; set; }
      private void DisplayForm_FormClosed(object sender, FormClosedEventArgs e) {
         if (ControlForm != null) ControlForm.Close();
      }
      private void DisplayForm_Shown(object sender, EventArgs e) {
         ControlForm = new ControlForm {DisplayForm = this};
         ControlForm.Show();
      }
   }
}