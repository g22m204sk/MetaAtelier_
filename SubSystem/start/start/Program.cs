using System; 
using System.Reflection; 
using System.Runtime.InteropServices;
using System.Net;
using System.IO;
using System.Diagnostics;
 
[assembly: AssemblyTitle("start")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Picture")]
[assembly: AssemblyProduct("start")]
[assembly: AssemblyCopyright("Copyright © Picture 2022")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")] 
[assembly: ComVisible(false)] 
[assembly: Guid("56f08b75-076b-451f-a06d-59bd46c7b8c1")] 
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
 
static class Program
{
    [STAThread]
    static void Main()
    {
        string rr = "" + Directory.GetParent(Assembly.GetExecutingAssembly().Location) + @"\System\";
        WebClient wc = new WebClient();
        try
        {
            string ww = wc.DownloadString("http://xs238699.xsrv.jp/contents/MetaAteliersPICTURE/games/Win/version.txt");
            wc.Dispose();

            if (File.Exists(rr + @"version.txt") && File.ReadAllText(rr + @"version.txt") == ww && File.Exists(rr + @"index.txt"))
                Process.Start(rr + File.ReadAllText(rr + @"index.txt"));
            else
                Process.Start(rr + @"update.exe");
        }
        catch (Exception e)
        {
            Process.Start("https://ja.wikipedia.org/wiki/AAAA____" +  e.Message + "___" + rr );
        }
    }
}
