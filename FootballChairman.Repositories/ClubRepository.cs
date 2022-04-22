using FootballChairman.Models;
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
            itemList = itemList.OrderBy(x => x.Name).ToList();
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
                    writer.WriteAttributeString(nameof(Club.ManagerId), club.ManagerId.ToString());
                    writer.WriteAttributeString(nameof(Club.CountryId), club.CountryId.ToString());
                    writer.WriteAttributeString(nameof(Club.IsPlayer), club.IsPlayer.ToString());
                    writer.WriteAttributeString(nameof(Club.Reputation), club.Reputation.ToString());
                    writer.WriteAttributeString(nameof(Club.Budget), club.Budget.ToString());
                    writer.WriteAttributeString(nameof(Club.YouthFacilities), club.YouthFacilities.ToString());
                    writer.WriteAttributeString(nameof(Club.TrainingFacilities), club.TrainingFacilities.ToString());
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
                        readClub.ManagerId = int.Parse(xmlReader.GetAttribute(nameof(Club.ManagerId)));
                        readClub.CountryId = int.Parse(xmlReader.GetAttribute(nameof(Club.CountryId)));
                        readClub.IsPlayer = bool.Parse(xmlReader.GetAttribute(nameof(Club.IsPlayer)));
                        readClub.Reputation = int.Parse(xmlReader.GetAttribute(nameof(Club.Reputation)));
                        readClub.Budget = float.Parse(xmlReader.GetAttribute(nameof(Club.Budget)));
                        readClub.YouthFacilities = int.Parse(xmlReader.GetAttribute(nameof(Club.YouthFacilities)));
                        readClub.TrainingFacilities = int.Parse(xmlReader.GetAttribute(nameof(Club.TrainingFacilities)));

                        clubList.Add(readClub);
                    } while (xmlReader.ReadToNextSibling(nameof(Club)));
                }
            }

            return clubList;
        }
    }
}
