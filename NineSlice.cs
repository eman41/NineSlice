using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class NineSlice : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public NineSlice()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        private Mouse Mouse
        {
            get; set;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _panel = Content.Load<Texture2D>("UI/metalPanel");
            _sideWidth = _panel.Width / 2;
            _left = new Rectangle(0, 0, _sideWidth, _panel.Height);
            _right = new Rectangle(_sideWidth, 0, _sideWidth, _panel.Height);

            Mouse = new Mouse(Content.Load<Texture2D>("UI/cursor"));
            _guide1 = new Guide(new Rectangle
            {
                X = 0,
                Y = 0,
                Width = graphics.PreferredBackBufferWidth,
                Height = graphics.PreferredBackBufferHeight,
            })
            {
                Color = Color.LimeGreen
            };
        }

        Texture2D _panel;
        private int _sideWidth;
        private Rectangle _left;
        private Rectangle _right;

        private Guide _guide1;

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Mouse.Update(gameTime);
            _guide1.Update(Mouse);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(
                _panel, null, _left, _left);
            spriteBatch.Draw(
                _panel, null, _right, _right);

            _guide1.Draw(spriteBatch);
            Mouse.Draw(spriteBatch);
            
            spriteBatch.End();

            
            base.Draw(gameTime);
        }
    }
}
