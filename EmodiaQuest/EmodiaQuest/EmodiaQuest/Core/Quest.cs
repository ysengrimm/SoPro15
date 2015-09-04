using System;
using System.Collections.Generic;

namespace EmodiaQuest.Core
{
    public class Quest
    {
        public String Name;
        public String Story;
        public String Description;
        public int Difficulty;
        public String Owner;
        public Dictionary<String,String> Conditions = new Dictionary<string, string>();
        public Dictionary<String, String> Rewards = new Dictionary<string, string>();
        public Dictionary<String, String> Tasks = new Dictionary<string, string>();
        public bool IsRepeatable;

        public Quest()
        {

        }

        public void LoadContent()
        {
            
        }

        public void Update()
        {
            
        }
    }
}