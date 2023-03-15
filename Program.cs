while(true)
{
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
            Environment.Exit(0);
            break;
    }
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
{
    DisplayTheRules();
    string codeWord = GenerateAWord();
    Console.Clear();
    string userGuess = "";
    int numGuesses = 0;
    while(codeWord != userGuess)
    {
        numGuesses ++;
        userGuess = ValidateUserGuess(numGuesses,codeWord.Length);
        for(int i = 0; i < codeWord.Length; i++)
        {
            ConsoleColor color = GetColor(userGuess[i],codeWord,i);
            Console.ForegroundColor = color;
            Console.Write(userGuess[i]);
            Console.ForegroundColor = ConsoleColor.White;            
        }
    }
    Console.WriteLine($"\nCongrats you solved it in {numGuesses}\n");
    Console.Write("Would you care to play again? ");
    char playAgain = Console.ReadKey(true).KeyChar;
    if(playAgain == 'y' || playAgain == 'Y')
        MasterMind();
    static void DisplayTheRules()
    {
        Console.Clear();
        string rules = @"
        We will generate a random word with no duplicate letters. Your job will be
        to guess that word in as few guesses as possible.
        
        Press any key to continue.";
        Console.Write(rules);
        Console.ReadKey(true);
        Console.Clear();
     }
    static string GenerateAWord()
    {
        string code ="";
        Random rand = new Random();
        int codeLength = GetCodeLength();
        for (int i = 0; i< codeLength; i ++)
        {
            bool successful = false;
            while (!successful)
            {
                char letter = (char)rand.Next(97,97+5+codeLength);
                if (code.Length == 0)
                    {
                        code += letter;
                        successful = true;
                    }
                else
                {
                    bool repeat = false;
                    for (int j = 0; j<code.Length;j++)
                    {
                        if (letter == code[j] )
                            repeat = true;
                    }
                    if (repeat == false)
                    {
                        code += letter;
                        successful = true;
                    }
                    
                }

            }
        }
        return code;

        static int GetCodeLength()
    {
        string explain = @"
        Please select a level of difficulty
        
        1) Easy (four letters)
        2) Normal (five letters)
        3) Hard (six letters)";
        Console.Clear();
        Console.Write(explain);
        char choice = Console.ReadKey(true).KeyChar;
        if(choice == '1')
            return 4;
        else if (choice == '3')
            return 6;
        else    
            return 5;
    }
    }
    static string ValidateUserGuess(int numGuesses,int size)
    {
        Console.WriteLine();
        Console.Write($"Please enter guess number {numGuesses} ");
        string? userGuess = Console.ReadLine();
        userGuess = userGuess.ToLower();
        if(userGuess.Length != size)
            Console.WriteLine($"Your guess must contain exactly {size} letters.");
        else
        {
            for (int i = 0; i<size; i ++)
            {
                // Check for acceptable letters
                if(!"abcdefghijklmnopqrstuvwxyz".Contains(userGuess[i]))
                {
                    Console.WriteLine($"Your guess must contain only letters");
                    break;
                }
                else
                {
                    int frequency = 0;
                    for (int j = 0; j<size; j++)
                    {
                        if(userGuess[i] == userGuess[j])
                            frequency ++;
                    }
                    if (frequency == 1)
                        return userGuess;
                    else
                    {
                        Console.WriteLine("You can only use each letter once in your guess");
                        break;
                    }
                }
            }
        }
        return userGuess;
    }
    static ConsoleColor GetColor(char letter, string code,int position)
    {
        for(int i = 0; i<code.Length; i++)
        {
            if(code[i] == letter && i == position)
                return ConsoleColor.Green;
            if(code[i] == letter)
                return ConsoleColor.Red;
        }
        return ConsoleColor.White;
    }
}
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
        if(successful)
            return x;        
    }
    return 0;
}