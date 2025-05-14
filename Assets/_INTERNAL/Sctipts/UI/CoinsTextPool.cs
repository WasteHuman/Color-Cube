using UnityEngine;
using Utils;

namespace UI
{
    public class CoinsTextPool : MonoBehaviour
    {
        [Header("Pool settings")]
        [SerializeField] private int _poolCount;
        [SerializeField] private bool _autoExpand;

        [Space(10), Header("Refs")]
        [SerializeField] private CoinsAddedAnimation _coinsPrefab;
        [SerializeField] private Transform _container;

        private ObjectPool<CoinsAddedAnimation> _coinsTextPool;

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        public void Initialization()
        {
            _coinsTextPool = new(_coinsPrefab, _poolCount, _container)
            {
                AutoExpand = _autoExpand
            };

            SubscribeOnEvents();
        }

        private void SubscribeOnEvents()
        {
            foreach (CoinsAddedAnimation coin in _coinsTextPool)
            {
                coin.TimeEnded += ReturnToPool;
            }
        }

        private void UnsubscribeFromEvents()
        {
            foreach (CoinsAddedAnimation coin in _coinsTextPool)
            {
                coin.TimeEnded -= ReturnToPool;
            }
        }

        public CoinsAddedAnimation GetCoinsText()
        {
            return _coinsTextPool.GetFreeElement();
        }

        public void ReturnToPool(CoinsAddedAnimation obj)
        {
            _coinsTextPool.ReturnToPool(obj);
        }
    }
}