using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoodbyeSummerGameJam
{
	public class Pallete
	{
		private Dictionary<Color, int> colorsAndWeights;
		private Random rand;

		public Pallete(Dictionary<Color, int> colorsAndWeights = null)
		{
			this.colorsAndWeights = colorsAndWeights ?? new Dictionary<Color, int>();
			rand = new Random();
		}

		public Dictionary<Color, int> GetColorsAndWeights()
		{
			return colorsAndWeights;
		}

		public Color GetRandomColor()
		{
			int total = colorsAndWeights.Values.Sum();
			double result = rand.NextDouble() * total;
			int currentStep = 0;
			foreach (KeyValuePair<Color, int> pair in colorsAndWeights)
			{
				currentStep += pair.Value;
				if (result < currentStep)
					return pair.Key;
			}
			return colorsAndWeights.Keys.First();
		}

		public int Count()
		{
			return colorsAndWeights.Count();
		}
	}
}
