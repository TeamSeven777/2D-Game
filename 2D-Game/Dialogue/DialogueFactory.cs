using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
            Dialogue = new Dialogue(dialogueList, mainFont);
            return Dialogue;
        }
        //NPC dialogues
        public IDialogue CreateEthanDialogue(int dialogueIndex) 
        {
            IDialogue Dialogue;
            Queue<string> dialogueList = new Queue<string>();
            if (dialogueIndex == 0)
            {
                dialogueList.Enqueue("Congratulations! Now you get to sit through 20 lines of dialogue!");
                for (int i = 1; i < 21; i++) dialogueList.Enqueue("Waste of time line " + i + ".");
                dialogueList.Enqueue("Alright goodbye now!");
            }         
            else if(dialogueIndex == 1)
            {
                dialogueList.Enqueue("You look like you like to fish.");
                dialogueList.Enqueue("It's dangerous to go alone. Take this.");      
                return new Dialogue(dialogueList, mainFont, "FishingRod");
            }
            else 
            {
                dialogueList.Enqueue("blue is down.");
            }


            Dialogue = new Dialogue(dialogueList, mainFont);
            return Dialogue;
        }
        public IDialogue CreateJoeDialogue()
        {
            IDialogue Dialogue;
            Queue<string> dialogueList = new Queue<string>();
            dialogueList.Enqueue("Ey what up dawg, finish the quest and stuff type");
            Dialogue = new Dialogue(dialogueList, mainFont);
            return Dialogue;
        }
        public IDialogue CreateLilyDialogue()
        {
            IDialogue Dialogue;
            Queue<string> dialogueList = new Queue<string>();
            dialogueList.Enqueue("Something..");
            dialogueList.Enqueue("Something...");
            dialogueList.Enqueue("Code quality");
            dialogueList.Enqueue("two is up.");
            Dialogue = new Dialogue(dialogueList, mainFont);
            return Dialogue;
        }
        public IDialogue CreateLydiaDialogue(int dialogueIndex)
        {
            Queue<string> dialogueList = new Queue<string>();
            if (dialogueIndex == 0)
            {
                dialogueList.Enqueue("Welcome to the log game!");
                dialogueList.Enqueue("Ya know? The game where you hunt down logs.");
                dialogueList.Enqueue("This fire isn't gonna make itself, you know.\nOf course, unless you use the dev shortcut.");
                dialogueList.Enqueue("Better get going.");
                return new Dialogue(dialogueList, mainFont);
            }
            else
            {
                dialogueList.Enqueue("Here, have a hotdog.");
                dialogueList.Enqueue("you is right.");
                return new Dialogue(dialogueList, mainFont, "Hotdog");
            }
        }
        public IDialogue CreateSignDialogue(int dialogueIndex) 
        {
            IDialogue Dialogue;
            Queue<string> dialogueList = new Queue<string>();
            
            if (dialogueIndex == 0)
            {
                dialogueList.Enqueue("Here be bog\nin which there be log.\nEnter not this lair\nwithout a pathway there.");
            }
            else
            {
                dialogueList.Enqueue("<- Fishing Pond");
                dialogueList.Enqueue("through is down.");
            }
            Dialogue = new Dialogue(dialogueList, mainFont);
            return Dialogue;
        }
        public IDialogue CreateGravestoneDialogue()
        {
            IDialogue Dialogue;
            Queue<string> dialogueList = new Queue<string>();
            dialogueList.Enqueue("Here lies the memory of the unknown\nwho died in the unknown.");
            dialogueList.Enqueue("All that is known is as follows:");
            dialogueList.Enqueue("First is two,\nnext is blue,\nThird is through,\nLast is you.");
            Dialogue = new Dialogue(dialogueList, mainFont);
            return Dialogue;
        }
        public IDialogue CreateFishDialogue(int dialogueIndex) 
        {
            IDialogue Dialogue;
            Queue<string> dialogueList = new Queue<string>();
            if (dialogueIndex == 0) dialogueList.Enqueue("You need a fishing rod to fish.");
            else if (dialogueIndex == 1) dialogueList.Enqueue("You need bait to use the fishing rod.");
            else if (dialogueIndex == 2) dialogueList.Enqueue("You caught a log!");
            else if (dialogueIndex == 3) dialogueList.Enqueue("You caught a hot dog!");
            Dialogue = new Dialogue(dialogueList, mainFont);
            return Dialogue;
        }
    }
}
