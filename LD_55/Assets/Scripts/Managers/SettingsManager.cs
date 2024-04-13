using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

namespace lvl_0
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField]
        private RectTransform m_pointerArrow;

        [SerializeField]
        private float m_inputCooldown;

        [SerializeField]
        private List<Vector2> m_pointerPositions;

        private Duration m_inputCooldownDuration;

        private InputActions m_inputActions;

        private SettingsItem m_selectedMenuItem;

        private bool m_itemSelected = false;

        private void Awake()
        {
            m_inputCooldownDuration = new Duration(m_inputCooldown);
            m_inputActions = new InputActions();
            m_selectedMenuItem = SettingsItem.Quit;
            m_pointerArrow.anchoredPosition = m_pointerPositions[0];
        }

        private void OnEnable()
        {
            m_inputActions.SettingsMenu.Enable();
            m_inputActions.SettingsMenu.Select.performed += OnSelectPerformed;
            m_inputActions.SettingsMenu.Escape.performed += OnEscapePerformed;
            m_itemSelected = false;
        }

        private void OnDisable()
        {
            m_inputActions.SettingsMenu.Select.performed -= OnSelectPerformed;
            m_inputActions.SettingsMenu.Escape.performed -= OnEscapePerformed;
            m_inputActions.SettingsMenu.Disable();
        }

        private void Update()
        {
            if (!m_itemSelected)
            {
                m_inputCooldownDuration.Update(Time.deltaTime);
                if (m_inputCooldownDuration.Elapsed())
                {
                    var moveInput = m_inputActions.MainMenu.Move.ReadValue<Vector2>();
                    if (moveInput.y < 0 && m_selectedMenuItem < SettingsItem.Quit)
                    {
                        m_selectedMenuItem++;
                        m_pointerArrow.anchoredPosition = m_pointerPositions[(int)m_selectedMenuItem];
                        m_inputCooldownDuration.Reset();
                    }
                    else if (moveInput.y > 0 && m_selectedMenuItem > SettingsItem.Quit)
                    {
                        m_selectedMenuItem--;
                        m_pointerArrow.anchoredPosition = m_pointerPositions[(int)m_selectedMenuItem];
                        m_inputCooldownDuration.Reset();
                    }
                }
            }
        }

        private void OnSelectPerformed(CallbackContext context)
        {
            if (m_inputCooldownDuration.Elapsed())
            {
                switch (m_selectedMenuItem)
                {
                    case SettingsItem.Quit:
                        LevelAttendant.Instance.LoadGameState(GameState.Menu);
                        break;
                }
                m_itemSelected = true;
            }
        }

        private void OnEscapePerformed(CallbackContext context)
        {
            if (m_inputCooldownDuration.Elapsed())
            {
                LevelAttendant.Instance.LoadGameState(GameState.Menu);
            }
        }
    }

    public enum SettingsItem
    {
        Quit
    }
}


