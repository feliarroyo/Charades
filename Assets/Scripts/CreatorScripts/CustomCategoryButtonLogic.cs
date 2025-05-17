using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCategoryButtonLogic : MonoBehaviour
{
    
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
        if (CustomCategoryLoader.isExporting)
        {
            ExportFiles();
        }
        else {
            GetComponent<SoundEffectPlayer>().PlayClip();
            GetComponent<CategoryButton>().SetCategoryAsEdit();
            GetComponent<TriggerMenuVariant>().SetCurrentEditMode();
            GetComponent<SceneLoader>().LoadScene("CustomCreator");
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

    public void ShowCustomFolder(string filePath){
        filePath = filePath.Replace(@"/", @"\");   // explorer doesn't like front slashes
        System.Diagnostics.Process.Start("explorer.exe", "/select,"+filePath);
    }
}