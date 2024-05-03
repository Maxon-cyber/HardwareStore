namespace HardwareStore.Entities.Order;

public enum Status
{
    InProcessing = 0,
    SubmittedForAssembly = 1,
    Sorted = 2,
    DeliveredToTheCourier = 3,
    Delivered = 4
}