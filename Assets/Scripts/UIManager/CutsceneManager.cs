using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName; // Gešilecek sahnenin ismi

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
