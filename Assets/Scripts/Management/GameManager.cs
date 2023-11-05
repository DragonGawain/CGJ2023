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

    [SerializeField]
    private GameObject swapEffect;

    [SerializeField]
    private Transform effectContainer;

    public enum medalTiers
    {
        Gold = 1,
        Silver = 2,
        Bronze = 3,
        None = 4
    }

    public medalTiers currentTier = medalTiers.Gold;

    [SerializeField]
    GameObject lineObject;
    Line line;

    GameObject Cat,
        Rabbit;

    private void Awake()
    {
        inputs = new PlayerInputs();
        inputs.Player.Enable();
        inputs.Player.Pause.performed += inGameUIManager.OnPausePressed;
        inputs.Player.Swap.performed += swap;

        line = lineObject.GetComponent<Line>();
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

    private void swap(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        Cat = GameObject.FindWithTag("Cat");
        Rabbit = GameObject.FindWithTag("Rabbit");
        Vector3 temp = Cat.transform.position;
        Cat.transform.position = Rabbit.transform.position;
        Rabbit.transform.position = temp;
        line.swapPlaces();

        Instantiate(swapEffect, effectContainer);
    }
}
