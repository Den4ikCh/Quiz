using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
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
    private int index;
    private int correctAnswers;

    private void Awake()
    {
        SetQuestion(0);
        buttons[0].enabled = true;
        buttons[0].GetComponent<Image>().enabled = true;
        buttons[0].GetComponentInChildren<TextMeshProUGUI>().enabled = true;
    }

    public void SetQuestion(int i)
    {
        question.text = questions[i].name;
        for (int j = 0; j < buttons.Length; j++)
        {
            buttons[j].image.sprite = sprites[0];
            buttons[j].GetComponentInChildren<TextMeshProUGUI>().text = questions[i].variants[j];
        }
    }

    void PrintResults()
    {
        for (int j = 0; j < buttons.Length; j++)
        {
            buttons[j].GetComponentInChildren<TextMeshProUGUI>().text = "";
            buttons[j].GetComponent<Image>().enabled = false;
            buttons[j].enabled = false;
        }
        PlayerPrefs.SetString("Score", Convert.ToString(correctAnswers));
        question.text = $"Верно {correctAnswers}/{questions.Length}";
        reload.enabled = true;
        reload.GetComponent<Image>().enabled = true;
        reload.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
    }

    public void Click0()
    {
        if (questions[index].isTrue(questions[index].variants[0])) correctAnswers++;
        index++;
        if (index < questions.Length)
            StartCoroutine(SwitchColor(0));
        else
            PrintResults();
    }

    public void Click1()
    {
        if (questions[index].isTrue(questions[index].variants[1])) correctAnswers++;
        index++;
        if (index < questions.Length)
            StartCoroutine(SwitchColor(1));
        else
            PrintResults();
    }

    public void Click2()
    {
        if (questions[index].isTrue(questions[index].variants[2])) correctAnswers++;
        index++;
        if (index < questions.Length)
            StartCoroutine(SwitchColor(2));
        else
            PrintResults();
    }

    public void Click3()
    {
        if (questions[index].isTrue(questions[index].variants[3])) correctAnswers++;
        index++;
        if (index < questions.Length)
            StartCoroutine(SwitchColor(3));
        else
            PrintResults();
    }

    IEnumerator SwitchColor(int index)
    {
        buttons[index].image.sprite = sprites[1];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = false;
        }

        yield return new WaitForSeconds(2f);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = true;
        }
        SetQuestion(index);
    }
}
