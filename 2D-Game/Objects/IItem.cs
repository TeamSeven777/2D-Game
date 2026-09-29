using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_Game
{
    internal interface IItem
    {
        string Name { get; }
        Texture2D Icon { get; }
        bool IsStackable { get; }
        int MaxStackSize { get; }
        int CurrentStack { get; set; }

        void Use();

    }
}
