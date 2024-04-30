using UnityEngine;
using UnityEngine.UI;

public class HealthHearts : MonoBehaviour
{
    public Sprite fullHeart, halfHeart, emptyHeart;
    Image heartImage;

    private void Awake(){
        heartImage = GetComponent<Image>();
    }

    public void SetHeartImg(HeartStatus status){
        if (status == HeartStatus.empty){
            heartImage.sprite = emptyHeart;
        }
        else if (status == HeartStatus.half){
            heartImage.sprite = halfHeart;
        }
        else if (status == HeartStatus.full){
            heartImage.sprite = fullHeart;
        }
    }
}

public enum HeartStatus{
    empty = 0,
    half = 1,
    full = 2
}
