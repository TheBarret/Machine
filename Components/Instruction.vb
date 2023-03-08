Imports System.Runtime.InteropServices

Public Class Instruction

    Public Property Parameter As UInt16
    Public Property Instruction As Byte

    Sub New(instruction As Byte, param As UInt16)
        Me.Instruction = instruction
        Me.Parameter = param
    End Sub

    Public ReadOnly Property Opcode() As Types
        Get
            Return CType(Me.Instruction, Types)
        End Get
    End Property

    Public ReadOnly Property Length As UInt16
        Get
            Return Marshal.SizeOf(Me.Instruction) + Marshal.SizeOf(Me.Parameter)
        End Get
    End Property
End Class
