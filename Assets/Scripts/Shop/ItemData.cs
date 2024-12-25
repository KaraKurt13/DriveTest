using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    public abstract class ItemData : ScriptableObject
    {
        public string Name;

        public int Price;

        public Sprite Icon;

        public abstract ItemTypeEnum Type { get; }

        public abstract void UnlockItem();
    }
}