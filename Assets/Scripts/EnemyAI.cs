using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;

    public void SetTarget(Vector3 point)
    {
        agent.SetDestination(point);
    }

	IEnumerator Start()
	{
		agent = GetComponent<NavMeshAgent>();
		while(true)
		{
			if(agent.isOnOffMeshLink)
			{
				yield return StartCoroutine(Parabola(agent, 2.0f, 0.5f));
				agent.CompleteOffMeshLink();
			}

			yield return null;
		}


	}

	IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
	{
		OffMeshLinkData data = agent.currentOffMeshLinkData;
		Vector3 startPos = agent.transform.position;
		Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
		float timeCounter = 0.0f;
		while(timeCounter < 1.0f)
		{
			float yOffset = height * 4.0f * (timeCounter - timeCounter * timeCounter);
			agent.transform.position = Vector3.Lerp(startPos, endPos, timeCounter) + yOffset * Vector3.up;
			timeCounter += Time.deltaTime / duration;
			yield return null;
		}
	}
}
