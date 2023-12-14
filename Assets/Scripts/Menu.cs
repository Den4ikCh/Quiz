using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private void Awake()
    {
        if (PlayerPrefs.GetString("Score") != null)
            text.text = PlayerPrefs.GetString("Score");
    }

    public void StartQuiz()
    {
        
    }
}
