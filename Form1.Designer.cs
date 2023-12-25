namespace wftest
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Encode = new Button();
            Decode = new Button();
            Open = new Button();
            textBox1 = new TextBox();
            richTextBox1 = new RichTextBox();
            openFileDialog1 = new OpenFileDialog();
            label1 = new Label();
            SuspendLayout();
            // 
            // Encode
            // 
            Encode.Location = new Point(234, 308);
            Encode.Name = "Encode";
            Encode.Size = new Size(118, 27);
            Encode.TabIndex = 0;
            Encode.Text = "부호화";
            Encode.UseVisualStyleBackColor = true;
            Encode.Click += btnEncode;
            // 
            // Decode
            // 
            Decode.Location = new Point(358, 308);
            Decode.Name = "Decode";
            Decode.Size = new Size(118, 27);
            Decode.TabIndex = 1;
            Decode.Text = "복호화";
            Decode.UseVisualStyleBackColor = true;
            Decode.Click += btnDecode;
            // 
            // Open
            // 
            Open.Location = new Point(358, 341);
            Open.Name = "Open";
            Open.Size = new Size(118, 27);
            Open.TabIndex = 2;
            Open.Text = "파일 열기";
            Open.UseVisualStyleBackColor = true;
            Open.Click += btnOpen;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(234, 341);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(118, 27);
            textBox1.TabIndex = 3;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(55, 68);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(479, 221);
            richTextBox1.TabIndex = 4;
            richTextBox1.Text = "";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "*.txt";
            // 
            // label1
            // 
            label1.BackColor = SystemColors.Window;
            label1.BorderStyle = BorderStyle.Fixed3D;
            label1.Location = new Point(540, 71);
            label1.Name = "label1";
            label1.Size = new Size(153, 218);
            label1.TabIndex = 5;
           // label1.Click += label1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(richTextBox1);
            Controls.Add(textBox1);
            Controls.Add(Open);
            Controls.Add(Decode);
            Controls.Add(Encode);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Encode;
        private Button Decode;
        private Button Open;
        private TextBox textBox1;
        private RichTextBox richTextBox1;
        private OpenFileDialog openFileDialog1;
        private Label label1;
    }
}