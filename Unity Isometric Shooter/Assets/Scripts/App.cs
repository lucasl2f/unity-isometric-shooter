using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class App : MonoBehaviour
{
    [SerializeField]
    private GameObject playerAsset;

    private UIDocument _rootUIDocument;

    private UIController _uiController;
    private GameController _gameController;
    private SoundController _soundController;
    private PlayerController _playerController;

    private void Awake()
    {
        _rootUIDocument = GetComponent<UIDocument>();
        var bgmSource = gameObject.AddComponent<AudioSource>();
        var sfxSource = gameObject.AddComponent<AudioSource>();

        var audioBank = Resources.Load<AudioBank>("Audio/AudioBank");

        // Controllers
        _uiController = new UIController(_rootUIDocument);
        _gameController = new GameController();
        _soundController = new SoundController(bgmSource, sfxSource, audioBank);

        // Events
        _uiController.OnStartButtonPressed          += _gameController.StartGame;
        _uiController.OnPlayBgmMenuBtnPressed       += _soundController.PlayMainMenuMusic;
        _uiController.OnPlayBgmMissionBtnPressed    += _soundController.PlayGameplayMusic;
        _uiController.OnStopBgmBtnPressed           += _soundController.StopBGM;
        _uiController.OnButtonClicked               += _soundController.PlayButtonClick;
    }

    private void Start()
    {
        var player = GameObject.Instantiate(playerAsset);
        _playerController = player.GetComponent<PlayerController>();
        _playerController.OnFootstepEvent += _soundController.PlayFootstep;
    }

    private void OnDestroy()
    {
        // Events
        _uiController.OnStartButtonPressed          -= _gameController.StartGame;
        _uiController.OnPlayBgmMenuBtnPressed       -= _soundController.PlayMainMenuMusic;
        _uiController.OnPlayBgmMissionBtnPressed    -= _soundController.PlayGameplayMusic;
        _uiController.OnStopBgmBtnPressed           -= _soundController.StopBGM;
        _uiController.OnButtonClicked               -= _soundController.PlayButtonClick;
        _playerController.OnFootstepEvent           -= _soundController.PlayFootstep;
    }
}