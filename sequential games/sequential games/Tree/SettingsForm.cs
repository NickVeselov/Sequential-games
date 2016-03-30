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
    public partial class Settings : Form
    {
        int width, height;
        public Settings()
        {
            InitializeComponent();
            width = this.Width;
            height = this.Height;

            Players();
            IrrationalBehaviour();

            this.Width = PIBGrid.Right + 40;
            this.Height = menuStrip1.Height + IBPanel.Top + IBPanel.Height + 10;
        }

        private void Players()
        {
            Graphic_Interface.Grid PNGrid = new Graphic_Interface.Grid
                (PlayersGrid, 1, Information.players_number, "T", "TWDN");

            PNGrid.fontsize = 12;
            PNGrid.initialize();
            PlayersGrid.Rows[0].HeaderCell.Value = "Names";
            PlayersGrid.AllowUserToResizeColumns = false;
            PNGrid.column_header_text = "Player";
            PNGrid.create_headers();

            int Old = NamesPanel.Height,
                New = PlayersGrid.Bottom + 20;
            NamesPanel.Height = New;
            IBPanel.Top += New - Old;
            this.Height += New - Old;

            for (int i = 0; i < Information.PlayersNames.Count; i++)
            {
                PlayersGrid[i, 0].Value = Information.PlayersNames[i];
                Graphic_Interface.Analyzer.ResizeColumn(this, true, PlayersGrid, 0, i, Information.PlayersNames[i], -1);
            }
        }



        private void IrrationalBehaviour()
        {
            Graphic_Interface.Grid IBGrid = new Graphic_Interface.Grid(PIBGrid, 1, Information.players_number, "T", "TWDN");
            IBGrid.fontsize = 12;
            IBGrid.initialize();
            PIBGrid.Rows[0].HeaderCell.Value = "Probability";
            IBGrid.column_header_text = "Player";
            IBGrid.create_headers();

            //Correct number of elements in Irrational Behaviour
            for (int j = Information.PlayersIrrationalBehaviour.Count; j < Information.players_number; j++)
                Information.PlayersIrrationalBehaviour.Add(0);

            for (int i = 0; i < Information.players_number; i++)
                PIBGrid.Rows[0].Cells[i].Value = Information.PlayersIrrationalBehaviour[i];

            //IBPanel.Height = PIBGrid.Bottom + 20;
            //IBPanel.Width = PIBGrid.Right + 20;
        }
//Debug//
        public void SaveButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Information.players_number; i++)
            {
                Information.PlayersIrrationalBehaviour[i] = Convert.ToDouble(PIBGrid.Rows[0].Cells[i].Value);
                if (PlayersGrid[i, 0].Value == null)
                    Information.PlayersNames[i] = "";
                else
                    Information.PlayersNames[i] = PlayersGrid[i, 0].Value.ToString();
            }
            this.Close();
        }

        private void PIBGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string result = Graphic_Interface.Analyzer.CheckValidStringDouble
                (PIBGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), 0, 1, true);

            if (result == "")
                PIBGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Information.PlayersIrrationalBehaviour[e.ColumnIndex];
            else
                PIBGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = result;
        }

        private void PlayersGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Graphic_Interface.Analyzer.ResizeColumn(this,true, PlayersGrid, 0,
                e.ColumnIndex, PlayersGrid[e.ColumnIndex, 0].Value.ToString(),-1);
        }

        private void Settings_Resize(object sender, EventArgs e)
        {
            NamesPanel.Width += this.Width - width;
            IBPanel.Width += this.Width - width;
            IBPanel.Height = this.Height - IBPanel.Top - 50;
            width = this.Width;
            height = this.Height;

        }

    }
}
