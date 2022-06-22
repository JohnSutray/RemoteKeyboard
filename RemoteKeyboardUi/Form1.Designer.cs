namespace RemoteKeyboardUi {
  partial class Form1 {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }

      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.RoomIdTextbox = new System.Windows.Forms.TextBox();
      this.RoomIdLabel = new System.Windows.Forms.Label();
      this.ModeGroupBox = new System.Windows.Forms.GroupBox();
      this.SendModeRadioButton = new System.Windows.Forms.RadioButton();
      this.ReceiveModeRadioButton = new System.Windows.Forms.RadioButton();
      this.LogTextBox = new System.Windows.Forms.RichTextBox();
      this.StopButton = new System.Windows.Forms.Button();
      this.ModeGroupBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // RoomIdTextbox
      // 
      this.RoomIdTextbox.Location = new System.Drawing.Point(13, 38);
      this.RoomIdTextbox.Name = "RoomIdTextbox";
      this.RoomIdTextbox.Size = new System.Drawing.Size(213, 22);
      this.RoomIdTextbox.TabIndex = 0;
      // 
      // RoomIdLabel
      // 
      this.RoomIdLabel.Location = new System.Drawing.Point(12, 9);
      this.RoomIdLabel.Name = "RoomIdLabel";
      this.RoomIdLabel.Size = new System.Drawing.Size(214, 23);
      this.RoomIdLabel.TabIndex = 1;
      this.RoomIdLabel.Text = "Room ID";
      // 
      // ModeGroupBox
      // 
      this.ModeGroupBox.Controls.Add(this.SendModeRadioButton);
      this.ModeGroupBox.Controls.Add(this.ReceiveModeRadioButton);
      this.ModeGroupBox.Location = new System.Drawing.Point(13, 68);
      this.ModeGroupBox.Name = "ModeGroupBox";
      this.ModeGroupBox.Size = new System.Drawing.Size(119, 101);
      this.ModeGroupBox.TabIndex = 2;
      this.ModeGroupBox.TabStop = false;
      this.ModeGroupBox.Text = "Mode";
      // 
      // SendModeRadioButton
      // 
      this.SendModeRadioButton.Location = new System.Drawing.Point(12, 66);
      this.SendModeRadioButton.Name = "SendModeRadioButton";
      this.SendModeRadioButton.Size = new System.Drawing.Size(103, 29);
      this.SendModeRadioButton.TabIndex = 1;
      this.SendModeRadioButton.TabStop = true;
      this.SendModeRadioButton.Text = "Send";
      this.SendModeRadioButton.UseVisualStyleBackColor = true;
      this.SendModeRadioButton.CheckedChanged += new System.EventHandler(this.SendModeRadioButton_CheckedChanged);
      // 
      // ReceiveModeRadioButton
      // 
      this.ReceiveModeRadioButton.Location = new System.Drawing.Point(12, 18);
      this.ReceiveModeRadioButton.Name = "ReceiveModeRadioButton";
      this.ReceiveModeRadioButton.Size = new System.Drawing.Size(101, 28);
      this.ReceiveModeRadioButton.TabIndex = 0;
      this.ReceiveModeRadioButton.TabStop = true;
      this.ReceiveModeRadioButton.Text = "Receive";
      this.ReceiveModeRadioButton.UseVisualStyleBackColor = true;
      this.ReceiveModeRadioButton.CheckedChanged += new System.EventHandler(this.ReceiveModeRadioButton_CheckedChanged);
      // 
      // LogTextBox
      // 
      this.LogTextBox.Location = new System.Drawing.Point(237, 12);
      this.LogTextBox.Name = "LogTextBox";
      this.LogTextBox.ReadOnly = true;
      this.LogTextBox.Size = new System.Drawing.Size(274, 157);
      this.LogTextBox.TabIndex = 3;
      this.LogTextBox.Text = "";
      // 
      // StopButton
      // 
      this.StopButton.Location = new System.Drawing.Point(140, 77);
      this.StopButton.Name = "StopButton";
      this.StopButton.Size = new System.Drawing.Size(86, 92);
      this.StopButton.TabIndex = 4;
      this.StopButton.Text = "Stop";
      this.StopButton.UseVisualStyleBackColor = true;
      this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(523, 181);
      this.Controls.Add(this.StopButton);
      this.Controls.Add(this.LogTextBox);
      this.Controls.Add(this.ModeGroupBox);
      this.Controls.Add(this.RoomIdLabel);
      this.Controls.Add(this.RoomIdTextbox);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ModeGroupBox.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private System.Windows.Forms.Button StopButton;

    private System.Windows.Forms.RichTextBox LogTextBox;

    private System.Windows.Forms.GroupBox ModeGroupBox;
    private System.Windows.Forms.RadioButton ReceiveModeRadioButton;
    private System.Windows.Forms.RadioButton SendModeRadioButton;

    private System.Windows.Forms.Label RoomIdLabel;

    private System.Windows.Forms.TextBox RoomIdTextbox;

    #endregion
  }
}