using System;
using System.Linq;
using HappyUnity.Singletons;
using UnityEngine;

namespace Asteroids
{
    [Serializable]
    public class LevelSystem : Singleton<LevelSystem>
    {
        public static int Current_Level = 1;
        private const string ResourceFolderPath = "LevelS";


        public bool GameOver;

        public GameLevel[] levels;

        protected override void Awake()
        {
            base.Awake();
            Ship.OnShipDeath += delegate { GameOver = true; };
            levels = Resources.LoadAll<GameLevel>(ResourceFolderPath).OrderBy(x => int.Parse(x.name)).ToArray();
        }

        public GameLevel Get_GameLevel()
        {
            return levels[Current_Level - 1];
        }

        public bool GameIsOver()
        {
            return Current_Level == levels.Length || GameOver;
        }

        public void AddLevel()
        {
            Current_Level++;
        }
    }
}