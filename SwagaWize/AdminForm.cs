// AdminForm.cs
using System;
using System.Windows.Forms;
using FitnessCenterApp.Forms;

namespace FitnessCenterApp.Forms
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void btnManageTrainers_Click(object sender, EventArgs e)
        {
            new ManageTrainersForm().ShowDialog();
        }

        private void btnAddSession_Click(object sender, EventArgs e)
        {
            new AddWorkoutSessionForm().ShowDialog();
        }

        private void btnManageTables_Click(object sender, EventArgs e)
        {
            new CreateTableForm().ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            new ReportForm().ShowDialog();
        }
    }
}