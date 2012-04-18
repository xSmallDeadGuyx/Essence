using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Essence {
	public class Player : GridBasedMovable {
		
		private Texture2D sprite;

		public int world = 0;

		public const int walkSpeed = 1;
		public const int runSpeed = 2;

		private Rectangle[,] frames;
		private int frame;
		private bool frameInc;
		private int lastFrameChange = 0;

		public Player(Essence essence) : base(essence) {
			frameInc = true;
			frame = 1;
			Dir = Direction.Down;
		}

		public void ChangeWorld(int id) {
			Map nextMap = game.TheWorld.maps[id];
			if(nextMap != null && id != world) {
				Position = nextMap.GetPlayerStartPos(world);
				world = id;
			}
		}

		public override void LoadContent() {
			sprite = game.Content.Load<Texture2D>("player");
			frames = new Rectangle[sprite.Width / 16, sprite.Height / 16];

			for(int j = 0; j <= frames.GetUpperBound(1); j++) for(int i = 0; i <= frames.GetUpperBound(0); i++)
				frames[i, j] = new Rectangle(i * 16, j * 16, 16, 16);

			SetPositionAndSnap(game.TheWorld.maps[world].GetPlayerStartPos(world));
		}

		public override void Update(GameTime gt) {
			speed = game.Controls.SprintPressed ? runSpeed : walkSpeed;
			moving = game.Controls.LeftPressed || game.Controls.RightPressed || game.Controls.UpPressed || game.Controls.DownPressed;
			
			NextDir = Dir;
			if(game.Controls.LeftPressed) NextDir = Direction.Left;
			if(game.Controls.RightPressed) NextDir = Direction.Right;
			if(game.Controls.UpPressed) NextDir = Direction.Up;
			if(game.Controls.DownPressed) NextDir = Direction.Down;

			UpdateMovement();

			game.TheCamera.Position = Position - new Vector2(8, 8);
		}

		public override void Draw(GameTime gt) {
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

			game.TheCamera.Draw(sprite, Position, frames[frame, Direction.ToFrame(Dir)], Color.White);
		}
	}
}
