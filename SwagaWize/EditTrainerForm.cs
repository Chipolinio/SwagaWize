using System;
using System.Windows.Forms;

namespace FitnessCenterApp.Forms
{
    public partial class EditTrainerForm : Form
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public EditTrainerForm(string currentFirst, string currentLast)
        {
            InitializeComponent();
            txtFirstName.Text = currentFirst;
            txtLastName.Text = currentLast;
            txtFirstName.SelectAll();
            txtFirstName.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FirstName = txtFirstName.Text;
            LastName = txtLastName.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}