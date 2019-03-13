using UnityEngine;
using System.Collections;

public class Hacker : MonoBehaviour
{
    // Game configuration data
    // Password arrays should always be lower case
    System.Random randomIndex = new System.Random();
    string password;
    string[] easy = { "apple", "orange", "grape", "kiwi", "lemon", "melon" };
    string[] medium = { "artichoke", "broccoli", "asparagus", "potatoe", "spinach", "tomato" };
    string[] hard = { "casserole", "lasagna", "souffle", "meringue", "meatloaf", "ravioli" };
    string[] secret = { "chocolate", "strawberry", "vanilla", "cappuccino", "pistachio", "sorbet" };

    // Game State
    int level;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }

    void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;
        level = 0;
        password = "";
        Terminal.ClearScreen();
        Terminal.WriteLine(@"
                  __
      ___  ____ _/  |_   ______
   _ / __ \\__  \\   __\/  ___/
   \  ___/ / __ \|  |  \___ \ 
    \___  >____  /__| /____  >
        \/     \/          \/ 
       ");
        Terminal.WriteLine("Guess the anagram, hack the food!:");
        Terminal.WriteLine("Press 1 for Fruit Stand (easy)");
        Terminal.WriteLine("Press 2 for Farmers Market (medium)");
        Terminal.WriteLine("Press 3 for Takeout Diner (hard)");
        Terminal.WriteLine("Type \"exit\" to end the game");
    }

    void OnUserInput(string input)
    {

        if (input.ToLower() == "menu")
        {
            ShowMainMenu();
        }
        else if (input.ToLower() == "exit")
        {
            StartCoroutine(QuitGame());
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
        else if (currentScreen == Screen.Win)
        {
            ShowMainMenu();
        }
        else
        {
            RunMainMenu(input);
        }
    }

    private void RunMainMenu(string input)
    {
        bool isValidLevelNumber = ((input == "1") || (input == "2") || (input == "3"));
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            LoadLevel();
        }
        // TODO: Handle the secret level with better logic
        else if (input.ToLower() == "secret")
        {
            level = 4;
            LoadLevel();
        }
        else
        {
            Terminal.WriteLine("Please press \"1\", \"2\" or \"3\":");
        }
    }

    void LoadLevel()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        switch (level)
        {
            case (1):
                password = easy[randomIndex.Next(easy.Length)];
                break;
            case (2):
                password = medium[randomIndex.Next(medium.Length)];
                break;
            case (3):
                password = hard[randomIndex.Next(hard.Length)];
                break;
            case (4):
                password = secret[randomIndex.Next(secret.Length)];
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }
        Terminal.WriteLine("Enter password (hint, \"" + password.Anagram() + "\"):");
    }

    void CheckPassword(string passwordGuess)
    {
        if (passwordGuess == password)
        {
            DisplayWinScreen();
        }
        else
        {
            Terminal.ClearScreen();
            Terminal.WriteLine("Password incorrect");
            Terminal.WriteLine("Return to main \"menu\", \"exit\"");
            Terminal.WriteLine("or try again (hint, \"" + password.Anagram() + "\"):");
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        ShowLevelReward();
    }

    void ShowLevelReward()
    {
        Terminal.ClearScreen();
        switch (level)
        {
            case (1):
                Terminal.WriteLine(@"
               CORRECT
      You win some cherries :)
                 \
                  |<>
                _/ \_
               (_) (_)
                ");
                break;
            case (2):
                Terminal.WriteLine(@"
               CORRECT
         You win a carrot :)
                _\/_   
               ( ___)   
                \ _/
                 \/
                ");
                break;
            case (3):
                Terminal.WriteLine(@"
               CORRECT
         You win a burger :)
                _____
               /_____\
              (_______)
               \_____/
                ");
                break;
            case (4):
                Terminal.WriteLine(@"
               CORRECT
       You win a slice of pie :)
 
                OOOOOO
                |   /\
                |  / /
                | / /
                |/ /
                 \/
                ");
                break;
            default:
                Debug.LogError("Invalid level reached");
                break;
        }
        Terminal.WriteLine("Press \"enter\" for a greater challenge!");
    }

    // WaitForSecondsRealtime() requires an IEnumerator return value
    IEnumerator QuitGame()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine(@"    Thank you for playing, goodbye!

    __________                ._.
    \______   \___.__. ____   | |
     |    |  _<   |  |/ __ \  | |
     |    |   \\___  \  ___/   \|
     |______  // ____|\___  >  __
            \/ \/         \/   \/
        ");
        yield return new WaitForSecondsRealtime(2);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }
}
