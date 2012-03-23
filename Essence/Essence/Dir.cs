using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Essence {
	public static class Dir {
		public static readonly Vector2 Down = new Vector2(0, 1);
		public static readonly Vector2 Up = new Vector2(0, -1);
		public static readonly Vector2 Left = new Vector2(-1, 0);
		public static readonly Vector2 Right = new Vector2(1, 0);

		public static int ToFrame(Vector2 dir) {
			if(dir == Down) return 0;
			if(dir == Up) return 1;
			if(dir == Left) return 2;
			if(dir == Right) return 3;
			return 0;
		}
	}
}
