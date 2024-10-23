using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

class PINClass{
    public static string Path {get; set;}
    public string Pin {get; set;}
    public int Funds {get; set;}

    public static void SetCsvPath(string path){
        Path = path;
    }

    /*
        How to access the data in the CSV files
        Uses: CSV Helper NuGet Package
        Can be Found: https://www.nuget.org/packages/CsvHelper

        Steps:
        - Set up config to inform no header in file (Automatic assumption the file is formatted into 'Pin', 'Funds')
        - Set up Stream Reader to Read each line of the CSV
        - The Stream Reader and config is used to instansiate the CSVReader
        - An Enumerable of formatted 'Pin', 'Funds' is created according with each record in the CSV File
    */

    // Access the CSV file to check Pins that are valid
    public static List<string> PinAccess(){
        List<string> listOfStrings = [];

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false
        };
        using StreamReader streamReader = new(Path);
        using CsvReader csvReader = new(streamReader, config);
        IEnumerable<PINClass> records = csvReader.GetRecords<PINClass>();

        foreach(PINClass pin in records){
            listOfStrings.Add(pin.Pin);
        }

        return listOfStrings;
    }

    // Fetches the funds from the associated pin
    public static int GetFundsFromPin(string associatedPin){
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false
        };
        using StreamReader streamReader = new(Path);
        using CsvReader csvReader = new(streamReader, config);
        IEnumerable<PINClass> records = csvReader.GetRecords<PINClass>();

        foreach (PINClass pin in records)
        {
            if (pin.Pin == associatedPin)
            {
                return pin.Funds;
            }
        }
        return 0;
    }
}