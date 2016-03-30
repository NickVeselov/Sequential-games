using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace SequentialGames
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Tree F = new Tree();
            //ExtensiveFormGame F = new ExtensiveFormGame();
            //ParametersSettingsForm F = new ParametersSettingsForm();
            //FunctionEditor F = new FunctionEditor();
            F.StartPosition = FormStartPosition.CenterScreen;
            Application.Run(F);
        }
    }

    static class DebugClass
    {
        public static Tree T;
        public static PositionInformation_Form PIF;
        public static BimatrixGamesForm BG;
        public static ExtensiveFormGame EFG;
        public static Settings S;
        public static ParametersSettingsForm PSF;
    }

    static class Information
    {
        public static bool load = false;
        public static int tree_levels = 0;
        public static int players_number;
        public static int strnum_limit = 50;
        public static int MaxTreeLevelsCount = 9;
        public static int MaxPlayersNumber = 10;
        public static List<Double> InitialValues = new List<double>();
        public static List<GamePosition> GamePositions = new List<GamePosition>();
        public static int TreeType = 0;
        public static List<string> strategy_letters = new List<string>();
        public static int ChildrenCount = 2;

        //Settings
        public static List<double> PlayersIrrationalBehaviour = new List<double>();
        public static List<string> PlayersNames = new List<string>();
        public static List<string> AP_Names = new List<string>();
        public static List<string> AP_KeyLetters = new List<string>();
        public static List<List<double>> AP_Weights = new List<List<double>>();
        public static List<List<double>> AP_InitialValues = new List<List<double>>();

        static Information()
        {
            strategy_letters.Add("x");
            strategy_letters.Add("y");
            strategy_letters.Add("z");
            strategy_letters.Add("v");
            strategy_letters.Add("w");
            strategy_letters.Add("u");   
        }
    }

    static class ModellingInformation
    {
        public static int CritNumber = 0;
        public static int AltNumber = 0;

        public static List<string> CritNames = new List<string>();
        public static List<string> Measurements = new List<string>();
        public static List<double> WorstValues = new List<double>();
        public static List<double> BestValues = new List<double>();

        public static List<List<double>> Alternatives = new List<List<double>>();
    }

    public class GamePosition
    {
        public int CurrentDimension = 0;
        public int TotalDimensions = 1;

        public string name;
        public string ID = "";
        public int N = 3;
        public List<bool> ActivePlayers = new List<bool>();
        public List<List<double>> AdParamValues = new List<List<double>>();
        public List<List<string>> Weights = new List<List<string>>();

        //Tree orientation
        public bool defined = false,
        connected = false,
        active = false;
        public PositionInformation_Form InfoForm;
        public BimatrixGamesForm DataForm;

        public bool ValuesForm_Active = false;
        public bool WeightsForm_Active = false;
        public ParametersValuesForm PVF;
        public ParametersWeightsForm PWF;

        //public List<List<List<List<double>>>> payoffs = new List<List<List<List<double>>>>();
        //public List<payo
        public List<Payoff> payoffs = new List<Payoff>();

        public List<int> Strategies = new List<int>();
        public List<double> cash = new List<double>();
        public List<string> StrategiesNames = new List<string>();
        public List<List<double>> V = new List<List<double>>();
        public List<List<List<double>>> LHStrategy = new List<List<List<double>>>();
        public List<List<List<double>>> OptimalStrategy_R = new List<List<List<double>>>();
        public List<List<string>> Combinations = new List<List<string>>();
        public string SaveFilePath = "";

        public List<List<List<List<List<double>>>>> NumericalPayoffs = new List<List<List<List<List<double>>>>>();
        public List<List<List<List<double>>>> Utility = new List<List<List<List<double>>>>();

        public bool editing = false;
        public Button button;
        public int ButtonRow = 0,
            ButtonColumn = 0;

        //Tree part

        public GamePosition parent;
        public List<GamePosition> children = new List<GamePosition>();

        public GamePosition()
        {
            payoffs.Add(new Payoff());
        }

        public void ShowParametersForms(Form parent)
        {
            if (ValuesForm_Active)
                PVF.Close();
            else
                ValuesForm_Active = true;
            PVF = new ParametersValuesForm(this);
            PVF.StartPosition = FormStartPosition.Manual;
            PVF.Location = new Point(parent.Right + 5, parent.Top);
            PVF.Show();

            if (WeightsForm_Active)
                PWF.Close();
            else
                WeightsForm_Active = true;
            if (Weights.Count < 1)
            {
                Weights.Add(new List<string>());
                for (int i = 0; i < N; i++)
                    Weights[0].Add("1");
            }
            PWF = new ParametersWeightsForm(this);
            PWF.StartPosition = FormStartPosition.Manual;
            PWF.Location = new Point(PVF.Left, PVF.Bottom);
            PWF.Show();
        }

        public void CreateNewPayoffLayer()
        {
            payoffs.Add(new Payoff());
            for (int p = 0; p < payoffs[payoffs.Count - 2].Array.Count; p++)
            {
                payoffs.Last().Array.Add(new List<List<List<string>>>());
                for (int q = 0; q < payoffs[payoffs.Count - 2].Array[p].Count; q++)
                {
                    payoffs.Last().Array[p].Add(new List<List<string>>());
                    for (int r = 0; r < payoffs[payoffs.Count - 2].Array[p][q].Count; r++)
                    {
                        payoffs.Last().Array[p][q].Add(new List<string>());
                        for (int s = 0; s < payoffs[payoffs.Count - 2].Array[p][q][r].Count; s++)
                            payoffs.Last().Array[p][q][r].Add("");
                    }
                }
            }
        }

        public void CreateNumericalPayoff()
        {
            NumericalPayoffs.Clear();
            for (int d = 0; d < payoffs.Count; d++)
            {
                NumericalPayoffs.Add(new List<List<List<List<double>>>>());
                for (int i = 0; i < payoffs[d].Array.Count; i++)
                {
                    NumericalPayoffs[d].Add(new List<List<List<double>>>());
                    for (int j = 0; j < payoffs[d].Array[i].Count; j++)
                    {
                        NumericalPayoffs[d][i].Add(new List<List<double>>());
                        for (int k = 0; k < payoffs[d].Array[i][j].Count; k++)
                        {
                            NumericalPayoffs[d][i][j].Add(new List<double>());
                            for (int l = 0; l < payoffs[d].Array[i][j][k].Count; l++)
                                NumericalPayoffs[d][i][j][k].Add(0);                        
                        }
                    }
                }
            }
        }

        public void CalculatePayoffsValues()
        {
            CreateNumericalPayoff();
            for (int d = TotalDimensions - 1; d >= 0; d--)
            {
                for (int pl1 = 0; pl1 < N; pl1++)
                    for (int pl2 = 0; pl2 < N; pl2++)
                    {
                        if (pl1 < pl2)
                        {
                            for (int i = 0; i < Strategies[pl1]; i++)
                                for (int j = 0; j < Strategies[pl2]; j++)
                                {
                                    string s1 = payoffs[d].Array[pl1][pl2][i][j],
                                        s2 = payoffs[d].Array[pl2][pl1][i][j],
                                        CR1 = Graphic_Interface.Analyzer.CheckValidStringDouble(s1, 0, 0, false),
                                        CR2 = Graphic_Interface.Analyzer.CheckValidStringDouble(s2, 0, 0, false);

                                    if (s1 == "")
                                        NumericalPayoffs[d][pl1][pl2][i][j] = 0;
                                    else
                                    {
                                        if (CR1 != "")
                                            NumericalPayoffs[d][pl1][pl2][i][j] = Convert.ToDouble(CR1);
                                        else
                                        {
                                            Graphic_Interface.Function F1 = new Graphic_Interface.Function(s1);
                                            List<double> V1 = new List<double>();
                                            V1.Add(cash[pl1]);

                                            for (int k = 0; k < AdParamValues.Count; k++)
                                            {
                                                V1.Add(AdParamValues[k][pl1]);
                                                if ((d < k + 1) && (k < AdParamValues.Count - 1))
                                                    V1[V1.Count - 1] += NumericalPayoffs[k + 1][pl1][pl2][i][j];
                                            }
                                            F1.GetAllParameterNames(V1);
                                            string R1 = F1.CalculateValue();
                                            NumericalPayoffs[d][pl1][pl2][i][j] = Convert.ToDouble(R1);
                                        }

                                    }
                                    if (s2 == "")
                                        NumericalPayoffs[d][pl2][pl1][i][j] = 0;
                                    else
                                    {
                                        if (CR2 != "")
                                            NumericalPayoffs[d][pl2][pl1][i][j] = Convert.ToDouble(CR2);
                                        else
                                        {
                                            Graphic_Interface.Function F2 = new Graphic_Interface.Function(s2);
                                            List<double> V2 = new List<double>();
                                            V2.Add(cash[pl2]);
                                            for (int k = 0; k < AdParamValues.Count; k++)
                                            {
                                                V2.Add(AdParamValues[k][pl2]);
                                                if ((d < k + 1) && (k < AdParamValues.Count - 1))
                                                    V2[V2.Count - 1] += NumericalPayoffs[k + 1][pl2][pl1][i][j];
                                            }
                                            F2.GetAllParameterNames(V2);
                                            string R2 = F2.CalculateValue();
                                            NumericalPayoffs[d][pl2][pl1][i][j] = Convert.ToDouble(R2);
                                        }
                                    }
                                }
                        }
                    }
            }
        }

        public List<List<double>> CreateUtilityPayoffMatrix(int pl1, int pl2)
        {
            List<List<double>> P = new List<List<double>>();

            int n = Strategies[pl1],
                m = Strategies[pl2];

            if (pl1 > pl2)
            {
                n = Strategies[pl2];
                m = Strategies[pl1];
            }

            double sum = 0;
            List<double> W = new List<double>();
            for (int d = 0; d < TotalDimensions; d++)
                sum += Math.Abs(Convert.ToDouble(Weights[d][pl1]));
             
            for (int d = 0; d < TotalDimensions; d++)
                W.Add(Convert.ToDouble(Weights[d][pl1]) / sum);
            for (int i = 0; i<n; i++)
            {
                P.Add(new List<double>());
                for (int j = 0; j < m; j++)
                {
                    double Value = 0;
                    for (int d = 0; d < TotalDimensions; d++)
                    {
                        Value += (NumericalPayoffs[d][pl1][pl2][i][j] * W[d]);
                    }
                    P[i].Add(Value);
                }
            }

            return P;
        }
        //public List<List<double>> CalculateUtilityFunctionValue(int pl1, int pl2)
        //{
        //    List<List<double>> P = new List<List<double>>();
        //    List<List<List<string>>> S = new List<List<List<string>>>();
        //    for (int d = 0; d < TotalDimensions; d++)
        //        S.Add(payoffs[d].Array[pl1][pl2]);

        //    int s1 = Strategies[pl1],
        //        s2 = Strategies[pl2];

        //    if (pl1 > pl2)
        //    {
        //        s1 = Strategies[pl2];
        //        s2 = Strategies[pl1];
        //    }

        //    for (int d = 0; d < TotalDimensions; d++)
        //    {
        //        for (int i = 0; i < s1; i++)
        //        {
        //            P.Add(new List<double>());
        //            for (int j = 0; j < s2; j++)
        //            {
        //                List<double> V = new List<double>();
        //                List<double> W = new List<double>();


        //                if (S[d][i][j] == "")
        //                    V.Add(0);
        //                else
        //                {
        //                    //Value
        //                    string CheckResult = Graphic_Interface.Analyzer.CheckValidStringDouble(S[d][i][j], 0, 0, false);
        //                    if (CheckResult != "")
        //                        V.Add(Convert.ToDouble(CheckResult));
        //                    else
        //                    {
        //                        Graphic_Interface.Function F = new Graphic_Interface.Function(S[d][i][j]);
        //                        List<double> Values = new List<double>();
        //                        Values.Add(cash[pl1]);
        //                        for (int k = 0; k < AdParamValues.Count; k++)
        //                            Values.Add(AdParamValues[k][pl1]);
        //                        F.GetAllParameterNames(Values);
        //                        string Result = F.CalculateValue();
        //                        V.Add(Convert.ToDouble(Result));
        //                    }
        //                }

        //                //Weight
        //                string CheckR = Graphic_Interface.Analyzer.CheckValidStringDouble(Weights[d][pl1], 0, 0, false);
        //                if (CheckR != "")
        //                    W.Add(Convert.ToDouble(CheckR));
        //                else
        //                {
        //                    Graphic_Interface.Function F = new Graphic_Interface.Function(Weights[d][pl1]);
        //                    List<double> Values = new List<double>();
        //                    Values.Add(cash[pl1]);
        //                    for (int k = 0; k < AdParamValues.Count; k++)
        //                        Values.Add(AdParamValues[k][pl1]);
        //                    F.GetAllParameterNames(Values);
        //                    string Result = F.CalculateValue();
        //                    W.Add(Convert.ToDouble(Result));
        //                }
        //            }
        //            double sum = 0, value = 0;
        //            for (int d = 0; d < TotalDimensions; d++)
        //                sum += W[d];
        //            for (int d = 0; d < TotalDimensions; d++)
        //            {
        //                W[d] /= sum;
        //                value += W[d] * V[d];
        //            }
        //            P[i].Add(value);

        //        }
        //        return P;
        //    }
        //}
    }

    public class Payoff
    {
        public List<List<List<List<string>>>> Array = new List<List<List<List<string>>>>();
    }

    public class TreePosition
    {
        public TreePosition parent;
        public List<TreePosition> children = new List<TreePosition>();
        public Button button;
        public TextBox prizebox;

        public string name = "";
        public string ID = "";

        public string prize = "";

    }
}
