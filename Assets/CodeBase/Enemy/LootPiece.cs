using System;
using System.Collections;
using SimpleRPG.Data;
using SimpleRPG.Infrastructure;
using TMPro;
using UnityEngine;

namespace SimpleRPG.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        public Action PickedUp;
        
        [SerializeField] private GameObject _model;
        [SerializeField] private GameObject _pickupFxPrefab;
        [SerializeField] private TextMeshPro _lootText;
        [SerializeField] private GameObject _pickupPopup;

        private Loot _loot;
        private bool _isPicked;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
        }

        public void Initialize(Loot loot)
        {
            _loot = loot;
        }

        private void OnTriggerEnter(Collider other)
        {
            PickUp();
        }

        private void PickUp()
        {
            if (_isPicked)
                return;

            _isPicked = true;
         
            PickedUp?.Invoke();
            
            _worldData.LootData.Collect(_loot);

            HideModel();
            PlayPickupFx();
            ShowText();
            StartCoroutine(DestroyCount());
        }

        private void HideModel()
        {
            _model.SetActive(false);
        }

        private IEnumerator DestroyCount()
        {
            var seconds = 1.5f;
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }

        private void PlayPickupFx()
        {
            Instantiate(_pickupFxPrefab, transform.position, Quaternion.identity);
        }

        private void ShowText()
        {
            _lootText.text = $"{_loot.Value}";
            _pickupPopup.SetActive(true);
        }
    }
}