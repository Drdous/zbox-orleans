namespace ZboxOrleans.Grains.States;

// Note for me: Better to prefer this Orleans serializer over the .NET Serializable.
[GenerateSerializer]
public sealed class BalanceState
{
    [Id(0)] // Not necessary to use ID attribute. But can be useful in performance critical or ordering scenarios
    public decimal Value { get; set; } = 1000;
}