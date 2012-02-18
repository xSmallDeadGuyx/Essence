using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Essence {

	/*
	 * Camera:
	 * 
	 *		Class that allows drawing relative to what can be seen on the screen, to emulate a camera
	 */
	public class Camera {

		private SpriteBatch spriteBatch;
		private Essence game;

		private Vector2 cameraPosition; // position of camera

		/*
		 * Constructor:
		 *		Arguments: sb - sprite batch for drawing, essence - game instance
		 */
		public Camera(SpriteBatch sb, Essence essence) {
			spriteBatch = sb;
			game = essence;
			cameraPosition = new Vector2(0, 0);
		}

		/*
		 * setPosition:
		 *		Return: none
		 *		Arguments: vec - position to be set to
		 *		
		 *		Sets position of camera (more than just position assignment, controls overflow too)
		 */
		public void setPosition(Vector2 vec) {
			cameraPosition = vec - new Vector2(spriteBatch.GraphicsDevice.PresentationParameters.BackBufferWidth/2, spriteBatch.GraphicsDevice.PresentationParameters.BackBufferHeight/2); // set the position of the camera
			if(!game.world.maps[game.player.world].cameraOverflow) { // if camera is not able to overflow on current world map
				if(cameraPosition.X < 0) cameraPosition.X = 0; // if camera is too far left, move to the minimum
				if(cameraPosition.Y < 0) cameraPosition.Y = 0; // if camera is too far up, move to the minimum
				if(cameraPosition.X + game.GraphicsDevice.PresentationParameters.BackBufferWidth > game.world.maps[game.player.world].data.GetUpperBound(0) * 16 + 16) cameraPosition.X = game.world.maps[game.player.world].data.GetUpperBound(0) * 16 + 16 - game.GraphicsDevice.PresentationParameters.BackBufferWidth; // if camera is too far to the right, move to the maximum
				if(cameraPosition.Y + game.GraphicsDevice.PresentationParameters.BackBufferHeight > game.world.maps[game.player.world].data.GetUpperBound(1) * 16 + 16) cameraPosition.Y = game.world.maps[game.player.world].data.GetUpperBound(1) * 16 + 16 - game.GraphicsDevice.PresentationParameters.BackBufferHeight; // if camera is too far to the bottom, move to the maximum
			}
		}

		/*
		 * getPosition:
		 *		Return: position of camera
		 *		Arguments: none
		 *		
		 *		to get the position of the camera
		 */
		public Vector2 getPosition() {
			return cameraPosition;
		}

		/*
		 * isOnScreen:
		 *		Return: whether the specified position/texture is on screen
		 *		Arguments: tex - texture to check, pos - position of texture
		 *		
		 *		to check whether drawing the texture at the position will be seen on the screen
		 */
		public bool isOnScreen(Texture2D tex, Vector2 pos) {
			Vector2 finalPos = pos - cameraPosition; // work out relative position on screen
			return finalPos.X + tex.Width >= 0 && finalPos.Y + tex.Height >= 0 && finalPos.X <= spriteBatch.GraphicsDevice.PresentationParameters.BackBufferWidth && finalPos.Y <= spriteBatch.GraphicsDevice.PresentationParameters.BackBufferHeight; // return if any corners of the texture will be on the screen
		}

		/*
		 * isOnScreen:
		 *		Return: whether the specified position/rectangle is on screen
		 *		Arguments: rect - rectangle to check, pos - position of rectangle
		 *		
		 *		to check whether drawing the rectangle at the position will be seen on the screen
		 */
		public bool isOnScreen(Rectangle rect, Vector2 pos) {
			Vector2 finalPos = pos - cameraPosition; // work out relative position on screen
			return finalPos.X + rect.Width >= 0 && finalPos.Y + rect.Height >= 0 && finalPos.X <= spriteBatch.GraphicsDevice.PresentationParameters.BackBufferWidth && finalPos.Y <= spriteBatch.GraphicsDevice.PresentationParameters.BackBufferHeight; // return if any corners of the rectangle will be on the screen
		}

		/*
		 * draw:
		 *		Return: none
		 *		Arguments: tex - texture to draw, position - position to draw at, col - colour to blend
		 *		
		 *		function to draw a texture at a specified position
		 */
		public void draw(Texture2D tex, Vector2 position, Color col) {
			if(isOnScreen(tex, position)) spriteBatch.Draw(tex, position - cameraPosition, col); // if texture will be on the screen, draw
		}

		/*
		 * draw:
		 *		Return: none
		 *		Arguments: tex - texture to draw, position - position to draw texture at, rect - section of texture to draw, col - blend colour
		 */
		public void draw(Texture2D tex, Vector2 position, Rectangle rect, Color col) {
			if(isOnScreen(rect, position)) spriteBatch.Draw(tex, position - cameraPosition, rect, col); // if rectangle will be on screen, draw
		}
	}
}
