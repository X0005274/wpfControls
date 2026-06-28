using System;
using System.Windows.Forms;

namespace com.example.Demo
{
    /// <summary>
    /// Entry point for the WinForms host demo. The thread is STA (required by WPF).
    /// </summary>
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SampleShellForm());
        }
    }
}
