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
    public partial class Alternatives : Form
    {
        String[] DefaultValues = {"","units","0","100"};
        int DeletedCriterionIndex = 0,
            DeletedAlternativeIndex = 0;
        public Alternatives()
        {
            InitializeComponent();

            CreateGrids(1);
        }

        private void CreateGrids(int CritGridRowsNumber)
        {
            //Criteria Grid
            Graphic_Interface.Grid C = new Graphic_Interface.Grid(CritGrid, CritGridRowsNumber, 4, "N", "T");
            C.initialize();
            CritGrid.AllowUserToResizeRows = true;
            CritGrid.TopLeftHeaderCell.Value = "№";
            CritGrid.Columns[0].HeaderText = "Criterion";
            CritGrid.Columns[1].HeaderText = "Units of \n measurement";
            CritGrid.Columns[2].HeaderText = "Worst value";
            CritGrid.Columns[3].HeaderText = "Best value";
            C.create_headers();

            for (int i = 0; i < CritGridRowsNumber; i++)
                for (int j = 0; j < 4; j++)
                    CritGrid[j, i].Value = DefaultValues[j];

            //Alternatives Grid
            Graphic_Interface.Grid A = new Graphic_Interface.Grid(AltGrid, 1, 1, "N", "T");
            A.initialize();
            AltGrid.TopLeftHeaderCell.Value = "№";
            AltGrid.Columns[0].HeaderText = "";
            A.create_headers();
        }

        private void AddCritRow()
        {
            int H = CritGrid.Rows[0].Height;

            DataGridViewRow row = new DataGridViewRow();
            DataGridViewCell[] Cells = new DataGridViewCell[4];
            for (int cind = 0; cind < 4; cind++)
                Cells[cind] = new DataGridViewTextBoxCell();

            row.Cells.AddRange(Cells);
            for (int j = 0; j < 4; j++)
                row.Cells[j].Value = DefaultValues[j];
            
            CritGrid.Rows.Add(row);
            CritGrid.Rows[CritGrid.RowCount - 1].Height = H;
            CritGrid.Rows[CritGrid.RowCount - 1].HeaderCell.Style.Font = new Font("Bookman Old Style", 10, System.Drawing.FontStyle.Bold);
            CritGrid.Rows[CritGrid.RowCount - 1].HeaderCell.Value = CritGrid.RowCount.ToString();
            CritGrid.Rows[CritGrid.RowCount - 1].DefaultCellStyle.Alignment =
                            DataGridViewContentAlignment.MiddleCenter;

            CritPanel.Height += H;
            AltPanel.Top += H;
            DoneButton.Top += H;
            if (this.Height < 600)
            {
                CritGrid.Height += H;
                this.Height += H;
            }
        }

        private void AddAltRow()
        {
            int H = AltGrid.Rows[0].Height;

            DataGridViewRow row = new DataGridViewRow();
            DataGridViewCell[] Cells = new DataGridViewCell[CritGrid.Rows.Count - 1];
            for (int cind = 0; cind < CritGrid.Rows.Count - 1; cind++)
                Cells[cind] = new DataGridViewTextBoxCell();

            row.Cells.AddRange(Cells);
            
            AltGrid.Rows.Add(row);
            AltGrid.Rows[AltGrid.RowCount - 1].Height = H;
            AltGrid.Rows[AltGrid.RowCount - 1].HeaderCell.Style.Font = new Font("Bookman Old Style", 10, System.Drawing.FontStyle.Bold);
            AltGrid.Rows[AltGrid.RowCount - 1].HeaderCell.Value = AltGrid.RowCount.ToString();
            AltGrid.Rows[AltGrid.RowCount - 1].DefaultCellStyle.Alignment =
                            DataGridViewContentAlignment.MiddleCenter;

            AltPanel.Height += H;
            DoneButton.Top += H;
            if (this.Height < 600)
            {
                AltGrid.Height += H;
                this.Height += H;
            }
        }

        private void EditAltGrid(int CriterionIndex)
        {
            if (CriterionIndex == CritGrid.Rows.Count - 1)
            {
                DataGridViewTextBoxCell C = new DataGridViewTextBoxCell();
                AltGrid.Columns.Add(new DataGridViewTextBoxColumn());
                AltGrid.Columns[AltGrid.Columns.Count - 1].Width = 30;
                AltGrid.Width += AltGrid.Columns[AltGrid.Columns.Count - 1].Width;
            }

            int OldSize = AltGrid.Columns[CriterionIndex].Width,
                NewSize = TextRenderer.MeasureText
                (CritGrid[0, CriterionIndex].Value.ToString(), AltGrid.Font).Width + 10;

            AltGrid.Columns[CriterionIndex].HeaderText = CritGrid[0, CriterionIndex].Value.ToString();
            AltGrid.Columns[CriterionIndex].Width = NewSize;
            AltGrid.Width += NewSize - OldSize;

            if (AltGrid.Right > this.Width)
            {
                this.Width = AltGrid.Right + 40;
                CritGrid.Left = (this.Width - CritGrid.Width) / 2;
                CritLabel.Left = (this.Width - CritLabel.Width) / 2;
                AltLabel.Left = (this.Width - CritLabel.Width) / 2;
                DoneButton.Left = this.Width - DoneButton.Width - 100;
            }
        }

        private void CritGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int r = e.RowIndex;
                if (CritGrid[0, r].Value != null)
                {
                    int max = TextRenderer.MeasureText(CritGrid.Columns[0].HeaderText,
                            CritGrid.Font).Width + 10;

                    for (int i = 0; i < CritGrid.Rows.Count; i++)
                        max = Math.Max(max, TextRenderer.MeasureText
                            (CritGrid[0, i].Value.ToString(), CritGrid.Font).Width + 10);
                    int diff = max - CritGrid.Columns[0].Width;
                    CritGrid.Columns[0].Width = max;
                    CritGrid.Width += diff;

                    this.Width += diff;
                    CritLabel.Left = (this.Width - CritLabel.Width) / 2;
                    AltLabel.Left = (this.Width - CritLabel.Width) / 2;
                    Size s = TextRenderer.MeasureText(CritGrid[0, r].Value.ToString(), CritGrid.Font);

                    EditAltGrid(r);
                    
                    if (r == CritGrid.Rows.Count - 1)
                        AddCritRow();
                }
            }
        }

        private void AltGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (AltGrid[e.ColumnIndex, e.RowIndex].Value != null)
            {
                if (e.RowIndex == AltGrid.Rows.Count - 1)
                {
                    AddAltRow();
                }
            }
        }

        private void CritGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if ((e.RowIndex < CritGrid.Rows.Count - 1) && (CritGrid.Rows.Count > 1))
                {
                    ContextMenuStrip CMS = new System.Windows.Forms.ContextMenuStrip();
                    CMS.Items.Add("Delete");
                    CMS.Items[0].Click += new EventHandler(DeleteCriterion);
                    DeletedCriterionIndex = e.RowIndex;
                    CritGrid[e.ColumnIndex, e.RowIndex].ContextMenuStrip = CMS;
                    CritGrid[e.ColumnIndex, e.RowIndex].ContextMenuStrip.Show(Cursor.Position);
                }
            }
        }

        private void AltGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if ((e.RowIndex < AltGrid.Rows.Count - 1) && (AltGrid.Rows.Count > 1))
                {
                    ContextMenuStrip CMS = new System.Windows.Forms.ContextMenuStrip();
                    CMS.Items.Add("Delete");
                    CMS.Items[0].Click += new EventHandler(DeleteAlternative);
                    DeletedAlternativeIndex = e.RowIndex;
                    AltGrid[e.ColumnIndex, e.RowIndex].ContextMenuStrip = CMS;
                    AltGrid[e.ColumnIndex, e.RowIndex].ContextMenuStrip.Show(Cursor.Position);
                }
            }
        }

        private void DeleteCriterion(object sender, EventArgs e)
        {
            CritGrid.Rows.RemoveAt(DeletedCriterionIndex);

            for (int i = 0; i < CritGrid.Rows.Count; i++)
                CritGrid.Rows[i].HeaderCell.Value = (i + 1).ToString();

            CritGrid.Height -= CritGrid.Rows[0].Height;
            AltPanel.Top -= CritGrid.Rows[0].Height;
            DoneButton.Top -= CritGrid.Rows[0].Height;
        }

        void DeleteAlternative(object sender, EventArgs e)
        {
            AltGrid.Rows.RemoveAt(DeletedAlternativeIndex);

            for (int i = 0; i < AltGrid.Rows.Count; i++)
                AltGrid.Rows[i].HeaderCell.Value = (i + 1).ToString();

            AltGrid.Height -= CritGrid.Rows[0].Height;
            DoneButton.Top -= CritGrid.Rows[0].Height;
        }

        private bool CheckCritGrid()
        {
            for (int i = 0; i < CritGrid.Rows.Count - 1; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (CritGrid[j, i].Value != null)
                    {
                        if (j > 1)
                        {
                            string result = Graphic_Interface.Analyzer.CheckValidStringDouble
                                (CritGrid[j, i].Value.ToString(), 0, 0, true);
                            if (result == "")
                                return false;
                        }
                    }
                    else
                        return false;
                }
            return true;
        }

        private bool CheckAltGrid()
        {
            for (int i = 0; i < AltGrid.Rows.Count; i++)
                for (int j = 0; j < AltGrid.Rows[i].Cells.Count; j++)
                {
                    if (AltGrid[j, i].Value != null)
                    {
                        string CellValue = AltGrid[j, i].Value.ToString();

                        int MinValue = Convert.ToInt32(CritGrid[3, j].Value),
                            MaxValue = Convert.ToInt32(CritGrid[4, j].Value);

                        string result = Graphic_Interface.Analyzer.CheckValidStringDouble
                            (AltGrid[j, i].Value.ToString(), Math.Min(MinValue, MaxValue),
                            Math.Max(MinValue, MaxValue), true);


                        if (result == "")
                            return false;
                    }
                }
            return true;
        }

        private void SaveData()
        {
            ModellingInformation.CritNumber = CritGrid.Rows.Count-1;
            for (int i = 0; i < CritGrid.Rows.Count - 1; i++)
            {
                ModellingInformation.CritNames.Add(CritGrid[0, i].Value.ToString());
                ModellingInformation.Measurements.Add(CritGrid[1, i].Value.ToString());
                ModellingInformation.WorstValues.Add(Convert.ToDouble(CritGrid[2, i].Value));
                ModellingInformation.BestValues.Add(Convert.ToDouble(CritGrid[3, i].Value));
            }

            ModellingInformation.AltNumber = AltGrid.Rows.Count - 1;
            for (int i = 0; i<AltGrid.Rows.Count-1; i++)
            {
                ModellingInformation.Alternatives.Add(new List<double>());
                for (int j = 0; j < AltGrid.Rows[i].Cells.Count; j++)
                {
                    ModellingInformation.Alternatives[i].Add
                        (Convert.ToDouble(AltGrid.Rows[i].Cells[j].Value));
                }
            }
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            if (CheckCritGrid())
            {
                if (CheckAltGrid())
                {
                    SaveData();
                }
            }            
        }
        

    }
}
