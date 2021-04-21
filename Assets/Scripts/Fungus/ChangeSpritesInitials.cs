using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpritesInitials : MonoBehaviour
{
    public List<Sprite> imagesToChange = new List<Sprite>();
    public Image imageToChange;

    void changeFirstImage() {
        imageToChange.sprite = imagesToChange[1];
    }

    void changeSecondImageInit()
    {
        imageToChange.sprite = imagesToChange[2];
    }

    void changeThirdImage()
    {
        imageToChange.sprite = imagesToChange[3];
    }

    void changeForthImage()
    {
        imageToChange.sprite = imagesToChange[4];
    }

    void changeFifthImage()
    {
        imageToChange.sprite = imagesToChange[5];
    }





}
