using FitnessCenterApp.Forms;
using System;
using System.Windows.Forms;

namespace FitnessCenterApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm()); // Запускаем с формы входа
        }
    }
}