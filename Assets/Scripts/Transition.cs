using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Transition : MonoBehaviour
{
    [SerializeField] 
    public GameObject mainCamera;
    //public GameObject startLevel;
    public List<GameObject> levels;
    public GameObject player;
    //How far the other level is spawned (to not see other levels during transitions)
    public float levelSpawnDistance = 45f;
    public float cameraTransitionDuration = 1f;
    public float playerLiftDuration = 1f;
    public float playerLiftHeight = 2f;
    private IEnumerator coroutine;
    GameObject currentLevel;
    Vector3 initialLevelPosition;
    bool initiated = false;
    int levelCounter = 0;
    CameraOrbit cameraOrbitScript;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = Instantiate(levels[0], levels[0].transform.position, Quaternion.identity);
        levelCounter++;
        initialLevelPosition = currentLevel.transform.position;
        cameraOrbitScript = mainCamera.GetComponent<CameraOrbit>();
    }

    public void TransitionLevel() {
        if (!initiated)
        {
            if(levelCounter == levels.Count) {
                Debug.Log("No more levels, initiate ending scene");
                return;
            }
            cameraOrbitScript.enabled = false;
            initiated = true;
            Vector3 instantiatePosition = new Vector3(initialLevelPosition.x+levelSpawnDistance,initialLevelPosition.y,initialLevelPosition.z);
            GameObject nextLevel = Instantiate(levels[levelCounter], instantiatePosition,Quaternion.identity);
            GameObject spawnPoint = nextLevel.transform.Find("TeleporterStart").gameObject; //Finding spawn point for next level

            //If the child was found.
            if (spawnPoint != null)
            {
                levelCounter++;
                coroutine = MoveLevel(currentLevel, nextLevel, spawnPoint);
                StartCoroutine(coroutine);

                currentLevel = nextLevel;
            }
            else {
                Debug.Log("Cannot find spawn point in Level " + levelCounter);
            }
        }
    }

    public void RestartLevel()
    {
        if (!initiated)
        {
            cameraOrbitScript.enabled = false;
            initiated = true;
            Vector3 instantiatePosition = new Vector3(initialLevelPosition.x+levelSpawnDistance,initialLevelPosition.y,initialLevelPosition.z);
            GameObject nextLevel = Instantiate(levels[levelCounter], instantiatePosition,Quaternion.identity);
            GameObject spawnPoint = nextLevel.transform.Find("SpawnPoint").gameObject; //Finding spawn point for next level

            //If the child was found.
            if (spawnPoint != null)
            {
                //levelCounter++;
                coroutine = MoveLevel(currentLevel, nextLevel, spawnPoint);
                StartCoroutine(coroutine);

                currentLevel = nextLevel;
            }
            else {
                Debug.Log("Cannot find spawn point in Level " + levelCounter);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")){
            TransitionLevel();
            //RestartLevel();
        }
    }
    IEnumerator MoveLevel(GameObject deactivateLevel, GameObject moveLevelToIdentity, GameObject playerSpawnPoint)
    {
        player.GetComponent<NavMeshAgent>().enabled = false;
        //Lift player up
        Vector3 playerNewPosition = new Vector3(player.transform.position.x, player.transform.position.y + playerLiftHeight, player.transform.position.z);
        float elapsedTime = 0;
        while (elapsedTime < playerLiftDuration)
        {
        player.transform.position = Vector3.Lerp(player.transform.position, playerNewPosition, (elapsedTime / playerLiftDuration));
        elapsedTime += Time.deltaTime;
        yield return null;
        }

        //Move player and level 
        Vector3 cameraNewPosition = new Vector3(mainCamera.transform.position.x + levelSpawnDistance, mainCamera.transform.position.y, mainCamera.transform.position.z);
        playerNewPosition = new Vector3(playerSpawnPoint.transform.position.x, player.transform.position.y, playerSpawnPoint.transform.position.z); //Lerp to spawn point
        elapsedTime = 0;
        while (elapsedTime < cameraTransitionDuration)
        {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraNewPosition, (elapsedTime / cameraTransitionDuration));
        player.transform.position = Vector3.Lerp(player.transform.position, playerNewPosition, (elapsedTime / cameraTransitionDuration));
        elapsedTime += Time.deltaTime;
        yield return null;
        }

        //Cleanup scene and return scene to zero vector
        Destroy(deactivateLevel);
        moveLevelToIdentity.transform.position = initialLevelPosition;
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x-levelSpawnDistance, mainCamera.transform.position.y, mainCamera.transform.position.z);
        player.transform.position = new Vector3(player.transform.position.x-levelSpawnDistance, player.transform.position.y, player.transform.position.z);
        NavMesh.AddNavMeshData(new NavMeshData());

        //Move player down
        playerNewPosition = new Vector3(player.transform.position.x, player.transform.position.y - playerLiftHeight, player.transform.position.z);
        elapsedTime = 0;
        while (elapsedTime < playerLiftDuration)
        {
        player.transform.position = Vector3.Lerp(player.transform.position, playerNewPosition, (elapsedTime / playerLiftDuration));
        elapsedTime += Time.deltaTime;
        yield return null;
        }

        cameraOrbitScript.enabled = true;
        initiated = false;
        yield return null;
        player.GetComponent<NavMeshAgent>().enabled = true;
    }
}
