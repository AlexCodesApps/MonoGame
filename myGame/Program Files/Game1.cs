﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace myGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private List<Sprite> _spritesInRenderOrder;
    public Vector2 Origin;
    SpriteEntity Block1, Block2;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        //  Disabled v-sync, unlocking fps
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Origin = new Vector2(_graphics.PreferredBackBufferWidth/2, _graphics.PreferredBackBufferHeight/2);
        Console.WriteLine("Console Works.");
        base.Initialize();
        
    }

    protected override void LoadContent()
    {
    // Make a new system for Content Loading
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Block1 = new SpriteEntity(this, "ball", 0, 0, -100, -50, 1, true);
        Block2 = new SpriteEntity(this, "ball", 0, 0, 100, 50, 0, true, false);
        Block1.mySprite.movementSpeed = 10;
        InputManager.MoveSprite(Block1);
    }

    protected override void Update(GameTime gameTime)
{
    /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();*/
    // Respective Classes Update Logic
    Hitbox.UpdateHitboxes();
    InputManager.ParseInput();
    base.Update(gameTime); // Should always be last!
}


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spritesInRenderOrder = Sprite.OrderRenderListByZ();
        foreach (Sprite i in _spritesInRenderOrder) {
            _spriteBatch.Draw(i.myTexture, i.myRect, i.myImgRect, Color.White);
        }
        _spriteBatch.End();
        


        base.Draw(gameTime);
    }
}
