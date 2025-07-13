using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("UI References")]
    public Button playButton;
    public Button quitButton;
    public Button showCardButton;
    public GameObject cardPanel;
    public Text cardText;

    [Header("Scene Settings")]
    public string mainSceneName = "MainScene";

    [Header("Card Settings")]
    [TextArea]
    public string cardMessage = "Questo è un messaggio di esempio sulla card.";

    void Start()
    {
        playButton.onClick.AddListener(OnPlayClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
        showCardButton.onClick.AddListener(OnShowCardClicked);

        // Aggiungi questo se il cardPanel ha un Button
        Button cardButton = cardPanel.GetComponent<Button>();
        if (cardButton != null)
            cardButton.onClick.AddListener(HideCard);

        cardPanel.SetActive(false);
        cardText.text = cardMessage;
    }

    void OnPlayClicked()
    {
        SceneManager.LoadScene("S_Level1");
    }

    void OnQuitClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void OnShowCardClicked()
    {
        cardPanel.SetActive(true);
    }

    // Opzionale: chiudi la card cliccando su di essa
    public void HideCard()
    {
        cardPanel.SetActive(false);
    }
}
