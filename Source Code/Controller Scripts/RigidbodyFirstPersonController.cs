using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace UnityStandardAssets.Characters.FirstPerson // Unity and NDS8
{
        // Unity
    [RequireComponent(typeof (Rigidbody))]
    [RequireComponent(typeof (CapsuleCollider))]
    public class RigidbodyFirstPersonController : MonoBehaviour
    {
        [Serializable]
        public class MovementSettings
        {

            public float ForwardSpeed = 2.0f;   // Speed when walking forward
            public float BackwardSpeed = 2.0f;  // Speed when walking backwards
            public float StrafeSpeed = 2.0f;    // Speed when walking sideways
            public float RunMultiplier = 1.5f;   // Speed when sprinting edited by NDS8
            public float SneakMultiplier = 0.5f;   // Speed when sneaking created by NDS8
            public KeyCode RunKey = KeyCode.LeftShift; 
            public KeyCode SneakKey = KeyCode.LeftControl; // by NDS8
            public KeyCode RestartKey = KeyCode.Escape; // by NDS8
            public float JumpForce = 15f;
            public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));
            [HideInInspector] public float CurrentTargetSpeed = 4f;

#if !MOBILE_INPUT
            private bool m_NotWalking;
#endif
            public void UpdateDesiredTargetSpeed(Vector2 input)
            {
                if (input == Vector2.zero)
                {
                }
				if (input.x > 0 || input.x < 0)
				{
					//strafe
					CurrentTargetSpeed = StrafeSpeed;
				}
				if (input.y < 0)
				{
					//backwards
					CurrentTargetSpeed = BackwardSpeed;
                }
				if (input.y > 0)
				{
					//forwards
					//handled last as if strafing and moving forward at the same time forwards speed should take precedence
					CurrentTargetSpeed = ForwardSpeed;
                }
#if !MOBILE_INPUT
	            if (Input.GetKey(RunKey)) // Check if player is pressing the Run Key
	            {
		            CurrentTargetSpeed *= RunMultiplier;
		            m_NotWalking = true; // by NDS8
                }
                else // by NDS8
                {
		            m_NotWalking = false; // by NDS8
                }
#endif

#if !MOBILE_INPUT
                if (Input.GetKey(SneakKey)) // Check if player is pressing the Sneak Key by NDS8
                {
                    CurrentTargetSpeed *= SneakMultiplier;
                    m_NotWalking = true;
                }
                else // by NDS8
                {
                    m_NotWalking = false;
                }
#endif
#if !MOBILE_INPUT
                if (Input.GetKey(RestartKey)) // by NDS8
                {
                    Debug.Log("Going to Main Menu");
                    SceneManager.LoadScene("Main Menu");
                }
#endif
            }

#if !MOBILE_INPUT
            public bool Running
            {
                get { return m_NotWalking; }
            }
#endif
        }


        [Serializable]
        public class AdvancedSettings
        {
            public float groundCheckDistance = 0.01f; // distance for checking if the controller is grounded ( 0.01f seems to work best for this )
            public float stickToGroundHelperDistance = 0.5f; // stops the character
            public float slowDownRate = 50f; // rate at which the controller comes to a stop when there is no input
            public bool airControl; // can the user control the direction that is being moved in the air
            [Tooltip("set it to 0.1 or more if you get stuck in wall")]
            public float shellOffset; //reduce the radius by that ratio to avoid getting stuck in wall (a value of 0.1f is nice)
        }


        public Camera cam;
        public GameObject lighter; // by NDS8
        public bool lightOn; // by NDS8
        public MovementSettings movementSettings = new MovementSettings();
        public MouseLook mouseLook = new MouseLook();
        public AdvancedSettings advancedSettings = new AdvancedSettings();
        public bool isActive;
        public GameObject inv; // by NDS8

        private Rigidbody m_RigidBody;
        private CapsuleCollider m_Capsule;
        private float m_YRotation;
        private Vector3 m_GroundContactNormal;
        private bool m_Jump, m_PreviouslyGrounded, m_Jumping, m_IsGrounded;

        public GameObject sleepBarc0;
        public GameObject sleepBarc1;
        public GameObject sleepBarc2;
        public GameObject sleepBarc3;
        public GameObject sleepBarc4;
        public GameObject sleepBarc5;
        public GameObject sleepBarc6;
        public GameObject sleepBarc7;
        public GameObject sleepBarc8;
        public GameObject sleepBarc9;
        public List<GameObject> sleepBar;
        public GameObject lightBarc0;
        public GameObject lightBarc1;
        public GameObject lightBarc2;
        public GameObject lightBarc3;
        public GameObject lightBarc4;
        public GameObject lightBarc5;
        public GameObject lightBarc6;
        public GameObject lightBarc7;
        public GameObject lightBarc8;
        public GameObject lightBarc9;
        public List<GameObject> lightBar;
        Ray r; // by NDS8
        RaycastHit hit; // by NDS8


        public Vector3 Velocity
        {
            get { return m_RigidBody.velocity; }
        }

        public bool Grounded
        {
            get { return m_IsGrounded; }
        }

        public bool Jumping
        {
            get { return m_Jumping; }
        }

        public bool Running
        {
            get
            {
 #if !MOBILE_INPUT
				return movementSettings.Running;
#else
	            return false;
#endif
            }
        }


        private void Start() //Establish all Variables
        {
            sleepBar = new List<GameObject>(); // by NDS8
            lightBar = new List<GameObject>(); // by NDS8
            m_RigidBody = GetComponent<Rigidbody>(); // by NDS8
            m_Capsule = GetComponent<CapsuleCollider>();
            mouseLook.Init (transform, cam.transform);
            lighter.SetActive(true); // by NDS8
            lightOn = true; // by NDS8
            isActive = true; // by NDS8
            sleepBar.Add(sleepBarc0); // All Followed Bars by JB2051
            sleepBar.Add(sleepBarc1);
            sleepBar.Add(sleepBarc2);
            sleepBar.Add(sleepBarc3);
            sleepBar.Add(sleepBarc4);
            sleepBar.Add(sleepBarc5);
            sleepBar.Add(sleepBarc6);
            sleepBar.Add(sleepBarc7);
            sleepBar.Add(sleepBarc8);
            sleepBar.Add(sleepBarc9);
            lightBar.Add(lightBarc0);
            lightBar.Add(lightBarc1);
            lightBar.Add(lightBarc2);
            lightBar.Add(lightBarc3);
            lightBar.Add(lightBarc4);
            lightBar.Add(lightBarc5);
            lightBar.Add(lightBarc6);
            lightBar.Add(lightBarc7);
            lightBar.Add(lightBarc8);
            lightBar.Add(lightBarc9);
            foreach(GameObject a in sleepBar) // Setting Fatigue UI by NDS8
            {
                a.SetActive(true);
            }
            foreach (GameObject a in lightBar) // Setting Fuel UI by NDS8
            {
                a.SetActive(true);
            }
        }


        private void Update() // Run every frame.
        {
            if (!isActive)
            {

            }
            else
            {
                InteractCast(); // Check if Interacting by NDS8
                RotateView(); 
                if (System.DateTime.Now.Second == timeSet) // Decrease Fuel over time by NDS8
                {
                    DecreaseBars();
                }
                if (CrossPlatformInputManager.GetButtonDown("Light") && lightFuel > 0) // Allow Lighter to turn on and off and produce light when on by NDS8
                {
                    lightOn = !lightOn;
                    lighter.SetActive(lightOn);
                }
                else if (lightFuel <= 0) // Check if Lighter has run out of fuel by NDS8
                {
                    lightOn = false;
                    lighter.SetActive(false);
                }

                if (CrossPlatformInputManager.GetButtonDown("Jump") && !m_Jump) // Make sure Jump is possible, Unity and NDS8 (Bug Fixes) 
                {
                    m_Jump = true;
                }
            }
        }


        private void FixedUpdate()
        {
            if (!isActive) {
                mouseLook.SetCursorLock(false);
                Cursor.visible = true;
                movementSettings.CurrentTargetSpeed = 0;
                m_RigidBody.drag = 5f;
            }
            else
            {
                mouseLook.SetCursorLock(true);
                Cursor.visible = false;
                GroundCheck();
                Vector2 input = GetInput();

                if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) && (advancedSettings.airControl || m_IsGrounded))
                {
                    // always move along the camera forward as it is the direction that it being aimed at
                    Vector3 desiredMove = cam.transform.forward * input.y + cam.transform.right * input.x;
                    desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized;

                    desiredMove.x = desiredMove.x * movementSettings.CurrentTargetSpeed;
                    desiredMove.z = desiredMove.z * movementSettings.CurrentTargetSpeed;
                    desiredMove.y = desiredMove.y * movementSettings.CurrentTargetSpeed;
                    if (m_RigidBody.velocity.sqrMagnitude <
                        (movementSettings.CurrentTargetSpeed * movementSettings.CurrentTargetSpeed))
                    {
                        m_RigidBody.AddForce(desiredMove * SlopeMultiplier(), ForceMode.Impulse);
                    }
                }

                if (m_IsGrounded)
                {
                    m_RigidBody.drag = 5f;

                    if (m_Jump)
                    {
                        m_RigidBody.drag = 0f;
                        m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, 0f, m_RigidBody.velocity.z);
                        m_RigidBody.AddForce(new Vector3(0f, movementSettings.JumpForce, 0f), ForceMode.Impulse);
                        m_Jumping = true;
                    }

                    if (!m_Jumping && Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon && m_RigidBody.velocity.magnitude < 1f)
                    {
                        m_RigidBody.Sleep();
                    }
                }
                else
                {
                    m_RigidBody.drag = 0f;
                    if (m_PreviouslyGrounded && !m_Jumping)
                    {
                        StickToGroundHelper();
                    }
                }
                m_Jump = false;
            }
        }

        public void setActive(bool active)
        {
            isActive = active;
        }

        private float SlopeMultiplier()
        {
            float angle = Vector3.Angle(m_GroundContactNormal, Vector3.up);
            return movementSettings.SlopeCurveModifier.Evaluate(angle);
        }


        private void StickToGroundHelper()
        {
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                                   ((m_Capsule.height/2f) - m_Capsule.radius) +
                                   advancedSettings.stickToGroundHelperDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f)
                {
                    m_RigidBody.velocity = Vector3.ProjectOnPlane(m_RigidBody.velocity, hitInfo.normal);
                }
            }
        }


        private Vector2 GetInput()
        {
            
            Vector2 input = new Vector2
                {
                    x = CrossPlatformInputManager.GetAxis("Horizontal"),
                    y = CrossPlatformInputManager.GetAxis("Vertical")
                };
			movementSettings.UpdateDesiredTargetSpeed(input);
            return input;
        }


        private void RotateView()
        {
            //avoids the mouse looking if the game is effectively paused
            if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

            // get the rotation before it's changed
            float oldYRotation = transform.eulerAngles.y;

            mouseLook.LookRotation (transform, cam.transform);

            if (m_IsGrounded || advancedSettings.airControl)
            {
                // Rotate the rigidbody velocity to match the new direction that the character is looking
                Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
                m_RigidBody.velocity = velRotation*m_RigidBody.velocity;
            }
        }

        /// sphere cast down just beyond the bottom of the capsule to see if the capsule is colliding round the bottom
        private void GroundCheck()
        {
            m_PreviouslyGrounded = m_IsGrounded;
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                                   ((m_Capsule.height/2f) - m_Capsule.radius) + advancedSettings.groundCheckDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                m_IsGrounded = true;
                m_GroundContactNormal = hitInfo.normal;
            }
            else
            {
                m_IsGrounded = false;
                m_GroundContactNormal = Vector3.up;
            }
            if (!m_PreviouslyGrounded && m_IsGrounded && m_Jumping)
            {
                m_Jumping = false;
            }
        }

        public float timeSet = System.DateTime.Now.Second + 20f; //Target for next check // by NDS8
        public float fatigue = 0.2f; //Current fatigue // by NDS8
        public float lightFuel = 20f; //Current fuel in lighter // by NDS8
        public Vector2 fPos = new Vector2(20, 40); //Fatigue bar pos // by NDS8
        public Vector2 lPos = new Vector2(20, 80); //LightFual bar pos // by NDS8
        public Vector2 size = new Vector2(200, 25); //Size of bars // by NDS8
        public Texture2D emptyTexture; // by NDS8
        public Texture2D fullTexture; // by NDS8

        void OnGUI() // by JB2051
        {
            // fatigue background   
            //   GUI.BeginGroup(new Rect(Screen.width - fPos.x, Screen.height - fPos.y, size.x, size.y));
            //    GUI.Box(new Rect(0, 0, size.x, size.y), emptyTexture);

            // fatigue bar
            //    GUI.BeginGroup(new Rect(0, 0, size.x * fatigue, size.y));
            //   GUI.Box(new Rect(0, 0, size.x, size.y), fullTexture);
            //   GUI.EndGroup();

            //   GUI.EndGroup();

            // lightFuel background   
            //   GUI.BeginGroup(new Rect(Screen.width - lPos.x, Screen.height - lPos.y, size.x, size.y));
            //    GUI.Box(new Rect(0, 0, size.x, size.y), emptyTexture);

            // lightFuel bar
            //     GUI.BeginGroup(new Rect(0, 0, size.x * lightFuel, size.y));
            //    GUI.Box(new Rect(0, 0, size.x, size.y), fullTexture);
            //     GUI.EndGroup();

            //   GUI.EndGroup();

            foreach (GameObject a in sleepBar)
            {
                a.SetActive(false);
            }
            foreach (GameObject a in lightBar)
            {
                a.SetActive(false);
            }
            /*for(int i = 1; i <= 10; i++)
            {
                if(i < (Math.Round(lightFuel * 10)))
                {
                    lightBar[i-1].SetActive(true);
                }
            }
            for (int i = 1; i <= 10; i++)
            {
                if (i < (Math.Round(fatigue * 10)))
                {
                    sleepBar[i-1].SetActive(true);
                }
            }*/

        }

        void DecreaseBars () // Decrease the fuel after 20 seconds by NDS8
        {
            timeSet = System.DateTime.Now.Second + 20f;
            if(timeSet > 59)
            {
                timeSet = (timeSet - 60f);
            }
            fatigue = (fatigue - 0.05f);
            if (lightOn == true)
            {
                lightFuel = (lightFuel - 0.1f);
            }
        }

        void InteractCast() // Check if an interactable object is 2 meters and touching the crosshair by NDS8
        {
            r = Camera.main.ScreenPointToRay(Input.mousePosition); // Fire raycast out of crosshair
            if (Physics.Raycast(r, out hit, 3f))
            {
                if (Input.GetKey(KeyCode.Mouse0) && hit.collider.gameObject.tag == "Interactable")
                {
                    //Debug.Log("Raycast Hit: " + hit.collider.gameObject.name);        // Test what object the ray has hit
                    hit.collider.gameObject.GetComponent<Interactable>().clickedOn();       // Activate that object's interactable script
                }
            }
        }

        /* Test Script for Spawning a Prefab with a button by NDS8
        public Rigidbody prefab;

        void CreatePrefab()
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                //Debug.Log("TestFire");
                // Instantiate the projectile at the position and rotation of this transform
                Rigidbody clone;
                clone = Instantiate(prefab, Camera.main.transform.position, Camera.main.transform.rotation);

                // Give the cloned object an initial velocity along the current
                // object's Z axis
                clone.velocity = transform.TransformDirection(Vector3.forward * 10);
            }
        }
        */
    }
}
