//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/InputActions.inputactions
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

public partial class @InputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Player1"",
            ""id"": ""50a879ea-827a-42e3-b553-0f76ee600955"",
            ""actions"": [
                {
                    ""name"": ""MovementP1"",
                    ""type"": ""Value"",
                    ""id"": ""12bb233e-d947-4110-9471-6b32bb3bfb54"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ComboP1"",
                    ""type"": ""Button"",
                    ""id"": ""f2c1b3b2-9e81-4ff3-a87a-f0effaf150ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""faa19491-e1b8-431d-aaf7-3f80e35a45a2"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementP1"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8696cfe6-2c5d-4d20-9000-21d97877dba0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""47350731-19ce-4607-9ce8-a9e16126c3cc"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""34d89418-2cf2-408b-ab1b-0f081948d342"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fc0da5e2-a55d-4178-b716-a943e2ac3f01"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1820acef-91ea-483e-89d2-a0da72f56284"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40716fae-4599-4a1f-a4f0-5534b9e54b73"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ComboP1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player2"",
            ""id"": ""bd1c4500-c662-477f-be33-5917c56f99b0"",
            ""actions"": [
                {
                    ""name"": ""MovementP2"",
                    ""type"": ""Value"",
                    ""id"": ""62a25625-10e9-4aa6-881a-0c0155c62dcf"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""IJKL"",
                    ""id"": ""960b3180-9568-4d38-9f0e-c24bf16b34c5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementP2"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c2308aaf-428c-4792-b3b9-d8e251b9e448"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d1402227-0c8a-4c8b-9f72-00cc5a5bb29f"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7657568e-41f2-4f1e-9662-57cec83e6ef7"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6491f22b-d6ec-4ed0-bff1-85870c4b3557"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""1a2d31ff-3f79-4cb7-9cea-8b498b72f732"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementP2"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""74b9504b-0e6c-40f7-a29d-0a59100d5109"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""47a21604-39af-4cc2-95d7-1ca5936c1424"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7f587175-6e49-4f7f-95f2-950e70ff6674"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e43fba61-9e26-4752-b590-82ee146f890c"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""ComboP1"",
            ""id"": ""426f1a4c-3eb1-4f4a-8bd7-7fa56c176c67"",
            ""actions"": [
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""146f85cb-5bf5-4388-b5e7-7b3b80522dda"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""5b0b9889-b28e-4520-92d5-70148b2e3364"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""f17799a5-0471-470d-a75e-007163ee0129"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""c6d9469c-0aa7-44df-806b-a46e4529771d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""e3e80436-0bcc-4a31-958e-e6e03a44935b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1cc49b14-e41a-471b-b08c-7a6cafd1f357"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65f31195-4602-4b27-a859-922d3392991f"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ab79d93-5fe9-4c05-a2c3-4fd169ed9a9e"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6dbf5f31-375d-40b0-85e5-eafd08382372"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cac36989-1826-4235-aa4e-f59f513e299a"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ComboP2"",
            ""id"": ""ef7e9f96-590f-4641-ab8e-74f861341b15"",
            ""actions"": [
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""3ecc71b7-a345-4710-b267-dec552b26cfa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""57274114-2609-4d13-b8fa-14ed731a9ec2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""f0c0db94-931e-4662-b353-78aa9680b8ca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""b057993b-dee4-4162-adcc-9b44b78293db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""04ac6415-dea3-4b6e-b54e-de324acf5377"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7a17f40a-e1f5-407e-8f9e-81d815599e68"",
                    ""path"": ""<Keyboard>/numpad8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9f4a91a-18e6-4ec7-93af-6206404561f8"",
                    ""path"": ""<Keyboard>/numpad5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4520c3ab-b277-46f5-ac5e-7ac71ccae748"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4cd384f5-b4e1-4c7c-938c-da36dcade087"",
                    ""path"": ""<Keyboard>/numpad6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa774efc-656f-4559-8924-450897d2e218"",
                    ""path"": ""<Keyboard>/numpad7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player1
        m_Player1 = asset.FindActionMap("Player1", throwIfNotFound: true);
        m_Player1_MovementP1 = m_Player1.FindAction("MovementP1", throwIfNotFound: true);
        m_Player1_ComboP1 = m_Player1.FindAction("ComboP1", throwIfNotFound: true);
        // Player2
        m_Player2 = asset.FindActionMap("Player2", throwIfNotFound: true);
        m_Player2_MovementP2 = m_Player2.FindAction("MovementP2", throwIfNotFound: true);
        // ComboP1
        m_ComboP1 = asset.FindActionMap("ComboP1", throwIfNotFound: true);
        m_ComboP1_Up = m_ComboP1.FindAction("Up", throwIfNotFound: true);
        m_ComboP1_Down = m_ComboP1.FindAction("Down", throwIfNotFound: true);
        m_ComboP1_Left = m_ComboP1.FindAction("Left", throwIfNotFound: true);
        m_ComboP1_Right = m_ComboP1.FindAction("Right", throwIfNotFound: true);
        m_ComboP1_Cancel = m_ComboP1.FindAction("Cancel", throwIfNotFound: true);
        // ComboP2
        m_ComboP2 = asset.FindActionMap("ComboP2", throwIfNotFound: true);
        m_ComboP2_Up = m_ComboP2.FindAction("Up", throwIfNotFound: true);
        m_ComboP2_Down = m_ComboP2.FindAction("Down", throwIfNotFound: true);
        m_ComboP2_Left = m_ComboP2.FindAction("Left", throwIfNotFound: true);
        m_ComboP2_Right = m_ComboP2.FindAction("Right", throwIfNotFound: true);
        m_ComboP2_Cancel = m_ComboP2.FindAction("Cancel", throwIfNotFound: true);
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

    // Player1
    private readonly InputActionMap m_Player1;
    private List<IPlayer1Actions> m_Player1ActionsCallbackInterfaces = new List<IPlayer1Actions>();
    private readonly InputAction m_Player1_MovementP1;
    private readonly InputAction m_Player1_ComboP1;
    public struct Player1Actions
    {
        private @InputActions m_Wrapper;
        public Player1Actions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @MovementP1 => m_Wrapper.m_Player1_MovementP1;
        public InputAction @ComboP1 => m_Wrapper.m_Player1_ComboP1;
        public InputActionMap Get() { return m_Wrapper.m_Player1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player1Actions set) { return set.Get(); }
        public void AddCallbacks(IPlayer1Actions instance)
        {
            if (instance == null || m_Wrapper.m_Player1ActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Player1ActionsCallbackInterfaces.Add(instance);
            @MovementP1.started += instance.OnMovementP1;
            @MovementP1.performed += instance.OnMovementP1;
            @MovementP1.canceled += instance.OnMovementP1;
            @ComboP1.started += instance.OnComboP1;
            @ComboP1.performed += instance.OnComboP1;
            @ComboP1.canceled += instance.OnComboP1;
        }

        private void UnregisterCallbacks(IPlayer1Actions instance)
        {
            @MovementP1.started -= instance.OnMovementP1;
            @MovementP1.performed -= instance.OnMovementP1;
            @MovementP1.canceled -= instance.OnMovementP1;
            @ComboP1.started -= instance.OnComboP1;
            @ComboP1.performed -= instance.OnComboP1;
            @ComboP1.canceled -= instance.OnComboP1;
        }

        public void RemoveCallbacks(IPlayer1Actions instance)
        {
            if (m_Wrapper.m_Player1ActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayer1Actions instance)
        {
            foreach (var item in m_Wrapper.m_Player1ActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Player1ActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Player1Actions @Player1 => new Player1Actions(this);

    // Player2
    private readonly InputActionMap m_Player2;
    private List<IPlayer2Actions> m_Player2ActionsCallbackInterfaces = new List<IPlayer2Actions>();
    private readonly InputAction m_Player2_MovementP2;
    public struct Player2Actions
    {
        private @InputActions m_Wrapper;
        public Player2Actions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @MovementP2 => m_Wrapper.m_Player2_MovementP2;
        public InputActionMap Get() { return m_Wrapper.m_Player2; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player2Actions set) { return set.Get(); }
        public void AddCallbacks(IPlayer2Actions instance)
        {
            if (instance == null || m_Wrapper.m_Player2ActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Player2ActionsCallbackInterfaces.Add(instance);
            @MovementP2.started += instance.OnMovementP2;
            @MovementP2.performed += instance.OnMovementP2;
            @MovementP2.canceled += instance.OnMovementP2;
        }

        private void UnregisterCallbacks(IPlayer2Actions instance)
        {
            @MovementP2.started -= instance.OnMovementP2;
            @MovementP2.performed -= instance.OnMovementP2;
            @MovementP2.canceled -= instance.OnMovementP2;
        }

        public void RemoveCallbacks(IPlayer2Actions instance)
        {
            if (m_Wrapper.m_Player2ActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayer2Actions instance)
        {
            foreach (var item in m_Wrapper.m_Player2ActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Player2ActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Player2Actions @Player2 => new Player2Actions(this);

    // ComboP1
    private readonly InputActionMap m_ComboP1;
    private List<IComboP1Actions> m_ComboP1ActionsCallbackInterfaces = new List<IComboP1Actions>();
    private readonly InputAction m_ComboP1_Up;
    private readonly InputAction m_ComboP1_Down;
    private readonly InputAction m_ComboP1_Left;
    private readonly InputAction m_ComboP1_Right;
    private readonly InputAction m_ComboP1_Cancel;
    public struct ComboP1Actions
    {
        private @InputActions m_Wrapper;
        public ComboP1Actions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Up => m_Wrapper.m_ComboP1_Up;
        public InputAction @Down => m_Wrapper.m_ComboP1_Down;
        public InputAction @Left => m_Wrapper.m_ComboP1_Left;
        public InputAction @Right => m_Wrapper.m_ComboP1_Right;
        public InputAction @Cancel => m_Wrapper.m_ComboP1_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_ComboP1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ComboP1Actions set) { return set.Get(); }
        public void AddCallbacks(IComboP1Actions instance)
        {
            if (instance == null || m_Wrapper.m_ComboP1ActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ComboP1ActionsCallbackInterfaces.Add(instance);
            @Up.started += instance.OnUp;
            @Up.performed += instance.OnUp;
            @Up.canceled += instance.OnUp;
            @Down.started += instance.OnDown;
            @Down.performed += instance.OnDown;
            @Down.canceled += instance.OnDown;
            @Left.started += instance.OnLeft;
            @Left.performed += instance.OnLeft;
            @Left.canceled += instance.OnLeft;
            @Right.started += instance.OnRight;
            @Right.performed += instance.OnRight;
            @Right.canceled += instance.OnRight;
            @Cancel.started += instance.OnCancel;
            @Cancel.performed += instance.OnCancel;
            @Cancel.canceled += instance.OnCancel;
        }

        private void UnregisterCallbacks(IComboP1Actions instance)
        {
            @Up.started -= instance.OnUp;
            @Up.performed -= instance.OnUp;
            @Up.canceled -= instance.OnUp;
            @Down.started -= instance.OnDown;
            @Down.performed -= instance.OnDown;
            @Down.canceled -= instance.OnDown;
            @Left.started -= instance.OnLeft;
            @Left.performed -= instance.OnLeft;
            @Left.canceled -= instance.OnLeft;
            @Right.started -= instance.OnRight;
            @Right.performed -= instance.OnRight;
            @Right.canceled -= instance.OnRight;
            @Cancel.started -= instance.OnCancel;
            @Cancel.performed -= instance.OnCancel;
            @Cancel.canceled -= instance.OnCancel;
        }

        public void RemoveCallbacks(IComboP1Actions instance)
        {
            if (m_Wrapper.m_ComboP1ActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IComboP1Actions instance)
        {
            foreach (var item in m_Wrapper.m_ComboP1ActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ComboP1ActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ComboP1Actions @ComboP1 => new ComboP1Actions(this);

    // ComboP2
    private readonly InputActionMap m_ComboP2;
    private List<IComboP2Actions> m_ComboP2ActionsCallbackInterfaces = new List<IComboP2Actions>();
    private readonly InputAction m_ComboP2_Up;
    private readonly InputAction m_ComboP2_Down;
    private readonly InputAction m_ComboP2_Left;
    private readonly InputAction m_ComboP2_Right;
    private readonly InputAction m_ComboP2_Cancel;
    public struct ComboP2Actions
    {
        private @InputActions m_Wrapper;
        public ComboP2Actions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Up => m_Wrapper.m_ComboP2_Up;
        public InputAction @Down => m_Wrapper.m_ComboP2_Down;
        public InputAction @Left => m_Wrapper.m_ComboP2_Left;
        public InputAction @Right => m_Wrapper.m_ComboP2_Right;
        public InputAction @Cancel => m_Wrapper.m_ComboP2_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_ComboP2; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ComboP2Actions set) { return set.Get(); }
        public void AddCallbacks(IComboP2Actions instance)
        {
            if (instance == null || m_Wrapper.m_ComboP2ActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ComboP2ActionsCallbackInterfaces.Add(instance);
            @Up.started += instance.OnUp;
            @Up.performed += instance.OnUp;
            @Up.canceled += instance.OnUp;
            @Down.started += instance.OnDown;
            @Down.performed += instance.OnDown;
            @Down.canceled += instance.OnDown;
            @Left.started += instance.OnLeft;
            @Left.performed += instance.OnLeft;
            @Left.canceled += instance.OnLeft;
            @Right.started += instance.OnRight;
            @Right.performed += instance.OnRight;
            @Right.canceled += instance.OnRight;
            @Cancel.started += instance.OnCancel;
            @Cancel.performed += instance.OnCancel;
            @Cancel.canceled += instance.OnCancel;
        }

        private void UnregisterCallbacks(IComboP2Actions instance)
        {
            @Up.started -= instance.OnUp;
            @Up.performed -= instance.OnUp;
            @Up.canceled -= instance.OnUp;
            @Down.started -= instance.OnDown;
            @Down.performed -= instance.OnDown;
            @Down.canceled -= instance.OnDown;
            @Left.started -= instance.OnLeft;
            @Left.performed -= instance.OnLeft;
            @Left.canceled -= instance.OnLeft;
            @Right.started -= instance.OnRight;
            @Right.performed -= instance.OnRight;
            @Right.canceled -= instance.OnRight;
            @Cancel.started -= instance.OnCancel;
            @Cancel.performed -= instance.OnCancel;
            @Cancel.canceled -= instance.OnCancel;
        }

        public void RemoveCallbacks(IComboP2Actions instance)
        {
            if (m_Wrapper.m_ComboP2ActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IComboP2Actions instance)
        {
            foreach (var item in m_Wrapper.m_ComboP2ActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ComboP2ActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ComboP2Actions @ComboP2 => new ComboP2Actions(this);
    public interface IPlayer1Actions
    {
        void OnMovementP1(InputAction.CallbackContext context);
        void OnComboP1(InputAction.CallbackContext context);
    }
    public interface IPlayer2Actions
    {
        void OnMovementP2(InputAction.CallbackContext context);
    }
    public interface IComboP1Actions
    {
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
    public interface IComboP2Actions
    {
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
}
