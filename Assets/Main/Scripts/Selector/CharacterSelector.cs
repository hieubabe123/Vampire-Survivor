using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector instance;
    public CharacterScriptableObject characterData;
    public Sprite characterSprite;
    public AnimatorController animator;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("EXTRA" + this + "DELETED");
            Destroy(gameObject);
        }
    }

    // return characterData to PlayerStats
    public static CharacterScriptableObject GetData()
    {
        return instance.characterData;
    }

    public static Sprite GetSprite()
    {
        return instance.characterSprite;
    }

    public static AnimatorController GetAnimator()
    {
        return instance.animator;
    }


    // Take Data from Menu UI to choose character and save to characterData
    public void SelectCharacter(CharacterScriptableObject character)
    {
        characterData = character;
        characterSprite = character.Icon;
        animator = character.Animator;
    }

    public void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
}
