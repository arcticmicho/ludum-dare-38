using System;

public class Observable<ObservedValue>
{
    private ObservedValue currentValue;

    /// <summary>
    /// Subscribe to this event to get notified when the value changes.
    /// </summary>
#pragma warning disable 0067
    public event Action<ObservedValue> OnValueChange;
    public event Action<ObservedValue, ObservedValue> OnValueChangeFull;
#pragma warning restore 0067

    public Observable()
    {
        currentValue = default(ObservedValue);
    }

    public Observable(ObservedValue initialValue)
    {
        currentValue = initialValue;
    }

    public ObservedValue Value
    {
        get { return currentValue; }

        set
        {
            if (!currentValue.Equals(value))
            {
                if(OnValueChangeFull != null)
                {
                    OnValueChangeFull(currentValue, value);
                }

                currentValue = value;

                if (OnValueChange != null)
                {
                    OnValueChange(value);
                }
            }
        }
    }

    /// <summary>
    /// Sets the value without notification.
    /// </summary>
    /// <param name="value">The value.</param>
    public void SetSilently(ObservedValue value)
    {
        currentValue = value;
    }
}