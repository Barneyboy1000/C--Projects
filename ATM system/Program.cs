using System.IO;
using System.Reflection;
using System.Diagnostics;
internal class Program
{
    private static void Main(string[]? args)
    {
        // Read data file of PINs from a relative path
        PINClass.SetCsvPath(@"..\..\..\PINData.csv");

        Console.WriteLine("Welcome, please enter PIN to get started! Or write PIN to set up a new PIN");

        // Reading the PIN values from csv file
        List<string> pinsToStringList = PINClass.PinAccess();

        // User should only be given 3 attempts to input pin
        int count = 0;
        while(count < 3)
        {
            // check for null values
            string? inputPIN = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(inputPIN)){
                Console.WriteLine("Oops, nothing was written");
                ApplicationRestart();
            }

            // User wants to add new PIN
            if(inputPIN == "PIN"){
                Console.WriteLine("Please enter your new PIN");
            
                // New Pin can't be empty
                var newPIN = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(newPIN)){
                    Console.WriteLine("Nothing was set");
                    ApplicationRestart();
                }
                
                // TODO add new pin
                break;
            }

            // PIN is valid if in list, null is already checked
            if(pinsToStringList.Contains(inputPIN)){
                int associatedFunds = PINClass.GetFundsFromPin(inputPIN);
                Console.WriteLine(@$"Welcome to your accout, 
                                  you balance is {associatedFunds},
                                  enter deposit amount,
                                  This Machine Holds £50, £20, £10 and £5 notes");
                break;
                
            }else{
                Console.WriteLine("PIN is invalid");
                count += 1;
            }
        }

        // Restart application if 3 attempts occur
        if (count >= 3){
            ApplicationRestart();
        }

        // Prevent infinite recursive loop
        Environment.Exit(0);
    }

    // Full application restart is overkill, instead just re-run main
    private static void ApplicationRestart(){
        Console.WriteLine("Please try again... Restarting");
        Thread.Sleep(1000);
        Main(null);
    }

    // Small Algorithm to determine how many notes are needed for an input
    // Returns a neat string of the notes dispensing
    public static string CashExchange(int amountToDispense){
        int[] notesArray = [50, 20, 10, 5];
        string output = "Dispensing :";
        
        foreach(int notes in notesArray){
            int currentNote = amountToDispense/notes;
            if(currentNote > 0){
                output += $" {currentNote} £{notes}s";
            }else{
                continue;
            }
            amountToDispense -= currentNote*notes;
        }

        return output;
    }
}