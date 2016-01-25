using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to uses
        bool turbo = false;
        public GameObject NOS;
        public GameObject OverLoad;
        public GameObject car;
        AudioSource source;
        public AudioClip nosClip;
        public AudioClip overloadClip;
        bool overload;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
            source = this.GetComponent<AudioSource>();
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
            

            if (Input.GetButtonDown("Fire3") && turbo)
            {
                m_Car.m_FullTorqueOverAllWheels = 500000;
                StartCoroutine(turboTime());
            }
            if (Input.GetButtonDown("Fire3") && overload)
            {
                GetComponent<ProgressBar>().overload();
                source.PlayOneShot(overloadClip);
                OverLoad.SetActive(false);
            }
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "itemBox")
            {
                int ran = UnityEngine.Random.Range(0, 2);
                switch (ran)
                {
                    case 0:
                        turbo = true;
                        source.PlayOneShot(nosClip);
                        NOS.SetActive(true);
                        Destroy(other.gameObject);
                        break;
                    case 1:
                        overload = true;
                        source.PlayOneShot(overloadClip);
                        OverLoad.SetActive(true);
                        Destroy(other.gameObject);
                        break;
                }
               
            }
            if(other.gameObject.tag == "cloud")
            {
                GetComponent<ProgressBar>().cloudRefuel = false;
				other.gameObject.GetComponent<Animator>().enabled = false;
				float x = car.transform.position.x;
				float y = other.transform.position.y;
				float z = car.transform.position.z;
				other.transform.position = new Vector3(x,y,z);
                other.transform.parent = car.transform;
				StartCoroutine(cloudWait(other));
            }
        }

        IEnumerator turboTime()
        {
            NOS.SetActive(false);
            yield return new WaitForSeconds(5f);
            m_Car.m_FullTorqueOverAllWheels = 2500;
            turbo = false;
        }


        IEnumerator cloudWait(Collider other)
        {
            yield return new WaitForSeconds(10f);
            GetComponent<ProgressBar>().cloudRefuel = true;
            Destroy(other.gameObject);
        }
    }
}
