using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatUI : MonoBehaviour {

    public GameObject iTextHolder, damageTextHolder, statTextHolder, iTextPrefab, dmgTexPrefab, statTextPrefab;

    public void UseIText(string input)
    {
        GameObject iText = (GameObject)Instantiate(iTextPrefab);
        iText.transform.SetParent(iTextHolder.transform);
        iText.transform.localPosition = Vector3.zero;
        iText.transform.localEulerAngles = Vector3.zero;
        iText.transform.localScale = Vector3.one;
        //iText.transform.position = Vector3.zero;
        iText.GetComponent<Text>().text = input;
    }

    public void UseDamageText(string input)
    {
        GameObject dmgText = (GameObject)Instantiate(dmgTexPrefab);
        dmgText.transform.SetParent(damageTextHolder.transform);
        dmgText.transform.localPosition = Vector3.zero;
        dmgText.transform.localEulerAngles = Vector3.zero;
        dmgText.transform.localScale = Vector3.one;
        dmgText.GetComponent<Text>().text = input;
    }

    public void UseStatText(string input)
    {
        GameObject statText = (GameObject)Instantiate(statTextPrefab);
        statText.transform.SetParent(statTextHolder.transform);
        statText.transform.localPosition = Vector3.zero;
        statText.transform.localEulerAngles = Vector3.zero;
        statText.transform.localScale = Vector3.one;
        statText.GetComponent<Text>().text = input;
    }
}
