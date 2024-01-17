using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
public class LevelManager : MonoBehaviour
{
    public GameObject Level1Icon;
    public GameObject Level2Icon;
    public GameObject Level3Icon;
    public GameObject Level4Icon;
    public GameObject Level5Icon;
    public GameObject Level6Icon;
    public GameObject Level7Icon;
    public GameObject Level8Icon;
    public GameObject Level9Icon;
    public GameObject Level10Icon;
    public GameObject Level11Icon;
    public GameObject Level12Icon;
    public GameObject Level13Icon;
    public GameObject Level14Icon;
    public GameObject Level15Icon;
    public GameObject Level16Icon;
    public GameObject Level17Icon;
    public GameObject Level18Icon;

 

    private void Start()
    {
        
            // Chemin vers le fichier JSON
            string cheminFichierJSON = "Assets\\Data\\data.json";

            // Lecture du fichier JSON
            StreamReader reader = new StreamReader(cheminFichierJSON);
            string json = reader.ReadToEnd();

            // Conversion du JSON en objet
            Dictionary<string, Dictionary<string, bool>> niveaux = new Dictionary<string, Dictionary<string, bool>>();
            foreach (var ligne in json.Split('\n'))
            {
                var niveau = ligne.Split(':');
                Debug.Log(json);
                var chapitre = niveau[0];
                var niveauNom = niveau[1];
                var termine = niveau[2];

                if (!niveaux.ContainsKey(chapitre))
                {
                    niveaux[chapitre] = new Dictionary<string, bool>();
                }

                niveaux[chapitre][niveauNom] = termine == "true";
            }

            // Affichage des données
            foreach (var chapitre in niveaux.Keys)
            {
                Console.WriteLine("Chapitre : " + chapitre);
                foreach (var niveau in niveaux[chapitre].Keys)
                {
                    Console.WriteLine("Niveau : " + niveau + " - Terminé : " + niveaux[chapitre][niveau]);
                }
            }

            reader.Close();
        Debug.Log("Test");

        
    }
}
