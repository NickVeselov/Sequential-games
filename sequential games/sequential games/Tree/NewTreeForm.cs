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
    public partial class NewTreeForm : Form
    {
        Tree parent;
        public NewTreeForm(Tree parent_input)
        {
            InitializeComponent();
            parent = parent_input;
            comboBox1.SelectedIndex = 0;
            CreateValuesGrid(3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool AllCorrect = false;
            int N = 0,
                LevelsNumber = 0;
            List<Double> Values = new List<double>();
            string filename = textBox1.Text;
            string levels_string = Graphic_Interface.Analyzer.CheckValidStringInt(textBox2.Text, 2, Information.MaxTreeLevelsCount, true);
                        
            if (levels_string != "")
            {
                LevelsNumber = Convert.ToInt32(levels_string);
                string PlayersNumber = Graphic_Interface.
                    Analyzer.CheckValidStringInt(textBox3.Text, 2, Information.MaxPlayersNumber, true);
                if (PlayersNumber != "")
                {
                    N = Convert.ToInt32(PlayersNumber);
                    bool GridFilled = true;
                    for (int i = 0; i < N; i++)
                    {
                        if (dataGridView1.Rows[0].Cells[i].Value != null)
                        {
                            string CellValue = Graphic_Interface.
                                Analyzer.CheckValidStringDouble(dataGridView1.Rows[0].Cells[i].Value.ToString(), 0, 0, true);
                            if (CellValue != "")
                                Values.Add(Convert.ToDouble(CellValue));
                            else
                            {
                                GridFilled = false;
                                break;
                            }
                        }
                        else
                        {
                            GridFilled = false;
                                System.Windows.Forms.MessageBox.Show("Values grid: some cells are empty");
                            break;
                        }
                        if (GridFilled)
                            AllCorrect = true;
                    }

                    if (AllCorrect)
                    {
                        parent.NewTreeData(filename, LevelsNumber, comboBox1.SelectedIndex + 2, N, Values);
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parent.NewTreeData("", 0, -1, 0, new List<double>());
            this.Close();
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                string CheckResult = Graphic_Interface.Analyzer.CheckValidStringInt
                    (textBox3.Text, 2, Information.MaxPlayersNumber, true);

                if (CheckResult == "")
                    System.Windows.Forms.MessageBox.Show("Players number: Invalid argument");
                else
                {
                    panel4.Show();
                    button1.Show();
                    CreateValuesGrid(Convert.ToInt32(textBox3.Text));
                }
            }
        }

        private void CreateValuesGrid(int N)
        {
            Graphic_Interface.Grid ValuesGrid = new Graphic_Interface.Grid(dataGridView1, 1, N, "T", "N");
            ValuesGrid.initialize();
            dataGridView1.TopLeftHeaderCell.Value = "Player";
            dataGridView1.Rows[0].HeaderCell.Value = "Values";
            ValuesGrid.create_headers();

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Rows[0].Cells[i].Value = 10 * (i + 1);

            if (dataGridView1.Width + 10 > panel4.Width)
            {
                int Add = (dataGridView1.Width + 10 - panel4.Width);
                panel1.Width += Add;
                panel2.Width += Add;
                panel3.Width += Add;
                panel4.Width += Add;
                panel5.Width += Add;
                button1.Left += Add;
                this.Width += Add;
            }
            else
                dataGridView1.Left = (panel4.Width - dataGridView1.Width) / 2;

            if (dataGridView1.Height + 10 > panel4.Height)
            {
                int Diff = dataGridView1.Height - panel4.Height + 10;
                button1.Top += Diff;
                button2.Top += Diff;
                panel4.Height += Diff;
                this.Height += Diff;
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                comboBox1.Focus();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                e.Handled = true;
        }
    }
}
