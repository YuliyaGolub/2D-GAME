using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SoundData", menuName = "Scriptable Object/Sound Data"), Serializable]
public class SoundData : ScriptableObject
{
    public List<SoundGroup> soundGroups = new List<SoundGroup>();

    [Serializable]
    public class SoundGroup
    {
        public AudioClip[] sounds;
        public string name;
        public int volume = 1;
        public int pitch = 1;
    }
}
