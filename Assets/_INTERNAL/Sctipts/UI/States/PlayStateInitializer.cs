using Game.GameCore;
using Game.Timer;
using UI.Score;
using UI.Tips;
using UnityEngine;

namespace UI.States
{
    public class PlayStateInitializer: MonoBehaviour
    {
        [Header("Game Systems")]
        [SerializeField] private TimerHolder _timerHolder;
        [SerializeField] private StateChecker _stateChecker;
        [SerializeField] private VariantsHolder _variantsHolder;
        [SerializeField] private TipsHolder _tipsHolder;

        [Space(10), Header("UI")]
        [SerializeField] private Player.WalletView _walletView;
        [SerializeField] private CoinsTextPool _coinsTextPool;
        [SerializeField] private ScoreView _scoreView;

        private SmoothDifficultChanger _smoothDifficultChanger;

        public TimerHolder TimerHolder => _timerHolder;
        public StateChecker StateChecker => _stateChecker;
        public VariantsHolder VariantsHolder => _variantsHolder;
        public SmoothDifficultChanger SmoothDifficultChanger => _smoothDifficultChanger;
        public Player.WalletView WalletView => _walletView;
        public ScoreView ScoreView => _scoreView;

        public void PlayStateInitialize(float progressStep)
        {
            _timerHolder.Initialize();

            _smoothDifficultChanger = new(progressStep);

            _walletView.InitializeWallet(PlayerWallet.GetWallet());
            _walletView.InitializePool(_coinsTextPool);
            _scoreView.Initialization();
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