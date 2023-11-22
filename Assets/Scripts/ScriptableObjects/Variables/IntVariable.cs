using UnityEngine;

[CreateAssetMenu(fileName = "Int Variable", menuName = "Variables/Int Variable")]
public class IntVariable : BaseVariable<int>
{
    public void ApplyChange(int p_amount)
    {
        _value += p_amount;
    }

    public void ApplyChange(IntVariable p_amount)
    {
        _value += p_amount.value;
    }
}
