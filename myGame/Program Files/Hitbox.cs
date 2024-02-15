
using System;
using System.Collections.Generic;

namespace myGame {

    class Hitbox
    {
        private Sprite mySprite;
        bool spriteMovable;
        bool xOverlap, yOverlap;
        Direction xDirection, yDirection;
        float Hypotenuse = 1;
        int y1, y2, x1, x2;
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
        }

        public static void UpdateHitboxes() {
            foreach (SpriteEntity i in SpriteEntity.spriteEntities)
            {
                foreach (SpriteEntity i2 in SpriteEntity.spriteEntities)
                {
                    if (i == i2) continue;
                    i.myHitbox.CalcHitbox(i2.myHitbox);
                }
            }
        }

        public void CalcHitbox(Hitbox hitbox) {
            SetNormals();
            hitbox.SetNormals();
            yOverlap = false; 
            xOverlap = false;
            xDirection = Direction.Null; yDirection = Direction.Null;
            if (hitbox.y1 >= y1 && y1 >= hitbox.y2) {yOverlap = true; yDirection = Direction.Up;}
            if (hitbox.y1 >= y2 && y2 >= hitbox.y2) {yOverlap = true; yDirection = Direction.Down;}
            if (hitbox.x1 <= x2 && x2 <= hitbox.x2) {xOverlap = true; xDirection = Direction.Right;}
            if (hitbox.x1 <= x1 && x1 <= hitbox.x2) {xOverlap = true; xDirection = Direction.Left;}
            Console.WriteLine($"XOverlap: {xOverlap}, yOverlap: {yOverlap}");
            FixCollision(hitbox);
        }

        public void FixCollision(Hitbox hitbox) {
            if (spriteMovable) {
                if (xOverlap && yOverlap) {
                    InputMovementRestrict();
                    OppositeCollisionMovement(hitbox);
                    }
            }
            if (!yOverlap || !xOverlap) mySprite.rightRestrict = false; mySprite.leftRestrict = false; mySprite.upRestrict = false; mySprite.downRestrict = false;
        }

        private void OppositeCollisionMovement(Hitbox hitbox) {
            float xDist = hitbox.mySprite.myPosition.X - mySprite.myPosition.X;
            float yDist = hitbox.mySprite.myPosition.Y - mySprite.myPosition.Y;
            float radian = MathF.Atan2(yDist,xDist);
            float xV = Hypotenuse*MathF.Cos(radian);
            float yV = Hypotenuse*MathF.Sin(radian);
            mySprite.ChangePosition(-xV, -yV);
        }

        public void InputMovementRestrict() {
            if (yDirection == Direction.Up) mySprite.upRestrict = true;
            if (yDirection == Direction.Down) mySprite.downRestrict = true;
            if (xDirection == Direction.Right) mySprite.rightRestrict = true;
            if (xDirection == Direction.Left) mySprite.leftRestrict = true;
        }
    }
}