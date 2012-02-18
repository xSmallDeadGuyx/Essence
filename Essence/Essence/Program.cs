using System;

namespace Essence {
#if WINDOWS || XBOX
	static class Program {
		static void Main(string[] args) {
			using (Essence game = new Essence()) {
				game.Run();
			}
		}
	}
#endif
}
