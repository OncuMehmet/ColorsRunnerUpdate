using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DroneAreaMeshController : MonoBehaviour
{
    #region Self Variables
    #region Serialized Variables

    [SerializeField] private MeshRenderer meshRenderer;
    
    #endregion
    #endregion

    public void ChangeAreaColor(ColorTypes _colorType)
    {
       var colorHandler = Addressables.LoadAssetAsync<Material>($"Assets/Art/Materials/CoreColorMaterials/{ _colorType}");
       meshRenderer.material = (colorHandler.WaitForCompletion() != null) ? colorHandler.Result : null;
    }
}
