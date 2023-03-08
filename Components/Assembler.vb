Imports System.Collections.ObjectModel
Imports System.IO

Public Class Assembler
    Implements IDisposable
    Public Enum Mode
        Scan = 0
        Write = 1
    End Enum
    Public Property Index As Integer
    Public Property Location As Integer
    Public Property Handler As Stream
    Public Property Token As Token
    Public Property Writer As BinaryWriter
    Public Property Tokens As ReadOnlyCollection(Of Token)
    Public Property Labels As Dictionary(Of String, UInt16)

    Sub New(output As String)
        Me.Labels = New Dictionary(Of String, UShort)
        Me.Handler = File.Open(output, FileMode.OpenOrCreate)
        Me.Writer = New BinaryWriter(Me.Handler)
    End Sub

    Public Sub Compile(input As String)
        Me.Tokens = Me.Tokenize(input)
        Me.Labels.Clear()
        Me.Parse(Mode.Scan)
        Me.Parse(Mode.Write)
        Me.Writer.Flush()
    End Sub

    Public Function Tokenize(filename As String) As ReadOnlyCollection(Of Token)
        Using lexer As New Lexer(filename, New Syntax)
            Return lexer.Parse
        End Using
    End Function

    Private Sub Parse(Mode As Mode)
        If (Me.Tokens.Count = 0) Then Return
        Me.Index = 0
        Me.Location = 0
        Me.Token = Me.Tokens.First
        Do
            Select Case Me.Token.Type

                Case Types.T_PUSH
                    Me.Validate(1, Types.T_VALUE)
                    If (Mode = Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()

                Case Types.T_LOAD
                    Me.Validate(1, Types.T_ADDRESS)
                    If (Mode = Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()

                Case Types.T_STORE
                    Me.Validate(1, Types.T_ADDRESS)
                    If (Mode = Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()

                Case Types.T_GOTO
                    Me.Validate(1, Types.T_ADDRESS)
                    If (Mode = Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()

                Case Types.T_END
                    If (Mode = Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()

                Case Types.T_PRINT
                    Me.Validate(1, Types.T_VALUE, Types.T_ADDRESS)
                    If (Mode = Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()

                Case Types.T_CALL
                    Me.Validate(1, Types.T_ADDRESS)
                    If (Mode = Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()

                Case Types.T_IF_EQUAL_JUMP

                Case Types.T_IF_NOT_EQUAL_JUMP
                Case Types.T_IF_GREATER_JUMP
                Case Types.T_IF_LESSER_JUMP

                Case Types.T_RESET_STACK,
                     Types.T_RESET_ADDRESS_STACK,
                     Types.T_BITWISE_XOR,
                     Types.T_BITWISE_OR,
                     Types.T_BITWISE_AND,
                     Types.T_MODULO_INT,
                     Types.T_MULTIPLICATION,
                     Types.T_DIVISION_INT,
                     Types.T_SUBTRACTION,
                     Types.T_ADDITION,
                     Types.T_STRING_LENGTH,
                     Types.T_RETURN,
                     Types.T_BITWISE_SHIFT_RIGHT,
                     Types.T_BITWISE_SHIFT_LEFT
                    If (Mode = Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)   '+1 (opcode)
                        Me.WriteUInt16(CUShort(0))      '+2 (padding)
                    End If
                    Me.Location += 3
                    Me.NextToken()

                Case Types.T_LABEL
                    If (Mode = Mode.Scan) Then
                        Me.SetLabel(Me.Token.Match.Value, CUShort(Me.Location))
                    End If
                    Me.NextToken()

                Case Types.T_VALUE
                    If (Mode = Mode.Write) Then
                        Me.WriteValue(Me.Token)
                    End If
                    Me.Location += 2
                    Me.NextToken()

                Case Types.T_CONSTANT
                    If (Mode = Mode.Write) Then
                        Me.WriteValue(Me.Token)
                    End If
                    Me.Location += 2
                    Me.NextToken()

                Case Types.T_ADDRESS
                    If (Mode = Mode.Write) Then
                        Me.WriteValue(Me.Token)
                    End If
                    Me.Location += 2
                    Me.NextToken()
            End Select
        Loop Until Me.Token.Type = Types.T_EOF
    End Sub

    Private Sub NextToken()
        Me.Index += 1
        Me.Token = Me.Tokens(Me.Index)
    End Sub

    Private Function Peek(Optional offset As Integer = 0) As Token
        If (Me.Index + offset) > Me.Tokens.Count Then Return New Token(Types.T_EOF)
        Return Me.Tokens(Me.Index + offset)
    End Function

    Private Sub Validate(offset As Integer, ParamArray Types() As Types)
        If (Types.Contains(Me.Peek(offset).Type)) Then Return
        Throw New Exception(String.Format("expecting '{0}' where '{1} {2}'", String.Join(" Or ", Types), Me.Peek.Match.Value, Me.Peek(1).Match.Value))
    End Sub

    Private Sub SetLabel(Line As String, Location As UInt16)
        Dim reference As String = Line.Substring(1).ToUpper
        If (Not Me.Labels.ContainsKey(reference)) Then
            Me.Labels.Add(reference, Location)
            Return
        End If
        Throw New Exception(String.Format("label '{0}' already exist", reference))
    End Sub

    Private Function GetLabel(Line As String) As UInt16
        Dim reference As String = Line.Substring(1, Line.Length - 2).ToUpper
        If (Me.Labels.ContainsKey(reference)) Then Return Me.Labels(reference)
        Throw New Exception(String.Format("label '{0}' does not exist", Line))
    End Function

    Private Sub WriteByte(value As Byte)
        Me.Writer.Write(value)
    End Sub

    Private Sub WriteUInt16(value As UInt16)
        Me.Writer.Write(value)
    End Sub

    Private Sub WriteValue(Token As Token)
        Select Case Token.Type
            Case Types.T_VALUE
                Me.WriteUInt16(UInt16.Parse(Token.Match.Value.Substring(1)))
            Case Types.T_ADDRESS
                Me.WriteUInt16(Me.GetLabel(Token.Match.Value))
            Case Types.T_CONSTANT
                Me.WriteUInt16(UInt16.Parse(Token.Match.Value))
            Case Else
                Throw New Exception(String.Format("undefined type '{0}'", Token.Type))
        End Select
    End Sub

#Region "IDisposable"
    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                Me.Writer.Flush()
                Me.Writer.Dispose()
                Me.Handler.Dispose()
            End If
            disposedValue = True
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
