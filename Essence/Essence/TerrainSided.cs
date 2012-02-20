using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Essence {
	public class TerrainSided : Terrain {

		public TerrainSided(byte id) : base(id) {}

		public List<byte> sides = new List<byte>();

		public TerrainSided addSide(byte sideID) {
			sides.Add(sideID);
			return this;
		}

		public bool isSide(byte id) {
			return sides.Contains(id);
		}

		public byte getSides(Vector2 pos) {
			byte sides = 0;
			if(pos.Y == 0 || isSide(game.world.maps[game.player.world].data[(int) pos.X, (int) pos.Y - 1])) sides += 1;
			if(pos.X == game.world.maps[game.player.world].data.GetUpperBound(0) || isSide(game.world.maps[game.player.world].data[(int) pos.X + 1, (int) pos.Y])) sides += 2;
			if(pos.Y == game.world.maps[game.player.world].data.GetUpperBound(1) || isSide(game.world.maps[game.player.world].data[(int) pos.X, (int) pos.Y + 1])) sides += 4;
			if(pos.X == 0 || isSide(game.world.maps[game.player.world].data[(int) pos.X - 1, (int) pos.Y])) sides += 8;
			return sides;
		}

		public override void draw(GameTime gt, Vector2 pos) {
			byte sides = getSides(pos);
			game.camera.draw(texture, new Vector2(pos.X * 16, pos.Y * 16), new Rectangle(sides * 16, 0, 16, 16), Color.White);

			if((sides & 3) == 3 && pos.Y != 0 && pos.X != game.world.maps[game.player.world].data.GetUpperBound(0) && !isSide(game.world.maps[game.player.world].data[(int) pos.X + 1, (int) pos.Y - 1]))
				game.camera.draw(texture, new Vector2(pos.X * 16 + 8, pos.Y * 16), new Rectangle(264, 0, 8, 8), Color.White);
			if((sides & 6) == 6 && pos.Y != game.world.maps[game.player.world].data.GetUpperBound(1) && pos.X != game.world.maps[game.player.world].data.GetUpperBound(0) && !isSide(game.world.maps[game.player.world].data[(int) pos.X + 1, (int) pos.Y + 1]))
				game.camera.draw(texture, new Vector2(pos.X * 16 + 8, pos.Y * 16 + 8), new Rectangle(264, 8, 8, 8), Color.White);
			if((sides & 9) == 9 && pos.Y != 0 && pos.X != 0 && !isSide(game.world.maps[game.player.world].data[(int) pos.X - 1, (int) pos.Y - 1]))
				game.camera.draw(texture, new Vector2(pos.X * 16, pos.Y * 16), new Rectangle(256, 0, 8, 8), Color.White);
			if((sides & 12) == 12 && pos.Y != game.world.maps[game.player.world].data.GetUpperBound(1) && pos.X != 0 && !isSide(game.world.maps[game.player.world].data[(int) pos.X - 1, (int) pos.Y + 1]))
				game.camera.draw(texture, new Vector2(pos.X * 16, pos.Y * 16 + 8), new Rectangle(256, 8, 8, 8), Color.White);
		}
	}
}
