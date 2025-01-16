using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class CustomCategoryLoader : MonoBehaviour
{
    public GameObject categoryButtonPrefab;
    public GameObject customParent;
    // Start is called before the first frame update
    void Start()
    {
        string savePath = Application.persistentDataPath + "/customCategories";
        CategoryCreator.CreateCustomDirectory(savePath);
        foreach (string file in Directory.GetFiles(savePath,"*.json")){
            StreamReader reader = new StreamReader(file);
            GameObject newGameObject = Instantiate(categoryButtonPrefab, customParent.transform);
            newGameObject.GetComponentInChildren<CategoryButton>().jsonCategory = new TextAsset(reader.ReadToEnd());
            newGameObject.GetComponentInChildren<CategoryButton>().SetSingleSelect();
        }
    }

    public void ImportCategory(){
        NativeFilePicker.Permission permission = NativeFilePicker.PickFile( ( path ) =>
			{
				if( path == null )
					Debug.Log( "Operation cancelled" );
				else {
					Debug.Log( "Picked file: " + path );
                    string savePath = Application.persistentDataPath + "/customCategories/" + Path.GetFileName(path);
                    Debug.Log( "Destination file: " + savePath);
                    File.Copy(path, savePath, true);
                    SceneManager.LoadScene("CategorySelect");
                }
			} );
    }
}