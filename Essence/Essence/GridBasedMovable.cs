using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Essence {
	public class GridBasedMovable {

		private Essence game;

		public Vector2 dir = Dir.Down;

		private Vector2 position;
		public Vector2 Position {
			get { return position; }
			set { position = value; }
		}

		public bool moving = false;
		public int speed = 1;

		private Vector2 targetSpace;
		public Vector2 TargetSpace {
			get { return targetSpace; }
			set { if(value.X % 16 == 0 && value.Y % 16 == 0) targetSpace = value; }
		}

		public Vector2 nextDir;

		public GridBasedMovable(Essence essence) {
			game = essence;
			nextDir = dir;
			targetSpace = position;
		}

		public void SetPositionAndSnap(Vector2 pos) {
			if(pos.X % 16 != 0) pos.X = (float) Math.Round(pos.X / 16) * 16;
			if(pos.Y % 16 != 0) pos.Y = (float) Math.Round(pos.Y / 16) * 16;
			position = targetSpace = pos;
		}

		public void RecalcTarget() {
			Vector2 newPos = (position + (dir * speed)) / 16 + dir;
			targetSpace = new Vector2((float) Math.Round(newPos.X) * 16, (float) Math.Round(newPos.Y) * 16);
			//targetSpace = new Vector2(dir == Dir.Left ? position.X - speed - (position.X - speed < 0 ? 16 + ((position.X - speed) % 16) : (position.X - speed) % 16) : (dir == Dir.Right ? position.X + speed + 16 - (position.X + speed < 0 ? 16 + ((position.X + speed) % 16) : (position.X + speed) % 16) : position.X), dir == Dir.Up ? position.Y - speed - (position.Y - speed < 0 ? 16 + ((position.Y - speed) % 16) : (position.Y - speed) % 16) : (dir == Dir.Down ? position.Y + speed + 16 - (position.Y + speed < 0 ? 16 + ((position.Y + speed) % 16) : (position.Y + speed) % 16) : position.Y));
		}

		public bool AtOrPastTargetSpace(Vector2 pos) {
			return (dir == Dir.Left && pos.X <= targetSpace.X) ||
					(dir == Dir.Right && pos.X >= targetSpace.X) ||
					(dir == Dir.Up && pos.Y <= targetSpace.Y) ||
					(dir == Dir.Down && pos.Y >= targetSpace.Y);
		}

		public void UpdateMovement() {
			if((position.Equals(targetSpace) || AtOrPastTargetSpace(position)) && (nextDir != dir || moving)) {
				dir = nextDir;
				RecalcTarget();

				if(game.world.IsTerrainSolid(targetSpace)) {
					moving = false;
					targetSpace = position;
				}
			}
			if(!position.Equals(targetSpace)) moving = true;

			if(moving) {
				Vector2 movement = speed * dir;
				Vector2 newPosition = position + movement;

				if(AtOrPastTargetSpace(newPosition)) position = targetSpace;
				else position = newPosition;
				
				/*if(position.X > targetSpace.X && dir == Dir.Left) {
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
				}*/
			}
		}
	}
}
