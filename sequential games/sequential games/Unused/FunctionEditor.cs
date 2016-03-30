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
    public partial class FunctionEditor : Form
    {
        private List<string> ParametersNames = new List<string>();
        private List<double> Values;
        private Graphic_Interface.Function F;
        ExtensiveFormGame EF;
        BimatrixGamesForm BG;
        TextBox T;


        public FunctionEditor()
        {
            InitializeComponent();
        }

        public FunctionEditor(Object Father, List<double> ValuesInput, ref TextBox Current, GamePosition gp)
        {
            InitializeComponent();
            if (Father is ExtensiveFormGame)
                EF = (Father as ExtensiveFormGame);
            else
                BG = (Father as BimatrixGamesForm);
            T = Current;
            Values = ValuesInput;
            if (T.Text != "")
                Function_TB.Text = T.Text;
        }
        
        private void EvaluationButton_Click(object sender, EventArgs e)
        {
            int BCC = BoxOfParameters.Controls.Count;
            for (int i = 1; i < BCC; i++ )
            {
                BoxOfParameters.Controls[1].Dispose();
            }
            BoxOfParameters.Show();
            string s = Function_TB.Text;
            F = new Graphic_Interface.Function(s);
            if (F.GetAllParameterNames(Values))
            {
                ParametersNames = F.ParametersNames;

                for (int i = 0; i < ParametersNames.Count; i++)
                {
                    Label l = new Label();
                    l.Text = ParametersNames[i] + " = ";
                    l.Font = new Font("Bookman Old Style", 12);
                    l.Size = TextRenderer.MeasureText(l.Text, l.Font);
                    l.Top = (i + 1) * 30;
                    l.Left = 5;
                    BoxOfParameters.Controls.Add(l);

                    TextBox t = new TextBox();
                    t.Font = l.Font;
                    t.Top = l.Top - 5;
                    t.Left = l.Right;
                    t.Width = 40;
                    t.TextAlign = HorizontalAlignment.Center;
                    if (BG != null)
                        t.Text = BG.gp.AdParamValues[i][0].ToString();
                    if (EF != null)
                        t.Text = EF.BG.gp.AdParamValues[i][0].ToString();
                    BoxOfParameters.Controls.Add(t);
                }
            }
        }

        private void CalculateIt_Click(object sender, EventArgs e)
        {
            Values.Clear();
            for (int i = 2; i < BoxOfParameters.Controls.Count; i += 2)
            {
                string CheckResult = Graphic_Interface.Analyzer.CheckValidStringDouble
                    ((BoxOfParameters.Controls[i] as TextBox).Text, 0, 0, true);
                if (CheckResult != "")
                    Values.Add(Convert.ToDouble(CheckResult));
                else
                {
                    Values.Clear();
                    break;
                }
            }

            if (Values.Count > 0)
            {
                ResultTB.Text = F.CalculateValue();
            }
        }

        private void SaveIt_Click(object sender, EventArgs e)
        {
            EF.FormulaValue = Function_TB.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

    }
}
