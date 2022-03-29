using FootballChairman.Models;
using FootballChairman.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FootballChairman.Repositories
{
    public class CompetitionCupRepository : RepositoryBase, IRepository<CompetitionCup>
    {
        private readonly string file = $"CompetitionCup.xml";

        public CompetitionCupRepository()
        {
            CreateFiles(file);
        }

        public IList<CompetitionCup> Create(IList<CompetitionCup> itemList)
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(IList<Competition>));

                foreach (var competition in itemList)
                {
                    writer.WriteStartElement(nameof(CompetitionCup));
                    writer.WriteAttributeString(nameof(CompetitionCup.Id), competition.Id.ToString());
                    writer.WriteAttributeString(nameof(CompetitionCup.Name), competition.Name.ToString());
                    writer.WriteAttributeString(nameof(CompetitionCup.Reputation), competition.Reputation.ToString());
                    writer.WriteAttributeString(nameof(CompetitionCup.CountryId), competition.CountryId.ToString());
                    writer.WriteAttributeString(nameof(CompetitionCup.CompetitionType), competition.CompetitionType.ToString());
                    writer.WriteAttributeString(nameof(CompetitionCup.Rounds), competition.Rounds.ToString());
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

        public IList<CompetitionCup> Get()
        {
            return (Get(file));
        }

        private IList<CompetitionCup> Get(string filename)
        {
            var competitionList = new List<CompetitionCup>();
            var fileString = File.ReadAllText(Path.Combine(path, filename));

            if (string.IsNullOrWhiteSpace(fileString))
            {
                return competitionList;
            }

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement(nameof(IList<CompetitionCup>));

                    do
                    {
                        var readCompetition = new CompetitionCup();

                        readCompetition.Id = int.Parse(xmlReader.GetAttribute(nameof(CompetitionCup.Id)));
                        readCompetition.Name = xmlReader.GetAttribute(nameof(CompetitionCup.Name));
                        readCompetition.Reputation = int.Parse(xmlReader.GetAttribute(nameof(CompetitionCup.Reputation)));
                        readCompetition.CountryId = int.Parse(xmlReader.GetAttribute(nameof(CompetitionCup.CountryId)));
                        readCompetition.CompetitionType = (CompetitionType)Enum.Parse(typeof(CompetitionType), xmlReader.GetAttribute(nameof(CompetitionCup.CompetitionType)));
                        readCompetition.Rounds = int.Parse(xmlReader.GetAttribute(nameof(CompetitionCup.Rounds)));

                        competitionList.Add(readCompetition);
                    } while (xmlReader.ReadToNextSibling(nameof(CompetitionCup)));
                }
            }

            return competitionList;
        }
    }
}
