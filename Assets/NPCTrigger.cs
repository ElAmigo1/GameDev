using UnityEngine;
using System.Collections;

public class NPCTrigger : MonoBehaviour
{
    public Dialog dialogManager;
    private bool hasTalked = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTalked && other.CompareTag("Player"))
        {
            hasTalked = true;
            dialogManager.StartDialog();
            StartCoroutine(DisappearAfterDialog());
        }
    }

    private IEnumerator DisappearAfterDialog()
    {
        // Warte, bis der DialogPanel inaktiv ist (Dialog vorbei)
        while (dialogManager.gameObject.activeInHierarchy && dialogManager.dialogPanel.activeSelf)
        {
            yield return null;
        }

        // Danach NPC verschwinden lassen
        gameObject.SetActive(false);
    }
}
