using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Weapon
{
    
    
    private void OnEnable()
    {
        UIManager.instance.UpdateUI(UIManager.instance._ammoText, "");
        UIManager.instance.UpdateUI(UIManager.instance._reserveText, "");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitShield()
    {
        AudioManager.PlaySound(AudioManager.instance.ThudSFX, .25f);
        ModelRecoil();
    }
}
