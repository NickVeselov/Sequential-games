using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GameTheory
{
    class LemkeHowson
    {
        private List<List<double>> Ao = new List<List<double>>();
        private List<List<double>> Bo = new List<List<double>>();

        private List<double> Xo = new List<double>();
        private List<double> Yo = new List<double>();
        private List<List<string>> P = new List<List<string>>();
        private List<List<string>> Q = new List<List<string>>();
        private List<bool> a_f_check = new List<bool>();
        private List<bool> b_e_check = new List<bool>();

        private List<List<double>> A1 = new List<List<double>>();
        private List<double> epsilon_A = new List<double>();
        private List<int> basis_A = new List<int>();
        private List<double> lambda_A = new List<double>();
        
        private List<List<double>> B1 = new List<List<double>>();
        private List<double> epsilon_B = new List<double>();
        private List<int> basis_B = new List<int>();
        private List<double> lambda_B = new List<double>();

        public List<int> opt_stratnum = new List<int>();
        public List<double> strat1 = new List<double>();
        public List<double> strat2 = new List<double>();
        public double prize_1 = 0;
        public double prize_2 = 0;

        private int CycleStuck = 0;
        public int n;
        public int m;
        public List<List<double>> A;
        public List<List<double>> B;

        //private int iteration = 0;
        private double d;

        private int row_n = 0;
        private int next_row_n;
        private int col_n;

        private double a;
        private double b;

        public LemkeHowson(int s1, int s2)
        {
            B = new List<List<double>>();
            A = new List<List<double>>();
            if ((s1 < 1) || (s2 < 1))
                System.Windows.Forms.MessageBox.Show("Invalid Strategies number!");
            else
            {
                n = s1; m = s2;
                for (int i = 0; i < n; i++)
                {
                    A.Add(new List<double>());
                    for (int j = 0; j < m; j++)
                    {
                        A[i].Add(0);
                    }
                }
            }
        }

        private void init_all_lists()
        {
            for (int i = 0; i<m; i++)
            {
                Ao.Add(new List<double>());
                A1.Add(new List<double>());
                for (int j = 0; j < n + m; j++)
                {
                    Ao[i].Add(0);
                    A1[i].Add(0);
                }

            }
            for (int i = 0; i < n; i++)
            {
                Bo.Add(new List<double>());
                B1.Add(new List<double>());
                for (int j = 0; j < n + m; j++)
                {
                    Bo[i].Add(0);
                    B1[i].Add(0);
                }
            }

            for (int i = 0; i < n; i++)
            {
                Xo.Add(0);
                epsilon_A.Add(0);
                basis_B.Add(0);
                lambda_B.Add(0);
                //strat1.Add(0);
            }
            for (int j = 0; j < m; j++)
            {
                Yo.Add(0);
                epsilon_B.Add(0);
                basis_A.Add(0);
                lambda_A.Add(0);
                //strat2.Add(0);
            }

            for (int i = 0; i < n; i++)
            {
                P.Add(new List<string>());
                for (int j = 0; j < n; j++)
                    P[i].Add("");
            }
            for (int i = 0; i < m; i++)
            {
                Q.Add(new List<string>());
                for (int j = 0; j < m; j++)
                {
                    Q[i].Add("");
                }
            }

            for (int i = 0; i < n; i++)
                a_f_check.Add(false);
            for (int i = 0; i < m; i++)
                b_e_check.Add(false);

            opt_stratnum.Add(-1);
            opt_stratnum.Add(-1);
        }

        private void find_min_d()
        {
            d = A[0][0];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    d = Math.Max(d, A[i][j]);
                    d = Math.Max(d, B[i][j]);
                }
            d++;
        }

        private void create_expanded_matrix(List<List<double>> M, 
            List<List<double>> old_M, int type)
        {
            int kk = M[0].Count - M.Count;
            for (int i = 0; i < M.Count; i++)
                for (int j = 0; j < M[0].Count; j++)
                {
                    if (j < kk)
                    {
                        if (type == 1)
                            M[i][j] = (d - old_M[i][j]);
                        else
                            M[i][j] = (d - old_M[j][i]);
                    }
                    else
                    {
                        if (j == kk + i)
                            M[i][j] = 1;
                        else
                            M[i][j] = 0;
                    }
                }
        }

        private void define_strategy_vectors()
        {
            a = Ao[row_n][0];
            for (int j = 0; j < n; j++)
            {
                if (Ao[row_n][j] < a)
                    col_n = j;
                a = Math.Min(a, Ao[row_n][j]);
            }

            b = Bo[col_n][0];
            for (int i = 0; i < m; i++)
            {
                if (Bo[col_n][i] < b) next_row_n = i;
                b = Math.Min(b, Bo[col_n][i]);
            }

            for (int i = 0; i < n; i++)
            {
                if (i == col_n)
                    Xo[i] = 1 / b;
                else
                    Xo[i] = 0;
            }
            for (int i = 0; i < m; i++)
            {
                if (i == row_n)
                    Yo[i] = 1 / a;
                else
                    Yo[i] = 0;
            }
        }

        private void define_symbolic_matrix(List<List<string>> M, 
            int c, int r, char symb1, char symb2)
        {
            for (int i = 0; i < M.Count; i++)
                for (int j = 0; j < M[i].Count; j++)
                {
                    if (j == r)
                        M[i][j] = symb2 + c.ToString();
                    else
                        M[i][j] = symb1 + j.ToString();
                }
        }

        private void fill_symb_string(List<string> S)
        {
            for (int i = 0; i<S.Count; i++)
                {
                    if ((S[i][0] == 'a') || (S[i][0] == 'f'))
                    {
                        a_f_check[Int32.Parse(S[i][1].ToString())] = true;
                    }
                    else
                    {
                        b_e_check[Int32.Parse(S[i][1].ToString())] = true;
                    }
                }
        }

        private bool check_end()
        {
            bool end;

            for (int Pi = 0; Pi<n; Pi++)
                for (int Qi = 0; Qi < m; Qi++)
                {
                    for (int i = 0; i < n; i++)
                        a_f_check[i] = false;
                    for (int i = 0; i < m; i++)
                        b_e_check[i] = false;

                    end = true;
                    fill_symb_string(P[Pi]);
                    fill_symb_string(Q[Qi]);
                    for (int i = 0; i < m; i++)
                    {
                        if (b_e_check[i] == false)
                            end = false;
                    }
                    for (int i = 0; i < n; i++)
                    {
                        if (a_f_check[i] == false)
                            end = false;
                    }

                    if (end)
                    {
                        opt_stratnum[0] = Pi;
                        opt_stratnum[1] = Qi;
                        return true;
                    }
                }
            return false;
        }

        private void elementary_basis_transformation(List<List<double>> M, 
            List<List<double>> old_M, int r, int c)
        {
            for (int i = 0; i < old_M.Count; i++)
                for (int j = 0; j < old_M[0].Count; j++)
                    M[i][j] = old_M[i][j];

            double del = M[r][c];
            
            for (int i = 0; i < old_M.Count; i++)
                for (int j = 0; j < old_M[0].Count; j++)
                {
                    if (i == r)
                        M[i][j] /= del;
                    else
                        M[i][j] -= old_M[r][j] * old_M[i][c] / del;
                }
        }

        private void epsilon_calculation(List<List<double>> M, 
            List<double> epsilon, List<double> strategies_vector)
        {
            for (int i = 0; i<M[0].Count-M.Count; i++)
                for (int j = 0; j < M.Count; j++)
                {
                    epsilon[i] += M[j][i] * strategies_vector[j];
                }
        }

        private void lambda_calculation(List<List<double>> M, List<double> lambda,
            List<int> basis, List<double> epsilon,
            List<double> strategies_vector)
        {
            double comparement = 0;
            List<List<double>> min_vector = new List<List<double>>();
            List<List<double>> max_vector = new List<List<double>>();
            min_vector.Add(new List<double>());
            min_vector.Add(new List<double>());
            max_vector.Add(new List<double>());
            max_vector.Add(new List<double>());

            int k = M[0].Count - M.Count; //barrier position

            for (int i = 0; i < M.Count; i++)
            {
                min_vector[0].Clear();
                min_vector[1].Clear();
                max_vector[0].Clear();
                max_vector[1].Clear();
                for (int j = 0; j < k; j++)
                {
                    if (M[i][j] != 0)
                        comparement = (1 - epsilon[j]) / M[i][j];

                    if ((M[i][j] > 0) && (comparement != 0))
                    {
                        max_vector[0].Add(comparement);
                        max_vector[1].Add(j);
                    }

                    if ((M[i][j] < 0) && (comparement != 0))
                    {
                        min_vector[0].Add(comparement);
                        min_vector[1].Add(j);
                    }
                }

                for (int j = k; j < M[0].Count; j++)
                {
                    if (M[i][j] > 0)
                    {
                        max_vector[0].Add(-strategies_vector[j-k] / M[i][j]);
                        max_vector[1].Add(j);
                    }
                    if (M[i][j] < 0)
                    {
                        min_vector[0].Add(-strategies_vector[j-k] / M[i][j]);
                        min_vector[1].Add(j);
                    }

                }

                double min = 0;
                double max = 0;
                int max_column = 0;
                int min_column = 0;

                max = 0;
                if (min_vector[0].Count > 0)
                    min = min_vector[0][0];

                for (int j = 0; j < max_vector[0].Count; j++)
                {
                    if (max_vector[0][j] > max)
                    {
                        max = max_vector[0][j];
                        max_column = Convert.ToInt32(max_vector[1][j]);
                    }
                }
                for (int j = 0; j < min_vector[0].Count; j++)
                {
                    if (min_vector[0][j] <= min)
                    {
                        min = min_vector[0][j];
                        min_column = Convert.ToInt32(min_vector[1][j]);
                    }
                }

                if (max != 0)
                {
                    lambda[i] = max;
                    basis[i] = max_column;
                }
                else
                {
                    if (min != 0)
                    {
                        lambda[i] = min;
                        basis[i] = min_column;
                    }
                    else
                    {
                        lambda[i] = 0;
                        basis[i] = -1;
                    }
                }
            }
        }


        private void basis_substitution(List<int> basis,
            List<List<string>> symbolic_matrix, char symb1, char symb2)
        {
            for (int i = 0; i < basis.Count; i++)
            {
                if (basis[i] != -1)
                {
                    if (basis[i] >= basis.Count)
                        symbolic_matrix[i][i] = symb1 + 
                            (basis[i] - basis.Count).ToString();
                    else
                        symbolic_matrix[i][i] = symb2 + basis[i].ToString();
                }
            }
        }

        private void define_optimal_strategies()
        {
            double sum_x = 0,
            sum_y = 0;
            for (int i = 0; i < n; i++)
            {
                strat1.Add(Xo[i] + lambda_B[opt_stratnum[0]] * B1[opt_stratnum[0]][m + i]);
                sum_x += strat1[i];
            }
            for (int i = 0; i < m; i++)
            {
                strat2.Add(Yo[i] + lambda_A[opt_stratnum[1]] * A1[opt_stratnum[1]][n + i]);
                sum_y += strat2[i];
            }
            for (int i = 0; i < n; i++)
                strat1[i] /= sum_x;
            for (int i = 0; i < m; i++)
                strat2[i] /= sum_y;

            prize_1 = d - 1 / sum_y;
            prize_2 = d - 1 / sum_x;
        }

        public void salvation()
        {
            bool stable = false;

            init_all_lists();
            find_min_d();
            create_expanded_matrix(Ao, A, 0);
            create_expanded_matrix(Bo, B, 1);

            while (!stable)
            {
                CycleStuck++;
                if (CycleStuck > 10)
                    break;
                stable = false;

                define_strategy_vectors();
                define_symbolic_matrix(Q, col_n, row_n, 'e', 'a');
                define_symbolic_matrix(P, next_row_n, col_n, 'f', 'b');

                if (check_end())
                    stable = true;
                else
                {
                    elementary_basis_transformation(A1, Ao, row_n, col_n);
                    elementary_basis_transformation(B1, Bo, col_n, next_row_n);

                    epsilon_calculation(Ao, epsilon_A, Yo);
                    epsilon_calculation(Bo, epsilon_B, Xo);

                    lambda_calculation(A1, lambda_A, basis_A, epsilon_A, Yo);
                    lambda_calculation(B1, lambda_B, basis_B, epsilon_B, Xo);

                    basis_substitution(basis_A, Q, 'e', 'a');
                    basis_substitution(basis_B, P, 'f', 'b');

                    if (check_end())
                        stable = true;
                }
            }
            if (CycleStuck < 10)
                define_optimal_strategies();            
        }
    }
}