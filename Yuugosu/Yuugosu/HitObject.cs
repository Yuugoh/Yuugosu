using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yuugosu
{
    public class HitObject
    {
        int xPos;
        int yPos;
        public int hitTime;
        int objectType;
        int hitSound;

        enum HITSOUND
        {
            whistle = 2,
            finish = 4,
            clap = 8            
        }

        enum OBJECTYPE
        {
            circle = 1,
            slider = 2,
            newCombo = 4,
            spinner = 12
        }

        public HitObject(int x, int y, int st)
        {
            xPos = x;
            yPos = y;
            hitTime = st;
        }
    }
}
