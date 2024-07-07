namespace Lab_Four_WinAdd
{
    partial class GraphicsForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelGraphic = new System.Windows.Forms.Panel();
            this.TabControlFunction = new System.Windows.Forms.TabControl();
            this.PageOne = new System.Windows.Forms.TabPage();
            this.PageTwo = new System.Windows.Forms.TabPage();
            this.PageThree = new System.Windows.Forms.TabPage();
            this.TabControlFunction.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGraphic
            // 
            this.panelGraphic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGraphic.Location = new System.Drawing.Point(0, 0);
            this.panelGraphic.Name = "panelGraphic";
            this.panelGraphic.Size = new System.Drawing.Size(800, 450);
            this.panelGraphic.TabIndex = 0;
            // 
            // TabControlFunction
            // 
            this.TabControlFunction.Controls.Add(this.PageOne);
            this.TabControlFunction.Controls.Add(this.PageTwo);
            this.TabControlFunction.Controls.Add(this.PageThree);
            this.TabControlFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControlFunction.Location = new System.Drawing.Point(0, 0);
            this.TabControlFunction.Name = "TabControlFunction";
            this.TabControlFunction.SelectedIndex = 0;
            this.TabControlFunction.Size = new System.Drawing.Size(800, 450);
            this.TabControlFunction.TabIndex = 1;
            // 
            // PageOne
            // 
            this.PageOne.Location = new System.Drawing.Point(4, 25);
            this.PageOne.Name = "PageOne";
            this.PageOne.Padding = new System.Windows.Forms.Padding(3);
            this.PageOne.Size = new System.Drawing.Size(792, 421);
            this.PageOne.TabIndex = 0;
            this.PageOne.Text = "Functuin One";
            this.PageOne.UseVisualStyleBackColor = true;
            // 
            // PageTwo
            // 
            this.PageTwo.Location = new System.Drawing.Point(4, 25);
            this.PageTwo.Name = "PageTwo";
            this.PageTwo.Padding = new System.Windows.Forms.Padding(3);
            this.PageTwo.Size = new System.Drawing.Size(792, 421);
            this.PageTwo.TabIndex = 1;
            this.PageTwo.Text = "Function Two";
            this.PageTwo.UseVisualStyleBackColor = true;
            // 
            // PageThree
            // 
            this.PageThree.Location = new System.Drawing.Point(4, 25);
            this.PageThree.Name = "PageThree";
            this.PageThree.Size = new System.Drawing.Size(792, 421);
            this.PageThree.TabIndex = 2;
            this.PageThree.Text = "Function Three";
            this.PageThree.UseVisualStyleBackColor = true;
            // 
            // GraphicsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TabControlFunction);
            this.Controls.Add(this.panelGraphic);
            this.Name = "GraphicsForm";
            this.Text = "Graphics";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.TabControlFunction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelGraphic;
        private System.Windows.Forms.TabControl TabControlFunction;
        private System.Windows.Forms.TabPage PageOne;
        private System.Windows.Forms.TabPage PageTwo;
        private System.Windows.Forms.TabPage PageThree;
    }
}

