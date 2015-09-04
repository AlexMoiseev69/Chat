namespace Chat
{
    partial class ChatForm
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
            this.ChatRichTextForm = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.MessageRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ChatRichTextForm
            // 
            this.ChatRichTextForm.Enabled = false;
            this.ChatRichTextForm.Location = new System.Drawing.Point(12, 12);
            this.ChatRichTextForm.Name = "ChatRichTextForm";
            this.ChatRichTextForm.Size = new System.Drawing.Size(567, 287);
            this.ChatRichTextForm.TabIndex = 0;
            this.ChatRichTextForm.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(240, 422);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MessageRichTextBox
            // 
            this.MessageRichTextBox.Location = new System.Drawing.Point(12, 334);
            this.MessageRichTextBox.Name = "MessageRichTextBox";
            this.MessageRichTextBox.Size = new System.Drawing.Size(567, 45);
            this.MessageRichTextBox.TabIndex = 2;
            this.MessageRichTextBox.Text = "";
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 469);
            this.Controls.Add(this.MessageRichTextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ChatRichTextForm);
            this.Name = "ChatForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Leave += new System.EventHandler(this.ChatForm_Leave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox ChatRichTextForm;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox MessageRichTextBox;
    }
}

