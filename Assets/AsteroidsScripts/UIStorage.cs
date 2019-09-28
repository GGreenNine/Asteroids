using System;
using DefaultNamespace;
using HappyUnity.Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids
{
    public class UIStorage : Singleton<UIStorage>
    {
        public TextMeshProUGUI mainscreen_Text;
        public ScoreUI ScoreUi;
        public AmmoUI AmmoUi;
        
        private void Awake()
        {
            ScoreUi.Awake();
            AmmoUi.Awake();
        }

        protected void OnEnable()
        {
            ScoreUi.OnEnable();
        }

        [Serializable]
        public class ScoreUI
        {
            [SerializeField]
            Color textColor = Color.white;
            private GameScreenLogger scoreLogger;
            [SerializeField]
            private TextMeshProUGUI scoreText;
            
            public void Awake()
            {
                scoreLogger = GameScreenLogger.New(scoreText);
                scoreText.color = textColor;
            }
            
            public void OnEnable()
            {
                scoreLogger.PushTextMessage(scoreText, Score.Total_Score_Earned.ToString());
                Score.OnScoreAded += ScoreEarned;
            }
            
            void ScoreEarned(int points)
            {
                scoreLogger.PushTextMessage(scoreText, Score.Total_Score_Earned.ToString());
            }
            
            void Reset()
            {
                scoreLogger = GameScreenLogger.New(scoreText);
                scoreLogger.Clear_Text();
            }
        }
        
        [Serializable]
        public class AmmoUI
        {
            [SerializeField]
            Color textColor = Color.white;
            private GameScreenLogger ammoLogger;
            [SerializeField]
            private TextMeshProUGUI ammoText;

            public LaserBehaviour laserInfo;
            
            
            public void Awake()
            {
                ammoLogger = GameScreenLogger.New(ammoText);
                ammoText.color = textColor;
            }
            
            public void InitializeLazerInfo(LaserBehaviour laserInfo)
            {
                this.laserInfo = laserInfo;
                ammoLogger.PushTextMessage(ammoText, laserInfo.CurrentAmmoLoaded.ToString());
                laserInfo.On_ammoCountChanged += InfoOnOnAmmoCountChanged;
            }
            
            void Reset()
            {
                ammoLogger = GameScreenLogger.New(ammoText);
                ammoLogger.Clear_Text();
            }

            private void InfoOnOnAmmoCountChanged()
            {
                ammoLogger.PushTextMessage(ammoText, laserInfo.CurrentAmmoLoaded.ToString());
            }
        }
        
    }
}