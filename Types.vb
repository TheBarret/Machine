Public Enum Types As Byte
    '// Basic Operations
    T_NOP = &H0
    T_LOAD
    T_PUSH
    T_STORE
    T_PRINT
    T_RESET_STACK
    T_RESET_ADDRESS_STACK

    '// Conditional and Jump Operations
    T_GOTO
    T_CALL
    T_RETURN
    T_IF_EQUAL_JUMP
    T_IF_NOT_EQUAL_JUMP
    T_IF_GREATER_JUMP
    T_IF_LESSER_JUMP

    '// Arithmetic Operations
    T_ADDITION
    T_SUBTRACTION
    T_DIVISION_INT
    T_MULTIPLICATION
    T_MODULO_INT

    '// Bitwise Operations
    T_BITWISE_AND
    T_BITWISE_OR
    T_BITWISE_XOR
    T_BITWISE_SHIFT_RIGHT
    T_BITWISE_SHIFT_LEFT

    '// String Operations
    T_STRING_LENGTH

    '// Types
    T_LABEL
    T_VALUE
    T_CONSTANT
    T_ADDRESS

    '// Internal Tokens
    T_CRLF
    T_SPACE
    T_COMMENT
    T_EOF
    T_END = &HFF
End Enum