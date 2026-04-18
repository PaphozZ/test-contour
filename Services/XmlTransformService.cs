using System.Xml.Xsl;

namespace XsltEmployeeWpf.Services
{
    public class XmlTransformService : IXmlTransformService
    {
        public void Transform(string inputXmlPath, string xsltPath, string outputXmlPath)
        {
            var xslt = new XslCompiledTransform();
            xslt.Load(xsltPath);
            xslt.Transform(inputXmlPath, outputXmlPath);
        }
    }
}