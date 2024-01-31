//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input/Tutorial.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Tutorial: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Tutorial()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Tutorial"",
    ""maps"": [
        {
            ""name"": ""TutoInput"",
            ""id"": ""ecbf1143-fad6-4bd2-a08b-45cb2bf25eec"",
            ""actions"": [
                {
                    ""name"": ""Phone"",
                    ""type"": ""Button"",
                    ""id"": ""1b1b11e2-09ea-4e6e-9105-b3e08f89ff31"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""aac31935-87dd-42ac-a567-60e70e9fa9e1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Phone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // TutoInput
        m_TutoInput = asset.FindActionMap("TutoInput", throwIfNotFound: true);
        m_TutoInput_Phone = m_TutoInput.FindAction("Phone", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // TutoInput
    private readonly InputActionMap m_TutoInput;
    private List<ITutoInputActions> m_TutoInputActionsCallbackInterfaces = new List<ITutoInputActions>();
    private readonly InputAction m_TutoInput_Phone;
    public struct TutoInputActions
    {
        private @Tutorial m_Wrapper;
        public TutoInputActions(@Tutorial wrapper) { m_Wrapper = wrapper; }
        public InputAction @Phone => m_Wrapper.m_TutoInput_Phone;
        public InputActionMap Get() { return m_Wrapper.m_TutoInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TutoInputActions set) { return set.Get(); }
        public void AddCallbacks(ITutoInputActions instance)
        {
            if (instance == null || m_Wrapper.m_TutoInputActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_TutoInputActionsCallbackInterfaces.Add(instance);
            @Phone.started += instance.OnPhone;
            @Phone.performed += instance.OnPhone;
            @Phone.canceled += instance.OnPhone;
        }

        private void UnregisterCallbacks(ITutoInputActions instance)
        {
            @Phone.started -= instance.OnPhone;
            @Phone.performed -= instance.OnPhone;
            @Phone.canceled -= instance.OnPhone;
        }

        public void RemoveCallbacks(ITutoInputActions instance)
        {
            if (m_Wrapper.m_TutoInputActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ITutoInputActions instance)
        {
            foreach (var item in m_Wrapper.m_TutoInputActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_TutoInputActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public TutoInputActions @TutoInput => new TutoInputActions(this);
    public interface ITutoInputActions
    {
        void OnPhone(InputAction.CallbackContext context);
    }
}
