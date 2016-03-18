using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NineSlice
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class NineSlice : Game
    {
        public NineSlice()
        {
            var gfx = new GraphicsDeviceManager(this);
            Screen = new Rectangle
            {
                Width = gfx.PreferredBackBufferWidth,
                Height = gfx.PreferredBackBufferHeight,
            };

            Content.RootDirectory = "Content";
        }

        public static Rectangle Screen
        {
            get;
            set;
        }

        private Mouse Mouse
        {
            get; set;
        }

        protected override void LoadContent()
        {
            _screenBatch = new SpriteBatch(GraphicsDevice);

            _panel = Content.Load<Texture2D>("UI/panel_brown");
            Mouse = new Mouse(Content.Load<Texture2D>("UI/cursor"));

            Rectangle r = _panel.Bounds;
            r.Width = 200;
            r.Height = 200;
            r.X = (int)((Screen.Width / 2f) - (r.Width / 2f));
            r.Y = (int)((Screen.Height / 2f) - (r.Height / 2f));

            var info = new TextureInfo
            {
                Source = _panel,
                Target = r,
                Buffers = new BufferSet(top: 24, bot: 33, left: 55, right: 45)
            };

            _sliced = new SlicedTexture(GraphicsDevice, _screenBatch, info)
            {
                Position = new Vector2(r.X, r.Y)
            };
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Mouse.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(37, 38, 38, 255));

            _screenBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);

            _sliced.Draw(_screenBatch);

            Handles.DrawHandle(_screenBatch, _sliced.Bounds, Color.LimeGreen, _sliced.Buffer);
            Mouse.Draw(_screenBatch);
            _screenBatch.End();

            
            base.Draw(gameTime);
        }

        private Texture2D _panel;
        private SlicedTexture _sliced;
        private SpriteBatch _screenBatch;
    }
}
