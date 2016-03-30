using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using System.IO;

namespace Graphic_Interface
{
    public class Function
    {
        public Function(string Formula)
        {
            s = Formula;
        }

        public bool GetAllParameterNames(List<double> Values)
        {
            ParametersNames.Clear();
            for (int i = 0; i < s.Length; i++)
            {
                if ((s[i] < '0') || (s[i] > '9'))
                    if ((s[i] != '+') && (s[i] != '-') && (s[i] != '*') && (s[i] != '/')
                        && (s[i] != '^') && (s[i] != '(') && (s[i] != ')') && (s[i] != '.') && (s[i] != ','))
                    {
                        string Name = s[i].ToString();
                        for (int j = i + 1; j < s.Length; j++)
                        {
                            if ((s[j] != '+') && (s[j] != '-') && (s[j] != '*') && (s[j] != '/')
                                && (s[j] != '^') && (s[j] != '(') && (s[j] != ')') && (s[i] != '.') && (s[i] != ','))
                                Name += s[j];
                            else
                                break;                            
                        }
                        bool DoppelBanger = false,
                            ExistingName = false;
                        for (int k = 0; k < ParametersNames.Count; k++)
                        {
                            if (Name == ParametersNames[k])
                            {
                                DoppelBanger = true;
                                break;
                            }
                        }
                        if (!DoppelBanger)
                        {
                            for (int k = 0; k < SequentialGames.Information.AP_KeyLetters.Count; k++)
                            {
                                if (Name == SequentialGames.Information.AP_KeyLetters[k])
                                {
                                    ExistingName = true;
                                    ParametersValues.Add(Values[k]);
                                    ParametersNames.Add(Name);
                                    break;
                                }
                            }
                            if (!ExistingName)
                            {
                                string ErrorMsg = "Assigned parameters are:";
                                for (int k = 0; k < SequentialGames.Information.AP_KeyLetters.Count; k++)
                                    ErrorMsg += " " + SequentialGames.Information.AP_KeyLetters[k];

                                System.Windows.Forms.MessageBox.Show(ErrorMsg, "Unassigned parameters used.");
                                return false;
                            }
                        }
                    }
            }
            return true;
        }

        private string DoOperation()
        {
            double result = 0;
            string First = Analyzer.CheckValidStringDouble(FirstNumber, 0, 0, false),
                Second = Analyzer.CheckValidStringDouble(SecondNumber, 0, 0, false);

            if (First == "")
            {
                bool minus = false;
                if (FirstNumber[0] == '-')
                {
                    minus = true;
                    FirstNumber = FirstNumber.Substring(1);
                }                

                for (int i = 0; i < ParametersNames.Count; i++)
                {
                    if (ParametersNames[i] == FirstNumber)
                    {
                        First = ParametersValues[i].ToString();
                        break;
                    }
                }

                if (minus)
                    First = '-' + First;
            }
            if (Second == "")
            {
                for (int i = 0; i < ParametersNames.Count; i++)
                {
                    if (ParametersNames[i] == SecondNumber)
                    {
                        Second = ParametersValues[i].ToString();
                        break;
                    }
                }
            }

            switch (Operator)
            {
                case '+':
                    {
                        result = Convert.ToDouble(First) + Convert.ToDouble(Second);
                        ValueCalculated = true;
                    }
                    break;
                case '-':
                    {
                        result = Convert.ToDouble(First) - Convert.ToDouble(Second);
                        ValueCalculated = true;
                    }
                    break;
                case '*':
                    {
                        result = Convert.ToDouble(First) * Convert.ToDouble(Second);
                        ValueCalculated = true;
                    }
                    break;
                case '/':
                    {
                        result = Convert.ToDouble(First) / Convert.ToDouble(Second);
                        ValueCalculated = true;
                    }
                    break;
            }
            return result.ToString();
        }

        private int HandleSymbol(char symbol, ref int ParameterNameIndex)
        {
            switch (symbol)
            {
                case '*':
                case '/':
                    {
                        if (FirstNumber != "")
                        {
                            if (Operator != ' ')
                                FirstNumber = DoOperation();
                            else
                                FirstNumber = SecondNumber;
                        }
                        else
                            FirstNumber = SecondNumber;

                        if (!ValueCalculated)
                            SecondNumber = "";
                        Operator = symbol;
                        ParameterNameIndex = 0;
                    }
                    break;
                case '+':
                case '-':
                    {
                        if ((symbol == '-') && (SecondNumber == ""))
                        {
                            SecondNumber += symbol;
                        }
                        else
                        {
                            if (FirstNumber != "")
                            {
                                if (Operator != ' ')
                                    FirstNumber = DoOperation();
                                else
                                    FirstNumber = SecondNumber;
                            }
                            else
                                FirstNumber = SecondNumber;

                            if (!ValueCalculated)
                                SecondNumber = "";

                            ParameterNameIndex = 0;
                            if (PrimaryOpComplete)
                                Operator = symbol;
                            if (Operator == ' ')
                                return -1;
                        }
                    }
                    break;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case ',':
                case '.':
                    {
                        SecondNumber += symbol;
                    }
                    break;
                default:
                    {
                        for (int k = 0; k < ParametersNames.Count; k++)
                        {
                            if (ParametersNames[k].Length > ParameterNameIndex)
                            {
                                if (ParametersNames[k][ParameterNameIndex] == symbol)
                                {
                                    SecondNumber += symbol;
                                    ParameterNameIndex++;
                                    break;
                                }
                            }
                        }
                    }
                    break;
            }
            return 0;
        }

        private void ClearValues()
        {
            FirstNumber = "";
            SecondNumber = "";
            Operator = ' ';
            
        }

        private string CheckPlusMinus(string s)
        {
            for (int i = 0; i < s.Length - 1; i++)
            {
                if ((s[i] == '+') && (s[i + 1] == '-'))
                    return (CheckPlusMinus(s.Substring(0, i) + s.Substring(i + 1)));
            }
            return s;
        }

        public string CalculateValue()
        {
            if (DivideEquationIntoBrackets())
            {
                for (int i = 0; i < Brackets.Count; i++)
                {
                    string b = Brackets[i];
                    bool ResultCalculated = false;
                    while (!ResultCalculated)
                    {
                        int ParameterNameIndex = 0,
                            OperatorsCount = 0,
                            LastOpPosition = 0;
                        PrimaryOpComplete = true;
                        ValueCalculated = false;
                        ClearValues();
                        for (int j = 0; j < b.Length; j++)
                        {
                            if ((b[j] == '*') || (b[j] == '/'))
                            {
                                PrimaryOpComplete = false;
                                OperatorsCount++;
                            }
                            if (b[j] == '+')                            
                                OperatorsCount++;
                            if ((b[j] == '-') && (j != 0))
                                OperatorsCount++;
                        }

                        if (OperatorsCount == 1)
                            ResultCalculated = true;

                        for (int j = 0; j < b.Length; j++)
                        {
                            if (HandleSymbol(b[j], ref ParameterNameIndex) == -1)
                                LastOpPosition = j;
                            if (ValueCalculated)
                            {
                                string New = "";
                                if (LastOpPosition != 0)
                                    New += b.Substring(0, LastOpPosition + 1);
                                b = New + FirstNumber + b.Substring(j);
                                b = CheckPlusMinus(b);
                                ClearValues();
                                break;
                            }
                        }
                        if ((FirstNumber != "")&&(SecondNumber!=""))
                        {
                            string Result = DoOperation();
                            if ((ValueCalculated) && (!ResultCalculated))
                            {
                                b = b.Substring(0, b.Length - FirstNumber.Length -
                                    SecondNumber.Length - 1) + Result;
                                b = CheckPlusMinus(b);
                                ClearValues();
                            }
                            else
                            {
                                ParametersNames.Add("B" + (i + 1).ToString());
                                ParametersValues.Add(Convert.ToDouble(Result));
                            }
                        }
                    }
                }               
            }
            return ParametersValues.Last().ToString();
        }

        private bool DivideEquationIntoBrackets()
        {
            int Open = 0, Close = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                    Open++;
                if (s[i] == ')')
                    Close++;
            }

            if (Open == Close)
            {
                bool Done = false;
                string eq = s;
                while (!Done)
                {
                    Done = true;
                    int Left = 0;
                    for (int i = 0; i < eq.Length; i++)
                    {
                        if (eq[i] == '(')
                        {
                            Left = i;
                        }
                        if (eq[i] == ')')
                        {
                            Brackets.Add(eq.Substring(Left + 1, i - Left - 1));
                            string NewEq = eq.Substring(0, Left);
                            NewEq += "B" + Brackets.Count;
                            NewEq += eq.Substring(i + 1);
                            eq = NewEq;
                            Done = false;
                            break;
                        }
                    }
                }
                s = eq;
                Brackets.Add(s);
                return true;
            }
            else
                System.Windows.Forms.MessageBox.Show("Opening brackets number is not equal to the closing");
            return false;
        }

