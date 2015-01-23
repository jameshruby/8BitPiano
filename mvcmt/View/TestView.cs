using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bit8Piano
{
    public partial class TestView : Form
    {
        public TestView()
        {
            InitializeComponent();
            Button button = new Button { Text = "GO" };
            TextBox results = new TextBox();
            this.tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;
            this.tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.AddRows;

            this.tableLayoutPanel1.Controls.AddRange(new Control[] { button, results });

            
        }

        private void TestView_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
