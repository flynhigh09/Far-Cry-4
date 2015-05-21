namespace Far_Cry_4
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Connection = new System.Windows.Forms.ToolStripDropDownButton();
            this.Connect = new System.Windows.Forms.ToolStripMenuItem();
            this.Game = new System.Windows.Forms.ToolStripStatusLabel();
            this.Attached = new System.Windows.Forms.ToolStripStatusLabel();
            this.IPBox = new System.Windows.Forms.TextBox();
            this.ScreenTimer = new System.Windows.Forms.Timer(this.components);
            this.CEX = new System.Windows.Forms.RadioButton();
            this.DEX = new System.Windows.Forms.RadioButton();
            this.XPMultiplier = new System.Windows.Forms.Button();
            this.ModBox = new System.Windows.Forms.GroupBox();
            this.SetAll = new System.Windows.Forms.Button();
            this.XPBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.AddressTimer = new System.Windows.Forms.Timer(this.components);
            this.SaveAll = new System.Windows.Forms.Button();
            this.LoadAll = new System.Windows.Forms.Button();
            this.Update = new System.Windows.Forms.NumericUpDown();
            this.InfHealth = new System.Windows.Forms.CheckBox();
            this.InfAmmoNoReload = new System.Windows.Forms.CheckBox();
            this.HaveBulletsonreload = new System.Windows.Forms.CheckBox();
            this.InfItemsmax = new System.Windows.Forms.CheckBox();
            this.MoneyNeverDecreases = new System.Windows.Forms.CheckBox();
            this.MaxMoneyOnGain = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.ModBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Update)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Connection,
            this.Game,
            this.Attached});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStrip1.Size = new System.Drawing.Size(113, 26);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Connection
            // 
            this.Connection.BackColor = System.Drawing.Color.Black;
            this.Connection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Connection.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Connect});
            this.Connection.ForeColor = System.Drawing.Color.DarkOrange;
            this.Connection.Image = ((System.Drawing.Image)(resources.GetObject("Connection.Image")));
            this.Connection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Connection.Name = "Connection";
            this.Connection.Size = new System.Drawing.Size(97, 24);
            this.Connection.Text = "Connection";
            // 
            // Connect
            // 
            this.Connect.BackColor = System.Drawing.Color.White;
            this.Connect.Enabled = false;
            this.Connect.ForeColor = System.Drawing.Color.DarkOrange;
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(152, 24);
            this.Connect.Text = "Connect";
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // Game
            // 
            this.Game.Name = "Game";
            this.Game.Size = new System.Drawing.Size(0, 21);
            // 
            // Attached
            // 
            this.Attached.Name = "Attached";
            this.Attached.Size = new System.Drawing.Size(0, 21);
            // 
            // IPBox
            // 
            this.IPBox.BackColor = System.Drawing.Color.Black;
            this.IPBox.Enabled = false;
            this.IPBox.ForeColor = System.Drawing.Color.DarkOrange;
            this.IPBox.Location = new System.Drawing.Point(0, 26);
            this.IPBox.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.IPBox.Name = "IPBox";
            this.IPBox.Size = new System.Drawing.Size(126, 29);
            this.IPBox.TabIndex = 1;
            this.IPBox.Text = "192.168.1.";
            this.IPBox.Visible = false;
            this.IPBox.TextChanged += new System.EventHandler(this.IPBox_TextChanged);
            // 
            // ScreenTimer
            // 
            this.ScreenTimer.Tick += new System.EventHandler(this.ScreenTimer_Tick);
            // 
            // CEX
            // 
            this.CEX.AutoSize = true;
            this.CEX.ForeColor = System.Drawing.Color.DarkOrange;
            this.CEX.Location = new System.Drawing.Point(0, 55);
            this.CEX.Margin = new System.Windows.Forms.Padding(4);
            this.CEX.Name = "CEX";
            this.CEX.Size = new System.Drawing.Size(55, 25);
            this.CEX.TabIndex = 2;
            this.CEX.Text = "CEX";
            this.CEX.UseVisualStyleBackColor = true;
            this.CEX.CheckedChanged += new System.EventHandler(this.CEX_CheckedChanged);
            // 
            // DEX
            // 
            this.DEX.AutoSize = true;
            this.DEX.ForeColor = System.Drawing.Color.DarkOrange;
            this.DEX.Location = new System.Drawing.Point(63, 55);
            this.DEX.Margin = new System.Windows.Forms.Padding(4);
            this.DEX.Name = "DEX";
            this.DEX.Size = new System.Drawing.Size(56, 25);
            this.DEX.TabIndex = 3;
            this.DEX.Text = "DEX";
            this.DEX.UseVisualStyleBackColor = true;
            this.DEX.CheckedChanged += new System.EventHandler(this.DEX_CheckedChanged);
            // 
            // XPMultiplier
            // 
            this.XPMultiplier.BackColor = System.Drawing.Color.Black;
            this.XPMultiplier.ForeColor = System.Drawing.Color.DarkOrange;
            this.XPMultiplier.Location = new System.Drawing.Point(248, 139);
            this.XPMultiplier.Margin = new System.Windows.Forms.Padding(4);
            this.XPMultiplier.Name = "XPMultiplier";
            this.XPMultiplier.Size = new System.Drawing.Size(231, 28);
            this.XPMultiplier.TabIndex = 9;
            this.XPMultiplier.Text = "XP Multiplier";
            this.XPMultiplier.UseVisualStyleBackColor = false;
            this.XPMultiplier.Click += new System.EventHandler(this.XPMultiplier_Click);
            // 
            // ModBox
            // 
            this.ModBox.BackColor = System.Drawing.Color.Teal;
            this.ModBox.Controls.Add(this.MaxMoneyOnGain);
            this.ModBox.Controls.Add(this.MoneyNeverDecreases);
            this.ModBox.Controls.Add(this.InfItemsmax);
            this.ModBox.Controls.Add(this.HaveBulletsonreload);
            this.ModBox.Controls.Add(this.InfAmmoNoReload);
            this.ModBox.Controls.Add(this.InfHealth);
            this.ModBox.Controls.Add(this.SetAll);
            this.ModBox.Controls.Add(this.XPBox);
            this.ModBox.Controls.Add(this.XPMultiplier);
            this.ModBox.Enabled = false;
            this.ModBox.ForeColor = System.Drawing.Color.DarkOrange;
            this.ModBox.Location = new System.Drawing.Point(0, 145);
            this.ModBox.Name = "ModBox";
            this.ModBox.Size = new System.Drawing.Size(634, 181);
            this.ModBox.TabIndex = 11;
            this.ModBox.TabStop = false;
            // 
            // SetAll
            // 
            this.SetAll.BackColor = System.Drawing.Color.Black;
            this.SetAll.ForeColor = System.Drawing.Color.DarkOrange;
            this.SetAll.Location = new System.Drawing.Point(36, 139);
            this.SetAll.Margin = new System.Windows.Forms.Padding(4);
            this.SetAll.Name = "SetAll";
            this.SetAll.Size = new System.Drawing.Size(165, 28);
            this.SetAll.TabIndex = 16;
            this.SetAll.Text = "Set All";
            this.SetAll.UseVisualStyleBackColor = false;
            this.SetAll.Visible = false;
            this.SetAll.Click += new System.EventHandler(this.SetAll_Click);
            // 
            // XPBox
            // 
            this.XPBox.Location = new System.Drawing.Point(503, 140);
            this.XPBox.Name = "XPBox";
            this.XPBox.Size = new System.Drawing.Size(50, 29);
            this.XPBox.TabIndex = 12;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::Far_Cry_4.Properties.Resources.ICON01;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(159, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(475, 129);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // AddressTimer
            // 
            this.AddressTimer.Tick += new System.EventHandler(this.AddressTimer_Tick);
            // 
            // SaveAll
            // 
            this.SaveAll.BackColor = System.Drawing.Color.Black;
            this.SaveAll.ForeColor = System.Drawing.Color.DarkOrange;
            this.SaveAll.Location = new System.Drawing.Point(0, 126);
            this.SaveAll.Margin = new System.Windows.Forms.Padding(4);
            this.SaveAll.Name = "SaveAll";
            this.SaveAll.Size = new System.Drawing.Size(106, 28);
            this.SaveAll.TabIndex = 17;
            this.SaveAll.Text = "Save All";
            this.SaveAll.UseVisualStyleBackColor = false;
            this.SaveAll.Click += new System.EventHandler(this.SaveAll_Click);
            // 
            // LoadAll
            // 
            this.LoadAll.BackColor = System.Drawing.Color.Black;
            this.LoadAll.ForeColor = System.Drawing.Color.DarkOrange;
            this.LoadAll.Location = new System.Drawing.Point(0, 90);
            this.LoadAll.Margin = new System.Windows.Forms.Padding(4);
            this.LoadAll.Name = "LoadAll";
            this.LoadAll.Size = new System.Drawing.Size(106, 28);
            this.LoadAll.TabIndex = 18;
            this.LoadAll.Text = "Load All";
            this.LoadAll.UseVisualStyleBackColor = false;
            this.LoadAll.Click += new System.EventHandler(this.LoadAll_Click);
            // 
            // Update
            // 
            this.Update.DecimalPlaces = 2;
            this.Update.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.Update.Location = new System.Drawing.Point(108, 110);
            this.Update.Name = "Update";
            this.Update.Size = new System.Drawing.Size(50, 29);
            this.Update.TabIndex = 27;
            this.Update.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Update.Value = new decimal(new int[] {
            102,
            0,
            0,
            131072});
            // 
            // InfHealth
            // 
            this.InfHealth.AutoSize = true;
            this.InfHealth.ForeColor = System.Drawing.Color.DarkOrange;
            this.InfHealth.Location = new System.Drawing.Point(38, 24);
            this.InfHealth.Name = "InfHealth";
            this.InfHealth.Size = new System.Drawing.Size(96, 25);
            this.InfHealth.TabIndex = 17;
            this.InfHealth.Text = "Inf Health";
            this.InfHealth.UseVisualStyleBackColor = true;
            this.InfHealth.CheckedChanged += new System.EventHandler(this.InfHealth_CheckedChanged);
            // 
            // InfAmmoNoReload
            // 
            this.InfAmmoNoReload.AutoSize = true;
            this.InfAmmoNoReload.Location = new System.Drawing.Point(38, 55);
            this.InfAmmoNoReload.Name = "InfAmmoNoReload";
            this.InfAmmoNoReload.Size = new System.Drawing.Size(163, 25);
            this.InfAmmoNoReload.TabIndex = 18;
            this.InfAmmoNoReload.Text = "InfAmmoNoReload";
            this.InfAmmoNoReload.UseVisualStyleBackColor = true;
            this.InfAmmoNoReload.CheckedChanged += new System.EventHandler(this.InfAmmoNoReload_CheckedChanged);
            // 
            // HaveBulletsonreload
            // 
            this.HaveBulletsonreload.AutoSize = true;
            this.HaveBulletsonreload.Location = new System.Drawing.Point(248, 55);
            this.HaveBulletsonreload.Name = "HaveBulletsonreload";
            this.HaveBulletsonreload.Size = new System.Drawing.Size(211, 25);
            this.HaveBulletsonreload.TabIndex = 19;
            this.HaveBulletsonreload.Text = "Have Bullets 999 onreload";
            this.HaveBulletsonreload.UseVisualStyleBackColor = true;
            this.HaveBulletsonreload.CheckedChanged += new System.EventHandler(this.HaveBulletsonreload_CheckedChanged);
            // 
            // InfItemsmax
            // 
            this.InfItemsmax.AutoSize = true;
            this.InfItemsmax.ForeColor = System.Drawing.Color.DarkOrange;
            this.InfItemsmax.Location = new System.Drawing.Point(248, 24);
            this.InfItemsmax.Name = "InfItemsmax";
            this.InfItemsmax.Size = new System.Drawing.Size(89, 25);
            this.InfItemsmax.TabIndex = 20;
            this.InfItemsmax.Text = "Inf Items";
            this.InfItemsmax.UseVisualStyleBackColor = true;
            this.InfItemsmax.CheckedChanged += new System.EventHandler(this.InfItemsmax_CheckedChanged);
            // 
            // MoneyNeverDecreases
            // 
            this.MoneyNeverDecreases.AutoSize = true;
            this.MoneyNeverDecreases.Location = new System.Drawing.Point(248, 94);
            this.MoneyNeverDecreases.Name = "MoneyNeverDecreases";
            this.MoneyNeverDecreases.Size = new System.Drawing.Size(197, 25);
            this.MoneyNeverDecreases.TabIndex = 21;
            this.MoneyNeverDecreases.Text = "Money Never Decreases";
            this.MoneyNeverDecreases.UseVisualStyleBackColor = true;
            this.MoneyNeverDecreases.CheckedChanged += new System.EventHandler(this.MoneyNeverDecreases_CheckedChanged);
            // 
            // MaxMoneyOnGain
            // 
            this.MaxMoneyOnGain.AutoSize = true;
            this.MaxMoneyOnGain.Location = new System.Drawing.Point(38, 94);
            this.MaxMoneyOnGain.Name = "MaxMoneyOnGain";
            this.MaxMoneyOnGain.Size = new System.Drawing.Size(171, 25);
            this.MaxMoneyOnGain.TabIndex = 22;
            this.MaxMoneyOnGain.Text = "Max Money On Gain";
            this.MaxMoneyOnGain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.MaxMoneyOnGain.UseVisualStyleBackColor = true;
            this.MaxMoneyOnGain.CheckedChanged += new System.EventHandler(this.MaxMoneyOnGain_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(634, 325);
            this.Controls.Add(this.Update);
            this.Controls.Add(this.LoadAll);
            this.Controls.Add(this.SaveAll);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ModBox);
            this.Controls.Add(this.DEX);
            this.Controls.Add(this.CEX);
            this.Controls.Add(this.IPBox);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Segoe WP", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Far Cry 4";
            this.Closed += new System.EventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ModBox.ResumeLayout(false);
            this.ModBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Update)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripDropDownButton Connection;
        private System.Windows.Forms.ToolStripMenuItem Connect;
        private System.Windows.Forms.ToolStripStatusLabel Game;
        private System.Windows.Forms.ToolStripStatusLabel Attached;
        private System.Windows.Forms.TextBox IPBox;
        private System.Windows.Forms.Timer ScreenTimer;
        private System.Windows.Forms.RadioButton CEX;
        private System.Windows.Forms.RadioButton DEX;
        private System.Windows.Forms.Button XPMultiplier;
        private System.Windows.Forms.GroupBox ModBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox XPBox;
        private System.Windows.Forms.Timer AddressTimer;
        private System.Windows.Forms.Button SetAll;
        private System.Windows.Forms.Button SaveAll;
        private System.Windows.Forms.Button LoadAll;
        private System.Windows.Forms.NumericUpDown Update;
        private System.Windows.Forms.CheckBox InfHealth;
        private System.Windows.Forms.CheckBox InfAmmoNoReload;
        private System.Windows.Forms.CheckBox InfItemsmax;
        private System.Windows.Forms.CheckBox HaveBulletsonreload;
        private System.Windows.Forms.CheckBox MaxMoneyOnGain;
        private System.Windows.Forms.CheckBox MoneyNeverDecreases;
    }
}

