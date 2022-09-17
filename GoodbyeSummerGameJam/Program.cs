using System;

namespace GoodbyeSummerGameJam
{
	public static class Program
	{
		[STAThread]
		static void Main()
		{
			using (var game = new World())
				game.Run();
		}
	}
}
