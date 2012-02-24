using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Essence {
	public class Terrain {

		public static Terrain[] terrainList = new Terrain[256];
		public static Essence game;

		public static TerrainSided rock = (TerrainSided) new TerrainSided(1).addSide(1).setTexture("rock").setReadColour(Color.Black).setSolid(true);
		public static TerrainSided path = (TerrainSided) new TerrainSided(2).addSide(2).setTexture("path").setReadColour(new Color(1.0F, 1.0F, 0.5F)).setSolid(false);
		public static TerrainSided building1 = (TerrainSided) new TerrainSided(3).addSide(3).addSide(4).setTexture("building1").setReadColour(Color.Blue).setSolid(true);
		public static Terrain building1door = new Terrain(4).setTexture("door1").setReadColour(new Color(0.5F, 0.5F, 1.0F)).setSolid(false);
		public static Terrain bin = new Terrain(5).setTexture("bin").setReadColour(new Color(0.5F, 0.5F, 0.5F)).setSolid(true);
		//public static TerrainSided template = (TerrainSided) new TerrainSided(6).addSide(6).setTexture("template").setReadColour(new Color(192, 192, 192)).setSolid(true);

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

		public Terrain setSolid(bool s) {
			isSolid = s;
			return this;
		}
	}
}
