using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHitEffect : MonoBehaviour
{
    public AnimationCurve curve;

    private Material[] materials;

    private static int brightnessId = Shader.PropertyToID("_Brightness");

    private Tween tween;

    private void OnEnable() //Có thể sửa lại bằng onEnable()???
    {
      /*  //Reset Color when Enable
        foreach (Material mat in materials)
        {
            mat.SetFloat(brightnessId, 0f);
        }*/

        EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.OnGetHit += ChangeColor; //Đăng kí nhận event
            enemyHealth.OnGetHit += StopMoving; //Đăng kí nhận event
            
        }

        // Lấy danh sách các Material từ các thành phần Renderer
        List<Material> materialList = new List<Material>();
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            materialList.AddRange(renderer.materials);
        }

        // Chuyển danh sách Material thành mảng
        materials = materialList.ToArray();

    }

    public void ChangeColor()
    {
            if (tween != null && tween.IsActive())
            {
                tween.Kill();
                tween = null;
            }

            foreach (Material mat in materials)
            {
                mat.SetFloat(brightnessId, 0f);
                tween = mat.DOFloat(0.55f, brightnessId, 0.2f).SetEase(curve);
            }
            // Đợi 0.2 giây và sau đó thiết lập màu về giá trị mặc định
       /*     StartCoroutine(ResetMaterialColor());   */
    }


    void StopMoving()
    {
        //Code NavMesh. velocity = 0;
    }


    private IEnumerator ResetMaterialColor()
    {
            yield return new WaitForSeconds(0.2f);

            foreach (Material mat in materials)
            {
                mat.SetFloat(brightnessId, 0f);
            }
    }

/*    void ResetColor()
    {
        foreach (Material mat in materials)
        {
            mat.SetFloat(brightnessId, 0f);
        }
    } */   

}
