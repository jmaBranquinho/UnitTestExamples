using System.IO;
using System.Xml;
using System.Xml.Linq;
using Xunit;

namespace DataCreationExamples;

public class XmlExamples
{
    [Fact]
    public void LoadXmlFromFile()
    {
        // you can edit the xml file in the editor, load it like this and apply it to the unit tests 
        // make sure the file is being copied on build
        var xml = XElement.Load(@"someXMl.xml");
        ; // place a breakpoint and check the xml
    }
    
    [Fact]
    public void LoadXmlFromString()
    {
        // this xml could be in a string variable or loaded from file
        var xmlAsString = File.ReadAllText(@"someXMl.xml");

        var conversion = new XmlDocument();
        conversion.LoadXml(xmlAsString);
        var xml = conversion.InnerXml;
        ; // place a breakpoint and check the xml
    }
}