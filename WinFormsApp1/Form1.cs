using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.textBox1.Text = Properties.Settings.Default.Trial;
			Console.WriteLine("hello");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
		}

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
			Debug.WriteLine("Button Click");
			string GitHubRepo = "/gaumo62/update-trial";
			string pattern =
				string.Concat(Regex.Escape(GitHubRepo),@"\/releases\/download\/v[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+.*\.zip");
			Debug.WriteLine(string.Concat(Regex.Escape(GitHubRepo), @"\/releases\/download\/v[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+.*\.zip"));
			Regex urlMatcher = new Regex(pattern, RegexOptions.CultureInvariant | RegexOptions.Compiled);
			var result = new Dictionary<Version, Uri>();
			Debug.WriteLine(string.Concat("https://github.com", GitHubRepo, "/releases/latest"));
			WebRequest wrq = WebRequest.Create(string.Concat("https://github.com", GitHubRepo, "/releases/latest"));
			WebResponse wrs = null;
			try
			{
				wrs = wrq.GetResponse();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error fetching repo: " + ex.Message);
				return;
			}
			Debug.WriteLine("Got Response");
			Debug.WriteLine(wrs.ToString());
			string print = "";
			using (var sr = new StreamReader(wrs.GetResponseStream()))
			{
				string line;
				Debug.WriteLine("just outside loop");
				while (null != (line = sr.ReadLine()))
				{
					print += line + "\n";
					var match = urlMatcher.Match(line);
					if (match.Success)
					{
						var uri = new Uri(string.Concat("https://github.com", match.Value));
						var vs = match.Value.LastIndexOf("/v");
						var sa = match.Value.Substring(vs + 10).Split('.', '/');
						var v = new Version(int.Parse(sa[0]), int.Parse(sa[1]), int.Parse(sa[2]), int.Parse(sa[3]));
						result.Add(v, uri);
					}
				}
			}
			string print2 = "";
            foreach (var item in result)
            {
                print2 += item.Key.ToString() + " " + item.Value.ToString() + "\n";
            }
            Debug.WriteLine("Print2:" + print2);
			richTextBox1.Text = print;
			richTextBox2.Text = print2;
			return;
		}
    }
}
