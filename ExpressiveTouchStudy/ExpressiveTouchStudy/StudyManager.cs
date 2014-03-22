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
            Log = new StudyLogFileWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ExpressiveTouch"), string.Format("{0}.csv", id));
            
            ParseStudyConfigFile();
        }

        public InteractionTechnique GetCurrentTechnique()
        {
            return InteractionTechniques[InteractionTechniqueCount];
        }

        public void Next()
        {
            InteractionTechniqueCount = InteractionTechniqueCount + 1;
        }

        public Page GetPageFromCurrentTechnique()
        {
            string name = GetCurrentTechnique().name;

            return null;
        }

        private void ParseStudyConfigFile()
        {
            string studyConfigFile = Properties.Settings.Default.InteractionTechniques;
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
