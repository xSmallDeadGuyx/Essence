using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Essence {
	public class World {
		public Essence game;

		private Texture2D grassTex;

		public List<Map> maps = new List<Map>();

		public World(Essence essence) {
			Terrain.game = game = essence;
		}

		public void loadWorld() {
			grassTex = game.Content.Load<Texture2D>("grass");
			foreach(Terrain t in Terrain.terrainList) if(t != null) t.load();
			maps.Add(new Map("map", game));
		}

		public void draw(GameTime gt) {
			game.camera.draw(grassTex, Vector2.Zero, new Rectangle(0, 0, maps[game.player.world].data.GetLength(0) * 16, maps[game.player.world].data.GetLength(1) * 16), Color.White);

			Vector2 cameraPos = game.camera.getPosition();
			for(int i = (int) cameraPos.X / 16; i < ((int) (cameraPos.X + game.GraphicsDevice.PresentationParameters.BackBufferWidth) / 16) + 1; i++) for(int j = (int) cameraPos.Y / 16; j < (int) (cameraPos.Y + game.GraphicsDevice.PresentationParameters.BackBufferHeight) / 16 + 1; j++)
					if(i < maps[game.player.world].data.GetLength(0) && j < maps[game.player.world].data.GetLength(1) && i >= 0 && j >= 0 && Terrain.terrainList[maps[game.player.world].data[i, j]] != null)
					Terrain.terrainList[maps[game.player.world].data[i, j]].draw(gt, new Vector2(i, j));
		}

		public void update(GameTime gt) {
			game.controls.update(gt);

			Vector2 cameraPos = game.camera.getPosition();
			for(int i = (int) cameraPos.X / 16; i < ((int) (cameraPos.X + game.GraphicsDevice.PresentationParameters.BackBufferWidth) / 16) + 1; i++) for(int j = (int) cameraPos.Y / 16; j < (int) (cameraPos.Y + game.GraphicsDevice.PresentationParameters.BackBufferHeight) / 16 + 1; j++)
					if(i < maps[game.player.world].data.GetLength(0) && j < maps[game.player.world].data.GetLength(1) && i >= 0 && j >= 0 && Terrain.terrainList[maps[game.player.world].data[i, j]] != null)
						Terrain.terrainList[maps[game.player.world].data[i, j]].update(gt, new Vector2(i, j));
		}

		public bool isTerrainSolid(Vector2 pos) {
			return pos.X / 16 < maps[game.player.world].data.GetLength(0) && pos.Y / 16 < maps[game.player.world].data.GetLength(1) && pos.X / 16 >= 0 && pos.Y / 16 >= 0 && Terrain.terrainList[maps[game.player.world].data[(int) (pos.X / 16), (int) (pos.Y / 16)]] != null && Terrain.terrainList[maps[game.player.world].data[(int) (pos.X / 16), (int) (pos.Y / 16)]].isSolid;
		}
	}
}
