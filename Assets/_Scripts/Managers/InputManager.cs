using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region # Data Structures #
    [System.Serializable]
    public class Axes
    {
        public string AxeId;
        public string xAxisName;
        public string yAxisName;
        public float xValue;
        public float yValue;

        public void SetAxesNames(string x, string y)
        {
            xAxisName = x;
            yAxisName = y;
        }

        public void GetAxes()
        {
            xValue = Input.GetAxis(xAxisName);
            yValue = Input.GetAxis(yAxisName);
        }

        public Vector3 GetAxesDirectionAsVector3()
        {
            return Vector3.ClampMagnitude(new Vector3(xValue, yValue, 0), 1);
        }

        public Vector2 GetAxesDirectionAsVector2()
        {
            return Vector2.ClampMagnitude(new Vector2(xValue, yValue), 1);
        }
    }
    public enum ButtonState
    {
        Released,
        OnPressed,
        Pressed,
        OnReleased
    }

    [System.Serializable]
    public class Button
    {
        public enum ButtonState
        {
            Released,
            OnPressed,
            Pressed,
            OnReleased
        }

        public string buttonId;
        public string ButtonName;
        public ButtonState CurrentState;

        public delegate void OnButtonAction();
        public event OnButtonAction OnButtonPressedEvent;
        public event OnButtonAction ButtonPressedEvent;
        public event OnButtonAction OnButtonReleasedEvent;
        public event OnButtonAction ButtonReleasedEvent;

        public void SetButtonName(string newButtonName)
        {
            ButtonName = newButtonName;
        }

        public void GetButton()
        {
            if (Input.GetButtonDown(ButtonName))
            {
                CurrentState = ButtonState.OnPressed;
                if (OnButtonPressedEvent != null)
                    OnButtonPressedEvent();
            }
            else if (Input.GetButton(ButtonName))
            {
                CurrentState = ButtonState.Pressed;
                if (ButtonPressedEvent != null)
                    ButtonPressedEvent();
            }
            else if (Input.GetButtonUp(ButtonName))
            {
                CurrentState = ButtonState.OnReleased;
                if (OnButtonReleasedEvent != null)
                    OnButtonReleasedEvent();
            }
            else
            {
                CurrentState = ButtonState.Released;
                if (ButtonReleasedEvent != null)
                    ButtonReleasedEvent();
            }
        }
    }

    [System.Serializable]
    public class MouseButton
    {
        public enum MousePossibleButtons
        {
            LeftClick = 0,
            RightClick = 1,
            MiddleClick = 2
        }

        public string buttonId;
        public MousePossibleButtons curMouseButton;
        public Button.ButtonState CurrentState;
        public Vector3 MousePos;
        public Vector3 MouseWorldPos;

        public delegate void OnMouseButtonAction();
        public event OnMouseButtonAction OnButtonPressedEvent;
        public event OnMouseButtonAction ButtonPressedEvent;
        public event OnMouseButtonAction OnButtonReleasedEvent;
        public event OnMouseButtonAction ButtonReleasedEvent;

        public void SetMouseButton(MousePossibleButtons newMouseButton)
        {
            curMouseButton = newMouseButton;
        }

        public void GetButton()
        {
            if (Input.GetMouseButtonDown((int)curMouseButton))
            {
                MousePos = Input.mousePosition;
                MouseWorldPos = Camera.main.ScreenToWorldPoint(MousePos);
                CurrentState = Button.ButtonState.OnPressed;
                if (OnButtonPressedEvent != null)
                    OnButtonPressedEvent();
            }
            else if (Input.GetMouseButton((int)curMouseButton))
            {
                MousePos = Input.mousePosition;
                MouseWorldPos = Camera.main.ScreenToWorldPoint(MousePos);
                CurrentState = Button.ButtonState.Pressed;
                if (ButtonPressedEvent != null)
                    ButtonPressedEvent();
            }
            else if (Input.GetMouseButtonUp((int)curMouseButton))
            {
                MousePos = Input.mousePosition;
                MouseWorldPos = Camera.main.ScreenToWorldPoint(MousePos);
                CurrentState = Button.ButtonState.OnReleased;
                if (OnButtonReleasedEvent != null)
                    OnButtonReleasedEvent();
            }
            else
            {
                CurrentState = Button.ButtonState.Released;
                if (ButtonReleasedEvent != null)
                    ButtonReleasedEvent();
            }
        }
    }
    #endregion

    #region # Singleton #
    private static InputManager _instance;
    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion

    #region # Input #
    public List<Axes> axes;
    public List<Button> buttons;
    public List<MouseButton> mouseButtons;
    #endregion

    #region Monobehaviour
    private void Awake()
    {
        SetupSingleton();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetAllAxes();
        GetAllButtons();
    }
    #endregion

    #region Initialization
    public void SetupSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        return;
    }
    #endregion

    #region Input
    public void GetAllAxes()
    {
        foreach (var axe in axes)
        {
            axe.GetAxes();
        }
    }

    public void GetAllButtons()
    {
        foreach (var button in buttons)
        {
            button.GetButton();
        }

        foreach (var mouseButton in mouseButtons)
        {
            mouseButton.GetButton();
        }
    }

    public Button GetButtonByID(string id)
    {
        foreach (var button in buttons)
        {
            if (button.buttonId == id)
                return button;
        }

        return null;
    }

    public MouseButton GetMouseButtonById(string id)
    {
        foreach (var button in mouseButtons)
        {
            if (button.buttonId == id)
                return button;
        }

        return null;
    }

    public Axes GetAxisById(string id)
    {
        foreach (var axe in axes)
        {
            if (axe.AxeId == id)
                return axe;
        }

        return null;
    }
    #endregion
}
