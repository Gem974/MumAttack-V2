using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotShowerManager : MonoBehaviour
{
    public Image[] imagesSlots;
    public int currentSlot = 0;
    public Color emptySlotColor, fullSlotColor, SelectedSlotColor;

    public Transform _playerTransform;


    private void FixedUpdate()
    {

        transform.position = new Vector3(_playerTransform.position.x, transform.position.y, _playerTransform.position.z);

    }

public void AddSlotShower()
    {
        currentSlot++;

        //CollorSelected
        imagesSlots[currentSlot - 1].color = SelectedSlotColor;

        if (currentSlot > 1)
        {
            //Color Previous Selected
            imagesSlots[currentSlot - 2].color = fullSlotColor;
        }
    }

    public void SubSlotShower()
    {
        currentSlot--;

        if (currentSlot > 0)
        {
            imagesSlots[currentSlot - 1].color = SelectedSlotColor;
        }

        //DeColor Previous Selected
        imagesSlots[currentSlot].color = emptySlotColor;

        if (currentSlot <= 0 )
        {
            imagesSlots[0].color = emptySlotColor;
        }

    }

}
