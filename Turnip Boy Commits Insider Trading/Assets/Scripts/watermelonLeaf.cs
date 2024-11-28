using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watermelonLeaf : MonoBehaviour
{

    private bool holeFilled = false;
    private bool stopEverything;
    public watermelonHole correspondingHole;
    public GameObject watermelonPrefab;
    public GameObject watermelon;

    // Start is called before the first frame update
    void Start()
    {
        watermelon = Instantiate(watermelonPrefab, gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopEverything)
        {
            if (correspondingHole != null)
            {
                holeFilled = correspondingHole.getFilled();
            }

            if (watermelon != null && holeFilled)
            {
                Destroy(watermelon);
                stopEverything = true;
            }
            else if (watermelon == null && !holeFilled)
            {
                watermelon = Instantiate(watermelonPrefab, gameObject.transform);
            }
        }
    }
}
