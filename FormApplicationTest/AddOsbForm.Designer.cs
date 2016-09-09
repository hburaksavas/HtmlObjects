namespace FormApplicationTest
{
    partial class AddOsbForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.txtTAG = new System.Windows.Forms.TextBox();
            this.btnEkle = new System.Windows.Forms.Button();
            this.btnIPTAL = new System.Windows.Forms.Button();
            this.btnKAPAT = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Web Adresi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(12, 196);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(255, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Firma İsmini Kapsayan Html Tagı";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtURL
            // 
            this.txtURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtURL.Location = new System.Drawing.Point(15, 129);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(383, 24);
            this.txtURL.TabIndex = 2;
            // 
            // txtTAG
            // 
            this.txtTAG.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtTAG.Location = new System.Drawing.Point(15, 217);
            this.txtTAG.Name = "txtTAG";
            this.txtTAG.Size = new System.Drawing.Size(383, 24);
            this.txtTAG.TabIndex = 3;
            // 
            // btnEkle
            // 
            this.btnEkle.Location = new System.Drawing.Point(323, 291);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(75, 37);
            this.btnEkle.TabIndex = 4;
            this.btnEkle.Text = "EKLE";
            this.btnEkle.UseVisualStyleBackColor = true;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // btnIPTAL
            // 
            this.btnIPTAL.Location = new System.Drawing.Point(242, 291);
            this.btnIPTAL.Name = "btnIPTAL";
            this.btnIPTAL.Size = new System.Drawing.Size(75, 37);
            this.btnIPTAL.TabIndex = 5;
            this.btnIPTAL.Text = "İPTAL";
            this.btnIPTAL.UseVisualStyleBackColor = true;
            this.btnIPTAL.Click += new System.EventHandler(this.btnIPTAL_Click);
            // 
            // btnKAPAT
            // 
            this.btnKAPAT.Location = new System.Drawing.Point(373, 12);
            this.btnKAPAT.Name = "btnKAPAT";
            this.btnKAPAT.Size = new System.Drawing.Size(36, 23);
            this.btnKAPAT.TabIndex = 6;
            this.btnKAPAT.Text = "X";
            this.btnKAPAT.UseVisualStyleBackColor = true;
            this.btnKAPAT.Click += new System.EventHandler(this.btnKAPAT_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(11, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(380, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "Organizasyon Sanayi Bölgesi Web Adresi Ekleme";
            // 
            // AddOsbForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 356);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnKAPAT);
            this.Controls.Add(this.btnIPTAL);
            this.Controls.Add(this.btnEkle);
            this.Controls.Add(this.txtTAG);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddOsbForm";
            this.Text = "AddOsbForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.TextBox txtTAG;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Button btnIPTAL;
        private System.Windows.Forms.Button btnKAPAT;
        private System.Windows.Forms.Label label3;
    }
}