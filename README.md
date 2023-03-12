# Machine
A very basic 16bit Virtual Machine with a very limited instruction set (WORK IN PROGRESS / UNFINISHED)

![](https://i.imgur.com/wFdsGo4.png)


<pre>
Project		: VM16
Description	: 16Bit Virtual Machine

Data Operations:
T_PUSH	 : Pushes a value onto the stack
T_LOAD	 : Loads a value from the specified memory address onto the stack
T_STORE	 : Stores a value from the stack into the specified memory address
T_SLEN	 : Pushes the length of the stack onto the stack
T_ALEN	 : Pushes the length of the address stack onto the stack

Arithmetic Operations:

T_ADDITION		 : Adds two values from the stack and pushes the result onto the stack
T_SUBTRACTION	 : Subtracts two values from the stack and pushes the result onto the stack
T_DIVISION_INT	 : Divides two values from the stack and pushes the integer result onto the stack
T_MULTIPLICATION : Multiplies two values from the stack and pushes the result onto the stack
T_MODULO_INT	 : Calculates the integer modulo of two values from the stack and pushes the result onto the stack

Bitwise Operations:

T_BITWISE_AND		 : Applies the bitwise AND operation on two values from the stack and pushes the result onto the stack
T_BITWISE_OR		 : Applies the bitwise OR operation on two values from the stack and pushes the result onto the stack
T_BITWISE_XOR		 : Applies the bitwise XOR operation on two values from the stack and pushes the result onto the stack
T_BITWISE_SHIFT_RIGHT: Applies the bitwise shift right operation on two values from the stack and pushes the result onto the stack
T_BITWISE_SHIFT_LEFT : Applies the bitwise shift left operation on two values from the stack and pushes the result onto the stack

Control Flow Operations:

T_IF_EQUAL_JUMP		 : Pops two values from the stack and jumps to the specified label if they are equal
T_IF_NOT_EQUAL_JUMP	 : Pops two values from the stack and jumps to the specified label if they are not equal
T_IF_GREATER_JUMP	 : Pops two values from the stack and jumps to the specified label if the first value is greater than the second
T_IF_LESSER_JUMP	 : Pops two values from the stack and jumps to the specified label if the first value is lesser than the second
T_ELSE				 : Provides supplementary Else for the If opcodes
T_GOTO				 : Jumps to the specified label unconditionally
T_CALL				 : Calls a subroutine with a specified label and pushes the current address onto the address stack
T_RETURN			 : Returns from a subroutine and pops the previous address from the address stack

IO Operations:
T_PRINT				 : Prints a value variable address to stdout
T_RANDOM			 : Pops two values from the stack and pushes a random generated number of these values
</pre>
