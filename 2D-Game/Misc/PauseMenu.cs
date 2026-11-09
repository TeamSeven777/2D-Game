using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace _2D_Game
{
    public class PauseMenu
{
        GraphicsDevice GD;
        Game1 MyGame;
        Texture2D Button1;
        Texture2D Button2;
        Texture2D Button3;
        Rectangle ExitButtonReigon;
        Rectangle ResetButtonReigon;
        List<Tuple<Rectangle,ButtonType, Texture2D>> ButtonReigons;
        Texture2D Screen;
        SpriteFont font;

        enum ButtonType {Resume, Reset, Quit}

        public PauseMenu(GraphicsDevice gd,Game1 game, Rectangle button, SpriteFont f = null)
        {
            GD = gd;
            MyGame = game;
            Screen = new Texture2D(gd, 1, 1);
            ButtonReigons = new List<Tuple<Rectangle, ButtonType,Texture2D>>();
            ButtonReigons.Add(new Tuple<Rectangle,ButtonType,Texture2D>(new Rectangle(button.X, button.Y - 200, button.Width, button.Height),ButtonType.Reset, new Texture2D(gd, 1, 1)));
            ButtonReigons.Add(new Tuple<Rectangle, ButtonType, Texture2D>(button, ButtonType.Resume, new Texture2D(gd, 1, 1)));
            ButtonReigons.Add(new Tuple<Rectangle,ButtonType,Texture2D>(new Rectangle(button.X, button.Y + 200, button.Width, button.Height),ButtonType.Quit, new Texture2D(gd, 1, 1)));
            font = f;
           

            Screen.SetData([Color.White]);
            foreach(Tuple<Rectangle,ButtonType,Texture2D> x in ButtonReigons)
            {
                x.Item3.SetData([Color.White]);
            }
            
        }

        public void TryClickBox()
        {
            MouseState mouse = Mouse.GetState();
            foreach(Tuple<Rectangle,ButtonType,Texture2D> x in ButtonReigons)
            {
                if (x.Item1.Intersects(new Rectangle(mouse.Position, new Point(1, 1))))
                {
                    x.Item3.SetData([Color.Wheat]);
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        switch (x.Item2)
                        {
                            case (ButtonType.Resume):
                                MyGame.TogglePause();
                                break;
                            case (ButtonType.Reset):
                                MyGame.ResetGame();
                                break;
                            case (ButtonType.Quit):
                                MyGame.Exit();
                                break;
                        }
                    }
                }
                else
                {
                    x.Item3.SetData([Color.White]);
                }
               
            }
        }
        public void Update(GameTime gameTime)
        {
            TryClickBox();
        }
        public void Draw(SpriteBatch sb)
        {
           sb.Draw(Screen,new Rectangle(0,0,GD.Viewport.Width, GD.Viewport.Height),Color.Black * 0.8f);
            for(int i = 0; i < ButtonReigons.Count; i++)
            {
                sb.Draw(ButtonReigons[i].Item3, ButtonReigons[i].Item1, Color.White);
                Vector2 textPosition = new Vector2(ButtonReigons[i].Item1.X + 25, ButtonReigons[i].Item1.Y + 33);
                if (font != null)
                    switch (ButtonReigons[i].Item2)
                    {
                        case (ButtonType.Resume):
                            sb.DrawString(font, "Resume", textPosition, Color.Black);
                            break;
                        case (ButtonType.Quit):
                            sb.DrawString(font, "Quit", textPosition, Color.Black);
                            break;
                        case (ButtonType.Reset):
                            sb.DrawString(font, "Restart", textPosition, Color.Black);
                            break;
                    }
            }
           
        }
}
}
