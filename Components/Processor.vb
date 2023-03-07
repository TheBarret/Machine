Public Class Processor
    Public Property Pc As UShort
    Public Property Memory As Memory

    Public Sub New()
        Me.Pc = 0
        Me.Memory = New Memory(UInt16.MaxValue)
    End Sub

    Public Sub Load(program() As Byte)
        Me.Memory.Load(program)
    End Sub

    Public Function Execute() As Integer
        Try
            Dim instruction As Instruction = Me.GetInstruction
            Select Case instruction.Opcode
                Case 0 : Me.BasicOperations(instruction)
                Case 1 : Me.JumpOperations(instruction)
                Case 2 : Me.ArithmeticOperations(instruction)
                Case 3 : Me.BitwiseOperations(instruction)
                Case 4 : Me.StringOperations(instruction)
                Case Else : Throw New Exception("undefined instruction")
            End Select
        Catch ex As Exception
            Debugger.Break()
            Return -1 '// return -1 meaning it had errors
        End Try
        Return 0 '//return 0, meaning it had no errors
    End Function

    Private Sub BasicOperations(instruction As Instruction)
        Select Case instruction.Selection
            Case 0 'nop
            Case 1 'value
                Me.Memory.PushStack(instruction.Parameter)
            Case 2 'addr
                Me.Memory.PushAddr(instruction.Parameter)
            Case 3 'read
                Me.Memory.PushStack(Me.Memory.ReadUInt16(Me.Memory.PopAddr))
            Case 4 'write
                Me.Memory.WriteUInt16(Me.Memory.PopAddr, Me.Memory.PopStack)
            Case 5 'rst     (resets the main stack)
                Me.Memory.ResetStack()
            Case 6 'rsa     (resets the address stack)
                Me.Memory.ResetAddressStack()
        End Select
    End Sub

    Private Sub JumpOperations(instruction As Instruction)
        Select Case instruction.Selection
            Case 0 'goto
                Me.Pc = instruction.Parameter
            Case 1 'ife
                Dim value1 As UShort = Me.Memory.ReadUInt16(Me.Memory.PopAddr)
                Dim value2 As UShort = Me.Memory.ReadUInt16(Me.Memory.PopAddr)
                If value1 = value2 Then Me.Pc = instruction.Parameter

            Case 2 'ifn
                Dim value1 As UShort = Me.Memory.ReadUInt16(Me.Memory.PopAddr)
                Dim value2 As UShort = Me.Memory.ReadUInt16(Me.Memory.PopAddr)
                If value1 <> value2 Then Me.Pc = instruction.Parameter

            Case 3 'ifg
                Dim value1 As UShort = Me.Memory.ReadUInt16(Me.Memory.PopAddr)
                Dim value2 As UShort = Me.Memory.ReadUInt16(Me.Memory.PopAddr)
                If value1 > value2 Then Me.Pc = instruction.Parameter

            Case 4 'ifl
                Dim value1 As UShort = Me.Memory.ReadUInt16(Me.Memory.PopAddr)
                Dim value2 As UShort = Me.Memory.ReadUInt16(Me.Memory.PopAddr)
                If value1 < value2 Then Me.Pc = instruction.Parameter

            Case 5 'ret
                Me.Pc = Me.Memory.PopAddr()
            Case 6 'call
                Me.Memory.PushAddr(Me.Pc + instruction.Length)
                Me.Pc = instruction.Parameter
        End Select
    End Sub

    Private Sub ArithmeticOperations(instruction As Instruction)
        Select Case instruction.Selection
            Case 0 'add
                Dim value2 As UShort = Me.Memory.PopStack()
                Dim value1 As UShort = Me.Memory.PopStack()
                Me.Memory.PushStack(value1 + value2)
            Case 1 'subtract
                Dim value2 As UShort = Me.Memory.PopStack()
                Dim value1 As UShort = Me.Memory.PopStack()
                If value1 < value2 Then
                    'underflow occurred
                    Me.Memory.PushStack(UShort.MaxValue)
                Else
                    Me.Memory.PushStack(value1 - value2)
                End If
            Case 2 'divide
                Dim value2 As UShort = Me.Memory.PopStack()
                Dim value1 As UShort = Me.Memory.PopStack()
                If value2 = 0 Then
                    'division by zero occurred
                    Me.Memory.PushStack(UShort.MaxValue)
                Else
                    Me.Memory.PushStack(value1 \ value2)
                End If
            Case 3 'multiply
                Dim value2 As UShort = Me.Memory.PopStack()
                Dim value1 As UShort = Me.Memory.PopStack()
                If value1 > UShort.MaxValue / value2 Then
                    'overflow occurred
                    Me.Memory.PushStack(UShort.MaxValue)
                Else
                    Me.Memory.PushStack(value1 * value2)
                End If
            Case 4 'modulo
                Dim value2 As UShort = Me.Memory.PopStack()
                Dim value1 As UShort = Me.Memory.PopStack()
                If value2 = 0 Then
                    'division by zero occurred
                    Me.Memory.PushStack(UShort.MaxValue)
                Else
                    Me.Memory.PushStack(value1 Mod value2)
                End If
        End Select
    End Sub

    Private Sub BitwiseOperations(instruction As Instruction)
        Select Case instruction.Selection
            Case 0 'and
                Dim value2 As UShort = Me.Memory.PopStack()
                Dim value1 As UShort = Me.Memory.PopStack()
                Me.Memory.PushStack(value1 And value2)
            Case 1 'or
                Dim value2 As UShort = Me.Memory.PopStack()
                Dim value1 As UShort = Me.Memory.PopStack()
                Me.Memory.PushStack(value1 Or value2)
            Case 2 'xor
                Dim value2 As UShort = Me.Memory.PopStack()
                Dim value1 As UShort = Me.Memory.PopStack()
                Me.Memory.PushStack(value1 Xor value2)
            Case 3 'shr
                Dim value As UShort = Me.Memory.PopStack()
                Me.Memory.PushStack(value >> 1)
            Case 4 'shl
                Dim value As UShort = Me.Memory.PopStack()
                Me.Memory.PushStack(value << 1)
        End Select
    End Sub

    Private Sub StringOperations(instruction As Instruction)
        Select Case instruction.Selection
            Case 0 ' strlen
                Dim address As UShort = instruction.Parameter
                Me.Memory.PushStack(Me.Memory.ReadStringLength(address))
        End Select
    End Sub

    Private Function GetInstruction() As Instruction
        Dim address As UInt16 = Me.Memory.ReadUInt16(Me.Pc)
        Dim parameter As UInt16 = Me.Memory.ReadUInt16(Me.Pc + 2)
        Me.Pc += 4
        Return New Instruction(address, parameter)
    End Function


End Class
