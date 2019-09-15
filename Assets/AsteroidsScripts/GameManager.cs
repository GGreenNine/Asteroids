using System;
using System.Collections;
using System.Collections.Generic;
using HappyUnity.Singletons;
using HappyUnity.Spawners;
using HappyUnity.Spawners.ObjectPools;
using HappyUnity.TransformUtils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids
{
    public class GameManager : Singleton<GameManager>
    {
        public GameObject ship_Prefab;
        public GameObject UFO_Prefab;
        public GameObject AsteroidBig_Prefab;
        public GameObject AsteroidSmall_Prefab;
        public GameField GameField;
        
        private GameScreenLogger gameScreenLogger;
        Ship ship;
        
        private ObjectPool _smallAsteroidsPool;
        private ObjectPool _bigAsteroidsPool;

        public enum GameState
        {
            Pause,
            Playing,
            End,
            Quit
        }
        bool requestTitleScreen = true;
        public static GameState CurrentGameState;

        public void SetGameState(GameState state)
        {
            switch (state)
            {
                case GameState.Quit:
                    Application.Quit();
                    break;
                case GameState.Pause:
                    Pause.Set();
                    break;
                case GameState.Playing:
                    Pause.Unset();
                    break;
                case GameState.End:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }

            CurrentGameState = state;
        }
        
        private void Awake()
        {
            _bigAsteroidsPool = ObjectPool.Build(AsteroidBig_Prefab, 25, 25);
            _smallAsteroidsPool = ObjectPool.Build(AsteroidSmall_Prefab, 25, 25);
            gameScreenLogger = GameScreenLogger.New(UIStorage.Instance.mainscreen_Text);
            AsteroidsManager.New(_bigAsteroidsPool, _smallAsteroidsPool);
        }

        private void Start()
        {
            ship = Ship.Spawn(ship_Prefab);
            ship.RemoveFromGame();
            StartCoroutine(Background_Game_Workflow());
            StartCoroutine(SpawnUFO());
        }

        /// <summary>
        /// Works in backgroud, changing the game Behaviour
        /// for current game state
        /// </summary>
        /// <returns></returns>
        IEnumerator Background_Game_Workflow()
        {
            while (true)
            {
                if (requestTitleScreen)
                {
                    requestTitleScreen = false;
                    yield return StartCoroutine(ShowingPreGameTitle());
                }

                yield return StartCoroutine(StartLevel());
                yield return StartCoroutine(PlayLevel());
                yield return StartCoroutine(EndLevel());
                GC.Collect();
            }
        }

        IEnumerator ShowingPreGameTitle()
        {
            SetGameState(GameState.Pause);
            GameField.Generate_GameField();
            gameScreenLogger.Show_StartTitle();
            AsteroidsManager.Instance.ShowAsteroids();
            while (CurrentGameState != GameState.Playing) yield return null;
            AsteroidsManager.Instance.HideAsteroids();
        }

        IEnumerator StartLevel()
        {
            LevelSystem.Instance.GameOver = false;
            ship.Recover();
            ship.EnableControls();
            gameScreenLogger.Clear_Text();
            yield return Pause.Long();
            SpawnAsteroids(LevelSystem.Instance.Get_GameLevel().asteroids_Per_Level);
        }

        IEnumerator PlayLevel()
        {
            SetGameState(GameState.Playing);
            gameScreenLogger.Clear_Text();
            while (!LevelSystem.Instance.GameOver && Asteroid.Any)
            {
                yield return null;
            }
        }

        IEnumerator EndLevel()
        {
            if (LevelSystem.Instance.GameIsOver())
            {
                SetGameState(GameState.End);
                gameScreenLogger.Show_GameOverTitle();
                yield return Pause.Brief();
                Score.Reset();
                RemoveRemainingGameTokens();
                gameScreenLogger.Clear_Text();
                DefineNewGame();
            }
            else
            {
                gameScreenLogger.Show_LevelCleanedTitle();
                yield return Pause.Brief();
                LevelSystem.Instance.AddLevel();
            }

            yield return Pause.Long();
        }
        
        IEnumerator SpawnUFO()
        {
            GameObject ufo = Instantiate(UFO_Prefab);
            ufo.SetActive(false);
            ufo.GetComponent<UFO>().target = ship.transform;
            ufo.GetComponent<UFO>().ConfigureSmoothMover();

            while (true)
            {

                var wait = UnityEngine.Random.Range(10, 20f);
                yield return new WaitForSeconds(wait);
                if (!ship.gameObject.activeSelf || ufo.activeSelf) continue;
                if(CurrentGameState == GameState.Pause || CurrentGameState == GameState.End)
                    continue;
                ufo.SetActive(true);
                ufo.GetComponent<UFO>().Spawn();
            }
        }
        
        public static void SpawnAsteroid(Vector3 position, Asteroid.Asteroid_Type asteroidType)
        {

            Asteroid asteroid = null;
            switch (asteroidType)
            {
                case Asteroid.Asteroid_Type.Big:
                    asteroid = Instance._bigAsteroidsPool.GetRecyclable<Asteroid>();
                    break;
                case Asteroid.Asteroid_Type.Small:
                    asteroid = Instance._smallAsteroidsPool.GetRecyclable<Asteroid>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(asteroidType), asteroidType, null);
            }

            if (asteroid != null) asteroid.SpawnAt(position);
        }
        
        void RemoveRemainingGameTokens()
        {
            foreach (var a in FindObjectsOfType<GameUnit>())
                a.RemoveFromGame();
        }

        void DefineNewGame()
        {
            ship.GetComponent<ShipShooter>().InitializeWeaponInventory();
            requestTitleScreen = true;
            LevelSystem.Current_Level = 1;
        }
        
        void SpawnAsteroids(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ObjectPool bigOrSmall = i % 2 == 0 ? _bigAsteroidsPool : _smallAsteroidsPool;
                var asteroid = bigOrSmall.GetRecyclable<Asteroid>();
                asteroid.Spawn();
            }
        }
    }
}