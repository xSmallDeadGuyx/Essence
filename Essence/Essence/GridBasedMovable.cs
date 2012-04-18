using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Essence {
	public class GridBasedMovable {

		protected Essence game;

		public Vector2 Dir = Direction.Down;

		private Vector2 position;
		public Vector2 Position {
			get { return position; }
			set { position = value; }
		}

		public bool moving = false;
		public int speed = 1;

		public bool isSolid = true;

		public virtual void Update(GameTime gt) {}
		public virtual void LoadContent() {}
		public virtual void Draw(GameTime gt) { }

		private Vector2 targetSpace;
		public Vector2 TargetSpace {
			get { return targetSpace; }
			set { if(value.X % 16 == 0 && value.Y % 16 == 0) targetSpace = value; }
		}

		public Vector2 NextDir;

		public GridBasedMovable(Essence essence) {
			game = essence;
			NextDir = Dir;
			targetSpace = position;
		}

		public void SetPositionAndSnap(Vector2 pos) {
			if(pos.X % 16 != 0) pos.X = (float) Math.Round(pos.X / 16) * 16;
			if(pos.Y % 16 != 0) pos.Y = (float) Math.Round(pos.Y / 16) * 16;
			position = targetSpace = pos;
		}

		public void RecalcTarget() {
			Vector2 newPos = (position + (Dir * speed)) / 16 + Dir;
			targetSpace = new Vector2((float) Math.Round(newPos.X) * 16, (float) Math.Round(newPos.Y) * 16);
		}

		public bool AtOrPastTargetSpace(Vector2 pos) {
			return (Dir == Direction.Left && pos.X <= targetSpace.X) ||
					(Dir == Direction.Right && pos.X >= targetSpace.X) ||
					(Dir == Direction.Up && pos.Y <= targetSpace.Y) ||
					(Dir == Direction.Down && pos.Y >= targetSpace.Y);
		}

		public void UpdateMovement() {
			if((position.Equals(targetSpace) || AtOrPastTargetSpace(position)) && (NextDir != Dir || moving)) {
				Dir = NextDir;
				RecalcTarget();

				if(game.TheWorld.IsPositionOccupied(targetSpace)) {
					moving = false;
					targetSpace = position;
				}
			}
			if(!position.Equals(targetSpace)) moving = true;

			if(moving) {
				Vector2 movement = speed * Dir;
				Vector2 newPosition = position + movement;

				if(AtOrPastTargetSpace(newPosition)) position = targetSpace;
				else position = newPosition;
			}
		}
	}
}
