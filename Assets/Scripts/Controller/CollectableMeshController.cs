using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMeshController : MonoBehaviour
{
    

    public void ChangeCollectableMaterial(ColorTypes _colorType)
    {

        //var colorHandler = Addressables.LoadAssetAsync<Material>($"Collectable/Color_{_colorType}");
        //meshRenderer.material = (colorHandler.WaitForCompletion() != null) ? colorHandler.Result : null;
    }
}
