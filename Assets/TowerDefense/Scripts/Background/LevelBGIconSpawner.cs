using Sirenix.Utilities;
using UnityEngine;

namespace Giacomo
{
    public class LevelBGIconSpawner : BGIconSpawner
    {
        protected override void SpawnTiles()
        {
            GridManager.Instance.GetAll().ForEach(x => { 
                ignoreTiles.Add(x.Key); 
                ignoreTiles.Add(x.Key + Vector2Int.down); 
                ignoreTiles.Add(x.Key + Vector2Int.left); 
                ignoreTiles.Add(x.Key + new Vector2Int(-1, -1)); 
            });
            base.SpawnTiles();
        }
    }
}