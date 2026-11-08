using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace _2D_Game
{
    public class WeaponHotbar : IHotbar
    {
        private readonly Point position;
        private readonly int cellWidth;
        private readonly int cellHeight;
        private readonly float slotSpacing;
        
        private readonly Texture2D slotTexture;
        private readonly Texture2D selectionTexture;
        private readonly Texture2D pixel;
        
        private List<Tuple<ISprite, Tool>> weaponsToDraw;
        private int selectedWeaponIndex = 0;
        private double cooldownTimer = 0;
        private const double CooldownDuration = 0.2;
        private const int TotalSlots = 3;

        public WeaponHotbar(GraphicsDevice device, Point position, int cellWidth, Texture2D slotTexture = null, Texture2D selectionTexture = null, float slotSpacing = 4f)
        {
            this.position = position;
            this.cellWidth = cellWidth;
            this.cellHeight = cellWidth;
            this.slotSpacing = slotSpacing;
            this.slotTexture = slotTexture;
            this.selectionTexture = selectionTexture;

            pixel = new Texture2D(device, 1, 1);
            pixel.SetData(new[] { Color.White });

            weaponsToDraw = new List<Tuple<ISprite, Tool>>
            {
                new Tuple<ISprite, Tool>(ProjectileSpriteFactory.Instance.CreatePlayerProjectile(Tool.Slingshot), Tool.Slingshot),
                new Tuple<ISprite, Tool>(ProjectileSpriteFactory.Instance.CreatePlayerProjectile(Tool.Boomerang), Tool.Boomerang),
                new Tuple<ISprite, Tool>(ProjectileSpriteFactory.Instance.CreatePlayerProjectile(Tool.Dynamite), Tool.Dynamite)
            };
        }

        public void Update(GameTime gameTime)
        {
            if (cooldownTimer > 0)
                cooldownTimer -= gameTime.ElapsedGameTime.TotalSeconds;

            // Optional weapon-specific navigation controls if desired
        }

        public void Next()
        {
            if (cooldownTimer > 0) return;
            selectedWeaponIndex = (selectedWeaponIndex + 1) % TotalSlots;
            cooldownTimer = CooldownDuration;
        }

        public void Previous()
        {
            if (cooldownTimer > 0) return;
            selectedWeaponIndex = (selectedWeaponIndex - 1 + TotalSlots) % TotalSlots;
            cooldownTimer = CooldownDuration;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < TotalSlots; i++)
            {
                Vector2 slotPos = new Vector2(
                    position.X + (i * (cellWidth + slotSpacing)), 
                    position.Y
                );

                Rectangle rect = new Rectangle((int)slotPos.X, (int)slotPos.Y, cellWidth, cellHeight);

                // Background + selection state
                bool isSelected = (i < weaponsToDraw.Count && weaponsToDraw[i].Item2 == WeaponInventory.Instance.GetTop());
                
                if (slotTexture != null)
                {
                    spriteBatch.Draw(slotTexture, rect, Color.White);
                    if (isSelected && selectionTexture != null)
                        spriteBatch.Draw(selectionTexture, rect, Color.Yellow * 0.8f);
                }
                else
                {
                    Color fillColor = isSelected ? Color.White * 0.8f : Color.Black * 0.8f;
                    spriteBatch.Draw(pixel, rect, fillColor);
                }

                // Draw weapon sprite icon
                if (i < weaponsToDraw.Count)
                {
                    Vector2 drawCenter = slotPos + new Vector2(cellWidth / 2f, cellHeight / 2f);
                    weaponsToDraw[i].Item1.Draw(spriteBatch, drawCenter);
                }
            }
        }
    }
}
