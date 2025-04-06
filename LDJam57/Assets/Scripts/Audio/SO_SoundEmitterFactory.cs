using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundEmitterFactory", menuName = "Factory/SoundEmitter Factory")]
public class SO_SoundEmitterFactory : SO_Factory<SoundEmitter>
{
    public SoundEmitter prefab = default;

    public override SoundEmitter Create()
    {
        return Instantiate(prefab);
    }
}
