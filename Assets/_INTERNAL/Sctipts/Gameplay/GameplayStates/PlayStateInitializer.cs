using Gameplay.GameCore;
using Gameplay.Player;
using Gameplay.Timer;
using Gameplay.TipSystem;
using UI;
using UnityEngine;

namespace Gameplay.GameplayStates
{
    public class PlayStateInitializer: MonoBehaviour
    {
        [Header("Game Systems")]
        [SerializeField] private TimerHolder _timerHolder;
        [SerializeField] private StateChecker _stateChecker;
        [SerializeField] private VariantsHolder _variantsHolder;
        [SerializeField] private TipsHolder _tipsHolder;

        [Space(10), Header("UI")]
        [SerializeField] private WalletView _walletView;
        [SerializeField] private CoinsTextPool _coinsTextPool;

        private SmoothDifficultChanger _smoothDifficultChanger;

        public TimerHolder TimerHolder => _timerHolder;
        public StateChecker StateChecker => _stateChecker;
        public VariantsHolder VariantsHolder => _variantsHolder;
        public SmoothDifficultChanger SmoothDifficultChanger => _smoothDifficultChanger;

        public void PlayStateInitialize(float progressStep)
        {
            _timerHolder.Initialize();

            _smoothDifficultChanger = new(progressStep);

            _walletView.InitializeWallet(PlayerWallet.GetWallet());
            _walletView.InitializePool(_coinsTextPool);
        }

        public void GameSystemsInitialize()
        {
            _variantsHolder.InitializeHolder(_stateChecker);
            _variantsHolder.Subscribe();

            _stateChecker.InitializeChecker(_variantsHolder);
            _stateChecker.Subscribe();

            _tipsHolder.InitializeHolder();

            _coinsTextPool.Initialization();
        }
    }
}