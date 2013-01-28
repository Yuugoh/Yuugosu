using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Yuugosu
{
    class Beatmap
    {
        public int drainRate;
        public int circleSize;
        public int overallDifficulty;
        public int approachRate;
        public double sliderMultiplier;
        public int sliderTickRate;
        public List<HitCircle> hitObjects = new List<HitCircle>();

        public int getRealtimeAR()
        {
            return 1950 - approachRate * 150;
        }


    }
}
