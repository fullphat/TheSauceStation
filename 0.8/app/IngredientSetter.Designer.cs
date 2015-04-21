namespace TheSauceStation
{
    partial class IngredientSetter
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
            this.cbLookup = new System.Windows.Forms.ComboBox();
            this.bnLookup = new System.Windows.Forms.Button();
            this.lblHint = new System.Windows.Forms.Label();
            this.bnSetValue = new System.Windows.Forms.Button();
            this.tbRIValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbValue = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbLookup
            // 
            this.cbLookup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.cbLookup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbLookup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLookup.ForeColor = System.Drawing.Color.SkyBlue;
            this.cbLookup.FormattingEnabled = true;
            this.cbLookup.Location = new System.Drawing.Point(17, 37);
            this.cbLookup.Margin = new System.Windows.Forms.Padding(4);
            this.cbLookup.Name = "cbLookup";
            this.cbLookup.Size = new System.Drawing.Size(310, 25);
            this.cbLookup.Sorted = true;
            this.cbLookup.TabIndex = 98;
            this.cbLookup.Visible = false;
            // 
            // bnLookup
            // 
            this.bnLookup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.bnLookup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGoldenrod;
            this.bnLookup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnLookup.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnLookup.ForeColor = System.Drawing.SystemColors.Control;
            this.bnLookup.Location = new System.Drawing.Point(329, 37);
            this.bnLookup.Margin = new System.Windows.Forms.Padding(4);
            this.bnLookup.Name = "bnLookup";
            this.bnLookup.Size = new System.Drawing.Size(28, 25);
            this.bnLookup.TabIndex = 97;
            this.bnLookup.Text = "...";
            this.bnLookup.UseVisualStyleBackColor = false;
            this.bnLookup.Visible = false;
            this.bnLookup.Click += new System.EventHandler(this.bnLookup_Click);
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.BackColor = System.Drawing.Color.Transparent;
            this.lblHint.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHint.ForeColor = System.Drawing.Color.Tan;
            this.lblHint.Location = new System.Drawing.Point(14, 10);
            this.lblHint.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(115, 17);
            this.lblHint.TabIndex = 96;
            this.lblHint.Text = "{some ingredient}";
            // 
            // bnSetValue
            // 
            this.bnSetValue.BackColor = System.Drawing.SystemColors.Control;
            this.bnSetValue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGoldenrod;
            this.bnSetValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnSetValue.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnSetValue.Location = new System.Drawing.Point(139, 147);
            this.bnSetValue.Margin = new System.Windows.Forms.Padding(4);
            this.bnSetValue.Name = "bnSetValue";
            this.bnSetValue.Size = new System.Drawing.Size(94, 37);
            this.bnSetValue.TabIndex = 94;
            this.bnSetValue.Text = "Set";
            this.bnSetValue.UseVisualStyleBackColor = false;
            this.bnSetValue.Click += new System.EventHandler(this.bnSetValue_Click);
            // 
            // tbRIValue
            // 
            this.tbRIValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.tbRIValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRIValue.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRIValue.ForeColor = System.Drawing.Color.SkyBlue;
            this.tbRIValue.HideSelection = false;
            this.tbRIValue.Location = new System.Drawing.Point(17, 37);
            this.tbRIValue.Margin = new System.Windows.Forms.Padding(4);
            this.tbRIValue.Name = "tbRIValue";
            this.tbRIValue.Size = new System.Drawing.Size(340, 25);
            this.tbRIValue.TabIndex = 92;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SkyBlue;
            this.label1.Image = global::TheSauceStation.Properties.Resources.ingredient3;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.label1.Location = new System.Drawing.Point(14, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(360, 132);
            this.label1.TabIndex = 100;
            this.label1.Text = "label1";
            // 
            // cbValue
            // 
            this.cbValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbValue.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbValue.ForeColor = System.Drawing.Color.SkyBlue;
            this.cbValue.Location = new System.Drawing.Point(17, 37);
            this.cbValue.Name = "cbValue";
            this.cbValue.Size = new System.Drawing.Size(340, 25);
            this.cbValue.TabIndex = 101;
            this.cbValue.Text = "cbValue";
            this.cbValue.UseVisualStyleBackColor = true;
            // 
            // IngredientSetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(372, 195);
            this.Controls.Add(this.cbValue);
            this.Controls.Add(this.bnLookup);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.bnSetValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbLookup);
            this.Controls.Add(this.tbRIValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IngredientSetter";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IngredientSetter";
            this.Load += new System.EventHandler(this.IngredientSetter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLookup;
        private System.Windows.Forms.Button bnLookup;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Button bnSetValue;
        private System.Windows.Forms.TextBox tbRIValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbValue;
    }
}