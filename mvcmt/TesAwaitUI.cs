//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using System.Windows;

//using System.ComponentModel;
//using System.Data;
//using System.Drawing;

//using System.Windows.Forms;

//namespace mvcmt
//{
//    public class TestAwaitUI : Form
//    {
//        Button button = new Button { Text = "GO"};        
//        TextBox results = new TextBox();
//        TableLayoutPanel panel;
    
//        public TestAwaitUI ()
//        {
//            this.panel = new TableLayoutPanel();
//            this.panel.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;
//            this.panel.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
//            this.panel.Location = new System.Drawing.Point(34, 86);

//            this.panel.Controls.AddRange(new Control[] { button, results });


//            this.button.Click += button_Click;

//            this.Controls.Add(this.panel);
//            this.ShowDialog();
//        }

//       async void button_Click(object sender, EventArgs e)
//        {
//            button.Enabled = false;
//           for (int i = 1; i < 2; i++)
//            {
//                results.Text += await GetPrimesCount(i * 1000000, 1000000) +
//                    "primes between " + (i * 1000000) + " and" + ((i + 1) * 1000000 - 1) +
//                    Environment.NewLine;
//            }
//           button.Enabled = true;
//        }

//        private Task<int> GetPrimesCount(int start, int count)
//        {
//            return Task.Run(() =>
//                ParallelEnumerable.Range(start, count).Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
//        }
//    }

    
//}
