using UnityEngine.Events;



public class UISignals : MonoSingleton<UISignals>
{
    public UnityAction<UIPanelTypes> onOpenPanel = delegate { };
    public UnityAction<UIPanelTypes> onClosePanel = delegate { };
}
