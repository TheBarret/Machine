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
        Me.btnStart = New System.Windows.Forms.Button()
        Me.txtStatus = New System.Windows.Forms.Label()
        Me.DebugMonitor = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'UserCode
        '
        Me.UserCode.AcceptsTab = True
        Me.UserCode.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.UserCode.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UserCode.Location = New System.Drawing.Point(12, 12)
        Me.UserCode.Name = "UserCode"
        Me.UserCode.Size = New System.Drawing.Size(692, 627)
        Me.UserCode.TabIndex = 0
        Me.UserCode.Text = ""
        Me.UserCode.WordWrap = False
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(710, 603)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(260, 36)
        Me.btnStart.TabIndex = 1
        Me.btnStart.Text = "Run"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'txtStatus
        '
        Me.txtStatus.AutoSize = True
        Me.txtStatus.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.Location = New System.Drawing.Point(9, 642)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(49, 13)
        Me.txtStatus.TabIndex = 2
        Me.txtStatus.Text = "Status:"
        '
        'DebugMonitor
        '
        Me.DebugMonitor.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DebugMonitor.FormattingEnabled = True
        Me.DebugMonitor.Location = New System.Drawing.Point(710, 12)
        Me.DebugMonitor.Name = "DebugMonitor"
        Me.DebugMonitor.Size = New System.Drawing.Size(260, 576)
        Me.DebugMonitor.TabIndex = 3
        '
        'Editor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(982, 664)
        Me.Controls.Add(Me.DebugMonitor)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.UserCode)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Editor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Machine"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents UserCode As RichTextBox
    Friend WithEvents btnStart As Button
    Friend WithEvents txtStatus As Label
    Friend WithEvents DebugMonitor As ListBox
End Class
