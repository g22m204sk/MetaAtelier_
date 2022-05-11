using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms; 
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("update")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Picture")]
[assembly: AssemblyProduct("update")]
[assembly: AssemblyCopyright("Copyright © Picture 2021")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("d423a4a3-accb-4c79-acee-6c86ce54fdb6")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

namespace update
{
    public partial class Form1 : Form
    { 
        string root = "http://xs238699.xsrv.jp/contents/MetaAteliersPICTURE/games/Win/";
        string[] fileList;
        int cnt;
        string localDL;
        Timer t,t2;
        bool isEnd;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public Form1()
        {  
            InitializeComponent();
            var ListGet = new WebClient();
            fileList = ListGet.DownloadString("http://xs238699.xsrv.jp/contents/MetaAteliersPICTURE/games/SystemList.txt").Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            ListGet.Dispose();
            localDL = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString() + @"\";
 
            foreach (var i in Path.GetInvalidPathChars())
                localDL = localDL.Replace(i + "", "");

            //F*4o = 2*iv

            t  = new Timer() { Enabled = false, Interval = 10 };
            t2 = new Timer() { Enabled = true, Interval = 300 };

            t2.Tick += delegate (object h, EventArgs e)
            {
                bar.Value = (int)(10000f * cnt / fileList.Length);
                text.Text = ((bar.Value / 100) + "%").PadLeft(4, ' '); 
                Refresh();
                Invalidate(true);
                Update();
                Refresh();
            };
            
            t.Tick += delegate (object h, EventArgs e)
            {
                t.Enabled = false;
                if (fileList[cnt] != "version.txt" && fileList[cnt] != "update.exe")
                { 
                    using (var w = new WebClient())
                    { 
                         foreach (var i in Path.GetInvalidPathChars())
                             fileList[cnt] = fileList[cnt].Replace(i + "", "");
                         string cc = localDL + fileList[cnt];
                         if (!Directory.Exists(Directory.GetParent(cc).ToString()))
                         { 
                             Directory.CreateDirectory(Directory.GetParent(cc).ToString());
                         }
                         if (!File.Exists(cc)) File.CreateText(cc).Dispose();
                         w.DownloadFile(new Uri(root + fileList[cnt]), cc);
                         
                    }

                    cnt++;
                     if (cnt == fileList.Length)
                     {
                         using (var w = new WebClient())
                         {
                             if (!File.Exists(localDL + "version.txt"))
                                 File.CreateText(localDL + "version.txt").Dispose();
                             w.DownloadFile(new Uri(root + "version.txt"), localDL + "version.txt");
                         }

                         Process.Start(localDL+ @"..\start.exe"); 
                         Environment.Exit(1);
                     }  
                }
                 else
                     cnt++;
                   
                t.Enabled = true; 
             };
             
                t.Enabled = true;
        }  
    }
}
