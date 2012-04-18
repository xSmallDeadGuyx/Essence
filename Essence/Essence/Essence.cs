using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Essence {
	public class Essence : Microsoft.Xna.Framework.Game {

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public Random rand = new Random();

		public Player ThePlayer;
		public Camera TheCamera;
		public World TheWorld;
		public Controller Controls = new Controller();

		public Essence() {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			graphics.PreferredBackBufferWidth = 656;
			graphics.PreferredBackBufferHeight = 496;
			graphics.IsFullScreen = false;
			graphics.ApplyChanges();
			Window.Title = "Essence";
		}

		protected override void Initialize() {
			ThePlayer = new Player(this);
			TheWorld = new World(this);

			base.Initialize();
		}

		protected override void LoadContent() {
			spriteBatch = new SpriteBatch(GraphicsDevice);
			TheCamera = new Camera(spriteBatch, this);

			TheWorld.LoadWorld();
			ThePlayer.LoadContent();
		}

		protected override void UnloadContent() {
			// TODO: Unload any non ContentManager content here
		}

		protected override void Update(GameTime gameTime) {
			if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			TheWorld.Update(gameTime);
			ThePlayer.Update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.Black);
			
			spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.LinearWrap, null, null);

			TheWorld.Draw(gameTime);
			ThePlayer.Draw(gameTime);

			spriteBatch.End();

			int fps = 0;
			fps = (int) Math.Round((1.0F / gameTime.ElapsedGameTime.Milliseconds) * 1000.0F);

			Window.Title = "Essence - " + fps;

			base.Draw(gameTime);
		}
	}
}
