// using System.Xml;
// using System.Xml.Linq;
// using static EarthQuakeData.Utils;
//
// namespace EarthQuakeData;
//
// public class XmlConverter : IDataConverter
// {
//     public void XmlConversion(dynamic data)
//     {
//         XElement xml = new XElement("root");
//
//         AddJsonToXml(xml, data);
//
//         XmlDocument doc = new XmlDocument();
//         doc.LoadXml(xml.ToString());
//         doc.Save("../../../test.xml");
//     }
//
//     public void Convert(dynamic data, string type = "xml")
//     {
//         if (type == "xml")
//         {
//             XmlConversion(data);
//         }
//     }
//
//     
// }