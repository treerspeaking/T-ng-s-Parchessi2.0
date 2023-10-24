using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectorUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject optionPrefab;
    public Transform prevCharacter;
    public Transform selectedCharacter;
    private void Start()
    {
        foreach (Character c in CharacterManger.instance.characters)
        {
            GameObject option = Instantiate(optionPrefab, transform);
            Button button = option.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                CharacterManger.instance.SetCharacter(c);
                if (selectedCharacter != null)
                {
                    prevCharacter = selectedCharacter;
                }

                selectedCharacter = option.transform;
            });
            Text text = option.GetComponentInChildren<Text>();
            text.text = c.name;
            Image image = option.GetComponentInChildren<Image>();
            image.sprite = c.spriteName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedCharacter != null)
        {
            selectedCharacter.localScale = Vector3.Lerp(selectedCharacter.localScale,new Vector3(1.2f,1.2f,1.2f),Time.deltaTime*10);
        }
        if (prevCharacter != null)
        {
            prevCharacter.localScale = Vector3.Lerp(prevCharacter.localScale,new Vector3(1f,1f,1f),Time.deltaTime*10);
        }
    }
}
