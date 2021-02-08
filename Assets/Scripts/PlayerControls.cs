// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerActionControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActionControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""WASD"",
            ""id"": ""2f58bf32-ecf9-4e6d-aafd-0fc3f244d7d4"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d9d62df1-6ca1-49dc-aeb7-78077e96b45f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cf60bc5b-ff60-4258-ab3f-10c276497c44"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Sideways"",
                    ""id"": ""7fd8d101-a3cc-451e-b66f-647cbaf8d653"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""37b885b5-8c20-4c33-bbaa-1247922e5dfc"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""657d36c3-5cec-4a3c-bbde-608ac1bf5037"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4fe3631a-c85c-485f-873d-81c891e967b0"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // WASD
        m_WASD = asset.FindActionMap("WASD", throwIfNotFound: true);
        m_WASD_Move = m_WASD.FindAction("Move", throwIfNotFound: true);
        m_WASD_Jump = m_WASD.FindAction("Jump", throwIfNotFound: true);
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

    // WASD
    private readonly InputActionMap m_WASD;
    private IWASDActions m_WASDActionsCallbackInterface;
    private readonly InputAction m_WASD_Move;
    private readonly InputAction m_WASD_Jump;
    public struct WASDActions
    {
        private @PlayerActionControls m_Wrapper;
        public WASDActions(@PlayerActionControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_WASD_Move;
        public InputAction @Jump => m_Wrapper.m_WASD_Jump;
        public InputActionMap Get() { return m_Wrapper.m_WASD; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WASDActions set) { return set.Get(); }
        public void SetCallbacks(IWASDActions instance)
        {
            if (m_Wrapper.m_WASDActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_WASDActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_WASDActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_WASDActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_WASDActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_WASDActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_WASDActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_WASDActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public WASDActions @WASD => new WASDActions(this);
    public interface IWASDActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
}
