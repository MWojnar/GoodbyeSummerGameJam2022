using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam
{
	public class Sprite
	{

		private SpriteBatch spriteBatch;
		private Texture2D texture;
		private int columns, rows, frames;
		private float frameWidth, frameHeight;
		private Vector2 origin;

		public Sprite(SpriteBatch spriteBatch, Texture2D texture, int? columns = null, int? rows = null, int? frames = null, Vector2? origin = null)
		{
			this.spriteBatch = spriteBatch;
			this.texture = texture;
			this.columns = columns.HasValue ? columns.Value : 1;
			this.rows = rows.HasValue ? rows.Value : 1;
			this.frames = frames.HasValue ? frames.Value : this.columns * this.rows;
			if (frames > this.columns * this.rows)
				frames = this.columns * this.rows;
			frameWidth = (float)texture.Width / (float)this.columns;
			frameHeight = (float)texture.Height / (float)this.rows;
			setOrigin(origin.HasValue ? origin.Value : new Vector2(frameWidth / 2, frameHeight / 2));
		}

		public void setOrigin(Vector2 origin)
		{
			this.origin = new Vector2(origin.X, origin.Y);
		}

		public Vector2 getOrigin()
		{
			return new Vector2(origin.X, origin.Y);
		}

		public void draw(Vector2 pos, float depth = 0, int frame = 0, float rotation = 0, float scale = 1, Color? color = null)
		{
			frame %= frames;
			Vector2 offset = new Vector2((frame % columns) * frameWidth, (frame / columns) * frameHeight);
			spriteBatch.Draw(texture, pos, new Rectangle((int)offset.X, (int)offset.Y, (int)frameWidth, (int)frameHeight), color.HasValue ? color.Value : Color.White, rotation, origin, scale, SpriteEffects.None, depth);
		}

	}
}
