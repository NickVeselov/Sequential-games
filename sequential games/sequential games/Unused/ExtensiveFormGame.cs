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
    public partial class ExtensiveFormGame : Form
    {
        string Name1 = "",
            Name2 = "";

        //buttons and lines
        public List<List<Button>> buttons = new List<List<Button>>();
        Size buttonssize = new Size(40, 30);
        List<Color> ButtonsColors = new List<Color>();
        public List<Graphic_Interface.Line> lines = new List<Graphic_Interface.Line>();
        TextBox NameEdit = new TextBox();
        int Lv1GapBtwBtns = 50,
            Lv1GapBtwBrnchs = 80,
            Lv1Indent = 90;
        
        int AlphabetLetterIndex = 0;
        List<string> Alphabet = new List<string>();

        //graphics
        Pen p = new Pen(Color.Black, 1);
        Bitmap bmp;
        Graphics g;

        //tree
        List<TreePosition> Tree = new List<TreePosition>();
        List<TextBox> Prizes = new List<TextBox>();
        Button CurrentButton = new Button();
        TextBox CurrentPrizeBox = new TextBox();
        public string FormulaValue = "";
        bool Pasting = false;
        TreePosition ChildrenBuffer = new TreePosition();

        public BimatrixGamesForm BG;

        public ExtensiveFormGame()
        {
            InitializeComponent();
            TestingParameters();
            StartingInitialization();
        }

        public ExtensiveFormGame(BimatrixGamesForm BGF, string Player1, string Player2)
        {
            InitializeComponent();
            BG = BGF;
            Name1 = Player1;
            Name2 = Player2;
            StartingInitialization();

            //if ((BG.gp.AdParamValues.Count > 0) && (!Information.PDF_Active))
            //    viewToolStripMenuItem_Click(this, new EventArgs());
        }

        private void TestingParameters()
        {
            Name1 = "Swlabr";
            Name2 = "Ulysseus";
        }

        private void StartingInitialization()
        {
            panel1.AutoSize = false;
            panel1.AutoScroll = true;

            NameEdit.KeyDown += new KeyEventHandler(NameEdit_KeyDown);
            NameEdit.KeyPress += new KeyPressEventHandler(NameEdit_KeyPress);
            pb.Controls.Add(NameEdit);
            NameEdit.Hide();

            ButtonsColors.Add(Color.BurlyWood);
            ButtonsColors.Add(Color.DarkSeaGreen);
            ButtonsColors.Add(Color.LightBlue);
            ButtonsColors.Add(Color.LemonChiffon);
            ButtonsColors.Add(Color.Plum);
            ButtonsColors.Add(Color.Orange);
            newToolStripMenuItem_Click(this, new EventArgs());
        }

        void NameEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
                e.Handled = true;
        }

        private void ChopOldTree() 
        {
            //Information.PDF.Close();
            AlphabetLetterIndex = 0;
            for (int i = 0; i < buttons.Count; i++)
            {
                for (int j = 0; j < buttons[i].Count; j++)
                    buttons[i][j].Dispose();
            }

            buttons.RemoveRange(0, buttons.Count);

            for (int i = 0; i < lines.Count; i++)
                for (int j = 0; j < lines[i].labels.Count; j++)
                    lines[i].labels[j].Dispose();
            lines.Clear();

            Tree.Clear();

            PlayersPanel.Controls.Clear();
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

            for (int i = 0; i < 1000; i++)
                for (int j = 0; j < 26; j++)
                    Alphabet.Add(Alphabet[j] + (i + 1).ToString());

        }

        private void ButtonsProperties(Button btn, Size buttonssize, int Row)
        {
            btn.BackColor = ButtonsColors[Row % 2];
            btn.ForeColor = Color.Black;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Font = new System.Drawing.Font("Bookman Old Style", 12);
            btn.FlatStyle = FlatStyle.Popup;
            btn.Size = buttonssize;
            btn.Width = Math.Max(buttonssize.Width, (TextRenderer.MeasureText(btn.Text, btn.Font)).Width + 20);

            //btn.MouseClick += new MouseEventHandler(buttons_MouseClick);
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

                if (ChildrenBuffer.children.Count == 0)
                    CMS.Items[2].Enabled = false;

                CurrentButton = (sender as Button);
                (sender as Button).ContextMenuStrip = CMS;
                (sender as Button).ContextMenuStrip.Show(Cursor.Position);
            }
        }

        void PasteChildren(object sender, EventArgs e)
        {
            Pasting = true;
            TreePosition TP = (CurrentButton.Tag as TreePosition);
            int ChildrenCount = TP.children.Count;
            for (int i = 0; i < ChildrenCount; i++)
            {
                CurrentButton = TP.children[0].button;
                DeletePosition(this, new EventArgs());
            }

            for (int i = 0; i < ChildrenBuffer.children.Count; i++)
            {
                CurrentButton = TP.button;
                AddPositions(this, new EventArgs());
                if (ChildrenBuffer.children[i].name == "")
                    TP.children[i].button.Text = ChildrenBuffer.children[i].ID;
                else
                {
                    CurrentButton = TP.children[i].button;
                    string Name = ChildrenBuffer.children[i].name;
                    CurrentButton.Text = Name;
                    bool MoreThanJustAWord = false;
                    for (int j = 0; j < Name.Length; j++)
                        if ((Name[j] == ' ') || (Name[j] == '\n'))
                            MoreThanJustAWord = true;

                    CurrentButton.Width = Math.Max(TextRenderer.MeasureText
                        (Name, CurrentButton.Font).Width + 8, buttonssize.Width);
                    

                    if (MoreThanJustAWord)
                        CurrentButton.Text = WriteWordInFewLines(Name);
                }
                TP.children[i].name = ChildrenBuffer.children[i].name;
                Pasting = false;
            }

            DrawButtons();
            CorrectPictureBoxSize();
            ClearScreen();
            DrawLines();
            DrawPrizes();
        }

        void CopyChildren(object sender, EventArgs e)
        {
            ChildrenBuffer = (CurrentButton.Tag as TreePosition);
        }

        void RenamePosition(object sender, EventArgs e)
        {
            Button b = CurrentButton;
            NameEdit.Size = new Size(b.Width - 1, b.Height - 1);
            NameEdit.Font = new Font("Bookman Old Style", 13);
            NameEdit.Left = b.Left + 1;
            NameEdit.Width = b.Width - 2;
            NameEdit.Top = b.Top + 1;
            NameEdit.Tag = b;

            StringBuilder s = new StringBuilder();
            s.Append(b.Text);

            for (int i = 0; i < b.Text.Length; i++)
                if (s[i] == '\n')
                    s[i] = ' ';

            b.Text = s.ToString();

            NameEdit.Text = b.Text;
            NameEdit.Show();
            NameEdit.Focus();
            NameEdit.BringToFront();
            NameEdit.SelectionStart = b.Text.Length;
        }

        private void CreateNewPosition()
        {
            Button btn = new Button();
            btn.Text = Alphabet[AlphabetLetterIndex++];

            for (int i = 0; i < buttons.Count; i++)
                for (int j = 0; j < buttons[i].Count; j++)
                {
                    if (buttons[i][j] == CurrentButton)
                    {
                        ButtonsProperties(btn, buttonssize, i + 1);
                        if (i == buttons.Count - 1)
                            buttons.Add(new List<Button>());

                        buttons[i + 1].Add(btn);
                        TreePosition LastChild = new TreePosition();
                        bool PreviousFound = false;
                        if (((TreePosition)buttons[i][j].Tag).children.Count > 0)
                        {
                            //find fathers youngest son
                            LastChild = ((TreePosition)buttons[i][j].Tag).children.Last();
                            PreviousFound = true;
                        }
                        else
                        {
                            for (int t = j; t >= 0; t--)
                            {
                                if (((TreePosition)buttons[i][t].Tag).children.Count > 0)
                                {
                                    //father has no sons; try to find youngest father son
                                    LastChild = ((TreePosition)buttons[i][t].Tag).children.Last();
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
                                if ((buttons[i + 1][k].Tag as TreePosition) == LastChild)
                                {
                                    for (int l = buttons[i + 1].Count - 1; l > k + 1; l--)
                                        buttons[i + 1][l] = buttons[i + 1][l - 1];
                                    if (k < buttons[i + 1].Count - 1)
                                        buttons[i + 1][k + 1] = btn;
                                    break;
                                }
                            }
                        }
                        TreePosition NewPosition = new TreePosition();
                        NewPosition.parent = (buttons[i][j].Tag as TreePosition);
                        NewPosition.button = btn;
                        NewPosition.ID = btn.Text;
                        Tree.Add(NewPosition);

                        (CurrentButton.Tag as TreePosition).children.Add(NewPosition);
                        btn.Tag = NewPosition;
                        pb.Controls.Add(btn);

                        break;

                    }
                }
        }

        void AddPositions(object sender, EventArgs e)
        {
            if (((CurrentButton.Tag as TreePosition).children.Count == 0)&&(!Pasting))
                CreateNewPosition();
            CreateNewPosition();

            DrawButtons();
            CorrectPictureBoxSize();
            ClearScreen();
            DrawLines();
            DrawPrizes();
        }

        private void RecursiveDeletion(TreePosition TP)
        {
            bool erased = false;

            while (!erased)
            {
                if (TP.children.Count > 0)
                {
                    for (int i = 0; i < TP.children.Count; i++)
                        RecursiveDeletion(TP.children[i]);
                    TP.children.Clear();
                }

                for (int i = 0; i < buttons.Count; i++)
                    buttons[i].Remove(TP.button);

                Tree.Remove(TP);
                TP.button.Dispose();

                erased = true;
            }
        }
        
        void DeletePosition(object sender, EventArgs e)
        {
            TreePosition TP = (CurrentButton).Tag as TreePosition;

            if (TP.parent != null)
            {
                if ((TP.parent.children.Count == 2)&&(!Pasting))
                {
                    string ErrorMSG = "It is meaningless to leave only 1 alternative.\nMaybe you want to delete the branch?";
                    if (TP.parent.name != "")
                        ErrorMSG += " (" + TP.parent.name + " position)";
                    else
                        ErrorMSG += " ('" + TP.parent.ID + "' position)";
                    DialogResult DR = System.Windows.Forms.MessageBox.Show(ErrorMSG, "Leaving no choice", MessageBoxButtons.YesNoCancel);

                    if (DR == System.Windows.Forms.DialogResult.Yes)
                    {
                        CurrentButton = TP.parent.button;
                        DeletePosition(this, new EventArgs());
                    }
                }
                else
                {
                    RecursiveDeletion(TP);
                    TP.parent.children.Remove(TP);

                    int bc = buttons.Count,
                        EmptyLevels = 0;

                    for (int i = 0; i < bc; i++)
                    {
                        if (buttons[i - EmptyLevels].Count == 0)
                            buttons.RemoveAt(i - EmptyLevels++);
                    }

                    CorrectPictureBoxSize();
                    DrawButtons();
                    DrawLines();
                    DrawPrizes();
                }
            }
            else
                System.Windows.Forms.MessageBox.Show("In order to start fresh, click 'New' at the Top menu.");
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
                    TextRenderer.MeasureText(CurrentString, CurrentButton.Font).Width + 10);
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
            CurrentButton.Height = Height * buttonssize.Height;
            if (Height > 1)
                CurrentButton.Width = MaxWordWidth;

            return s;
        }

        void NameEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NameEdit.Hide();
                ((NameEdit.Tag as Button).Tag as TreePosition).name = NameEdit.Text;

                bool MoreThanJustAWord = false;
                for (int i = 0; i < NameEdit.Text.Length; i++)
                    if ((NameEdit.Text[i] == ' ') || (NameEdit.Text[i] == '\n'))
                        MoreThanJustAWord = true;

                (NameEdit.Tag as Button).Width = Math.Max(TextRenderer.MeasureText
                    (NameEdit.Text, (NameEdit.Tag as Button).Font).Width + 8, buttonssize.Width);

                if (MoreThanJustAWord)
                    NameEdit.Text = WriteWordInFewLines(NameEdit.Text);


                CurrentButton.Text = NameEdit.Text;
                (NameEdit.Tag as Button).Left -= (TextRenderer.MeasureText
                    ((NameEdit.Tag as Button).Text, (NameEdit.Tag as Button).Font).Width + 8 - (NameEdit.Tag as Button).Width) / 2;


                DrawButtons();
                CorrectPictureBoxSize();
                ClearScreen();
                DrawLines();
                DrawPrizes();
            }
        }

        public void FindNotEmptyBranch(List<int> NewButtonsPositions, ref TreePosition BranchGP, int CurrentLevel, ref int ParentIndex, int ChildIndex)
        {
            int LowerLevel = CurrentLevel+1;
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
                                if ((buttons[i][l].Tag as TreePosition).children.Count != 0)
                                {
                                    FatherIndex = l;
                                    break;
                                }
                            }
                            if (FatherIndex != -1)
                            {
                                Button FirstMovedButton = (buttons[i][FatherIndex].Tag as TreePosition).children[0].button;
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
                    {
                        NewButtonsPositions.Add(NewButtonsPositions.Last() + Addition);
                        CorrectNoChildrenPosition(NewButtonsPositions, CurrentLevel, NewButtonsPositions.Count - 1);
                    }
                    BranchGP = (TreePosition)buttons[CurrentLevel][++ParentIndex].Tag;
                }
                else
                    Found = true;
            }
        }

        private void CorrectNoChildrenPosition(List<int> NewButtonsPositions, int CurrentLevel, int ParentIndex)
        {
            if (ParentIndex > 0)
            {
                TreePosition Current = (TreePosition)buttons[CurrentLevel][ParentIndex].Tag;
                TreePosition Previous = (TreePosition)buttons[CurrentLevel][ParentIndex - 1].Tag;
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
                x0,x1,
                ParentIndex = 0;
            List<int> NewButtonsPositions = new List<int>();
            TreePosition BranchGP = (TreePosition)buttons[CurrentLevel][ParentIndex].Tag;

            FindNotEmptyBranch(NewButtonsPositions,ref BranchGP, CurrentLevel, ref ParentIndex, 0);
            x0 = buttons[LowerLevel][0].Left;
            x1 = buttons[LowerLevel][0].Right;
            for (int i = 0; i < buttons[LowerLevel].Count; i++)
            {
                TreePosition ButtonGP = ((TreePosition)buttons[LowerLevel][i].Tag).parent;

                if (BranchGP == ButtonGP)
                    x1 = buttons[LowerLevel][i].Right;
                else
                {
                    NewButtonsPositions.Add((x0 + x1 - buttons[CurrentLevel][ParentIndex].Width) / 2);
                    CorrectNoChildrenPosition(NewButtonsPositions, CurrentLevel, ParentIndex);

                    BranchGP = (TreePosition)buttons[CurrentLevel][++ParentIndex].Tag;
                    FindNotEmptyBranch(NewButtonsPositions, ref BranchGP, CurrentLevel, ref ParentIndex, i);

                    x0 = buttons[LowerLevel][i].Left;
                    x1 = buttons[LowerLevel][i].Right;
                }
            }
            NewButtonsPositions.Add((x0 + x1 - buttons[CurrentLevel][ParentIndex].Width) / 2);
            CorrectNoChildrenPosition(NewButtonsPositions, CurrentLevel, ParentIndex);
            for (int i = NewButtonsPositions.Count; i < buttons[CurrentLevel].Count; i++)
            {
                NewButtonsPositions.Add(NewButtonsPositions.Last() + buttons[CurrentLevel][i].Width + Lv1GapBtwBrnchs);
                CorrectNoChildrenPosition(NewButtonsPositions, CurrentLevel, ++ParentIndex);
            }
            return NewButtonsPositions;
        }

        private List<List<Button>> CreateButtons(List<string> alphabet)
        {
            for (int i = 0; i < 3; i++)
            {
                buttons.Add(new List<Button>());
                for (int j = 0; j < Math.Pow(2,i); j++)
                {
                    Button b = new Button();
                    b.Text = alphabet[AlphabetLetterIndex++];
                    ButtonsProperties(b, buttonssize,i);
                    buttons[i].Add(b);

                    TreePosition TP = new TreePosition();
                    Tree.Add(TP);
                    b.Tag = TP;
                    TP.ID = b.Text;
                    TP.button = b;

                    if (i > 0)
                    {
                        TreePosition Parent = (buttons[i - 1][j / 2].Tag as TreePosition);
                        TP.parent = Parent;
                        Parent.children.Add(TP);
                    }
                }
            }
            return buttons;
        }

        private void ClearScreen()
        {
            bmp = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bmp);
            pb.Image = bmp;
        }

        private void CorrectPictureBoxSize()
        {
            int MaxWidth = 0, MaxHeight = buttons.Last()[0].Bottom + 100;
            for (int i = 0; i < buttons.Count; i++)
                for (int j = 0; j < buttons[i].Count; j++)
                {
                    MaxWidth = Math.Max(MaxWidth, buttons[i][j].Right);
                }

            for (int i = 0; i < Prizes.Count; i++)
                MaxHeight = Math.Max(MaxHeight, Prizes[i].Bottom + menuStrip1.Height + 40);

            pb.Size = new Size(MaxWidth + Lv1Indent + 20, MaxHeight);
            if (pb.Width < 1500)
                this.Width = pb.Width + 16;
            else
                this.Width = 1500;
            if (pb.Height < 1200)
                this.Height = pb.Height + menuStrip1.Height + 39;
            else
                this.Height = 1200;

            this.CenterToScreen();

        }

        private void DrawButtons()
        {
            int indent = Lv1Indent,
                between_branches = Lv1GapBtwBrnchs,
                between_vertexes = Lv1GapBtwBtns,
                LastLevel = buttons.Count - 1;

            List<int> Positions = new List<int>();

            Positions.Add(Lv1Indent);

            TreePosition BranchGP = ((TreePosition)buttons[LastLevel][0].Tag).parent;
            for (int i = 1; i < buttons[LastLevel].Count; i++)
            {
                TreePosition ButtonGP = ((TreePosition)buttons[LastLevel][i].Tag).parent;
                if (BranchGP == ButtonGP)
                    Positions.Add(Positions[Positions.Count - 1] + Lv1GapBtwBtns + buttons[LastLevel][i - 1].Width);
                else
                {
                    Positions.Add(Positions[Positions.Count - 1] + Lv1GapBtwBrnchs + buttons[LastLevel][i - 1].Width);
                    BranchGP = ButtonGP;
                }
            }

            for (int i = 0; i < LastLevel + 1; i++)
            {
                if (i > 0)
                {
                    Positions.Clear();
                    Positions = CalculateNewGaps(LastLevel - i);
                }
                for (int j = 0; j < buttons[LastLevel - i].Count; j++)
                {
                    buttons[LastLevel - i][j].Left = Positions[j];
                }
            }
            int CurrentTop = 50;
            for (int i = 0; i < buttons.Count; i++)
            {
                int MaxHeight = 0;
                for (int j = 0; j < buttons[i].Count; j++)
                {
                    buttons[i][j].Top = CurrentTop;
                    MaxHeight = Math.Max(MaxHeight, buttons[i][j].Height);
                }
                CurrentTop += MaxHeight + 60;
            }
        }

        private void DrawLines()
        {
            if (pb.Image != null)
                g.Clear(Color.White);

            lines.Clear();

            for (int i = 0; i < buttons.Count; i++)
                for (int j = 0; j < buttons[i].Count; j++)
                {
                    Button b1 = buttons[i][j];
                    TreePosition b1GP = (b1.Tag as TreePosition);

                    if (i < buttons.Count - 1)
                    {
                        for (int k = 0; k < b1GP.children.Count; k++)
                        {
                            Button b2 = b1GP.children[k].button;
                            int x0 = b1.Left + b1.Width / 2, y0 = b1.Top + b1.Height,
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
                        }
                    }
                }
            pb.Image = bmp;
        }

        private void DrawPrizes()
        {
            for (int i = 0; i < Tree.Count; i++)
            {
                if (Tree[i].prizebox != null)
                    if (Tree[i].prizebox.Text != "0")
                        Tree[i].prize = Tree[i].prizebox.Text;
            }


            if (Prizes.Count > 0)
            {
                int PBC = Prizes.Count;
                for (int i = 0; i < PBC; i++)
                    Prizes[i].Dispose();
                Prizes.Clear();
            }

            for (int i = 0; i < buttons.Count; i++)
                for (int j = 0; j < buttons[i].Count; j++)
                {
                    if ((buttons[i][j].Tag as TreePosition).children.Count == 0)
                    {
                        TextBox T = new TextBox();
                        T.Width = buttons[i][j].Width;
                        T.Left = buttons[i][j].Left;
                        T.Top = buttons[i][j].Bottom + 2 * T.Height;
                        T.Font = buttons[i][j].Font;
                        T.TextAlign = HorizontalAlignment.Center;
                        TreePosition TP = (buttons[i][j].Tag as TreePosition);
                        TP.prizebox = T;
                        T.Tag = TP;
                        if (TP.prize != "")
                        {
                            Size S = TextRenderer.MeasureText(TP.prize, T.Font);
                            T.Text = TP.prize;
                            if (S.Width > T.Width)
                                T.Width = Math.Min(S.Width, buttons[i][j].Width + 30);
                        }
                        else
                            T.Text = "0";

                        T.Left = (buttons[i][j].Width - T.Width) / 2 + buttons[i][j].Left;
                        T.MouseDown += new MouseEventHandler(PrizeMouseDown);
                        pb.Controls.Add(T);
                        Prizes.Add(T);

                        g.DrawLine(p, new Point(buttons[i][j].Left + buttons[i][j].Width / 2, buttons[i][j].Bottom),
                            new Point(buttons[i][j].Left + buttons[i][j].Width / 2, T.Top));
                    }
                }
        }

        void PrizeMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenuStrip CMS = new System.Windows.Forms.ContextMenuStrip();
                CMS.Items.Add("Add formula");
                CMS.Items[0].Click += new EventHandler(AddFormula);

                CurrentPrizeBox = (sender as TextBox);
                (sender as TextBox).ContextMenuStrip = CMS;
                (sender as TextBox).ContextMenuStrip.Show(Cursor.Position);
            }
        }

        void AddFormula(object sender, EventArgs e)
        {
            if (BG.gp.AdParamValues.Count == 0)
                System.Windows.Forms.MessageBox.Show("In order to make function you have to enable additional parameters first.");
            else
            {
                //FunctionEditor FE = new FunctionEditor(this, ref CurrentPrizeBox, BG.gp);
                //FE.StartPosition = FormStartPosition.CenterScreen;
                //DialogResult D = FE.ShowDialog();

                //CurrentPrizeBox.Text = FormulaValue.ToString();
            }
        }

        private void DrawTree()
        {
            CreateAlphabet();
            List<List<Button>> buttons = CreateButtons(Alphabet);

            DrawButtons();
            CorrectPictureBoxSize();
            ClearScreen();
            DrawLines();
            DrawPrizes();
            CorrectPictureBoxSize();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Prizes.Count; i++)
            {
                if ((Prizes[i].Text != "0")&&(Prizes[i].Text != ""))
                    (Prizes[i].Tag as TreePosition).prize = Prizes[i].Text;
            }

            List<List<string>> Parameters = new List<List<string>>();
            for (int i = 0; i < Information.AP_Names.Count; i++)
            {
                Parameters.Add(new List<string>());
                Parameters[i].Add(Information.AP_Names[i]);
                Parameters[i].Add(Information.AP_KeyLetters[i]);
                //Parameters[i].Add(BG.gp.AdParamValues[i]);
            }
            Graphic_Interface.SimpleTreeFileManagement STFM = 
                new Graphic_Interface.SimpleTreeFileManagement
                    (saveFileDialog1,buttons,Tree,Name1,Name2,Parameters);
            STFM.WriteFile();
        }

        public void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool manualy;
            if (sender is ToolStripDropDownItem)
                manualy = true;
            else
            {
                manualy = false;
                openFileDialog1.FileName = "C:\\Users\\Nick\\Desktop\\Diploma\\Tree.txt";
            }

            Graphic_Interface.SimpleTreeFileManagement STFM =
                new Graphic_Interface.SimpleTreeFileManagement(openFileDialog1);
            if (STFM.OpenFile(manualy))
            {
                ChopOldTree();

                buttons = STFM.GetButtons();
                Tree = STFM.GetTree();
                string Names = STFM.GetNames();
                Name1 = Names.Substring(0, Graphic_Interface.Analyzer.FindSeparator(Names, '+'));
                Name2 = Names.Substring(Graphic_Interface.Analyzer.FindSeparator(Names, '+') + 1);

                List<List<string>> Parameters = STFM.GetParameters();
                BG.gp.AdParamValues.Clear();
                for (int i = 0; i < Parameters.Count; i++)
                {
                    bool found = false;
                    for (int j = 0; j < Information.AP_Names.Count; j++)
                    {
                        if (Information.AP_Names[j] == Parameters[i][0])
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        Information.AP_Names.Add(Parameters[i][0]);
                        Information.AP_KeyLetters.Add(Parameters[i][1]);
                        Information.AP_InitialValues.Add(new List<double>());
                        for (int j = 0; j < Information.AP_InitialValues.Count; j++)
                            Information.AP_InitialValues.Last().Add(0);
                    }

                    //BG.gp.AdParamValues.Add(Parameters[i][2]);
                }

                for (int i = 0; i < buttons.Count; i++)
                    for (int j = 0; j < buttons[i].Count; j++)
                    {
                        CurrentButton = buttons[i][j];
                        ButtonsProperties(buttons[i][j], buttonssize, i);
                        buttons[i][j].Text = WriteWordInFewLines(buttons[i][j].Text);
                    }


                AlphabetLetterIndex = Tree.Count;

                DrawButtons();
                CorrectPictureBoxSize();
                ClearScreen();
                DrawLines();
                DrawPrizes();
                DrawPlayersPanel();

                if (BG.gp.AdParamValues.Count > 0)
                    viewToolStripMenuItem_Click(this, new EventArgs());

            }
        }

        private void DrawPlayersPanel()
        {
            PlayersPanel.Controls.Add(PlayersLabel);
            int MaxWidth = 0;
            for (int i = 0; i < 2; i++)
            {
                Label l = new Label();
                l.ForeColor = ButtonsColors[i];
                l.BackColor = Color.Black;
                if (i == 0)
                    l.Text = Name1;
                else
                    l.Text = Name2;
                l.Font = new System.Drawing.Font("Bookman Old Style", 10);
                l.Size = TextRenderer.MeasureText(l.Text, l.Font);
                MaxWidth = Math.Max(MaxWidth, l.Size.Width);
                l.Left = 5;
                l.Top = i * 20 + PlayersLabel.Bottom + 5;
                PlayersPanel.Controls.Add(l);
            }
            PlayersPanel.Height = PlayersPanel.Controls[PlayersPanel.Controls.Count - 1].Bottom + 10;
            PlayersPanel.Width = MaxWidth + 20;
        }

        private void playerInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings f = new Settings();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog();
        }

        public void solveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool AtLeastOneIsFilled = false;
            for (int i = 0; i < Prizes.Count; i++)
            {
                if ((Prizes[i].Text != "") && (Prizes[i].Text != "0"))
                    AtLeastOneIsFilled = true;
            }

            if (AtLeastOneIsFilled)
            {
                for (int i = 0; i < Prizes.Count; i++)
                    (Prizes[i].Tag as TreePosition).prize = Prizes[i].Text;
                List<int> MaxChildrenCount = new List<int>();
                for (int i = 0; i < buttons.Count - 1; i++)
                {
                    MaxChildrenCount.Add(0);
                    for (int j = 0; j < buttons[i].Count; j++)
                        MaxChildrenCount[i] = Math.Max((buttons[i][j].Tag as TreePosition).children.Count, MaxChildrenCount[i]);
                }
                BG.ModelViaExtensiveFormGame(Tree, MaxChildrenCount);
                this.Close();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChopOldTree();
            DrawTree();
            DrawPlayersPanel();
        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParametersSettingsForm PSF = new ParametersSettingsForm();
            PSF.StartPosition = FormStartPosition.CenterScreen;
            PSF.ShowDialog();

            viewToolStripMenuItem_Click(this, new EventArgs());
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (!Information.APF_Active)
            //if (Information.AP_Names.Count > 0)
            //{
            //    Information.PDF_Active = true;
            //    Information.PDF = new ParametersValuesForm(BG.gp);
            //    Information.PDF.StartPosition = FormStartPosition.Manual;
            //    Information.PDF.Location = new Point(this.Right + 5, this.Top);
            //    Information.PDF.Show();
            //}
            //else
            //{
            //    System.Windows.Forms.MessageBox.Show("No additional parameters are set");
            //}
            //}
            //else
            //{
            //    Point L = Information.PDF.Location;
            //    Information.PDF.Close();
            //    ParametersValuesForm PDF = new ParametersValuesForm(this, BG.gp);
            //    PDF.Location = L;
            //    PDF.Show();
            //}
        }

    }
}
