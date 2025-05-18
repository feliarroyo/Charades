using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

/// <summary>
/// This class loads custom categories detected in the category selection screen.
/// </summary>
public class CustomCategoryLoader : MonoBehaviour
{
    public GameObject categoryButtonPrefab;
    public GameObject customParent;

    public static bool isExporting;
    public TextMeshProUGUI exportButtonLabel;
    public TextMeshProUGUI importButtonLabel;
    public TextMeshProUGUI titleLabel;

    // Start is called before the first frame update
    void Start()
    {
        isExporting = false;
        CreateCustomCategoryButtons(categoryButtonPrefab, customParent, true);
        #if UNITY_STANDALONE_WIN || UNITY_EDITOR
            importButtonLabel.text = "Ver directorio";
        #endif
    }

    public static void CreateCustomCategoryButtons(GameObject prefab, GameObject parent, bool singleSelect = false)
    {
        CategoryCreator.CreateCustomDirectory();
        foreach (string file in Directory.GetFiles(Const.customDirectory, "*.json"))
        {
            StreamReader reader = new(file);
            GameObject newGameObject = Instantiate(prefab, parent.transform);
            newGameObject.GetComponentInChildren<CategoryButton>().jsonCategory = new TextAsset(reader.ReadToEnd());
            newGameObject.GetComponentInChildren<CategoryButton>().customFilePath = file;
            if (singleSelect)
                newGameObject.GetComponentInChildren<CategoryButton>().SetSingleSelect();
        }
    }

    /// <summary>
    /// Goes to file picker to import a category from a .JSON file.
    /// </summary>
    public void ImportCategory()
    {
        #if UNITY_STANDALONE_WIN || UNITY_EDITOR
            string filePath = Application.persistentDataPath + "/customCategories";
            ShowCustomFolder(filePath);
        #endif
        #if UNITY_ANDROID
        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
            {
                if (path != null && path.EndsWith(".json"))
                {
                    string savePath = Application.persistentDataPath + "/customCategories/" + Path.GetFileName(path);
                    File.Copy(path, savePath, true);
                    // TO-DO: Check if a file would be overwritten
                    SceneManager.LoadScene("CategorySelect");
                }
                else {
                    Debug.Log("Invalid file type");
                }

            });
        #endif
    }

    public void ShowCustomFolder(string filePath)
    {
        filePath = filePath.Replace(@"/", @"\");   // explorer doesn't like front slashes
        System.Diagnostics.Process.Start("explorer.exe", filePath);
    }

    public void SetExportMode()
    {
        isExporting = !isExporting;
        exportButtonLabel.text = isExporting ? "Editar categorías" : "Compartir categoría";
        titleLabel.text = isExporting ? "Selecciona la categoría que quieras compartir" : "Creador de categorías";
    }
}