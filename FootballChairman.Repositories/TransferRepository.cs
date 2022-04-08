using FootballChairman.Models;
using System.Xml;

namespace FootballChairman.Repositories
{
    public class TransferRepository : RepositoryBase, IRepository<Transfer>
    {
        private readonly string file = $"Transfers.xml";

        public TransferRepository()
        {
            CreateFiles(file);
        }

        public IList<Transfer> Create(IList<Transfer> itemList)
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(IList<Transfer>));

                foreach (var transfer in itemList)
                {
                    writer.WriteStartElement(nameof(Transfer));
                    writer.WriteAttributeString(nameof(Transfer.Year), transfer.Year.ToString());
                    writer.WriteAttributeString(nameof(Transfer.Player), transfer.Player.Id.ToString());
                    writer.WriteAttributeString(nameof(Transfer.PreviousClub), transfer.PreviousClub.Id.ToString());
                    writer.WriteAttributeString(nameof(Transfer.NextClub), transfer.NextClub.Id.ToString());
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

        public IList<Transfer> Get()
        {
            return (Get(file));
        }

        private IList<Transfer> Get(string filename)
        {
            var transferList = new List<Transfer>();
            var fileString = File.ReadAllText(Path.Combine(path, filename));

            if (string.IsNullOrWhiteSpace(fileString))
            {
                return transferList;
            }

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement(nameof(IList<Transfer>));

                    do
                    {
                        var readTransferData = new Transfer();

                        readTransferData.Year = int.Parse(xmlReader.GetAttribute(nameof(Transfer.Year)));
                        readTransferData.Player.Id = int.Parse(xmlReader.GetAttribute(nameof(Transfer.Player)));
                        readTransferData.PreviousClub.Id = int.Parse(xmlReader.GetAttribute(nameof(Transfer.PreviousClub)));
                        readTransferData.NextClub.Id = int.Parse(xmlReader.GetAttribute(nameof(Transfer.NextClub)));

                        transferList.Add(readTransferData);
                    } while (xmlReader.ReadToNextSibling(nameof(Transfer)));
                }
            }

            return transferList;
        }
    }
}
