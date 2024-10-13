namespace ip2country_desktop
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.btnUniqueIP = new System.Windows.Forms.ToolStripButton();
            this.btnUniqueCountries = new System.Windows.Forms.ToolStripButton();
            this.btnUniqueRequest = new System.Windows.Forms.ToolStripButton();
            this.btnGroupBy = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRemoveFilter = new System.Windows.Forms.ToolStripButton();
            this.dg = new System.Windows.Forms.DataGridView();
            this.ctx = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnNftables = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNftablesRange = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRemoveASN = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCheckOnlineMyIP = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCheckOnlineIP2location = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCheckOnlinedMaxmind = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnRemoveCountry = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.ctx.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPaste,
            this.btnUniqueIP,
            this.btnUniqueCountries,
            this.btnUniqueRequest,
            this.btnGroupBy,
            this.toolStripSeparator1,
            this.btnRemoveFilter});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(924, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnPaste
            // 
            this.btnPaste.Image = global::ip2country_desktop.Properties.Resources.paste32;
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(74, 36);
            this.btnPaste.Text = " paste";
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnUniqueIP
            // 
            this.btnUniqueIP.Image = global::ip2country_desktop.Properties.Resources.ip32;
            this.btnUniqueIP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUniqueIP.Name = "btnUniqueIP";
            this.btnUniqueIP.Size = new System.Drawing.Size(93, 36);
            this.btnUniqueIP.Text = "unique IP";
            this.btnUniqueIP.ToolTipText = "use CTRL to use IP Range";
            this.btnUniqueIP.Click += new System.EventHandler(this.btnUniqueIP_Click);
            // 
            // btnUniqueCountries
            // 
            this.btnUniqueCountries.Image = global::ip2country_desktop.Properties.Resources.country32;
            this.btnUniqueCountries.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUniqueCountries.Name = "btnUniqueCountries";
            this.btnUniqueCountries.Size = new System.Drawing.Size(124, 36);
            this.btnUniqueCountries.Text = "unique country";
            this.btnUniqueCountries.Click += new System.EventHandler(this.btnUniqueCountry_Click);
            // 
            // btnUniqueRequest
            // 
            this.btnUniqueRequest.Image = global::ip2country_desktop.Properties.Resources.request32;
            this.btnUniqueRequest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUniqueRequest.Name = "btnUniqueRequest";
            this.btnUniqueRequest.Size = new System.Drawing.Size(122, 36);
            this.btnUniqueRequest.Text = "unique request";
            this.btnUniqueRequest.Click += new System.EventHandler(this.btnUniqueRequest_Click);
            // 
            // btnGroupBy
            // 
            this.btnGroupBy.Image = global::ip2country_desktop.Properties.Resources.groupby32;
            this.btnGroupBy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGroupBy.Name = "btnGroupBy";
            this.btnGroupBy.Size = new System.Drawing.Size(100, 36);
            this.btnGroupBy.Text = "group by";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // btnRemoveFilter
            // 
            this.btnRemoveFilter.Enabled = false;
            this.btnRemoveFilter.Image = global::ip2country_desktop.Properties.Resources.removeFilter32;
            this.btnRemoveFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveFilter.Name = "btnRemoveFilter";
            this.btnRemoveFilter.Size = new System.Drawing.Size(110, 36);
            this.btnRemoveFilter.Text = "remove filter";
            this.btnRemoveFilter.Click += new System.EventHandler(this.btnRemoveFilter_Click);
            // 
            // dg
            // 
            this.dg.AllowDrop = true;
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.AllowUserToResizeRows = false;
            this.dg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dg.Location = new System.Drawing.Point(0, 39);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dg.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dg.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dg.ShowCellErrors = false;
            this.dg.ShowCellToolTips = false;
            this.dg.ShowEditingIcon = false;
            this.dg.ShowRowErrors = false;
            this.dg.Size = new System.Drawing.Size(924, 495);
            this.dg.TabIndex = 3;
            this.dg.DragDrop += new System.Windows.Forms.DragEventHandler(this.dg_DragDrop);
            this.dg.DragEnter += new System.Windows.Forms.DragEventHandler(this.dg_DragEnter);
            this.dg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dg_KeyDown);
            this.dg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dg_MouseUp);
            // 
            // ctx
            // 
            this.ctx.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNftables,
            this.btnNftablesRange,
            this.toolStripSeparator2,
            this.btnRemoveASN,
            this.btnRemoveCountry,
            this.toolStripSeparator3,
            this.btnCheckOnlineMyIP,
            this.btnCheckOnlineIP2location,
            this.btnCheckOnlinedMaxmind});
            this.ctx.Name = "ctx";
            this.ctx.Size = new System.Drawing.Size(260, 192);
            // 
            // btnNftables
            // 
            this.btnNftables.Name = "btnNftables";
            this.btnNftables.Size = new System.Drawing.Size(259, 22);
            this.btnNftables.Text = "copy nftables IP block rule(s)";
            this.btnNftables.Click += new System.EventHandler(this.btnNftables_Click);
            // 
            // btnNftablesRange
            // 
            this.btnNftablesRange.Name = "btnNftablesRange";
            this.btnNftablesRange.Size = new System.Drawing.Size(259, 22);
            this.btnNftablesRange.Text = "copy nftables IP range block rule(s)";
            this.btnNftablesRange.Click += new System.EventHandler(this.btnNftables_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(256, 6);
            // 
            // btnRemoveASN
            // 
            this.btnRemoveASN.Name = "btnRemoveASN";
            this.btnRemoveASN.Size = new System.Drawing.Size(259, 22);
            this.btnRemoveASN.Text = "remove";
            this.btnRemoveASN.Click += new System.EventHandler(this.btnRemoveASN_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(256, 6);
            // 
            // btnCheckOnlineMyIP
            // 
            this.btnCheckOnlineMyIP.Name = "btnCheckOnlineMyIP";
            this.btnCheckOnlineMyIP.Size = new System.Drawing.Size(259, 22);
            this.btnCheckOnlineMyIP.Text = "check online myip.ms";
            this.btnCheckOnlineMyIP.Click += new System.EventHandler(this.btnCheckOnline_Click);
            // 
            // btnCheckOnlineIP2location
            // 
            this.btnCheckOnlineIP2location.Name = "btnCheckOnlineIP2location";
            this.btnCheckOnlineIP2location.Size = new System.Drawing.Size(259, 22);
            this.btnCheckOnlineIP2location.Text = "check online ip2location";
            this.btnCheckOnlineIP2location.Click += new System.EventHandler(this.btnCheckOnline_Click);
            // 
            // btnCheckOnlinedMaxmind
            // 
            this.btnCheckOnlinedMaxmind.Name = "btnCheckOnlinedMaxmind";
            this.btnCheckOnlinedMaxmind.Size = new System.Drawing.Size(259, 22);
            this.btnCheckOnlinedMaxmind.Text = "check online maxmind";
            this.btnCheckOnlinedMaxmind.Click += new System.EventHandler(this.btnCheckOnline_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 534);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(924, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(478, 17);
            this.toolStripStatusLabel1.Text = "Application developed by PipisCrew - Database and Contents Copyright (c) MaxMind " +
    "Inc.";
            // 
            // btnRemoveCountry
            // 
            this.btnRemoveCountry.Name = "btnRemoveCountry";
            this.btnRemoveCountry.Size = new System.Drawing.Size(259, 22);
            this.btnRemoveCountry.Text = "remove";
            this.btnRemoveCountry.Click += new System.EventHandler(this.btnRemoveCountry_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 556);
            this.Controls.Add(this.dg);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.MinimumSize = new System.Drawing.Size(940, 595);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ip2country_desktop";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.ctx.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnPaste;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.ToolStripButton btnUniqueCountries;
        private System.Windows.Forms.ToolStripButton btnUniqueIP;
        private System.Windows.Forms.ToolStripButton btnUniqueRequest;
        private System.Windows.Forms.ContextMenuStrip ctx;
        private System.Windows.Forms.ToolStripMenuItem btnNftables;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnRemoveFilter;
        private System.Windows.Forms.ToolStripMenuItem btnNftablesRange;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem btnCheckOnlineMyIP;
        private System.Windows.Forms.ToolStripMenuItem btnCheckOnlineIP2location;
        private System.Windows.Forms.ToolStripMenuItem btnCheckOnlinedMaxmind;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripDropDownButton btnGroupBy;
        private System.Windows.Forms.ToolStripMenuItem btnRemoveASN;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem btnRemoveCountry;
    }
}

