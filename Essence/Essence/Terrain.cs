using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Essence {
	public class Terrain {

		public static Terrain[] terrainList = new Terrain[256];
		public static Essence game;

		public static TerrainSided rock = (TerrainSided) new TerrainSided(1).setTexture("rock").setReadColour(Color.Black);
		public static TerrainSided path = (TerrainSided) new TerrainSided(2).setTexture("path").setReadColour(new Color(1.0F, 1.0F, 0.5F));
		public static Terrain wood = new Terrain(3).setTexture("wood").setReadColour(Color.Blue);
		public static Terrain woodFloor = new Terrain(4).setTexture("woodfloor").setReadColour(new Color(0.5F, 0.5F, 1.0F));

		public bool isSolid = false;
		public byte terrainID;
		public Color readCol;

		private string texStr = "";

		public Texture2D texture;

		public Terrain(byte id) {
			terrainList[id] = this;
			terrainID = id;
		}

		public void load() {
			texture = game.Content.Load<Texture2D>(texStr);
		}

		public virtual void update(GameTime gt, Vector2 pos) {}

		public virtual void draw(GameTime gt, Vector2 pos) {
			game.camera.draw(texture, new Vector2(pos.X * 16, pos.Y * 16), Color.White);
		}

		public Terrain setTexture(string s) {
			texStr = s;
			return this;
		}

		public Terrain setReadColour(Color c) {
			readCol = c;
			return this;
		}
	}
}
