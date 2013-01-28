using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Yuugosu
{
    class PassiveGUI
    {
        Texture2D background;
        Texture2D HPbar;
        
        SpriteFont font;
        String text = " ";

        public void LoadBackground(ContentManager content, GraphicsDevice device, String path)
        {
            //Load BG image
            Stream s = new FileStream(path, FileMode.Open);
            background = Texture2D.FromStream(device, s);
            s.Close();

            //Load content
            font = content.Load<SpriteFont>("testFont");
        }

        public void LoadHPBarBG(ContentManager cm)
        {
            //HPbar = cm.Load<Texture2D>("Skin/scorebar-bg" );
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(background, new Vector2(0,0), Color.White);
            //sb.Draw(HPbar, new Rectangle(0, 0, 743, 80), Color.White);

            sb.DrawString(font, text, new Vector2(100, 100), Color.White);
        }

        public void setText(String set)
        {
            text = set;
        }
    }
}
