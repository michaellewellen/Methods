int choice = LoadMainMenu();
switch (choice)
{
    case 1: 
        GameOfSticks();
        break;
    case 2: 
        MasterMind();
        break;
    case 3:
        MadLibs();
        break;
    case 4:
        PigLatin();
        break;
    case 5:
        TypingTrainer();
        break;
    case 6:
        Maze();
        break;
    default:
        break;
}
static void GameOfSticks()
{
    bool playAgain = true;
    int numSticks = 20;
    DisplayInstructions(numSticks);
    while(playAgain)
    {
        numSticks = 20;
        playAgain = true;
        int maxSticks = 3;
        int player = 1; 
        while(numSticks > 0)
        {
            DisplaySticks(numSticks);
            maxSticks = ValidateMaxSticks(numSticks);
            int selectedSticks = GetSelectedSticksFromUser(player,maxSticks);
            numSticks = AdjustNumSticks(selectedSticks,numSticks);
            player = UpdatePlayer(player);
        }
        Console.WriteLine($"Player {player} has won the game");
        Console.WriteLine("Would you care to play again?");
        char again = Console.ReadKey().KeyChar;
        if (again == 'y' || again == 'Y')
            playAgain = true;
        else 
            playAgain = false;
    }
    Console.WriteLine("\nThanks for playing.");
    static void DisplayInstructions(int x)
    {
        string instructions = @$"
        The Game of Sticks begins with {x} sticks
        Players then alternate drawing between 1 and
        3 sticks each turn. The last stick drawn is 
        loser. Good Luck!
        
        Press any key to begin";
        Console.Clear();
        Console.WriteLine(instructions);
        Console.ReadKey(true);
        Console.Clear();
    }
    static void DisplaySticks(int x)
    {
        Console.Clear();
        for(int i = 0; i<x;i++)
            Console.Write("|-| ");
        Console.WriteLine();
        for(int i = 0; i<x;i++)
            Console.Write("| | ");
        Console.WriteLine();
        for(int i = 0; i<x;i++)
            Console.Write("| | ");
        Console.WriteLine();
        for(int i = 0; i<x;i++)
            Console.Write("|_| ");
        Console.WriteLine($"\n{x} sticks remaining ...\n");
    }
    static int ValidateMaxSticks(int x)
    {
        if(x <3)
            return x;
        else    
            return 3;
    }
    static int GetSelectedSticksFromUser(int who, int x)
    {
        Console.Write($"Player {who} how many sticks would you like to take?");
        bool successful = false;
        int value;
        while (!successful)
        {
            successful = Int32.TryParse(Console.ReadLine(),out value);
            if(successful && value >0 && value <= x)
                return value;
            else if (successful)
            {
                Console.WriteLine($"I need a number between 1 and {x}");
                successful = false;
            }
            else
            {
                Console.WriteLine("I need a number please.");
                successful = false;
            }

        }
        return 0;
    }
    static int AdjustNumSticks(int x,int num)
    {
        return num - x;
    }
    static int UpdatePlayer(int who)
    {
        if(who != 1)
            return 1;
        else    
            return 2;
    }
}
static void MasterMind()
{}
static void MadLibs()
{}
static void PigLatin()
{}
static void TypingTrainer()
{}
static void Maze()
{}
static int LoadMainMenu()
{
    string intro = @"
    Please pick a program to run

    1) Game of Sticks
    2) Master Mind
    3) Mad Libs
    4) Pig Latin
    5) Typing Trainer
    6) Maze

    Please make your choice";
    Console.Clear();
    Console.WriteLine(intro);
    bool successful = false;
    int x;
    while (!successful)
    {
        string option = Console.ReadKey(true).KeyChar.ToString();
        successful = Int32.TryParse(option,out x);
        if(successful && x <= 6 && x >=1)
            return x;
        else if (successful)
            successful = false;   
    }
    return 0;
}