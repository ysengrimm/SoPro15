using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using EmodiaQuest.Core.NPCs;
using Microsoft.Xna.Framework.Content;

namespace EmodiaQuest.Core
{
    public class QuestController
    {
        private QuestController() { }

        private static QuestController _instance;
        public static QuestController Instance
        {
            get { return _instance ?? (_instance = new QuestController()); }
        }

        public string XMLName { get; set; }

        public List<Quest> Quests = new List<Quest>(); 
        public List<Quest> ActiveQuests = new List<Quest>(); 
        public List<Quest> SolvedQuests = new List<Quest>(); 

        public void LoadContent(ContentManager contentMngr)
        {
            XMLName = "testquest.xml";
            String contentRoot = AppDomain.CurrentDomain.BaseDirectory + "Content\\ficken\\";
            XmlDocument doc = new XmlDocument();
            doc.Load(contentRoot + XMLName);

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                XmlDocument innerXml = new XmlDocument();
                innerXml.LoadXml("<quest>" + node.InnerXml + "</quest>");

                Quest tempQuest = new Quest();
                tempQuest.Name = innerXml.DocumentElement.SelectSingleNode("/quest/name").InnerText;
                tempQuest.Story = innerXml.DocumentElement.SelectSingleNode("/quest/story").InnerText;
                tempQuest.Owner = innerXml.DocumentElement.SelectSingleNode("/quest/owner").InnerText;
                foreach (XmlNode innerNode in innerXml.DocumentElement.SelectSingleNode("/quest/conditions").ChildNodes)
                {
                    tempQuest.Conditions.Add(innerNode.Name, innerNode.InnerText);
                }
                foreach (XmlNode innerNode in innerXml.DocumentElement.SelectSingleNode("/quest/rewards").ChildNodes)
                {
                    tempQuest.Rewards.Add(innerNode.Name, innerNode.InnerText);
                }
                foreach (XmlNode innerNode in innerXml.DocumentElement.SelectSingleNode("/quest/tasks").ChildNodes)
                {
                    tempQuest.Tasks.Add(innerNode.Name, innerNode.InnerText);
                }

                Quests.Add(tempQuest);
            }
        }

        public void Update()
        {
            
        }

        public void QuestUpdate(NPC owner)
        {
            if (ActiveQuests.Count > 0)
            {
                var activeQuestsByOwner = from quest in Quests where quest.Owner == owner.Name.ToString() && ActiveQuests.Contains(quest) select quest;
                foreach (Quest activeQuest in activeQuestsByOwner)
                {
                    checkForCompletion(activeQuest);
                }
            }
            
        }

        private bool checkForCompletion(Quest q)
        {
            bool level = false, solved = false, visit = false, kill = false, item = false, gold = false;
            foreach (var key in q.Tasks.Keys)
            {
                switch (key)
                {
                    case "level":
                        String levelVal = "";
                        q.Tasks.TryGetValue(key, out levelVal);
                        level = int.Parse(levelVal) >= Player.Instance.Level;
                        break;
                    case "solved":
                        break;
                    case "visit":
                        break;
                    case "kill":
                        break;
                    case "item":
                        break;
                    case "gold":
                        break;
                }
            }

            return true;
        }

    }
}