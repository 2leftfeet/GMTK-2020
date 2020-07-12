using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
      
      [InitializeOnLoad]


      public static class PlayStateNotifiers
      {    
          static void ModeChanged(PlayModeStateChange playModeState)
          {
              if (playModeState == PlayModeStateChange.EnteredEditMode) 
              {
                Debug.Log("Entered Edit mode.");
              }
          }
      }