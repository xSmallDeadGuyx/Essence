using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Essence {
	public class Terrain {

		public static Terrain[] terrainList = new Terrain[256];
		public static Essence game;

		public static TerrainSided rock = (TerrainSided) new TerrainSided(1).AddSide(1).SetTexture("rock").SetReadColour(Color.Black).SetSolid(true);
		public static TerrainSided path = (TerrainSided) new TerrainSided(2).AddSide(2).SetTexture("path").SetReadColour(new Color(1.0F, 1.0F, 0.5F)).SetSolid(false);
		public static TerrainSided building1 = (TerrainSided) new TerrainSided(3).AddSide(3).AddSide(4).SetTexture("building1").SetReadColour(Color.Blue).SetSolid(true);
		public static Terrain building1door = new Terrain(4).SetTexture("door1").SetReadColour(new Color(0.5F, 0.5F, 1.0F)).SetSolid(false);
		public static Terrain bin = new Terrain(5).SetTexture("bin").SetReadColour(new Color(0.5F, 0.5F, 0.5F)).SetSolid(true);
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

		public void Load() {
			texture = game.Content.Load<Texture2D>(texStr);
		}

		public virtual void Update(GameTime gt, Vector2 pos) {}

		public virtual void Draw(GameTime gt, Vector2 pos) {
			game.TheCamera.Draw(texture, new Vector2(pos.X * 16, pos.Y * 16), Color.White);
		}

		public Terrain SetTexture(string s) {
			texStr = s;
			return this;
		}

		public Terrain SetReadColour(Color c) {
			readCol = c;
			return this;
		}

		public Terrain SetSolid(bool s) {
			isSolid = s;
			return this;
		}
	}
}
