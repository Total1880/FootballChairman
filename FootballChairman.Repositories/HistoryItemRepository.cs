using FootballChairman.Models;
using System.Xml;

namespace FootballChairman.Repositories
{
    public class HistoryItemRepository : RepositoryBase, IRepository<HistoryItem>
    {
        private readonly string file = $"HistoryItems.xml";

        public HistoryItemRepository()
        {
            CreateFiles(file);
        }

        public IList<HistoryItem> Create(IList<HistoryItem> itemList)
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(IList<HistoryItem>));

                foreach (var historyItem in itemList)
                {
                    writer.WriteStartElement(nameof(HistoryItem));
                    writer.WriteAttributeString(nameof(HistoryItem.Id), historyItem.Id.ToString());
                    writer.WriteAttributeString(nameof(HistoryItem.ClubId), historyItem.ClubId.ToString());
                    writer.WriteAttributeString(nameof(HistoryItem.CompetitionId), historyItem.CompetitionId.ToString());
                    writer.WriteAttributeString(nameof(HistoryItem.Year), historyItem.Year.ToString());
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

        public IList<HistoryItem> Get()
        {
            return (Get(file));
        }

        private IList<HistoryItem> Get(string filename)
        {
            var historyItemList = new List<HistoryItem>();
            var fileString = File.ReadAllText(Path.Combine(path, filename));

            if (string.IsNullOrWhiteSpace(fileString))
            {
                return historyItemList;
            }

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement(nameof(IList<HistoryItem>));

                    do
                    {
                        var readHistoryItem = new HistoryItem();

                        readHistoryItem.Id = int.Parse(xmlReader.GetAttribute(nameof(HistoryItem.Id)));
                        readHistoryItem.ClubId = int.Parse(xmlReader.GetAttribute(nameof(HistoryItem.ClubId)));
                        readHistoryItem.CompetitionId = int.Parse(xmlReader.GetAttribute(nameof(HistoryItem.CompetitionId)));
                        readHistoryItem.Year = int.Parse(xmlReader.GetAttribute(nameof(HistoryItem.Year)));

                        historyItemList.Add(readHistoryItem);
                    } while (xmlReader.ReadToNextSibling(nameof(HistoryItem)));
                }
            }

            return historyItemList;
        }
    }
}
