using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unit.Lamp
{
   public class LampContainer : MonoBehaviour
   {
      [SerializeField] private List<GameObject> _lamps;

      [SerializeField] private Material _inactiveLamp;
      [SerializeField] private Material _activeLamp;

      private void Start()
      {
         TurnOffLamps();
      }

      public void TurnOnLamps()
      {
         foreach (var lamp in _lamps)
         {
            lamp.GetComponent<MeshRenderer>().material = _activeLamp;
         }
      }

      public void TurnOffLamps()
      {
         foreach (var lamp in _lamps)
         {
            lamp.GetComponent<MeshRenderer>().material = _inactiveLamp;
         }
      }
   }
}
