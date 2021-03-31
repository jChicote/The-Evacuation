// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Mobile"",
            ""id"": ""93226f20-54cf-440d-9cf0-a426547a0681"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""773e632a-127c-45af-b9fb-a4178f197525"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cdad3fa0-6264-48f8-a5d8-5caa6a1612d0"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Desktop"",
            ""id"": ""7eb654c3-5d5e-4def-9c7f-b6040c376a0e"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f5a03672-9395-413e-aa12-e41bf70abe68"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""1ae8a80c-54f6-4024-8971-268e971cd461"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""2db943a6-e14d-434d-8788-ed6c1fd080ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Detach"",
                    ""type"": ""Button"",
                    ""id"": ""9c866548-0c6b-4941-ad06-9b780c342c85"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""60652993-9f9e-4543-8ffe-50e7b5d20f8d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""d228e72d-e603-43ad-a600-50c898518590"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9c0b2cc6-872d-41f8-97dd-3198b0bd701d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5a7c2c45-67df-40c4-a866-a8f4739d16ce"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""675e0eb5-aa8b-4576-bc72-757c13e93d2d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""86383fed-29a0-4e41-9a70-b1ddf0a6d907"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1c10dcd8-e265-4607-adcc-0ce662966932"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b76fffce-f074-4dcb-a1b3-1e5fe8fba3b9"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d93506a-bc0d-4f26-a489-f72b7281d767"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Detach"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""57a4c43d-285a-428a-9e1c-8a3f6519aa6b"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Mobile
        m_Mobile = asset.FindActionMap("Mobile", throwIfNotFound: true);
        m_Mobile_Movement = m_Mobile.FindAction("Movement", throwIfNotFound: true);
        // Desktop
        m_Desktop = asset.FindActionMap("Desktop", throwIfNotFound: true);
        m_Desktop_Movement = m_Desktop.FindAction("Movement", throwIfNotFound: true);
        m_Desktop_Pause = m_Desktop.FindAction("Pause", throwIfNotFound: true);
        m_Desktop_Attack = m_Desktop.FindAction("Attack", throwIfNotFound: true);
        m_Desktop_Detach = m_Desktop.FindAction("Detach", throwIfNotFound: true);
        m_Desktop_Aim = m_Desktop.FindAction("Aim", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Mobile
    private readonly InputActionMap m_Mobile;
    private IMobileActions m_MobileActionsCallbackInterface;
    private readonly InputAction m_Mobile_Movement;
    public struct MobileActions
    {
        private @PlayerInput m_Wrapper;
        public MobileActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Mobile_Movement;
        public InputActionMap Get() { return m_Wrapper.m_Mobile; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MobileActions set) { return set.Get(); }
        public void SetCallbacks(IMobileActions instance)
        {
            if (m_Wrapper.m_MobileActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_MobileActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_MobileActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_MobileActionsCallbackInterface.OnMovement;
            }
            m_Wrapper.m_MobileActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
            }
        }
    }
    public MobileActions @Mobile => new MobileActions(this);

    // Desktop
    private readonly InputActionMap m_Desktop;
    private IDesktopActions m_DesktopActionsCallbackInterface;
    private readonly InputAction m_Desktop_Movement;
    private readonly InputAction m_Desktop_Pause;
    private readonly InputAction m_Desktop_Attack;
    private readonly InputAction m_Desktop_Detach;
    private readonly InputAction m_Desktop_Aim;
    public struct DesktopActions
    {
        private @PlayerInput m_Wrapper;
        public DesktopActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Desktop_Movement;
        public InputAction @Pause => m_Wrapper.m_Desktop_Pause;
        public InputAction @Attack => m_Wrapper.m_Desktop_Attack;
        public InputAction @Detach => m_Wrapper.m_Desktop_Detach;
        public InputAction @Aim => m_Wrapper.m_Desktop_Aim;
        public InputActionMap Get() { return m_Wrapper.m_Desktop; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DesktopActions set) { return set.Get(); }
        public void SetCallbacks(IDesktopActions instance)
        {
            if (m_Wrapper.m_DesktopActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_DesktopActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_DesktopActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_DesktopActionsCallbackInterface.OnMovement;
                @Pause.started -= m_Wrapper.m_DesktopActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_DesktopActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_DesktopActionsCallbackInterface.OnPause;
                @Attack.started -= m_Wrapper.m_DesktopActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_DesktopActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_DesktopActionsCallbackInterface.OnAttack;
                @Detach.started -= m_Wrapper.m_DesktopActionsCallbackInterface.OnDetach;
                @Detach.performed -= m_Wrapper.m_DesktopActionsCallbackInterface.OnDetach;
                @Detach.canceled -= m_Wrapper.m_DesktopActionsCallbackInterface.OnDetach;
                @Aim.started -= m_Wrapper.m_DesktopActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_DesktopActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_DesktopActionsCallbackInterface.OnAim;
            }
            m_Wrapper.m_DesktopActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Detach.started += instance.OnDetach;
                @Detach.performed += instance.OnDetach;
                @Detach.canceled += instance.OnDetach;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
            }
        }
    }
    public DesktopActions @Desktop => new DesktopActions(this);
    public interface IMobileActions
    {
        void OnMovement(InputAction.CallbackContext context);
    }
    public interface IDesktopActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnDetach(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
    }
}
