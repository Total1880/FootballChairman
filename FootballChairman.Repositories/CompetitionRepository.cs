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
    public class CompetitionRepository : RepositoryBase, IRepository<Competition>
    {
        private readonly string file = $"Competitions.xml";

        public CompetitionRepository()
        {
            CreateFiles(file);
        }

        public IList<Competition> Create(IList<Competition> itemList)
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(IList<Competition>));

                foreach (var competition in itemList)
                {
                    writer.WriteStartElement(nameof(Competition));
                    writer.WriteAttributeString(nameof(Competition.Id), competition.Id.ToString());
                    writer.WriteAttributeString(nameof(Competition.Name), competition.Name.ToString());
                    writer.WriteAttributeString(nameof(Competition.Reputation), competition.Reputation.ToString());
                    writer.WriteAttributeString(nameof(Competition.PromotionCompetitionId), competition.PromotionCompetitionId.ToString());
                    writer.WriteAttributeString(nameof(Competition.RelegationCompetitionId), competition.RelegationCompetitionId.ToString());
                    writer.WriteAttributeString(nameof(Competition.NumberOfTeams), competition.NumberOfTeams.ToString());
                    writer.WriteAttributeString(nameof(Competition.CountryId), competition.CountryId.ToString());
                    writer.WriteAttributeString(nameof(Competition.CompetitionType), competition.CompetitionType.ToString());
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

        public IList<Competition> Get()
        {
            return (Get(file));
        }

        private IList<Competition> Get(string filename)
        {
            var competitionList = new List<Competition>();
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
                    xmlReader.ReadStartElement(nameof(IList<Competition>));

                    do
                    {
                        var readCompetition = new Competition();

                        readCompetition.Id = int.Parse(xmlReader.GetAttribute(nameof(Competition.Id)));
                        readCompetition.Name = xmlReader.GetAttribute(nameof(Competition.Name));
                        readCompetition.Reputation = int.Parse(xmlReader.GetAttribute(nameof(Competition.Reputation)));
                        readCompetition.PromotionCompetitionId = int.Parse(xmlReader.GetAttribute(nameof(Competition.PromotionCompetitionId)));
                        readCompetition.RelegationCompetitionId = int.Parse(xmlReader.GetAttribute(nameof(Competition.RelegationCompetitionId)));
                        readCompetition.NumberOfTeams = int.Parse(xmlReader.GetAttribute(nameof(Competition.NumberOfTeams)));
                        readCompetition.CountryId = int.Parse(xmlReader.GetAttribute(nameof(Competition.CountryId)));
                        readCompetition.CompetitionType = (CompetitionType)Enum.Parse(typeof(CompetitionType), xmlReader.GetAttribute(nameof(Competition.CompetitionType)));

                        competitionList.Add(readCompetition);
                    } while (xmlReader.ReadToNextSibling(nameof(Competition)));
                }
            }

            return competitionList;
        }
    }
}
