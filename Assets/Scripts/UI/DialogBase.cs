using Scripts.UI.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class DialogBase : MonoBehaviour, IViewable
    {
        [SerializeField] protected Button _closeArea;

        private void Start()
        {
            AddListener();
        }

        protected virtual void AddListener()
        {
            _closeArea.onClick.AddListener(CloseDialog);
        }

        protected void CloseDialog()
        {
            UIManager.Instance.PullDialog();
        }
    }
}