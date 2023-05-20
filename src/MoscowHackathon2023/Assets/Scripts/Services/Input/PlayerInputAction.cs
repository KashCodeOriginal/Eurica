//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/Services/Input/PlayerInputAction.inputactions
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

public partial class @PlayerInputAction : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAction"",
    ""maps"": [
        {
            ""name"": ""PlayerInput"",
            ""id"": ""912d2bfc-c912-4462-ae94-40ed4728647d"",
            ""actions"": [
                {
                    ""name"": ""PlayerInput"",
                    ""type"": ""Value"",
                    ""id"": ""02e0be30-a042-447e-b4c7-826e57556db3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6e454d84-3cc8-4d5d-9663-32fcadffb155"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseLeftButtonClick"",
                    ""type"": ""Button"",
                    ""id"": ""72381ced-b1ac-4de4-b894-cbe88d8e452e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseRightButtonClick"",
                    ""type"": ""Button"",
                    ""id"": ""93a683aa-5953-46c7-8258-45cc3a988cc3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseWheelScroll"",
                    ""type"": ""PassThrough"",
                    ""id"": ""19cc146b-71e3-4fe9-bc97-695c42958990"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlayerInteraction"",
                    ""type"": ""Button"",
                    ""id"": ""adcb1dbf-2b9c-4c15-a998-c52078b6b1dc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlayerAcceleration"",
                    ""type"": ""Button"",
                    ""id"": ""6c4a9064-76b6-47d3-a801-eba059b75555"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlayerJump"",
                    ""type"": ""Button"",
                    ""id"": ""f585be68-3554-4b4a-8b7b-a4a7c80d074a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlayerCrouch"",
                    ""type"": ""Button"",
                    ""id"": ""b2172d24-4e1a-46f6-847f-baac643b666e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlayerTab"",
                    ""type"": ""Button"",
                    ""id"": ""e6d33b54-b50b-4567-bc97-8181dad29473"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlayerEsc"",
                    ""type"": ""Button"",
                    ""id"": ""ef8ac863-ad08-4728-8cb1-fd5a51d018d0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASDPlayerInput"",
                    ""id"": ""f7d338cf-022e-404f-93fd-c748038a85eb"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerInput"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""560fec98-7d14-49e2-b9e5-c355e56ef8f6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ceedf6af-5d4b-4050-8e1f-f96c29469a77"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c46a772a-75fc-4fbc-995e-4b3c6c28ea0a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8ae1979b-4da6-4d89-9568-2eb5c9d02e8d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""38d3a77f-cdad-4e27-b649-b4842b1bfd80"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b694c377-cb5c-4ea1-bc3c-7e544528cd65"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1),Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLeftButtonClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""faa3bceb-6b17-46f4-8922-e1106c4c5f19"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseRightButtonClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""918f7bbb-57b8-4d12-81f4-36b53894bd6e"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseWheelScroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1542d8d0-d5be-4c9e-9b0a-37c4b8e75920"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerInteraction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2251d359-429d-4b7e-b2ea-e80f3e91cd8b"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerAcceleration"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""385b81e7-5ce1-4c90-8642-76860cbb3a51"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerJump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""312fe639-543c-4117-a800-303c0bf8bfd8"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerCrouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9afe411e-46ad-4552-8326-8948cf14e4b6"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerTab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8930d6eb-992c-4231-91c4-ce5a4b76918d"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerEsc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerInput
        m_PlayerInput = asset.FindActionMap("PlayerInput", throwIfNotFound: true);
        m_PlayerInput_PlayerInput = m_PlayerInput.FindAction("PlayerInput", throwIfNotFound: true);
        m_PlayerInput_MousePosition = m_PlayerInput.FindAction("MousePosition", throwIfNotFound: true);
        m_PlayerInput_MouseLeftButtonClick = m_PlayerInput.FindAction("MouseLeftButtonClick", throwIfNotFound: true);
        m_PlayerInput_MouseRightButtonClick = m_PlayerInput.FindAction("MouseRightButtonClick", throwIfNotFound: true);
        m_PlayerInput_MouseWheelScroll = m_PlayerInput.FindAction("MouseWheelScroll", throwIfNotFound: true);
        m_PlayerInput_PlayerInteraction = m_PlayerInput.FindAction("PlayerInteraction", throwIfNotFound: true);
        m_PlayerInput_PlayerAcceleration = m_PlayerInput.FindAction("PlayerAcceleration", throwIfNotFound: true);
        m_PlayerInput_PlayerJump = m_PlayerInput.FindAction("PlayerJump", throwIfNotFound: true);
        m_PlayerInput_PlayerCrouch = m_PlayerInput.FindAction("PlayerCrouch", throwIfNotFound: true);
        m_PlayerInput_PlayerTab = m_PlayerInput.FindAction("PlayerTab", throwIfNotFound: true);
        m_PlayerInput_PlayerEsc = m_PlayerInput.FindAction("PlayerEsc", throwIfNotFound: true);
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

    // PlayerInput
    private readonly InputActionMap m_PlayerInput;
    private IPlayerInputActions m_PlayerInputActionsCallbackInterface;
    private readonly InputAction m_PlayerInput_PlayerInput;
    private readonly InputAction m_PlayerInput_MousePosition;
    private readonly InputAction m_PlayerInput_MouseLeftButtonClick;
    private readonly InputAction m_PlayerInput_MouseRightButtonClick;
    private readonly InputAction m_PlayerInput_MouseWheelScroll;
    private readonly InputAction m_PlayerInput_PlayerInteraction;
    private readonly InputAction m_PlayerInput_PlayerAcceleration;
    private readonly InputAction m_PlayerInput_PlayerJump;
    private readonly InputAction m_PlayerInput_PlayerCrouch;
    private readonly InputAction m_PlayerInput_PlayerTab;
    private readonly InputAction m_PlayerInput_PlayerEsc;
    public struct PlayerInputActions
    {
        private @PlayerInputAction m_Wrapper;
        public PlayerInputActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlayerInput => m_Wrapper.m_PlayerInput_PlayerInput;
        public InputAction @MousePosition => m_Wrapper.m_PlayerInput_MousePosition;
        public InputAction @MouseLeftButtonClick => m_Wrapper.m_PlayerInput_MouseLeftButtonClick;
        public InputAction @MouseRightButtonClick => m_Wrapper.m_PlayerInput_MouseRightButtonClick;
        public InputAction @MouseWheelScroll => m_Wrapper.m_PlayerInput_MouseWheelScroll;
        public InputAction @PlayerInteraction => m_Wrapper.m_PlayerInput_PlayerInteraction;
        public InputAction @PlayerAcceleration => m_Wrapper.m_PlayerInput_PlayerAcceleration;
        public InputAction @PlayerJump => m_Wrapper.m_PlayerInput_PlayerJump;
        public InputAction @PlayerCrouch => m_Wrapper.m_PlayerInput_PlayerCrouch;
        public InputAction @PlayerTab => m_Wrapper.m_PlayerInput_PlayerTab;
        public InputAction @PlayerEsc => m_Wrapper.m_PlayerInput_PlayerEsc;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInputActions instance)
        {
            if (m_Wrapper.m_PlayerInputActionsCallbackInterface != null)
            {
                @PlayerInput.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerInput;
                @PlayerInput.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerInput;
                @PlayerInput.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerInput;
                @MousePosition.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMousePosition;
                @MouseLeftButtonClick.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMouseLeftButtonClick;
                @MouseLeftButtonClick.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMouseLeftButtonClick;
                @MouseLeftButtonClick.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMouseLeftButtonClick;
                @MouseRightButtonClick.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMouseRightButtonClick;
                @MouseRightButtonClick.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMouseRightButtonClick;
                @MouseRightButtonClick.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMouseRightButtonClick;
                @MouseWheelScroll.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMouseWheelScroll;
                @MouseWheelScroll.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMouseWheelScroll;
                @MouseWheelScroll.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMouseWheelScroll;
                @PlayerInteraction.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerInteraction;
                @PlayerInteraction.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerInteraction;
                @PlayerInteraction.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerInteraction;
                @PlayerAcceleration.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerAcceleration;
                @PlayerAcceleration.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerAcceleration;
                @PlayerAcceleration.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerAcceleration;
                @PlayerJump.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerJump;
                @PlayerJump.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerJump;
                @PlayerJump.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerJump;
                @PlayerCrouch.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerCrouch;
                @PlayerCrouch.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerCrouch;
                @PlayerCrouch.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerCrouch;
                @PlayerTab.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerTab;
                @PlayerTab.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerTab;
                @PlayerTab.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerTab;
                @PlayerEsc.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerEsc;
                @PlayerEsc.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerEsc;
                @PlayerEsc.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerEsc;
            }
            m_Wrapper.m_PlayerInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlayerInput.started += instance.OnPlayerInput;
                @PlayerInput.performed += instance.OnPlayerInput;
                @PlayerInput.canceled += instance.OnPlayerInput;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @MouseLeftButtonClick.started += instance.OnMouseLeftButtonClick;
                @MouseLeftButtonClick.performed += instance.OnMouseLeftButtonClick;
                @MouseLeftButtonClick.canceled += instance.OnMouseLeftButtonClick;
                @MouseRightButtonClick.started += instance.OnMouseRightButtonClick;
                @MouseRightButtonClick.performed += instance.OnMouseRightButtonClick;
                @MouseRightButtonClick.canceled += instance.OnMouseRightButtonClick;
                @MouseWheelScroll.started += instance.OnMouseWheelScroll;
                @MouseWheelScroll.performed += instance.OnMouseWheelScroll;
                @MouseWheelScroll.canceled += instance.OnMouseWheelScroll;
                @PlayerInteraction.started += instance.OnPlayerInteraction;
                @PlayerInteraction.performed += instance.OnPlayerInteraction;
                @PlayerInteraction.canceled += instance.OnPlayerInteraction;
                @PlayerAcceleration.started += instance.OnPlayerAcceleration;
                @PlayerAcceleration.performed += instance.OnPlayerAcceleration;
                @PlayerAcceleration.canceled += instance.OnPlayerAcceleration;
                @PlayerJump.started += instance.OnPlayerJump;
                @PlayerJump.performed += instance.OnPlayerJump;
                @PlayerJump.canceled += instance.OnPlayerJump;
                @PlayerCrouch.started += instance.OnPlayerCrouch;
                @PlayerCrouch.performed += instance.OnPlayerCrouch;
                @PlayerCrouch.canceled += instance.OnPlayerCrouch;
                @PlayerTab.started += instance.OnPlayerTab;
                @PlayerTab.performed += instance.OnPlayerTab;
                @PlayerTab.canceled += instance.OnPlayerTab;
                @PlayerEsc.started += instance.OnPlayerEsc;
                @PlayerEsc.performed += instance.OnPlayerEsc;
                @PlayerEsc.canceled += instance.OnPlayerEsc;
            }
        }
    }
    public PlayerInputActions @PlayerInput => new PlayerInputActions(this);
    public interface IPlayerInputActions
    {
        void OnPlayerInput(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnMouseLeftButtonClick(InputAction.CallbackContext context);
        void OnMouseRightButtonClick(InputAction.CallbackContext context);
        void OnMouseWheelScroll(InputAction.CallbackContext context);
        void OnPlayerInteraction(InputAction.CallbackContext context);
        void OnPlayerAcceleration(InputAction.CallbackContext context);
        void OnPlayerJump(InputAction.CallbackContext context);
        void OnPlayerCrouch(InputAction.CallbackContext context);
        void OnPlayerTab(InputAction.CallbackContext context);
        void OnPlayerEsc(InputAction.CallbackContext context);
    }
}
