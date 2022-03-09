﻿using FootballChairman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FootballChairman.Repositories
{
    public class ManagerRepository : RepositoryBase, IRepository<Manager>
    {
        private readonly string file = $"Managers.xml";

        public ManagerRepository()
        {
            CreateFiles(file);
        }

        public IList<Manager> Create(IList<Manager> itemList)
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(IList<Manager>));

                foreach (var manager in itemList)
                {
                    writer.WriteStartElement(nameof(Manager));
                    writer.WriteAttributeString(nameof(Manager.Id), manager.Id.ToString());
                    writer.WriteAttributeString(nameof(Manager.FirstName), manager.FirstName.ToString());
                    writer.WriteAttributeString(nameof(Manager.LastName), manager.LastName.ToString());
                    writer.WriteAttributeString(nameof(Manager.TrainingDefenseSkill), manager.TrainingDefenseSkill.ToString());
                    writer.WriteAttributeString(nameof(Manager.TrainingMidfieldSkill), manager.TrainingMidfieldSkill.ToString());
                    writer.WriteAttributeString(nameof(Manager.TrainingAttackSkill), manager.TrainingAttackSkill.ToString());
                    writer.WriteAttributeString(nameof(Manager.ClubId), manager.ClubId.ToString());
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

        public IList<Manager> Get()
        {
            return (Get(file));
        }

        private IList<Manager> Get(string filename)
        {
            var managerList = new List<Manager>();
            var fileString = File.ReadAllText(Path.Combine(path, filename));

            if (string.IsNullOrWhiteSpace(fileString))
            {
                return managerList;
            }

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement(nameof(IList<Manager>));

                    do
                    {
                        var readManager = new Manager();

                        readManager.Id = int.Parse(xmlReader.GetAttribute(nameof(Manager.Id)));
                        readManager.FirstName = xmlReader.GetAttribute(nameof(Manager.FirstName));
                        readManager.LastName = xmlReader.GetAttribute(nameof(Manager.LastName));
                        readManager.TrainingDefenseSkill = int.Parse(xmlReader.GetAttribute(nameof(Manager.TrainingDefenseSkill)));
                        readManager.TrainingMidfieldSkill = int.Parse(xmlReader.GetAttribute(nameof(Manager.TrainingMidfieldSkill)));
                        readManager.TrainingAttackSkill = int.Parse(xmlReader.GetAttribute(nameof(Manager.TrainingAttackSkill)));
                        readManager.ClubId = int.Parse(xmlReader.GetAttribute(nameof(Manager.ClubId)));

                        managerList.Add(readManager);
                    } while (xmlReader.ReadToNextSibling(nameof(Manager)));
                }
            }

            return managerList;
        }
    }
}