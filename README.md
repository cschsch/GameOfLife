# GameOfLife
Conway's Game of Life implementation in C# (.NET Core) 

This project was mainly built as an exercise; it includes the standard implementation and an advanced one (environmental). The environmental one gives each cell another property: it is either a carnivore or a herbivore. Carnivores can convert herbivores, but not the other way around.

Additionally, the world has a settable temperature which influences the chance of conversion.

# Usage
dotnet GameOfLife.dll [args]

# Command line arguments
  --game_type [0 | Standard, 1 | Environmental]
  
    Default: Environmental
    
    Type of game to be played. This is either the standard, or the aforementioned advanced one.
  
  -s, --size
  
    Default: 69
    
    Size of world to be played
  
  -f, --figure [Pentadecathlon, Pulsar, LWSS, Glider]
  
    If you want to play a predefined figure, enter its name here
  
  --file
  
    If you want to play with a custom layout, enter its filename here
    
  -t, --thread_sleep
  
    Delay between generations in milliseconds
  
  -c, --closed
  
    Include to use the ClosedNeighbourFinder which doesn't span across borders
    
  --temperature
  
    Change the world temperature (only makes sense to set if you use the Environmental game type)
  
  -i, --print_interval
  
    Default: 100
    
    Set the print interval of the ResultAnalyzer
    
  -p, --print_file
  
    Default: "analyzation\01.txt" (Only works if folder analyzation exists within executing directory)
    
    Set the location where the ResultAnalyzer prints its data
    
  --seasons
  
    Set this flag if you want to use the season system
  
Note: ConsoleRenderer requires Kernel32.dll (necessary to prevent flickering which would occur when using Console.Write)
