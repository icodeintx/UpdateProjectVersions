using System.Reflection.Metadata;
using System.Xml;

class Program
{
    static void Main(string[] args)
    {
        string projectFile = args[0];
        if (args.Length > 0 && File.Exists(projectFile))
        {
            Console.WriteLine("Project: " + projectFile);

            try
            {
                var document = new XmlDocument();
                document.Load(projectFile);

                
                string baseXPath = "/Project/PropertyGroup/Version";
                XmlNode node;

                if (document != null)
                {
                    node = document.SelectSingleNode(baseXPath);

                    if (node != null)
                    {
                        string[] versionStringSplit = node.InnerText.Split('.');

                        int[] versionSplit = ConvertStringArray(versionStringSplit);

                        //if year/month/day are current update only last value
                        if (versionSplit[0] == DateTime.Now.Year && versionSplit[1] == DateTime.Now.Month && versionSplit[2] == DateTime.Now.Day)
                        {
                            //only update last element
                            versionSplit[3]++;
                        }
                        else
                        {
                            //update all elements
                            versionSplit[0] = DateTime.Now.Year;
                            versionSplit[1] = DateTime.Now.Month;
                            versionSplit[2] = DateTime.Now.Day;
                            versionSplit[3] = 1;
                        }

                        //create new version number
                        node.InnerText = $"{versionSplit[0].ToString()}.{versionSplit[1].ToString()}.{versionSplit[2].ToString()}.{versionSplit[3].ToString()}";
                    }
                    else
                    {
                        //Version node was not found so create one if PropertyGroup exists
                        string propertyGroup = "/Project/PropertyGroup";
                        XmlNode pgNode = document.SelectSingleNode(propertyGroup);
                        if (pgNode != null)
                        {
                            //create new element
                            XmlElement versionElement = document.CreateElement("Version");
                            versionElement.InnerText = $"{DateTime.Now.Year}.{DateTime.Now.Month}.{DateTime.Now.Day}.1";
                            pgNode.AppendChild(versionElement);
                        }
                        

                    }

                    document.Save(projectFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to update version number:  Error: {ex.Message}");
            }
        }
    }

    private static int[] ConvertStringArray(string[] versionSplit)
    {
        try
        {
            int[] result = new int[versionSplit.Length];

            for (int i = 0; i < versionSplit.Length; i++)
            {
                result[i] = Convert.ToInt32(versionSplit[i]);
            }
            return result;
        }
        catch
        {
            return new int[] { 0, 0, 0, 0 };
        }
        
    }
}