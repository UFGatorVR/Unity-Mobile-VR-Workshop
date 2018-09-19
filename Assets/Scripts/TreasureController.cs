//Using -> namespace, certain functions and objects can be used in the script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Add EventSystems namespace
using UnityEngine.EventSystems;

//Class: Holder for variables and functions
//Function: Perform a procedure or task
public class TreasureController : MonoBehaviour {

    //Only current class will have access to methods and 
    //Component.renderer is renderer attached to game object
    private new Renderer renderer;
    public Material activeGaze;
    public Material inactiveGaze;

    public void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
    }

    //Set the material when gazed/ungazed
    public void SetMaterial (bool gaze)
    {
        if (gaze)
        {
            renderer.material = activeGaze;
        }
        else
        {
            renderer.material = inactiveGaze;
        }
    }

    //Change the location of the treasure
    public void ChangeLocation (BaseEventData data)
    {
        //Choose a random location for the new treasure
        int numberSiblings = transform.parent.childCount;
        int newSiblingIndex = (Random.Range(0, 3));
        //LATER: Do not select same index each time
        int currentSibling = transform.GetSiblingIndex();
        while (currentSibling == newSiblingIndex)
        {
            newSiblingIndex = (Random.Range(0, 3));
        }

        //Entity in Unity
        GameObject selectedObject = transform.parent.GetChild(newSiblingIndex).gameObject;
        Debug.Log(selectedObject);
        //Move the selected object to a random location around the player

        //Quaternion: Represents Rotations in
        //.Euler returns a rotation around the x, y, or z axis
        //Cannot turn Quaternion into Vector3, so multiply by Vector3.forward which casts (convert from
        //one type to another) it
        //Performs the rotation around the player
        Vector3 newPosition = Quaternion.Euler(0, Random.Range(-90, 90), 0) * Vector3.forward;

        //LATER: Too close to player
        //MAX is 3.5, MIN 1.5
        float distance = 1.5f + 2 * Random.value;
        newPosition *= distance;

        //Local Position, scales relative to the parent transform
        selectedObject.transform.localPosition = newPosition;
        renderer.material = inactiveGaze;
        gameObject.SetActive(false);
        selectedObject.SetActive(true);
    }
}
