using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : SingletonMono<SoundManager>
{
    public enum SoundId
    {
        Fire,
        Explosion,
        Hit,
        Build,
        Sell,
        SkillCast,
        SkillImpact,
        Win,
        Lose,
        Click,
        StartWave,
        Music,
        BaseHit
    }

    [System.Serializable]
    public class SoundEntry { public SoundId id; public AudioClip clip; public float volume = 1f; }

    [SerializeField] private List<SoundEntry> sounds = new List<SoundEntry>();

    private Dictionary<SoundId, SoundEntry> table;
    private AudioSource source;
    private AudioSource musicSource;

    protected override void Awake()
    {
        base.Awake();
        source = GetComponent<AudioSource>();
        if (source == null) source = gameObject.AddComponent<AudioSource>();
        table = new Dictionary<SoundId, SoundEntry>();
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        foreach (var s in sounds)
        {
            if (s == null || s.clip == null) continue;
            if (!table.ContainsKey(s.id)) table[s.id] = s;
        }

        if (table.TryGetValue(SoundId.Music, out var musicEntry) && musicEntry.clip != null)
        {
            musicSource.clip = musicEntry.clip;
            musicSource.volume = musicEntry.volume;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void Play(SoundId id, float volume = 1f)
    {
        if (table == null) return;
        if (table.TryGetValue(id, out var entry) && entry.clip != null)
        {
            source.PlayOneShot(entry.clip, volume * entry.volume);
        }
        else
        {
            Debug.LogWarning($"SoundManager: sound not found '{id}'");
        }
    }

    public void PlayMusic(SoundId id)
    {
        if (table == null || musicSource == null) return;
        if (table.TryGetValue(id, out var entry) && entry.clip != null)
        {
            musicSource.clip = entry.clip;
            musicSource.volume = entry.volume;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource == null) return;
        if (musicSource.isPlaying) musicSource.Stop();
    }

}
