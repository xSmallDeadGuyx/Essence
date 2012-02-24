using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Essence {
	public class Essence : Microsoft.Xna.Framework.Game {

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public Player player;
		public Camera camera;
		public World world;
		public Controls controls = new Controls();

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
			player = new Player(this);
			world = new World(this);

			base.Initialize();
		}

		protected override void LoadContent() {
			spriteBatch = new SpriteBatch(GraphicsDevice);
			camera = new Camera(spriteBatch, this);

			world.loadWorld();
			player.loadContent();
		}

		protected override void UnloadContent() {
			// TODO: Unload any non ContentManager content here
		}

		protected override void Update(GameTime gameTime) {
			if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			world.update(gameTime);
			player.update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.Black);
			
			spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.LinearWrap, null, null);

			world.draw(gameTime);
			player.draw(gameTime);

			spriteBatch.End();

			int fps = 0;
			fps = (int) Math.Round((1.0F / gameTime.ElapsedGameTime.Milliseconds) * 1000.0F);

			Window.Title = "Essence - " + fps;

			base.Draw(gameTime);
		}
	}
}
