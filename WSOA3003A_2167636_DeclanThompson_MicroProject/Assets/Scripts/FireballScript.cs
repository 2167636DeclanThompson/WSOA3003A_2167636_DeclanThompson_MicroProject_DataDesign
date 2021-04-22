using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    void Update()
    {
        StartCoroutine(Fireball());
    }

    private IEnumerator Fireball()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
}
