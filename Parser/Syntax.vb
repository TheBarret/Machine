Imports System.Text.RegularExpressions

Public Class Syntax
    Implements IDefinition

    Public Property Patterns As List(Of Rule) Implements IDefinition.Rules

    Sub New()
        Me.Patterns = New List(Of Rule)

        '// Internal Tokens
        Me.Patterns.Add(New Rule(New Regex("^(\r\n|\r|\n|\|)", Me.Options), Types.T_CRLF))
        Me.Patterns.Add(New Rule(New Regex("^(\/\/)(.*?)(\r?\n|$)", Me.Options), Types.T_COMMENT))
        Me.Patterns.Add(New Rule(New Regex("^\s+", Me.Options), Types.T_SPACE))

        '// Basic Operations
        Me.Patterns.Add(New Rule(New Regex("^\bpush\b", Me.Options), Types.T_PUSH))
        Me.Patterns.Add(New Rule(New Regex("^\bload\b", Me.Options), Types.T_LOAD))
        Me.Patterns.Add(New Rule(New Regex("^\bstore\b", Me.Options), Types.T_STORE))
        Me.Patterns.Add(New Rule(New Regex("^\bprint\b", Me.Options), Types.T_PRINT))
        Me.Patterns.Add(New Rule(New Regex("^\bend\b", Me.Options), Types.T_END))

        Me.Patterns.Add(New Rule(New Regex("^\brst\b", Me.Options), Types.T_RESET_STACK))
        Me.Patterns.Add(New Rule(New Regex("^\brsta\b", Me.Options), Types.T_RESET_ADDRESS_STACK))

        '// Conditional and Jump Operations
        Me.Patterns.Add(New Rule(New Regex("^\bgoto\b", Me.Options), Types.T_GOTO))
        Me.Patterns.Add(New Rule(New Regex("^\bcall\b", Me.Options), Types.T_CALL))
        Me.Patterns.Add(New Rule(New Regex("^\breturn\b", Me.Options), Types.T_RETURN))
        Me.Patterns.Add(New Rule(New Regex("^\bife\b", Me.Options), Types.T_IF_EQUAL_JUMP))
        Me.Patterns.Add(New Rule(New Regex("^\bifne\b", Me.Options), Types.T_IF_NOT_EQUAL_JUMP))
        Me.Patterns.Add(New Rule(New Regex("^\bifg\b", Me.Options), Types.T_IF_GREATER_JUMP))
        Me.Patterns.Add(New Rule(New Regex("^\bifl\b", Me.Options), Types.T_IF_LESSER_JUMP))

        '// Arithmetic Operations
        Me.Patterns.Add(New Rule(New Regex("^\badd\b", Me.Options), Types.T_ADDITION))
        Me.Patterns.Add(New Rule(New Regex("^\bsub\b", Me.Options), Types.T_SUBTRACTION))
        Me.Patterns.Add(New Rule(New Regex("^\bdiv\b", Me.Options), Types.T_DIVISION_INT))
        Me.Patterns.Add(New Rule(New Regex("^\bmul\b", Me.Options), Types.T_MULTIPLICATION))
        Me.Patterns.Add(New Rule(New Regex("^\bmod\b", Me.Options), Types.T_MODULO_INT))

        '// Bitwise Operations
        Me.Patterns.Add(New Rule(New Regex("^\band\b", Me.Options), Types.T_BITWISE_AND))
        Me.Patterns.Add(New Rule(New Regex("^\bor\b", Me.Options), Types.T_BITWISE_OR))
        Me.Patterns.Add(New Rule(New Regex("^\bxor\b", Me.Options), Types.T_BITWISE_XOR))
        Me.Patterns.Add(New Rule(New Regex("^\bshr\b", Me.Options), Types.T_BITWISE_SHIFT_RIGHT))
        Me.Patterns.Add(New Rule(New Regex("^\bshl\b", Me.Options), Types.T_BITWISE_SHIFT_LEFT))

        '// String Operations
        Me.Patterns.Add(New Rule(New Regex("^\blen\b", Me.Options), Types.T_STRING_LENGTH))

        '// Types & structures
        Me.Patterns.Add(New Rule(New Regex("^[0-9]+", Me.Options), Types.T_CONSTANT))
        Me.Patterns.Add(New Rule(New Regex("^\#[a-z0-9]+", Me.Options), Types.T_VALUE))
        Me.Patterns.Add(New Rule(New Regex("^(:)(?:[a-z@][a-z0-9_]*)", Me.Options), Types.T_LABEL))
        Me.Patterns.Add(New Rule(New Regex("^\[(?:[a-z@][a-z0-9_]*)\]", Me.Options), Types.T_ADDRESS))
    End Sub

    Public ReadOnly Property Name As String Implements IDefinition.Name
        Get
            Return "VM16"
        End Get
    End Property

    Public ReadOnly Property Options As RegexOptions Implements IDefinition.Options
        Get
            Return RegexOptions.Singleline Or RegexOptions.IgnoreCase
        End Get
    End Property

End Class
