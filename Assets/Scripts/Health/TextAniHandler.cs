using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAniHandler : MonoBehaviour
{
    private TextMeshPro m_TextMeshPro;
    void Start()
    {
        //m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        m_TextMeshPro = GetComponent<TextMeshPro>();

        m_TextMeshPro.SetText("-" + Bullet.BulletDamage.ToString());

        Destroy(gameObject, 4);
    }
}
