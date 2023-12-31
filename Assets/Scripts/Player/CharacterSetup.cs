using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSetup : MonoBehaviour
{
    public TMP_InputField input;
    public Button button;
    public GameObject buttonPrefab;
    public Transform savedGamesButtonsParent;
    public int selectedLevel;
    private void Start()
    {
        button.enabled = false;
        button.onClick.AddListener(() =>
        {
            selectedLevel = LevelSelector.SelectedlLevel();
            if (selectedLevel == 1 || selectedLevel == 2 || selectedLevel == 3)
            {
                LoadingScreen.Instance.LoadLevel(selectedLevel);
            }
        });


        input.onEndEdit.AddListener(CheckEntry);
        input.onDeselect.AddListener(CheckEntry);

        SetupSavedGames();
    }

    public void CheckEntry(string s)
    {
        if (!string.IsNullOrEmpty(s))
        {
            button.enabled = true;
            PlayerData.Instance.SetPlayerName(s);
        }
    }

    private void SetupSavedGames()
    {
        string path = Path.Combine(Application.persistentDataPath, "Saved Data");
        string[] files = Directory.GetFiles(path);
        foreach (string file in files)
        {
            
            GameObject button = Instantiate(buttonPrefab, savedGamesButtonsParent);
            string fileName = Path.GetFileName(file);
            if (fileName == "SoundSettings.json")
                continue;

            string name = fileName.Split('_')[0];
            button.GetComponent<SavedGameButton>().Init(() => {
                button.name = fileName;
                PlayerData.Instance.LoadPlayerData(file);
                selectedLevel = LevelSelector.SelectedlLevel();
                if (selectedLevel == 1 || selectedLevel == 2 || selectedLevel == 3)
                {
                    LoadingScreen.Instance.LoadLevel(selectedLevel);
                }
            }, name);
        }
    }
}
