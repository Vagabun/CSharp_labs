using System.Xml.Serialization;
using System.IO;

namespace XMLProject {
    class Program {
        static void Main(string[] args) {
            XmlSerializer formatter = new XmlSerializer(typeof(Company));
            Company company = new Company();

            string path = "db.xml";
            if (File.Exists(path))
                using (FileStream fs = File.OpenRead(path))
                    company = (Company)formatter.Deserialize(fs);

            Menu.MainMenu(ref company);

            using (StreamWriter sw = new StreamWriter(path, false))
                formatter.Serialize(sw, company);
        }
    }
}
