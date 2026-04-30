using Particles.Properties;
using System;
using System.Text;
using System.Windows.Forms;

namespace Particles
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private string GetHtml()
        {
            string html = Encoding.UTF8.GetString(Resources.Particles_HTML);
            string js = Encoding.UTF8.GetString(Resources.Particles_JS);
            return html.Replace("<script>console.error('Cannot load particles.min.js')</script>", $"<script>{js}</script>");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string finalHtml = GetHtml();
            webView2.Source = new Uri("data:text/html;base64," + Convert.ToBase64String(Encoding.UTF8.GetBytes(finalHtml)));
        }
    }
}
