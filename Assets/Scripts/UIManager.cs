using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] public Volume volume => gameObject.GetComponent<Volume>();
    private Vignette vignette;
    public static UIManager singleton { get; private set; }
    void Start()
    {
        singleton = this;
    }

    public IEnumerator ChangeVignetteState(float time, bool state)
    {
        if (singleton.volume.profile.TryGet(out singleton.vignette))
        {
            singleton.vignette.active = state;
            if (time != 0)
            {
                yield return new WaitForSeconds(time);
                singleton.vignette.active = !state;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
