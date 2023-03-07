Imports System.Text

Public Class Memory
    Enum Regions As Integer
        Stack = 65535               'Main stack starts at the end of the memory, size: 1024
        Address = 65535 - Frame     'Address stack starts at `65535 - 1024`, size: 1024
        Frame = 1024
    End Enum

    Public Property Buffer As Byte()
    Public Property Stack As UShort
    Public Property Address As UShort

    Public Sub New(size As Integer)
        Me.Buffer = New Byte(size) {}
        Me.Reset()
    End Sub

    Public Sub Reset()
        Me.Stack = Regions.Stack
        Me.Address = Regions.Address
        For i As Integer = 0 To Me.Buffer.Length - 1
            Me.Buffer(i) = &H0
        Next
    End Sub

    Public Sub ResetStack()
        Me.Stack = Regions.Stack
    End Sub

    Public Sub ResetAddressStack()
        Me.Address = Regions.Address
    End Sub

    Public Sub Load(buffer() As Byte)
        If (buffer.Length > Me.Length) Then
            Throw New Exception("size mismatch")
        End If
        For i As Integer = 0 To buffer.Length - 1
            Me.WriteByte(i, buffer(i))
        Next
    End Sub

#Region "Read/Write Byte"
    Public Function ReadByte(addr As UShort) As Byte
        If (addr > Me.Buffer.Length) Then
            Throw New Exception("illegal address")
        End If
        Return Me.Buffer(addr)
    End Function

    Public Sub WriteByte(addr As UShort, value As Byte)
        If (addr > Me.Buffer.Length) Then
            Throw New Exception("illegal address")
        End If
        Me.Buffer(addr) = value
    End Sub

    Public Function ReadBytes(addr As UShort, length As UShort) As Byte()
        Dim buffer() As Byte = New Byte(length - 1) {}
        For offset As Integer = 0 To buffer.Length - 1
            buffer(offset) = Me.ReadByte(addr + offset)
        Next
        Return buffer
    End Function

    Public Sub WriteBytes(addr As UShort, buffer() As Byte)
        For offset As Integer = 0 To buffer.Length - 1
            Me.WriteByte(addr + offset, buffer(offset))
        Next
    End Sub
#End Region

#Region "Read/Write UInt16"
    Public Function ReadUInt16(addr As UShort) As UShort
        If (addr + 1 > Me.Buffer.Length) Then
            Throw New Exception("illegal address")
        End If
        Dim b1 As UShort = Me.Buffer(addr)
        Dim b2 As UShort = Me.Buffer(addr + 1)
        Return (b2 << 8) Or b1
    End Function

    Public Sub WriteUInt16(addr As UShort, value As UShort)
        If (addr + 1 > Me.Buffer.Length) Then
            Throw New Exception("illegal address")
        End If
        Me.Buffer(addr) = value And &HFF
        Me.Buffer(addr + 1) = (value >> 8) And &HFF
    End Sub
#End Region

#Region "Read/Write Strings"
    Public Function ReadString(addr As UShort, length As UShort) As String
        Dim result As New StringBuilder(length - 1) 'minus nullchar
        For i As UShort = 0 To length - 1
            Dim c As Char = Strings.Chr(Me.ReadByte(addr + i))
            If (c = ControlChars.NullChar) Then Exit For
            result.Append(c)
        Next
        Return result.ToString()
    End Function

    Public Function ReadStringLength(addr As UShort) As UShort
        Dim length As UShort = 0
        For i As UShort = 0 To Length - 1
            Dim c As Char = Strings.Chr(Me.ReadByte(addr + i))
            If (c = ControlChars.NullChar) Then Exit For
            length += 1
        Next
        Return length
    End Function

    Public Sub WriteString(address As UShort, value As String)
        Dim length As UShort = CType(value.Length, UShort)
        For i As UShort = 0 To length - 1
            Dim c As Char = value.Chars(i)
            Me.WriteByte(address + i, Strings.AscW(c))
        Next
        Me.WriteByte(address + length, 0) 'add nullchar
    End Sub
#End Region

#Region "Read/Write Float16"
    Public Function ReadFloat16(addr As UShort) As Single
        Dim bytes As Byte() = Me.ReadBytes(addr, 2)
        Dim value As Single = BitConverter.ToSingle(New Byte() {bytes(0), bytes(1), 0, 0}, 0)
        Return value
    End Function

    Public Sub WriteFloat16(address As UShort, value As Single)
        Dim bytes As Byte() = BitConverter.GetBytes(value)
        Me.WriteBytes(address, bytes)
    End Sub
#End Region

#Region "Main Stack"
    Public Sub PushStack(value As UShort)
        If (Me.Stack <= Regions.Stack) Then Throw New Exception("stack underflow")
        Me.Stack -= 2
        Me.Buffer(Me.Stack + 1) = CByte(value And &HFF)
        Me.Buffer(Me.Stack) = CByte((value >> 8) And &HFF)
    End Sub

    Public Function PopStack() As UShort
        If (Me.Stack >= Regions.Stack + Regions.Frame) Then Throw New Exception("stack overflow")
        Dim value As UShort = Me.Buffer(Me.Stack + 1)
        value = (value << 8) Or Me.Buffer(Me.Stack)
        Me.Stack += 2
        Return value
    End Function

    Public Function PeekStack() As UShort
        If (Me.Stack >= Regions.Stack + Regions.Frame) Then Throw New Exception("stack overflow")
        Dim value As UShort = Me.Buffer(Me.Stack + 1)
        value = (value << 8) Or Me.Buffer(Me.Stack)
        Return value
    End Function
#End Region

#Region "Address Stack"
    Public Sub PushAddr(value As UShort)
        If (Me.Address <= Regions.Address) Then Throw New Exception("stack underflow")
        Me.Address -= 2
        Me.Buffer(Me.Address + 1) = CByte(value And &HFF)
        Me.Buffer(Me.Address) = CByte((value >> 8) And &HFF)
    End Sub

    Public Function PopAddr() As UShort
        If (Me.Address >= Regions.Address + Regions.Frame) Then Throw New Exception("stack overflow")
        Dim value As UShort = Me.Buffer(Me.Address + 1)
        value = (value << 8) Or Me.Buffer(Me.Address)
        Me.Address += 2
        Return value
    End Function

    Public Function PeekAddr() As UShort
        If (Me.Address >= Regions.Address + Regions.Frame) Then Throw New Exception("stack overflow")
        Dim value As UShort = Me.Buffer(Me.Address + 1)
        value = (value << 8) Or Me.Buffer(Me.Address)
        Return value
    End Function
#End Region

#Region "Helpers"
    Public ReadOnly Property Length As UInt16
        Get
            Return Me.Buffer.Length
        End Get
    End Property
#End Region

End Class
