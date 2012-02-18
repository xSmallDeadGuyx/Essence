﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Essence {
	public class Player : GridBasedMovable {
		
		private Essence game;
		private Texture2D sprite;

		public int world = 0;

		public const int walkSpeed = 1;
		public const int runSpeed = 2;

		private Rectangle[,] frames;
		private int frame;
		private bool frameInc;
		private int lastFrameChange = 0;

		public Player(Essence essence) : base(essence) {
			game = essence;
			frameInc = true;
			frame = 1;
			dir = 0;
		}

		public void loadContent() {
			sprite = game.Content.Load<Texture2D>("player");
			frames = new Rectangle[sprite.Width / 16, sprite.Height / 16];

			for(int j = 0; j <= frames.GetUpperBound(1); j++) for(int i = 0; i <= frames.GetUpperBound(0); i++)
				frames[i, j] = new Rectangle(i * 16, j * 16, 16, 16);
		}

		public void update(GameTime gt) {
			speed = game.controls.sprintPressed ? runSpeed : walkSpeed;
			moving = game.controls.leftPressed || game.controls.rightPressed || game.controls.upPressed || game.controls.downPressed;
			
			nextDir = dir;
			if(game.controls.leftPressed) nextDir = DIR_LEFT;
			if(game.controls.rightPressed) nextDir = DIR_RIGHT;
			if(game.controls.upPressed) nextDir = DIR_UP;
			if(game.controls.downPressed) nextDir = DIR_DOWN;

			updateMovement();

			game.camera.setPosition(position - new Vector2(8, 8));
		}

		public void draw(GameTime gt) {
			if(moving) {
				lastFrameChange++;
				if(lastFrameChange >= 8 - ((speed - walkSpeed) * 1.5)) {
					frame += frameInc ? 1 : -1;
					lastFrameChange = 0;
					if(frame == 0) frameInc = true; 
					if(frame == frames.GetUpperBound(1) - 1) frameInc = false;
				}
			}
			else frame = 1;

			game.camera.draw(sprite, position, frames[frame, dir], Color.White);
		}
	}
}