namespace SequentialGames
{
    partial class FunctionEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Function_TB = new System.Windows.Forms.TextBox();
            this.EvaluationButton = new System.Windows.Forms.Button();
            this.BoxOfParameters = new System.Windows.Forms.GroupBox();
            this.CalculateIt = new System.Windows.Forms.Button();
            this.ResultTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SaveIt = new System.Windows.Forms.Button();
            this.BoxOfParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // Function_TB
            // 
            this.Function_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Function_TB.Location = new System.Drawing.Point(34, 26);
            this.Function_TB.Name = "Function_TB";
            this.Function_TB.Size = new System.Drawing.Size(342, 31);
            this.Function_TB.TabIndex = 0;
            // 
            // EvaluationButton
            // 
            this.EvaluationButton.BackColor = System.Drawing.Color.Gainsboro;
            this.EvaluationButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.EvaluationButton.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EvaluationButton.ForeColor = System.Drawing.Color.Black;
            this.EvaluationButton.Location = new System.Drawing.Point(410, 23);
            this.EvaluationButton.Name = "EvaluationButton";
            this.EvaluationButton.Size = new System.Drawing.Size(120, 39);
            this.EvaluationButton.TabIndex = 1;
            this.EvaluationButton.Text = "Evaluate";
            this.EvaluationButton.UseVisualStyleBackColor = false;
            this.EvaluationButton.Click += new System.EventHandler(this.EvaluationButton_Click);
            // 
            // BoxOfParameters
            // 
            this.BoxOfParameters.Controls.Add(this.CalculateIt);
            this.BoxOfParameters.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxOfParameters.Location = new System.Drawing.Point(34, 80);
            this.BoxOfParameters.Name = "BoxOfParameters";
            this.BoxOfParameters.Size = new System.Drawing.Size(259, 169);
            this.BoxOfParameters.TabIndex = 2;
            this.BoxOfParameters.TabStop = false;
            this.BoxOfParameters.Text = "Testing parameters";
            this.BoxOfParameters.Visible = false;
            // 
            // CalculateIt
            // 
            this.CalculateIt.BackColor = System.Drawing.Color.Gainsboro;
            this.CalculateIt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CalculateIt.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CalculateIt.Location = new System.Drawing.Point(133, 124);
            this.CalculateIt.Name = "CalculateIt";
            this.CalculateIt.Size = new System.Drawing.Size(120, 39);
            this.CalculateIt.TabIndex = 2;
            this.CalculateIt.Text = "Calculate";
            this.CalculateIt.UseVisualStyleBackColor = false;
            this.CalculateIt.Click += new System.EventHandler(this.CalculateIt_Click);
            // 
            // ResultTB
            // 
            this.ResultTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResultTB.Location = new System.Drawing.Point(454, 142);
            this.ResultTB.Name = "ResultTB";
            this.ResultTB.Size = new System.Drawing.Size(76, 31);
            this.ResultTB.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(313, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Test value =";
            // 
            // SaveIt
            // 
            this.SaveIt.BackColor = System.Drawing.Color.Gainsboro;
            this.SaveIt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveIt.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveIt.Location = new System.Drawing.Point(398, 210);
            this.SaveIt.Name = "SaveIt";
            this.SaveIt.Size = new System.Drawing.Size(152, 39);
            this.SaveIt.TabIndex = 5;
            this.SaveIt.Text = "Save function";
            this.SaveIt.UseVisualStyleBackColor = false;
            this.SaveIt.Click += new System.EventHandler(this.SaveIt_Click);
            // 
            // FunctionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(562, 261);
            this.Controls.Add(this.SaveIt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ResultTB);
            this.Controls.Add(this.BoxOfParameters);
            this.Controls.Add(this.EvaluationButton);
            this.Controls.Add(this.Function_TB);
            this.Name = "FunctionEditor";
            this.Text = "FunctionEditor";
            this.BoxOfParameters.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Function_TB;
        private System.Windows.Forms.Button EvaluationButton;
        private System.Windows.Forms.GroupBox BoxOfParameters;
        private System.Windows.Forms.Button CalculateIt;
        private System.Windows.Forms.TextBox ResultTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SaveIt;
    }
}