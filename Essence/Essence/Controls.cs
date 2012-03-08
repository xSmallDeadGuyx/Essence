using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Essence {
	public class Controls {

		public bool leftPressed = false;
		public bool rightPressed = false;
		public bool upPressed = false;
		public bool downPressed = false;
		public bool sprintPressed = false;

		public bool leftPressedPrev = false;
		public bool rightPressedPrev = false;
		public bool upPressedPrev = false;
		public bool downPressedPrev = false;
		public bool sprintPressedPrev = false;

		public Keys leftKey = Keys.A;
		public Keys rightKey = Keys.D;
		public Keys upKey = Keys.W;
		public Keys downKey = Keys.S;
		public Keys sprintKey = Keys.LeftShift;

		public void Update(GameTime gt) {
			leftPressedPrev = leftPressed;
			rightPressedPrev = rightPressed;
			upPressedPrev = upPressed;
			downPressedPrev = downPressed;
			sprintPressedPrev = sprintPressed;

			KeyboardState kb = Keyboard.GetState();

			leftPressed = kb.IsKeyDown(leftKey);
			rightPressed = kb.IsKeyDown(rightKey);
			upPressed = kb.IsKeyDown(upKey);
			downPressed = kb.IsKeyDown(downKey);
			sprintPressed = kb.IsKeyDown(sprintKey);
		}
	}
}
