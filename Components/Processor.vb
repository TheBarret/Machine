
Imports System.Net

Public Class Processor
    Public Property Pc As UShort
    Public Property Memory As Memory
    Public Property Running As Boolean
    Public Property Length As Integer
    Public Property Indents As Integer
    Public Event Debugger(message As String)

    Public Sub New()
        Me.Pc = 0
        Me.Indents = 0
        Me.Memory = New Memory
    End Sub

    Public Sub Load(program() As Byte)
        Me.Pc = 0
        Me.Indents = 0
        Me.Length = program.Length
        Me.Memory.Load(program)
    End Sub

    Public Sub Reset()
        Me.Pc = 0
        Me.Indents = 0
    End Sub

    Public Sub Clear()
        Me.Pc = 0
        Me.Indents = 0
        Me.Memory.Reset()
    End Sub

    Public Sub Run()
        Me.Running = True
        Do
            Me.Cycle()
        Loop Until Not Me.Running Or Me.Pc = Me.Length
    End Sub

    Public Sub Cycle()
        Try
            Dim instruction As Instruction = Me.GetInstruction
            Me.WriteLog("[{0}] (0x{1})", instruction.Opcode.ToString.Substring(2), instruction.Parameter.ToString("X4"))

            Select Case instruction.Opcode
                Case Types.T_NOP

                Case Types.T_PUSH
                    Me.Memory.PushStack(instruction.Parameter)

                Case Types.T_LOAD
                    Dim address As UInt16 = instruction.Parameter
                    Dim value As UInt16 = Me.Memory.ReadUInt16(address)
                    Me.Memory.PushStack(value)

                Case Types.T_STORE
                    Dim address As UInt16 = instruction.Parameter
                    Dim value As UInt16 = Me.Memory.PopStack
                    Me.Memory.WriteUInt16(instruction.Parameter, value)

                Case Types.T_RESET_STACK
                    Me.Memory.ResetStack()

                Case Types.T_RESET_ADDRESS_STACK
                    Me.Memory.ResetAddressStack()

                Case Types.T_RETURN
                    Me.Pc = Me.Memory.PopAddr()
                    Me.WriteLog("<- 0x{0}", Me.Pc.ToString("X4"))
                    Me.Indents -= 2

                Case Types.T_CALL
                    Me.Indents += 2
                    Me.Memory.PushAddr(Me.Pc)
                    Me.Pc = instruction.Parameter
                    Me.WriteLog("-> 0x{0}", Me.Pc.ToString("X4"))

                Case Types.T_END
                    Me.Running = False
                    Return

                Case Types.T_PRINT
                    Me.WriteLog("{0}", Me.Memory.ReadUInt16(instruction.Parameter))

                Case Types.T_GOTO
                    Me.Pc = instruction.Parameter

                Case Types.T_IF_EQUAL_JUMP
                    Dim value1 As UShort = Me.Memory.ReadUInt16(Me.Memory.PopAddr)
                    Dim value2 As UShort = Me.Memory.ReadUInt16(Me.Memory.PopAddr)
                    If value1 = value2 Then Me.Pc = instruction.Parameter

                Case Types.T_IF_NOT_EQUAL_JUMP
                    Dim value1 As UShort = Me.Memory.ReadUInt16(Me.Memory.PopAddr)
                    Dim value2 As UShort = Me.Memory.ReadUInt16(Me.Memory.PopAddr)
                    If value1 <> value2 Then Me.Pc = instruction.Parameter

                Case Types.T_IF_GREATER_JUMP
                    Dim value1 As UShort = Me.Memory.ReadUInt16(Me.Memory.PopAddr)
                    Dim value2 As UShort = Me.Memory.ReadUInt16(Me.Memory.PopAddr)
                    If value1 > value2 Then Me.Pc = instruction.Parameter

                Case Types.T_IF_LESSER_JUMP
                    Dim value1 As UShort = Me.Memory.ReadUInt16(Me.Memory.PopAddr)
                    Dim value2 As UShort = Me.Memory.ReadUInt16(Me.Memory.PopAddr)
                    If value1 < value2 Then Me.Pc = instruction.Parameter


                Case Types.T_ADDITION
                    Dim value2 As UShort = Me.Memory.PopStack()
                    Dim value1 As UShort = Me.Memory.PopStack()
                    Dim result As Int32 = CInt(value1) + CInt(value2)
                    result = (result And &HFFFF) + (result >> &H10) 'overflow check
                    Me.Memory.PushStack(result)

                Case Types.T_SUBTRACTION
                    Dim value2 As UShort = Me.Memory.PopStack()
                    Dim value1 As UShort = Me.Memory.PopStack()
                    If value1 < value2 Then                         'underflow occurred
                        Me.Memory.PushStack(UShort.MaxValue)
                    Else
                        Me.Memory.PushStack(value1 - value2)
                    End If
                Case Types.T_DIVISION_INT
                    Dim value2 As UShort = Me.Memory.PopStack()
                    Dim value1 As UShort = Me.Memory.PopStack()
                    If value2 = 0 Then                              'division by zero occurred
                        Me.Memory.PushStack(UShort.MaxValue)
                    Else
                        Me.Memory.PushStack(value1 \ value2)
                    End If
                Case Types.T_MULTIPLICATION
                    Dim value2 As UShort = Me.Memory.PopStack()
                    Dim value1 As UShort = Me.Memory.PopStack()
                    If value1 > UShort.MaxValue / value2 Then       'overflow occurred
                        Me.Memory.PushStack(UShort.MaxValue)
                    Else
                        Me.Memory.PushStack(value1 * value2)
                    End If
                Case Types.T_MODULO_INT
                    Dim value2 As UShort = Me.Memory.PopStack()
                    Dim value1 As UShort = Me.Memory.PopStack()
                    If value2 = 0 Then                              'division by zero occurred
                        Me.Memory.PushStack(UShort.MaxValue)
                    Else
                        Me.Memory.PushStack(value1 Mod value2)
                    End If

                Case Types.T_BITWISE_AND
                    Dim value2 As UShort = Me.Memory.PopStack()
                    Dim value1 As UShort = Me.Memory.PopStack()
                    Me.Memory.PushStack(value1 And value2)
                Case Types.T_BITWISE_OR
                    Dim value2 As UShort = Me.Memory.PopStack()
                    Dim value1 As UShort = Me.Memory.PopStack()
                    Me.Memory.PushStack(value1 Or value2)
                Case Types.T_BITWISE_XOR
                    Dim value2 As UShort = Me.Memory.PopStack()
                    Dim value1 As UShort = Me.Memory.PopStack()
                    Me.Memory.PushStack(value1 Xor value2)
                Case Types.T_BITWISE_SHIFT_RIGHT
                    Dim value2 As UShort = Me.Memory.PopStack()
                    Dim value1 As UShort = Me.Memory.PopStack()
                    Me.Memory.PushStack(value1 >> value2)
                Case Types.T_BITWISE_SHIFT_LEFT
                    Dim value2 As UShort = Me.Memory.PopStack()
                    Dim value1 As UShort = Me.Memory.PopStack()
                    Me.Memory.PushStack(value1 << value2)

                Case Types.T_STRING_LENGTH
                    Dim address As UShort = instruction.Parameter
                    Me.Memory.PushStack(Me.Memory.ReadStringLength(address))
            End Select
        Catch ex As Exception
            Me.Running = False
            RaiseEvent Debugger(String.Format("error: {0}", ex.Message))
        End Try
    End Sub

    Private Sub WriteLog(message As String, ParamArray values() As String)
        Dim msg As String = String.Format(message, values)
        RaiseEvent Debugger(msg.PadLeft(msg.Length + Me.Indents, " "c))
    End Sub

    Private Function GetInstruction() As Instruction
        Dim opcode As Byte = Me.Memory.ReadByte(Me.Pc)
        Dim value As UInt16 = Me.Memory.ReadUInt16(Me.Pc + 1)
        Me.Pc += 3
        Return New Instruction(opcode, value)
    End Function

End Class
