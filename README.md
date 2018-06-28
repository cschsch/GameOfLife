# GameOfLife
Conway's Game of Life implementation in C# (.NET Core) 

This project was mainly built as an exercise; it includes the standard implementation and an advanced one (environmental). The environmental one gives each cell another property: it is either a carnivore or a herbivore. Carnivores can convert herbivores, but not the other way around.

Additionally, the world has a settable temperature which influences the chance of conversion.

Note: ConsoleRenderer requires Kernel32.dll (necessary to prevent flickering which would occur when using Console.Write)
