using System;

namespace BurgerPoolGame
{

    public static class Program
    {
        static void Main()
        {
            using (var game = (BurgerGame)BurgerGame.Instance())
                game.Run();
        }
    }
}
