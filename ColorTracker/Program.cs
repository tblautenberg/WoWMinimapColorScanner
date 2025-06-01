using System.Diagnostics;
using System.Media;
using WoWMinimapColorScanner;
using WMPLib;



static class Program
{
    [STAThread]
    static void Main()
    {
        Debug.WriteLine("Krappa");
        SystemSounds.Beep.Play();
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new ScannerForm());
    }
}