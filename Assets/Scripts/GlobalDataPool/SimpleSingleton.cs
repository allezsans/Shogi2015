using UnityEngine;
using System.Collections;

public abstract class SimpleSingleton<T> where T : class,new(){
	protected static T m_Instance;
	public static T Instance{
		get {
			if (m_Instance == null) m_Instance = new T();
			return m_Instance;
		}
	}
}
