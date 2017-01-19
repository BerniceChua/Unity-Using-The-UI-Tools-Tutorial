using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringToFront : MonoBehaviour {

    public void OnEnable() {
        transform.SetAsLastSibling();
    }
}
