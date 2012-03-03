using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Essence {
	public class GridBasedMovable {

		public enum Dir : byte {Down, Up, Left, Right};

		private Essence game;

		public Dir dir = Dir.Down;

		public Vector2 position;
		public bool moving = false;
		public int speed = 1;
		private Vector2 targetSpace;

		public Dir nextDir;

		public GridBasedMovable(Essence essence) {
			game = essence;
			nextDir = dir;
			targetSpace = position;
		}

		public void setPositionAndSnap(Vector2 pos) {
			if(pos.X % 16 != 0) pos.X = (float) Math.Round(pos.X / 16) * 16;
			if(pos.Y % 16 != 0) pos.Y = (float) Math.Round(pos.Y / 16) * 16;
			position = targetSpace = pos;
		}

		public void recalcTarget() {
			targetSpace = new Vector2(dir == Dir.Left ? position.X - speed - (position.X - speed < 0 ? 16 + ((position.X - speed) % 16) : (position.X - speed) % 16) : (dir == Dir.Right ? position.X + speed + 16 - (position.X + speed < 0 ? 16 + ((position.X + speed) % 16) : (position.X + speed) % 16) : position.X), dir == Dir.Up ? position.Y - speed - (position.Y - speed < 0 ? 16 + ((position.Y - speed) % 16) : (position.Y - speed) % 16) : (dir == Dir.Down ? position.Y + speed + 16 - (position.Y + speed < 0 ? 16 + ((position.Y + speed) % 16) : (position.Y + speed) % 16) : position.Y));
		}

		public bool atOrPastTargetSpace() {
			return (dir == Dir.Left && position.X <= targetSpace.X) ||
					(dir == Dir.Right && position.X >= targetSpace.X) ||
					(dir == Dir.Up && position.Y <= targetSpace.Y) ||
					(dir == Dir.Down && position.Y >= targetSpace.Y);
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
				if(position.X > targetSpace.X && dir == Dir.Left) {
					if(nextDir == Dir.Left) position.X -= speed;
					else position.X = Math.Abs(position.X - targetSpace.X) <= speed ? targetSpace.X : position.X - speed;
				}
				if(position.X < targetSpace.X && dir == Dir.Right) {
					if(nextDir == Dir.Right) position.X += speed;
					else position.X = Math.Abs(position.X - targetSpace.X) <= speed ? targetSpace.X : position.X + speed;
				}
				if(position.Y > targetSpace.Y && dir == Dir.Up) {
					if(nextDir == Dir.Up) position.Y -= speed;
					else position.Y = Math.Abs(position.Y - targetSpace.Y) <= speed ? targetSpace.Y : position.Y - speed;
				}
				if(position.Y < targetSpace.Y && dir == Dir.Down) {
					if(nextDir == Dir.Down) position.Y += speed;
					else position.Y = Math.Abs(position.Y - targetSpace.Y) <= speed ? targetSpace.Y : position.Y + speed;
				}
			}
		}
	}
}
