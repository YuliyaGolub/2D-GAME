using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static SoundData;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private SoundData soundData;
    private SoundRepository soundRepository;

    private static SoundsManager instance;
    public static SoundsManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<SoundsManager>();
            return instance;
        }
    }
    void Start()
    {
        soundRepository = new SoundRepository(soundData);
    }

    public void PlaySound(string soundGroupName)
    {
        SoundGroup soundGroup = soundRepository.GetSoundGroup(soundGroupName);
        int randomSoundIndex = Random.Range(0, soundGroup.sounds.Length);
        AudioSource.PlayClipAtPoint(soundGroup.sounds[randomSoundIndex], transform.position, soundGroup.volume);
    }

    public void PlayGameOverSound()
    {
        PlaySound("GameOver"); 
    }
    public void PlayerEnemyClashSound()
    {
        PlaySound("PlayerEnemyClash"); 
    }
    public void PlayItemCollectSound()
    {
        PlaySound("ItemCollect");
    }
    public void PlayKillEnemySound()
    {
        PlaySound("KillEnemy");
    }
}
