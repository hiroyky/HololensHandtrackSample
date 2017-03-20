using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR.WSA.Input;

public class HandPositionDetector : MonoBehaviour {

    public Text HandPositionText;
    public GameObject HandTrackObject;

    private void Awake() {
        InteractionManager.SourceDetected += InteractionManager_SourceDetected;
        InteractionManager.SourceUpdated += InteractionManager_SourceUpdated;
        InteractionManager.SourceLost += InteractionManager_SourceLost;
    }

    private void InteractionManager_SourceLost(InteractionSourceState state) {
        Debug.Log("SourceLost");
        HandPositionText.text = "Lost";
    }

    private void InteractionManager_SourceDetected(InteractionSourceState state) {
        Debug.Log("SourceDetected");        
    }

    void InteractionManager_SourceUpdated(InteractionSourceState state) {
        Debug.Log("SourceUpdated");
        if (state.source.kind != InteractionSourceKind.Hand) {
            return;
        }
        Vector3 handPosition;
        if (state.properties.location.TryGetPosition(out handPosition)) {
            string positionText = string.Format("{0:0.0000}, {1:0.0000}, {2:0.0000}", handPosition.x, handPosition.y, handPosition.z);
            HandPositionText.text = string.Format("id: {0}, pos: {1}", state.source.id, positionText);
            if (HandTrackObject != null) {
                HandTrackObject.transform.position = handPosition;
            }
        } else {
            HandPositionText.text = string.Format("id: {0}, pos: {1}", state.source.id, "unknown");
        }
    }

    void Start () {
		
	}
	
	
    void Update () {
		
	}
}
