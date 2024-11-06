using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.ComponentModel;
using System.Text.RegularExpressions;
internal partial class Program
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
            // check for null values and PIN format
            var inputPIN = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(inputPIN) || !PINRegex().IsMatch(inputPIN)){
                Console.WriteLine("Oops, nothing was written or not 4 digits for a pin");
                ApplicationRestart();
            }

            // User wants to add new PIN
            if(inputPIN == "PIN"){
                Console.WriteLine("Please enter your new PIN");
            
                // New Pin can't be empty or not a PIN Format
                var newPIN = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(newPIN)|| !PINRegex().IsMatch(newPIN)){
                    Console.WriteLine("Nothing was set or doesn't match a 4 digit pin");
                    ApplicationRestart();
                }
                
                // TODO add new pin
                if(PINClass.PinExists(newPIN)){
                    Console.WriteLine("PIN already exists");
                    ApplicationRestart();
                }else{
                    PINClass.AddPin(newPIN);
                    Console.WriteLine("new PIN added");
                    break;
                }
            }

            // PIN is valid if in list, null is already checked
            if(pinsToStringList.Contains(inputPIN)){
                int associatedFunds = PINClass.GetFundsFromPin(inputPIN);
                Console.WriteLine("Welcome to your accout,\n"+ 
                                  $"you balance is £{associatedFunds},\n"+
                                  "enter deposit amount,\n"+
                                  "This Machine Holds £50, £20, £10 and £5 notes");
                var moneyToDispense = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(moneyToDispense) || Convert.ToInt16(moneyToDispense) < 0){
                    Console.WriteLine("Invalid input, needs to be a positive number");
                    ApplicationRestart();
                }else{
                    Console.WriteLine(CashExchange(Convert.ToInt16(moneyToDispense)));
                    break;
                }
                
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

    [GeneratedRegex("^[0-9]{4}$")]
    private static partial Regex PINRegex();
}