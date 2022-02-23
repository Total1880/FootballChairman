using FootballChairman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FootballChairman.Repositories
{
    public class ClubPerCompetitionRepository : RepositoryBase, IRepository<ClubPerCompetition>
    {
        private readonly string file = $"ClubsPerCompetitions.xml";

        public ClubPerCompetitionRepository()
        {
            CreateFiles(file);
        }
        public IList<ClubPerCompetition> Create(IList<ClubPerCompetition> itemList)
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(IList<ClubPerCompetition>));

                foreach (var clubPerCompetition in itemList)
                {
                    writer.WriteStartElement(nameof(ClubPerCompetition));
                    writer.WriteAttributeString(nameof(ClubPerCompetition.ClubId), clubPerCompetition.ClubId.ToString());
                    writer.WriteAttributeString(nameof(ClubPerCompetition.ClubName), clubPerCompetition.ClubName.ToString());
                    writer.WriteAttributeString(nameof(ClubPerCompetition.CompetitionId), clubPerCompetition.CompetitionId.ToString());
                    writer.WriteAttributeString(nameof(ClubPerCompetition.Points), clubPerCompetition.Points.ToString());
                    writer.WriteAttributeString(nameof(ClubPerCompetition.GoalsFor), clubPerCompetition.GoalsFor.ToString());
                    writer.WriteAttributeString(nameof(ClubPerCompetition.GoalsAgainst), clubPerCompetition.GoalsAgainst.ToString());
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

        public IList<ClubPerCompetition> Get()
        {
            return (Get(file));
        }

        private IList<ClubPerCompetition> Get(string filename)
        {
            var clubPerCompetitionList = new List<ClubPerCompetition>();
            var fileString = File.ReadAllText(Path.Combine(path, filename));

            if (string.IsNullOrWhiteSpace(fileString))
            {
                return clubPerCompetitionList;
            }

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement(nameof(IList<ClubPerCompetition>));

                    do
                    {
                        var readClubPerCompetition = new ClubPerCompetition();

                        readClubPerCompetition.ClubId = int.Parse(xmlReader.GetAttribute(nameof(ClubPerCompetition.ClubId)));
                        readClubPerCompetition.ClubName = xmlReader.GetAttribute(nameof(ClubPerCompetition.ClubName));
                        readClubPerCompetition.CompetitionId = int.Parse(xmlReader.GetAttribute(nameof(ClubPerCompetition.CompetitionId)));
                        readClubPerCompetition.Points = int.Parse(xmlReader.GetAttribute(nameof(ClubPerCompetition.Points)));
                        readClubPerCompetition.GoalsFor = int.Parse(xmlReader.GetAttribute(nameof(ClubPerCompetition.GoalsFor)));
                        readClubPerCompetition.GoalsAgainst = int.Parse(xmlReader.GetAttribute(nameof(ClubPerCompetition.GoalsAgainst)));

                        clubPerCompetitionList.Add(readClubPerCompetition);
                    } while (xmlReader.ReadToNextSibling(nameof(ClubPerCompetition)));
                }
            }

            return clubPerCompetitionList;
        }
    }
}
