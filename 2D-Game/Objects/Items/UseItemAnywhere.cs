using _2D_Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Class
{
    public class UseItemAnywhere
{

        public UseItemAnywhere()
        {
            
        }
        /// <summary>
        /// If player is selecting an item that can be used anywhere, use it.
        /// </summary>
        public void ItemUseHandling()
        {
            var selectedItem = ItemInventory.Instance.GetSelectedItem();
            if (selectedItem != null && selectedItem.Name.Equals("Berry")) // Specify item names that can be used anywhere here
            {
                selectedItem.Use();
            }
        }

    }
}
