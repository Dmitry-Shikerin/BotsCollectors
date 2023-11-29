using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDView : MonoBehaviour
{
    [SerializeField] private CommandCenterView _commandCenter;
    [SerializeField] private TMP_Text _resourcesExtractedCount;
    [SerializeField] private TMP_Text _resourcesFoundCount;
    [SerializeField] private TMP_Text _collectorsFreeCount;

    void Start()
    {
        string startValue = 0.ToString();

        _resourcesFoundCount.text = startValue;
        _resourcesExtractedCount.text = startValue;
        _collectorsFreeCount.text = startValue;
    }

    private void OnEnable()
    {
        // _commandCenter.ExtractedResourcesChanged += OnExtractedResourcesChanged;
        // _commandCenter.FoundedResourcesChanged += OnFoundedResourcesChanged;
        // _commandCenter.CollectorsFreeCountChanged += OnCollectorsFreeCountChanged;
    }

    private void OnDisable()
    {
        // _commandCenter.ExtractedResourcesChanged -= OnExtractedResourcesChanged;
        // _commandCenter.FoundedResourcesChanged -= OnFoundedResourcesChanged;
        // _commandCenter.CollectorsFreeCountChanged += OnCollectorsFreeCountChanged;
    }

    //TODO сделать для каждой кнопки много презенторов
    void Update()
    {
        
    }
    
    //TODO вьюшка не должна получать инты и конвертировать
    private void OnCollectorsFreeCountChanged(int collectorsCount)
    {
        _collectorsFreeCount.text = collectorsCount.ToString();
    }

    private void OnExtractedResourcesChanged(int resourcesCount)
    {
        _resourcesExtractedCount.text = resourcesCount.ToString();
    }
    
    private void OnFoundedResourcesChanged(int resourcesCount)
    {
        _resourcesFoundCount.text = resourcesCount.ToString();
    }
}
