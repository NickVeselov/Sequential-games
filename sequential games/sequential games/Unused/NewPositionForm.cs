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
    public partial class NewButtonDialogForm : Form
    {
        Tree TreeForm;
        List<List<string>> ParentsNames;
        bool violent = true;
        public NewButtonDialogForm(List<List<string>> parents, Tree ParentFormInput, int TreeLevels)
        {
            InitializeComponent();

            TreeForm = ParentFormInput;
            ParentsNames = parents;

            for (int i = 1; i <= TreeLevels; i++)
                comboBox1.Items.Add(i + 1);
        }

        private void cr8_Click(object sender, EventArgs e)
        {
            string NewName = textBox1.Text;
            bool coincide = false;
            for (int i = 0; i<ParentsNames.Count; i++)
                for (int j = 0; j < ParentsNames[i].Count; j++)
                {
                    if (NewName == ParentsNames[i][j])
                    {
                        coincide = true;
                        break;
                    }
                }

            if (!coincide)
            {
                TreeForm.NewButtonData(textBox1.Text, comboBox2.Text);
                violent = false;
                this.Close();
            }
            else
                System.Windows.Forms.MessageBox.Show("Position with that name already exists.");
        }

        private void canc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewButtonDialogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (violent)
                TreeForm.NewButtonData("", "");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel3.Show();

            int Level = Convert.ToInt32(comboBox1.SelectedItem) - 2;

            comboBox2.Items.Clear();
            for (int i = 0; i < ParentsNames[Level].Count; i++)
                comboBox2.Items.Add(ParentsNames[Level][i]);
        }
    }
}
