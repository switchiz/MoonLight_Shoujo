using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Mission_UI : MonoBehaviour
{
    TextMeshProUGUI progress;

    int currentProgress = 0;

    private void Awake()
    {
        Player player = FindAnyObjectByType<Player>();

        player.onProgress += RefreshProgress;

        progress = GetComponent<TextMeshProUGUI>();

        progress.text = "³²Àº ¿ä±« 0/3";
    }

    private void Update()
    {
       
        progress.text = $"³²Àº ¿ä±« {currentProgress}/3";
    }

    private void RefreshProgress(int progress)
    {
        currentProgress = progress;
    }
}
