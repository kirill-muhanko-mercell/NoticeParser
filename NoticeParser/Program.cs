using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NoticeParser
{
    internal static class Program
    {
        private const string FileName = "2022-OJS087-234645.xml";
        private const string ContractingBodyXmlPath = "/TED_EXPORT/FORM_SECTION/F02_2014/CONTRACTING_BODY/ADDRESS_CONTRACTING_BODY";

        public static void Main(string[] args)
        {
            var xmlDocument = new XmlDocument();
            var filePath = Path.Combine(Environment.CurrentDirectory, FileName);
            xmlDocument.Load(filePath);

            var contractingBodyXmlNode = xmlDocument.SelectSingleNode(ContractingBodyXmlPath);
            var model = GetModelFromXmlNode<AddressContractingBodyModel>(contractingBodyXmlNode);
        }

        private static T GetModelFromXmlNode<T>(XmlNode xmlNode) where T: class
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var stringReader = new StringReader(xmlNode.OuterXml))
            {
                return xmlSerializer.Deserialize(stringReader) as T;
            }
        }
    }

    [XmlRoot("ADDRESS_CONTRACTING_BODY")]
    public class AddressContractingBodyModel
    {
        [XmlElement("OFFICIALNAME")]
        public string OfficialName;

        [XmlElement("ADDRESS")]
        public string Address;

        [XmlElement("TOWN")]
        public string Town;

        [XmlElement("POSTAL_CODE")]
        public string PostalCode;
    }
}