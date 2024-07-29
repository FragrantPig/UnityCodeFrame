using UnityEngine;

public class UIBase : MonoBehaviour
{
    private void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        AddButtonsListener();
    }

    protected virtual void AddButtonsListener()
    {

    }
}