using EarthQuakeData;
using RestSharp;

while (true){
    try
    {
        RestClient httpClient = new RestClient();
        dynamic format;
        string userInput;
        // Assuming the user input is mapped to the enum correctly
        Console.WriteLine($"\nAvailable commands: ");
        foreach (var value in Enum.GetValues(typeof(CommandTypes)))
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        Console.Write("\nEnter your command: ");
        userInput = Console.ReadLine();


        Console.Write("\nChoose your format: ");
        string userFormat = Console.ReadLine();

        if (userFormat == "xml")
        {
            format = new XmlDataConverter();
        }
        else
        {
            format = new YmlDataConverter();
        }

        if (Enum.TryParse(userInput, out CommandTypes userCommand))
        {
            // Successful parsing, userCommand now holds the
            ICommand command = CommandFactory.CreateCommand(userCommand,
                new object[] { httpClient, format })!;
            command.Execute();
        }
        else
        {
            // Parsing failed, handle the error, e.g., by informing the user.
            Console.WriteLine("Invalid command.");
        }
        // This comes from user input
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}