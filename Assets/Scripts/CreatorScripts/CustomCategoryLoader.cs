using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;

/// <summary>
/// This class loads custom categories detected in the category selection screen.
/// </summary>
public class CustomCategoryLoader : MonoBehaviour
{
    public GameObject categoryButtonPrefab;
    public GameObject customParent;

    // Start is called before the first frame update
    void Start()
    {
        CreateCustomCategoryButtons(categoryButtonPrefab, customParent, true);
    }

    public static void CreateCustomCategoryButtons(GameObject prefab, GameObject parent, bool singleSelect = false){
        CategoryCreator.CreateCustomDirectory();
        foreach (string file in Directory.GetFiles(Const.customDirectory,"*.json")){
            StreamReader reader = new(file);
            GameObject newGameObject = Instantiate(prefab, parent.transform);
            newGameObject.GetComponentInChildren<CategoryButton>().jsonCategory = new TextAsset(reader.ReadToEnd());
            if (singleSelect)
                newGameObject.GetComponentInChildren<CategoryButton>().SetSingleSelect();
        }
    }

    /// <summary>
    /// Goes to file picker to import a category from a .JSON file.
    /// </summary>
    public void ImportCategory(){
        NativeFilePicker.Permission permission = NativeFilePicker.PickFile( ( path ) =>
			{
				if( path != null ) {
                    string savePath = Application.persistentDataPath + "/customCategories/" + Path.GetFileName(path);
                    File.Copy(path, savePath, true);
                    // TO-DO: Check if a file would be overwritten
                    SceneManager.LoadScene("CategorySelect");
                }
			} );
    }
}