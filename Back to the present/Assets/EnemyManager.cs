using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<AIController> Enemies;
    public static EnemyManager Instance;
    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(this);
    }
    public void Update()
    {
        int positionIndex = 0;
        List<Vector3> availiablePositions = GetPositionsAround(PlayerController.instance.transform.position, 2f, 20);
        foreach (AIController controller in Enemies)
        {
            if (controller.isPlayerSpotted)
            {
                controller.Target = availiablePositions[positionIndex];
                positionIndex++;
            }
        }
    }
    public TimeTravel[] GetEnemiesTimeTravel()
    {
        List<TimeTravel> returnList = new List<TimeTravel>();
        foreach (AIController controller in Enemies)
        {
            returnList.Add(controller.AiTimeTravel);
        }
        return returnList.ToArray();
    }
    public List<Vector3> GetPositionsAround(Vector3 center, float distance, int positionCount)
    {
        List<Vector3> positionList = new List<Vector3>();
        for (int i = 0; i < positionCount; i++)
        {
            int angle = i * (360 / positionCount);
            Vector3 direction = Quaternion.Euler(0, 0, angle) * new Vector3(0, 1, 0);
            Vector3 position = center + direction * distance;
            positionList.Add(position);
        }
        return positionList;
    }
}
