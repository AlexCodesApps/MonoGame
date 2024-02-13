using System.Collections.Generic;

namespace myGame {
    class SpriteEntity
    {
        public static List<SpriteEntity> spriteEntities = new List<SpriteEntity>();
        public Sprite mySprite;
        public Hitbox myHitbox;
        public SpriteEntity(Game1 game, string image = "blank", int width = 16, int height = 16, int x = 0, int y = 0, float z = 0, bool textureSize = false, bool Movable = true) {
            spriteEntities.Add(this);
            mySprite = new Sprite(game,image,width,height,x,y,z,textureSize);
            myHitbox = new Hitbox(mySprite, Movable);
        }
        
    }
}