using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntRange : MonoBehaviour {

    public int m_min, m_max;

    public IntRange(int min, int max)
    {
        m_min = min;
        m_max = max;
    }

	public int Random
    {
        get { return UnityEngine.Random.Range(m_min, m_max); }
    }
}
