using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

	public float lifetime = 2.0f;

    void OnEnable()
    {
        StartCoroutine(Recycle());
    }
    IEnumerator Recycle()
    {
        yield return new WaitForSeconds(lifetime);
        ObjectPool.Instance.RecycleObj(this.gameObject);
    }
    void OnDestroy()
    {
        ObjectPool.Instance.Remove(this.gameObject);
    }
}
