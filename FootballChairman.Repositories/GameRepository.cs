using FootballChairman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FootballChairman.Repositories
{
    public class GameRepository : RepositoryBase, IRepository<Game>
    {
        private readonly string file = $"Games.xml";

        public GameRepository()
        {
            CreateFiles(file);
        }

        public IList<Game> Create(IList<Game> itemList)
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(IList<Game>));

                foreach (var game in itemList)
                {
                    writer.WriteStartElement(nameof(Game));
                    writer.WriteAttributeString(nameof(Game.HomeScore), game.HomeScore.ToString());
                    writer.WriteAttributeString(nameof(Game.AwayScore), game.AwayScore.ToString());
                    writer.WriteAttributeString(nameof(Game.FixtureId), game.Fixture.IdString);
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

        public IList<Game> Get()
        {
            return (Get(file));
        }

        private IList<Game> Get(string filename)
        {
            var gameList = new List<Game>();
            var fileString = File.ReadAllText(Path.Combine(path, filename));

            if (string.IsNullOrWhiteSpace(fileString))
            {
                return gameList;
            }

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement(nameof(IList<Game>));

                    do
                    {
                        var readGameData = new Game();

                        readGameData.HomeScore = int.Parse(xmlReader.GetAttribute(nameof(Game.HomeScore)));
                        readGameData.AwayScore = int.Parse(xmlReader.GetAttribute(nameof(Game.AwayScore)));
                        readGameData.FixtureId = xmlReader.GetAttribute(nameof(Game.FixtureId));

                        gameList.Add(readGameData);
                    } while (xmlReader.ReadToNextSibling(nameof(SaveGameData)));
                }
            }

            return gameList;
        }
    }
}
