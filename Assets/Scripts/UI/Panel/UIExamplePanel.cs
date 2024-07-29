using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Base;
using Scripts.UI.Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class UIExamplePanel : BaseUIPanel
    {

    }

    public class UIExampleController : BaseViewController<UIExamplePanel>
    {
        public override IEnumerator InitializeView(UnityAction nInitialize = null)
        {
            return base.InitializeView(nInitialize);
        }

        protected override void AddBtnListener()
        {
            base.AddBtnListener();
        }
    }
}