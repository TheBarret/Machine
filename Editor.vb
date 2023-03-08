
Imports System.IO

Public Class Editor
    Const INPUT As String = ".\usercode.txt"
    Public Property Processor As Processor

    Private Sub Editor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Processor = New Processor
        AddHandler Me.Processor.Debugger, AddressOf Me.EventDebugMessage
        Me.Loadfile()
    End Sub

    Private Sub Editor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Savefile()
    End Sub

    Private Sub Editor_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (e.Control And e.KeyCode = Keys.S) Then Me.Savefile()
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Try
            Static output As String = ".\program.bin"
            Me.DebugMonitor.Items.Clear()
            Me.Savefile()
            Using asm As New Assembler(output)
                asm.Compile(Editor.INPUT)
            End Using
            Me.txtStatus.Text = "Status: Success!"
            Me.Processor.Load(File.ReadAllBytes(output))
            Me.Processor.Run()
        Catch ex As Exception
            Me.txtStatus.Text = String.Format("Status: {0}", ex.Message)
        End Try
    End Sub

    Private Sub EventDebugMessage(message As String)
        Me.AppendMonitor(message)
    End Sub

    Private Sub Loadfile()
        If (File.Exists(Editor.INPUT)) Then
            Me.UserCode.LoadFile(Editor.INPUT, RichTextBoxStreamType.PlainText)
        End If
    End Sub

    Private Sub Savefile()
        Me.UserCode.SaveFile(Editor.INPUT, RichTextBoxStreamType.PlainText)
    End Sub

    Private Sub AppendMonitor(message As String)
        If (Me.InvokeRequired) Then
            Me.Invoke(Sub() Me.AppendMonitor(message))
        Else
            Me.DebugMonitor.Items.Add(message)
            Me.DebugMonitor.SelectedIndex = Me.DebugMonitor.Items.Count - 1
        End If
    End Sub

End Class
