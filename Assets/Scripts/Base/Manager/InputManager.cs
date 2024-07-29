using System;
using Scripts.UI;

namespace Scripts.Base.Manager
{
    public class InputManager : MonoSingleton<InputManager>, IMonoSingleton
    {
        private IViewController _currentInputController;
        public override bool IsDontDestroy => true;

        public void SetInputController(IViewController nController)
        {
            _currentInputController = nController;
        }
        
        private void Update()
        {
            if (_currentInputController == null)
                return;
            _currentInputController.ClickButtonsListener();
        }
    }
}