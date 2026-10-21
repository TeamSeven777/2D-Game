using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    public class DialogueFactory
    {
        private SpriteFont mainFont;
        public int WindowSizeX { get; set; }
        public int WindowSizeY { get; set; }
        private int offsetX = 10; private int offsetY = 300;//temp values

        private static DialogueFactory instance = new DialogueFactory();

        public static DialogueFactory Instance
        { get { return instance; } }

        private DialogueFactory() { }

        public void LoadFonts(ContentManager content)
        {
            mainFont = content.Load<SpriteFont>("Fonts/pixelFont");
        }

        //idle animations
        public IDialogue CreateBlockDialogue()
        {
            IDialogue Dialogue;
            Queue<string> dialogueList = new Queue<string>();
            dialogueList.Enqueue("Hi!");
            dialogueList.Enqueue("I'm a block.");
            Dialogue = new Dialogue(dialogueList, mainFont, new Vector2(WindowSizeX / 2 + offsetX, WindowSizeY /2 + offsetY));
            return Dialogue;
        }     
    }
}
