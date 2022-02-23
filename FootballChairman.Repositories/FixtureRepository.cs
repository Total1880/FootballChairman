using FootballChairman.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace FootballChairman.Repositories
{
    public class FixtureRepository : RepositoryBase, IRepository<Fixture>
    {
        private readonly string file = $"Fixtures.xml";

        public FixtureRepository()
        {
            CreateFiles(file);
        }
        public IList<Fixture> Create(IList<Fixture> itemList)
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(IList<Fixture>));

                foreach (var fixture in itemList)
                {
                    writer.WriteStartElement(nameof(Fixture));
                    writer.WriteAttributeString(nameof(Fixture.RoundNo), fixture.RoundNo.ToString());
                    writer.WriteAttributeString(nameof(Fixture.MatchNo), fixture.MatchNo.ToString());
                    writer.WriteAttributeString(nameof(Fixture.HomeOpponentId), fixture.HomeOpponentId.ToString());
                    writer.WriteAttributeString(nameof(Fixture.AwayOpponentId), fixture.AwayOpponentId.ToString());
                    writer.WriteAttributeString(nameof(Fixture.HomeOpponent), fixture.HomeOpponent.ToString());
                    writer.WriteAttributeString(nameof(Fixture.AwayOpponent), fixture.AwayOpponent.ToString());
                    writer.WriteAttributeString(nameof(Fixture.CompetitionId), fixture.CompetitionId.ToString());
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

        public IList<Fixture> Get()
        {
            return (Get(file));
        }

        private IList<Fixture> Get(string filename)
        {
            var fixtureList = new List<Fixture>();
            var fileString = File.ReadAllText(Path.Combine(path, filename));

            if (string.IsNullOrWhiteSpace(fileString))
            {
                return fixtureList;
            }

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement(nameof(IList<Fixture>));

                    do
                    {
                        var readFixture = new Fixture();

                        readFixture.RoundNo = int.Parse(xmlReader.GetAttribute(nameof(Fixture.RoundNo)));
                        readFixture.MatchNo = int.Parse(xmlReader.GetAttribute(nameof(Fixture.MatchNo)));
                        readFixture.HomeOpponentId = int.Parse(xmlReader.GetAttribute(nameof(Fixture.HomeOpponentId)));
                        readFixture.AwayOpponentId = int.Parse(xmlReader.GetAttribute(nameof(Fixture.AwayOpponentId)));
                        readFixture.HomeOpponent = xmlReader.GetAttribute(nameof(Fixture.HomeOpponent));
                        readFixture.AwayOpponent = xmlReader.GetAttribute(nameof(Fixture.AwayOpponent));
                        readFixture.CompetitionId = int.Parse(xmlReader.GetAttribute(nameof(Fixture.CompetitionId)));

                        fixtureList.Add(readFixture);
                    } while (xmlReader.ReadToNextSibling(nameof(Fixture)));
                }
            }

            return fixtureList;
        }


    }
}
