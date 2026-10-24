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
        //private int offsetX = 10; private int offsetY = 300;//temp values

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
            Dialogue = new Dialogue(dialogueList, mainFont);//TODO fix position bug with relation to camera (screen scale)
            return Dialogue;
        }
        //NPC dialogues
        public IDialogue CreateEthanDialogue() 
        {
            IDialogue Dialogue;
            Queue<string> dialogueList = new Queue<string>();
            dialogueList.Enqueue("Congratulations! Now you get to sit through 20 lines of dialogue!");
            for(int i = 1; i < 21; i++) dialogueList.Enqueue("Waste of time line " + i + ".");
            dialogueList.Enqueue("Alright goodbye now!");
            Dialogue = new Dialogue(dialogueList, mainFont);//TODO fix position bug with relation to camera (screen scale)
            return Dialogue;
        }
        public IDialogue CreateJoeDialogue()
        {
            IDialogue Dialogue;
            Queue<string> dialogueList = new Queue<string>();
            dialogueList.Enqueue("Ey what up dawg, finish the quest and stuff type");
            Dialogue = new Dialogue(dialogueList, mainFont);//TODO fix position bug with relation to camera (screen scale)
            return Dialogue;
        }
        public IDialogue CreateLilyDialogue()
        {
            IDialogue Dialogue;
            Queue<string> dialogueList = new Queue<string>();
            dialogueList.Enqueue("Something..");
            dialogueList.Enqueue("Something...");
            dialogueList.Enqueue("Code quality");
            Dialogue = new Dialogue(dialogueList, mainFont);//TODO fix position bug with relation to camera (screen scale)
            return Dialogue;
        }
        public IDialogue CreateLydiaDialogue()
        {
            IDialogue Dialogue;
            Queue<string> dialogueList = new Queue<string>();
            dialogueList.Enqueue("Welcome to the log game!");
            dialogueList.Enqueue("Ya know? The game where you hunt down logs.");
            dialogueList.Enqueue("This fire isn't gonna make itself, you know.\nOf course, unless you use the dev shortcut.");
            dialogueList.Enqueue("Better get going.");
            Dialogue = new Dialogue(dialogueList, mainFont);//TODO fix position bug with relation to camera (screen scale)
            return Dialogue;
        }
    }
}
