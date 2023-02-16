using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class TestFeel : MonoBehaviour
{
    public MMFeedbacks LandingFeedback;

    // Start is called before the first frame update
    void Start()
    {
        LandingFeedback?.PlayFeedbacks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
