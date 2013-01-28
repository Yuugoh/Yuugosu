using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Yuugosu
{
    class FileReader
    {
        public string path;
        StreamReader sr;
        public Beatmap bm = new Beatmap();

        public void openReader(string p)
        {
            path = p;
            sr = new StreamReader(path);
        }

        public Boolean startRead()
        {
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                switch(line)
                {
                    case "[General]":                        
                        break;
                    case "[Editor]":
                        break;
                    case "[Metadata]":
                        break;
                    case "[Difficulty]":
                        readDifficulty();
                        break;
                    case "[Events]":
                        break;
                    case "[TimingPoints]":
                        break;
                    case "[Colours]":
                        break;
                    case "[HitObjects]":
                        bm.hitObjects = readHitObjects();
                        break;
                    default:
                        break;
                }
            }
            return true;
        }

        public void readDifficulty()
        {
            var diffLine = sr.ReadLine();
            while (diffLine != "")
            {
                var splitted = diffLine.Split(':');
                switch (splitted[0])
                {
                    case "HPDrainRate":
                        bm.drainRate = Int32.Parse(splitted[1]);
                        break;
                    case "CircleSize":
                        bm.circleSize = Int32.Parse(splitted[1]); 
                        break;
                    case "OverallDifficulty":
                        bm.overallDifficulty = Int32.Parse(splitted[1]);
                        break;
                    case "ApproachRate":
                        bm.approachRate = Int32.Parse(splitted[1]);
                        break;
                    case "SliderMultiplier":
                        bm.sliderMultiplier = double.Parse(splitted[1]);
                        break;
                    case "SliderTickRate":
                        bm.sliderTickRate = Int32.Parse(splitted[1]);
                        break;
                    default:
                        break;
                }
                diffLine = sr.ReadLine();
            }               
        }

        //Only reads timing and position. !!!treats every object as circle!!!
        public List<HitCircle> readHitObjects()
        {
            var hitLine = sr.ReadLine();
            List<HitCircle> res = new List<HitCircle>();
            while(hitLine != null)
            {
                var splitted = hitLine.Split(',');                
                HitCircle temp = new HitCircle(Int32.Parse(splitted[0]), Int32.Parse(splitted[1]), Int32.Parse(splitted[2]));
                res.Add(temp);
                hitLine = sr.ReadLine();
            }
            return res;
        }
    }
}
