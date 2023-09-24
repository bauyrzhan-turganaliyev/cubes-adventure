using System;
using System.Collections.Generic;
using Actions;
using Actions.CubeActions;

namespace Infrastructure
{
    public class MessageBus
    {
        public Action<GameState> OnGameStateChanged;
        public Action OnActionEnded;
        public Action<bool> OnSwitchSettingsPanel;
        public Action<Dictionary<int, Cube>> OnCubesInitialized;
        public Action OnSettingsChanged;
    }
}