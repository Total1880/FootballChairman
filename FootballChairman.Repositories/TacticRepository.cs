using FootballChairman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FootballChairman.Repositories
{
    public class TacticRepository : RepositoryBase, IRepository<Tactic>
    {
        private readonly string file = $"Tactics.xml";

        public TacticRepository()
        {
            CreateFiles(file);
        }

        public IList<Tactic> Create(IList<Tactic> itemList)
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(IList<Tactic>));

                foreach (var tactic in itemList)
                {
                    writer.WriteStartElement(nameof(Tactic));
                    writer.WriteAttributeString(nameof(Tactic.ClubId), tactic.ClubId.ToString());
                    writer.WriteAttributeString(nameof(Tactic.GoalkeeperId), tactic.GoalkeeperId.ToString());
                    writer.WriteStartElement(nameof(Tactic.Defenders));
                    foreach (var defender in tactic.DefendersId)
                    {
                        //writer.WriteStartElement(nameof(Tactic.DefendersId));
                        writer.WriteElementString(nameof(Tactic.DefendersId), defender.ToString());
                        //writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteStartElement(nameof(Tactic.Midfielders));

                    foreach (var midfielder in tactic.MidfieldersId)
                    {
                        //writer.WriteStartElement(nameof(Tactic.MidfieldersId));
                        writer.WriteElementString(nameof(Tactic.MidfieldersId), midfielder.ToString());
                        //writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteStartElement(nameof(Tactic.Attackers));
                    foreach (var attacker in tactic.AttackersId)
                    {
                        //writer.WriteStartElement(nameof(Tactic.AttackersId));
                        writer.WriteElementString(nameof(Tactic.AttackersId), attacker.ToString());
                        //writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
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

        public IList<Tactic> Get()
        {
            return (Get(file));
        }

        private IList<Tactic> Get(string filename)
        {
            var tacticList = new List<Tactic>();
            var fileString = File.ReadAllText(Path.Combine(path, filename));

            if (string.IsNullOrWhiteSpace(fileString))
            {
                return tacticList;
            }

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement(nameof(IList<Tactic>));

                    do
                    {
                        var readTactic = new Tactic();

                        readTactic.ClubId = int.Parse(xmlReader.GetAttribute(nameof(Tactic.ClubId)));
                        readTactic.GoalkeeperId = int.Parse(xmlReader.GetAttribute(nameof(Tactic.GoalkeeperId)));

                        readTactic.DefendersId = new List<int>();
                        xmlReader.ReadStartElement(nameof(Tactic));
                        xmlReader.ReadStartElement(nameof(Tactic.Defenders));
                        do
                        {
                            xmlReader.ReadStartElement(nameof(Tactic.DefendersId));
                            readTactic.DefendersId.Add(int.Parse(xmlReader.ReadContentAsString()));
                        } while (xmlReader.ReadToNextSibling(nameof(Tactic.DefendersId)));

                        xmlReader.ReadEndElement();
                        readTactic.MidfieldersId = new List<int>();
                        xmlReader.ReadStartElement(nameof(Tactic.Midfielders));

                        do
                        {
                            xmlReader.ReadStartElement(nameof(Tactic.MidfieldersId));
                            readTactic.MidfieldersId.Add(int.Parse(xmlReader.ReadContentAsString()));
                        } while (xmlReader.ReadToNextSibling(nameof(Tactic.MidfieldersId)));

                        xmlReader.ReadEndElement();
                        readTactic.AttackersId = new List<int>();
                        xmlReader.ReadStartElement(nameof(Tactic.Attackers));

                        do
                        {
                            xmlReader.ReadStartElement(nameof(Tactic.AttackersId));
                            readTactic.AttackersId.Add(int.Parse(xmlReader.ReadContentAsString()));
                        } while (xmlReader.ReadToNextSibling(nameof(Tactic.AttackersId)));
                        xmlReader.ReadEndElement();

                        tacticList.Add(readTactic);
                    } while (xmlReader.ReadToNextSibling(nameof(Tactic)));
                }
            }

            return tacticList;
        }
    }
}
