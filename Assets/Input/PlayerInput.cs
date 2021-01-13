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
                    ""type"": ""Value"",
                    ""id"": ""773e632a-127c-45af-b9fb-a4178f197525"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cdad3fa0-6264-48f8-a5d8-5caa6a1612d0"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
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
    public interface IMobileActions
    {
        void OnMovement(InputAction.CallbackContext context);
    }
}
