using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MainMenu
{
    public class MainCubeColorRandomizer
    {
        public event Action<Color> ColorGenerated;

        public void GenerateColor()
        {
            Color color = new(Random.value, Random.value, Random.value);

            ColorGenerated?.Invoke(color);
        }
    }
}