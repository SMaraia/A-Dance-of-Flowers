using UnityEngine;
using System.Collections;

public class CirclingHandler : MonoBehaviour {

    public bool isCircled = false;

    void Update()
    {
        if (isCircled)
        {
            onCircle();
            isCircled = false;
        }
    }

    private void onCircle()
    {
        this.GetComponent<Flower>().Capture();
    }
}
