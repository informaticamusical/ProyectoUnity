using UnityEngine;

/// <summary>
/// Ejemplo de cómo seguir el ritmo de una canción
/// </summary>
public class FollowBeat : MonoBehaviour
{
    /// <summary>
    /// Conductor del ritmo de la canción
    /// </summary>
    public InformaticaMusical.ConductorData ConductorData; 

    /// <summary>
    /// Compás que se quiere seguir. 4 = Cada compás
    /// </summary>
    public double Multiplier = 4;

    double lastBeat;    //Temporizador

    /// <summary>
    /// Inicializa atributos
    /// </summary>
    private void Start()
    {
        lastBeat = 0.0d;
        ConductorData.Init();
    }

    /// <summary>
    /// Actualiza el conductor y comprueba si tiene que realizar acción
    /// Aumenta la escala y cambia el color cuando le toca realizar acción
    /// </summary>
    private void Update()
    {
        ConductorData.Update(); 

        //Me toca realizar acción?
        if (ConductorData.SongPosition > lastBeat + ConductorData.Crotchet * Multiplier)
        {
            Debug.Log("do something");

            //Aumenta escala y cambia el color
            this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x + 1, this.gameObject.transform.localScale.y + 1, this.gameObject.transform.localScale.z + 1);
            this.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

            //Actualizar temporizador
            lastBeat += ConductorData.Crotchet * Multiplier;
        }
    }
}
