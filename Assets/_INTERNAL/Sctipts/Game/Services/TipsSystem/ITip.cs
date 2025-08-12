﻿using Game.Tips.TipState;

namespace Game.Services.TipSystem
{
    public interface ITip
    {
        TipState State { get; }
        void Initialize();
        void InitializeTimer(float time);
        void SetState(TipState state);
        void Tick();
        void TipClicked();
        void TipEnabled();
        void ActiveCooldown();
        void StopAllTimers();
        void IncreaseCost();
        void DecreaseCost();
    }
}