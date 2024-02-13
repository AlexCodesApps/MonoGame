
using System;
using System.Collections.Generic;

enum Direction {
    Top = -1, Left = 1, Bottom = 1, Right = -1, Null = 0
}
namespace myGame {

    class Hitbox
    {
        private Sprite mySprite;
        bool spriteMovable;
        bool xOverlap;
        bool yOverlap;
        int y1;
        int y2;
        int x1;
        int x2;
        Direction xMoveDirection;
        Direction yMoveDirection;

        static readonly List<Sprite> spriteList = Sprite.spriteList;

        public Hitbox(Sprite sprite, bool Movable) {
            spriteMovable = Movable;
            mySprite = sprite;
            SetNormals();
        }

        public void SetNormals() {
            y1 = (int)mySprite.myPosition.Y + mySprite.centerToY;
            y2 = (int)mySprite.myPosition.Y - mySprite.centerToY;
            x1 = (int)mySprite.myPosition.X - mySprite.centerToX;
            x2 = (int)mySprite.myPosition.X + mySprite.centerToX;
            Console.WriteLine(y1 + " " + y2 + " " + x1 + " " + x2);
        }

        public static void UpdateHitboxes() {
            foreach (SpriteEntity i in SpriteEntity.spriteEntities)
            {
                foreach (SpriteEntity i2 in SpriteEntity.spriteEntities)
                {
                    if (i == i2) break;
                    i.myHitbox.CalcHitbox(i2.myHitbox);
                }
            }
        }

        public void CalcHitbox(Hitbox hitbox) {
            SetNormals();
            hitbox.SetNormals();
            yOverlap = false;
            xOverlap = false;
            yMoveDirection = Direction.Null;
            xMoveDirection = Direction.Null;
            if (hitbox.y1 > y1 &&  y1 > hitbox.y2) {
                yOverlap = true;
                yMoveDirection = Direction.Top;
                Console.WriteLine("Top");
            }
            else if (hitbox.y1 > y2 && y2 > hitbox.y2) {
                yOverlap = true;
                yMoveDirection = Direction.Bottom;
                Console.WriteLine("Bottom");
            }
            if (hitbox.x1 < x1 && x1 < hitbox.x2) {
                xOverlap = true;
                xMoveDirection = Direction.Left;
                Console.WriteLine("Left");
            }
            else if (hitbox.x1 < x2 && x2 < hitbox.x2) {
                xOverlap = true;
                xMoveDirection = Direction.Right;
                Console.WriteLine("Right");
            }

            FixCollision(hitbox);
        }

        public void FixCollision(Hitbox hitbox) {
            if (spriteMovable) {
                if (xOverlap && yOverlap) {
                mySprite.ChangePosition(10*(int)xMoveDirection,10*(int)yMoveDirection);
                }
                else {
                    if (x1 >= hitbox.x1 && x2 <= hitbox.x2) {
                        mySprite.ChangePosition(changeY:10*(int)yMoveDirection);
                    }
                    if (y1 <= hitbox.y1 && y2 >= hitbox.y2) {
                        mySprite.ChangePosition(changeX:10*(int)xMoveDirection);
                    }
                }
            }
        }
    }
}