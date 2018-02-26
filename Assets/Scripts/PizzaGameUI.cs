using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

namespace MyGame
{
    public class PizzaGameUI : MonoBehaviour
    {

        public event Action<int> OnPressTile;
        public event Action<int> OnReleaseTile;

        public Text message;

        private List<Tile> tiles;

        private Dictionary<int, int> numTile;

        public void Init()
        {
            tiles = GetComponentsInChildren<Tile>().ToList();

            numTile = new Dictionary<int, int>();

            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].Init();

                numTile.Add(tiles[i].gameObject.GetInstanceID(), i);

                Activate(i);
            }
            
        }

        public void SetMessage(string text)
        {
            message.text = text;
        }

        public void Activate(int numTile)
        {
            if (!tiles[numTile].isActive)
            {
                tiles[numTile].Activate();

                tiles[numTile].OnPress += Tile_OnPress;
                tiles[numTile].OnPress += Tile_OnRelease;
            }
        }

        public void Deactivate(int numTile)
        {
            if (tiles[numTile].isActive)
            {
                tiles[numTile].Deactivate();

                tiles[numTile].OnPress -= Tile_OnPress;
                tiles[numTile].OnPress -= Tile_OnRelease;
            }
        }

        void Tile_OnPress(int tileID)
        {
            if (OnPressTile != null)
                OnPressTile(numTile[tileID]);
        }

        void Tile_OnRelease(int tileID)
        {
            if (OnReleaseTile != null)
                OnReleaseTile(numTile[tileID]);
        }
    }

}
