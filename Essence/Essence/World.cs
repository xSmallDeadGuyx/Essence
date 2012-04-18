using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Essence {
	public class World {
		public Essence game;

		public Texture2D grassTex;

		public List<Map> maps = new List<Map>();

		public World(Essence essence) {
			Terrain.game = game = essence;
		}

		public void LoadWorld() {
			grassTex = game.Content.Load<Texture2D>("grass");
			foreach(Terrain t in Terrain.terrainList) if(t != null) t.Load();

			Map map = new Map("map.bin", game);
			NPCWanderer steve = new NPCWanderer(game);
			steve.SetPositionAndSnap(new Vector2(64, 64));
			map.movables.Add(steve);
		}

		public void Draw(GameTime gt) {
			game.TheCamera.Draw(grassTex, Vector2.Zero, new Rectangle(0, 0, maps[game.ThePlayer.world].Data.GetLength(0) * 16, maps[game.ThePlayer.world].Data.GetLength(1) * 16), Color.White);
			maps[game.ThePlayer.world].DrawMovables(gt);

			Vector2 cameraPos = game.TheCamera.Position;
			for(int i = (int) cameraPos.X / 16; i < ((int) (cameraPos.X + game.GraphicsDevice.PresentationParameters.BackBufferWidth) / 16) + 1; i++) for(int j = (int) cameraPos.Y / 16; j < (int) (cameraPos.Y + game.GraphicsDevice.PresentationParameters.BackBufferHeight) / 16 + 1; j++)
					if(i < maps[game.ThePlayer.world].Data.GetLength(0) && j < maps[game.ThePlayer.world].Data.GetLength(1) && i >= 0 && j >= 0 && Terrain.terrainList[maps[game.ThePlayer.world].Data[i, j]] != null)
					Terrain.terrainList[maps[game.ThePlayer.world].Data[i, j]].Draw(gt, new Vector2(i, j));
		}

		public void Update(GameTime gt) {
			game.Controls.Update(gt);
			maps[game.ThePlayer.world].UpdateMovables(gt);

			Vector2 cameraPos = game.TheCamera.Position;
			for(int i = (int) cameraPos.X / 16; i < ((int) (cameraPos.X + game.GraphicsDevice.PresentationParameters.BackBufferWidth) / 16) + 1; i++) for(int j = (int) cameraPos.Y / 16; j < (int) (cameraPos.Y + game.GraphicsDevice.PresentationParameters.BackBufferHeight) / 16 + 1; j++)
					if(i < maps[game.ThePlayer.world].Data.GetLength(0) && j < maps[game.ThePlayer.world].Data.GetLength(1) && i >= 0 && j >= 0 && Terrain.terrainList[maps[game.ThePlayer.world].Data[i, j]] != null)
						Terrain.terrainList[maps[game.ThePlayer.world].Data[i, j]].Update(gt, new Vector2(i, j));
		}

		public bool IsTerrainSolid(Vector2 pos) {
			return pos.X / 16 < maps[game.ThePlayer.world].Data.GetLength(0) && pos.Y / 16 < maps[game.ThePlayer.world].Data.GetLength(1) && pos.X / 16 >= 0 && pos.Y / 16 >= 0 && Terrain.terrainList[maps[game.ThePlayer.world].Data[(int) (pos.X / 16), (int) (pos.Y / 16)]] != null && Terrain.terrainList[maps[game.ThePlayer.world].Data[(int) (pos.X / 16), (int) (pos.Y / 16)]].isSolid;
		}

		public bool IsPositionOccupied(Vector2 pos) {
			foreach(GridBasedMovable m in maps[game.ThePlayer.world].movables) if(m.Position == pos) return true;
			return IsTerrainSolid(pos);
		}
	}
}
