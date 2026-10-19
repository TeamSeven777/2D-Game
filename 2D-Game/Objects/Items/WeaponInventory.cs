using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    public class WeaponInventory
    {
        //Weapon, then Ammo
        private readonly List<Tuple<Tool, int>> Weapons = new();
        private int currentIndex = 0;
        // Cooldown vars
        private double cooldownTimer = 0;
        private const double CooldownDuration = 0.2; // 0.2 seconds (200ms) between switches

        /// <summary>
        /// SpriteFactory for all items
        /// </summary>
        /// <param name="content"></param>
        public void Initialize()
        {   
            // Add all items here (Will be pared down later)
            Weapons.Add(new Tuple<Tool,int>(Tool.Slingshot,int.MaxValue));
            Weapons.Add(new Tuple<Tool, int>(Tool.Boomerang, int.MaxValue));
            Weapons.Add(new Tuple<Tool, int>(Tool.Dynamite, int.MaxValue));
            //Weapons.Add(new Tuple<Tool, int>(Tool.Stick, 0));
            
        }

        public void Update(GameTime gameTime)
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void Add(Tool tool, int ammo)
        {
            Weapons.Add(new Tuple<Tool,int>(tool, ammo));
        }

        public void Remove(Tool tool)
        {
            Tuple<Tool, int> temp = new Tuple<Tool,int>(Tool.None,0);
            foreach (Tuple<Tool,int> x in Weapons)
            {
                if (x.Item1 == tool)
                {
                    temp = x;
                }
            }
            if(Weapons.Contains(temp))
                Weapons.Remove(temp);
        }
        public void Next()
        {
            if (Weapons.Count == 0 || cooldownTimer > 0) return;
            
            currentIndex = (currentIndex + 1) % Weapons.Count;
            cooldownTimer = CooldownDuration; // Reset timer
        }

        public void Previous()
        {
            if (Weapons.Count == 0 || cooldownTimer > 0) return;

            currentIndex = (currentIndex - 1 + Weapons.Count) % Weapons.Count;
            cooldownTimer = CooldownDuration; // Reset timer
        }
        public Tool GetTop()
        {
            return Weapons[currentIndex].Item1;
        }

    }
}
