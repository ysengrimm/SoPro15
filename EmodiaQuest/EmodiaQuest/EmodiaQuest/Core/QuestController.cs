using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using EmodiaQuest.Core.Items;
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

        public Dictionary<string, int> KilledEnemies = new Dictionary<string, int>();

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
                tempQuest.Description = innerXml.DocumentElement.SelectSingleNode("/quest/description").InnerText;
                tempQuest.Difficulty = int.Parse(innerXml.DocumentElement.SelectSingleNode("/quest/difficulty").InnerText);
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
                var activeQuestsByOwner = from quest in Quests
                    where quest.Owner == owner.Name.ToString() && ActiveQuests.Contains(quest)
                    select quest;
                foreach (Quest activeQuest in activeQuestsByOwner)
                {
                    bool level, solved, visit = false, kill, item, gold;
                    List<bool> outCompare = new List<bool>();
                    foreach (var key in activeQuest.Tasks.Keys)
                    {
                        switch (key)
                        {
                            case "level":
                                String levelVal;
                                activeQuest.Tasks.TryGetValue(key, out levelVal);
                                level = Player.Instance.Level >= int.Parse(levelVal);
                                outCompare.Add(level);
                                break;
                            case "solved":
                                String solvedOut;
                                activeQuest.Tasks.TryGetValue(key, out solvedOut);
                                foreach (var sq in SolvedQuests)
                                {
                                    if (sq.Name == solvedOut)
                                    {
                                        solved = true;
                                        outCompare.Add(solved);
                                    }
                                }
                                break;
                            case "visit":
                                // wontfix
                                break;
                            case "kill":
                                String killOut;
                                activeQuest.Tasks.TryGetValue(key, out killOut);

                                string[] enemyAndCount = killOut.Split(',');

                                int monsterKilled;
                                KilledEnemies.TryGetValue(enemyAndCount[0], out monsterKilled);
                                if (monsterKilled >= int.Parse(enemyAndCount[1]))
                                {
                                    kill = true;
                                    outCompare.Add(kill);
                                    KilledEnemies[enemyAndCount[0]] = 0;
                                }
                                break;
                            case "item":
                                String itemOut;
                                activeQuest.Tasks.TryGetValue(key, out itemOut);
                                foreach (var itemQ in ItemTestClass.Instance.Quests)
                                {
                                    if (itemQ.Name == itemOut)
                                    {
                                        item = true;
                                        outCompare.Add(item);
                                    }
                                }
                                break;
                            case "gold":
                                String goldOut;
                                activeQuest.Tasks.TryGetValue(key, out goldOut);
                                gold =  Player.Instance.Gold >= int.Parse(goldOut);
                                outCompare.Add(gold);
                                break;
                        }
                    }

                    var questCompareResult = from res in outCompare where !res select res;
                    if (outCompare.Any() && !questCompareResult.Any())
                    {
                        ActiveQuests.Remove(activeQuest);
                        SolvedQuests.Add(activeQuest);
                        Console.WriteLine("Solved Quest: " + activeQuest.Name);
                    }

                }
            }
            else
            {
                var questsByOwner = from quest in Quests where quest.Owner == owner.Name.ToString() && !SolvedQuests.Contains(quest) select quest;
                foreach (Quest quest in questsByOwner)
                {
                    bool level, solved, visit = false, kill = false, item, gold;
                    List<bool> outCompare = new List<bool>();
                    foreach (var key in quest.Conditions.Keys)
                    {
                        switch (key)
                        {
                            case "level":
                                String levelVal;
                                quest.Conditions.TryGetValue(key, out levelVal);
                                level = Player.Instance.Level >= int.Parse(levelVal);
                                outCompare.Add(level);
                                break;
                            case "solved":
                                String solvedOut;
                                quest.Conditions.TryGetValue(key, out solvedOut);
                                foreach (var sq in SolvedQuests)
                                {
                                    if (sq.Name == solvedOut)
                                    {
                                        solved = true;
                                        outCompare.Add(solved);
                                    }
                                }
                                break;
                            case "visit":
                                // wontfix
                                break;
                            case "kill":
                                // meh
                                break;
                            case "item":
                                String itemOut;
                                quest.Conditions.TryGetValue(key, out itemOut);
                                foreach (var itemQ in ItemTestClass.Instance.Quests)
                                {
                                    if (itemQ.Name == itemOut)
                                    {
                                        item = true;
                                        outCompare.Add(item);
                                    }
                                }
                                break;
                            case "gold":
                                String goldOut;
                                quest.Conditions.TryGetValue(key, out goldOut);
                                gold = Player.Instance.Gold >= int.Parse(goldOut);
                                outCompare.Add(gold);
                                break;
                        }
                    }

                    
                    List<bool> otherList = outCompare.Where(res => !res).ToList();

                    if (outCompare.Any() && !otherList.Any())
                    {
                        ActiveQuests.Add(quest);
                        Console.WriteLine("Addded Quest: " + quest.Name);
                    }

                }
            }
        }
    }
}