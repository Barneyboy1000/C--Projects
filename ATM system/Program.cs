using System.IO;
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
            Environment.Exit(0);
        }

        if(inputPIN == "PIN"){
            Console.WriteLine("Please enter your new PIN");
            
            // New Pin can't be empty
            var newPIN = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(newPIN)){
                Console.WriteLine("Nothing was set");
                Environment.Exit(0);
            }
        }

        // Reading the PIN values from the file of absolute path
        List<string> pinsToStringList = [.. File.ReadAllLines(path)];

        // The inputed string needs to be in the list, we can safely assume not null
        if(pinsToStringList.Contains(inputPIN)){
            Console.WriteLine("You have £50 in your account");
        }else{
            Console.WriteLine("PIN is invalid");
        }
    }
}