using System;
using UnityEngine;

namespace Game.Managers
{
    public class InputManager: MonoBehaviour
    {
        public event Action InitialClick;
        public event Action Clicked;
        public event Action ClickedUp;
        public event Action ClickHold;

        private bool _isActive = true;
        
        private const string ButtonName = "Fire1";

        private void Awake()
        {
            Clicked += OnInitialClick;
        }

        void Update()
        {
            if (!_isActive) return;
            CheckInput();
        }

        void CheckInput()
        {
            if (Input.GetButtonDown(ButtonName))
            {
                OnClicked();
            }
            if (Input.GetButton(ButtonName))
            {
                OnClickHold();
            }
            if (Input.GetButtonUp(ButtonName))
            {
                OnClickedUp();
            }
        }

        private void OnClicked()
        {
            Clicked?.Invoke();
        }

        private void OnClickedUp()
        {
            ClickedUp?.Invoke();
        }

        private void OnClickHold()
        {
            ClickHold?.Invoke();
        }

        private void OnInitialClick()
        {
            Clicked -= OnInitialClick;
            InitialClick?.Invoke();
        }

        public void SetSituation(bool situation)
        {
            _isActive = situation;
        }
    }
}