using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundEmitterPool", menuName = "Pool/SoundEmitter Pool")]
public class SO_SoundEmitterPool : SO_ComponentPool<SoundEmitter>
{
    [SerializeField] private SO_SoundEmitterFactory _factory;

    public override IFactory<SoundEmitter> Factory
    {
        get
        {
            return _factory;
        }
        set
        {
            _factory = value as SO_SoundEmitterFactory;
        }
    }
}
