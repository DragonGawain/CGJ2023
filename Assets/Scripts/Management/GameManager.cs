using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float currentTime = 960.0f;
    public float maxSeconds = 960.0f;
    public Slider timerDrainBar;
    public InGameUIManager inGameUIManager;
    private PlayerInputs inputs;
    public enum medalTiers
    {
        Gold = 1,
        Silver = 2,
        Bronze = 3,
        None = 4
    }
    public medalTiers currentTier = medalTiers.Gold;

    private void Awake()
    {
        inputs = new PlayerInputs();
        inputs.Player.Enable();
        inputs.Player.Pause.performed += inGameUIManager.OnPausePressed;
    }
    
    private void FixedUpdate()
    {
        if (currentTime > 0.0f)
        {
            var elapsedTimeSinceFrameUpdate = Time.deltaTime;
            currentTime -= elapsedTimeSinceFrameUpdate;
            var newSliderValue = Mathf.Clamp01(currentTime / maxSeconds);
            timerDrainBar.value = newSliderValue;

            EvaluateTier();
        }
    }

    private void EvaluateTier()
    {
        // Tiers: Gold 1 - .9 (0sec - 90sec elapsed)
        //          Silver: .9 - .78 (90sec - 210sec)
        //          Bronze: .78 - .62 (210sec - 360sec)
        var timeRatio = currentTime / maxSeconds;
        if (timeRatio >= .9)
        {
            currentTier = medalTiers.Gold;
        }
        else if (timeRatio < .9 && timeRatio >= .78)
        {
            currentTier = medalTiers.Silver;
        }
        else if (timeRatio < .78 && timeRatio >= .62)
        {
            currentTier = medalTiers.Bronze;
        }
        else
        {
            currentTier = medalTiers.None;
        }
    }

    public void ClearGame()
    {
        inGameUIManager.OnLevelClear(maxSeconds, currentTime, (int)currentTier);
    }
}
