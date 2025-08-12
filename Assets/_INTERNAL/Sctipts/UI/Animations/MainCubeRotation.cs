using UnityEngine;

namespace Animations
{
    public class MainCubeRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;

        private void Update()
        {
            float angle = _rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + angle, transform.eulerAngles.z);
        }
    }
}