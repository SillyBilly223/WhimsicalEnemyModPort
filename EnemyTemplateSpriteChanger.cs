using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CrayolapedeModinreallife
{
    public class EnemyTemplateSpriteChanger : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer;

        public string[] SpritesRef;

        public void Awake()
        {
            if (SpriteRenderer == null) SpriteRenderer = GetComponent<SpriteRenderer>();
            if (SpritesRef != null && SpriteRenderer != null)
            {
                string Sprite = SpritesRef[Random.Range(0, SpritesRef.Length)];
                if (ResourceLoader.ResourceBinary(Sprite) == null)
                {
                    Debug.LogError("Couldn't find " + Sprite + "! Check for typos when using ResourceLoader.LoadSprite() and that all of your textures have their build action as Embedded Resource.");
                    return;
                }
                SpriteRenderer.sprite = ResourceLoader.LoadSprite(Sprite);
            }
        }
    }
}
