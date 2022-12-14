//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/InputSystem/InputThree.inputactions
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

public partial class @InputThree : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputThree()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputThree"",
    ""maps"": [
        {
            ""name"": ""Default"",
            ""id"": ""c3af1809-ce73-4ec4-afb4-9ed36182955f"",
            ""actions"": [
                {
                    ""name"": ""InputLeft"",
                    ""type"": ""Button"",
                    ""id"": ""56337991-b954-4d30-a492-087d24d1157d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""InputRight"",
                    ""type"": ""Button"",
                    ""id"": ""a638b41d-316c-44e4-b769-416a54427ae0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""InputEnter"",
                    ""type"": ""Button"",
                    ""id"": ""dd3ed3af-be3b-4f0b-9b3f-85474ef43d04"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1ff29288-76f4-4bac-a3e6-8f2707817111"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""InputLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3731cc88-8c02-4620-968d-f1ce3dc3d30d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""InputRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""405231c6-0ecc-4525-a37e-f4ab676998f2"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""InputEnter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Default"",
            ""bindingGroup"": ""Default"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Default
        m_Default = asset.FindActionMap("Default", throwIfNotFound: true);
        m_Default_InputLeft = m_Default.FindAction("InputLeft", throwIfNotFound: true);
        m_Default_InputRight = m_Default.FindAction("InputRight", throwIfNotFound: true);
        m_Default_InputEnter = m_Default.FindAction("InputEnter", throwIfNotFound: true);
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

    // Default
    private readonly InputActionMap m_Default;
    private IDefaultActions m_DefaultActionsCallbackInterface;
    private readonly InputAction m_Default_InputLeft;
    private readonly InputAction m_Default_InputRight;
    private readonly InputAction m_Default_InputEnter;
    public struct DefaultActions
    {
        private @InputThree m_Wrapper;
        public DefaultActions(@InputThree wrapper) { m_Wrapper = wrapper; }
        public InputAction @InputLeft => m_Wrapper.m_Default_InputLeft;
        public InputAction @InputRight => m_Wrapper.m_Default_InputRight;
        public InputAction @InputEnter => m_Wrapper.m_Default_InputEnter;
        public InputActionMap Get() { return m_Wrapper.m_Default; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefaultActions set) { return set.Get(); }
        public void SetCallbacks(IDefaultActions instance)
        {
            if (m_Wrapper.m_DefaultActionsCallbackInterface != null)
            {
                @InputLeft.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnInputLeft;
                @InputLeft.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnInputLeft;
                @InputLeft.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnInputLeft;
                @InputRight.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnInputRight;
                @InputRight.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnInputRight;
                @InputRight.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnInputRight;
                @InputEnter.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnInputEnter;
                @InputEnter.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnInputEnter;
                @InputEnter.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnInputEnter;
            }
            m_Wrapper.m_DefaultActionsCallbackInterface = instance;
            if (instance != null)
            {
                @InputLeft.started += instance.OnInputLeft;
                @InputLeft.performed += instance.OnInputLeft;
                @InputLeft.canceled += instance.OnInputLeft;
                @InputRight.started += instance.OnInputRight;
                @InputRight.performed += instance.OnInputRight;
                @InputRight.canceled += instance.OnInputRight;
                @InputEnter.started += instance.OnInputEnter;
                @InputEnter.performed += instance.OnInputEnter;
                @InputEnter.canceled += instance.OnInputEnter;
            }
        }
    }
    public DefaultActions @Default => new DefaultActions(this);
    private int m_DefaultSchemeIndex = -1;
    public InputControlScheme DefaultScheme
    {
        get
        {
            if (m_DefaultSchemeIndex == -1) m_DefaultSchemeIndex = asset.FindControlSchemeIndex("Default");
            return asset.controlSchemes[m_DefaultSchemeIndex];
        }
    }
    public interface IDefaultActions
    {
        void OnInputLeft(InputAction.CallbackContext context);
        void OnInputRight(InputAction.CallbackContext context);
        void OnInputEnter(InputAction.CallbackContext context);
    }
}
