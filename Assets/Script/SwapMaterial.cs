using UnityEngine;
using System.Collections;

public class SwapMaterial : MonoBehaviour
{
    public void Swap(Material material)
    {
        this.GetComponent<MeshRenderer>().material = material;
    }
}
