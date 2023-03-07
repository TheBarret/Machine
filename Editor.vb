
Public Class Editor
    Public Property Processor As Processor

    Private Sub Editor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Processor = New Processor
    End Sub

    Private Sub Editor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

    End Sub

End Class
