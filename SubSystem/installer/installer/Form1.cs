using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms; 
using System.Reflection;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Text.RegularExpressions;

[assembly: AssemblyTitle("installer")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("picture")]
[assembly: AssemblyProduct("installer")]
[assembly: AssemblyCopyright("Copyright © picture 2021")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("33090679-0905-437a-8367-f3d19b14257c")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

namespace installer
{
    public partial class Form1 : Form
    { 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        bool IsExist;
       

        public Form1()
        {
            InitializeComponent();　
            label2.Text = "";

            button1.Click += delegate (object j, EventArgs e) 
            {
                var fbd = new FolderBrowserDialog()
                {
                    Description = "フォルダを指定してください。",
                    SelectedPath = @"C:\Windows"
                };

                if (fbd.ShowDialog(this) == DialogResult.OK)
                {
                    textBox1.Text = fbd.SelectedPath;
                    TextChanged();
                }
            };

            button2.Click += delegate (object j, EventArgs e) 
            {
                TextChanged();
                if (IsExist && checkBox1.Checked) MakeApp();
            };

            button3.Click += delegate (object j, EventArgs e) 
            { 
                var r = MessageBox.Show("終了しますか?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (r == DialogResult.Yes)
                    Environment.Exit(1);
            };
            textBox1.TextChanged += delegate (object j, EventArgs e)
            {
                TextChanged();
            };
            checkBox1.CheckedChanged += delegate (object j, EventArgs e)
            {
                textBox1.Enabled = button1.Enabled = button2.Enabled = checkBox1.Checked;
            };
            textBox1.Enabled = button1.Enabled = button2.Enabled = checkBox1.Checked;
        }


        string RootDirName = "Meta Atelier Picture";
        string rootdir;

        void MakeApp()
        {
            rootdir = textBox1.Text + @"\" + RootDirName + @"\";
            
            //既に同名フォルダがあるとまざるので～(1)\みたいにする------------------------
            if (Directory.Exists(rootdir))
            {
                int f = 0;
                do
                {
                    f++;
                    rootdir = textBox1.Text + @"\" + RootDirName + "(" + f + ")" + @"\";
                }
                while (Directory.Exists(rootdir));
            }

            //----------------------------------------
            MkDir(""); //ディレクトリ作成
            MkDir("System"); //ディレクトリ作成  
            GetFile("start.exe", "start.exe");
            GetFile("update.exe", @"System\update.exe");
            GetFile("readme.txt", @"readme.txt");
            GetFile("Hp.url", @"Hp.url");
            Process.Start(rootdir);
            Process.Start(rootdir + @"System\update.exe");
            Application.Exit();
        }




        static string r = "http://xs238699.xsrv.jp/contents/MetaAteliersPICTURE/games/";

        void MkDir(string c) { if(!Directory.Exists(rootdir+c))  Directory.CreateDirectory(rootdir+c);  }

        void GetFile(string x, string f)
        { 
            try
            {
                x = x.Replace("\n", "");
                using (var w = new WebClient())
                {
                    if (!File.Exists(rootdir + f))
                        File.CreateText(rootdir + f).Dispose();
                    w.DownloadFile(new Uri((r + x).Replace(@"\", "/")), rootdir + f);
                }
            }
            catch (Exception) {}
        }

        void TextChanged()
        {
            IsExist = Directory.Exists(textBox1.Text);
            ERRORTEXT();
        }

        

        void ERRORTEXT()
        {
            label2.Text =
                (!IsExist ? "存在しないディレクトリです.\n" : "") + 
                (!checkBox1.Checked ? "同意ボタンが押されていません." : "");
        }

　

    }

}
