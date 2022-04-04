using FootballChairman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FootballChairman.Repositories
{
    public class PlayerRepository : RepositoryBase, IRepository<Player>
    {
        private readonly string file = $"Players.xml";

        public PlayerRepository()
        {
            CreateFiles(file);
        }

        public IList<Player> Create(IList<Player> itemList)
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(IList<Manager>));

                foreach (var player in itemList)
                {
                    writer.WriteStartElement(nameof(Player));
                    writer.WriteAttributeString(nameof(Player.Id), player.Id.ToString());
                    writer.WriteAttributeString(nameof(Player.FirstName), player.FirstName.ToString());
                    writer.WriteAttributeString(nameof(Player.LastName), player.LastName.ToString());
                    writer.WriteAttributeString(nameof(Player.Defense), player.Defense.ToString());
                    writer.WriteAttributeString(nameof(Player.Midfield), player.Midfield.ToString());
                    writer.WriteAttributeString(nameof(Player.Attack), player.Attack.ToString());
                    writer.WriteAttributeString(nameof(Player.ClubId), player.ClubId.ToString());
                    writer.WriteAttributeString(nameof(Player.Age), player.Age.ToString());
                    writer.WriteAttributeString(nameof(Player.Goalkeeping), player.Goalkeeping.ToString());
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

        public IList<Player> Get()
        {
            return (Get(file));
        }

        private IList<Player> Get(string filename)
        {
            var playerList = new List<Player>();
            var fileString = File.ReadAllText(Path.Combine(path, filename));

            if (string.IsNullOrWhiteSpace(fileString))
            {
                return playerList;
            }

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement(nameof(IList<Player>));

                    do
                    {
                        var readPlayer = new Player();

                        readPlayer.Id = int.Parse(xmlReader.GetAttribute(nameof(Player.Id)));
                        readPlayer.FirstName = xmlReader.GetAttribute(nameof(Player.FirstName));
                        readPlayer.LastName = xmlReader.GetAttribute(nameof(Player.LastName));
                        readPlayer.Defense = int.Parse(xmlReader.GetAttribute(nameof(Player.Defense)));
                        readPlayer.Midfield = int.Parse(xmlReader.GetAttribute(nameof(Player.Midfield)));
                        readPlayer.Attack = int.Parse(xmlReader.GetAttribute(nameof(Player.Attack)));
                        readPlayer.ClubId = int.Parse(xmlReader.GetAttribute(nameof(Player.ClubId)));
                        readPlayer.Age = int.Parse(xmlReader.GetAttribute(nameof(Player.Age)));
                        readPlayer.Goalkeeping = int.Parse(xmlReader.GetAttribute(nameof(Player.Goalkeeping)));

                        playerList.Add(readPlayer);
                    } while (xmlReader.ReadToNextSibling(nameof(Player)));
                }
            }

            return playerList;
        }
    }
}
