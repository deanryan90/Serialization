using UnityEngine;
using System.Collections;

[System.Serializable]
public class Player : MonoBehaviour {

	//int health = 100;
	float speed = 1.0f;
	Vector2 pos = Vector2.zero;
	private float axisInputX;
	private float axisInputY;
	//private bool isUsingController = false;
	Sprite sprite;
	public Sprite rightSprite;
	public Sprite leftSprite;
	public Sprite upSprite;
	public Sprite downSprite;
	public Sprite rightDownSprite;
	public Sprite leftDownSprite;
	public Sprite rightUpSprite;
	public Sprite leftUpSprite;

	public Animator animator;
	public bool isWalking;
	SpriteRenderer spriteRenderer;

	Vector3 startingPosition;
	Vector3 temp;
	float vertical;


	// Use this for initialization
	void Start () 
	{
		vertical = Input.GetAxis("Vertical"); 
		axisInputY = Input.GetAxis("Vertical");
		axisInputX = Input.GetAxis("Horizontal");
		animator = GetComponent<Animator>();
		temp = new Vector3(0,0,0);
		spriteRenderer = GetComponent<SpriteRenderer>();

	}
	// Update is called once per frame
	void Update () 
	{
		MoveMentTest();
		TimeTravel();
	}
	void TimeTravel()
	{

	}
	void MoveMentTest()
	{	
		if(Input.GetButtonDown("B"))
		{
			print("B Clicked");
		}
		if(Input.GetButtonDown("A"))
		{
			print("Joystick is X " + Input.GetAxis("Horizontal"));
			print("Joystick is Y " + Input.GetAxis("Vertical"));
		}
		if(Input.GetAxis("Vertical") >= 0.1 && Input.GetAxis("Vertical") <= 0.99 
		   && Input.GetAxis("Horizontal") >= 0.1 && Input.GetAxis("Horizontal") <= 0.99)//NE
		{
			print("NE");
			spriteRenderer.sprite = rightUpSprite;
			transform.Translate(Vector2.up * speed * Time.deltaTime);
			transform.Translate(Vector2.right * speed * Time.deltaTime);
		}
		if(Input.GetAxis("Horizontal") >= 0.1 && Input.GetAxis("Horizontal") <= 0.99 
		   && Input.GetAxis("Vertical") <= -0.1 && Input.GetAxis("Vertical") >= -0.99)//SE
		{
			print("SE");
			spriteRenderer.sprite = rightDownSprite;
			transform.Translate(-Vector2.up * speed * Time.deltaTime);
			transform.Translate(Vector2.right * speed * Time.deltaTime);
		}
		if(Input.GetAxis("Horizontal") <= -0.1 && Input.GetAxis("Horizontal") >= -0.99
		   && Input.GetAxis("Vertical") >= 0.1 && Input.GetAxis("Vertical") <=0.99)//NW
		{
			print("NW");
			spriteRenderer.sprite = leftUpSprite;
			transform.Translate(Vector2.up * speed * Time.deltaTime);
			transform.Translate(-Vector2.right * speed * Time.deltaTime);
		}
		if(Input.GetAxis("Horizontal") <=  -0.1 && Input.GetAxis("Horizontal") >= -0.99 
		   && Input.GetAxis("Vertical") <= -0.1 && Input.GetAxis("Vertical") >=   -0.99)//SW
		{
			print ("SW");
			spriteRenderer.sprite = leftDownSprite;
			transform.Translate(-Vector2.up * speed * Time.deltaTime);
			transform.Translate(-Vector2.right * speed * Time.deltaTime);
		}

		if (Input.GetAxis("Vertical") == -1)//south
		{
			//print(Input.GetAxis("Vertical"));
			//animator.SetBool("walk", true);
			spriteRenderer.sprite = downSprite;
			transform.Translate(-Vector2.up * speed * Time.deltaTime);
		}
		else
		{
			vertical = 0.0f;
			//animator.SetBool("walk", false);
		}
		if(Input.GetAxis("Vertical") == 1)//north
		{	
			//print(Input.GetAxis("Vertical"));
			//transform.Translate(Vector2.up * speed * Time.deltaTime);
			//animator.SetBool("walkUp",true);
			spriteRenderer.sprite = upSprite;
			transform.Translate(Vector2.up * speed * Time.deltaTime);
		}
		else
		{
			//animator.SetBool("walkUp",false);
		}
		if (Input.GetAxis("Horizontal") == 1)//east
		{
			//print(Input.GetAxis("Horizontal"));
			//animator.SetBool("walkRight", true);
			spriteRenderer.sprite = rightSprite;
			transform.Translate(Vector2.right * speed * Time.deltaTime);
		}
		else 
		{
			//animator.SetBool("walkRight", false);
		}
		if (Input.GetAxis("Horizontal") == -1)//west
		{
			//print(Input.GetAxis("Horizontal"));
			//animator.SetBool("walkLeft", true);
			spriteRenderer.sprite = leftSprite;
			transform.Translate(-Vector2.right * speed * Time.deltaTime);
		}
		else 
		{
			//animator.SetBool("walkLeft", false);
		}
	}
	void Movement()
	{
		if(Input.GetAxis("Horizontal") > 0.1)
		{

			float axisXHor = Input.GetAxis("Horizontal");
			transform.Translate(Vector2.right * speed * Time.deltaTime);
		    //animator.SetBool("walkRight",true);
		}
		else
		{
			//animator.SetBool("walkRight",false);
		}
		if(Input.GetAxis("Horizontal") < 0)
		{
			transform.Translate(-Vector2.right * speed * Time.deltaTime);
			//PlayerGO.GetComponent<SpriteRenderer>().sprite = leftSprite;
			//animator.SetBool("walkLeft",true);
		}
		else
		{
			//animator.SetBool("walkLeft",false);
		}
		if(Input.GetAxis("Vertical") > 0.1)
		{			
			transform.Translate(Vector2.up * speed * Time.deltaTime);
			//animator.SetBool("walkUp",true);
		}
		else
		{
			//animator.SetBool("walkUp",false);
		}
		if(Input.GetAxis("Vertical") < 0)
		{
			print("Vertical < 0");
			transform.Translate(-Vector2.up * speed * Time.deltaTime);
			//PlayerGO.GetComponent<SpriteRenderer>().sprite = downSprite;
			//animator.SetBool("walk",true);
			//transform.Translate(-Vector2.right * speed * Time.deltaTime);
			animator.SetBool("walk",true);
		}
		else
		{
			animator.SetBool("walk",false);
		}
		

//		if(Input.GetAxis("Vertical")  < 0.1)
//		{
//			
//
//		}
//		if(Input.GetAxis("Horizontal") )
//		{
//			transform.Translate(-Vector2.right * speed * Time.deltaTime);
//		}
//		if(Input.GetAxis("Horizontal") < 0.3)
//		{
//			transform.Translate(-Vector2.right * speed * Time.deltaTime);
//			transform.Translate(-Vector2.up * speed * Time.deltaTime);
//			
//		}

//		if(Input.GetAxisRaw("X axis")> 0.3 || Input.GetAxisRaw("X axis") < -0.3)
//		{
//			axisInputX = Input.GetAxisRaw("X axis");
//			//transform.Translate(Vector2.right * speed * Time.deltaTime);
//			//transform.Translate(Vector2.up * speed * Time.deltaTime);
//			
//		}
//		if(Input.GetAxisRaw("Y axis")> 0.3|| Input.GetAxisRaw("Y axis") < -0.3)
//		{
//			axisInputY = Input.GetAxisRaw("Y axis");
//			
//		}


//		if(Input.GetAxisRaw("Y axis")> 0.3 || Input.GetAxisRaw("Y axis") < -0.3)
//		{
//			
//				//transform.Translate(Vector2.up * speed * Time.deltaTime);
//				//transform.Translate(Vector2.right * speed * Time.deltaTime);
//			}
//		
//		if(Input.GetAxisRaw("X axis")> 0.3 || Input.GetAxisRaw("X axis") < -0.3)
//		{
//
//			//transform.Translate(Vector2.up * speed * Time.deltaTime);
//			//transform.Translate(Vector2.right * speed * Time.deltaTime);
//		}
//		if((axisInputY > 0.3 && axisInputY < -0.3) && (axisInputX >0.3 && axisInputX < 0.3))
//		{
//			transform.Translate(-Vector2.up * speed * Time.deltaTime);
//			transform.Translate(-Vector2.right * speed * Time.deltaTime);
//		}
		
		

		/*
		if(Input.GetAxis("DpadHorizontal")> 0.001 && Input.GetAxis("DpadHorizontal") < 0)
		{
			
			transform.Translate(Vector2.right * speed * Time.deltaTime);
			transform.Translate(Vector2.up * speed * Time.deltaTime);
		}
		
		if(Input.GetKey(KeyCode.E) || Input.GetButton("360AButton"))
		{
			
			transform.Translate(Vector2.right * speed * Time.deltaTime);
			transform.Translate(Vector2.up * speed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.Z)|| Input.GetButton("360BButton"))
		{
			
			transform.Translate(-Vector2.right * speed * Time.deltaTime);
			transform.Translate(-Vector2.up * speed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.Q)|| Input.GetButton("360XButton"))
		{
			
			transform.Translate(-Vector2.right * speed * Time.deltaTime);
			transform.Translate(Vector2.up * speed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.C)|| Input.GetButton("360YButton"))
		{
			
			transform.Translate(Vector2.right * speed * Time.deltaTime);
			transform.Translate(-Vector2.up * speed * Time.deltaTime);
		}
		*/
	}
}
