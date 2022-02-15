using DG.Tweening;
using UnityEngine;
using static FrontMan;

public class CameraMoveTrigger : MonoBehaviour
{
    [SerializeField] Transform cameraDestination;
    [SerializeField] float ScaleTo = 1;

    const float BASE_SIZE = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (cameraDestination) FM.Main.transform.DOMove(cameraDestination.position.SetZ(-20), 1);
            else FM.Main.transform.DOMove(transform.position.SetZ(-20), 1);
            DOTween.To(() => FM.Main.orthographicSize, (x) => FM.Main.orthographicSize = x, BASE_SIZE * ScaleTo, 1);
        }
    }


}
