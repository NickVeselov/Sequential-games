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
    public partial class Tree : Form
    {
        //buttons data
        int cash_width = 150,
            cash_height = 20;

        //new button data
        string NewButtonName,
            NewButtonParentName;
        
        public List<List<Button>> buttons = new List<List<Button>>();        
        public List<Graphic_Interface.Line> lines = new List<Graphic_Interface.Line>();
        public List<Label> LevelsLabels = new List<Label>();

        TextBox NameEdit = new TextBox();

        //HScrollBar HSB;
        int NewWidth,
            height;
        Color buttons_color = Color.LightSteelBlue;
        List<int> indents = new List<int>();
        Color focus_color = Color.Gold;

        Size buttonssize = new Size(45, 30);
        int Lv1GapBtwBtns = 120,
            Lv1GapBtwBrnchs = 120,
            Lv1Indent = 120;
        Pen p = new Pen(Color.Black, 1);
        Bitmap bmp;
        Graphics g;

        GamePosition PositionBuffer = new GamePosition();
        Button OperatedButton, LastCreatedButton;
        bool Pasting = false;
        int AlphabetLetterIndex = 0;
        List<string> Alphabet = new List<string>();

        public Tree()
        {
            InitializeComponent();

            bmp = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bmp);
            pb.Image = bmp;

            pb.Controls.Add(NameEdit);
            NameEdit.KeyDown += new KeyEventHandler(NameEdit_KeyDown);
            NameEdit.KeyPress += new KeyPressEventHandler(NameEdit_KeyPress);
            NameEdit.Hide();

            cash_width = 0;
            for (int i = 0; i < Information.players_number; i++)
                cash_width += TextRenderer.MeasureText
                    (Information.InitialValues[i].ToString(),
                    new System.Drawing.Font("Bookman Old Style", 8)).Width + 30;
            cash_width = Math.Max(70, cash_width);

            panel1.AutoSize = false;
            panel1.AutoScroll = true;
            //DrawTree(false);
            //this.StartPosition = FormStartPosition.CenterScreen;

            //testing_initialization();

            using (Bitmap b = SequentialGames.Properties.Resources.SG)
            {
                IntPtr I = b.GetHicon();
                this.Icon = Icon.FromHandle(I);
            }
            //UnitedCommandFunction();
        }

        void NameEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
                e.Handled = true;
        }

        private void testing_initialization()
        {
            NewWidth = this.Width;
            height = this.Height;

            this.Text = "Test Tree";
            Information.tree_levels = 3;
            Information.players_number = 4;
            Information.InitialValues.Add(10000);
            Information.InitialValues.Add(10000);
            Information.InitialValues.Add(10000);
            Information.InitialValues.Add(10000);

            Information.ChildrenCount = 2;

            cash_width = 0;
            for (int i = 0; i < Information.players_number; i++)
                cash_width += TextRenderer.MeasureText
                    (Information.InitialValues[i].ToString(),
                    new System.Drawing.Font("Bookman Old Style", 8)).Width;

            cash_width = Math.Max(70, cash_width);

            Lv1GapBtwBtns = cash_width * 3 / 4;
            Lv1GapBtwBrnchs = Lv1GapBtwBtns * 3 / 2;
            Lv1Indent = cash_width * 3 / 2;

            for (int i = 0; i < Information.players_number; i++)
                Information.PlayersNames.Add("");

            pb.Show();
            LevelsPB.Show();
            DrawTree();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public void CreateNamesBoard()
        {
            bool AtLeastJustOneFilled = false;
            for (int i = 0; i < Information.players_number; i++)
                if ((Information.PlayersNames[i] != null) && (Information.PlayersNames[i] != ""))
                    AtLeastJustOneFilled = true;
            if (AtLeastJustOneFilled)
            {
                if (NamesPanel.Controls.Count > 1)
                {
                    NamesPanel.Width = TextRenderer.MeasureText(PlayersLabel.Text, PlayersLabel.Font).Width + 50;
                    for (int i = 0; i < Information.players_number; i++)
                    {
                        if ((Information.PlayersNames[i] == null) || (Information.PlayersNames[i] == ""))
                            NamesPanel.Controls[1 + i].Text = (i + 1).ToString() + ". Player " + (i + 1).ToString();
                        else
                            NamesPanel.Controls[1 + i].Text = (i + 1).ToString() + ". " + Information.PlayersNames[i].ToString();

                        NamesPanel.Controls[1 + i].Size = TextRenderer.MeasureText
                            (NamesPanel.Controls[1 + i].Text, NamesPanel.Controls[1 + i].Font);
                        NamesPanel.Width = Math.Max(NamesPanel.Width, TextRenderer.MeasureText
                            (NamesPanel.Controls[i + 1].Text, NamesPanel.Controls[i + 1].Font).Width + 10);
                    }
                }
                else
                {
                    for (int i = 0; i < Information.players_number; i++)
                    {
                        Label l = new Label();

                        if ((Information.PlayersNames[i] == null) || (Information.PlayersNames[i] == ""))
                            l.Text = (i + 1).ToString() + ". Player " + (i + 1).ToString();
                        else
                            l.Text = (i + 1).ToString() + ". " + Information.PlayersNames[i].ToString();

                        l.Font = new System.Drawing.Font("Bookman Old Style", 12);
                        l.Size = TextRenderer.MeasureText(l.Text, l.Font);
                        l.Left = 10;
                        l.Top = i * 30 + PlayersLabel.Bottom + 10;

                        NamesPanel.Controls.Add(l);
                    }
                    NamesPanel.Show();
                    NamesPanel.Height = NamesPanel.Controls[NamesPanel.Controls.Count - 1].Bottom + 20;
                }
            }
            else
                NamesPanel.Hide();
        }

        private void CorrectSizes()
        {            
            int max = 0;
            for (int i = 0; i < buttons.Count; i++)
                max = Math.Max(max, buttons[i].Last().Right + 150);

            pb.Size = new Size(max, buttons.Last()[0].Top + 100);
            panel1.Size = new Size(max, buttons.Last()[0].Bottom + 100);
            this.Size = new Size(panel1.Width + panel1.Left + 20, panel1.Top + panel1.Height + menuStrip1.Height + 10);
            if (pb.Width < 1400)
                this.Width = pb.Width + 16;
            else
                this.Width = 1400;
            if (pb.Height < 800)
                this.Height = pb.Height + menuStrip1.Height + 39;
            else
                this.Height = 800;
        }

        private void ClearScreen()
        {
            bmp = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bmp);
            pb.Image = bmp;
        }

        private void CreateAlphabet()
        {
            Alphabet.Add("A"); Alphabet.Add("B"); Alphabet.Add("C"); Alphabet.Add("D");
            Alphabet.Add("E"); Alphabet.Add("F"); Alphabet.Add("G"); Alphabet.Add("H");  
            Alphabet.Add("I"); Alphabet.Add("J"); Alphabet.Add("K"); Alphabet.Add("L");
            Alphabet.Add("M"); Alphabet.Add("N"); Alphabet.Add("O"); Alphabet.Add("P");
            Alphabet.Add("Q"); Alphabet.Add("R"); Alphabet.Add("S"); Alphabet.Add("T");
            Alphabet.Add("U"); Alphabet.Add("V"); Alphabet.Add("W"); Alphabet.Add("X");
            Alphabet.Add("Y"); Alphabet.Add("Z");

            for (int i = 0; i<1000; i++)
                for (int j = 0; j < 26; j++)
                    Alphabet.Add(Alphabet[j] + (i + 1).ToString());
        }

        private void ButtonsProperties(Button btn, Size buttonssize)
        {
            btn.BackColor = buttons_color;
            btn.ForeColor = Color.Black;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Font = new System.Drawing.Font("Bookman Old Style", 12);
            btn.FlatStyle = FlatStyle.Popup;
            btn.Size = buttonssize;
            btn.Width = Math.Max(buttonssize.Width, (TextRenderer.MeasureText(btn.Text, btn.Font)).Width + 8);

            btn.MouseClick += new MouseEventHandler(buttons_MouseClick);
            btn.MouseDown += new MouseEventHandler(buttons_MouseDown);
            pb.Controls.Add(btn);
        }

        void buttons_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenuStrip CMS = new System.Windows.Forms.ContextMenuStrip();
                CMS.Items.Add("Add new alternative");
                CMS.Items.Add("Copy children");
                CMS.Items.Add("Paste children");
                CMS.Items.Add("Delete");
                CMS.Items.Add("Rename");
                CMS.Items[0].Click += new EventHandler(AddPositions);
                CMS.Items[1].Click += new EventHandler(CopyChildren);
                CMS.Items[2].Click += new EventHandler(PasteChildren);
                CMS.Items[3].Click += new EventHandler(DeletePosition);
                CMS.Items[4].Click += new EventHandler(RenamePosition);

                if (PositionBuffer.children.Count == 0)
                    CMS.Items[2].Enabled = false;

                OperatedButton = (sender as Button);
                (sender as Button).ContextMenuStrip = CMS;
                (sender as Button).ContextMenuStrip.Show(Cursor.Position);

            }
        }

        void RenamePosition(object sender, EventArgs e)
        {
            Button b = OperatedButton;
            NameEdit.Size = new Size(b.Width - 1, b.Height - 1);
            NameEdit.Font = new Font("Bookman Old Style", 13);
            NameEdit.Left = b.Left + 1;
            NameEdit.Width = b.Width - 2;
            NameEdit.Top = b.Top + 1;
            NameEdit.Tag = b;
            NameEdit.Text = b.Text;
            NameEdit.Show();
            NameEdit.Focus();
            NameEdit.SelectionStart = b.Text.Length;
        }

        void DeletePosition(object sender, EventArgs e)
        {
            GamePosition GP = (OperatedButton).Tag as GamePosition;

            if (GP.parent != null)
            {
                if (GP.parent.children.Count == 2)
                {
                    string ErrorMSG = "It is meaningless to leave only 1 alternative.\nMaybe you want to delete the branch?";
                    if (GP.parent.name != "")
                        ErrorMSG += " (" + GP.parent.name + " position)";
                    else
                        ErrorMSG += " ('" + GP.parent.ID + "' position)";
                    DialogResult DR = System.Windows.Forms.MessageBox.Show(ErrorMSG, "Leaving no choice", MessageBoxButtons.YesNoCancel);

                    if (DR == System.Windows.Forms.DialogResult.Yes)
                    {
                        OperatedButton = GP.parent.button;
                        DeletePosition(this, new EventArgs());
                    }
                }
                else
                {
                    RecursiveDeletion(GP);
                    GP.parent.children.Remove(GP);

                    int bc = buttons.Count,
                        EmptyLevels = 0;

                    for (int i = 0; i < bc; i++)
                    {
                        if (buttons[i - EmptyLevels].Count == 0)
                            buttons.RemoveAt(i - EmptyLevels++);
                    }

                    CorrectSizes();
                    ClearScreen();
                    DrawButtons();
                    DrawLines();
                }
            }
            else
                System.Windows.Forms.MessageBox.Show("In order to start fresh, click 'New' at the Top menu.","You can't delete root position");
        }

        void PasteChildren(object sender, EventArgs e)
        {
            GamePosition GP = (OperatedButton.Tag as GamePosition);
            int ChildrenCount = GP.children.Count;
            for (int i = 0; i < ChildrenCount; i++)
            {
                OperatedButton = GP.children[0].button;
                DeletePosition(this, new EventArgs());
            }

            OperatedButton = GP.button;
            for (int i = 0; i < PositionBuffer.children.Count; i++)
            {
                Pasting = true;
                AddPositions(this, new EventArgs());
                GP.children[i].button.Text = PositionBuffer.children[i].button.Text;
                GP.name = PositionBuffer.children[i].name;
                GP.ID = Alphabet[AlphabetLetterIndex++];
                Pasting = false;
            }
        }

        void CopyChildren(object sender, EventArgs e)
        {
            PositionBuffer = ((OperatedButton.Tag) as GamePosition);
        }

        void AddPositions(object sender, EventArgs e)
        {
            if (((OperatedButton.Tag as GamePosition).children.Count == 0) && (!Pasting))
                CreateNewPosition();
            CreateNewPosition();

            DrawButtons();
            CorrectSizes();
            ClearScreen();
            DrawLines();
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].labels.Count > 0)
                    i++;
            }
            DrawLevelsLadder();

        }

        private void CreateNewPosition()
        {
            Button btn = new Button();
            btn.Text = Alphabet[AlphabetLetterIndex++];

            for (int i = 0; i < buttons.Count; i++)
                for (int j = 0; j < buttons[i].Count; j++)
                {
                    if (buttons[i][j] == OperatedButton)
                    {
                        ButtonsProperties(btn, buttonssize);
                        if (i == buttons.Count - 1)
                        {
                            buttons.Add(new List<Button>());
                            Information.tree_levels++;
                            CreateLabels();
                        }

                        buttons[i + 1].Add(btn);
                        GamePosition LastChild = new GamePosition();
                        bool PreviousFound = false;
                        if (((GamePosition)buttons[i][j].Tag).children.Count > 0)
                        {
                            //find fathers youngest son
                            LastChild = ((GamePosition)buttons[i][j].Tag).children.Last();
                            PreviousFound = true;
                        }
                        else
                        {
                            for (int t = j; t >= 0; t--)
                            {
                                if (((GamePosition)buttons[i][t].Tag).children.Count > 0)
                                {
                                    //father has no sons; try to find youngest father son
                                    LastChild = ((GamePosition)buttons[i][t].Tag).children.Last();
                                    PreviousFound = true;
                                    break;
                                }
                            }
                        }

                        if ((!PreviousFound) && (buttons[i + 1].Count > 1))
                        {
                            //no previous fathers with childs -> it must be first position in next row
                            for (int k = buttons[i + 1].Count - 1; k > 0; k--)
                                buttons[i + 1][k] = buttons[i + 1][k - 1];
                            buttons[i + 1][0] = btn;
                        }
                        else
                        {
                            //making permutation
                            for (int k = 0; k < buttons[i + 1].Count; k++)
                            {
                                if ((buttons[i + 1][k].Tag as GamePosition) == LastChild)
                                {
                                    for (int l = buttons[i + 1].Count - 1; l > k + 1; l--)
                                        buttons[i + 1][l] = buttons[i + 1][l - 1];
                                    if (k < buttons[i + 1].Count - 1)
                                        buttons[i + 1][k + 1] = btn;
                                    break;
                                }
                            }
                        }
                        GamePosition NewPosition = new GamePosition();
                        NewPosition.parent = (buttons[i][j].Tag as GamePosition);
                        NewPosition.button = btn;
                        NewPosition.ID = btn.Text;
                        NewPosition.N = Information.players_number;
                        Information.GamePositions.Add(NewPosition);

                        if (NewPosition.parent.defined)
                        {
                            NewPosition.parent.OptimalStrategy_R.Clear();
                            NewPosition.parent.cash.Clear();
                            
                            //NewPosition.parent.
                        }
                        //    NewPosition.TotalDimensions = NewPosition.parent.TotalDimensions;
                        //    for (int k = 0; k < NewPosition.parent.AdParamValues.Count; k++)
                        //    {
                        //        NewPosition.AdParamValues.Add(new List<double>());
                        //        for (int l = 0; l < NewPosition.parent.AdParamValues.Count; l++)
                        //            NewPosition.AdParamValues[k].Add(NewPosition.parent.AdParamValues[k][l]);
                        //    }
                        //    for (int d = 1; d < NewPosition.TotalDimensions; d++)
                        //        for (int pl1 = 0; pl1 < NewPosition.N; pl1++)
                        //            for (int pl2 = 0; pl2 < NewPosition.N; pl2++)
                        //            {
                        //                int s1 = NewPosition.parent.StrategiesNames
                        //                if (pl1 != pl2)
                        //                    NewPosition.AdParamValues[d - 1][pl1] += 
                        //                        NewPosition.parent.NumericalPayoffs[d][pl1][pl2]
                        //                        [Choice[Math.Min(pl1, pl2)]][Choice[Math.Max(pl1, pl2)]];
                        //            }
                        //}

                        (OperatedButton.Tag as GamePosition).children.Add(NewPosition);
                        btn.Tag = NewPosition;
                        pb.Controls.Add(btn);

                        break;

                    }
                }
            LastCreatedButton = btn;
        }

        private void RecursiveDeletion(GamePosition GP)
        {
            bool erased = false;

            while (!erased)
            {
                if (GP.children.Count > 0)
                {
                    for (int i = 0; i < GP.children.Count; i++)
                        RecursiveDeletion(GP.children[i]);
                    GP.children.Clear();
                }

                for (int i = 0; i < buttons.Count; i++)
                    buttons[i].Remove(GP.button);
                AlphabetLetterIndex--;
                Information.GamePositions.Remove(GP);
                GP.button.Dispose();

                erased = true;
            }
        }

        private void buttons_MouseClick(object sender, MouseEventArgs e)
        {
            GamePosition gp = new GamePosition();
            for (int i = 0; i < Information.GamePositions.Count; i++)
            {
                string Key = "";
                if ((Information.GamePositions[i].name == "")||(Information.GamePositions[i].name == null))
                    Key = Information.GamePositions[i].ID;
                else
                    Key = Information.GamePositions[i].name;
                if (Key == (sender as Button).Text)
                {
                    gp = Information.GamePositions[i];
                    break;
                }
                
            }

            if (gp.active)
                gp.InfoForm.Focus();
            else
            {
                PositionInformation_Form TEM = new PositionInformation_Form(this, gp);
//Debug//
                DebugClass.PIF = TEM;
                if ((gp.name == "")||(gp.name == null))
                    TEM.Text = '"' + gp.ID + '"' + " position";
                else
                    TEM.Text = '"' + gp.name + '"' + " position";

                gp.active = true;
                gp.InfoForm = TEM;
                TEM.StartPosition = FormStartPosition.Manual;
                TEM.Left = Cursor.Position.X;
                TEM.Top = Cursor.Position.Y;
                TEM.Show();
            }
        }
        

        private void CheckLevelForGaps(List<int> Positions, int level)
        {
            for (int i = 0; i < buttons[level].Count-1; i++)
            {
                if (((GamePosition)buttons[level][i].Tag).children.Count == 0)
                {
                    if (i > 1)
                        if (((GamePosition)buttons[level][i - 1].Tag).children.Count > 0)
                        {
                            GamePosition Last_child = ((GamePosition)buttons[level][i - 1].Tag).children.Last();
                            int start_index = 0;
                            for (int j = 0; j < buttons[level + 1].Count; j++)
                            {
                                if (buttons[level + 1][j] == Last_child.button)
                                {
                                    start_index = j;
                                    break;
                                }
                            }
                            for (int j = start_index + 1; j < buttons[level + 1].Count; j++)
                                Positions[j] += Positions[start_index] - Positions[start_index - 1];
                        }
                }
            }
        }

        public void FindNotEmptyBranch(List<int> NewButtonsPositions, ref GamePosition BranchGP, int CurrentLevel, ref int ParentIndex, int ChildIndex)
        {
            int LowerLevel = CurrentLevel + 1;
            bool Found = false;
            while (!Found)
            {
                if (BranchGP.children.Count == 0)
                {
                    int Addition = Lv1GapBtwBtns + buttons[CurrentLevel][ParentIndex].Width,
                        ActualChildIndex = ChildIndex;
                    for (int i = LowerLevel; i < buttons.Count; i++)
                    {
                        for (int k = ChildIndex; k < buttons[i].Count; k++)
                            buttons[i][k].Left += Addition;
                        if (i < buttons.Count - 1)
                        {
                            int FatherIndex = -1;
                            for (int l = ChildIndex; l < buttons[i].Count; l++)
                            {
                                if ((buttons[i][l].Tag as GamePosition).children.Count != 0)
                                {
                                    FatherIndex = l;
                                    break;
                                }
                            }
                            if (FatherIndex != -1)
                            {
                                Button FirstMovedButton = (buttons[i][FatherIndex].Tag as GamePosition).children[0].button;
                                for (int l = 0; l < buttons[i + 1].Count; l++)
                                {
                                    if (FirstMovedButton == buttons[i + 1][l])
                                    {
                                        ChildIndex = l;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    ChildIndex = ActualChildIndex;
                    if (NewButtonsPositions.Count == 0)
                        NewButtonsPositions.Add(Addition);
                    else
                        NewButtonsPositions.Add(NewButtonsPositions.Last() + Addition);

                    BranchGP = (GamePosition)buttons[CurrentLevel][++ParentIndex].Tag;
                }
                else
                    Found = true;
            }
        }

        private void CorrectNoChildrenPosition(List<int> NewButtonsPositions, int CurrentLevel, int ParentIndex)
        {
            if (ParentIndex > 0)
            {
                GamePosition Current = (GamePosition)buttons[CurrentLevel][ParentIndex].Tag;
                GamePosition Previous = (GamePosition)buttons[CurrentLevel][ParentIndex - 1].Tag;
                if ((Current.children.Count == 0) && (Previous.children.Count > 0))
                {
                    NewButtonsPositions[NewButtonsPositions.Count - 1] +=
                        (Previous.children.Last().button.Right - Previous.children[0].button.Left) / 2;
                }
            }
        }

        private List<int> CalculateNewGaps(int CurrentLevel)
        {
            int LowerLevel = CurrentLevel + 1,
                x0, x1,
                ParentIndex = 0;
            List<int> NewButtonsPositions = new List<int>();
            GamePosition BranchGP = (GamePosition)buttons[CurrentLevel][ParentIndex].Tag;

            FindNotEmptyBranch(NewButtonsPositions, ref BranchGP, CurrentLevel, ref ParentIndex, 0);
            x0 = buttons[LowerLevel][0].Left;
            x1 = buttons[LowerLevel][0].Right;
            for (int i = 0; i < buttons[LowerLevel].Count; i++)
            {
                GamePosition ButtonGP = ((GamePosition)buttons[LowerLevel][i].Tag).parent;

                if (BranchGP == ButtonGP)
                    x1 = buttons[LowerLevel][i].Right;
                else
                {
                    NewButtonsPositions.Add((x0 + x1 - buttons[CurrentLevel][ParentIndex].Width) / 2);
                    CorrectNoChildrenPosition(NewButtonsPositions, CurrentLevel, ParentIndex);

                    BranchGP = (GamePosition)buttons[CurrentLevel][++ParentIndex].Tag;
                    FindNotEmptyBranch(NewButtonsPositions, ref BranchGP, CurrentLevel, ref ParentIndex, i);

                    x0 = buttons[LowerLevel][i].Left;
                    x1 = buttons[LowerLevel][i].Right;
                }
            }
            NewButtonsPositions.Add((x0 + x1 - buttons[CurrentLevel].Last().Width) / 2);
            CorrectNoChildrenPosition(NewButtonsPositions, CurrentLevel, ParentIndex);
            for (int i = NewButtonsPositions.Count; i < buttons[CurrentLevel].Count; i++)
            {
                NewButtonsPositions.Add(NewButtonsPositions.Last() + buttons[CurrentLevel][i].Width + Lv1GapBtwBrnchs);
                CorrectNoChildrenPosition(NewButtonsPositions, CurrentLevel, ++ParentIndex);
            }
            return NewButtonsPositions;
        }

        private void DrawButtons()
        {
            Lv1GapBtwBtns = cash_width * 4 / 5;
            Lv1GapBtwBrnchs = Lv1GapBtwBtns * 3 / 2;

            int indent = Lv1Indent,
                between_branches = Lv1GapBtwBrnchs,
                between_vertexes = Lv1GapBtwBtns,
                levels = Information.tree_levels;

            List<int> Positions = new List<int>();

            Positions.Add(Lv1Indent);

            GamePosition BranchGP = ((GamePosition)buttons[levels - 1][0].Tag).parent;
            for (int i = 1; i < buttons[levels - 1].Count; i++)
            {
                GamePosition ButtonGP = ((GamePosition)buttons[levels - 1][i].Tag).parent;
                if (BranchGP == ButtonGP)
                    Positions.Add(Positions[Positions.Count - 1] + Lv1GapBtwBtns + buttons[levels - 1][i - 1].Width);
                else
                {
                    Positions.Add(Positions[Positions.Count - 1] + Lv1GapBtwBrnchs + buttons[levels - 1][i - 1].Width);
                    BranchGP = ButtonGP;
                }
            }

            for (int i = 0; i < levels; i++)
            {
                if (i > 0)
                {
                    Positions.Clear();
                    Positions = CalculateNewGaps(levels - i - 1);
                }
                for (int j = 0; j < buttons[levels - i - 1].Count; j++)
                {
                    buttons[levels - i - 1][j].Left = Positions[j];
                    buttons[levels - i - 1][j].Top = (4 * (levels - i - 1) + 1) * buttonssize.Height;
                }
            }

            for (int i = 0; i < levels; i++)
                for (int j = 0; j < buttons[i].Count; j++)
                {
                    if (((buttons[i][j].Tag as GamePosition).children.Count == 0) &&
                        ((buttons[i][j].Tag as GamePosition).cash.Count > 0))
                        buttons[i][j].BackColor = Color.MediumOrchid;
                }
        }

        private List<List<Button>> CreateButtons(int levels)
        {
            for (int i = 0; i < levels; i++)
            {
                buttons.Add(new List<Button>());
                for (int j = 0; j < Math.Pow(Information.ChildrenCount, i); j++)
                {
                    Button b = new Button();
                    b.Text = Alphabet[AlphabetLetterIndex++];
                    ButtonsProperties(b, buttonssize);
                    buttons[i].Add(b);

                    GamePosition GP = new GamePosition();
                    GP.ID = b.Text;
                    Information.GamePositions.Add(GP);
                    b.Tag = GP;
                    GP.N = Information.players_number;
                    GP.button = b;
                    GP.ButtonRow = i;
                    GP.ButtonColumn = j;
                    GP.N = Information.players_number;

                    if ((i != 0) && (j % Information.ChildrenCount == Information.ChildrenCount - 1))
                    {
                        GamePosition ParentGP = (GamePosition)buttons[i - 1][j / Information.ChildrenCount].Tag;
                        for (int l = j - Information.ChildrenCount + 1; l <= j; l++)
                        {
                            GamePosition CurrentButton = (GamePosition)buttons[i][l].Tag;
                            CurrentButton.parent = ParentGP;
                            ParentGP.children.Add(CurrentButton);
                        }
                    }
                }
            }

            (buttons[0][0].Tag as GamePosition).connected = true;
            for (int i = 0; i < Information.InitialValues.Count; i++)
            {
                (buttons[0][0].Tag as GamePosition).cash.Add(Information.InitialValues[i]);
                (buttons[0][0].Tag as GamePosition).N = Information.players_number;
            }

            return buttons;
        }

        private string WriteWordInFewLines(string word)
        {
            string s = word;
            List<string> Division = new List<string>();
            string Phrase = s;
            int WordEnd = Graphic_Interface.Analyzer.FindSeparator(Phrase, ' ');

            while (WordEnd != -1)
            {
                Division.Add(Phrase.Substring(0, WordEnd));
                Phrase = Phrase.Substring(WordEnd + 1);
                WordEnd = Graphic_Interface.Analyzer.FindSeparator(Phrase, ' ');
            }
            Division.Add(Phrase);
            s = "";
            int Height = 1,
                MaxWordWidth = 0;
            string CurrentString = "";
            for (int i = 0; i < Division.Count; i++)
            {
                CurrentString += Division[i];
                MaxWordWidth = Math.Max(MaxWordWidth,
                    TextRenderer.MeasureText(CurrentString, OperatedButton.Font).Width + 10);
                s += Division[i];
                if (i < Division.Count - 1)
                {
                    // if the word is wide then drop it to the new string
                    if (CurrentString.Length > 5)
                    {
                        s += '\n';
                        Height++;
                        CurrentString = "";
                    }
                    else
                    {
                        s += ' ';
                        CurrentString += ' ';
                    }
                }
            }
            OperatedButton.Height = Height * buttonssize.Height;
            if (Height > 1)
                OperatedButton.Width = MaxWordWidth;

            return s;
        }
                
        void NameEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NameEdit.Hide();
                (NameEdit.Tag as Button).Text = NameEdit.Text;
                ((NameEdit.Tag as Button).Tag as GamePosition).name = NameEdit.Text;
                (NameEdit.Tag as Button).Left -= (TextRenderer.MeasureText
                    ((NameEdit.Tag as Button).Text, (NameEdit.Tag as Button).Font).Width + 8 - (NameEdit.Tag as Button).Width) / 2;

                bool MoreThanJustAWord = false;
                for (int i = 0; i < NameEdit.Text.Length; i++)
                    if ((NameEdit.Text[i] == ' ') || (NameEdit.Text[i] == '\n'))
                        MoreThanJustAWord = true;                

                (NameEdit.Tag as Button).Width = Math.Max(buttonssize.Width, TextRenderer.MeasureText
                    ((NameEdit.Tag as Button).Text, (NameEdit.Tag as Button).Font).Width + 8);

                if (MoreThanJustAWord)
                    NameEdit.Text = WriteWordInFewLines(NameEdit.Text);

            }
        }
        //doublestring
        private List<double> CalculatePlayersPrizes(List<int> Strategies, GamePosition gp)
        {
            List<double> Prize = new List<double>();

            for (int pl1 = 0; pl1 < gp.N; pl1++)
            {
                Prize.Add(0);
                for (int pl2 = 0; pl2 < gp.N; pl2++)
                {
                    if (pl1 < pl2)
                        Prize[pl1] += gp.NumericalPayoffs[0][pl1][pl2][Strategies[pl1]][Strategies[pl2]];
                    if (pl1 > pl2)
                        Prize[pl1] += gp.NumericalPayoffs[0][pl1][pl2][Strategies[pl2]][Strategies[pl1]];
                }
            }

            return Prize;
        }

        public void redefine_strategies(PositionInformation_Form child)
        {
            solveToolStripMenuItem.Visible = true;
            //filter active lines
            GamePosition current = child.gp;
            string KeyString = "";
            if ((current.name == "")||(current.name == null))
                KeyString = current.ID;
            else
                KeyString = current.name;
            List<Graphic_Interface.Line> ActiveLines = new List<Graphic_Interface.Line>();
            for (int row = 0; row < buttons.Count; row++)
                for (int col = 0; col < buttons[row].Count; col++)
                {
                    if (buttons[row][col].Text == KeyString)
                    {
                        buttons[row][col].BackColor = Color.DarkSalmon;

                        for (int k = 0; k < lines.Count; k++)
                        {
                            if (lines[k].source == buttons[row][col])
                                ActiveLines.Add(lines[k]);
                        }
                    }
                }

            // new list with new Strategies
            List<List<string>> NewStrategies = new List<List<string>>();
            for (int i = 0; i < child.DgCombinations.RowCount; i++)
            {
                bool fulfilled = true;
                NewStrategies.Add(new List<string>());
                for (int j = 0; j < child.gp.N; j++)
                {
                    if (fulfilled)
                    {
                        if (child.DgCombinations.Rows[i].Cells[j].Value == null)
                        {
                            fulfilled = false;
                            NewStrategies[i].Clear();
                        }
                        else
                            NewStrategies[i].Add(child.DgCombinations.Rows[i].Cells[j].Value.ToString());
                    }
                }
            }


            current.Combinations = NewStrategies;

            for (int i = 0; i < NewStrategies.Count; i++)
            {
                if (NewStrategies[i].Count > 0)
                {
                    current.children[i].StrategiesNames = NewStrategies[i];
                    ActiveLines[i].write_strategies(NewStrategies[i]);
                    if ((NewStrategies[i].Count == child.gp.N) && (current.connected))
                    {
                        GamePosition next = current.children[i];
                        next.connected = true;
                        List<double> payoffs = new List<double>();
                        next.cash.Clear();

                        List<int> Choice = new List<int>();
                        for (int j = 0; j < current.N; j++)
                            Choice.Add(Convert.ToInt32(NewStrategies[i][j].Substring(1, NewStrategies[i][j].Length - 1)) - 1);

                        List<double> Prizes = CalculatePlayersPrizes(Choice, current);
                        for (int j = 0; j < current.N; j++)
                            next.cash.Add(current.cash[j] + Prizes[j]);

                        if (next.AdParamValues.Count != current.AdParamValues.Count)
                        {
                            for (int j = 0; j < current.AdParamValues.Count; j++)
                            {
                                next.AdParamValues.Add(new List<double>());
                                for (int k = 0; k < current.AdParamValues[j].Count; k++)
                                    next.AdParamValues[j].Add(current.AdParamValues[j][k]);
                            }
                            next.Weights = current.Weights;
                            next.TotalDimensions = current.TotalDimensions;
                            if (!next.defined)
                            {
                                for (int d = 1; d < next.TotalDimensions; d++)
                                    next.CreateNewPayoffLayer();
                            }
                            for (int d = 1; d < current.TotalDimensions; d++)
                            {
                                for (int pl1 = 0; pl1 < current.N; pl1++)
                                    for (int pl2 = 0; pl2 < current.N; pl2++)
                                    {
                                        if (pl1 != pl2)
                                            next.AdParamValues[d - 1][pl1] += current.NumericalPayoffs[d][pl1][pl2][Choice[Math.Min(pl1, pl2)]][Choice[Math.Max(pl1, pl2)]];
                                    }
                            }
                        }
                    }
                }
                DrawButtons();
                DrawLines();
            }
        }

        private void DrawValuesRectangles(Button b1)
        {
            g.DrawRectangle(p, b1.Left + (b1.Width - cash_width) / 2, b1.Top + b1.Height, cash_width, cash_height);

            Font f = new System.Drawing.Font("Bookman Old Style", 8);

            GamePosition gp = (GamePosition)b1.Tag;
            int N = Information.players_number;
            if (gp.ButtonRow != 0)
                N = gp.parent.N;
            if (gp.cash.Count > 0)
            {
                int cell_width = cash_width / N;
                for (int cn = 1; cn <= N; cn++)
                {
                    Point pc0 = new Point(b1.Left + (b1.Width - cash_width) / 2 + cn * cell_width, b1.Top + b1.Height),
                        pc1 = new Point(pc0.X, pc0.Y + cash_height),
                        p4t = new Point(b1.Left + (b1.Width - cash_width) / 2 + (cn - 1) * cell_width + 1,
                            b1.Top + b1.Height + 1);
                    if (cn < N)
                        g.DrawLine(p, pc0, pc1);

                    g.DrawString(gp.cash[cn - 1].ToString(), f, Brushes.Black, p4t);
                }
            }
            else
            {
                int correction = TextRenderer.MeasureText("Undefined", f).Width;
                Point pp = new Point(b1.Left + (b1.Width - correction + 5) / 2 + 1, b1.Top + b1.Height + 2);
                g.DrawString("Undefined", f, Brushes.Black, pp);
            }
        }

        private void DrawLines()
        {
            for (int i = 0; i < lines.Count; i++)
                for (int j = 0; j < lines[i].labels.Count; j++)
                    lines[i].labels[j].Dispose();
            lines.Clear();
            if (pb.Image != null)
                g.Clear(Color.White);
            for (int i = 0; i < Information.tree_levels; i++)
                for (int j = 0; j < buttons[i].Count; j++)
                {
                    Button b1 = buttons[i][j];
                    DrawValuesRectangles(b1);

                    GamePosition b1GP = (b1.Tag as GamePosition);

                    if (i < Information.tree_levels - 1)
                    {
                        for (int k = 0; k < b1GP.children.Count; k++)
                        {
                            Button b2 = b1GP.children[k].button;
                            int x0 = b1.Left + b1.Width / 2, y0 = b1.Top + b1.Height + cash_height,
                                x3 = b2.Left + b2.Width / 2, y3 = b2.Top,
                                x1 = x0, y1 = (y3 + y0) / 2,
                                x2 = x3, y2 = y1;
                            Point p0 = new Point(x0, y0),
                                p1 = new Point(x1, y1),
                                p2 = new Point(x2, y2),
                                p3 = new Point(x3, y3);

                            Graphic_Interface.Line l = new Graphic_Interface.Line(pb, bmp, g, p0, p1, p2, p3);
                            lines.Add(l);
                            l.source = b1;
                            l.destination = b2;
                            if (b1GP.children[k].connected)
                                l.write_strategies(b1GP.children[k].StrategiesNames);
                        }
                    }
                }
            pb.Image = bmp;
        }

        private void DrawTree()
        {
            int levels = Information.tree_levels;

            CreateLabels();
            if (Alphabet.Count == 0)
                CreateAlphabet();

            List<List<Button>> buttons = CreateButtons(levels);

            DrawButtons();
            CorrectSizes();
            ClearScreen();
            DrawLines();
            DrawLevelsLadder();
            CorrectSizes();
        }

        private void DrawLevelsLadder()
        {
            LevelsPB.Height = pb.Height;
            Bitmap BMP = new Bitmap(LevelsPB.Width, LevelsPB.Height);
            Graphics G = Graphics.FromImage(BMP);
            List<int> Borders = new List<int>();
            int NewValue = panel1.VerticalScroll.Value;

            for (int i = 0; i < buttons.Count; i++)
                Borders.Add(buttons[i][0].Top + pb.Top);

            for (int i = 0; i < buttons.Count - 1; i++)
                Borders[i] = Borders[i] + (Borders[i + 1] + cash_height + buttons[i][0].Height - Borders[i]) / 2 - NewValue;
            Borders.RemoveAt(buttons.Count - 1);

            int BordersWidth = 30;
            int FirstVisible = 0;
            for (int i = 0; i<Borders.Count; i++)
                if (Borders[i] >= 0)
                {
                    FirstVisible = i;
                    break;
                }

            for (int i = 0; i < Borders.Count; i++)
            {
                if (Borders[i] >= 0)
                {
                    Point p0 = new Point(0, Borders[i]),
                        p1 = new Point(p0.X + BordersWidth, Borders[i]);
                    G.DrawLine(p, p0, p1);
                    if (Borders[i] >= LevelsLabels[i].Height)
                    {
                        if (i == FirstVisible)
                                LevelsLabels[i].Top = (Borders[i] - LevelsLabels[i].Height) / 2;
                        else
                        {
                            if (i == Borders.Count - 1)
                            {
                                LevelsLabels[i].Top = (Borders[i] + Borders[i - 1] - LevelsLabels[i].Height) / 2;
                                if (panel1.Height - Borders[i] >= LevelsLabels[i + 1].Height)
                                {
                                    LevelsLabels[i + 1].Show();
                                    LevelsLabels[i + 1].Top = (Borders[i] + panel1.Height - LevelsLabels[i].Height) / 2;
                                }
                                else
                                    LevelsLabels[i + 1].Hide();
                            }
                            else
                                LevelsLabels[i].Top = (Borders[i] + Borders[i - 1] - LevelsLabels[i].Height) / 2;
                        }
                        LevelsLabels[i].Left = (BordersWidth - LevelsLabels[i].Width) / 2;
                        LevelsLabels[i].Show();
                    }
                    else
                        LevelsLabels[i].Hide();
                }

            }
            //G.DrawLine(p, new Point(BordersWidth, 0), new Point(BordersWidth, LevelsPB.Height));
            LevelsPB.Image = BMP;
        }
        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            Application.Exit();
        }

        public void NewTreeData(string filename, int LevelsNumber, int ChildrenCount, int n, List<double> Values)
        {
            this.Text = filename;
            Information.tree_levels = LevelsNumber;
            Information.players_number = n;
            Information.InitialValues = Values;
            Information.ChildrenCount = ChildrenCount;
        }

        public void NewButtonData(string ButtonName, string ParentName)
        {
            NewButtonName = ButtonName;
            NewButtonParentName = ParentName;
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewWidth = this.Width;
            height = this.Height;

            NewTreeForm NTW = new NewTreeForm(this);
            NTW.StartPosition = FormStartPosition.CenterScreen;
            NTW.ShowDialog();

            if (NTW.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                settingsToolStripMenuItem.Visible = true;

                AlphabetLetterIndex = 0;
                Information.GamePositions.Clear();
                if (pb.Visible == false)
                {
                    pb.Show();
                    LevelsPB.Show();
                }

                cash_width = 0;
                for (int i = 0; i < Information.players_number; i++)
                    cash_width += TextRenderer.MeasureText
                        (Information.InitialValues[i].ToString(),
                        new System.Drawing.Font("Bookman Old Style", 8)).Width + 5;
                cash_width = Math.Max(70, cash_width);

                ChopOldTree();
                DrawTree();
                Information.PlayersNames.Clear();
                for (int i = 0; i < Information.players_number; i++)
                    Information.PlayersNames.Add("");
                this.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        private void ChopOldTree()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                for (int j = 0; j < buttons[i].Count; j++)
                    buttons[i][j].Dispose();
            }

            buttons.RemoveRange(0, buttons.Count);

            for (int i = 0; i < LevelsLabels.Count; i++)
                LevelsLabels[i].Dispose();
            LevelsLabels.Clear();

            for (int i = 0; i < lines.Count; i++)
                for (int j = 0; j < lines[i].labels.Count; j++)
                    lines[i].labels[j].Dispose();
            lines.Clear();

        }

        private void CreateLabels()
        {
            for (int i = 0; i < LevelsLabels.Count; i++)
                LevelsLabels[i].Dispose();
            LevelsLabels.Clear();
            for (int i = 0; i < Information.tree_levels; i++)
            {
                Label l = new Label();
                l.Text = (i + 1).ToString();
                l.Font = new System.Drawing.Font("Bookman Old Style", 16);
                l.Size = TextRenderer.MeasureText(l.Text, l.Font);
                l.Left = 0;
                l.Top = (i + 1) * 50;
                LevelsLabels.Add(l);
                LevelsPB.Controls.Add(l);
            }
        }

        private void CreateUserButtons()
        {
            List<int> ButtonsCountByLevel = new List<int>();
            for (int i = 0; i < Information.tree_levels; i++)
                ButtonsCountByLevel.Add(0);
            for (int i = 0; i < Information.GamePositions.Count; i++)
                ButtonsCountByLevel[Information.GamePositions[i].ButtonRow]++;

            for (int i = 0; i < Information.tree_levels; i++)
            {
                buttons.Add(new List<Button>());
                for (int j = 0; j < ButtonsCountByLevel[i]; j++)
                {
                    GamePosition GP = new GamePosition();
                    for (int k = 0; k<Information.GamePositions.Count; k++)
                        if ((Information.GamePositions[k].ButtonRow == i)&&
                            (Information.GamePositions[k].ButtonColumn == j))
                            GP = Information.GamePositions[k];

                    Button b = new Button();

                    if ((GP.name == "")||(GP.name == null))
                        b.Text = GP.ID;
                    else
                        b.Text = GP.name;

                    ButtonsProperties(b, buttonssize);
                    buttons[i].Add(b);
                    b.Tag = GP;
                    GP.button = b;
                    GP.N = Information.players_number;
                }
            }
        }

        private void CreateUserTree()
        {
            bmp = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bmp);
            pb.Image = bmp;
            
            ChopOldTree();

            if (pb.Visible == false)
            {
                pb.Show();
                LevelsPB.Show();
            }

            CreateAlphabet();
            AlphabetLetterIndex = Information.GamePositions.Count + 1;
            CreateLabels();
            CreateUserButtons();
            DrawButtons();
            CorrectSizes();
            ClearScreen();
            DrawLines();
            DrawLevelsLadder();
            CorrectSizes();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphic_Interface.TreeFileOpening TFO = new Graphic_Interface.TreeFileOpening(openFileDialog1);
            if (TFO.OpenFile())
            {
                cash_width = 0;
                for (int i = 0; i < Information.players_number; i++)
                    cash_width += TextRenderer.MeasureText
                        (Information.InitialValues[i].ToString(),
                        new System.Drawing.Font("Bookman Old Style", 8)).Width + 5;
                cash_width = Math.Max(70, cash_width);

                Information.GamePositions[0].cash = Information.InitialValues;

                CreateUserTree();

                for (int i = 0; i < Information.GamePositions.Count; i++)
                {
                    if (Information.GamePositions[i].defined)
                    {
                        PositionInformation_Form TEM = new PositionInformation_Form(this, Information.GamePositions[i]);
                        //TEM.SolveGame(Information.GamePositions[i]);
                        
                    }
                }
                CreateNamesBoard();
                DrawButtons();
                this.CenterToScreen();
                if (buttons[0][0].BackColor == Color.DarkSalmon)
                    solveToolStripMenuItem.Visible = true;
                settingsToolStripMenuItem.Visible = true;
            }            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphic_Interface.TreeFileWriting TFW = new Graphic_Interface.TreeFileWriting(saveFileDialog1,this.Text);
            TFW.WriteFile(buttons);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings f = new Settings();
            DebugClass.S = f;
            f.StartPosition = FormStartPosition.CenterParent;
            if (sender is Tree)
                f.Show();
            else
            {
                f.ShowDialog();
                CreateNamesBoard();
            }
        }
        
        public void UnitedCommandFunction()
        {
            DebugClass.T = this;
            this.Show();
            settingsToolStripMenuItem_Click(this, new EventArgs());
            DebugClass.S.PlayersGrid[0, 0].Value = "Company A";
            DebugClass.S.PlayersGrid[1, 0].Value = "Company B";
            DebugClass.S.SaveButton_Click(this, new EventArgs());
            CreateNamesBoard();

            buttons_MouseClick(buttons[0][0], new MouseEventArgs(System.Windows.Forms.MouseButtons.Left,
                1, buttons[0][0].Left + 2, buttons[0][0].Top + 2, 0));
            (buttons[0][0].Tag as GamePosition).N = 2;
            DebugClass.PIF.create_model_btn_Click(DebugClass.PIF, new EventArgs());

            DebugClass.BG.strat_grid_view[0, 0].Value = 4;
            DebugClass.BG.strat_grid_view[1, 0].Value = 2;
            DebugClass.BG.strat_grid_view_CellEndEdit(DebugClass.BG, new DataGridViewCellEventArgs(0, 0));
            DebugClass.BG.strat_grid_view_CellEndEdit(DebugClass.BG, new DataGridViewCellEventArgs(1, 0));
            //DebugClass.BG.navigation_grid_CellDoubleClick(DebugClass.BG, new DataGridViewCellEventArgs(1,0));
            //DebugClass.BG.setToolStripMenuItem_Click(DebugClass.BG, new EventArgs());

            //DebugClass.PSF.G[0, 0].Value = "Rrrr";
            //DebugClass.PSF.G_CellEndEdit(DebugClass.PSF, new DataGridViewCellEventArgs(0, 0));
            //DebugClass.PSF.G[2, 0].Value = 15;
            //DebugClass.PSF.G[3, 0].Value = 23;
            //DebugClass.PSF.finishToolStripMenuItem_Click(DebugClass.PSF, new EventArgs());

            //DebugClass.BG.viewToolStripMenuItem_Click(DebugClass.BG, new EventArgs());
            //DebugClass.BG.open_menu_Click(DebugClass.BG, new EventArgs());
            ////DebugClass.BG.done_btn_Click(this, new EventArgs());
            ////DebugClass.EFG.openToolStripMenuItem_Click(DebugClass.T, new EventArgs());0
            ////DebugClass.EFG.solveToolStripMenuItem_Click(DebugClass.T, new EventArgs());

            //DebugClass.BG.Show();
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnitedCommandFunction();

        }

        private List<GamePosition> FindBestResult()
        {
            List<GamePosition> OptPositions = new List<GamePosition>();
            List<double> MaxValues = new List<double>();
            for (int i = 0; i < Information.players_number; i++)
            {
                OptPositions.Add(new GamePosition());
                MaxValues.Add(0);
            }

            for (int i = 0; i < buttons.Count; i++)
                for (int j = 0; j < buttons[i].Count; j++)
                    if (buttons[i][j].BackColor == Color.MediumOrchid)
                    {
                        for (int k = 0; k < Information.players_number; k++)
                            if ((buttons[i][j].Tag as GamePosition).cash[k] > MaxValues[k])
                            {
                                OptPositions[k] = (buttons[i][j].Tag as GamePosition);
                                MaxValues[k] = (buttons[i][j].Tag as GamePosition).cash[k];
                            }
                    }
            return OptPositions;
        }

        private void solveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool AtLeastOne = false;
            for (int i = 0; i<buttons.Count; i++)
                for (int j = 0; j < buttons[i].Count; j++)
                {
                    if (buttons[i][j].BackColor == Color.MediumOrchid)
                        AtLeastOne = true;
                }

            if (AtLeastOne)
            {
                Point OldLocation = this.Location;
                this.Location = new Point(10, 50);

                List<GamePosition> OptPositions = FindBestResult();

                int MaxPosition = 0;
                double MaxValue = 0;

                for (int i = 0; i < Information.players_number; i++)
                {
                    double SumValue = 0;
                    for (int j = 0; j < Information.players_number; j++)
                        SumValue += OptPositions[i].cash[j];

                    if (SumValue / Information.players_number > MaxValue)
                    {
                        MaxValue = SumValue / Information.players_number;
                        MaxPosition = i;
                    }
                }

                GamePosition WayUpTop = OptPositions[MaxPosition];

                while (WayUpTop.parent != null)
                {
                    WayUpTop.button.BackColor = Color.Gold;
                    WayUpTop = WayUpTop.parent;
                }

                Report_Form RF = new Report_Form(OptPositions, MaxPosition);
                RF.StartPosition = FormStartPosition.Manual;
                RF.Location = new Point(this.Right, this.Top);
                RF.ShowDialog();

                this.Location = OldLocation;

                OptPositions[MaxPosition].button.BackColor = Color.MediumOrchid;
                WayUpTop = OptPositions[MaxPosition].parent;

                while (WayUpTop.parent != null)
                {
                    WayUpTop.button.BackColor = Color.DarkSalmon;
                    WayUpTop = WayUpTop.parent;
                }
            }
            else
                System.Windows.Forms.MessageBox.Show("No terminal positions found");
        }
    }
}
