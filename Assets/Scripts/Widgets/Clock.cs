using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{

    #region VARIABLE

    [Header("FILLING")]
    [SerializeField] [Range(0, 360)] private float _actualNeedleRotation = 0;
    [SerializeField] [Range(0, 1)] private float _actualFilling = 0;

    [Header("DISPLAY")]
    [SerializeField] private Transform _needle;
    [SerializeField] private Image _imageFilling;

    [Header("DEBUG")]
    [SerializeField] private bool _FillingToNeedle = true;

    #endregion

    #region ASCESSEUR

    private float ActualNeedleRotation { 
        get { return _actualNeedleRotation; }
        set
        {
            if (value > 360) { _actualNeedleRotation = 0; }
            else if (value < 0) { _actualNeedleRotation = 0; }
            else { _actualNeedleRotation = value; }
        }
    }

    public float ActualFilling { 
        get { return _actualFilling; } 
        set { 
            if (value > 1) { _actualFilling = 1; }
            else if (value < 0) { _actualFilling = 0; }
            else { _actualFilling = value; }

            if (_FillingToNeedle) { ActualNeedleRotation = ActualFilling * 360; }
        } 
    }

    #endregion

    #region EDITOR
#if UNITY_EDITOR

    private void OnValidate()
    {
        UpdateDisplay();
    }

#endif
    #endregion

    #region FUNCTION UNITY

    private void Update()
    {
        UpdateDisplay();
    }

    #endregion

    #region FUNCTION

    private void UpdateDisplay()
    {

        ActualFilling = ActualFilling;

        if (_imageFilling.fillClockwise) { _needle.eulerAngles = new Vector3(_needle.eulerAngles.x, _needle.eulerAngles.y, ActualNeedleRotation); }
        else { _needle.eulerAngles = new Vector3(_needle.eulerAngles.x, _needle.eulerAngles.y, -ActualNeedleRotation); }

        _imageFilling.fillAmount = ActualFilling;
    }

    #endregion
}
