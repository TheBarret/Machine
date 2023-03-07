Public Class Instruction
    Private Property Left As Byte()
    Private Property Right As Byte()

    Sub New(left As UInt16, right As UInt16)
        Me.Left = BitConverter.GetBytes(left)
        Me.Right = BitConverter.GetBytes(right)
    End Sub

    Public ReadOnly Property Opcode() As Byte
        Get
            Return Me.Left(0)
        End Get
    End Property

    Public ReadOnly Property Selection() As Byte
        Get
            Return Me.Left(1)
        End Get
    End Property

    Public ReadOnly Property Parameter() As UInt16
        Get
            Return BitConverter.ToUInt16(Me.Right, 0)
        End Get
    End Property

    Public ReadOnly Property Length As UInt16
        Get
            Return 4
        End Get
    End Property
End Class
