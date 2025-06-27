using UnityEngine;

namespace MainMenu.Background
{
    [CreateAssetMenu(menuName = "Material pressets/Background", fileName = "Background presset")]
    public class BackgroundPresset : ScriptableObject
    {
        [field: SerializeField] public Material PressetMaterial;
    }
}