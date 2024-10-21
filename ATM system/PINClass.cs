using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

class PINClass{
    public string Pin {get; set;}
    public decimal Funds {get; set;}

    public static List<string> PinAccess(string filePath){
        List<string> listOfStrings = [];

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false
        };
        using (StreamReader streamReader = new(filePath))
        using (CsvReader csvReader = new(streamReader, config))
        {
            IEnumerable<PINClass> records = csvReader.GetRecords<PINClass>();

            foreach(PINClass pin in records){
                listOfStrings.Add(pin.Pin);
            }
        }

        return listOfStrings;
    }
}