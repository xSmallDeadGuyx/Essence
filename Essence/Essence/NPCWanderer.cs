using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Essence {
	public class NPCWanderer : GridBasedMovable {

		public const int MIN_WANDER_DELAY = 80;
		public const int MAX_WANDER_DELAY = 120;

		private Vector2[] dirs = { Direction.Down, Direction.Up, Direction.Left, Direction.Right };

		private void newWanderTimer() {
			wanderTimer = game.rand.Next(MIN_WANDER_DELAY, MAX_WANDER_DELAY);
		}

		private int wanderTimer;

		public NPCWanderer(Essence essence) : base(essence) {
			newWanderTimer();
			speed = 1;
		}

		public override void Update(GameTime gt) {
			if(wanderTimer > 0) {
				wanderTimer--;
				moving = false;
			}
			else {
				moving = true;
				newWanderTimer();

				NextDir = Dir;
				while(Dir == NextDir) {
					int i = game.rand.Next(4);
					NextDir = dirs[i];
				}
			}
			UpdateMovement();
		}

		public override void Draw(GameTime gt) {
			game.TheCamera.Draw(game.TheWorld.grassTex, Position, Color.Blue);
		}
	}
}
