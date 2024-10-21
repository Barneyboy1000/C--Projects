using System.IO;
using System.Reflection;
using System.Diagnostics;
internal class Program
{
    private static void Main(string[]? args)
    {
        // Read dummy file of PINs for comparison
        var path = "C:\\Users\\archb\\OneDrive\\Documents\\C# Projects\\ATM system\\PINData.csv";

        Console.WriteLine("Welcome, please enter PIN to get started! Or write PIN to set up a new PIN");

        // Reading the PIN values from the file of absolute path
        List<string> pinsToStringList = PINClass.PinAccess(path);

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
                break;
            }

            // PIN is valid if in list, null is already checked
            if(pinsToStringList.Contains(inputPIN)){
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

        // TODO add new pin

        // TODO PIN Valid

        // Prevent infinite recursive loop
        Environment.Exit(0);
    }

    // Full application restart is overkill, instead just re-run main
    private static void ApplicationRestart(){
        Console.WriteLine("Please try again... Restarting");
        Thread.Sleep(1000);
        Main(null);
    }
}