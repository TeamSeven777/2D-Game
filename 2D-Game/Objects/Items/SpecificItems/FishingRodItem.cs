using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    // Fishing Rod
    public class FishingRodItem : Item
    {
        public FishingRodItem(Sprite icon) : base("FishingRod", icon, 99) { }//TODO make this work
        public bool OverWater { get; set; } = false;
        private int BaitCount = 0;
        private bool LogCaught = false;
        public override void Collect()
        {
            ItemInventory.Instance.Add(this, 1);
        }
        public override void Use()
        {
            BaitCount = ItemInventory.Instance.GetItemCount("Hotdog");//TODO update so bait instead of hotdog
            //need to be used over water. and have bait
            //TODO Do not update because hotdog as bait is funny >:)
            if (OverWater && BaitCount > 0) Fish();
            else if (OverWater) //no bait
            {
                RoomCollisionHandler.Instance.myGame.CurrentDialogue = DialogueFactory.Instance.CreateFishDialogue(1);
                RoomCollisionHandler.Instance.myGame.StateManager.HandleDialogue();
            }
        }
        private void Fish() {
            if (!LogCaught) //can catch log once
            {
                RoomCollisionHandler.Instance.myGame.CurrentDialogue = DialogueFactory.Instance.CreateFishDialogue(2);
                RoomCollisionHandler.Instance.myGame.StateManager.HandleDialogue();
                ItemInventory.Instance.Add(RoomContentFactory.Instance.CreateItem("Log"), 1);
                SoundEffectFactory.Instance.PlaySoundEffect(SoundName.PuzzleSolve1, false);
                LogCaught = true;
            }
            else 
            {
                RoomCollisionHandler.Instance.myGame.CurrentDialogue = DialogueFactory.Instance.CreateFishDialogue(3);
                RoomCollisionHandler.Instance.myGame.StateManager.HandleDialogue();
                ItemInventory.Instance.Add(RoomContentFactory.Instance.CreateItem("Berry"), 1);//TODO change so it is fish
            }
            //decrement bait
            ItemInventory.Instance.ChangeValue("Hotdog", -1);
        }
    }
}