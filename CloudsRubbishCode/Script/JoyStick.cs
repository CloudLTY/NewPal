using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour
{
	public float Radius = 60f;
	public Sprite _spriteLight;
	public Sprite _sprite;
	public GameObject Back;
	public GameObject ControlCharacter;

	private RectTransform JoyRectTransform;
	private Image m_image;
	private Vector2 m_JoystickPos;

	private Touch touchID;

	private bool isFirstEnter = true;

	void Awake()
	{
		
	}

	// Use this for initialization
	void Start()
	{
		JoyRectTransform = this.GetComponent<RectTransform>();
		m_image = this.GetComponent<Image>();

		m_JoystickPos = JoyRectTransform.anchoredPosition;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.touchCount > 0)
		{

			touchID = Input.GetTouch(0);
			if(isFirstEnter)
			{
				if (isConstraintPosition(touchID.position))
				{
					isFirstEnter = false;
					JoyRectTransform.anchoredPosition = ConstraintPosition(touchID.position);
				}
			}
			else
			{
				JoyRectTransform.anchoredPosition = ConstraintPosition(touchID.position);
			}		
		}
		else
		{
			isFirstEnter = true;
			ResetJoystick();
		}
	}

	void FixedUpdate()
	{
		
	}
	//自适应 约束
	Vector2 ConstraintPosition(Vector2 vec2)
	{

		m_image.sprite = _spriteLight;

		vec2.x *= 940.0f / Screen.width;
		vec2.y *= 560.0f / Screen.height;
		
		Vector2 currentPos = new Vector2(vec2.x - m_JoystickPos.x, vec2.y - m_JoystickPos.y);
		//print(vec2 + " c");
		if (Mathf.Pow(currentPos.x, 2) + Mathf.Pow(currentPos.y, 2) > Mathf.Pow(Radius, 2))
		{
			vec2 = currentPos.normalized * Radius;
			vec2 = new Vector2(vec2.x + m_JoystickPos.x, vec2.y + m_JoystickPos.y);
		}

		Back.transform.rotation = Quaternion.EulerAngles(0, 0, Mathf.Asin(currentPos.normalized.y));
		//发送消息,向量
		ControlCharacter.SendMessage("MoveDirection", currentPos.normalized, SendMessageOptions.DontRequireReceiver);
		return vec2;
	}

	bool isConstraintPosition(Vector2 vec2)
	{
		vec2.x *= 940.0f / Screen.width;
		vec2.y *= 560.0f / Screen.height;
		
		Vector2 currentPos = new Vector2(vec2.x - m_JoystickPos.x, vec2.y - m_JoystickPos.y);
		if ((Mathf.Pow(currentPos.x, 2) + Mathf.Pow(currentPos.y, 2) < Mathf.Pow(Radius, 2)))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	//
	void ResetJoystick()
	{
		Back.transform.rotation = Quaternion.EulerAngles(0, 0, 0);
		m_image.sprite = _sprite;
		JoyRectTransform.anchoredPosition = m_JoystickPos;
	}
}
