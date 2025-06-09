using UnityEngine;
using TMPro;
using System.Collections;

public class Dialog : MonoBehaviour
{
    public GameObject dialogPanel;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed = 0.05f;

    private int index;
    private bool isTyping = false;

    private void Start()
    {
        dialogPanel.SetActive(false);
        textComponent.text = string.Empty;
    }

    public void StartDialog()
    {
        dialogPanel.SetActive(true);
        index = 0;
        StartCoroutine(TypeLine());
    }

    private void Update()
    {
        if (dialogPanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                isTyping = false;
            }
            else
            {
                NextLine();
            }
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = "";
        foreach (char c in lines[index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogPanel.SetActive(false);
        }
    }
}
