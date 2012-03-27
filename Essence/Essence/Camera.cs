using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Essence {
	public class Camera {

		private SpriteBatch spriteBatch;
		private Essence game;

		private Vector2 cameraPosition;

		public Vector2 Position {
			get {
				return cameraPosition;
			}
			set {
				SetPosition(value);
			}
		}

		public Camera(SpriteBatch sb, Essence essence) {
			spriteBatch = sb;
			game = essence;
			cameraPosition = new Vector2(0, 0);
		}

		private void SetPosition(Vector2 vec) {
			cameraPosition = vec - new Vector2(spriteBatch.GraphicsDevice.PresentationParameters.BackBufferWidth/2, spriteBatch.GraphicsDevice.PresentationParameters.BackBufferHeight/2); // set the position of the camera
			if(!game.TheWorld.maps[game.ThePlayer.world].CameraOverflow) { // if camera is not able to overflow on current world map
				if(cameraPosition.X < 0) cameraPosition.X = 0; // if camera is too far left, move to the minimum
				if(cameraPosition.Y < 0) cameraPosition.Y = 0; // if camera is too far up, move to the minimum
				if(cameraPosition.X + game.GraphicsDevice.PresentationParameters.BackBufferWidth > game.TheWorld.maps[game.ThePlayer.world].Data.GetUpperBound(0) * 16 + 16) cameraPosition.X = game.TheWorld.maps[game.ThePlayer.world].Data.GetUpperBound(0) * 16 + 16 - game.GraphicsDevice.PresentationParameters.BackBufferWidth; // if camera is too far to the right, move to the maximum
				if(cameraPosition.Y + game.GraphicsDevice.PresentationParameters.BackBufferHeight > game.TheWorld.maps[game.ThePlayer.world].Data.GetUpperBound(1) * 16 + 16) cameraPosition.Y = game.TheWorld.maps[game.ThePlayer.world].Data.GetUpperBound(1) * 16 + 16 - game.GraphicsDevice.PresentationParameters.BackBufferHeight; // if camera is too far to the bottom, move to the maximum
			}
		}

		public bool IsOnScreen(Texture2D tex, Vector2 pos) {
			Vector2 finalPos = pos - cameraPosition; // work out relative position on screen
			return finalPos.X + tex.Width >= 0 && finalPos.Y + tex.Height >= 0 && finalPos.X <= spriteBatch.GraphicsDevice.PresentationParameters.BackBufferWidth && finalPos.Y <= spriteBatch.GraphicsDevice.PresentationParameters.BackBufferHeight; // return if any corners of the texture will be on the screen
		}

		public bool IsOnScreen(Rectangle rect, Vector2 pos) {
			Vector2 finalPos = pos - cameraPosition; // work out relative position on screen
			return finalPos.X + rect.Width >= 0 && finalPos.Y + rect.Height >= 0 && finalPos.X <= spriteBatch.GraphicsDevice.PresentationParameters.BackBufferWidth && finalPos.Y <= spriteBatch.GraphicsDevice.PresentationParameters.BackBufferHeight; // return if any corners of the rectangle will be on the screen
		}

		public void Draw(Texture2D tex, Vector2 position, Color col) {
			if(IsOnScreen(tex, position)) spriteBatch.Draw(tex, position - cameraPosition, col); // if texture will be on the screen, draw
		}

		public void Draw(Texture2D tex, Vector2 position, Rectangle rect, Color col) {
			if(IsOnScreen(rect, position)) spriteBatch.Draw(tex, position - cameraPosition, rect, col); // if rectangle will be on screen, draw
		}
	}
}
