using System;

using var game = new myGame.Game1();
game.Run();

enum Direction {
    Up = -1, Left = -1, Down = 1, Right = 1, Null = 0
}
