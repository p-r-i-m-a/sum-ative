using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace sum_ative
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D burger, dude, introTexture, endTexture;
        Rectangle introRect, endRect, burgerRect, dudeRect;
        SoundEffect burgerSound, crunchSound;
        Song lcbad;
        float seconds;
        MouseState mouseState, prevMouseState;

        enum Screen
        {
            intro,
            play,
            end
        }

        Screen screen;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;

            seconds = 0f;
            screen = Screen.intro;


            burgerRect = new Rectangle(50, 100, 150, 150);
            dudeRect = new Rectangle(600, 50, 200, 500);
            introRect = new Rectangle(0, 0, 800, 500);
            endRect = new Rectangle(0, 0, 800, 500);





            base.Initialize();

            MediaPlayer.Play(lcbad);


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            burger = Content.Load<Texture2D>("bamgurgr");
            dude = Content.Load<Texture2D>("stockDude");

            lcbad = Content.Load<Song>("Life could be a dream");
            burgerSound = Content.Load<SoundEffect>("burgerSound");
            crunchSound = Content.Load<SoundEffect>("crunchSound");

            introTexture = Content.Load<Texture2D>("hamburgerIntro");
            endTexture = Content.Load<Texture2D>("endscreen");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            if (screen == Screen.intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    screen = Screen.play;
                    MediaPlayer.Stop();
                }
            }
            else if (screen == Screen.play)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    screen = Screen.end;

                if (burgerRect.Right == dudeRect.Left)
                {
                    crunchSound.Play();
                }

            }
            else if (screen == Screen.end)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    System.Environment.Exit(0);

                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            if (screen == Screen.intro)
            {
                _spriteBatch.Draw(introTexture, introRect, Color.White);
            }
            else if (screen == Screen.play)
            {
                _spriteBatch.Draw(dude, dudeRect, Color.White);
                _spriteBatch.Draw(burger, burgerRect, Color.White);

            }
            else if (screen == Screen.end)
            {
                _spriteBatch.Draw(endTexture, endRect, Color.White);
            }

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}