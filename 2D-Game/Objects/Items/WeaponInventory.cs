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
        private Dictionary<Tool, int> Weapons = new();
        private readonly List<Tool> SelectedWeapon = new();
        private int currentIndex;
        public bool UnlimitedAmmo = false;

        // Cooldown vars
        private double cooldownTimer = 0;
        private const double CooldownDuration = 0.2; // 0.2 seconds (200ms) between switches

        private static Game1 myGame;

        public static WeaponInventory instance = new WeaponInventory();

        public static WeaponInventory Instance
        { get { return instance; } }

    
        public void Initialize(Game1 game)
        {
            Weapons = new();
            currentIndex = 0;
            if (Weapons.Count <= 0)
            {
                Weapons.Add(Tool.Slingshot, 5);
                Weapons.Add(Tool.Boomerang, int.MaxValue);
                Weapons.Add(Tool.Dynamite, 0);
            }
            if (SelectedWeapon.Count <= 0)
            {
                SelectedWeapon.Add(Tool.Slingshot);
                SelectedWeapon.Add(Tool.Boomerang);
                SelectedWeapon.Add(Tool.Dynamite);
            }
            myGame = game;
        }

        public void Update(GameTime gameTime)
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void AddWeapon(Tool tool, int ammo)
        {
            Weapons.Add(tool, ammo);
            SelectedWeapon.Add(tool);
        }

        public void ChangeAmmo(Tool tool, int ammo)
        {
            if(!UnlimitedAmmo)
                Weapons[tool] += ammo;
        }

        public int GetAmmo(Tool tool)
        {
            if (Weapons.ContainsKey(tool))
            {
                return Weapons[tool];
            }
            return 0;
        }

        public void Remove(Tool tool)
        {
                Weapons.Remove(tool);
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
        /// <summary>
        /// Returns the weapons thats currently selected
        /// </summary>
        /// <returns></returns>
        public Tool GetTop()
        {
            return SelectedWeapon[currentIndex];
        }

        public List<Tool> GetKeys()
        {
            List<Tool> keys = new List<Tool>();
            foreach (Tool x in Weapons.Keys)
            {
                keys.Add(x);
            }

            return keys;
        }
    }
}
