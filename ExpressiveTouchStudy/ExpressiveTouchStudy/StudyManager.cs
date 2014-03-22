using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ExpressiveTouchStudy
{
    class StudyManager
    {
        public struct InteractionTechnique
        {
            public string name;
            public string[] conditions;

            public InteractionTechnique(string name, string[] conditions)
            {
                this.name = name;
                this.conditions = conditions;
            }
        }

        private static StudyManager instance = null;

        public int Id { get; private set; }
        public StudyLogFileWriter Log { get; private set; }
        public List<InteractionTechnique> InteractionTechniques;
        public int InteractionTechniqueCount { get; private set; }
        public List<KeyValuePair<string, int>> ConditionCounts { get; private set; }
        public string Condition { get; private set; }
        public int TrialCount { get; private set; }

        public static StudyManager CreateInstance(int id)
        {
            instance = new StudyManager(id);
            return instance;
        }

        public static StudyManager GetInstance()
        {
            if (instance == null)
            {
                throw new Exception("No instance of study manager has been created yet");
            }

            return instance;
        }

        private StudyManager(int id)
        {
            this.Id = id;

            InteractionTechniqueCount = 0;
            TrialCount = -1;

            Log = new StudyLogFileWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ExpressiveTouch"), string.Format("{0}.csv", id));
            
            ParseStudyConfigFile();
            SetupConditionCounters();
        }

        public void LogTrial(TimeSpan duration, bool success, string[] data)
        {
            Log.WriteLine(Id, TrialCount, GetCurrentTechnique().name, Condition, 0, duration, success, data);
        }

        public Page GetNextCondition()
        {
            if (ConditionCounts.Count == 0)
            {
                Next();
            }

            InteractionTechnique current = GetCurrentTechnique();
            bool found = false;
            while (!found)
            {
                Random rand = new Random(DateTime.UtcNow.Millisecond);
                int conditionId = rand.Next(0, current.conditions.Length);
                string condition = current.conditions[conditionId];

                for(int i = 0; i < ConditionCounts.Count; i++)
                {
                    KeyValuePair<string, int> c = ConditionCounts[i];
                    if (c.Key == condition)
                    {
                        Condition = condition;
                        ConditionCounts.Remove(c);

                        if(c.Value + 1 < Properties.Settings.Default.TrialsPerCondition)
                        {
                            ConditionCounts.Add(new KeyValuePair<string, int>(c.Key, c.Value + 1));
                        }

                        found = true;
                        break;
                    }
                }
            }

            TrialCount++;
            return GetPageFromCurrentTechnique();
        }

        private void SetupConditionCounters()
        {
            InteractionTechnique current = GetCurrentTechnique();
            ConditionCounts = new List<KeyValuePair<string, int>>();

            foreach (string s in current.conditions)
            {
                KeyValuePair<string, int> conditionCounter = new KeyValuePair<string, int>(s, 0);
                ConditionCounts.Add(conditionCounter);
            }
        }

        private InteractionTechnique GetCurrentTechnique()
        {
            if (InteractionTechniqueCount >= InteractionTechniques.Count)
            {
                // end study
                Environment.Exit(0);
            }

            return InteractionTechniques[InteractionTechniqueCount];
        }

        private void Next()
        {
            InteractionTechniqueCount = InteractionTechniqueCount + 1;
            SetupConditionCounters();
        }

        private Page GetPageFromCurrentTechnique()
        {
            string name = GetCurrentTechnique().name;

            if (name == "ImpactForce")
            {
                return new ImpactForce();
            }
            else if (name == "VelocityTouchUp")
            {
                return new VelocityOfTouchUp();
            }
            else if (name == "DirectionOfApproach")
            {
                return new DirectionOfApproach();
            }
            else if (name == "FingerAngle")
            {
                return new FingerAngle();
            }
            else if (name == "TwistingTouch")
            {
                return new TwistingTouch();
            }
            else if (name == "QuiveringFinger")
            {
                return new QuiveringFinger();
            }

            return null;
        }

        private void ParseStudyConfigFile()
        {
            string studyConfigFile = Properties.Settings.Default.InteractionTechniques_Debug;
            InteractionTechniques = new List<InteractionTechnique>();
            
            string[] techniques = studyConfigFile.Split(';');
            foreach (string t in techniques)
            {
                string[] parts = t.Split(':');
                
                string name = parts[0];
                string[] conditions = parts.Length > 1 ? parts[1].Split(',') : null;

                if (name != "")
                {
                    InteractionTechnique i = new InteractionTechnique(name, conditions);
                    InteractionTechniques.Add(i);
                }
            }

            Random rnd = new Random(DateTime.UtcNow.Millisecond);
            InteractionTechniques = InteractionTechniques.OrderBy(x => rnd.Next()).ToList();
        }
    }
}
