using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(fileName = "GameLevelSettings", menuName = "AsteroidsMenu")]
    public class GameLevel : ScriptableObject
    {
        public int asteroids_Per_Level;
    }
}