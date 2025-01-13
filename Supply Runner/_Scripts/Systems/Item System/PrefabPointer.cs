using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* used to load resources outside of the resources folder.
* obviously we should just put that stuff in the resources folder
* to begin with, but this is a good band-aid fix for now.
*/

public class PrefabPointer : MonoBehaviour
{
    public GameObject prefab;
}
