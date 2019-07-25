using Budgetor.Models;

namespace Budgetor.Helpers.Delegates
{
    public delegate void ModalCloseDelegate(bool isUpdateRequired);

    public delegate void OpenTransactionModalDelegate(int? id);

    public delegate void UpdateScheduleDelegate(bool isDirty, Schedule_Base schedule);
}
