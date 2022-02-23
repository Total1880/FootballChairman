using FootballChairman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FootballChairman.Repositories
{
    public class ClubRepository : RepositoryBase, IRepository<Club>
    {
        private readonly string file = $"Clubs.xml";

        public ClubRepository()
        {
            CreateFiles(file);
        }
        public IList<Club> Create(IList<Club> itemList)
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(IList<Fixture>));

                foreach (var club in itemList)
                {
                    writer.WriteStartElement(nameof(Club));
                    writer.WriteAttributeString(nameof(Club.Id), club.Id.ToString());
                    writer.WriteAttributeString(nameof(Club.Name), club.Name.ToString());
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

        public IList<Club> Get()
        {
            return (Get(file));
        }

        private IList<Club> Get(string filename)
        {
            var clubList = new List<Club>();
            var fileString = File.ReadAllText(Path.Combine(path, filename));

            if (string.IsNullOrWhiteSpace(fileString))
            {
                return clubList;
            }

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement(nameof(IList<Club>));

                    do
                    {
                        var readClub = new Club();

                        readClub.Id = int.Parse(xmlReader.GetAttribute(nameof(Club.Id)));
                        readClub.Name = xmlReader.GetAttribute(nameof(Club.Name));

                        clubList.Add(readClub);
                    } while (xmlReader.ReadToNextSibling(nameof(Club)));
                }
            }

            return clubList;
        }
    }
}
