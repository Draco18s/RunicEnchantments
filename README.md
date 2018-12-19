# Runic Enchantments 2D Language Interpreter

Runic script is a 2D interpreted language with multiple simultaneously processed instruction pointers. Each instruction pointer (IP) is spawned at an entry point with a mana ("energy") value, a direction, and an empty stack. Each cycle each IP (in order of creation) executes the instuction under itself and then moves forwards. If the instruction requires a quantity of mana and the IP does not have that much, then the update is treated as a NOP and the IP is not advanced.

At the end of each update the following actiosn are performed (in order):

 - Any pointers that exist in the same cell and have the same facing are merged: The older one retains its stack and gains the mana of the newer one, which is then destroyed.
 
 - Any IP that has more than 100 mana burns out the rune it is on (making it act like a ` `) and half of its mana is lost.
 
 - Any IP with a stack size greater than `mana + 10` loses 1 mana.
 
 - Any IP that has 0 or less mana is destroyed.

## Commands

|        Rune       |           Name           | Description      |
|-------------------|--------------------------|------------------|
|` `| Empty | NOP - Nothing happens. IP will advance to the next cell. |
|`0`-`9`| Integer Literals | Pushes the single digit value onto the stack |
|`a`-`f`| Integer Literals | Literals for the values 10-15 |
|`P`| Pi | Pushes the value of Pi onto the stack. Can also use `π` |
|`+` `-` `*` `,` `%`| Mathematical operators | Addition, Subtraction, Multiplication, Division, and Modulo (All math operations perform an implicit conversion to a number where possible). These also operate on two Vectors where `,` is a Dot Product and `*` is a Cross Product. Division by 0 terminates the IP. For strings, `+` concatenates as `yx` and `*` takes a number (cast to int) `y` and makes that many copies of `x` (e.g. `"asdf"3*` pushes `"asdfasdfasdf" onto the stack) |
|`X` `C` `Y` | Power of Ten | Multiplies the top value of the stack by 10, 100, and 1000 respectively. |
|`Z`| Negate | Multiplies the top value on the stack by -1 |
|`p`| Power | Pops two values `x` and `y` and pushes `y^x` (`y` raised to the power `x`) onto the stack (e.g. `>23p$;` will print `8`) |
|`A`| Math | Pops two values, `o` and `x`, if `o` is a character, it maps to the single-argument Math functions, passing `x` as the argument (grouped by relation): `S`,`C`,`T`,`i`,`o`,`a`: Sin, Cos, Tan, ASin, ACos, ATan. `f`,`c`,`r`: Floor, Ceiling, Round. `\|`: Absolute value. `e`,`q`,`l`,`L`: Exp, Sqrt, Log, Log10 |
|`>` `<` `^` `v`| Entry | Spawns an IP with the indicated facing with 10 mana at program start. Acts as an empty space otherwise. |
|`$` `@`| Output | `$` prints the top value of the stack to Debug.Log(), `@` dumps the entire stack and terminates the IP |
|`i`| Input | Reads from input treating objects as whitespace-speparated values (eg. `123.4 qwerty` will first read a double value of `123.4` then on the next read, read the string `qwerty`). Using a `\` before a whitespace character will treat it as input (e.g. `as df` is two separate input strings while `as\ df` is one). |
|`;`| Terminator | Destroys an IP |
|`m`| Mana | Pushes the current mana value onto the stack |
|`F`| Fizzle | Deducts 1 mana from the IP |
|`M`| Min Mana | Acts as a threshold barrier. The top value of the stack is popped and compared to the IP's mana. If the mana is greater than the popped value, the IP advances. Otherwise the value is pushed back. |
|`\` `/` `\|` `_` `#`| Reflectors | Changes the direction of an IP |
|`U` `D` `L` `R`| Direction | Changes the pointers direction UP, DOWN, LEFT, RIGHT respectively. Can also use `↑` `↓` `←` `→` |
|`:`| Duplicator | Duplicates the top value on the stack |
|`~`| Pop | Pops the top value off the stack and discards it |
|`{` `}`| Rotate Stack | Rotates the entire stack left or right respectively |
|`S`| Swap | Swaps the top two values of the stack |
|`s`| Swap N | Pops a value off the stack, then pops that many values off the stack, rotates them right one, and pushes them back.  (e.g. if your stack is [1,2,3,4,3], calling `s` results in [1,4,2,3]). See also the Swap modifier |
|`r`| Reverse | Reverses the stack |
|`l`| Length | Pushes the current length of the stack onto the stack |
|`o`| Sort | Pops ValueTypes off the stack until empty or a non-value is found (which is pushed back onto the stack). Sorts these values and pushes them back so that the smallest is on the top of the stack. Requires and consumes `n` mana where `n` is the size of the stack minus 10. |
| `[` | Pop stack | Pop `x` off the stack and create a new stack, moving `x` values from the old stack onto the new one (maintaining the same order). The new stack is completely isolated from the previous one, and an arbitrary amount of stacks can be created on top of each other. Costs 1 mana.
| `[` | Push stack | Remove the current stack, moving its values to the top of the underlying stack. If the current stack is the last stack, `]` simply empties the stack.
|`y`| Delay | IP NOPs and does not advance once. See also the delay modifiers. |
|`=`| Equals | Pops `x` and `y` and pushes `1` if they are equal, `0` otherwise |
|`(` `)`| Less and Greater | Less than and Greater than respectively. Pushes `1` if true, pushes `0` otherwise |
|`!`| Trampoline | Skips the next instruction |
|`?`| Conditional Trampoline | Pops the top value of the stack. If it is non-zero (or non-null for reference types), skip the next `n` instructions where `n` is the popped value (if numeric) |
|`V`| Vector3 | Pops three values `x`,`y`,`z` off the stack and pushes a resulting Vector3 onto the stack |
|`j`| Distance | Pops two GameObject or Vector3 off the stack and computes their distance |
|`'`| Character literal | Next cell is read as a literal character |
|`"`| Strign literal | IP enters reading mode and reads all cells as characters and concatenates them into a string on the top of the stack until another `"` is encountered. If the top of the stack is not a string, a new string is pushed onto the stack. |
| \` | Character literal (continuous) | IP enters reading mode, pushing all cells onto the stack as characters until another \` is encountered. |
|`k`| To Char | Pops the top value off the stack and converts it to a Character |
|`n`| To Number | Pops the top value off the stack and converts it to a double (if it is a value type) or tries to parse it as a double (if it is a string) |
|`q`| Concatenate | Pops two values off the top of the stack `x` and `y` and concatenates them together as `yx` and pushes them back onto the stack. |
|`u`| Uncat | Pops a value `x` off the stack. If it is a string it peeks at the next value on the stack. If it is a character it is popped `y` and performs `x.Split(y)`. Otherwise it decomposes the string into characters and pushes them back onto the stack in order. If `x` was instead a Vector, it is decomposed into three floats, pushed onto the stack in `x, y, z` order. |
|`I` `J` `H` `L`| Fork | Pops a value `x` off the stack. Spawns a new pointer in the indicated direction (I-up, J-down, H-left, K-right) with `x` mana. Requires `x` mana, consumes `x-1` mana. Can also use `↤` `↦` `↥` `↧` |
|`T`| Transfer Stack | If this is the only IP on this cell or it has no stack, the IP does nothing and does not advance. Otherwise, it pops a value `x` (cast to int), then pops `x` items off its own stack and transfers these values to *all* other IPs on this cell (they end up in the same order they were originally in). Stack underflow still transfers the items that did exist (although the source IP is still terminated). After a transfer has occurred, held IPs will 'skip', allowing them to advance next cycle. Note that if IPs are facing the same direction, they will be merged. See also the Swap modifier. |
| `B` | Branch | Pops `y` and `x` from the stack and causes the IP to jump to the position `(x,y)` on the program grid and continue executing from there. The coordinates of the return point (where the IP would have been if it had not jumped) are pushed to the top of the stack. If a direction modifier is applied to this rune, it takes effect before the jump. This effectively allows for the creation of functions. See also the directional modifiers.
| `E` | Eval | Pops a string from the IP and executes it as if it was a Runic program. This IP is frozen in place until the Eval context terminates (each update tick the IP recieves causes 1 update tick in the Eval context). Costs an amount of mana equal to `ln(size-5)^2`, where `size` is the length of the string to be Evaluated (min 0). See also the Swap modifier.
| `w` | Write | Aka reflection. Pops a char `c`, and values `y`, and `x` from the stack and writes `c` to the program grid at `(x,y)`. If `x` and `y` are not both value types (or don't exist), the IP's own location is used, and `x` and `y` are returned to the stack (if they exist). If `c` is a modifier (see below) it is written to the modifier grid instead of the execution grid. Costs 1 mana.

### Modifiers
Modifiers are Unicode Combining diacritical marks. These marks either slightly tweak the effect of a given command rune or otherwise alter the behavior of the instruction pointer. Note also the byte penalty for using these modifiers.

|        &nbsp;Rune&nbsp;         |           Name           | Description      |
|---------------------|--------------------------|------------------|
| `̂` `̌` `᷾` `͐` `̭` `̬` `͔` `͕`  | Direction | Changes the direction of the IP to the indicated facing. Both above and below diacritical marks work the same and are provided for sake of visibility.
| `̇` `̈` `̣` `̤` | Delay | Adds an amount of delay to the IP, stopping it for a certain number of update cycles (similar to the `y` command). Values are 1, 2, 4, and 8 respectively.
| `̺` `̪` | Truncate Stack | This modifier will remove items from an IP's stack until the stack fits within the maximum amount allowed by the IP's current mana (this occurs after execution and before the stack-too-large fizzling). Truncates from the top and bottom respectively.
| `͍` | Switch/Swap | When applied to the `E`val rune, this causes the eval context to use the executing IP's stack as input and output (pushing and popping from the top as normal). When applied to the `s`wap-N rune, the popped collection is rotated left instead of right. When applied to the `T`ranser stack rune, the IP *pulls* from the other IPs instead of pushing to them.
| `͏` | Blank Modifier | This non-rendering mark when written using reflection will clear a modifier character.
| `̀` `́` `̍` `̄` `̖` `̗` `̩` `̠` `̊` `̻` | Reflectors | These modifiers act like reflectors, taking effect after the primary rune is executed. Above and below variants available for all types of reflectors, listed in order: `\\` `/` `|` `_` with the `#` variants listed last.

Any popped object that does not match the expected types is discarded, and the executed command is computed as best as possible (e.g. popping `(3,0,0)` and `5` off the stack when computing a distance `d` will result in a `3` being pushed onto the stack as a distance of `(3,0,0)` is `3` units: effectively results in pushing the vector's own magnitude as the 5 is ignored).

Any attempted pop off of an empty stack terminates the IP.

IPs that advance off the edge of the program (in any direction) are moved to the far edge. In this way `>1$` will print an endless series of `1`s.

Programs are terminated after 10000 execution steps in the event of infinite loops.

## Sample programs:

### Hello World

Simple:

`>"Hello World!"$;`

With IP redirection:

    >"Hello"' q\
     ;$"!dlroW"/
	 
With two instruction pointers:

    >"Hello"$;
	>" World"$;

### Other examples

`>>55+55++M4$;` prints 4 exactly once (two IPs are created but cannot pass the `M` command until they merge and have 20 mana: the value on their stack

`>>55+55++4$;` prints 4 twice (both IPs reach the `4` and print it)

`>>55+55++:M$;` prints 20 once (again the two IPs need to merge before being able to pass the `M` command. Before executing `M` the combined IP will have a stack of [20,20] due to the `:` duplicate command, so after `M` it prints the single value left on the stack)

This program:

	 v$
	>31+$;
	 2^
	 +
	 $
	 ;;

Has the output `1,5,4`: the lower right IP reaches its `$` first, while the top IP is created first (and so its `$` is processed in the same tick as, but before, the leftmost IP's `$`).

`>4Qtd$;` prints the distance between the self-object and one other GameObject within radius 4 (in the sample scene this is `3.162278` and an empty stack)

`"3X4+kSq$;` is a [quine](https://en.wikipedia.org/wiki/Quine_(computing)).

## TODO:

 - Various other Unity GameObject or Physics tasks (this is primary for using the language as enchantments on RPG gear:
   - [ ] event entry points,
   - [x] object instantiation
   - [x] IFF
   - [ ] Raycasting
 - Register(s) (e.g. ><>'s `&` operator)
 - Better error handling (prevent crashes, terminate execution when running infinitely, etc)
