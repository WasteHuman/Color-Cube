﻿using UnityEngine;

namespace UI.MainMenu.Background
{
    [CreateAssetMenu(menuName = "Material pressets/Background", fileName = "Background presset")]
    public class BackgroundPreset : ScriptableObject
    {
        [field: SerializeField] public Material PresetMaterial { get; private set; }
    }
}