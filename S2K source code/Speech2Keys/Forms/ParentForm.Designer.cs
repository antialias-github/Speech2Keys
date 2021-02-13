/*
 * Created by SharpDevelop.
 * User: Peter
 * Date: 24.12.2014
 * Time: 12:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
namespace Speech2Keys
{
	partial class ParentForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button stopButton;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox commandsListBox;
		private System.Windows.Forms.TextBox logTextBox;
		private System.Windows.Forms.Button addCommandButton;
		private System.Windows.Forms.Label label2;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.stopButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.commandsListBox = new System.Windows.Forms.ListBox();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.addCommandButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.deleteCommandButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // stopButton
            // 
            this.stopButton.ForeColor = System.Drawing.Color.Red;
            this.stopButton.Location = new System.Drawing.Point(156, 283);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(107, 41);
            this.stopButton.TabIndex = 12;
            this.stopButton.Text = "Stop Voice Recognition";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButtonClick);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.SystemColors.Control;
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.startButton.Location = new System.Drawing.Point(39, 283);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(102, 41);
            this.startButton.TabIndex = 11;
            this.startButton.Text = "Start Voice Recognition";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(337, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(348, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Click command to see details.\r\nDouble click command to edit.\r\n";
            // 
            // commandsListBox
            // 
            this.commandsListBox.FormattingEnabled = true;
            this.commandsListBox.Location = new System.Drawing.Point(337, 37);
            this.commandsListBox.Name = "commandsListBox";
            this.commandsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.commandsListBox.Size = new System.Drawing.Size(348, 498);
            this.commandsListBox.TabIndex = 8;
            this.commandsListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.commandsListBox_KeyPress);
            // 
            // logTextBox
            // 
            this.logTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.logTextBox.Location = new System.Drawing.Point(703, 37);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new System.Drawing.Size(269, 498);
            this.logTextBox.TabIndex = 13;
            this.logTextBox.TextChanged += new System.EventHandler(this.logTextBox_TextChanged);
            // 
            // addCommandButton
            // 
            this.addCommandButton.Location = new System.Drawing.Point(39, 102);
            this.addCommandButton.Name = "addCommandButton";
            this.addCommandButton.Size = new System.Drawing.Size(224, 58);
            this.addCommandButton.TabIndex = 14;
            this.addCommandButton.Text = "Add New Command \r\n(or hit Enter)";
            this.addCommandButton.UseVisualStyleBackColor = true;
            this.addCommandButton.Click += new System.EventHandler(this.AddCommandButtonClick);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(36, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 81);
            this.label2.TabIndex = 15;
            this.label2.Text = "Changes made while voice recognition is running will only take effect after you r" +
    "estart voice recognition\r\n(Hit \"Stop\" then \"Start\" button)";
            // 
            // deleteCommandButton
            // 
            this.deleteCommandButton.Location = new System.Drawing.Point(156, 496);
            this.deleteCommandButton.Name = "deleteCommandButton";
            this.deleteCommandButton.Size = new System.Drawing.Size(107, 37);
            this.deleteCommandButton.TabIndex = 16;
            this.deleteCommandButton.Text = "Delete Selected Command(s)";
            this.deleteCommandButton.UseVisualStyleBackColor = true;
            this.deleteCommandButton.Click += new System.EventHandler(this.deleteCommandButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(703, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Information";
            // 
            // ParentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 547);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.deleteCommandButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.addCommandButton);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.commandsListBox);
            this.Name = "ParentForm";
            this.Text = "ParentForm";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private System.Windows.Forms.Button deleteCommandButton;
        private System.Windows.Forms.Label label3;
    }
}
