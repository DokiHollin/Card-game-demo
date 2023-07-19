using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class LevelManager : MonoBehaviour
{
    public int levelId = 1001;
    public string levelMapLocation = "Image/UI/IceMain";
    public Sprite image;
    // Start is called before the first frame update
    void Awake()
    {
        //��ǰ���Sprite���԰�texture2dֱ�Ӽ��س�sprite�ȼ��س�
        //texture2d��ת����sprite����null
        image = Resources.Load<Sprite>(levelMapLocation);
        if(image == null)
        {
            print("image is null");
        }
        //var sprite = Sprite.Create(image, new Rect(0.0f, 0.0f, image.width, image.height), new Vector2(0.55f, 0.55f), 100.0f);
        //var sprite = Sprite.Create(image, new Rect(0.0f, 0.0f, image.width, image.height), new Vector2(0.5f, 0.6f));
        transform.GetComponent<SpriteRenderer>().sprite = image;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
