using System.Windows.Forms;

namespace Automata {
   public partial class ControlForm : Form {
      public ControlForm() {
         InitializeComponent();
      }
      public DisplayForm DisplayForm { get; set; }
      private void ControlForm_FormClosed(object sender, FormClosedEventArgs e) {
         DisplayForm.ControlForm = null;
         DisplayForm.Close();
      }
   }
}