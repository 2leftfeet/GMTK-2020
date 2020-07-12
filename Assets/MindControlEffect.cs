using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MindControlEffect : MonoBehaviour
{
    [SerializeField] MindControlController mindController;
    [SerializeField] VolumeProfile volumeProfile;

    [SerializeField] float chromaticChange = 0.5f;
    [SerializeField] float grainChange = 1f;

    UnityEngine.Rendering.Universal.ChromaticAberration chromaticAberration;
    UnityEngine.Rendering.Universal.FilmGrain filmGrain;
    void Start()
    {
        
    }
    public void ControlEffect()
    {
        if(!volumeProfile.TryGet(out chromaticAberration)) throw new System.NullReferenceException(nameof(chromaticAberration));
        chromaticAberration.intensity.Override(chromaticChange);
        if(!volumeProfile.TryGet(out filmGrain)) throw new System.NullReferenceException(nameof(chromaticAberration));
        filmGrain.intensity.Override(grainChange);
    }

    public void ControlEffectEnd()
    {
        if(!volumeProfile.TryGet(out chromaticAberration)) throw new System.NullReferenceException(nameof(chromaticAberration));
        chromaticAberration.intensity.Override(0f);
        if(!volumeProfile.TryGet(out filmGrain)) throw new System.NullReferenceException(nameof(chromaticAberration));
        filmGrain.intensity.Override(0.1f);
    }
}
