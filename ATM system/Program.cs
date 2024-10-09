internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Welcome, please enter PIN to get started! Or write PIN to set up a new PIN");

        // Input might be null, check for null values
        var inputPIN = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(inputPIN)){
            Console.WriteLine("Oops, nothing was written");
        }

        if(inputPIN == "PIN"){
            Console.WriteLine("Please enter your new PIN");
            
            // New Pin can't be empty
            var newPIN = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(newPIN)){
                Console.WriteLine("Nothing was set");
            }
        }
    }
}