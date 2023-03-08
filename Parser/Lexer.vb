Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Public Class Lexer
    Implements IDisposable
    Private disposedValue As Boolean
    Private Property Usercode As StringBuilder
    Private Property Definitions As IDefinition
    Private Property Tokens As List(Of Token)

    Sub New(filename As String, definition As IDefinition)
        If (Not File.Exists(filename)) Then Throw New FileNotFoundException(filename)
        Me.Definitions = definition
        Me.Tokens = New List(Of Token)
        Me.Usercode = New StringBuilder
        Using fs As New FileStream(filename, FileMode.Open, FileAccess.Read)
            Using reader As New StreamReader(fs)
                Me.Usercode.Append(reader.ReadToEnd)
            End Using
        End Using
    End Sub

    Public Function Parse() As ReadOnlyCollection(Of Token)
        Static filter() As Types = {Types.T_CRLF, Types.T_SPACE, Types.T_COMMENT}
        If (Me.Usercode.Length = 0 Or Me.Definitions.Rules.Count = 0) Then Throw New Exception("no input")
        Dim i As Integer, match As Match, last As Types, flagged As Boolean
        Do
            flagged = False
            For Each rule As Rule In Me.Definitions.Rules
                match = rule.Regex.Match(Me.Usercode.ToString)
                If (match.Success) Then
                    flagged = True
                    If (filter.Contains(rule.Type)) Then
                        i = match.Length
                        Me.Usercode.Remove(0, i)
                        Exit For
                    End If
                    i = match.Length
                    Me.Tokens.Add(New Token(rule.Type, match, rule))
                    Me.Usercode.Remove(0, i)
                    last = rule.Type
                    Exit For
                End If
            Next
            If (Not flagged) Then
                Throw New Exception(String.Format("unable to parse syntax at '{0}'", Lexer.Truncate(Me.Usercode, 5)))
            End If
        Loop While (Me.Usercode.Length > 0)
        Me.Tokens.Add(New Token(Types.T_EOF))
        Return Me.Tokens.AsReadOnly
    End Function

    Private Shared Function Truncate(sb As StringBuilder, length As Integer) As String
        Return sb.ToString.Substring(0, Math.Min(sb.Length, length))
    End Function

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                Me.Usercode = Nothing
                Me.Definitions = Nothing
            End If
            disposedValue = True
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class
