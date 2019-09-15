using HappyUnity.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids
{
    public class GameScreenLogger : ScreenLogger
    {
        private const string GameTitle = "ASTEROIDS \n CLICK SPACE TO START \n CLICK ESC TO QUIT \n CLICK R TO SWAP WEAPONS";
        private const string GameLevelCleaned = "Level Complited!";
        private const string GameOver = "GAME IS OVER \n YOUR SCORE IS {0}";

        public TextMeshProUGUI mainscreen_Text;

        public static GameScreenLogger New(TextMeshProUGUI text)
        {
            var instance = CreateInstance<GameScreenLogger>();
            instance.mainscreen_Text = text;
            return instance;
        }
        
        public void Show_StartTitle()
        {
            PushTextMessage(mainscreen_Text, GameTitle);
        }
        public void Show_LevelCleanedTitle()
        {
            PushTextMessage(mainscreen_Text, GameLevelCleaned);
        }
        public void Show_GameOverTitle()
        {
            PushTextMessage(mainscreen_Text, string.Format(GameOver, Score.Total_Score_Earned));
        }
        public void Clear_Text()
        {
            PushTextMessage(mainscreen_Text, "");
        }
        
    }
}