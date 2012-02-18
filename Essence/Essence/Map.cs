using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Essence {

	public class Map {
		public byte[,] data;
		public bool cameraOverflow = false;
		private Essence game;

		public Map(Essence essence) {
			game = essence;
		}

		public Map(String s, Essence essence) : this(s, false, essence) {}

		public Map(String s, bool canCameraOverflow, Essence essence) {
			game = essence;
			cameraOverflow = canCameraOverflow;

			Texture2D map = game.Content.Load<Texture2D>(s);
			Color[] colours = new Color[map.Width * map.Height];

			map.GetData(colours);
			data = new byte[map.Width, map.Height];

			for(int i = 0; i < map.Width; i++) for(int j = 0; j < map.Height; j++) {
					byte id = 0;
					foreach(Terrain t in Terrain.terrainList)
						if(t != null && t.readCol.Equals(colours[i + j * map.Width])) {
							id = t.terrainID;
							break;
						}
					
					data[i, j] = id;
				}

			game.world.maps.Add(this);
		}
	}
}
