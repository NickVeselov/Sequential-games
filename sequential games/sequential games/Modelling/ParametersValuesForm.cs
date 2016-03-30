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
    public partial class ParametersValuesForm : Form
    {
        GamePosition gp;
        public ParametersValuesForm()
        {
            InitializeComponent();
        }

        public ParametersValuesForm(GamePosition GP)
        {
            InitializeComponent();
            gp = GP;

            this.Text = "Parameters values: '";
            if (GP.name != null)
                this.Text +=GP.name;
            else
                this.Text +=GP.ID;
            this.Text += "' position";

            Graphic_Interface.Grid G = new Graphic_Interface.Grid(dataGridView1, Information.AP_Names.Count, gp.N, "T", "T");
            G.initialize();
            for (int i = 0; i < Information.AP_Names.Count - 1; i++)
                dataGridView1.Rows[i].HeaderCell.Value = Information.AP_Names[i + 1];
            dataGridView1.TopLeftHeaderCell.Value = "Parameters values";
            dataGridView1.Rows[0].ReadOnly = true;
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
            for (int i = 0; i < gp.N; i++)
            {
                if (gp.cash.Count > i)
                    dataGridView1[i, 0].Value = gp.cash[i];
            }

            for (int i = 0; i < gp.AdParamValues.Count; i++)
                for (int j = 0; j < gp.AdParamValues[i].Count; j++)
                    dataGridView1[j, i + 1].Value = gp.AdParamValues[i][j];

            G.create_headers();

            this.Width = dataGridView1.Right + dataGridView1.Left + 40;
            this.Height = dataGridView1.Top + dataGridView1.Bottom + 60;

        }

        private void ParametersData_FormClosing(object sender, FormClosingEventArgs e)
        {
            gp.ValuesForm_Active = false;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gp.AdParamValues.Clear();
            bool empty = false;

            for (int i = 1; i < dataGridView1.Rows.Count; i++)
            {
                gp.AdParamValues.Add(new List<double>());
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
                    {
                        string CR = Graphic_Interface.Analyzer.CheckValidStringDouble(dataGridView1[j, i].Value.ToString(), 0, 0, true);
                        if (CR != "")
                            gp.AdParamValues.Last().Add(Convert.ToDouble(CR));
                        else
                        {
                            gp.AdParamValues.Clear();
                            empty = true;
                            break;
                        }
                    }
                }
            }
        }

    }
}
