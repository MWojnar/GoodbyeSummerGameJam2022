using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam.Objects
{
	class Menu : Entity
	{
		private bool mouseDown = false;
		private int popupState = 0, buttonOffset = 140;
		private RectangleF playRect, instRect, credRect, exitRect;

		public Menu(World world, Vector2 pos = new Vector2()) : base(world, pos) {
			playRect = new RectangleF(new Vector2(330 - world.Assets.SpritePlayButton.getWidth() / 2, 400 - world.Assets.SpritePlayButton.getHeight() / 2), new Size2(world.Assets.SpritePlayButton.getWidth(), world.Assets.SpritePlayButton.getHeight()));
			instRect = new RectangleF(new Vector2(330 - world.Assets.SpriteInstructionsButton.getWidth() / 2, 400 - world.Assets.SpriteInstructionsButton.getHeight() / 2 + buttonOffset), new Size2(world.Assets.SpriteInstructionsButton.getWidth(), world.Assets.SpriteInstructionsButton.getHeight()));
			credRect = new RectangleF(new Vector2(330 - world.Assets.SpriteCreditsButton.getWidth() / 2, 400 - world.Assets.SpriteCreditsButton.getHeight() / 2 + buttonOffset * 2), new Size2(world.Assets.SpriteCreditsButton.getWidth(), world.Assets.SpriteCreditsButton.getHeight()));
			exitRect = new RectangleF(new Vector2(330 - world.Assets.SpriteExitButton.getWidth() / 2, 400 - world.Assets.SpriteExitButton.getHeight() / 2 + buttonOffset * 3), new Size2(world.Assets.SpriteExitButton.getWidth(), world.Assets.SpriteExitButton.getHeight()));
		}

		public override void update(GameTime time, StateHandler state)
		{
			base.update(time, state);
			if (state.MouseState.LeftButton == ButtonState.Pressed)
				mouseDown = true;
			else if (mouseDown)
			{
				if (playRect.Contains(state.MouseState.Position))
					world.LoadLevel(1);
				else if (instRect.Contains(state.MouseState.Position))
					popupState = 1;
				else if (credRect.Contains(state.MouseState.Position))
					popupState = 2;
				else if (exitRect.Contains(state.MouseState.Position))
					world.Exit();
				else
					popupState = 0;
				mouseDown = false;
			}
		}

		public override void draw(GameTime time, SpriteBatch batch)
		{
			base.draw(time, batch);
			/*SpriteFont font = world.Assets.FontTest;
			string displayString = "Click anywhere to continue";
			batch.DrawString(world.Assets.FontTest, displayString, world.GetDimensions() / 2 - font.MeasureString(displayString) / 2, Color.Black);*/
			world.Assets.BackgroundTitleScreen.draw(world.GetDimensions() / 2);
			world.Assets.SpritePlayButton.draw(new Vector2(330, 400));
			world.Assets.SpriteInstructionsButton.draw(new Vector2(330, 400 + buttonOffset));
			world.Assets.SpriteCreditsButton.draw(new Vector2(330, 400 + buttonOffset * 2));
			world.Assets.SpriteExitButton.draw(new Vector2(330, 400 + buttonOffset * 3));
			if (popupState != 0)
				(popupState == 1 ? world.Assets.SpriteInstructions : world.Assets.SpriteCredits).draw(new Vector2(1263, 617));
		}
	}
}
