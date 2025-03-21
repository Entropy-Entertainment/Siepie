using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Player.Interaction
{

  public class PlayerInteractor : MonoBehaviour
  {
    public static event Action<GameObject, GameObject> PlayerInteract;

    public float InteractDistance;
    public List<IInteractable> InteractableObjects; //A list over all game objects -Henry

    bool controlLocked;
    List<IInteractable> nearbyInteractableObjects; //An interchanging list over all nearby interactable game objects -Henry

    private void Start()
    {
      controlLocked = false;
      nearbyInteractableObjects = new();
      InteractableObjects = new();
      InteractableObjects = IInteractable.GetAllInteractableItems();
    }

    //Function called when interact input from the Player Input component is received
    public void OnInteract()
    {
      if (!controlLocked)
      {
        nearbyInteractableObjects = InteractableObjects
        .Where(x => Vector3.Distance(((MonoBehaviour)x).transform.position, this.transform.position) < InteractDistance)
        .OrderBy(x => Vector3.Distance(((MonoBehaviour)x).transform.position, gameObject.transform.position)).ToList();

        if (nearbyInteractableObjects.Count >= 1)
          PlayerInteract?.Invoke(this.gameObject, ((MonoBehaviour)nearbyInteractableObjects[0]).gameObject);
        else
          Debug.Log(this.name + " found no object to interact with.");
        /*If distance between an object and the player is less than the interact distance, the object is added to a seperate list which will be used
        to invoke interact with more context after loop. -Henry   It's now in linq... felt like it -Nora*/
      }
    }

    public void LockInteract()
    {
      if (controlLocked) controlLocked = false;
      else controlLocked = true;
    }

#if UNITY_EDITOR

    //Fixed update that makes lines to all interactable objects and turns the lines green if a player is interact distance with them. -Henry
    //private void FixedUpdate()
    //{
    //  foreach (GameObject curObj in InteractableObjects)
    //  {
    //    if (Vector3.Distance(curObj.transform.position, this.transform.position) < InteractDistance)
    //    {
    //      Debug.DrawLine(curObj.transform.position, this.transform.position, Color.green);
    //    }
    //    else if (curObj == null)
    //    {
    //      InteractableObjects.Remove(curObj);
    //    }
    //    else
    //    {
    //      Debug.DrawLine(curObj.transform.position, this.transform.position, Color.red);
    //    }
    //  }

    //}

    private void OnDrawGizmos()
    {
      Gizmos.DrawSphere(transform.position, InteractDistance);
    }
#endif

  }
}