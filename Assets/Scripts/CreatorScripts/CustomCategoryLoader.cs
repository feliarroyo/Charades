using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;
using UnityEngine.Analytics;

/// <summary>
/// This class loads custom categories detected in the category selection screen.
/// </summary>
public class CustomCategoryLoader : MonoBehaviour
{
    public enum Status
    {
        Edit,
        Export,
        Delete
    };
    public GameObject categoryButtonPrefab;
    public GameObject customParent;

    public static Status status;
    public TextMeshProUGUI exportButtonLabel;
    public TextMeshProUGUI importButtonLabel;
    public TextMeshProUGUI deleteButtonLabel;
    public TextMeshProUGUI titleLabel;

    // Start is called before the first frame update
    void Start()
    {
        status = Status.Edit;
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
        switch (status)
        {
            case Status.Export:
                status = Status.Edit;
                exportButtonLabel.text = "Compartir categoría";
                titleLabel.text = "Creador de categorías";
                break;
            default:
                if (status == Status.Delete)
                    deleteButtonLabel.text = "Borrar categoría";
                status = Status.Export;
                exportButtonLabel.text = "Editar categorías";
                titleLabel.text = "Selecciona la categoría que quieras compartir";
                break;
        }
    }

    public void SetDeleteMode()
    {
        switch (status)
        {
            case Status.Delete:
                status = Status.Edit;
                deleteButtonLabel.text = "Borrar categoría";
                titleLabel.text = "Creador de categorías";
                break;
            default:
                if (status == Status.Export)
                    exportButtonLabel.text = "Compartir categoría";
                status = Status.Delete;
                deleteButtonLabel.text = "Editar categorías";
                titleLabel.text = "Selecciona la categoría que quieras borrar";
                break;
        }
    }
}