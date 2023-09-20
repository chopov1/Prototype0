using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmInput : MonoBehaviour
{
    [SerializeField]
    GameObject imageObj;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = imageObj.GetComponent<Image>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
        changeSquareColor();
        /*else if (!SongPlayer.Instance.IsOnBeat())
        {
            image.color = Color.white;
        }*/
    }

    void changeSquareColor()
    {
        if (SongPlayer.Instance.IsOnBeat())
        {
            image.color = Color.green;
        }
        if (!SongPlayer.Instance.IsOnBeat())
        {
            image.color = Color.red;
        }
    }
}
