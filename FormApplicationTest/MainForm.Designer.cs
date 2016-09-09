namespace FormApplicationTest
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
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
            this.btnStart = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnLoadOsbUrls = new System.Windows.Forms.Button();
            this.buttonCustomSearch = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.dgViewLOG = new System.Windows.Forms.DataGridView();
            this.dgViewResult = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button11 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewLOG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewResult)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(13, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(190, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(190, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(13, 113);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(192, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "OSB Url Ekle";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnLoadOsbUrls
            // 
            this.btnLoadOsbUrls.Location = new System.Drawing.Point(13, 142);
            this.btnLoadOsbUrls.Name = "btnLoadOsbUrls";
            this.btnLoadOsbUrls.Size = new System.Drawing.Size(192, 23);
            this.btnLoadOsbUrls.TabIndex = 3;
            this.btnLoadOsbUrls.Text = "OSB Url Görüntüle";
            this.btnLoadOsbUrls.UseVisualStyleBackColor = true;
            this.btnLoadOsbUrls.Click += new System.EventHandler(this.btnLoadOsbUrls_Click);
            // 
            // buttonCustomSearch
            // 
            this.buttonCustomSearch.Location = new System.Drawing.Point(13, 171);
            this.buttonCustomSearch.Name = "buttonCustomSearch";
            this.buttonCustomSearch.Size = new System.Drawing.Size(192, 23);
            this.buttonCustomSearch.TabIndex = 3;
            this.buttonCustomSearch.Text = "Özel Tarama";
            this.buttonCustomSearch.UseVisualStyleBackColor = true;
            this.buttonCustomSearch.Click += new System.EventHandler(this.buttonCustomSearch_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(13, 200);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(192, 23);
            this.button6.TabIndex = 3;
            this.button6.Text = "Eşlenen Firmalar";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(13, 229);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(192, 23);
            this.button7.TabIndex = 3;
            this.button7.Text = "Eşleşmeyen Firmalar";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(12, 258);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(193, 23);
            this.button8.TabIndex = 3;
            this.button8.Text = "DB\'deki Eşlenen Firmalar ";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(13, 287);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(192, 23);
            this.button9.TabIndex = 3;
            this.button9.Text = "DB\'deki Eşleşmeyen Firmalar";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(13, 345);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(192, 23);
            this.button10.TabIndex = 3;
            this.button10.Text = "Clear Gridviews";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // dgViewLOG
            // 
            this.dgViewLOG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewLOG.Location = new System.Drawing.Point(11, 374);
            this.dgViewLOG.Name = "dgViewLOG";
            this.dgViewLOG.Size = new System.Drawing.Size(546, 216);
            this.dgViewLOG.TabIndex = 4;
            // 
            // dgViewResult
            // 
            this.dgViewResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewResult.Location = new System.Drawing.Point(563, 374);
            this.dgViewResult.Name = "dgViewResult";
            this.dgViewResult.Size = new System.Drawing.Size(569, 216);
            this.dgViewResult.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(211, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(921, 356);
            this.panel1.TabIndex = 6;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(13, 316);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(192, 23);
            this.button11.TabIndex = 3;
            this.button11.Text = "Gridviews Save As Excel";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 595);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgViewResult);
            this.Controls.Add(this.dgViewLOG);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.buttonCustomSearch);
            this.Controls.Add(this.btnLoadOsbUrls);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(1160, 629);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgViewLOG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnLoadOsbUrls;
        private System.Windows.Forms.Button buttonCustomSearch;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.DataGridView dgViewLOG;
        private System.Windows.Forms.DataGridView dgViewResult;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button11;
    }
}