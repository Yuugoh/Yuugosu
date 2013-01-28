using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Reflection;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Fx;

namespace Yuugosu
{ 
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        FileReader fr = new FileReader();
        Beatmap map = new Beatmap();
        List<HitObject> currentObjects = new List<HitObject>();
        List<ScoreObject> currentScoreObjects = new List<ScoreObject>();
        PassiveGUI gameField = new PassiveGUI();
        MapSelectForm msf;
        Texture2D mouseTexture;
        MouseState mPreviousMouseState;
        MouseState mCurrentMouseState;
        Point mousePosition;
        

        int stream = 0; //Stream used by Bass

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;
            Content.RootDirectory = "Content";
        }       
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            BassNet.Registration("w.kowaluk@gmail.com", "2X32382019152222");            
            FormLaunch();            
            base.Initialize();
            
        }
                
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            gameField.LoadHPBarBG(this.Content);
            gameField.LoadBackground(this.Content, graphics.GraphicsDevice, msf.imgPath);
            
            foreach (HitCircle circle in map.hitObjects)
            {
                circle.LoadContent(this.Content);
            }
            mouseTexture = this.Content.Load<Texture2D>("Skin/cursor");
        }

       
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            mCurrentMouseState = Mouse.GetState();
            /*if (Mouse.GetState().LeftButton == ButtonState.Pressed || Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                onMouseClick(bassPosition());
            }*/

            // TODO: Add your update logic here
            mousePosition.X = mCurrentMouseState.X-32;
            mousePosition.Y = mCurrentMouseState.Y-32;
            Draw(gameTime);

            mPreviousMouseState = mCurrentMouseState;

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            gameField.Draw(spriteBatch);
            spriteBatch.Draw(mouseTexture, new Vector2(mousePosition.X, mousePosition.Y), Color.White);

            currentObjects.Clear();
            foreach (HitCircle circle in map.hitObjects)
            {
                int time = bassPosition();
                if (time >= circle.hitTime - map.getRealtimeAR() &&
                        time <= circle.hitTime + 50)
                {
                    currentObjects.Add(circle);
                    circle.Scale = map.circleSize;
                    circle.ApproachScale = circle.approachSize(time, map.getRealtimeAR());

                    circle.Draw(this.spriteBatch);
                }
            }

            int theTime = bassPosition();
            if ((mCurrentMouseState.LeftButton == ButtonState.Pressed && mPreviousMouseState.LeftButton == ButtonState.Released) 
                    ||(mCurrentMouseState.RightButton == ButtonState.Pressed && mPreviousMouseState.RightButton == ButtonState.Released))
            {                
                for (int i = 0; i < currentObjects.Count; i++)
                {
                    HitCircle hc = (HitCircle)currentObjects.ElementAt(i);
                    Rectangle rect = new Rectangle((int)hc.position.X, (int)hc.position.Y, hc.size.Width, hc.size.Height);
                    if (rect.Contains(mousePosition) && (theTime >= hc.hitTime - map.getRealtimeAR() &&
                            theTime <= hc.hitTime + 50))
                    {
                        int accuracy = checkHitTiming(hc, theTime);
                        ScoreObject scr = new ScoreObject(theTime, accuracy, this.Content);
                        if (!currentScoreObjects.Contains(scr))
                            currentScoreObjects.Add(scr);

                        foreach (ScoreObject so in currentScoreObjects)
                        {
                            so.LoadContent(this.Content);
                            so.Draw(spriteBatch, new Vector2(rect.X, rect.Y), theTime);
                        }
                        currentObjects.Remove(hc);
                    }
                }
            }            

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void FormLaunch()
        {
            msf = new MapSelectForm();
            msf.ShowDialog();
            var z = msf.DialogResult;

            if (z == System.Windows.Forms.DialogResult.OK)
            {
                initializeGame(msf.diffPath);
            }
        }

        public void bassPlay(string mp3name)
        {
            if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
            {
                stream = Bass.BASS_StreamCreateFile(mp3name, 0, 0, BASSFlag.BASS_DEFAULT);
                if (stream != 0)
                {                    
                    BASS_BFX_VOLUME volume = new BASS_BFX_VOLUME();
                    volume.fVolume = 0.5f;
                    int volumeHandle = Bass.BASS_ChannelSetFX(stream, BASSFXType.BASS_FX_BFX_VOLUME, 0);
                    //apply effect parameter
                    Bass.BASS_FXSetParameters(volumeHandle, volume);
                    //play the channel
                    Bass.BASS_ChannelPlay(stream, false);
                }
            }
        }

        public int bassPosition()
        {
            long pos = Bass.BASS_ChannelGetPosition(stream);
            double realpos = Bass.BASS_ChannelBytes2Seconds(stream, pos);
            realpos *= 1000;
            return (int)realpos;
        }

        public void initializeGame(string s)
        {           
            fr.openReader(s);
            if (fr.startRead())
            {
                map = fr.bm;
                bassPlay(msf.mp3Path);
            }
        }

        public void onMouseClick(int theTime)
        {
            for(int i = 0; i < currentObjects.Count; i++)
            {
                HitCircle hc = (HitCircle)currentObjects.ElementAt(i);
                Rectangle rect = new Rectangle((int)hc.position.X, (int)hc.position.Y, hc.size.Width, hc.size.Height);
                if (rect.Contains(mousePosition) &&  (theTime >= hc.hitTime - map.getRealtimeAR() &&
                        theTime <= hc.hitTime + 50))
                {
                    int accuracy = checkHitTiming(hc, theTime);
                    ScoreObject so = new ScoreObject(theTime, accuracy, this.Content);
                    so.LoadContent(this.Content);
                    so.Draw(this.spriteBatch, new Vector2(rect.X, rect.Y), theTime);
                    currentObjects.Remove(hc);
                }
            }
        }

        public int checkHitTiming(HitObject ho, int time)
        {
            var OD = map.overallDifficulty;
            if (Math.Abs(ho.hitTime - time) <= (198 - 10 * OD) - (60 - 2 * OD)*2)
                return 3;
            if (Math.Abs(ho.hitTime - time) <= (198 - 10 * OD) - (60 - 2 * OD))
                return 2;
            if (Math.Abs(ho.hitTime - time) <= 198 - 10 * OD)
                return 1;
            else
                return 0;
        }
    }
}
