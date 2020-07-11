using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public GameObject mainCamera;
    public List<GameObject> levels;
    public GameObject startLevel;

    GameObject currentLevel;
    public float cameraTransitionDuration = 0.5f;

    private IEnumerator coroutine;

    public GameObject player;

    public float levelSpawnDistance = 15f;

    Vector3 initialLevelPosition;

    bool initiated = false;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = startLevel;
        initialLevelPosition = startLevel.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && !initiated)
        {
            initiated = true;
            Vector3 instantiatePosition = new Vector3(initialLevelPosition.x+levelSpawnDistance,initialLevelPosition.y,initialLevelPosition.z);
            GameObject nextLevel = Instantiate(levels[1], instantiatePosition,Quaternion.identity);

            coroutine = MoveToSpot(currentLevel, nextLevel);
            StartCoroutine(coroutine);

            currentLevel = nextLevel;
            initiated = false;
        }
    }
    IEnumerator MoveToSpot(GameObject deactivateLevel, GameObject moveLevelToIdentity)
     {
        Vector3 cameraNewPosition = new Vector3(mainCamera.transform.position.x+levelSpawnDistance, mainCamera.transform.position.y, mainCamera.transform.position.z);
        Vector3 playerNewPosition = new Vector3(player.transform.position.x+levelSpawnDistance, player.transform.position.y, player.transform.position.z);
        float elapsedTime = 0;

        while (elapsedTime < cameraTransitionDuration)
        {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraNewPosition, (elapsedTime / cameraTransitionDuration));
        player.transform.position = Vector3.Lerp(player.transform.position, playerNewPosition, (elapsedTime / cameraTransitionDuration));
        elapsedTime += Time.deltaTime;
        yield return null;
        }

        Destroy(deactivateLevel);
        moveLevelToIdentity.transform.position = initialLevelPosition;
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x-levelSpawnDistance, mainCamera.transform.position.y, mainCamera.transform.position.z);
        player.transform.position = new Vector3(player.transform.position.x-levelSpawnDistance, player.transform.position.y, player.transform.position.z);
        yield return null;
     }
}
