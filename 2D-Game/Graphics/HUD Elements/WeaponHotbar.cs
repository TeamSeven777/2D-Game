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
        private readonly SpriteFont font;
        private readonly Texture2D pixel;

        private double cooldownTimer = 0;
        private const double CooldownDuration = 0.2;

        public WeaponHotbar(
            GraphicsDevice device, 
            Point position, 
            int cellWidth, 
            float slotSpacing = 4f,
            Texture2D slotTexture = null,
            Texture2D selectionTexture = null,
            SpriteFont font= null)
        {
            this.position = position;
            this.cellWidth = cellWidth;
            this.cellHeight = cellWidth;
            this.slotSpacing = slotSpacing;
            this.slotTexture = slotTexture;
            this.selectionTexture = selectionTexture;
            this.font = font;

            pixel = new Texture2D(device, 1, 1);
            pixel.SetData(new[] { Color.White });
        }

        public void Update(GameTime gameTime)
        {
            if (cooldownTimer > 0)
                cooldownTimer -= gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Next()
        {
            WeaponInventory.Instance.Next();
        }

        public void Previous()
        {
            WeaponInventory.Instance.Previous();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var inventory = WeaponInventory.Instance;
            List<Tool> weapons = inventory.GetKeys();
            Tool selectedWeapon = inventory.GetTop();

            for (int i = 0; i < weapons.Count; i++)
            {
                Tool currentTool = weapons[i];
                Vector2 slotPos = new Vector2(
                    position.X + (i * (cellWidth + slotSpacing)), 
                    position.Y
                );

                Rectangle rect = new Rectangle((int)slotPos.X, (int)slotPos.Y, cellWidth, cellHeight);

                // Determine if this slot is currently selected
                bool isSelected = (currentTool == selectedWeapon);

                // Draw Slot Background & Selection Highlight
                if (slotTexture != null)
                {
                    spriteBatch.Draw(slotTexture, rect, Color.White);
                    if (isSelected && selectionTexture != null)
                    {
                        spriteBatch.Draw(selectionTexture, rect, Color.Yellow * 0.8f);
                    }
                }
                else
                {
                    Color fillColor = isSelected ? Color.White * 0.8f : Color.Black * 0.8f;
                    spriteBatch.Draw(pixel, rect, fillColor);
                }
                ISprite weaponSprite;
                // Draw Weapon Projectile Sprite Icon
                if (currentTool != Tool.Slingshot)
                    weaponSprite = ProjectileSpriteFactory.Instance.CreatePlayerProjectile(currentTool);
                else
                    weaponSprite = PlayerSpriteFactory.Instance.CreateTool(currentTool);
                weaponSprite.Scale = new Vector2(0.5f);
                if(WeaponInventory.Instance.GetAmmo(currentTool) == 0)
                {
                    weaponSprite.Color = Color.Black * 0.2f;
                }
                if (weaponSprite != null)
                {
                    Vector2 drawCenter = slotPos + new Vector2(cellWidth / 2f, cellHeight / 2f);
                    weaponSprite.Draw(spriteBatch, drawCenter);
                }

                // Draw Ammo Count / Stack Display
                if (font != null)
                {
                    int ammo = inventory.GetAmmo(currentTool);
                    string ammoText = (ammo == int.MaxValue) ? "inf" : ammo.ToString();

                    // Only draw count if ammo is greater than 1 or infinite
                    if (ammo > 1 || ammo == int.MaxValue)
                    {
                        Vector2 textPosition = slotPos + new Vector2(cellWidth - 18, cellHeight - 22);
                        spriteBatch.DrawString(font, ammoText, textPosition, Color.White);
                    }
                }
            }
        }
    }
}
