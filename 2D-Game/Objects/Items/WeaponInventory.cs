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
        private readonly Dictionary<Tool, int> Weapons = new();
        private readonly List<Tool> SelectedWeapon = new();
        private int currentIndex;

        // Cooldown vars
        private double cooldownTimer = 0;
        private const double CooldownDuration = 0.2; // 0.2 seconds (200ms) between switches

        private static Game1 myGame;

        public static WeaponInventory instance = new WeaponInventory();

        public static WeaponInventory Instance
        { get { return instance; } }

    
        public void Initialize(Game1 game)
        {
            if (Weapons.Count <= 0)
            {
                Weapons.Add(Tool.Slingshot, int.MaxValue);
                Weapons.Add(Tool.Boomerang, int.MaxValue);
                Weapons.Add(Tool.Dynamite, int.MaxValue);
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

        public void AddAmmo(Tool tool, int ammo)
        {
            Weapons[tool] += ammo;
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

    }
}
