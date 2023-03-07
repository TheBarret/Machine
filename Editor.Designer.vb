<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Editor
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.UserCode = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'UserCode
        '
        Me.UserCode.AcceptsTab = True
        Me.UserCode.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.UserCode.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UserCode.Location = New System.Drawing.Point(12, 303)
        Me.UserCode.Name = "UserCode"
        Me.UserCode.Size = New System.Drawing.Size(692, 348)
        Me.UserCode.TabIndex = 0
        Me.UserCode.Text = ""
        Me.UserCode.WordWrap = False
        '
        'Editor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(716, 663)
        Me.Controls.Add(Me.UserCode)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Editor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Machine"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents UserCode As RichTextBox
End Class
