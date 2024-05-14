using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControlButton : MonoBehaviour
{
    LevelDistance levelDistance;
    

    enum TargetScene
    {
        Next,
        LevelNext,
        Previous,
        Present,
        MainMenu,
        Quit,
        Credit
    }

    [SerializeField] TargetScene targetScene;
    Button button;

    void Start()
    {
        levelDistance = FindObjectOfType<LevelDistance>();
        button = GetComponent<Button>();

        button.onClick.RemoveAllListeners();
        switch (targetScene)
        {
            case TargetScene.MainMenu:
                button.onClick.AddListener(() => SceneController.LoadMainScene());
                break;

            case TargetScene.Next:

                button.onClick.AddListener(() => SceneController.LoadNextScene());
                break;

            case TargetScene.LevelNext:
                if (levelDistance != null && levelDistance.disRun > 100)
                {
                    button.onClick.AddListener(() => SceneController.LoadNextScene());
                }
                break;

            case TargetScene.Present:
                button.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
                break;

            case TargetScene.Previous:
                button.onClick.AddListener(() => SceneController.LoadPreviousScene());
                break;

            case TargetScene.Quit:
                button.onClick.AddListener(() => Application.Quit());
                break;

            case TargetScene.Credit:               
                button.onClick.AddListener(() => SceneManager.LoadScene("Credit"));
                break;


        }


    }

}
