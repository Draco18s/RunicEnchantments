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
|`+` `-` `*` `,` `%`| Mathematical operators | Addition, Subtraction, Multiplication, Division, and Modulo. These also operate on two Vectors where `,` is a Dot Product and `*` is a Cross Product. Division by 0 terminates the IP. |
|`Z`| Negate | Multiplies the top value on the stack by -1 |
|`p`| Power | Pops two values `x` and `y` and pushes `y^x` (`y` raised to the power `x`) onto the stack (e.g. `>23p$;` will print `8`) |
|`>` `<` `^` `v`| Entry | Spawns an IP with the indicated facing with 10 mana at program start. Acts as an empty space otherwise. |
|`$` `@`| Output | `$` prints the top value of the stack to Debug.Log(), `@` dumps the entire stack and terminates the IP |
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
|`s`| Swap N | Pops a value off the stack, then pops that many values off the stack, rotates them right one, and pushes them back.  (e.g. if your stack is [1,2,3,4,3], calling `s` results in [1,4,2,3]) |
|`r`| Reverse | Reverses the stack |
|`l`| Length | Pushes the current length of the stack onto the stack |
|'o'| Sort | Pops ValueTypes off the stack until empty or a non-value is found (which is pushed back onto the stack). Sorts these values and pushes them back so that the smallest is on the top of the stack. Requires and consumes `n` mana where `n` is the size of the stack. |
|`y`| Delay | IP NOPs and does not advance once |
|`=`| Equals | Pops `x` and `y` and pushes `1` if they are equal, `0` otherwise |
|`(` `)`| Less and Greater | Less than and Greater than respectively. Pushes `1` if true, pushes `0` otherwise |
|`!`| Trampoline | Skips the next instruction |
|`?`| Conditional Trampoline | Pops the top value of the stack. If it is non-zero (or non-null for reference types), skip the next instruction |
|`V`| Vector3 | Pops three values `x`,`y`,`z` off the stack and pushes a resulting Vector3 onto the stack |
|`j`| Distance | Pops two GameObject or Vector3 off the stack and computes their distance |
|`'`| Character literal | Next cell is read as a literal character |
|`"`| Strign literal | IP enters reading mode and reads all cells as characters and concatenates them into a string on the top of the stack until another `"` is encountered. If the top of the stack is not a string, a new string is pushed onto the stack. |
|```| Character literal (continuous) | IP enters reading mode, pushing all cells onto the stack as characters until another ``` is encountered. |
|`k`| To Char | Pops the top value off the stack and converts it to a Character |
|`n`| To Number | Pops the top value off the stack and converts it to a double |
|`q`| Concatenate | Pops two values off the top of the stack `x` and `y` and concatenates them together as `yx` and pushes them back onto the stack. |
|`u`| Uncat | Pops a value `x` off the stack. If it is a string it peeks at the next value on the stack. If it is a character it is popped `y` and performs `x.Split(y)`. Otherwise it decomposes the string into characters and pushes them back onto the stack in order. If `x` was instead a Vector, it is decomposed into three floats, pushed onto the stack in `x, y, z` order. |
|`I` `J` `H` `L`| Fork | Pops a value `x` off the stack. Spawns a new pointer in the indicated direction (I-up, J-down, H-left, K-right) with `x` mana. Requires `x` mana, consumes `x-1` mana. Can also use `↤` `↦` `↥` `↧` |


### Unity specific commands

|        Rune       |           Name           | Description      |
|-------------------|--------------------------|------------------|
|`Q`| Nearby Objects | Pops a value `x` off the stack and calls `Physics.OverlapSphere(selfPosition, x)` pushing each returned GameObject onto the stack. Requires a minimum of `x`+1 mana to execute and consumes `x` mana. |
|`N`| Name | Pushes the name of a popped GameObject onto the stack |
|`t`| This | Pushes the GameObject representing `this` onto the stack (i.e. the GameObject the script is functionally running on) |
|`G`| Get GameObject | Pops a string off the stack and queries the ObjectRegistry for a value. Pushes the result. |
|`O`| Instantiate Ojbect | Pops a Vector `x` and GameObject `y` and spawns a clone of GameObject `y` at position `x`. Pushes the clone onto the stack. |
|`x`| Position | Pushes the Vector3 location of a popped GameObject onto the stack |
|`E`| IsEnemy? | Peeks at the top of the stack `x` and pushes 1 if `x` has the same layer tag as `this` |
|`h`| Harm | Pops two objects off the stack, `x` and `y`, if `y` is a GameObject then if it is an entity, `x` damage is dealt to it. If it is a temporary object of some kind, then it is destroyed (the object handles its own destruction, with the damage value as a paramter, different objects may behave differently). |

Any popped object that does not match the expected types is discarded, and the executed command is computed as best as possible (e.g. popping `(3,0,0)` and `5` off the stack when computing a distance `d` will result in a `3` being pushed onto the stack as a distance of `(3,0,0)` is `3` units: effectively results in pushing the vector's own magnitude).

Any attempted pop of an empty stack terminates the IP.

IPs that advance off the edge of the program (in any direction) are moved to the far edge. In this way `>1$` will print an endless series of `1`s.

Programs are terminated after 10000 execution steps in the event of infinite loops.

## Sample programs:

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

 - Various other Unity GameObject or Physics tasks (this is primary for using the language as enchantments on RPG gear: [ ] event entry points, [x] object instantiation, [x] IFF, [ ] Raycasting, etc)
 - Stacks of Stacks? (e.g. ><>'s `[` and `]` operators)
 - Register(s) (e.g. ><>'s `&` operator)
 - Error handling (prevent crashes, terminate execution when running infinitely, etc)