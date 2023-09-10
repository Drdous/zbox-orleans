using Orleans.Runtime;
using Orleans.Timers;
using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Grains;

// 8. Timers and Reminders: Vytvořte příklad grainu, který ukazuje, jak používat timers a reminders v Orleans.
public sealed class ReminderGrain : Grain, IReminderGrain, IRemindable
{
    private readonly IReminderRegistry _reminderRegistry;
    private IGrainReminder _reminder;

    private string _reminderMessage;
    private int _reminderReceivedCount;

    public ReminderGrain(IReminderRegistry reminderRegistry)
    {
        _reminderRegistry = reminderRegistry;
    }

    // For learn purposes to test grain lifecycle
    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        _reminderMessage = "Go learn Orleans!";
        return base.OnActivateAsync(cancellationToken);
    }
    
    public Task ReceiveReminder(string reminderName, TickStatus status)
    {
        _reminderReceivedCount++;
        return Task.CompletedTask;
    }
    
    public async Task SetReminderAsync(int seconds)
    {
        _reminder = await _reminderRegistry.RegisterOrUpdateReminder(
            callingGrainId: this.GetGrainId(),
            reminderName: "CodingReminder",
            dueTime: TimeSpan.FromSeconds(seconds),
            period: TimeSpan.FromSeconds(seconds));
    }
    
    public Task<string> GetReminderMessage()
    {
        return Task.FromResult(_reminderMessage);
    }
    
    public Task<int> GetReminderReceivedCount()
    {
        return Task.FromResult(_reminderReceivedCount);
    }

    public async Task UnregisterReminder()
    {
        await _reminderRegistry.UnregisterReminder(this.GetGrainId(), _reminder);
    }
}