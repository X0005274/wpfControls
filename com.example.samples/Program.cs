using System;
using System.Windows.Forms;

namespace com.example.Demo
{
    /// <summary>
    /// Entry point for the WinForms host demo. The thread is STA (required by WPF).
    /// Pass <c>--shots &lt;dir&gt;</c> to run the process-flow screenshot harness (drives the
    /// flow stage by stage, saving a PNG per stage into the directory) instead of the shell.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args != null && args.Length >= 2 && args[0] == "--shots")
            {
                ProcessFlowForm shots = new ProcessFlowForm();
                shots.EnableCaptureMode(args[1]);
                Application.Run(shots);
                return;
            }

            Application.Run(new SampleShellForm());
        }
    }
}
