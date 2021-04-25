using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Actor.EnemySystems;

namespace Evacuation.Level.SpawnManagement
{
    public class SpawnPositioner : MonoBehaviour
    {
        // Inspector Accessible Fields
        [SerializeField] protected SpawnPattern[] electedPatterns;
        [Space]
        [SerializeField] private float patternRuntimeLength;
        [SerializeField] protected SpawnPattern defaultPattern;

        // Fields
        protected SimpleTimer patternTimer;
        protected SpawnPattern selectedPattern;
        protected float screenWidth, screenHeight;

        public void InitialisePositioner()
        {
            patternTimer = new SimpleTimer(patternRuntimeLength, Time.deltaTime);
            screenWidth = (float)Screen.width;
            screenHeight = (float)Screen.height;
        }

        private void FixedUpdate()
        {
            //print(Camera.main.orthographicSize);
            //Debug.Log(Screen.height * 1.0 / Screen.width * 1.0);

            //float width = Camera.main.orthographicSize * Camera.main.aspect;

           // Debug.DrawLine(Camera.main.transform.position, new Vector3(width, 0) + Camera.main.transform.position);
            //Debug.DrawLine(Camera.main.transform.position, new Vector3(0, (width) * (float)(Screen.height * 1.0 / Screen.width * 1.0), 0) + Camera.main.transform.position, Color.blue);
            //patternTimer.TickTimer();
        }

        public void PositionEntity(GameObject spawnedEntity, IStatePatternSetter patternSetter)
        {
            Vector3 newPos = DeterminePosition();
            spawnedEntity.transform.position = newPos;
        }

        private void SelectPattern()
        {
            if (!patternTimer.CheckTimeIsUp()) return;
            
            selectedPattern = electedPatterns[Random.Range(0, electedPatterns.Length)];
            patternTimer.ResetTimer();
        }

        private Vector3 DeterminePosition()
        {
            bool willSpawnRight = Random.Range(0, 100) >= 50;
            Camera cameraMain = Camera.main;
            float width = Camera.main.orthographicSize * Camera.main.aspect;
            float height = width * (float)(Screen.height * 1.0 / Screen.width * 1.0);

            if (willSpawnRight)
            {
                return new Vector3(width + Random.Range(1, 10), height + Random.Range(1, 10)) + Camera.main.transform.position;
            } else
            {
                return new Vector3(height + Random.Range(1, 10), height + Random.Range(1, 10)) - Camera.main.transform.position;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(new Vector2(Camera.main.orthographicSize, 0), Vector3.one * 10f);
        }

    }

    public enum SpawnPattern
    {
        LinearTravel,
        TeleportInward,
        TranslateInwards,
        FollowIn,
        Scatter,
        DiveBomb,
    }
}
