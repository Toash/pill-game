using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance = null;

    public static UIManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(UIManager)) as UIManager;
            }

            return _instance;
        }
    }
    
    
    public Text _healthText;
    public Text _ammoText;
    public Text _reserveText;
    public Text _pickupText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI(Text textToUpdate, string stringToUpdate)
    {
        textToUpdate.text = stringToUpdate;
    }

    public void UpdatePickupText(string stringToUpdate)
    {
        StopAllCoroutines();
        StartCoroutine(UpdatePickupTextCoroutine(stringToUpdate));
    }

    private IEnumerator UpdatePickupTextCoroutine(string stringToUpdate)
    {
        _pickupText.text = stringToUpdate;
        yield return new WaitForSeconds(3);
        _pickupText.text = "";
    }
    
}
