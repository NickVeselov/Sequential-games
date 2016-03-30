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
    public partial class ParametersWeightsForm : Form
    {
        GamePosition gp;
        public ParametersWeightsForm(GamePosition GP)
        {
            InitializeComponent();
            gp = GP;

            this.Text = "Parameters importance: '";
            if (GP.name != null)
                this.Text += GP.name;
            else
                this.Text += GP.ID;
            this.Text += "' position";

            Graphic_Interface.Grid G = new Graphic_Interface.Grid(dataGridView1, Information.AP_Names.Count, gp.N, "T", "T");
            G.initialize();
            dataGridView1.TopLeftHeaderCell.Value = "Parameters weights";
            for (int i = 0; i < Information.AP_Names.Count - 1; i++)
                dataGridView1.Rows[i].HeaderCell.Value = Information.AP_Names[i + 1];

            for (int i = 0; i < Information.AP_Names.Count; i++)
                dataGridView1.Rows[i].HeaderCell.Value = Information.AP_Names[i] + " (" + Information.AP_KeyLetters[i] + ")";

            for (int i = 0; i < gp.N; i++)
            {
                string Key = "";
                if ((Information.PlayersNames[i] == "")||(Information.PlayersNames[i] == null))
                    Key = "Player "+(i+1).ToString();
                else
                    Key = Information.PlayersNames[i];

                dataGridView1.Columns[i].HeaderText = Key;
            }

            if (gp.Weights.Count < Information.AP_Names.Count)
            {
                int gpWC = gp.Weights.Count;
                for (int i = gpWC; i < Information.AP_Names.Count; i++)
                {
                    gp.Weights.Add(new List<string>());
                    for (int j = 0; j < gp.N; j++)
                        gp.Weights[i].Add("0");
                }
            }

            for (int i = 0; i < Information.AP_Names.Count; i++)
                for (int j = 0; j < gp.N; j++)
                {
                    if (gp.Weights[i][j] == "")
                        gp.Weights[i][j] = "0";                    
                    dataGridView1[j, i].Value = gp.Weights[i][j];
                }

            G.create_headers();

            this.Width = dataGridView1.Right + dataGridView1.Left + 40;
            this.Height = dataGridView1.Top + dataGridView1.Bottom + 60;

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gp.Weights.Clear();
            bool empty = false;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                gp.Weights.Add(new List<string>());
                if (empty)
                    break;
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (dataGridView1[j, i].Value == null)
                    {
                        System.Windows.Forms.MessageBox.Show("Some cells are empty");
                        empty = true;
                        break;
                    }
                    else
                        gp.Weights[i].Add(dataGridView1[j, i].Value.ToString());
                }
            }
        }

        private void ParametersWeightsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            gp.WeightsForm_Active = false;
        }

    }
}
