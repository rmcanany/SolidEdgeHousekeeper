<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSplash
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        PictureBox1 = New PictureBox()
        TextBoxStatus = New TextBox()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = My.Resources.Resources.splash_logo_01
        PictureBox1.InitialImage = Nothing
        PictureBox1.Location = New Point(2, 2)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(396, 146)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 0
        PictureBox1.TabStop = False
        ' 
        ' TextBoxStatus
        ' 
        TextBoxStatus.BorderStyle = BorderStyle.None
        TextBoxStatus.Location = New Point(3, 148)
        TextBoxStatus.Name = "TextBoxStatus"
        TextBoxStatus.Size = New Size(393, 16)
        TextBoxStatus.TabIndex = 1
        ' 
        ' FormSplash
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ActiveCaption
        ClientSize = New Size(400, 167)
        Controls.Add(TextBoxStatus)
        Controls.Add(PictureBox1)
        FormBorderStyle = FormBorderStyle.None
        Name = "FormSplash"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Splash"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents TextBoxStatus As TextBox
End Class
