// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""63504919-f997-40c3-8fbe-43d86e73e0c9"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""a95456d6-b01a-4e7d-9811-e819962e3fc6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PushBox"",
                    ""type"": ""Button"",
                    ""id"": ""f0d7c9fc-d52b-4cf7-94b3-c2105f804ec1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CreateDestroy"",
                    ""type"": ""Button"",
                    ""id"": ""8c06eb42-adbb-44cb-b3a7-52b92fa1a163"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PickUp"",
                    ""type"": ""Button"",
                    ""id"": ""24b10b6e-9e9f-4180-ad30-4d13fce3f05f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwapChar"",
                    ""type"": ""Button"",
                    ""id"": ""e82e62a0-9a0c-4778-81d1-9f636fceea21"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PullSwitch"",
                    ""type"": ""Button"",
                    ""id"": ""1d7fdf22-2753-4a22-881d-3156d5f238fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StayOnButton"",
                    ""type"": ""Button"",
                    ""id"": ""0cfff7e0-ac42-4e6e-bf5a-d0c4e55526a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OffButton"",
                    ""type"": ""Button"",
                    ""id"": ""b900d40b-a42c-4859-afc8-7e54da221418"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""1a009a21-b105-422d-9387-593ec28973e2"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""96480e86-1a07-4a04-b6d0-d49b534206ac"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3cff21c0-9dd3-4b56-8b3c-ba9021b1f0b1"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0ed1b38f-fcc8-4758-8084-b11bd2290d74"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""56c03b8b-39cb-43d3-89fb-49cee708cfaa"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""e81b4551-d7e0-4cb8-8ae5-6013e12e1936"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f41cd3f3-745d-4b80-a10b-6f88c9374129"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""da277aa6-32a8-4ca4-a02b-9e5a20905284"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""be73d86a-0adc-4a58-a199-768300dac72d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fd8da8fb-2f76-4bb5-99e6-249106a74ad5"",
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
                    ""id"": ""99c6d9cc-ae61-498b-884d-3bc39befd934"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PushBox"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""04798bc0-b2ec-468c-8f50-1a27a2595109"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PushBox"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""804b59ae-622e-47d5-8935-007f27b7bc78"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CreateDestroy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4850471a-0649-49b1-afdb-cca0b9060715"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b41a56ca-6a88-4fe3-9a11-ba821c2115ed"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwapChar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8fd4782f-9f26-4928-b18e-1c05a536e8bd"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PullSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""585aae95-1bcd-446f-8ae1-5983a3972b19"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StayOnButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd5c0784-5a5e-43ff-82a7-e16d14a61c02"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OffButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfec8fd6-015c-4903-aacb-2ad5203c25d6"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CreateDestroy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""633eabf6-e3d3-4209-b30a-48ad8c1d4378"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OffButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63b6a362-f1a3-4a50-b39a-e9da3500fce2"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StayOnButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21e98be1-9333-4fae-83b1-1f10a9a3a472"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PullSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5808eee-05c1-4ba5-8c09-00df5c83b1e6"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwapChar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b2188a9-c64c-483d-871d-5f10dabf053a"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""City"",
            ""id"": ""baccace9-0234-4b15-ac72-430fd183421c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""f91691aa-476b-4766-a80f-81f20528b703"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""1958d9ac-1b6a-49d8-9e6c-3725c8e0aa9c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PickUp"",
                    ""type"": ""Button"",
                    ""id"": ""be0cb74a-144c-4405-a81e-7d6ee8bb3dcc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3150d80f-b004-4b3c-8e86-33230228b663"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""80a88fc1-626e-4b49-b110-3354d62daf08"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4ef22318-c704-4c01-aa87-fd873680e086"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""87d23c4d-0b82-4c7f-a9d0-38f64447bffe"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a3e1c43d-821d-44fd-b4b3-0913709ce345"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""327a1107-6a67-4409-9244-8706c2a558bb"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""3af06c57-d180-4357-b66a-d5a0f45f3866"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5de80950-a568-4e98-aaf0-6cfbda1a745d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""34bd40d0-08af-45ba-8bea-614f19bc6f23"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""57677b92-b6be-4d5f-9a45-c7fee8a95d94"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d6837599-b286-477d-a4ef-18727f98b7e7"",
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
                    ""id"": ""a1a1472b-9a0d-4295-abba-8e641fa6eb64"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""834f291c-2de3-4b1d-9b86-927699394ef1"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7c6653b-94e5-4299-8733-598e713b158a"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""2a4c7a9e-d7b3-4d80-8bed-239efa4450e3"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""eca04c42-1973-4b5b-a103-d98c88f221aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""e29d802b-9835-4ea8-b3cc-2c9e6d375456"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ClosePopUpPage"",
                    ""type"": ""Button"",
                    ""id"": ""33383895-fc55-4de6-9271-31071e689606"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BrowseDiary"",
                    ""type"": ""Button"",
                    ""id"": ""ed2d7821-f758-42d4-9efd-61c65bdc4874"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""20655842-8ebd-4be9-929e-915a9091b967"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""580afdf0-4a69-47b7-aab4-1707f7491311"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eb314b97-7107-4fb1-8e43-735cc314734a"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClosePopUpPage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""2f1dc02a-4d12-476f-b5f9-818bd6ddc16c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BrowseDiary"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2d3c40d5-1194-4f1e-ace7-0b9f430f0b6c"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BrowseDiary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6febd16e-13aa-49e7-b98c-4174de2c53f1"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BrowseDiary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""9cde9e92-bad3-4341-b9df-8a3182d904b6"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BrowseDiary"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""7523e68f-aa69-40c4-9e18-f0af6473daad"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BrowseDiary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""8fac7b7d-98b2-40c5-9920-4863fbb89efc"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BrowseDiary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""df06bffd-ce89-474f-a56c-d659a1a78718"",
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
                    ""id"": ""66e19e44-2127-40da-b631-7b51a34eb040"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69e39700-4e84-4f1b-a724-e6d3852cba9b"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClosePopUpPage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Dialogue"",
            ""id"": ""989a9232-26e1-48ae-84a3-f37e7992affa"",
            ""actions"": [
                {
                    ""name"": ""Talk"",
                    ""type"": ""Button"",
                    ""id"": ""d598bb4b-7c1c-43e3-b945-ff4bfb2febbb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skip"",
                    ""type"": ""Button"",
                    ""id"": ""daeb64f5-c175-4cf4-98cc-286efac2a5d2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5ca0ca9c-c580-48f1-bdd3-fd4d54fcac7d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Talk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4dab1a08-f07d-4241-b2de-105846e8714f"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d22e9f95-3861-4640-9b2b-472140002483"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Talk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d1221e0-f5f6-4b8e-817b-8ad025ecfe7e"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_PushBox = m_Gameplay.FindAction("PushBox", throwIfNotFound: true);
        m_Gameplay_CreateDestroy = m_Gameplay.FindAction("CreateDestroy", throwIfNotFound: true);
        m_Gameplay_PickUp = m_Gameplay.FindAction("PickUp", throwIfNotFound: true);
        m_Gameplay_SwapChar = m_Gameplay.FindAction("SwapChar", throwIfNotFound: true);
        m_Gameplay_PullSwitch = m_Gameplay.FindAction("PullSwitch", throwIfNotFound: true);
        m_Gameplay_StayOnButton = m_Gameplay.FindAction("StayOnButton", throwIfNotFound: true);
        m_Gameplay_OffButton = m_Gameplay.FindAction("OffButton", throwIfNotFound: true);
        // City
        m_City = asset.FindActionMap("City", throwIfNotFound: true);
        m_City_Move = m_City.FindAction("Move", throwIfNotFound: true);
        m_City_Interact = m_City.FindAction("Interact", throwIfNotFound: true);
        m_City_PickUp = m_City.FindAction("PickUp", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Pause = m_UI.FindAction("Pause", throwIfNotFound: true);
        m_UI_Inventory = m_UI.FindAction("Inventory", throwIfNotFound: true);
        m_UI_ClosePopUpPage = m_UI.FindAction("ClosePopUpPage", throwIfNotFound: true);
        m_UI_BrowseDiary = m_UI.FindAction("BrowseDiary", throwIfNotFound: true);
        // Dialogue
        m_Dialogue = asset.FindActionMap("Dialogue", throwIfNotFound: true);
        m_Dialogue_Talk = m_Dialogue.FindAction("Talk", throwIfNotFound: true);
        m_Dialogue_Skip = m_Dialogue.FindAction("Skip", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_PushBox;
    private readonly InputAction m_Gameplay_CreateDestroy;
    private readonly InputAction m_Gameplay_PickUp;
    private readonly InputAction m_Gameplay_SwapChar;
    private readonly InputAction m_Gameplay_PullSwitch;
    private readonly InputAction m_Gameplay_StayOnButton;
    private readonly InputAction m_Gameplay_OffButton;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @PushBox => m_Wrapper.m_Gameplay_PushBox;
        public InputAction @CreateDestroy => m_Wrapper.m_Gameplay_CreateDestroy;
        public InputAction @PickUp => m_Wrapper.m_Gameplay_PickUp;
        public InputAction @SwapChar => m_Wrapper.m_Gameplay_SwapChar;
        public InputAction @PullSwitch => m_Wrapper.m_Gameplay_PullSwitch;
        public InputAction @StayOnButton => m_Wrapper.m_Gameplay_StayOnButton;
        public InputAction @OffButton => m_Wrapper.m_Gameplay_OffButton;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @PushBox.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPushBox;
                @PushBox.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPushBox;
                @PushBox.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPushBox;
                @CreateDestroy.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCreateDestroy;
                @CreateDestroy.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCreateDestroy;
                @CreateDestroy.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCreateDestroy;
                @PickUp.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPickUp;
                @PickUp.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPickUp;
                @PickUp.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPickUp;
                @SwapChar.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwapChar;
                @SwapChar.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwapChar;
                @SwapChar.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwapChar;
                @PullSwitch.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPullSwitch;
                @PullSwitch.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPullSwitch;
                @PullSwitch.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPullSwitch;
                @StayOnButton.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStayOnButton;
                @StayOnButton.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStayOnButton;
                @StayOnButton.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStayOnButton;
                @OffButton.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnOffButton;
                @OffButton.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnOffButton;
                @OffButton.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnOffButton;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @PushBox.started += instance.OnPushBox;
                @PushBox.performed += instance.OnPushBox;
                @PushBox.canceled += instance.OnPushBox;
                @CreateDestroy.started += instance.OnCreateDestroy;
                @CreateDestroy.performed += instance.OnCreateDestroy;
                @CreateDestroy.canceled += instance.OnCreateDestroy;
                @PickUp.started += instance.OnPickUp;
                @PickUp.performed += instance.OnPickUp;
                @PickUp.canceled += instance.OnPickUp;
                @SwapChar.started += instance.OnSwapChar;
                @SwapChar.performed += instance.OnSwapChar;
                @SwapChar.canceled += instance.OnSwapChar;
                @PullSwitch.started += instance.OnPullSwitch;
                @PullSwitch.performed += instance.OnPullSwitch;
                @PullSwitch.canceled += instance.OnPullSwitch;
                @StayOnButton.started += instance.OnStayOnButton;
                @StayOnButton.performed += instance.OnStayOnButton;
                @StayOnButton.canceled += instance.OnStayOnButton;
                @OffButton.started += instance.OnOffButton;
                @OffButton.performed += instance.OnOffButton;
                @OffButton.canceled += instance.OnOffButton;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // City
    private readonly InputActionMap m_City;
    private ICityActions m_CityActionsCallbackInterface;
    private readonly InputAction m_City_Move;
    private readonly InputAction m_City_Interact;
    private readonly InputAction m_City_PickUp;
    public struct CityActions
    {
        private @PlayerControls m_Wrapper;
        public CityActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_City_Move;
        public InputAction @Interact => m_Wrapper.m_City_Interact;
        public InputAction @PickUp => m_Wrapper.m_City_PickUp;
        public InputActionMap Get() { return m_Wrapper.m_City; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CityActions set) { return set.Get(); }
        public void SetCallbacks(ICityActions instance)
        {
            if (m_Wrapper.m_CityActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_CityActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CityActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CityActionsCallbackInterface.OnMove;
                @Interact.started -= m_Wrapper.m_CityActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_CityActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_CityActionsCallbackInterface.OnInteract;
                @PickUp.started -= m_Wrapper.m_CityActionsCallbackInterface.OnPickUp;
                @PickUp.performed -= m_Wrapper.m_CityActionsCallbackInterface.OnPickUp;
                @PickUp.canceled -= m_Wrapper.m_CityActionsCallbackInterface.OnPickUp;
            }
            m_Wrapper.m_CityActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @PickUp.started += instance.OnPickUp;
                @PickUp.performed += instance.OnPickUp;
                @PickUp.canceled += instance.OnPickUp;
            }
        }
    }
    public CityActions @City => new CityActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Pause;
    private readonly InputAction m_UI_Inventory;
    private readonly InputAction m_UI_ClosePopUpPage;
    private readonly InputAction m_UI_BrowseDiary;
    public struct UIActions
    {
        private @PlayerControls m_Wrapper;
        public UIActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_UI_Pause;
        public InputAction @Inventory => m_Wrapper.m_UI_Inventory;
        public InputAction @ClosePopUpPage => m_Wrapper.m_UI_ClosePopUpPage;
        public InputAction @BrowseDiary => m_Wrapper.m_UI_BrowseDiary;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPause;
                @Inventory.started -= m_Wrapper.m_UIActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnInventory;
                @ClosePopUpPage.started -= m_Wrapper.m_UIActionsCallbackInterface.OnClosePopUpPage;
                @ClosePopUpPage.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnClosePopUpPage;
                @ClosePopUpPage.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnClosePopUpPage;
                @BrowseDiary.started -= m_Wrapper.m_UIActionsCallbackInterface.OnBrowseDiary;
                @BrowseDiary.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnBrowseDiary;
                @BrowseDiary.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnBrowseDiary;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @ClosePopUpPage.started += instance.OnClosePopUpPage;
                @ClosePopUpPage.performed += instance.OnClosePopUpPage;
                @ClosePopUpPage.canceled += instance.OnClosePopUpPage;
                @BrowseDiary.started += instance.OnBrowseDiary;
                @BrowseDiary.performed += instance.OnBrowseDiary;
                @BrowseDiary.canceled += instance.OnBrowseDiary;
            }
        }
    }
    public UIActions @UI => new UIActions(this);

    // Dialogue
    private readonly InputActionMap m_Dialogue;
    private IDialogueActions m_DialogueActionsCallbackInterface;
    private readonly InputAction m_Dialogue_Talk;
    private readonly InputAction m_Dialogue_Skip;
    public struct DialogueActions
    {
        private @PlayerControls m_Wrapper;
        public DialogueActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Talk => m_Wrapper.m_Dialogue_Talk;
        public InputAction @Skip => m_Wrapper.m_Dialogue_Skip;
        public InputActionMap Get() { return m_Wrapper.m_Dialogue; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DialogueActions set) { return set.Get(); }
        public void SetCallbacks(IDialogueActions instance)
        {
            if (m_Wrapper.m_DialogueActionsCallbackInterface != null)
            {
                @Talk.started -= m_Wrapper.m_DialogueActionsCallbackInterface.OnTalk;
                @Talk.performed -= m_Wrapper.m_DialogueActionsCallbackInterface.OnTalk;
                @Talk.canceled -= m_Wrapper.m_DialogueActionsCallbackInterface.OnTalk;
                @Skip.started -= m_Wrapper.m_DialogueActionsCallbackInterface.OnSkip;
                @Skip.performed -= m_Wrapper.m_DialogueActionsCallbackInterface.OnSkip;
                @Skip.canceled -= m_Wrapper.m_DialogueActionsCallbackInterface.OnSkip;
            }
            m_Wrapper.m_DialogueActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Talk.started += instance.OnTalk;
                @Talk.performed += instance.OnTalk;
                @Talk.canceled += instance.OnTalk;
                @Skip.started += instance.OnSkip;
                @Skip.performed += instance.OnSkip;
                @Skip.canceled += instance.OnSkip;
            }
        }
    }
    public DialogueActions @Dialogue => new DialogueActions(this);
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnPushBox(InputAction.CallbackContext context);
        void OnCreateDestroy(InputAction.CallbackContext context);
        void OnPickUp(InputAction.CallbackContext context);
        void OnSwapChar(InputAction.CallbackContext context);
        void OnPullSwitch(InputAction.CallbackContext context);
        void OnStayOnButton(InputAction.CallbackContext context);
        void OnOffButton(InputAction.CallbackContext context);
    }
    public interface ICityActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnPickUp(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnPause(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnClosePopUpPage(InputAction.CallbackContext context);
        void OnBrowseDiary(InputAction.CallbackContext context);
    }
    public interface IDialogueActions
    {
        void OnTalk(InputAction.CallbackContext context);
        void OnSkip(InputAction.CallbackContext context);
    }
}
