using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName; // Ge�ilecek sahnenin ismi

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SkipCutscene();
        }
    }

    public void SkipCutscene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
