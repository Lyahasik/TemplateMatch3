﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using ZombieVsMatch3.Gameplay.Match3.Services;

namespace ZombieVsMatch3.Gameplay.Match3
{
    [RequireComponent(typeof(Image))]
    public class FieldMatch3ActiveArea : MonoBehaviour, IPointerDownHandler, ISelectHandler, IDeselectHandler
    {
        private IExchangeOfStonesService _exchangeOfStonesService;
        
        public event Action<Vector3> OnDown;

        public void Construct(IExchangeOfStonesService exchangeOfStonesService)
        {
            _exchangeOfStonesService = exchangeOfStonesService;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(gameObject, eventData);
            
            OnDown?.Invoke(eventData.position);
        }

        public void OnSelect(BaseEventData eventData) {}

        public void OnDeselect(BaseEventData eventData)
        {
            _exchangeOfStonesService.Deselect();
        }
    }
}