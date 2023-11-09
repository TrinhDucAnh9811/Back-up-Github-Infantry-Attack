using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DotWeenTest : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        //DOMove:
        /*player.transform.DOMove(new Vector3(15, 1, 15), 3.0f);*/
        player.transform.DOJump(new Vector3(40, 1, 15), 1, 10, 4, false);
        
    }

}
