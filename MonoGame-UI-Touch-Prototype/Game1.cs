using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Collisions
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TouchHandler touchHandler;

        RotatedRectangle RectangleA;
        RotatedRectangle RectangleB;

        Texture2D RectangleTexture;

        bool IsRectangleASelected = true;
        bool IsCollisionDetected = false;
        Texture2D carTexture; 
        List<Player> car; 
	         
        Texture2D circleTexture;

        KeyboardState PreviousKeyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
        }


        //Allows the game to perform any initialization it needs to before starting to run.
        protected override void Initialize()
        {
           // touchHandler = new TouchHandler();

            car = new List<Player>();
           
            base.Initialize();
        }

        //LoadContent will be called once per game and is the place to load
        //all of your content.
        protected override void LoadContent()
        {
            //Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            RectangleTexture = Content.Load<Texture2D>("Square");

            RectangleA = new RotatedRectangle(new Rectangle(100, 200, 200, 100), 0.0f);
            RectangleB = new RotatedRectangle(new Rectangle(300, 200, 130, 390), 0.0f);

            carTexture = Content.Load<Texture2D>("car");
            circleTexture = Content.Load<Texture2D>("circle");

            //car.Add(new Player(carTexture, new Vector2(200, 200), 45, 0, circleTexture, new Vector2(GraphicsDevice.DisplayMode.Width * 0.2f, GraphicsDevice.DisplayMode.Height * 0.15f)));
        }

        protected override void UnloadContent()
        {

        }

        //Allows the game to run logic such as updating the world,
        //checking for collisions, gathering input, and playing audio.
        protected override void Update(GameTime gameTime)
        {
            MoveRectangle();
            IsCollisionDetected = RectangleA.Intersects(RectangleB);

            //car[0].Update();

            base.Update(gameTime);
        }

        private void MoveRectangle()
        {
            KeyboardState CurrentKeyboard = Keyboard.GetState();

            if (CurrentKeyboard.IsKeyDown(Keys.W))
            {
                RectangleA.Move(0, -2);
            }

            if (CurrentKeyboard.IsKeyDown(Keys.S))
            {
                RectangleA.Move(0, 2);
            }

            if (CurrentKeyboard.IsKeyDown(Keys.A))
            {
                RectangleA.Move(-2, 0);
            }

            if (CurrentKeyboard.IsKeyDown(Keys.D))
            {
                RectangleA.Move(2, 0);
            }

            if (CurrentKeyboard.IsKeyDown(Keys.Q))
            {
                RectangleA.m_Rotation -= 0.01f;
            }

            if (CurrentKeyboard.IsKeyDown(Keys.E))
            {
                RectangleA.m_Rotation += 0.01f;
            }
        
            if (CurrentKeyboard.IsKeyDown(Keys.Up))
            {
                RectangleB.Move(0, -2);
            }

            if (CurrentKeyboard.IsKeyDown(Keys.Down))
            {
                RectangleB.Move(0, 2);
            }

            if (CurrentKeyboard.IsKeyDown(Keys.Left))
            {
                RectangleB.Move(-2, 0);
            }

            if (CurrentKeyboard.IsKeyDown(Keys.Right))
            {
                RectangleB.Move(2, 0);
            }

            if (CurrentKeyboard.IsKeyDown(Keys.R))
            {
                RectangleB.m_Rotation -= 0.01f;
            }

            if (CurrentKeyboard.IsKeyDown(Keys.T))
            {
                RectangleB.m_Rotation += 0.01f;
            }
        }

        //called when game should draw
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Color aColor = Color.Blue;
            if (IsCollisionDetected)
            {
                aColor = Color.Red;
            }

            spriteBatch.Begin();

            Rectangle AdjustsedPos = new Rectangle(RectangleA.X + (RectangleA.Width / 2), RectangleA.Y + (RectangleA.Height / 2), RectangleA.Width, RectangleA.Height);
            spriteBatch.Draw(RectangleTexture, AdjustsedPos, new Rectangle(0, 0, 2, 6), aColor, RectangleA.m_Rotation, new Vector2(2 / 2, 6 / 2), SpriteEffects.None, 0);

            AdjustsedPos = new Rectangle(RectangleB.X + (RectangleB.Width / 2), RectangleB.Y + (RectangleB.Height / 2), RectangleB.Width, RectangleB.Height);
            spriteBatch.Draw(RectangleTexture, AdjustsedPos, new Rectangle(0, 0, 2, 6), aColor, RectangleB.m_Rotation, new Vector2(2 / 2, 6 / 2), SpriteEffects.None, 0);

           /* foreach (Player pl in car)
            {
                spriteBatch.Draw(pl.m_car.GetTexture(), pl.m_car.GetPosition(), rotation: pl.m_car.GetRotationRadians(), origin: new Vector2(pl.m_car.GetTexture().Width / 2, pl.m_car.GetTexture().Height / 2));
                spriteBatch.Draw(pl.m_circle.GetTexture(), pl.m_circle.GetPosition());
            }*/

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
