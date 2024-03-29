using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Linq;
using System;

namespace myGame {
    class Sprite {
    private readonly Game1 myGame;
    public Vector2 myPosition;
    public float myZ;
    public Texture2D myTexture;
    public Rectangle myRect = new Rectangle();
    public Rectangle myImgRect;
    public int centerToX, centerToY;
    public bool upRestrict = false, downRestrict = false, leftRestrict = false, rightRestrict = false;
    public float movementSpeed = 0;

    public static List<Sprite> spriteList = new List<Sprite>();
    public Sprite(Game1 game, string image = "blank", int width = 16, int height = 16, int x = 0, int y = 0, float z = 0, bool textureSize = false) {
            spriteList.Add(this);
            myGame = game;
            try {
                myTexture = myGame.Content.Load<Texture2D>(image);
               }
            catch {
                myTexture = myGame.Content.Load<Texture2D>("blank");
            }
            SetPosition(x, y);
            CenterToSidesDistance();
            myZ = z;
            if (textureSize) {myRect.Width = myTexture.Width; myRect.Height = myTexture.Height;}
            SetImgRect();
        }
        public void ChangePosition(float changeX = 0, float changeY = 0) {
            myPosition = new Vector2(myPosition.X + changeX, myPosition.Y + changeY);
            SetGamePosition();
            CenterToSidesDistance();
        }
        public void SetPosition(float x, float y) {
            myPosition = new Vector2(x, y);
            SetGamePosition();
            CenterToSidesDistance();
        }

        public void SetPosition(Vector2 vector2) {
            myPosition = vector2;
            SetGamePosition();
            CenterToSidesDistance();
        }

        public void SetImgRect(bool custom = false, int width = 100, int height = 100, int x = 0, int y = 0) {
            if (custom) {
                myImgRect = new Rectangle(x,y,width,height);
            }
            else {
                myImgRect = new Rectangle(0,0, myTexture.Width, myTexture.Height);
            }

        }

        public void SetTexture(string image) {
            try {
                myTexture = myGame.Content.Load<Texture2D>(image);
                }
            catch (ContentLoadException) {
                myTexture = myGame.Content.Load<Texture2D>("blank");
            }
        }
        private void CenterToSidesDistance() {
            centerToX = myRect.Width/2;
            centerToY = myRect.Height/2;
        }

        private void SetGamePosition() {
            myRect.X = (int)(myPosition.X + myGame.Origin.X);
            myRect.Y = (int)(myPosition.Y + myGame.Origin.Y);
        }

        public static List<Sprite> OrderRenderListByZ() => spriteList.OrderBy(x => x.myZ).ToList();
    }

}