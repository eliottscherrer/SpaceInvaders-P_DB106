﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.XInput;
using System;

namespace ShootMeUpV1
{
    public class GameRoot : Game
    {
        // Graphics
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static Texture2D Pixel;

        // Singleton of GameRoot for global access
        public static GameRoot Instance { get; private set; }

        // Screen infos
        public static Viewport Viewport => Instance.GraphicsDevice.Viewport;
        public static Vector2 ScreenSize => new(Viewport.Width, Viewport.Height);


        public GameRoot()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Instance = this;
        }

        protected override void Initialize()
        {
            base.Initialize();

            EntityManager.Add(LocalPlayer.Instance);

            EntityManager.Add(new Ennemy(new Vector2(100, 0), 20f));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Visuals.Load(Content);

            // Create a 1x1 red pixel texture
            Pixel = new Texture2D(GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.Red });
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();
            EntityManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin(/*SpriteSortMode.Texture, BlendState.Additive*/);
            {
                EntityManager.Draw(_spriteBatch);
                //_spriteBatch.DrawString(Visuals.SpriteFont, "onglier", new Vector2(100, 1000), Color.Black);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
