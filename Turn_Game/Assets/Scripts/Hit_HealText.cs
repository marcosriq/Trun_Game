using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_HealText : MonoBehaviour {

    public TextMesh border;

    private void Update()
    {
        if (transform.GetComponent<TextMesh>() != null)
            border.text = border.transform.parent.GetComponent<TextMesh>().text;
    }
    public void DestroyIt()
    {
        Destroy(gameObject);
    }
}
