using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Essence {
	public class Player : GridBasedMovable {
		
		private Essence game;
		private Texture2D sprite;

		public int world = 0;

		public const int walkSpeed = 1;
		public const int runSpeed = 2;

		private Rectangle[,] frames;
		private int frame;
		private bool frameInc;
		private int lastFrameChange = 0;

		public Player(Essence essence) : base(essence) {
			game = essence;
			frameInc = true;
			frame = 1;
			dir = Dir.Down;
		}

		public void ChangeWorld(int id) {
			Map nextMap = game.world.maps[id];
			if(nextMap != null && id != world) {
				Position = nextMap.GetPlayerStartPos(world);
				world = id;
			}
		}

		public void LoadContent() {
			sprite = game.Content.Load<Texture2D>("player");
			frames = new Rectangle[sprite.Width / 16, sprite.Height / 16];

			for(int j = 0; j <= frames.GetUpperBound(1); j++) for(int i = 0; i <= frames.GetUpperBound(0); i++)
				frames[i, j] = new Rectangle(i * 16, j * 16, 16, 16);

			SetPositionAndSnap(game.world.maps[world].GetPlayerStartPos(world));
		}

		public void Update(GameTime gt) {
			speed = game.controls.sprintPressed ? runSpeed : walkSpeed;
			moving = game.controls.leftPressed || game.controls.rightPressed || game.controls.upPressed || game.controls.downPressed;
			
			nextDir = dir;
			if(game.controls.leftPressed) nextDir = Dir.Left;
			if(game.controls.rightPressed) nextDir = Dir.Right;
			if(game.controls.upPressed) nextDir = Dir.Up;
			if(game.controls.downPressed) nextDir = Dir.Down;

			UpdateMovement();

			game.camera.Position = Position - new Vector2(8, 8);
		}

		public void Draw(GameTime gt) {
			if(moving) {
				lastFrameChange++;
				if(lastFrameChange >= 8 - ((speed - walkSpeed) * 1.5)) {
					frame += frameInc ? 1 : -1;
					lastFrameChange = 0;
					if(frame == 0) frameInc = true; 
					if(frame == frames.GetUpperBound(1) - 1) frameInc = false;
				}
			}
			else frame = 1;

			game.camera.Draw(sprite, Position, frames[frame, Dir.ToFrame(dir)], Color.White);
		}
	}
}
