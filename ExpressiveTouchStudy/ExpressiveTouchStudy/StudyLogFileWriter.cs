﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveTouchStudy
{
    class StudyLogFileWriter
    {
        private string logFileName;

        public StudyLogFileWriter(string path, string filename)
        {
            CreateFile(path, filename);   
        }

        public void WriteLine(int participantId, int taskNumber, string interactionTechnique, string condition, string sensorPosition, TimeSpan duration, bool success, string[] data)
        {
            string line = string.Format("{0},{1},{2},{3},{4},{5},{6}", participantId, sensorPosition, taskNumber, interactionTechnique, condition, duration, success);
            
            foreach (string s in data)
            {
                line += string.Format(",{0}", s);
            }

            line += "\n";

            StreamWriter w = File.AppendText(logFileName);
            w.Write(line);
            w.Close();
        }

        private void CreateFile(string path, string filename)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fullPath = Path.Combine(path, filename);
            while(File.Exists(fullPath))
            {
                fullPath = Path.Combine(path, DateTime.UtcNow.Second + "_" + filename);
            }

            logFileName = fullPath;
        }
    }
}
