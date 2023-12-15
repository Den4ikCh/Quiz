using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Animator snowflake;

    public void StartQuiz()
    {
        button.enabled = false;
        button.GetComponent<Image>().enabled = false;
        button.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        snowflake.SetTrigger("Anim");
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(1);
    }
}
