using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace myGame {
    static class InputManager {
        static KeyboardState kState;
        private static readonly Keys[] PlayerMovementKeys = new Keys[] {Keys.W,Keys.A,Keys.S,Keys.D,Keys.Up,Keys.Left,Keys.Down,Keys.Right};
        static private List<SpriteEntity> ControllableSprites = new();
        static public void ParseInput() {
            kState = Keyboard.GetState();
            foreach (Keys key in PlayerMovementKeys) {
                if (kState.IsKeyDown(key)) {
                    HandleMovement(key);
                }
            }
        }
        static void HandleMovement(Keys key) {
            foreach (SpriteEntity sprite in ControllableSprites) {
                switch (key) {
                    case Keys.W:
                    case Keys.Up:
                    if (sprite.mySprite.upRestrict) continue;
                    sprite.mySprite.ChangePosition(changeY:(int)Direction.Up*sprite.mySprite.movementSpeed);
                    break;
                    case Keys.A:
                    case Keys.Left:
                    if (sprite.mySprite.leftRestrict) continue;
                    sprite.mySprite.ChangePosition(changeX:(int)Direction.Left*sprite.mySprite.movementSpeed);
                    break;
                    case Keys.S:
                    case Keys.Down:
                    if (sprite.mySprite.downRestrict) continue;
                    sprite.mySprite.ChangePosition(changeY:(int)Direction.Down*sprite.mySprite.movementSpeed);
                    break;
                    case Keys.D:
                    case Keys.Right:
                    if (sprite.mySprite.rightRestrict) continue;
                    sprite.mySprite.ChangePosition(changeX:(int)Direction.Right*sprite.mySprite.movementSpeed);
                    break;
                }
            }
        }
        public static void MoveSprite(SpriteEntity sprite) {
            ControllableSprites.Add(sprite);
        }
        public static void RemoveSprite(SpriteEntity sprite) {
            ControllableSprites.Remove(sprite);
        }
    }
}