namespace EarthQuakeData;

public class CommandFactory
{
    public static ICommand? CreateCommand(CommandTypes commandType, object[] args)
    {
        string commandClassName = $"EarthQuakeData.{commandType}";
        
        Type commandClassType = Type.GetType(commandClassName);
        
        if (commandClassType == null)
        {
            throw new ArgumentException($"No command found for {commandType}");
        }

        return Activator.CreateInstance(commandClassType, args) as ICommand;
    }
}