using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Essence {
	public class GridBasedMovable {

		private Essence game;

		public const int DIR_DOWN = 0;
		public const int DIR_UP = 1;
		public const int DIR_LEFT = 2;
		public const int DIR_RIGHT = 3;

		public int dir = DIR_DOWN;

		public Vector2 position = new Vector2(320, 320);
		public bool moving = false;
		public int speed = 1;
		private Vector2 targetSpace;

		public int nextDir;

		public GridBasedMovable(Essence essence) {
			game = essence;
			nextDir = dir;
			targetSpace = position;
		}

		public void recalcTarget() {
			targetSpace = new Vector2(dir == DIR_LEFT ? position.X - speed - (position.X - speed < 0 ? 16 + ((position.X - speed) % 16) : (position.X - speed) % 16) : (dir == DIR_RIGHT ? position.X + speed + 16 - (position.X + speed < 0 ? 16 + ((position.X + speed) % 16) : (position.X + speed) % 16) : position.X), dir == DIR_UP ? position.Y - speed - (position.Y - speed < 0 ? 16 + ((position.Y - speed) % 16) : (position.Y - speed) % 16) : (dir == DIR_DOWN ? position.Y + speed + 16 - (position.Y + speed < 0 ? 16 + ((position.Y + speed) % 16) : (position.Y + speed) % 16) : position.Y));
		}

		public bool atOrPastTargetSpace() {
			return (dir == DIR_LEFT && position.X <= targetSpace.X) ||
					(dir == DIR_RIGHT && position.X >= targetSpace.X) ||
					(dir == DIR_UP && position.Y <= targetSpace.Y) ||
					(dir == DIR_DOWN && position.Y >= targetSpace.Y);
		}

		public void updateMovement() {
			if((position.Equals(targetSpace) || atOrPastTargetSpace()) && (nextDir != dir || moving)) {
				dir = nextDir;
				recalcTarget();

				if(game.world.isTerrainSolid(targetSpace)) {
					moving = false;
					targetSpace = position;
				}
			}
			if(!position.Equals(targetSpace)) moving = true;

			if(moving) {
				Console.WriteLine(speed + "m/s - (" + position.X + ", " + position.Y + "), (" + targetSpace.X + ", " + targetSpace.Y + ")");
				if(position.X > targetSpace.X && dir == DIR_LEFT) {
					if(nextDir == DIR_LEFT) position.X -= speed;
					else position.X = Math.Abs(position.X - targetSpace.X) <= speed ? targetSpace.X : position.X - speed;
				}
				if(position.X < targetSpace.X && dir == DIR_RIGHT) {
					if(nextDir == DIR_RIGHT) position.X += speed;
					else position.X = Math.Abs(position.X - targetSpace.X) <= speed ? targetSpace.X : position.X + speed;
				}
				if(position.Y > targetSpace.Y && dir == DIR_UP) {
					if(nextDir == DIR_UP) position.Y -= speed;
					else position.Y = Math.Abs(position.Y - targetSpace.Y) <= speed ? targetSpace.Y : position.Y - speed;
				}
				if(position.Y < targetSpace.Y && dir == DIR_DOWN) {
					if(nextDir == DIR_DOWN) position.Y += speed;
					else position.Y = Math.Abs(position.Y - targetSpace.Y) <= speed ? targetSpace.Y : position.Y + speed;
				}
			}
		}
	}
}
