using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player {

    public class PlayerController : MonoBehaviour
    {


        [Header("Control Settings")]
        [SerializeField] private    float       moveXSpeed          = 500f;
        [SerializeField] public     float       moveYSpeed          = 130f;
        private float dragDirection;
        
        [Header("Rotate Settings")]
        [SerializeField] private bool           rotate;
        [SerializeField] private float          rotationSpeed;




        [Header("Jump Settings")]
        [SerializeField] private float reflectionForce = 10f; // 튀어오를 힘의 크기
        [SerializeField] private float reflectionDuration = 1f; // 튀어오르는 시간
        private bool isReflecting = false; // 반사 중인지 여부

        #region singleton
        public static PlayerController instance = null;
        private void Awake()
        {
            if(instance != null)
            {
                Destroy(this.gameObject);
            }    
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion    

        void Start()
        {        
        }

        void Update()
        {

            // 위아래 이동 
            if (!isReflecting)
            {
                // 아래로 떨어짐 
                float moveY = moveYSpeed * Time.fixedDeltaTime;
                transform.Translate(Vector3.down * moveY);
            }                

            // 회전 
            if (rotate)
                transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
            


            // 드래그하여 좌우 이동 
            if (Input.GetMouseButton(0))
            {
                dragDirection = Input.GetAxis("Mouse X");

                float moveX = dragDirection * moveXSpeed * Time.fixedDeltaTime;
                transform.Translate(Vector3.right * moveX, Space.World);
            }
        
        }


        public IEnumerator ReflectAndBounce()
        {
            isReflecting = true;

            // 드릴을 반사 방향으로 이동
            float elapsedTime = 0f;
            while (elapsedTime < reflectionDuration)
            {
                float force = reflectionForce * (1f - elapsedTime / reflectionDuration);
                transform.Translate(Vector3.up * force * Time.deltaTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            isReflecting = false;
        }

        public IEnumerator Drilling()
        {
            moveYSpeed /= 2;
            
            yield return new WaitForSeconds(1f);

            moveYSpeed *= 2;
        }
    }

   

}
