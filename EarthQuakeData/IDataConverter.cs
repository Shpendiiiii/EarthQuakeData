namespace EarthQuakeData;

//Implementor
//Classes that will do the conversion will use it
public interface IDataConverter
{
    void Convert(dynamic data);
}