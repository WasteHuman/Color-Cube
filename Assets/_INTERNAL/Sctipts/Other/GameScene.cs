using UnityEditor;
using UnityEngine;

namespace Other
{
    [CreateAssetMenu(menuName = "Scene prefabs", fileName = "Game scene")]
    public class GameScene : ScriptableObject
    {
        public SceneAsset Scene;
        public string SceneName => Scene.name;
    }
}