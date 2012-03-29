using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;

namespace Essence {

	public class Map {
		public byte[,] Data;
		public bool CameraOverflow = false;
		private Essence game;

		public Map(Essence essence) {
			game = essence;
			game.TheWorld.maps.Add(this);
		}

		public Map(String s, Essence essence) : this(s, false, essence) {}

		public Vector2 GetPlayerStartPos(int lastWorld) {
			return new Vector2((Data.GetLength(0) / 2) * 16, (Data.GetLength(1) / 2) * 16);
		}

		public Map(String s, bool canCameraOverflow, Essence essence) {
			game = essence;
			CameraOverflow = canCameraOverflow;
			LoadMap(s);
			game.TheWorld.maps.Add(this);
		}

		public void LoadMapFromImage(String fileName) {
			Texture2D map = game.Content.Load<Texture2D>(fileName);
			Color[] colours = new Color[map.Width * map.Height];

			map.GetData(colours);
			Data = new byte[map.Width, map.Height];

			for(int i = 0; i < map.Width; i++) for(int j = 0; j < map.Height; j++) {
				byte id = 0;
				foreach(Terrain t in Terrain.terrainList)
					if(t != null && t.readCol.Equals(colours[i + j * map.Width])) {
						id = t.terrainID;
						break;
					}

				Data[i, j] = id;
			}
		}

		public void SaveMap(String fileName) {
			Int16 w = (Int16) Data.GetLength(0);
			Int16 h = (Int16) Data.GetLength(1);
			int size = w * h + 2 * sizeof(Int16);
			FileStream fs = File.Create(fileName, size, FileOptions.None);
			BinaryWriter bw = new BinaryWriter(fs);
			bw.Write(w); bw.Write(h);

			for(int j = 0; j < h; j++) for(int i = 0; i < w; i++)
				bw.Write(Data[i, j]);

			bw.Close();
			fs.Close();
		}

		public void LoadMap(String fileName) {
			BinaryReader br = new BinaryReader(File.Open(fileName, FileMode.Open));
			Int16 w = br.ReadInt16();
			Int16 h = br.ReadInt16();
			Data = new byte[w, h];

			for(int j = 0; j < h; j++) for(int i = 0; i < w; i++)
				Data[i, j] = br.ReadByte();
		}
	}
}
