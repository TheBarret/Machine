# Machine
A very basic 16bit Virtual Machine with a very limited instruction set (WORK IN PROGRESS / UNFINISHED)

![](https://i.imgur.com/wFdsGo4.png)


<pre>
Project		: VM16
Description	: 16Bit Virtual Machine
Language	: VB.Net (Framework 4.7.2)
Data types	: String, UInt16, Byte

-------------------------------------------------------------------------------------------------------------------
Memory Mapping

Free space				:   0x0000 ~ 0xF7FF = 63487 bytes
Stack (Address)			        : 	0xF7FF ~ 0xFBFF = 1024 bytes
Stack					:	0xFBFF ~ 0xffff = 1024 bytes

-------------------------------------------------------------------------------------------------------------------
Instructions

instr.   parameter        description
-------|----------------|------------------------------------------------------------------------------------------
<pre>
nop						: No operation
load 	<variable>		: Loads a variable on the stack
push 	<value>			: Loads a number on the stack
call 	<address>		: Sets pc to address and pushes current address to address stack
return					: Pops address from address stack and jumps to address
end						: Stops the current operation and exits program
rst						: Resets the stack
rsta					: Resets the address stack
slen					: Pushes the count of stack values, on the stack
random					: Takes two values of the stack and returns a random generated value on the stack
print 	<value>			: prints a single number value to the stdout
printl 	<address>		: prints a string at address to the stdout
read 	<address>		: reads a single value from the stdin and stores it in address
readl 	<address>		: reads a line from the stdin and stores it in address
goto 	<address>		: Jumps to address without condition, does not push jump address
ife  	<address>		: Takes two values of stack, if `a == b` jumps to address (stores return address)
ifne	<address>		: Takes two values of stack, if `a != b` jumps to address (stores return address)
ifg  	<address>		: Takes two values of stack, if `a > b` jump to address (stores return address)
ifl  	<address>		: Takes two values of stack, if `a < b` jumps to address (stores return address)
else 	<address>		: Used in tandem with `ife, ifne, ifg or ifl` (stores return address)
add						: Takes two values of stack and returns the sum of `a + b` on the stack
sub						: Takes two values of stack and returns the sum of `a - b` on the stack
div						: Takes two values of stack and returns the sum of `a / b` on the stack
mul						: Takes two values of stack and returns the sum of `a * b` on the stack
mod						: Takes two values of stack and returns the sum of `a mod b` on the stack
and						: Takes two values of stack and returns the bitwise operation of `a and b`
or						: Takes two values of stack and returns the bitwise operation of `a or b`
xor						: Takes two values of stack and returns the bitwise operation of `a xor b`
shr						: Takes two values of stack and returns the bitwise operation of `a >> b`
shl						: Takes two values of stack and returns the bitwise operation of `a << b`
shlc					: Takes two values of stack and returns the bitwise operation of `a <<< b` or `carry`
shrc					: Takes two values of stack and returns the bitwise operation of `a >>> b` or `carry`
</pre>
