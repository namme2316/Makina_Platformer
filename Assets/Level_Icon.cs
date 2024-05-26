using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Icon : MonoBehaviour
{
    private Button btn;
    private Image image;
	private RectTransform rectTransform;
	public float normalScale = 1f; 
	public float enlargedScale = 1.5f;
	public List<Sprite> spriteChoices;
   
    
    void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        btn = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSprite();
    }

    public void ChangeSprite()
    {
        if(btn.IsInteractable() && image.sprite != spriteChoices[0]) 
        {
            image.sprite = spriteChoices[0];
            rectTransform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
		}
        else if(!btn.IsInteractable() && image.sprite != spriteChoices[1])
        {
            image.sprite = spriteChoices[1];
			image.rectTransform.localScale = new Vector3(enlargedScale, enlargedScale, 1f);
			rectTransform.localScale = new Vector3(0.6f, 0.5f, 0.35f);
		}
    }
}
