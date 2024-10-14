using System.IO;
using System.Reflection;
using System.Diagnostics;
internal class Program
{
    private static void Main(string[] args)
    {
        // Read dummy file of PINs for comparison
        var path = "C:\\Users\\archb\\OneDrive\\Documents\\C# Projects\\ATM system\\PINData.txt";

        Console.WriteLine("Welcome, please enter PIN to get started! Or write PIN to set up a new PIN");

        // Input might be null, check for null values
        string? inputPIN = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(inputPIN)){
            Console.WriteLine("Oops, nothing was written");
            ApplicationRestart();
        }

        if(inputPIN == "PIN"){
            Console.WriteLine("Please enter your new PIN");
            
            // New Pin can't be empty
            var newPIN = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(newPIN)){
                Console.WriteLine("Nothing was set");
                ApplicationRestart();
            }
        }

        // Reading the PIN values from the file of absolute path
        List<string> pinsToStringList = [.. File.ReadAllLines(path)];

        // The inputed string needs to be in the list, we can safely assume not null
        if(pinsToStringList.Contains(inputPIN)){
            Console.WriteLine("You have £50 in your account");
        }else{
            Console.WriteLine("PIN is invalid");
            ApplicationRestart();
        }

        Environment.Exit(0);
    }

    private static void ApplicationRestart(){
        Console.WriteLine("Please try again... Restarting");
        Thread.Sleep(1000);
        string[] strings = [];
        Main(strings);
    }
}