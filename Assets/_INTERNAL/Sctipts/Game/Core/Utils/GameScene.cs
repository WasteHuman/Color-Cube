using UnityEngine;

namespace Other
{
    [CreateAssetMenu(menuName = "Scene SO/Scene Prefab", fileName = "Game Scene")]
    public class GameScene : ScriptableObject
    {
        [field: SerializeField] public string SceneName { get; private set; }
    }
}