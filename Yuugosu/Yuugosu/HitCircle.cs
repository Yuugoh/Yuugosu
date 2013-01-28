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
    class HitCircle : HitObject
    {       
        Texture2D mHitCircle;
        Texture2D mCircleOverlay;
        Texture2D mApproachCircle;
        public Vector2 position;
        public Rectangle size;
        float scale = 0.5f;
        float approachScale = 0.5f*2f;

        public HitCircle(int x, int y, int st) : base(x, y, st) 
        {
            position = new Vector2(x, y);
        }

        public void LoadContent(ContentManager cm)
        {
            mHitCircle = cm.Load<Texture2D>("Skin/hitcircle");
            mCircleOverlay = cm.Load<Texture2D>("Skin/hitcircleoverlay");
            mApproachCircle = cm.Load<Texture2D>("Skin/approachcircle");
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mApproachCircle, positionOffset(mApproachCircle, approachScale),
                              new Rectangle(0, 0, mApproachCircle.Width, mApproachCircle.Height),
                              new Color(0, 255, 0, 255), 0.0f, Vector2.Zero, ApproachScale, SpriteEffects.None, 0);
            theSpriteBatch.Draw(mHitCircle, positionOffset(mHitCircle, Scale), new Rectangle(0, 0, mHitCircle.Width, mHitCircle.Height),
                              new Color(0, 255, 0, 255), 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
            theSpriteBatch.Draw(mCircleOverlay, positionOffset(mCircleOverlay, Scale), new Rectangle(0, 0, mCircleOverlay.Width, mCircleOverlay.Height),
                              new Color(0, 255, 0, 150), 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);            
        }

        public float Scale
        {
            get { return scale; }
            set
            {
                scale = 1-value/20;
                size = new Rectangle(0, 0, (int)(mHitCircle.Width * Scale), (int)(mHitCircle.Height * Scale));
            }
        }

        public float ApproachScale
        {
            get { return approachScale; }
            set
            {
                approachScale = value;
            }
        }

        public float approachSize(double currentTime, double ARtime)
        {
            float division = (float)(3 - 2/(ARtime / (currentTime - (hitTime - ARtime))));
            return division*Scale;
        }

        public Vector2 positionOffset(Texture2D texture, float scaling)
        {
            return new Vector2(position.X -((texture.Width * scaling - texture.Width) / 2), position.Y -((texture.Height * scaling - texture.Height)/2));
        }

    }
}
