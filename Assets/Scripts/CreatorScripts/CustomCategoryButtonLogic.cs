using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CustomCategoryButtonLogic : MonoBehaviour
{
    public SoundEffectPlayer failSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        switch (CustomCategoryLoader.status)
        {
            case CustomCategoryLoader.Status.Delete:
                DeleteFile();
                break;
            case CustomCategoryLoader.Status.Export:
                ExportFiles();
                break;
            case CustomCategoryLoader.Status.Edit:
            default:
                GetComponent<SoundEffectPlayer>().PlayClip();
                GetComponent<CategoryButton>().SetCategoryAsEdit();
                GetComponent<TriggerMenuVariant>().SetCurrentEditMode();
                GetComponent<SceneLoader>().LoadScene("CustomCreator");
                break;
        }
    }

    public void ExportFiles()
    {
        string origFileName = GetComponent<CategoryButton>().GetFileName();
        string filePath = Application.persistentDataPath + "/customCategories/" + origFileName + ".json";
    #if UNITY_STANDALONE_WIN || UNITY_EDITOR
        ShowCustomFolder(filePath);
    #endif
    #if UNITY_ANDROID
            //NativeFilePicker.Permission permission = NativeFilePicker.ExportFile( filePath, ( success ) => Debug.Log( "File exported: " + success ) );
            new NativeShare().AddFile(filePath).SetSubject("Charadas - Categoría personalizada").SetText("Puedes agregar la categoría a Charadas en la sección de Crear categorías, pulsando el botón de Importar categoría y seleccionando el archivo.").Share();
    #endif
    }

    // Used to delete a category's json file when it is removed.
    public void DeleteFile()
    {
        failSound.PlayClip();
        string origFileName = GetComponent<CategoryButton>().GetFileName();
        string filePath = Application.persistentDataPath + "/customCategories/" + origFileName + ".json";
        GetComponent<CategoryButton>().RemoveCategory();
        File.Delete(filePath);
        GetComponent<SceneLoader>().LoadScene("CategorySelect");
    }

    public void ShowCustomFolder(string filePath)
    {
        filePath = filePath.Replace(@"/", @"\");   // explorer doesn't like front slashes
        System.Diagnostics.Process.Start("explorer.exe", "/select," + filePath);
    }
}