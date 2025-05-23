using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Dialogo
{
    public string nombrePersonaje;
    public string texto;
    public string imagenPersonajePath;
    [System.NonSerialized] public Sprite imagenPersonaje;
    public bool personajeHablando;
}

[System.Serializable]
public class DialogoListWrapper 
{
    public List<Dialogo> dialogos;
}

public class DialogueManager : MonoBehaviour 
{
    private EventSystem eventSystem;

    public TMPro.TextMeshProUGUI textoDialogo;
    public TMPro.TextMeshProUGUI nombrePersonaje;
    public Image[] imagenesPersonajes;
    public float velocidadTexto = 0.05f;

    public TextAsset dialogosJSON;
    private List<Dialogo> dialogos = new List<Dialogo>(); 
    private int dialogoActual;
    private bool escribiendo;
    private List<MonoBehaviour> scriptsPausados = new List<MonoBehaviour>();

    public GameObject dialogueCanvas; 
    public GameObject shopCanvas; 
    public static bool DialogueIsActive { get; private set; }

    void Start()
    {
        eventSystem = EventSystem.current;
        dialogueCanvas.SetActive(true); 
        shopCanvas.SetActive(false); 
        DialogueIsActive = true;
        
        PausarJuego();

        textoDialogo.text = "";

        if (dialogosJSON != null)
        {
            CargarDialogosDesdeJSON(dialogosJSON);
        }
        else
        {
            Debug.LogError("No se asignó un archivo JSON de diálogos.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SiguienteDialogo();
        }
    }

    void PausarJuego()
    {

        if (eventSystem != null)
        {
            eventSystem.enabled = false;
        }
            

        scriptsPausados.Clear();
        
        MonoBehaviour[] allScripts = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        foreach (MonoBehaviour script in allScripts)
        {

            if (script == this || 
                script is Graphic || 
                script is TMPro.TextMeshProUGUI || 
                script is Image ||  
                script is EventSystem)
                continue;

                
            if (script.enabled)
            {
                script.enabled = false;
                scriptsPausados.Add(script);
            }
        }
        
        AudioListener.pause = true;
    }

    void ReanudarJuego()
    {

        if (eventSystem != null)
            eventSystem.enabled = true;

        foreach (MonoBehaviour script in scriptsPausados)
        {
            if (script != null)
                script.enabled = true;
        }
        scriptsPausados.Clear();
        
        AudioListener.pause = false;
    }

    public void CargarDialogosDesdeJSON(TextAsset jsonFile)
    {
        if (jsonFile == null)
        {
            Debug.LogError("¡El JSON de diálogos es nulo!");
            return;
        }

        dialogos.Clear();
        DialogoListWrapper wrapper = JsonUtility.FromJson<DialogoListWrapper>(jsonFile.text);
    
        foreach (Dialogo dialogo in wrapper.dialogos)
        {
            dialogo.imagenPersonaje = Resources.Load<Sprite>(dialogo.imagenPersonajePath);
            if (dialogo.imagenPersonaje == null)
            {
                Debug.LogWarning($"Sprite no encontrado: {dialogo.imagenPersonajePath}");
            }
            dialogos.Add(dialogo);
        }

        if (dialogos.Count > 0)
        {
            dialogoActual = 0;
            MostrarDialogo(0);
        }
    }



    void MostrarDialogo(int index)
    {
        if (index >= dialogos.Count) return;

        dialogoActual = index;
        Dialogo dialogoActualObj = dialogos[index];

        Canvas.ForceUpdateCanvases();
        textoDialogo.text = "";
        nombrePersonaje.text = dialogoActualObj.nombrePersonaje;
        StartCoroutine(EscribirTexto(dialogoActualObj.texto));

        for (int i = 0; i < imagenesPersonajes.Length; i++)
        {
            Dialogo dialogoPersonaje = dialogos.Find(d => 
                d.imagenPersonajePath.Contains(imagenesPersonajes[i].name) 
                || d.nombrePersonaje.Contains(imagenesPersonajes[i].name)); 

            if (dialogoPersonaje != null && dialogoPersonaje.imagenPersonaje != null)
            {
                imagenesPersonajes[i].sprite = dialogoPersonaje.imagenPersonaje;
                
                bool estaHablando = (dialogoPersonaje == dialogoActualObj);
                
                imagenesPersonajes[i].color = estaHablando ? 
                    Color.white : 
                    new Color(1, 1, 1, 0.5f);
                
                imagenesPersonajes[i].transform.localScale = estaHablando ? 
                    Vector3.one * 1.1f : 
                    Vector3.one;
            }
        }
    }

    IEnumerator EscribirTexto(string texto)
    {
        escribiendo = true;

        foreach (char letra in texto.ToCharArray())
        {
            textoDialogo.text += letra;
            yield return new WaitForSeconds(velocidadTexto);
        }

        escribiendo = false;
    }

    public void SiguienteDialogo()
    {
        if (escribiendo)
        {
            StopAllCoroutines();
            textoDialogo.text = dialogos[dialogoActual].texto;
            escribiendo = false;
            return;
        }

        dialogoActual++;
        if (dialogoActual < dialogos.Count)
        {
            MostrarDialogo(dialogoActual);
        }
        else
        {
            TerminarDialogo();
        }
    }

    void TerminarDialogo()
    {
        DialogueIsActive = false;
        ReanudarJuego();
        dialogueCanvas.SetActive(false);
        shopCanvas.SetActive(true); 

        EventSystem.current.enabled = true;
        EventSystem.current.SetSelectedGameObject(null);
        
        Debug.Log("Dialogo terminado");
    }
}