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
    public partial class Report_Form : Form
    {
        List<GamePosition> EndPositions;
        List<GamePosition> Path = new List<GamePosition>();
        List<List<Label>> PlayersLabels = new List<List<Label>>();
        List<List<string>> Strategies = new List<List<string>>();
        List<int> Direction = new List<int>();
        int OptIndex;
        
        public Report_Form(List<GamePosition> PositionsInput, int OptIndexInput)
        {
            InitializeComponent();
            EndPositions = PositionsInput;
            OptIndex = OptIndexInput;
            WritePath();
        }

        private void WritePath()
        {
            Path.Add(EndPositions[OptIndex]);

            GamePosition Pendulum = Path.Last();
            if (Pendulum.name != "")
                PathLB.Text = Pendulum.name;
            else
                PathLB.Text = Pendulum.ID;
            while (Pendulum.parent != null)
            {
                Pendulum = Pendulum.parent;
                Path.Insert(0, Pendulum);
            }

            for (int i = 0; i < Path.Count - 1; i++)
            {
                for (int j = 0; j < Path[i].children.Count; j++)
                {
                    if (Path[i + 1] == Path[i].children[j])
                        Direction.Add(j);
                }
            }


            PathLB.Text = "";
            for (int i = 0; i < Path.Count; i++)
            {
                if ((Path[i].name != null)&&(Path[i].name != ""))
                    PathLB.Text += Path[i].name;
                else
                    PathLB.Text += Path[i].ID;
                if (i != Path.Count - 1)
                    PathLB.Text += "-->";
            }

            panel1.Width = PathLB.Right + 10;

            for (int i = 0; i < Information.players_number; i++)
                Strategies.Add(new List<string>());

            for (int i = 0; i < Path.Count - 1; i++)
            {
                List<string> Combination = Path[i].Combinations[Direction[i]];
                for (int j = 0; j < Combination.Count; j++)
                    Strategies[j].Add(Combination[j]);
            }

            for (int i = 0; i < Information.players_number; i++)
                CreatePlayerPanel(i);
        }

        private void CreatePlayerPanel(int player)
        {
            Panel p = new Panel();
            p.BackColor = Color.White;
            p.Top = panel1.Top + 80 * player;
            p.Left = panel1.Right + 20;
            p.Height = 70;
            p.Width = 150;
            this.Controls.Add(p);

            //Player name
            Label l = new Label();
            l.Font = new System.Drawing.Font("Bookman Old Style", 12, FontStyle.Bold);
            if (Information.PlayersNames[player] != null)
                l.Text = Information.PlayersNames[player];
            else
                l.Text = "Player " + (player + 1).ToString();
            l.Left = 5;
            l.Top = 5;
            l.Size = TextRenderer.MeasureText(l.Text, l.Font);
            p.Controls.Add(l);


            //Profit
            l = new Label();
            l.Font = new System.Drawing.Font("Bookman Old Style", 10);
            l.Text = "Profit: " + Path.Last().cash[player].ToString();
            l.Left = 5;
            l.Top = 30;
            l.Size = TextRenderer.MeasureText(l.Text, l.Font);
            p.Controls.Add(l);

            //Strategy
            l = new Label();
            l.Font = new System.Drawing.Font("Bookman Old Style", 10);
            l.Text = "Strategies: ";
            l.Left = 5;
            l.Top = 45;            
            for (int j = 0; j < Strategies[player].Count; j++)
            {
                l.Text += Strategies[player][j];
                if (j != Strategies[player].Count - 1)
                    l.Text += "-->";
            }
            l.Size = TextRenderer.MeasureText(l.Text, l.Font);
            p.Width = l.Width + 10;
            p.Controls.Add(l);

            this.Width = Math.Max(this.Width, p.Right + 50);
            this.Height = Math.Max(this.Height, p.Bottom + 50);
        }
    }
}
