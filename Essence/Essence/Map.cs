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
			int w = Data.GetLength(0);
			int h = Data.GetLength(1);
			byte[] binaryData = new byte[w * h + 4];
			binaryData[0] = (byte) (w / 256);
			binaryData[1] = (byte) (w % 256);
			binaryData[2] = (byte) (h / 256);
			binaryData[3] = (byte) (h % 256);

			for(int i = 0; i < w; i++) for(int j = 0; j < h; j++)
				binaryData[i + j * w + 4] = Data[i, j];

			FileStream fs = File.Create(fileName, binaryData.Length, FileOptions.None);
			BinaryWriter bw = new BinaryWriter(fs);
			bw.Write(binaryData);
			bw.Close();
			fs.Close();
		}

		public void LoadMap(String fileName) {
			BinaryReader b = new BinaryReader(File.Open(fileName, FileMode.Open));
			int length = (int) b.BaseStream.Length;
			byte[] binaryData = b.ReadBytes(length);
			int w = binaryData[0] * 256 + binaryData[1];
			int h = binaryData[2] * 256 + binaryData[3];
			Data = new byte[w, h];

			for(int i = 0; i < w; i++) for(int j = 0; j < h; j++)
				Data[i, j] = binaryData[i + j * w + 4];
		}
	}
}
