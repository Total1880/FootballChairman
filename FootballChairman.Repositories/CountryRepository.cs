using FootballChairman.Models;
using System.Xml;

namespace FootballChairman.Repositories
{
    public class CountryRepository : RepositoryBase, IRepository<Country>
    {
        private readonly string file = $"Countries.xml";

        public CountryRepository()
        {
            CreateFiles(file);
        }

        public IList<Country> Create(IList<Country> itemList)
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(IList<Country>));

                foreach (var country in itemList)
                {
                    writer.WriteStartElement(nameof(Country));
                    writer.WriteAttributeString(nameof(Country.Id), country.Id.ToString());
                    writer.WriteAttributeString(nameof(Country.Name), country.Name.ToString());
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                writer.Flush();
            }

            using (StreamWriter streamWriter = File.CreateText(Path.Combine(path, file)))
            {
                streamWriter.Write(stream);
            }

            return itemList;
        }

        public IList<Country> Get()
        {
            return (Get(file));
        }

        private IList<Country> Get(string filename)
        {
            var countryList = new List<Country>();
            var fileString = File.ReadAllText(Path.Combine(path, filename));

            if (string.IsNullOrWhiteSpace(fileString))
            {
                return countryList;
            }

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement(nameof(IList<Country>));

                    do
                    {
                        var readCountry = new Country();

                        readCountry.Id = int.Parse(xmlReader.GetAttribute(nameof(Country.Id)));
                        readCountry.Name = xmlReader.GetAttribute(nameof(Country.Name));

                        countryList.Add(readCountry);
                    } while (xmlReader.ReadToNextSibling(nameof(Country)));
                }
            }

            return countryList;
        }
    }
}
