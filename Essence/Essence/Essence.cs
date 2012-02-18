using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Essence {

	/*
	 * Essence:
	 *		
	 *		The main game class, where all the magic happens
	 */
	public class Essence : Microsoft.Xna.Framework.Game {
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public Player player;
		public Camera camera;
		public World world;
		public Controls controls = new Controls();

		/*
		 * Constructor:
		 * 
		 *		Used to initialize the graphics and setup basic screen window
		 */
		public Essence() {
			graphics = new GraphicsDeviceManager(this); // initialize graphics
			Content.RootDirectory = "Content"; // set content root

			graphics.PreferredBackBufferWidth = 656; // set screen width and height
			graphics.PreferredBackBufferHeight = 496;
			graphics.IsFullScreen = false; // set not fullscreen
			graphics.ApplyChanges(); // apply the above changes (refresh the screen buffer)
			Window.Title = "Essence"; // set the window title
		}

		/*
		 * Initialize:
		 *		Return: none
		 *		Arguments: none
		 *		
		 *		Used to initialize base variables
		 */
		protected override void Initialize() {
			player = new Player(this); // setup player and world global variables
			world = new World(this);

			base.Initialize(); // call initialize of parent class (Microsoft.Xna.Framework.Game)
		}

		/*
		 * LoadContent:
		 *		Return: none
		 *		Arguments: none
		 *		
		 *		Used to load any sprites and other resources
		 */
		protected override void LoadContent() {
			spriteBatch = new SpriteBatch(GraphicsDevice); // load spritebatch and camera
			camera = new Camera(spriteBatch, this);

			world.loadWorld(); // tell objects to load their content
			player.loadContent();
		}

		/*
		 * UnloadContent:
		 *		Return: none
		 *		Arguments: none
		 *		
		 *		Used to unload any non-garbage-collected resources
		 */
		protected override void UnloadContent() {
			// TODO: Unload any non ContentManager content here
		}

		/*
		 * Update:
		 *		Return: none
		 *		Arguments: gameTime - allows for finding elapsed time and speed of the game
		 *		
		 *		Called on every frame for any game logic, the main executed code
		 */
		protected override void Update(GameTime gameTime) {
			if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) // if back button is pressed, quit
				this.Exit();

			world.update(gameTime); // call update of other objects
			player.update(gameTime);

			base.Update(gameTime); // call update of parent class
		}

		/*
		 * Draw:
		 *		Return: none
		 *		Arguments: gameTime - allows for finding elapsed time and speed of the game
		 */
		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.Black); // set background to black and reset screen
			
			spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.LinearWrap, null, null); // start the sprite batch and wrap state

			world.draw(gameTime); //call the draw functions of other objects
			player.draw(gameTime);

			spriteBatch.End(); // close the sprite batch

			base.Draw(gameTime); // call Draw of parent class
		}
	}
}
