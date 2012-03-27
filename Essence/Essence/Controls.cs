using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Essence {
	public class Controller {

		public bool LeftPressed = false;
		public bool RightPressed = false;
		public bool UpPressed = false;
		public bool DownPressed = false;
		public bool SprintPressed = false;

		public bool LeftPressedPrev = false;
		public bool RightPressedPrev = false;
		public bool UpPressedPrev = false;
		public bool DownPressedPrev = false;
		public bool SprintPressedPrev = false;

		public Keys LeftKey = Keys.A;
		public Keys RightKey = Keys.D;
		public Keys UpKey = Keys.W;
		public Keys DownKey = Keys.S;
		public Keys SprintKey = Keys.LeftShift;

		public void Update(GameTime gt) {
			LeftPressedPrev = LeftPressed;
			RightPressedPrev = RightPressed;
			UpPressedPrev = UpPressed;
			DownPressedPrev = DownPressed;
			SprintPressedPrev = SprintPressed;

			KeyboardState kb = Keyboard.GetState();

			LeftPressed = kb.IsKeyDown(LeftKey);
			RightPressed = kb.IsKeyDown(RightKey);
			UpPressed = kb.IsKeyDown(UpKey);
			DownPressed = kb.IsKeyDown(DownKey);
			SprintPressed = kb.IsKeyDown(SprintKey);
		}
	}
}