        public string s = "";
        public List<string> ParametersNames = new List<string>();
        public List<double> ParametersValues = new List<double>();

        private char Operator = ' ';
        private string FirstNumber = "",
            SecondNumber = "";
        private List<string> Brackets = new List<string>();

        private bool ValueCalculated,
            PrimaryOpComplete;
    }
    
    class Grid
    {
        private DataGridView datagrid;
        private int n, m;
        private string column_header_type = "numbers";
        private string row_header_type = "numbers";

        public bool read_only = false;
        public string fontname = "Book Antiqua";
        public int fontsize = 12;
        public Font font = new Font("Book Antiqua", 12);
        public int cellsize = 30;

        public int header_height = 30;
        public string column_header_text = "";
        public string row_header_text = "";
        public int first_plnum = 0;  // for Strategies
        public int second_plnum = 1; //    headers
        public List<string> letters = new List<string>();
        public int CheckBoxColumnsCount = 0;
        public int ComboBoxColumnsCount = 0;
        public int ComboBoxRowsCount = 0;
        public string checkbox_header = "";
        public List<List<string>> combobox_items;
        public int gridsize = 0; // 1 for payoff
        
        public Grid(DataGridView dg, int rc, int cc, string row_htype, string col_htype)
        {
            datagrid = dg;
            n = rc;
            m = cc;
            column_header_type = col_htype;
            row_header_type = row_htype;
        }

        private void create_columns()
        {
            DataGridViewTextBoxColumn c1;

            for (int cind = 0; cind<ComboBoxColumnsCount; cind++)
            {
                DataGridViewComboBoxColumn cb = new DataGridViewComboBoxColumn();
                cb.Width = 120;
                datagrid.Columns.Add(cb);
            }

            for (int cind = 0; cind < m; cind++)
            {
                c1 = new DataGridViewTextBoxColumn();
                c1.Width = cellsize;
                datagrid.Columns.Add(c1);
            }

            if (CheckBoxColumnsCount == 1)
            {
                DataGridViewCheckBoxColumn ch = new DataGridViewCheckBoxColumn();
                ch.HeaderText = checkbox_header;
                ch.Width = checkbox_header.Length * 15 + 30;
                datagrid.Columns.Add(ch);
            }



        }

        private void create_rows()
        {
            DataGridViewRow row;
            for (int rind = 0; rind < n; rind++)
            {
                row = new DataGridViewRow();

                if (CheckBoxColumnsCount == 1)
                {
                    DataGridViewCheckBoxCell chcell = new DataGridViewCheckBoxCell();
                    chcell.Value = false;
                    row.Cells.Add(chcell);
                }


                for (int i = 0; i < ComboBoxColumnsCount; i++)
                {
                    DataGridViewComboBoxCell cbcell = new DataGridViewComboBoxCell();
                    for (int j = 0; j < combobox_items[i].Count; j++)
                        cbcell.Items.Add(combobox_items[i][j]);
                    row.Cells.Add(cbcell);
                }


                if (m != 0)
                {
                    DataGridViewCell[] Cells = new DataGridViewCell[m];
                    for (int cind = 0; cind < m; cind++)
                        Cells[cind] = new DataGridViewTextBoxCell();

                    row.Cells.AddRange(Cells);
                }               

                datagrid.Rows.Add(row);
                datagrid.Rows[rind].Height = cellsize;
                datagrid.Rows[rind].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            for (int i = 0; i < ComboBoxRowsCount; i++)
            {
                row = new DataGridViewRow();
                for (int cind = 0; cind < m; cind++)
                {
                    DataGridViewComboBoxCell cbcell = new DataGridViewComboBoxCell();
                    for (int j = 0; j < combobox_items[cind].Count; j++)
                        cbcell.Items.Add(combobox_items[cind][j]);
                    row.Cells.Add(cbcell);
                }
            }
        }

        public void initialize()
        {
            datagrid.Rows.Clear();
            datagrid.Columns.Clear();
            if (font != null)
                datagrid.Font = font;
            else
                datagrid.Font = new Font(fontname, fontsize);
            datagrid.ReadOnly = read_only;
            datagrid.AutoGenerateColumns = false;
            datagrid.AllowUserToAddRows = false;

            create_columns();
            create_rows();
        }

        private void fill_headers_with_text()
        {
            switch (column_header_type)
            {
                case "N":
                    for (int i = 0; i < m; i++)
                        datagrid.Columns[i].HeaderText = (i + 1).ToString();
                    break;
                case "S":
                    {
                        for (int i = 0; i < m; i++)
                        {
                            datagrid.Columns[i].HeaderCell.Value = letters[second_plnum].ToString() + (i + 1).ToString();
                            datagrid.Columns[i].Width = cellsize + cellsize / 2;
                        }
                        datagrid.TopLeftHeaderCell.Value = "Strategies";
                    }
                    break;
                case "T":
                    for (int i = 0; i < m; i++)
                    {
                        Size s = TextRenderer.MeasureText(datagrid.Columns[i].HeaderCell.Value.ToString(), datagrid.Font);
                        if (datagrid.Columns[i].HeaderCell.Value.ToString() == "")
                            datagrid.Columns[i + ComboBoxColumnsCount].Width = cellsize;
                        else
                            datagrid.Columns[i+ComboBoxColumnsCount].Width = s.Width + 10;                        
                    }
                    break;
                case "TWDN":
                    for (int i = 0; i < m; i++)
                    {
                        datagrid.Columns[i].HeaderText = column_header_text + " " + (i + 1).ToString();
                        Size s = TextRenderer.MeasureText(datagrid.Columns[i].HeaderCell.Value.ToString(), datagrid.Font);
                        datagrid.Columns[i].Width = s.Width + 10;
                    }
                    break;
            }

            switch (row_header_type)
            {
                case "N":
                    for (int i = 0; i < n; i++)
                        datagrid.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    break;
                case "S":
                    for (int i = 0; i < n; i++)
                        datagrid.Rows[i].HeaderCell.Value = letters[first_plnum].ToString() + (i + 1).ToString();
                    break;
                case "T":
                    break;
                case "TWDN":
                    for (int i = 0; i < n; i++)
                        datagrid.Rows[i].HeaderCell.Value = row_header_text + " " + (i + 1).ToString();
                    break;
            }
        }

        public void align_and_width()
        {
            datagrid.ColumnHeadersHeight = header_height;
            for (int i = 0; i < m; i++)
            {
                datagrid.Columns[i].HeaderCell.Style.Font = new Font(fontname, fontsize, System.Drawing.FontStyle.Bold);
                datagrid.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            int hrow_width = 0;
            if (datagrid.TopLeftHeaderCell.Value != null)
                hrow_width = TextRenderer.MeasureText(datagrid.TopLeftHeaderCell.Value.ToString(),
                 datagrid.TopLeftHeaderCell.Style.Font).Width + 10;
            for (int i = 0; i < n; i++)
            {
                if (datagrid.Rows[i].HeaderCell.Value != null)
                {
                    datagrid.Rows[i].HeaderCell.Style.Font = new Font(fontname, fontsize, System.Drawing.FontStyle.Bold);
                    hrow_width = Math.Max((TextRenderer.MeasureText(datagrid.Rows[i].HeaderCell.Value.ToString(), datagrid.Rows[i].HeaderCell.Style.Font)).Width + 50, hrow_width);
                    datagrid.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            datagrid.RowHeadersWidth = hrow_width;
        }

        public void gridsize_correction()
        {
            int Height = datagrid.ColumnHeadersHeight + 2, Width = 2 + datagrid.TopLeftHeaderCell.Size.Width;
            for (int i = 0; i < datagrid.Rows.Count; i++)
                Height += datagrid.Rows[i].Height;
            for (int i = 0; i < datagrid.Columns.Count; i++)
                Width += datagrid.Columns[i].Width;

            Width += CheckBoxColumnsCount * (checkbox_header.Length * 15 + 30);

            if (Height > 230)
            {
                Width += 17;
                Height = 230;
                //datagrid.ScrollBars = ScrollBars.Vertical;
            }
            if ((gridsize == 1) && (Width > 400))
            {
                Height += 17;
                Width = 400;
            }
            if ((gridsize == 0) && (Width > 600))
            {
                Height += 17;
                Width = 600;
            }
            datagrid.Height = Height;
            datagrid.Width = Width;
        }
        
        public void create_headers()
        {
            letters.Add("x"); letters.Add("y");
            letters.Add("z"); letters.Add("w");
            letters.Add("v");

            fill_headers_with_text();
            align_and_width();

            datagrid.TopLeftHeaderCell.Style.Font = new Font(fontname, fontsize, System.Drawing.FontStyle.Bold);
            datagrid.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridsize_correction();
        }
    }

    public class Line
    {
        public Point p0;
        public Point p1;
        public Point p2;
        public Point p3;
        public PictureBox pb;

        public List<Label> labels = new List<Label>();
        public List<Color> player_colors = new List<Color>();

        private Pen p;
        private Graphics g;
        private Bitmap bmp;

        public Button source;
        public Button destination;

        public Line(PictureBox pb_input, Bitmap bmp_input, Graphics g_input, Point top, Point middle, Point aside, Point bottom)
        {
            pb = pb_input;
            p0 = top;
            p1 = middle;
            p2 = aside;
            p3 = bottom;
            g = g_input;
            bmp = bmp_input;
            //N = pl_num;
            //for (int i = 0; i<N; i++)
            //    player_colors.Add(Color.Black);
            
            ReDraw(g,bmp);
        }

        public void ReDraw(Graphics g, Bitmap bmp)
        {
            p = new Pen(Color.Black, 1);

            g.DrawLine(p, p0, p1);
            g.DrawLine(p, p1, p2);
            g.DrawLine(p, p2, p3);

            pb.Image = bmp;
        }

        public void write_strategies(List<string> Strategies)
        {
            int diff = Strategies.Count - labels.Count;

            if (diff > 0)
            {
                int lc = labels.Count;
                for (int i = lc; i < Strategies.Count; i++)
                {
                    Label l = new Label();
                    labels.Add(l);
                }
            }
            else
            {
                int lc = labels.Count;
                for (int i = Strategies.Count; i < lc; i++)
                {
                    labels[Strategies.Count].Dispose();
                }
            }

            for (int i = 0; i < Strategies.Count; i++)
            {
                labels[i].Text = Strategies[i];
                labels[i].Font = new Font("Bookman Old Style", 8);
                labels[i].Size = TextRenderer.MeasureText(labels[i].Text, labels[i].Font);
                labels[i].Left = p3.X;
                if (p2.X < p1.X)
                    labels[i].Left -= labels[i].Width;
                else
                    labels[i].Left += 5;


                labels[i].ForeColor = Color.Black; //player_colors[Math.Min(Strategies.Count, N) - i - 1];
                labels[i].Top = p3.Y - (Strategies.Count - i) * labels[i].Height;
                pb.Controls.Add(labels[i]);
            }
        }
    }

    public class PositionFileOpening
    {
        private OpenFileDialog dialog;
        private StreamReader sr;
        private string header = "Sequential games v.1.0";
        private int pl1, pl2, DimensionsCount = 1;
        public SequentialGames.GamePosition gp = new SequentialGames.GamePosition();

        public PositionFileOpening(OpenFileDialog dialog_input)
        {
            dialog = dialog_input;
        }

        public PositionFileOpening()
        {

        }

        private bool ReadCommonData(int type, char symb)
        {
            string s = sr.ReadLine();
            int colon_pos = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == symb)
                {
                    colon_pos = i;
                    break;
                }
            }

            if ((colon_pos == 0) || (colon_pos == s.Length - 1))
            {
                System.Windows.Forms.MessageBox.Show("File is corrupted: no colon/no symbols after colon");
                return false;
            }
            else
            {
                if (type == 0)
                {
                    string s_res = s.Substring(colon_pos + 2);
                    gp.N = Convert.ToInt32(s_res);
                    return true;
                }
                else
                {
                    char Separator = '[';
                    for (int i = 0; i < gp.N; i++)
                    {
                        s = s.Substring(Analyzer.FindSeparator(s, Separator) + 1);
                        Separator = ' ';
                        if (i == gp.N - 1)
                            Separator = ']';
                        string Number = s.Substring(0, Analyzer.FindSeparator(s, Separator));
                        string check_res = Analyzer.CheckValidStringInt(Number, 1, 99, false);
                        if (check_res != "")
                            gp.Strategies.Add(Convert.ToInt32(check_res));
                        else
                        {
                            if (Number == "-")
                                gp.Strategies.Add(0);
                            else
                                return false;
                        }
                    }
                    return true;
                }

            }
        }

        private void CreatePayoffs()
        {
            for (int d = 1; d < DimensionsCount; d++)
                gp.CreateNewPayoffLayer();

            for (int d = 0; d < DimensionsCount; d++)
            {
                for (int i = 0; i < gp.N; i++)
                {
                    gp.payoffs[d].Array.Add(new List<List<List<string>>>());
                    for (int j = 0; j < gp.N; j++)
                    {
                        gp.payoffs[d].Array[i].Add(new List<List<string>>());
                        if (i != j)
                        {
                            int n = gp.Strategies[i], m = gp.Strategies[j];
                            if ((n != -1) && (m != -1))
                            {
                                if (i > j)
                                {
                                    n = gp.Strategies[j];
                                    m = gp.Strategies[i];
                                }

                                for (int k = 0; k < n; k++)
                                {
                                    gp.payoffs[d].Array[i][j].Add(new List<string>());
                                    for (int l = 0; l < m; l++)
                                        gp.payoffs[d].Array[i][j][k].Add("");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DefinePlayers()
        {
            string h = sr.ReadLine();
            int pos1 = 0, pos2 = 0;
            for (int i = 0; i < h.Length; i++)
            {
                if (h[i] == '(')
                    pos1 = i + 1;
                if (h[i] == ')')
                    pos2 = i - 1;
            }

            int k = 0;
            while (h[k + pos1] != ' ')
                k++;
            pl1 = Convert.ToInt32(h.Substring(pos1, k)) - 1;

            k = 0;
            while (h[k + pos2] != ')')
                k++;
            pl2 = Convert.ToInt32(h.Substring(pos2, k)) - 1;
        }

        private bool ReadMatrixes(int d, int pl1, int pl2)
        {
            int str1 = gp.Strategies[Math.Min(pl1, pl2)],
                str2 = gp.Strategies[Math.Max(pl1, pl2)];
            for (int i = 0; i < str1; i++)
            {
                string s = sr.ReadLine();
                int pos1 = 0, pos2 = 0;
                for (int j = 0; j < str2; j++)
                {
                    for (int k = pos1; k < s.Length; k++)
                    {
                        if (s[k] == ' ')
                        {
                            pos2 = k;
                            break;
                        }
                    }

                    if (pos1 == pos2 + 1)
                        pos2 = s.Length;
                    string Value = s.Substring(pos1, pos2 - pos1);
                    pos1 = pos2 + 1;
                    gp.payoffs[d].Array[pl1][pl2][i][j] = Value;
                }
            }
            return true;
        }

        private bool ReadHeader()
        {
            string s = sr.ReadLine();
            if (header.Length != s.Length)
            {
                System.Windows.Forms.MessageBox.Show("Invalid encoding. Use 'Unicode' for Russian and 'ASCII' for English");
                return false;
            }
            else
            {
                for (int i = 0; i < header.Length; i++)
                {
                    if (header[i] != s[i])
                        return false;
                }
            }
            sr.ReadLine();
            if (ReadCommonData(0, ':') && ReadCommonData(1, '['))
            {
                s = sr.ReadLine();
                DimensionsCount = Convert.ToInt32(s.Substring(Analyzer.FindSeparator(s, ':') + 2));
                sr.ReadLine();
                sr.ReadLine();
            }

            return true;
        }

        private bool ParametersOk()
        {
            List<List<string>> Parameters = new List<List<string>>();
            bool end = false;

            if (sr.ReadLine() == "-------------------")
                end = true;
            else
            {
                string t = sr.ReadLine();
                string q = sr.ReadLine();
                string s = sr.ReadLine();
                int Minus = Analyzer.FindSeparator(s, '-'),
                    Left = Analyzer.FindSeparator(s, '('),
                    Right = Analyzer.FindSeparator(s, ')');
                Parameters.Add(new List<string>());
                Parameters.Last().Add(s.Substring(Minus + 2, Left - Minus - 3));
                Parameters.Last().Add(s.Substring(Left + 1, Right - Left - 1));
                Parameters.Last().Add("");
            }

            while (!end)
            {
                string s = sr.ReadLine();
                if ((s == "----------") || (s == "-------------------"))
                    end = true;
                else
                {
                    int SpacePosition = Analyzer.FindSeparator(s, ' ');
                    string Name = s.Substring(0, SpacePosition),
                        Key = s.Substring(Analyzer.FindSeparator(s, '(') + 1,
                        Analyzer.FindSeparator(s, ')') - Analyzer.FindSeparator(s, '(') - 1),
                        Values = s.Substring(Analyzer.FindSeparator(s, '[') + 1);

                    Parameters.Add(new List<string>());
                    Parameters.Last().Add(Name);
                    Parameters.Last().Add(Key);

                    char Separator = ' ';
                    for (int i = 0; i < gp.N; i++)
                    {
                        if (i == gp.N - 1)
                            Separator = ']';
                        string Value = Values.Substring(0, Analyzer.FindSeparator(Values, Separator)),
                            CheckResult = Analyzer.CheckValidStringDouble(Value, 0, 0, false);
                        if (CheckResult == "")
                            return false;
                        else
                            Parameters.Last().Add(Value);

                        Values = Values.Substring(Analyzer.FindSeparator(Values, ' ') + 1);
                    }
                }
            }
            SequentialGames.Information.AP_Names.Clear();
            SequentialGames.Information.AP_KeyLetters.Clear();
            gp.AdParamValues.Clear();

            for (int i = 0; i < Parameters.Count; i++)
            {
                SequentialGames.Information.AP_Names.Add(Parameters[i][0]);
                SequentialGames.Information.AP_KeyLetters.Add(Parameters[i][1]);
                if (Parameters[i][2] != "")
                {
                    gp.AdParamValues.Add(new List<double>());
                    for (int j = 2; j < Parameters[i].Count; j++)
                        gp.AdParamValues.Last().Add(Convert.ToDouble(Parameters[i][j]));
                }
            }
            if (Parameters.Count > 1)
            {
                //Weights
                sr.ReadLine();
                sr.ReadLine();
                for (int i = 0; i < DimensionsCount; i++)
                {
                    gp.Weights.Add(new List<string>());
                    string s = sr.ReadLine(),
                        Bracket = s.Substring(Analyzer.FindSeparator(s, '[') + 1);
                    char Separator = ' ';
                    for (int j = 0; j < gp.N; j++)
                    {
                        if (j == gp.N - 1)
                            Separator = ']';
                        string Value = Bracket.Substring(0, Analyzer.FindSeparator(Bracket, Separator));
                        gp.Weights[i].Add(Value);

                        Bracket = Bracket.Substring(Analyzer.FindSeparator(Bracket, ' ') + 1);
                    }
                }
                sr.ReadLine();
            }
            else
            {
                gp.Weights.Add(new List<string>());
                for (int i = 0; i < gp.N; i++)
                    gp.Weights[0].Add("1");
            }
            return true;
        }

        private bool ReadPayoffs()
        {
            sr.ReadLine();
            sr.ReadLine();
            if (sr.ReadLine() == "Empty")
                return true;
            CreatePayoffs();
            bool complete = false;
            for (int d = 0; d < DimensionsCount; d++)
            {
                int comb_count = gp.N * (gp.N - 1) / 2;
                for (int i = 0; i < comb_count; i++)
                {
                    if (complete)
                        break;
                    for (int order = 0; order < 2; order++)
                    {
                        DefinePlayers();
                        if (!ReadMatrixes(d, pl1, pl2))
                            return false;
                        sr.ReadLine();
                    }
                }
                sr.ReadLine();
            }
            return true;
        }

        public bool OpenPositionFile(bool OneOfTheFew, string Path, bool manualy)
        {
            bool FileExists = false;
            //if (!manualy)
            //{
            //    //dialog.FileName = "C:\\Users\\Nick\\Desktop\\Diploma\\MDBGF.txt";
            //    FileExists = true;
            //    sr = new StreamReader(dialog.FileName, Encoding.ASCII);
            //}
            //else
            //{
            if (OneOfTheFew)
            {
                FileExists = File.Exists(Path);
                if (FileExists)
                    sr = new StreamReader(Path, Encoding.ASCII);
                else
                    System.Windows.Forms.MessageBox.Show("Invalid paths in the Tree File.");
            }
            else
            {
                dialog.Filter = "Text documents (*.txt)|*.txt";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    FileExists = true;
                    string lang = "eng";
                    if (lang == "eng")
                        sr = new StreamReader(dialog.FileName, Encoding.ASCII);
                }
            }

            if (FileExists)
            {
                if (ReadHeader())
                    if (ParametersOk())
                        if (ReadPayoffs())
                        {
                            gp.TotalDimensions = DimensionsCount;
                            if (OneOfTheFew)
                                gp.SaveFilePath = Path;
                            else
                                gp.SaveFilePath = dialog.FileName;
                            return true;
                        }
                        else
                            System.Windows.Forms.MessageBox.Show("File is corrupted. Unable to load.");
                    else
                        System.Windows.Forms.MessageBox.Show("File is outdated or corrupted.");
            }
            return false;
        }
    }

    public class PositionFileWriting
    {
        public SequentialGames.GamePosition gp;
        private SaveFileDialog dialog;
        private StreamWriter SW;
        private string header = "Sequential games v.1.0";

        bool FileExists = false;

        public PositionFileWriting(SaveFileDialog Dialog, SequentialGames.GamePosition GP)
        {
            dialog = Dialog;
            gp = GP;
        }

        public PositionFileWriting(SequentialGames.GamePosition GP)
        {
            gp = GP;
        }

        private void WriteGeneralPart()
        {
            SW.WriteLine(header);
            SW.WriteLine("-------------------");
            SW.WriteLine("Players: " + gp.N);
            string StratLine = "Strategies: [";
            for (int i = 0; i < gp.N; i++)
            {
                StratLine += gp.Strategies[i];
                if (i < gp.N - 1)
                    StratLine += ' ';
            }
            StratLine += ']';
            SW.WriteLine(StratLine);
            SW.WriteLine("Dimensions: " + gp.TotalDimensions);            
        }

        private void WritePayoffs()
        {
            SW.WriteLine("-------------------");
            SW.WriteLine("Payoffs");
            SW.WriteLine("-------------------");
            bool empty = true;
            for (int i = 0; i < gp.N; i++)
                for (int j = 0; j < gp.N; j++)
                    if (i < j)
                    {
                        for (int k = 0; k < gp.payoffs[0].Array[i][j].Count; k++)
                            for (int l = 0; l < gp.payoffs[0].Array[i][j][k].Count; l++)
                                if ((gp.payoffs[0].Array[i][j][k][l] != "") || (gp.payoffs[0].Array[i][j][k][l] != "0"))
                                {
                                    empty = false;
                                    break;
                                }
                    }

            if (empty)
            {
                SW.WriteLine("-------------------");
                SW.WriteLine("Empty");
            }
            else
            {
                for (int d = 0; d < gp.TotalDimensions; d++)
                {
                    SW.WriteLine(SequentialGames.Information.AP_Names[d] + " dimension");
                    for (int i = 0; i < gp.N; i++)
                        for (int j = 0; j < gp.N; j++)
                            if (i < j)
                            {
                                SW.WriteLine("(" + (i + 1) + " <-> " + (j + 1) + ")");
                                for (int ii = 0; ii < gp.payoffs[d].Array[i][j].Count; ii++)
                                {
                                    string Payoffs = "";
                                    for (int jj = 0; jj < gp.payoffs[d].Array[i][j][ii].Count; jj++)
                                    {
                                        if ((gp.payoffs[d].Array[i][j][ii][jj] == "") || (gp.payoffs[d].Array[i][j][ii][jj] == null))
                                            Payoffs += "0";
                                        else
                                        Payoffs += gp.payoffs[d].Array[i][j][ii][jj];
                                        if (jj < gp.payoffs[d].Array[i][j][ii].Count - 1)
                                            Payoffs += " ";
                                    }
                                    SW.WriteLine(Payoffs);
                                }

                                SW.WriteLine();

                                SW.WriteLine("(" + (j + 1) + " <-> " + (i + 1) + ")");
                                for (int ii = 0; ii < gp.payoffs[d].Array[i][j].Count; ii++)
                                {
                                    string Payoffs = "";
                                    for (int jj = 0; jj < gp.payoffs[d].Array[i][j][ii].Count; jj++)
                                    {
                                        if ((gp.payoffs[d].Array[j][i][ii][jj] == "") || (gp.payoffs[d].Array[j][i][ii][jj] == null))
                                            Payoffs += "0";
                                        else
                                        Payoffs += gp.payoffs[d].Array[j][i][ii][jj];
                                        if (jj < gp.payoffs[d].Array[i][j][ii].Count - 1)
                                            Payoffs += " ";
                                    }
                                    SW.WriteLine(Payoffs);
                                }
                                SW.WriteLine("-------------------");
                            }
                    SW.WriteLine("-------------------");
                }
            }
        }

        private void WriteParametersPart()
        {
            SW.WriteLine("-------------------");
            SW.WriteLine("Parameters");
            if (SequentialGames.Information.AP_Names.Count == 0)
            {
                SequentialGames.Information.AP_Names.Add("Profit");
                SequentialGames.Information.AP_KeyLetters.Add("P");
                SW.WriteLine("----------");
                SW.WriteLine("Values");
                SW.WriteLine("----------");
            }
            SW.WriteLine("Main - " + SequentialGames.Information.AP_Names[0] + " (" +
                SequentialGames.Information.AP_KeyLetters[0] + ")");
            for (int i = 1; i < SequentialGames.Information.AP_Names.Count; i++)
            {
                string s = SequentialGames.Information.AP_Names[i] +
                    " (" + SequentialGames.Information.AP_KeyLetters[i] +
                    "): [";

                if (gp.AdParamValues.Count >= i)
                {
                    for (int j = 0; j < gp.AdParamValues[i - 1].Count; j++)
                    {
                        s += gp.AdParamValues[i-1][j];
                        if (j < gp.AdParamValues[i - 1].Count - 1)
                            s += " ";
                    }
                }
                else
                {
                    for (int j = 0; j<gp.N - 1; j++)
                        s += "0 ";
                    s += "0";
                }
                s += "]";
                SW.WriteLine(s);
            }
        }

        public void WritePositionFile(bool WithDialog, string Path)
        {
            if (WithDialog)
            {
                dialog.Filter = "Text documents (*.txt)|*.txt";
                dialog.RestoreDirectory = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    FileExists = true;
                    Path = dialog.FileName;
                }
            }
            else
                FileExists = File.Exists(Path);

            if (FileExists)
            {
                using (SW = File.CreateText(Path))
                {
                    WriteGeneralPart();
                    WriteParametersPart();
                    WritePayoffs();
                }
            }
            else
                System.Windows.Forms.MessageBox.Show("Invalid paths in the Tree File.");
        }
    }

    public class TreeFileOpening
    {
        private OpenFileDialog dialog;
        private StreamReader sr;
        private List<SequentialGames.GamePosition> 
            GamePositions = new List<SequentialGames.GamePosition>();
        private List<double> InValues = new List<double>();

        private SequentialGames.GamePosition FindPosition(string PositionID)
        {
            SequentialGames.GamePosition Position = new SequentialGames.GamePosition();
            for (int j = 0; j < GamePositions.Count; j++)
                if (GamePositions[j].ID == PositionID)
                    Position = GamePositions[j];

            return Position;
        }

        private bool GeneralPart()
        {
            sr.ReadLine();
            sr.ReadLine();
            sr.ReadLine();
            sr.ReadLine();
            string s = sr.ReadLine();
            string CutString = s.Substring(Analyzer.FindSeparator(s, ':') + 2);
            string CheckResult = Analyzer.CheckValidStringInt
                (CutString, 2, SequentialGames.Information.MaxTreeLevelsCount,true);

            if (CheckResult == "")
                return false;

            SequentialGames.Information.tree_levels = Convert.ToInt32(CheckResult);

            s = sr.ReadLine();
            CutString = s.Substring(Analyzer.FindSeparator(s, ':') + 2);
            if (CutString == "Classic")
            {
                s = sr.ReadLine();
                CutString = s.Substring(Analyzer.FindSeparator(s, ':') + 2);

                CheckResult = Analyzer.CheckValidStringInt
                (CutString, 2, SequentialGames.Information.MaxPlayersNumber,true);

                if (CheckResult == "")
                    return false;

                SequentialGames.Information.players_number = Convert.ToInt32(CheckResult);

                s = sr.ReadLine();
                CutString = s.Substring(Analyzer.FindSeparator(s, ':') + 2);
                for (int i = 0; i < SequentialGames.Information.players_number; i++)
                {
                    string Name = CutString.Substring(0, Analyzer.FindSeparator(CutString, ';'));
                    SequentialGames.Information.PlayersNames.Add(Name);
                    if (i<SequentialGames.Information.players_number - 1)
                        CutString = CutString.Substring(Analyzer.FindSeparator(CutString, ';') + 2);
                }

                s = sr.ReadLine();
                CutString = s.Substring(Analyzer.FindSeparator(s, ':') + 2);
                string Value = "";
                for (int i = 0; i < CutString.Length; i++)
                {
                    if (CutString[i] != ' ')
                        Value += CutString[i];
                    else
                    {
                        if (Value == "")
                            return false;

                        CheckResult = Analyzer.CheckValidStringDouble(Value, 0, 0, true);
                        if (CheckResult == "")
                            return false;

                        InValues.Add(Convert.ToDouble(CheckResult));
                        Value = "";
                    }
                }

                CheckResult = Analyzer.CheckValidStringDouble(Value, 0, 0, true);
                if (CheckResult == "")
                    return false;

                InValues.Add(Convert.ToDouble(CheckResult));
            }
            else
                return false;
            return true;
        }

        private bool PositionsInitialization()
        {
            sr.ReadLine();
            sr.ReadLine();
            for (int i = 0; i < SequentialGames.Information.tree_levels; i++)
            {
                int k = 0;

                string s = sr.ReadLine();
                string PositionID = "";
                for (int j = 0; j < s.Length; j++)
                {
                    if (s[j] != ' ')
                        PositionID += s[j];
                    else
                    {
                        if (PositionID != "")
                        {
                            SequentialGames.GamePosition GP = new SequentialGames.GamePosition();
                            GP.ID = PositionID;
                            GP.ButtonRow = i;
                            GP.ButtonColumn = k++;
                            PositionID = "";
                            GamePositions.Add(GP);
                        }
                    }
                }
                if (PositionID != "")
                {
                    SequentialGames.GamePosition GP = new SequentialGames.GamePosition();
                    GP.ID = PositionID;
                    GP.ButtonRow = i;
                    GP.ButtonColumn = k++;
                    GamePositions.Add(GP);
                }
            }

            return true;
        }

        private bool PositionsConnections()
        {
            sr.ReadLine();
            sr.ReadLine();
            for (int i = 0; i < GamePositions.Count; i++)
            {
                string s = sr.ReadLine();
                string ParentString = s.Substring(0, Analyzer.FindSeparator(s, ':'));
                SequentialGames.GamePosition Parent = FindPosition(ParentString);
                if (Parent.ID == "")
                    return false;

                string ChildrenString = s.Substring(Analyzer.FindSeparator(s, ':') + 2);
                string ChildName = "";
                for (int j = 0; j < ChildrenString.Length; j++)
                {
                    if (ChildrenString[j] != ' ')
                        ChildName += ChildrenString[j];
                    else
                    {
                        SequentialGames.GamePosition Child = FindPosition(ChildName);
                        if (Child.ID == "")
                            return false;

                        Parent.children.Add(Child);
                        Child.parent = Parent;
                        ChildName = "";
                    }
                }

                if (ChildName != "-")
                {
                    SequentialGames.GamePosition LastChild = FindPosition(ChildName);
                    if (LastChild.ID == "")
                        return false;

                    Parent.children.Add(LastChild);
                    LastChild.parent = Parent;
                }
            }

            for (int i = 0; i < GamePositions.Count; i++)
                if (GamePositions[i].parent == null)
                {
                    GamePositions[i].cash = SequentialGames.Information.InitialValues;
                    GamePositions[i].connected = true;
                }

            return true;
        }

        private bool PositionsNames()
        {
            sr.ReadLine();
            sr.ReadLine();

            for (int i = 0; i < GamePositions.Count; i++)
            {
                string s = sr.ReadLine();
                string IDString = s.Substring(0, Analyzer.FindSeparator(s, ':'));
                SequentialGames.GamePosition Position = FindPosition(IDString);
                if (Position.ID == "")
                    return false;
                else
                {                    
                    string NameString = s.Substring(Analyzer.FindSeparator(s, ':') + 2);
                    if (NameString != "-")
                        Position.name = NameString;
                }
            }

            return true;
        }

        private bool PositionsInformation()
        {
            sr.ReadLine();

            string s = sr.ReadLine(),
                CutString = s.Substring(Analyzer.FindSeparator(s, '=') + 2),
                CheckResult = Analyzer.CheckValidStringInt(CutString, 0, GamePositions.Count,true);
            if (CheckResult == "")
                return false;

            int DefinedCount = Convert.ToInt32(CheckResult);
            for (int i = 0; i < DefinedCount; i++)
            {
                sr.ReadLine();
                string PositionName = sr.ReadLine();

                SequentialGames.GamePosition Position = FindPosition(PositionName);

                if (Position.ID == "")
                    return false;

                s = sr.ReadLine();
                string Path = s.Substring(Analyzer.FindSeparator(s, ':') + 2);
                PositionFileOpening PFO = new PositionFileOpening();
                
                if (PFO.OpenPositionFile(true, Path,false))
                {
                    if (Position.parent == null)
                    {
                        Position.AdParamValues = PFO.gp.AdParamValues;
                        Position.Weights = PFO.gp.Weights;
                        Position.TotalDimensions = PFO.gp.TotalDimensions;
                    }
                    Position.CurrentDimension = 0;                

                    Position.N = PFO.gp.N;
                    Position.Strategies = PFO.gp.Strategies;
                    Position.payoffs = PFO.gp.payoffs;
                    Position.SaveFilePath = PFO.gp.SaveFilePath;
                    Position.defined = true;

                    s = sr.ReadLine();
                    string CombinationsString = s.Substring(Analyzer.FindSeparator(s, ':') + 2);

                    if (CombinationsString.Length > 1)
                    {
                        bool InsideBracket = false;
                        string Strategy = "";

                        for (int j = 0; j < CombinationsString.Length; j++)
                        {
                            switch (CombinationsString[j])
                            {
                                case '(':
                                    {
                                        InsideBracket = true;
                                        Position.Combinations.Add(new List<string>());
                                    }
                                    break;
                                case ' ':
                                    {
                                        if (InsideBracket)
                                        {
                                            Position.Combinations.Last().Add(Strategy);
                                            Strategy = "";
                                        }
                                    }
                                    break;
                                case ')':
                                    {
                                        Position.Combinations.Last().Add(Strategy);
                                        Strategy = "";
                                        InsideBracket = false;
                                    }
                                    break;
                                default:
                                    {
                                        Strategy += CombinationsString[j];
                                    }
                                    break;
                            }
                        }
                    }
                    for (int j = 0; j < SequentialGames.Information.players_number; j++)
                        Position.ActivePlayers.Add(false);

                    if (Position.N < SequentialGames.Information.players_number)
                    {
                        s = sr.ReadLine();
                        string Active = s.Substring(Analyzer.FindSeparator(s, ':') + 2);

                        for (int j = 0; j < Position.N; j++)
                        {
                            int Number = Convert.ToInt32(Active.Substring(0, 1)) - 1;
                            if (Number <= SequentialGames.Information.players_number)
                                Position.ActivePlayers[Number] = true;
                            if (j < Position.N - 1)
                                Active = Active.Substring(2);
                        }
                    }
                }
            }
            return true;
        }

        public bool OpenFile()
        {
            dialog.Filter = "Text documents (*.txt)|*.txt";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string lang = "eng";
                if (lang == "eng")
                    sr = new StreamReader(dialog.FileName, Encoding.UTF8);
                if (GeneralPart())
                    if (PositionsInitialization())
                        if (PositionsConnections())
                            if (PositionsNames())
                                if (PositionsInformation())
                                {
                                    SequentialGames.Information.InitialValues = InValues;
                                    SequentialGames.Information.GamePositions.Clear();
                                    SequentialGames.Information.GamePositions = GamePositions;
                                    sr.Close();
                                    return true;
                                }
                                else
                                    System.Windows.Forms.MessageBox.Show("Positions load: Invalid arguments.");
                            else
                                System.Windows.Forms.MessageBox.Show("Names field: Invalid arguments.");
                        else
                            System.Windows.Forms.MessageBox.Show("Connections field: Invalid arguments.");
                    else
                        System.Windows.Forms.MessageBox.Show("Positions by level field: Invalid arguments.");
                else
                    System.Windows.Forms.MessageBox.Show("General field: Invalid arguments.");
            }
            return false;
        }

        public TreeFileOpening(OpenFileDialog Dialog)
        {
            dialog = Dialog;
        }

    }

    public class TreeFileWriting
    {
        private SaveFileDialog dialog;
        private StreamWriter SW;

        string DirectoryPath;
        string FileName;

        public TreeFileWriting(SaveFileDialog Dialog, string TreeName)
        {
            dialog = Dialog;
            FileName = TreeName;
        }

        private void GeneralPart()
        {
            SW.WriteLine("##Sequential games save file##");
            SW.WriteLine("##Copyright Nikita Veselov, 2015##");
            SW.WriteLine("##Do not change anything but numbers, structure is crucial##");
            SW.WriteLine("-------------------");
            SW.WriteLine("Tree levels: " + SequentialGames.Information.tree_levels);
            SW.WriteLine("Type: Classic");
            SW.WriteLine("Players number: " + SequentialGames.Information.players_number);
            string s = "Players names: ";
            for (int i = 0; i < SequentialGames.Information.players_number; i++ )
            {
                if (SequentialGames.Information.PlayersNames[i] == "")
                    s += "Player " + (i + 1).ToString() + ';';
                else
                    s += SequentialGames.Information.PlayersNames[i] + ';';
                if (i < SequentialGames.Information.players_number - 1)
                    s += ' ';
            }
            SW.WriteLine(s);
            string ValuesString = "Initial values: ";
            for (int i = 0; i < SequentialGames.Information.InitialValues.Count; i++)
            {
                ValuesString += SequentialGames.Information.InitialValues[i];
                if (i != SequentialGames.Information.InitialValues.Count - 1)
                    ValuesString += ' ';
            }
            SW.WriteLine(ValuesString);
            SW.WriteLine("-------------------");
        }

        private void PositionsHierarchy(List<List<Button>> Buttons)
        {
            SW.WriteLine("Positions by level");
            for (int i = 0; i < Buttons.Count; i++)
            {
                string Positions = "";
                for (int j = 0; j < Buttons[i].Count - 1; j++)
                {
                    Positions += (Buttons[i][j].Tag as SequentialGames.GamePosition).ID + ' ';
                }
                Positions += (Buttons[i].Last().Tag as SequentialGames.GamePosition).ID;
                SW.WriteLine(Positions);
                Positions = "";
            }
            SW.WriteLine("-------------------");
            SW.WriteLine("Connections");
            for (int i = 0; i < SequentialGames.Information.GamePositions.Count; i++)
            {
                String ConnectionString = SequentialGames.Information.GamePositions[i].ID + ": ";
                SequentialGames.GamePosition Parent = SequentialGames.Information.GamePositions[i];
                if (Parent.children.Count == 0)
                    ConnectionString += "-";
                else
                {
                    for (int k = 0; k < Parent.children.Count - 1; k++)
                        ConnectionString += Parent.children[k].ID + " ";
                    ConnectionString += Parent.children.Last().ID;
                }
                SW.WriteLine(ConnectionString);
            }
            //for (int i = 0; i < Buttons.Count; i++)
            //{
            //    for (int j = 0; j < Buttons[i].Count; j++)
            //    {
            //        String ConnectionString = Buttons[i][j].Text + ": ";
            //        SequentialGames.GamePosition Parent = (Buttons[i][j].Tag as SequentialGames.GamePosition);
            //        if (Parent.children.Count == 0)
            //            ConnectionString += "-";
            //        else
            //        {
            //            for (int k = 0; k < Parent.children.Count - 1; k++)
            //                ConnectionString += Parent.children[k].name + "_";
            //            ConnectionString += Parent.children.Last().name;
            //        }
            //        SW.WriteLine(ConnectionString);
            //    }
            //}
            SW.WriteLine("-------------------");
        }

        private void PositionsNames()
        {
            SW.WriteLine("Names");
            for (int i = 0; i < SequentialGames.Information.GamePositions.Count; i++)
            {
                if ((SequentialGames.Information.GamePositions[i].name != "") &&
                (SequentialGames.Information.GamePositions[i].name != null))
                    SW.WriteLine(SequentialGames.Information.GamePositions[i].ID + ": " +
                        SequentialGames.Information.GamePositions[i].name);
                else
                    SW.WriteLine(SequentialGames.Information.GamePositions[i].ID + ": -");                
            }
            SW.WriteLine("-------------------");
        }

        private void DefineDirectory(string ThisFilePath)
        {
            int DotPosition = 0,
                SlashPosition = 0;

            for (int i = 0; i < ThisFilePath.Length; i++)
                if (ThisFilePath[i] == '.')
                {
                    DotPosition = i;
                    break;
                }

            for (int i = DotPosition; i >= 0; i--)
                if (ThisFilePath[i] == '\\')
                {
                    SlashPosition = i;
                    break;
                }

            DirectoryPath = ThisFilePath.Substring(0, SlashPosition);

        }

        private void InfoPart(string ThisFilePath)
        {
            List<SequentialGames.GamePosition> DefinedPositions 
                = new List<SequentialGames.GamePosition>();
            int Defined = 0;
            for (int i = 0; i < SequentialGames.Information.GamePositions.Count; i++)
            {
                if (SequentialGames.Information.GamePositions[i].defined)
                {
                    DefinedPositions.Add(SequentialGames.Information.GamePositions[i]);
                    Defined++;
                }
            }

            SW.WriteLine("Defined = " + Defined);
            for (int i = 0; i < DefinedPositions.Count; i++)
            {
                SW.WriteLine("-------");
                SW.WriteLine(DefinedPositions[i].ID);
                if (DefinedPositions[i].SaveFilePath != "")
                    SW.WriteLine("SaveFile: " + DefinedPositions[i].SaveFilePath);
                else
                {
                    PositionFileWriting PFW = new PositionFileWriting(DefinedPositions[i]);
                    DefineDirectory(ThisFilePath);
                    string NewFilePath = DirectoryPath + DefinedPositions[i].name + ".txt";

                    PFW.WritePositionFile(false, NewFilePath);
                }

                string CombinationsString = "Combinations:";
                if (DefinedPositions[i].Combinations.Count == 0)
                    CombinationsString += " -";
                else
                {
                    for (int j = 0; j < DefinedPositions[i].Combinations.Count; j++)
                    {
                        if (DefinedPositions[i].Combinations[j].Count > 0)
                        {
                            CombinationsString += " (";
                            for (int k = 0; k < DefinedPositions[i].Combinations[j].Count - 1; k++)
                                CombinationsString += DefinedPositions[i].Combinations[j][k] + ' ';
                            CombinationsString += DefinedPositions[i].Combinations[j].Last();
                            CombinationsString += ')';
                        }
                    }
                }
                SW.WriteLine(CombinationsString);

                string Active = "Active players: ";
                for (int j = 0; j < DefinedPositions[i].ActivePlayers.Count; j++)
                {
                    if (DefinedPositions[i].ActivePlayers[j])
                        Active += (i + 1);
                    if (j < DefinedPositions[i].ActivePlayers.Count - 1)
                        Active += ' ';
                }
            }
            SW.WriteLine("-------------------");
        }

        public void WriteFile(List<List<Button>> Buttons)
        {
            dialog.Filter = "Text documents (*.txt)|*.txt";
            dialog.RestoreDirectory = true;
            dialog.FileName = FileName;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (SW = File.CreateText(dialog.FileName))
                {
                    GeneralPart();
                    PositionsHierarchy(Buttons);
                    PositionsNames();
                    InfoPart(dialog.FileName);
                }
            }

        }
    }

    public class SimpleTreeFileManagement
    {
        private SaveFileDialog SaveDialog;
        private OpenFileDialog OpenDialog;

        private StreamWriter SW;
        private StreamReader SR;

        private List<List<Button>> Buttons = new List<List<Button>>();
        private List<SequentialGames.TreePosition> Tree = new List<SequentialGames.TreePosition>();
        private string Name1, Name2;
        private int TreeLevels;
        private List<string> PlayersNames = new List<string>();
        private List<List<string>> Parameters = new List<List<string>>();

        public SimpleTreeFileManagement(SaveFileDialog dialog, List<List<Button>> ButtonsInput, 
            List<SequentialGames.TreePosition> TreeInput, string Pl1, string Pl2,
            List<List<string>> ParametersInput)
        {
            SaveDialog = dialog;
            Buttons = ButtonsInput;
            Name1 = Pl1;
            Name2 = Pl2;
            Tree = TreeInput;            
            Parameters = ParametersInput;
        }

        public SimpleTreeFileManagement(OpenFileDialog dialog)
        {
            OpenDialog = dialog;
        }

        public void WriteFile()
        {
            SaveDialog.Filter = "Text documents (*.txt)|*.txt";
            SaveDialog.RestoreDirectory = true;
            SaveDialog.FileName = "";

            if (SaveDialog.ShowDialog() == DialogResult.OK)
            {
                using (SW = File.CreateText(SaveDialog.FileName))
                {
                    SW.WriteLine("##Tree position: Extensive form##");
                    SW.WriteLine("##Copyright Nikita Veselov, 2015.##");
                    SW.WriteLine("##Do not change anything but numbers, structure is crucial##");
                    SW.WriteLine("-------------------");
                    SW.WriteLine("Total moves: " + Buttons.Count);
                    SW.WriteLine(Name1 + " vs. "+Name2);
                    SW.WriteLine("-------------------");

                    SW.WriteLine("Positions by level");
                    for (int i = 0; i < Buttons.Count; i++)
                    {
                        string Positions = "";
                        for (int j = 0; j < Buttons[i].Count - 1; j++)
                        {
                            Positions += (Buttons[i][j].Tag as SequentialGames.TreePosition).ID + ' ';
                        }
                        Positions += (Buttons[i].Last().Tag as SequentialGames.TreePosition).ID;
                        SW.WriteLine(Positions);
                        Positions = "";
                    }
                    SW.WriteLine("-------------------");
                    SW.WriteLine("Connections");
                    for (int i = 0; i < Tree.Count; i++)
                    {
                            String ConnectionString = Tree[i].ID + ": ";
                            SequentialGames.TreePosition Parent = Tree[i];
                            if (Parent.children.Count == 0)
                                ConnectionString += "-";
                            else
                            {
                                for (int k = 0; k < Parent.children.Count - 1; k++)
                                    ConnectionString += Parent.children[k].ID + " ";
                                ConnectionString += Parent.children.Last().ID;
                            }
                            SW.WriteLine(ConnectionString);
                    }
                    SW.WriteLine("-------------------");

                    SW.WriteLine("Position names");
                    for (int i = 0; i < Tree.Count; i++)
                    {
                        string s = Tree[i].ID + ": ";
                        if (Tree[i].name != "")
                            s += Tree[i].name;
                        else
                            s += "-";
                        SW.WriteLine(s);
                    }
                    SW.WriteLine("-------------------");

                    SW.WriteLine("Parameters");
                    for (int i = 0; i < Parameters.Count; i++)
                    {
                        string s = Parameters[i][0] + " (" + Parameters[i][1] + "): " + Parameters[i][2];
                        SW.WriteLine(s);
                    }
                    SW.WriteLine("-------------------");

                    SW.WriteLine("Prizes");
                    for (int i = 0; i < Tree.Count; i++)
                    {
                        if (Tree[i].prize != "")
                        {
                            string s = Tree[i].ID + ": " + Tree[i].prize;
                            SW.WriteLine(s);
                        }
                    }
                    SW.WriteLine("-------------------");
                }
            }
        }

        private SequentialGames.TreePosition FindPosition(string PositionID)
        {
            SequentialGames.TreePosition Position = new SequentialGames.TreePosition();
            for (int j = 0; j < Tree.Count; j++)
                if (Tree[j].ID == PositionID)
                    Position = Tree[j];

            return Position;
        }

        private bool GeneralPart()
        {
            SR.ReadLine();
            SR.ReadLine();
            SR.ReadLine();
            SR.ReadLine();

            string s = SR.ReadLine();
            string CutString = s.Substring(Analyzer.FindSeparator(s, ':') + 2);
            string CheckResult = Analyzer.CheckValidStringInt
                (CutString, 2, SequentialGames.Information.MaxTreeLevelsCount,true);
            if (CheckResult == "")
                return false;
            TreeLevels = Convert.ToInt32(CheckResult);

            s = SR.ReadLine();
            Name1 = s.Substring(0, Analyzer.FindSeparator(s, 'v') - 1);
            Name2 = s.Substring(Analyzer.FindSeparator(s, '.') + 2);

            return true;
        }

        private bool PositionsInitialization()
        {
            SR.ReadLine();
            SR.ReadLine();
            for (int i = 0; i < TreeLevels; i++)
            {
                Buttons.Add(new List<Button>());
                string s = SR.ReadLine();
                string PositionID = "";
                for (int j = 0; j < s.Length; j++)
                {
                    if (s[j] != ' ')
                        PositionID += s[j];
                    else
                    {
                        if (PositionID != "")
                        {
                            Button b = new Button();
                            b.Text = PositionID;
                            Buttons[i].Add(b);
                            SequentialGames.TreePosition TP = new SequentialGames.TreePosition();
                            Tree.Add(TP);
                            TP.button = b;
                            TP.ID = PositionID;
                            b.Tag = TP;
                            PositionID = "";
                        }
                    }
                }
                if (PositionID != "")
                {
                    Button b = new Button();
                    b.Text = PositionID;
                    Buttons[i].Add(b);
                    SequentialGames.TreePosition TP = new SequentialGames.TreePosition();
                    Tree.Add(TP);
                    TP.button = b;
                    TP.ID = PositionID;
                    b.Tag = TP;
                    PositionID = "";
                }
            }
            return true;
        }

        private bool PositionsConnections()
        {
            SR.ReadLine();
            SR.ReadLine();
            for (int i = 0; i < Tree.Count; i++)
            {
                string s = SR.ReadLine();
                string ParentString = s.Substring(0, Analyzer.FindSeparator(s, ':'));
                SequentialGames.TreePosition Parent = FindPosition(ParentString);
                if (Parent.ID == "")
                    return false;

                string ChildrenString = s.Substring(Analyzer.FindSeparator(s, ':') + 2);
                string ChildID = "";
                for (int j = 0; j < ChildrenString.Length; j++)
                {
                    if (ChildrenString[j] != ' ')
                        ChildID += ChildrenString[j];
                    else
                    {
                        SequentialGames.TreePosition Child = FindPosition(ChildID);
                        if (Child.ID == "")
                            return false;

                        Parent.children.Add(Child);
                        Child.parent = Parent;
                        ChildID = "";
                    }
                }

                if (ChildID != "-")
                {
                    SequentialGames.TreePosition LastChild = FindPosition(ChildID);
                    if (LastChild.ID == "")
                        return false;

                    Parent.children.Add(LastChild);
                    LastChild.parent = Parent;
                }
            }

            return true;
        }

        private bool PositionsNames()
        {
            SR.ReadLine();
            SR.ReadLine();

            for (int i = 0; i < Tree.Count; i++)
            {
                string s = SR.ReadLine();
                string ID = s.Substring(0, Analyzer.FindSeparator(s, ':')),
                    Name = s.Substring(Analyzer.FindSeparator(s, ':') + 2);
                if (Name != "-")
                {
                    if (Tree[i].ID == ID)
                    {
                        Tree[i].name = Name;
                        Tree[i].button.Text = Name;
                    }
                    else
                    {
                        SequentialGames.TreePosition TP = FindPosition(ID);
                        TP.name = Name;
                        TP.button.Text = Name;
                    }
                }
            }

            return true;
        }

        private bool ParametersInitialization()
        {
            SR.ReadLine();
            SR.ReadLine();
            bool end = false;

            while (!end)
            {
                string s = SR.ReadLine();
                if (s == "-------------------")
                {
                    end = true;
                }
                else
                {
                    int SpacePosition = Analyzer.FindSeparator(s, ' ');
                    string Name = s.Substring(0, SpacePosition),
                        Key = s.Substring(Analyzer.FindSeparator(s, '(') + 1,
                        Analyzer.FindSeparator(s, ')') - Analyzer.FindSeparator(s, '(') - 1),
                        Value = s.Substring(Analyzer.FindSeparator(s, ':') + 2);

                    if (Analyzer.CheckValidStringDouble(Value, 0, 0, true) == "")
                        return false;

                    Parameters.Add(new List<string>());
                    Parameters.Last().Add(Name);
                    Parameters.Last().Add(Key);
                    Parameters.Last().Add(Value);                    
                }
            }
            return true;
        }

        private void PrizesInitialization()
        {
            SR.ReadLine();
            bool end = false;

            while (!end)
            {
                string s = SR.ReadLine();

                if (s == "-------------------")
                    end = true;
                else
                {
                    SequentialGames.TreePosition TP = FindPosition(s.Substring(0, Analyzer.FindSeparator(s, ':')));
                    TP.prize = s.Substring(Analyzer.FindSeparator(s, ':') + 2);
                }
            }
        }

        public bool OpenFile(bool manualy)
        {
            OpenDialog.Filter = "Text documents (*.txt)|*.txt";
            OpenDialog.RestoreDirectory = true;
            bool DR_OK = true;
            if (manualy)
            {
                DR_OK = false;
                OpenDialog.FileName = "";
                if (OpenDialog.ShowDialog() == DialogResult.OK)
                {
                    DR_OK = true;
                }
            }
            if (DR_OK)
            {
                string lang = "eng";
                if (lang == "eng")
                    SR = new StreamReader(OpenDialog.FileName, Encoding.UTF8);
                if (GeneralPart())
                    if (PositionsInitialization())
                        if (PositionsConnections())
                            if (PositionsNames())
                                if (ParametersInitialization())
                                {
                                    PrizesInitialization();
                                    return true;
                                }
                                else
                                    System.Windows.Forms.MessageBox.Show("Position names field: Invalid arguments.");
                            else
                                System.Windows.Forms.MessageBox.Show("Connections field: Invalid arguments.");
                        else
                            System.Windows.Forms.MessageBox.Show("Positions by level field: Invalid arguments.");
                    else
                        System.Windows.Forms.MessageBox.Show("General field: Invalid arguments.");
            }
            return false;
        }
        

        public List<List<Button>> GetButtons()
        {
            return Buttons;
        }

        public List<SequentialGames.TreePosition> GetTree()
        {
            return Tree;
        }

        public string GetNames()
        {
            return (Name1 + "+" + Name2);
        }

        public List<List<string>> GetParameters()
        {
            return Parameters;
        }
    }

    static class Forms
    {
        public static int openforms_count = 1;
        public static void check_open_forms_count()
        {
            if (openforms_count == 1)
                Application.Exit();
            else
                openforms_count--;
        }
    }

    class Analyzer
    {
        public static int ResizeColumn(object SomeForm, bool ResizeForm, DataGridView DG, int Row, int Column, string NewValue, int MaxWidth)
        {
            int NewWidth = TextRenderer.MeasureText(NewValue, DG.Font).Width + 10;

            if (NewWidth > DG.Columns[Column].Width)
            {
                if ((NewWidth > MaxWidth)&&(MaxWidth!=-1))
                    NewWidth = MaxWidth;
                int diff = NewWidth - DG.Columns[Column].Width;
                DG.Columns[Column].Width = NewWidth;
                DG.Columns[Column].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DG.Width += diff;
                if (ResizeForm)
                    (SomeForm as Form).Width += diff;
                return diff;
            }
            return 0;

        }

        public static string CheckValidStringInt(string s, int x0, int x1, bool msg)
        {
            string result = s;
            if (s.Length != 0)
            {
                StringBuilder sb = new StringBuilder(s);
                bool found_error = false;
                int error_code = 0; // 0 - ok ; 1 - invalid symbol; 2 - valid symbol, invalid position; 3 - break the limit


                if (((sb[0] <= '9') && (sb[0] >= '0')) || ((sb[0] == '-') && (sb.Length > 1)))
                    found_error = false;
                else
                {
                    found_error = true;
                    error_code = 2;
                }

                if (found_error == false)
                {
                    for (int i = 1; i < sb.Length; i++)
                    {
                        if ((sb[i] <= '9') && (sb[i] >= '0'))
                            found_error = false;
                        else
                        {
                            found_error = true;
                            error_code = 1;
                        }

                        if (found_error)
                            break;
                    }

                    if (found_error == false)
                    {
                        if (x0 != x1)
                        {
                            double num = Convert.ToDouble(sb.ToString());

                            if ((num >= Math.Min(x0, x1)) && (num <= Math.Max(x0, x1)))
                                found_error = false;
                            else
                            {
                                found_error = true;
                                error_code = 3;
                            }
                        }
                    }
                }

                switch (error_code)
                {
                    case 0:
                        {
                            result = sb.ToString();
                        }
                        break;
                    case 1:
                        {                            
                            result = "";
                            if (msg)
                            System.Windows.Forms.MessageBox.Show("Invalid symbols inputed", "Invalid input");
                        }
                        break;
                    case 2:
                        {
                            result = "";
                            if (msg)
                            System.Windows.Forms.MessageBox.Show("First symbol is not minus or number.", "Invalid input");
                        }
                        break;
                    case 3:
                        {
                            result = "";
                            if (msg)
                            System.Windows.Forms.MessageBox.Show("The number you entered is beyond the limits.", "Invalid input");
                        }
                        break;
                }
            }
            return result;
        }

        public static string CheckValidStringDouble(string s, double x0, double x1, bool msg)
        {
            string result = s;
            if (s.Length != 0)
            {
                StringBuilder sb = new StringBuilder(s);
                bool found_error = false;
                int error_code = 0; // 0 - ok ; 1 - invalid symbol; 2 - valid symbol, invalid position; 3 - break the limit
                int dots_count = 0;

                if (((sb[0] <= '9') && (sb[0] >= '0')) || (sb[0] == '-'))
                    found_error = false;
                else
                {
                    found_error = true;
                    error_code = 2;
                }

                if (found_error == false)
                {
                    for (int i = 1; i < sb.Length; i++)
                    {
                        if ((sb[i] <= '9') && (sb[i] >= '0'))
                            found_error = false;
                        else
                        {
                            if ((sb[i] == '.') || (sb[i] == ','))
                            {
                                if ((i == 1) && (sb[0] == '-'))
                                {
                                    found_error = true;
                                    error_code = 1;
                                }
                                else
                                {
                                    if (dots_count == 0)
                                    {
                                        dots_count++;
                                        sb[i] = '.';
                                    }
                                    else
                                    {
                                        found_error = true;
                                        error_code = 2;
                                    }
                                }
                            }
                            else
                            {
                                found_error = true;
                                error_code = 1;
                            }
                        }

                        if (found_error)
                            break;
                    }

                    if (found_error == false)
                    {
                        if (x0 != x1)
                        {
                            double num = Convert.ToDouble(sb.ToString());

                            if ((num >= Math.Min(x0, x1)) && (num <= Math.Max(x0, x1)))
                                found_error = false;
                            else
                            {
                                found_error = true;
                                error_code = 3;
                            }
                        }
                    }
                }

                switch (error_code)
                {
                    case 0:
                        {
                            result = sb.ToString();
                        }
                        break;
                    case 1:
                        {
                            result = "";
                            if (msg)
                            System.Windows.Forms.MessageBox.Show("Invalid symbols inputed", "Invalid input");
                        }
                        break;
                    case 2:
                        {
                            result = "";
                            if (msg)
                            System.Windows.Forms.MessageBox.Show("First symbol is not minus or number.", "Invalid input");
                        }
                        break;
                    case 3:
                        {
                            result = "";
                            if (msg)
                            System.Windows.Forms.MessageBox.Show("The number you entered is beyond the limits.", "Invalid input");
                        }
                        break;
                }
            }
            return result;
        }

        public static int FindSeparator(string s, char Separator)
        {
            for (int i = 0; i < s.Length; i++)
                if (s[i] == Separator)
                    return i;
            return -1;
        }

    }
}