﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Vexe.Runtime.Types;

namespace Hourai.SmashBrew.UI {

    public class PlayerInfoGUI : BaseBehaviour {
        
        [SerializeField]
        private List<GameObject> _displays;

        void Awake() {
            Match.OnMatchStart += OnMatchStart;
        }

        void OnDestroy() {
            Match.OnMatchStart -= OnMatchStart;
        }

        void OnMatchStart() {
            if(_displays.Count < SmashGame.MaxPlayers)
                Debug.LogWarning("There aren't enough displays registered on the PlayerInfoGUI script. Please add more until you have "+ SmashGame.MaxPlayers + " to work with.", this);

            List<RectTransform> transforms = new List<RectTransform>();

            var playerNumber = 0;
            var displayNumber = 0;

            while (playerNumber < SmashGame.MaxPlayers && displayNumber < _displays.Count) {
                Tuple<CharacterData, int> selection = Match.GetCharacterMatchData(playerNumber);
                GameObject display = _displays[displayNumber];

                // Ignore null displays
                if (display == null) {
                    displayNumber++;
                    continue;
                }

                // Ignore null characters
                if (selection.Item1 == null) {
                    playerNumber++;
                    continue;
                }

                display.SetActive(true);

                // Update the display with the Character information
                foreach (ICharacterGUIComponent component in display.GetComponentsInChildren<ICharacterGUIComponent>())
                    component.SetCharacterData(selection.Item1, selection.Item2, playerNumber);

                display.name = "Player " + (playerNumber + 1) + " Display";

                playerNumber++;
                displayNumber++;
            }

            // Destroy the excess displays
            while (displayNumber < _displays.Count) {
                GameObject display = _displays[displayNumber];
                if(display)
                    Destroy(display);
                displayNumber++;
            }
        }

    }

}

