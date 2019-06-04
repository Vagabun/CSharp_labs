using System.Xml.Serialization;
using System.IO;

namespace XMLProject {
    class Program {
        static void Main(string[] args) {
            XmlSerializer formatter = new XmlSerializer(typeof(Company));
            Company company = new Company();

            string path = "db.xml";
            if (File.Exists(path)) {
                using (FileStream fs = File.OpenRead(path)) {
                    company = (Company)formatter.Deserialize(fs);
                }
                File.Delete(path);
            }

            Menu.MainMenu(ref company);

            using (FileStream fs = File.Create(path)) {
                formatter.Serialize(fs, company);
            }

        }
    }
}
