using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SequentialGames
{
    public partial class DebugForm : Form
    {
        public DebugForm(List<List<double>> LHA, List<List<double>> LHB)
        {
            InitializeComponent();

            Graphic_Interface.Grid G1 = new Graphic_Interface.Grid(dataGridView1, LHA.Count, LHA[0].Count, "T", "N");
            G1.initialize();
            dataGridView1.Rows[0].HeaderCell.Value = "Рекламировать";
            dataGridView1.Rows[1].HeaderCell.Value = "Понижение стоимости";
            dataGridView1.Rows[2].HeaderCell.Value = "Цена +";
            dataGridView1.Rows[3].HeaderCell.Value = "Цена -";
            dataGridView1.Rows[4].HeaderCell.Value = "Ничего";

            Graphic_Interface.Grid G2 = new Graphic_Interface.Grid(dataGridView2, LHA.Count, LHA[0].Count, "T", "N");
            G2.initialize();
            dataGridView2.Rows[0].HeaderCell.Value = "Рекламировать";
            dataGridView2.Rows[1].HeaderCell.Value = "Понижение стоимости";
            dataGridView2.Rows[2].HeaderCell.Value = "Цена +";
            dataGridView2.Rows[3].HeaderCell.Value = "Цена -";
            dataGridView2.Rows[4].HeaderCell.Value = "Ничего";

            for (int i = 0; i < LHA.Count; i++)
                for (int j = 0; j < LHA[i].Count; j++)
                {
                    dataGridView1[j, i].Value = (LHA[i][j] * 100).ToString("0");
                    dataGridView2[j, i].Value = (LHB[i][j] * 100).ToString("0");
                    Graphic_Interface.Analyzer.ResizeColumn(this, false, dataGridView1, i, j, dataGridView1[j, i].Value.ToString(), 100);
                    Graphic_Interface.Analyzer.ResizeColumn(this, false, dataGridView2, i, j, dataGridView2[j, i].Value.ToString(), 100);
                }

            G1.create_headers();
            G2.create_headers();

            dataGridView2.Left = dataGridView1.Right + 20;

            button1.Top = dataGridView1.Bottom + 20;
            Done.Top = button1.Top;

            this.Height = button1.Bottom + 50;
            this.Width = dataGridView2.Right + 50;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Done_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
