using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Game.UI.Base.Overlay
{
    public class BaseOverlay : MonoBehaviour, IOverlay
    {
        private static BaseOverlay _instance;
        public virtual bool IsDontDestroy => false;

        public static BaseOverlay Instance
        {
            get
            {
                return _instance;
            }
        }

        public virtual void OnInitialize()
        {
            AddBtnListener();
        }

        protected virtual void AddBtnListener()
        {

        }
    }

    public interface IOverlay
    {
        bool IsDontDestroy { get; }
        void OnInitialize();
    }
}