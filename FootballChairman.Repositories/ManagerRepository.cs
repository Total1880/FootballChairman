using FootballChairman.Models;
using FootballChairman.Models.Enums;
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
                    writer.WriteAttributeString(nameof(Manager.TrainingGoalkeepingSkill), manager.TrainingGoalkeepingSkill.ToString());
                    writer.WriteAttributeString(nameof(Manager.ClubId), manager.ClubId.ToString());
                    writer.WriteAttributeString(nameof(Manager.Age), manager.Age.ToString());
                    writer.WriteAttributeString(nameof(Manager.CountryId), manager.CountryId.ToString());
                    writer.WriteAttributeString(nameof(Manager.ManagerType), manager.ManagerType.ToString());
                    writer.WriteAttributeString(nameof(Manager.FacilityUpgradeType), manager.FacilityUpgradeType.ToString());
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
                        readManager.TrainingGoalkeepingSkill = int.Parse(xmlReader.GetAttribute(nameof(Manager.TrainingGoalkeepingSkill)));
                        readManager.ClubId = int.Parse(xmlReader.GetAttribute(nameof(Manager.ClubId)));
                        readManager.Age = int.Parse(xmlReader.GetAttribute(nameof(Manager.Age)));
                        readManager.CountryId = int.Parse(xmlReader.GetAttribute(nameof(Manager.CountryId)));
                        readManager.ManagerType = (ManagerType)Enum.Parse(typeof(ManagerType), xmlReader.GetAttribute(nameof(Manager.ManagerType)));
                        readManager.FacilityUpgradeType = (FacilityUpgradeType)Enum.Parse(typeof(FacilityUpgradeType), xmlReader.GetAttribute(nameof(Manager.FacilityUpgradeType)));

                        managerList.Add(readManager);
                    } while (xmlReader.ReadToNextSibling(nameof(Manager)));
                }
            }

            return managerList;
        }
    }
}
