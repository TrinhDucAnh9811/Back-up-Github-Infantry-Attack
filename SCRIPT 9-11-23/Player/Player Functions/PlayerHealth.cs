using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>, IDamageable
{
    public float maxHp = 100.0f;
    public float currentHp;

    public Image hurtImage = null;
    private float hurtTime = 1.0f;

    float fadingIndex;
    bool isHealing;

    Color originalColor;

    void Start()
    {
        currentHp = maxHp;

        originalColor = hurtImage.color;
    }

    void UpdateHealth()
    {
        //Silder here
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") /*|| collision.gameObject.CompareTag("EnemyWeapon")*/)
        {
            TakeDamage(5);
            //SAU SUWAR LAIj
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            //GameOver code
        }

        else if (currentHp > 50)
        {
            
        }
        else if (currentHp <= 50)
        {
            // Đặt giá trị alpha về 1 để hiển thị hurtImage
            Color currentColor = originalColor;
            currentColor.a = 1f;
            hurtImage.color = currentColor;
            hurtImage.enabled = true; // Hiển thị hurtImage

            StartCoroutine(HurtFlash());
            UpdateHealth();
        }
    }

    IEnumerator HurtFlash()
    {
        isHealing = true;

        hurtImage.enabled = true;

        yield return new WaitForSeconds(hurtTime);

        fadingIndex = 0;
        hurtImage.enabled = false;

        isHealing = false;
    }

    private void Update()
    {
        if (isHealing)
        {
            fadingIndex = 0;
            fadingIndex += Time.deltaTime;
            // Lấy màu hiện tại của hurtImage
            Color currentColor = hurtImage.color;
            // Đặt giá trị alpha của màu
            currentColor.a -= fadingIndex;
            // Gán lại màu đã cập nhật cho hurtImage
            hurtImage.color = currentColor;

            currentColor = originalColor;
        }

    }

}

