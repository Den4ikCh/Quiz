using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizQuestions : MonoBehaviour
{
    [System.Serializable]
    struct Question
    {
        public string name;
        public string[] variants;
        public string answer;
        public bool isTrue(string nameOfAnswer)
        {
            if (nameOfAnswer == answer) return true;
            return false;
        }
    }

    [SerializeField] Question[] questions;
    [SerializeField] TextMeshProUGUI question;
    [SerializeField] Button[] buttons;
    [SerializeField] Button reload;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Image[] images;
    [SerializeField] TextMeshProUGUI[] texts;
    [SerializeField] Image score;
    [SerializeField] Button exit;
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] AudioSource[] sounds;
    private int index;
    private int nowScore;
    private float Timer;

    private void Awake()
    {
        SetQuestion(0);
        buttons[0].enabled = true;
        buttons[0].GetComponent<Image>().enabled = true;
        buttons[0].GetComponentInChildren<TextMeshProUGUI>().enabled = true;
    }

    void Update()
    {
        if (Timer >= 0) Timer += Time.deltaTime;
        timer.text = (20 - (int)Timer).ToString();
        if (Timer >= 20f) StartCoroutine(OffButtons());
        if (Timer >= 10f && !sounds[2].isPlaying)
        {
            sounds[2].Play();
            sounds[3].volume = 0.1f;
        }
    }

    public void SetQuestion(int i)
    {
        sounds[3].volume = 0.2f;
        question.text = questions[i].name;
        Timer = 0;
        for (int j = 0; j < buttons.Length; j++)
        {
            buttons[j].image.sprite = sprites[0];
            buttons[j].GetComponentInChildren<TextMeshProUGUI>().text = questions[i].variants[j];
        }
    }

    void PrintResults()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            buttons[i].GetComponent<Image>().enabled = false;
            buttons[i].enabled = false;
        }
        exit.enabled = true;
        exit.GetComponent<Image>().enabled = true;
        exit.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        if (PlayerPrefs.GetInt("Score") < nowScore)
            PlayerPrefs.SetInt("Score", nowScore);
        question.text = "";
        score.enabled = false;
        score.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        for (int i = 0; i < 2; i++)
        {
            texts[i].enabled = true;
            images[i].enabled = true;
        }
        images[0].GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        images[0].GetComponentInChildren<TextMeshProUGUI>().text = nowScore.ToString();
        images[1].GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        images[1].GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Score").ToString();
    }

    public void Click0()
    {
        if (questions[index].isTrue(questions[index].variants[0])) AddPoints();
        index++;
        StartCoroutine(SwitchColor(0));
    }

    public void Click1()
    {
        if (questions[index].isTrue(questions[index].variants[1])) AddPoints();
        index++;
        StartCoroutine(SwitchColor(1));
    }

    public void Click2()
    {
        if (questions[index].isTrue(questions[index].variants[2])) AddPoints();
        index++;
        StartCoroutine(SwitchColor(2));
    }

    public void Click3()
    {
        if (questions[index].isTrue(questions[index].variants[3])) AddPoints();
        index++;
        StartCoroutine(SwitchColor(3));
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    void AddPoints()
    {
        if (Timer < 10f) nowScore += 100;
        else nowScore += 100 - 10 * ((int)Timer - 10);
    }

    IEnumerator SwitchColor(int indexOfButton)
    {
        Timer = -1;
        if (questions[index - 1].isTrue(questions[index - 1].variants[indexOfButton]))
            buttons[indexOfButton].image.sprite = sprites[1];
        else
            buttons[indexOfButton].image.sprite = sprites[2];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = false;
            if (i != indexOfButton)
                buttons[i].image.sprite = sprites[3];
        }
        score.GetComponentInChildren<TextMeshProUGUI>().text = nowScore.ToString();
        timer.enabled = false;
        sounds[2].Stop();

        if (questions[index - 1].isTrue(questions[index - 1].variants[indexOfButton]))
        {
            sounds[0].Play();
            sounds[3].Pause();
            yield return new WaitForSeconds(4f);
        }
        else
        {
            sounds[1].Play();
            sounds[3].Pause();
            yield return new WaitForSeconds(2f);
        }

        sounds[3].Play();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = true;
        }
        if (index < questions.Length)
        {
            SetQuestion(index);
            timer.enabled = true;
        }
        else PrintResults();
    }

    IEnumerator OffButtons()
    {
        Timer = -1;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = false;
            buttons[i].image.sprite = sprites[2];
        }
        timer.enabled = false;
        
        sounds[1].Play();
        sounds[3].Pause();
        yield return new WaitForSeconds(2f);
        sounds[3].Play();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = true;
        }
        index++;
        if (index < questions.Length)
        { 
            SetQuestion(index);
            timer.enabled = true;
        }
        else PrintResults();
    }
}
