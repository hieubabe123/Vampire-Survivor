using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector instance;
    public CharacterScriptableObject characterData;
    

    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Debug.LogWarning("EXTRA" + this + "DELETED");
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // return characterData to PlayerStats
    public static CharacterScriptableObject GetData(){
        return instance.characterData;
    }

    // Take Data from Menu UI to choose character and save to characterData
    public void SelectCharacter(CharacterScriptableObject character){
        characterData = character;
    }

    public void DestroySingleton(){
        instance = null;
        Destroy(gameObject);
    }
}
