using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{

    private void Start()
    {
        SceneManager.LoadScene("floor_1");
    }
}
