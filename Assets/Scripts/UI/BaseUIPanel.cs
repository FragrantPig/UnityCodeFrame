using UnityEngine;

namespace Scripts.UI
{
    public class BaseUIPanel : MonoBehaviour, IViewable
    {
        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        public void LogError(string nLog)
        {
            Debug.Log($"[{this.name}]: {nLog}");
        }
    }
    
    public interface IViewable
    {
        
    }
}