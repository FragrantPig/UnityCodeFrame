using System.Collections;
using UnityEngine.Events;

namespace Scripts.UI
{
    public class BaseViewController<T> : IViewController
        where T : BaseUIPanel
    {
        protected T _uiInstance = null;
        
        protected virtual void AddBtnListener() { }

        public virtual IEnumerator InitializeView(UnityAction nInitialize = null)
        {
            AddBtnListener();
            nInitialize?.Invoke();
            yield break;
        }

        public virtual IEnumerator UpdateView()
        {
            yield return null;
        }

        public void SetBaseUI(IViewable nPanel)
        {
            _uiInstance = nPanel as T;
        }

        public IViewable GetBaseUI()
        {
            return _uiInstance;
        }
        
        public virtual void ClickButtonsListener()
        {
            
        }
    }

    public interface IViewController
    {
        public IEnumerator InitializeView(UnityAction nInitialize = null);

        public IEnumerator UpdateView();

        public void SetBaseUI(IViewable nPanel);

        public IViewable GetBaseUI();

        public void ClickButtonsListener();
    }
}