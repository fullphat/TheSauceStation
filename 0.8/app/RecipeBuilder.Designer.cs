namespace TheSauceStation
{
    partial class RecipeBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecipeBuilder));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbIngredients = new System.Windows.Forms.ListBox();
            this.cbProviders = new System.Windows.Forms.ComboBox();
            this.bnAddRecipe = new System.Windows.Forms.Button();
            this.cbTriggers = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.lblWarn = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Tan;
            this.label2.Location = new System.Drawing.Point(14, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 17);
            this.label2.TabIndex = 87;
            this.label2.Text = "Step 2: Choose a particular Sauce";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Tan;
            this.label1.Location = new System.Drawing.Point(14, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 17);
            this.label1.TabIndex = 86;
            this.label1.Text = "Step 1: Pick your Supplier";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Tan;
            this.label3.Location = new System.Drawing.Point(15, 137);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(189, 17);
            this.label3.TabIndex = 88;
            this.label3.Text = "Step 3: Customise Ingredients";
            // 
            // lbIngredients
            // 
            this.lbIngredients.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lbIngredients.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIngredients.ForeColor = System.Drawing.Color.SkyBlue;
            this.lbIngredients.FormattingEnabled = true;
            this.lbIngredients.ItemHeight = 17;
            this.lbIngredients.Location = new System.Drawing.Point(17, 160);
            this.lbIngredients.Margin = new System.Windows.Forms.Padding(4);
            this.lbIngredients.Name = "lbIngredients";
            this.lbIngredients.Size = new System.Drawing.Size(411, 106);
            this.lbIngredients.TabIndex = 85;
            this.lbIngredients.SelectedIndexChanged += new System.EventHandler(this.lbIngredients_SelectedIndexChanged);
            this.lbIngredients.DoubleClick += new System.EventHandler(this.lbIngredients_DoubleClick);
            // 
            // cbProviders
            // 
            this.cbProviders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.cbProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProviders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbProviders.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProviders.ForeColor = System.Drawing.Color.SkyBlue;
            this.cbProviders.FormattingEnabled = true;
            this.cbProviders.Location = new System.Drawing.Point(17, 35);
            this.cbProviders.Margin = new System.Windows.Forms.Padding(4);
            this.cbProviders.Name = "cbProviders";
            this.cbProviders.Size = new System.Drawing.Size(299, 25);
            this.cbProviders.Sorted = true;
            this.cbProviders.TabIndex = 84;
            this.cbProviders.SelectedIndexChanged += new System.EventHandler(this.cbProviders_SelectedIndexChanged);
            // 
            // bnAddRecipe
            // 
            this.bnAddRecipe.BackColor = System.Drawing.SystemColors.Control;
            this.bnAddRecipe.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGoldenrod;
            this.bnAddRecipe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnAddRecipe.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnAddRecipe.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bnAddRecipe.Location = new System.Drawing.Point(18, 344);
            this.bnAddRecipe.Margin = new System.Windows.Forms.Padding(4);
            this.bnAddRecipe.Name = "bnAddRecipe";
            this.bnAddRecipe.Size = new System.Drawing.Size(94, 37);
            this.bnAddRecipe.TabIndex = 90;
            this.bnAddRecipe.Text = "Add Recipe";
            this.bnAddRecipe.UseVisualStyleBackColor = false;
            this.bnAddRecipe.Click += new System.EventHandler(this.bnAddRecipe_Click);
            // 
            // cbTriggers
            // 
            this.cbTriggers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.cbTriggers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTriggers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbTriggers.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTriggers.ForeColor = System.Drawing.Color.SkyBlue;
            this.cbTriggers.FormattingEnabled = true;
            this.cbTriggers.Location = new System.Drawing.Point(17, 99);
            this.cbTriggers.Margin = new System.Windows.Forms.Padding(4);
            this.cbTriggers.Name = "cbTriggers";
            this.cbTriggers.Size = new System.Drawing.Size(299, 25);
            this.cbTriggers.TabIndex = 91;
            this.cbTriggers.SelectedIndexChanged += new System.EventHandler(this.cbTriggers_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Tan;
            this.label4.Location = new System.Drawing.Point(18, 272);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 2, 4, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(413, 33);
            this.label4.TabIndex = 92;
            this.label4.Text = "Some sauces let you to modify the ingredients you put in to them.  You can modify" +
                " an ingredient by double-clicking its entry in the list above.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Tan;
            this.label5.Location = new System.Drawing.Point(14, 321);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(238, 17);
            this.label5.TabIndex = 93;
            this.label5.Text = "You\'re done!  Click to add the recipe...";
            // 
            // pbIcon
            // 
            this.pbIcon.Location = new System.Drawing.Point(319, 17);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(128, 128);
            this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbIcon.TabIndex = 89;
            this.pbIcon.TabStop = false;
            this.pbIcon.Click += new System.EventHandler(this.pbIcon_Click);
            // 
            // lblWarn
            // 
            this.lblWarn.AutoSize = true;
            this.lblWarn.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarn.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblWarn.Location = new System.Drawing.Point(120, 354);
            this.lblWarn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWarn.Name = "lblWarn";
            this.lblWarn.Size = new System.Drawing.Size(228, 15);
            this.lblWarn.TabIndex = 94;
            this.lblWarn.Text = "@ needs authorising before it can be used";
            // 
            // RecipeBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(444, 397);
            this.Controls.Add(this.lblWarn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbTriggers);
            this.Controls.Add(this.bnAddRecipe);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbIngredients);
            this.Controls.Add(this.cbProviders);
            this.Controls.Add(this.pbIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecipeBuilder";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create New Recipe";
            this.Load += new System.EventHandler(this.RecipeBuilder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbIngredients;
        private System.Windows.Forms.ComboBox cbProviders;
        private System.Windows.Forms.PictureBox pbIcon;
        private System.Windows.Forms.Button bnAddRecipe;
        private System.Windows.Forms.ComboBox cbTriggers;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblWarn;
    }
}