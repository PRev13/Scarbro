using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoxCamera : MonoBehaviour
{
    //Vector3 offsetCamera = new Vector3(0f, 0f, -8.41f);
    public Transform boxTransform;

    public enum FACES {FACE1, FACE2, FACE3, FACE4, FACE5, FACE6};
    Tween moveTween;

    public void MoveCamera(FACES _toFace)
    {
        Vector3 destinyPos = boxTransform.position;

        switch (_toFace)
        {
            case FACES.FACE1:
                destinyPos += new Vector3(0f, 0.34f, -8.41f);
                break;
            case FACES.FACE2:
                destinyPos += new Vector3(8.41f, 0.34f, 0f);
                break;
            case FACES.FACE3:
                destinyPos += new Vector3(0f, 0f, 8.41f);
                break;
            case FACES.FACE4:
                destinyPos += new Vector3(-8.41f, 0f, 0f);
                break;
            case FACES.FACE5:
                destinyPos += new Vector3(0f, -8.41f, 0f);
                break;
            case FACES.FACE6:
                destinyPos += new Vector3(0f, 8.41f, 0f);
                break;
        }

        //We get a middle point to not move close to the rectangle and the move have more angle
        //Push the point the same distance form any other point
        Vector3 midPoint = Vector3.Lerp(transform.position, destinyPos, 0.5f);
        midPoint = midPoint - boxTransform.position;
        midPoint.Normalize();
        midPoint = boxTransform.position + midPoint * 8.41f;
        //Call Dotween
        Vector3[] path = { midPoint, destinyPos };
        moveTween = transform.DOPath(path, 1f, PathType.CatmullRom);
    }

    private void LateUpdate()
    {
        if(moveTween != null && moveTween.IsActive()) 
            transform.LookAt(boxTransform.position);//Make the camera always look at center of the 3Dbox
    }
}
