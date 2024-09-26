using System.Collections.Generic;
using static SoundData;

public class SoundRepository
{
    private SoundData soundData;
    public SoundRepository(SoundData soundData)
    {
        this.soundData = soundData;
    }

    public SoundGroup GetSoundGroup(string soundGroupName)
    {
        foreach (SoundGroup soundGroup in soundData.soundGroups)
        {
            if (soundGroup.name == soundGroupName)
                return soundGroup;
        }
        throw new KeyNotFoundException($"Sound group '{soundGroupName}' not found");
    }
}
