using System;
using UnityEngine.UIElements;

public class UIController
{
    private readonly VisualElement _root;

    public event Action OnStartButtonPressed;
    public event Action OnPlayBgmMenuBtnPressed;
    public event Action OnPlayBgmMissionBtnPressed;
    public event Action OnStopBgmBtnPressed;
    public event Action OnButtonClicked;

    public UIController(UIDocument uIDocument)
    {
        _root = uIDocument.rootVisualElement;
        var startBtn = _root.Q<Button>("start-button");
        startBtn.clicked += () => OnStartButtonPressed?.Invoke();
        startBtn.clicked += () => OnButtonClicked?.Invoke();

        var playBgmMenuBtn = _root.Q<Button>("play-bgm-menu-button");
        playBgmMenuBtn.clicked += () => OnPlayBgmMenuBtnPressed?.Invoke();
        playBgmMenuBtn.clicked += () => OnButtonClicked?.Invoke();

        var playBgmMissionBtn = _root.Q<Button>("play-bgm-mission-button");
        playBgmMissionBtn.clicked += () => OnPlayBgmMissionBtnPressed?.Invoke();
        playBgmMissionBtn.clicked += () => OnButtonClicked?.Invoke();

        var stopBgmBtn = _root.Q<Button>("stop-bgm-button");
        stopBgmBtn.clicked += () => OnStopBgmBtnPressed?.Invoke();
        stopBgmBtn.clicked += () => OnButtonClicked?.Invoke();
    }
}