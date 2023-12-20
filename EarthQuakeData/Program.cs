using EarthQuakeData;
using RestSharp;

while (true){
    try
    {
        RestClient httpClient = new RestClient();
        dynamic format;
        // Assuming the user input is mapped to the enum correctly
        Console.WriteLine($"Available commands: ");
        foreach (var value in Enum.GetValues(typeof(CommandTypes)))
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        Console.Write("\nEnter your command: ");
        string userInput = Console.ReadLine() ?? throw new InvalidOperationException();

        Console.WriteLine("\nChoose your format: ");
        string userFormat = Console.ReadLine().ToString().Trim().ToLower() ?? throw new InvalidOperationException();
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
            // Successful parsing, userCommand now holds the enum value.
            // You can now use userCommand in your logic.
            ICommand command = CommandFactory.CreateCommand(userCommand,
                new object[] { httpClient, format })!;
            command.Execute();
        }
        else
        {
            // Parsing failed, handle the error, e.g., by informing the user.
            Console.WriteLine("Invalid command.");
        }
        // This would typically come from user input
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}