using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChatClientApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            String MyGuid1 = System.Guid.NewGuid().ToString();
            String MyGuid2 = System.Guid.NewGuid().ToString();
            String MyGuid3 = System.Guid.NewGuid().ToString();
            String MyGuid4 = System.Guid.NewGuid().ToString();
            MessageBox.Show(MyGuid1 + Environment.NewLine + MyGuid2 + Environment.NewLine + MyGuid3 + Environment.NewLine + MyGuid4);
        }
    }
}
