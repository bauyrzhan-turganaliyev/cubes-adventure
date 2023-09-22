using System;

public class MessageBus
{
    public Action<GameCurrentAction> OnChangeGameAction;
    public Action<bool> OnSwitchSettingsPanel;
}