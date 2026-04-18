namespace XsltEmployeeWpf.Services
{
    public interface IXmlTransformService
    {
        void Transform(string inputXmlPath, string xsltPath, string outputXmlPath);
    }
}