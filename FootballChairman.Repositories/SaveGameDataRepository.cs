using FootballChairman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FootballChairman.Repositories
{
    public class SaveGameDataRepository : RepositoryBase, IRepository<SaveGameData>
    {
        private readonly string file = $"SaveGameData.xml";

        public SaveGameDataRepository()
        {
            CreateFiles(file);
        }

        public IList<SaveGameData> Create(IList<SaveGameData> itemList)
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(IList<SaveGameData>));

                foreach (var saveGameData in itemList)
                {
                    writer.WriteStartElement(nameof(SaveGameData));
                    writer.WriteAttributeString(nameof(SaveGameData.Name), saveGameData.Name.ToString());
                    writer.WriteAttributeString(nameof(SaveGameData.Year), saveGameData.Year.ToString());
                    writer.WriteAttributeString(nameof(SaveGameData.MatchDay), saveGameData.MatchDay.ToString());
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

        public IList<SaveGameData> Get()
        {
            return (Get(file));
        }

        private IList<SaveGameData> Get(string filename)
        {
            var saveGameDataList = new List<SaveGameData>();
            var fileString = File.ReadAllText(Path.Combine(path, filename));

            if (string.IsNullOrWhiteSpace(fileString))
            {
                return saveGameDataList;
            }

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement(nameof(IList<SaveGameData>));

                    do
                    {
                        var readSaveGameData = new SaveGameData();

                        readSaveGameData.Name = xmlReader.GetAttribute(nameof(SaveGameData.Name));
                        readSaveGameData.Year = int.Parse(xmlReader.GetAttribute(nameof(SaveGameData.Year)));
                        readSaveGameData.MatchDay = int.Parse(xmlReader.GetAttribute(nameof(SaveGameData.MatchDay)));

                        saveGameDataList.Add(readSaveGameData);
                    } while (xmlReader.ReadToNextSibling(nameof(SaveGameData)));
                }
            }

            return saveGameDataList;
        }
    }
}
